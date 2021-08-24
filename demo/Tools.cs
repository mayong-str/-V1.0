using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;

namespace demo
{
    class Tools
    {
        private static ConfigAppSetting m_AppSettings;
        private static ConfigConnectionStrings m_ConnectionStrings;

        public static ConfigAppSetting AppSettings
        {
            get
            {
                if (m_AppSettings == null)
                {
                    m_AppSettings = new ConfigAppSetting();
                    m_AppSettings.AppSettingChanged += OnAppSettingChanged;
                }
                return m_AppSettings;
            }
        }
        public static ConfigConnectionStrings ConnectionStrings
        {
            get
            {
                if (m_ConnectionStrings == null)
                {
                    m_ConnectionStrings = new ConfigConnectionStrings();
                    m_ConnectionStrings.ConnectionStringsChanged += OnConnectionStringsChanged;
                }
                return m_ConnectionStrings;
            }
        }



        private static void OnAppSettingChanged(string name, string value)
        {
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (!File.Exists(configPath))
            {
                const string content = @"<?xml version=""1.0""?><configuration></configuration>";
                File.WriteAllText(configPath, content, Encoding.UTF8);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNode nodeConfiguration = doc.SelectSingleNode(@"configuration");
            if (nodeConfiguration == null)
            {
                nodeConfiguration = doc.CreateNode(XmlNodeType.Element, "configuration", string.Empty);
                doc.AppendChild(nodeConfiguration);
            }

            XmlNode nodeAppSettings = nodeConfiguration.SelectSingleNode(@"appSettings");
            if (nodeAppSettings == null)
            {
                nodeAppSettings = doc.CreateNode(XmlNodeType.Element, "appSettings", string.Empty);
                if (!nodeConfiguration.HasChildNodes)
                    nodeConfiguration.AppendChild(nodeAppSettings);
                else
                {
                    //configSections 必须放在 第一个, 所以得 避开 configSections
                    XmlNode firstNode = nodeConfiguration.ChildNodes[0];
                    bool firstNodeIsSections = string.Equals(firstNode.Name, "configSections", StringComparison.CurrentCultureIgnoreCase);

                    if (firstNodeIsSections)
                        nodeConfiguration.InsertAfter(nodeAppSettings, firstNode);
                    else
                        nodeConfiguration.InsertBefore(nodeAppSettings, firstNode);
                }
            }

            string xmlName = FormatXmlStr(name);
            XmlNode nodeAdd = nodeAppSettings.SelectSingleNode(@"add[@key='" + xmlName + "']");
            if (nodeAdd == null)
            {
                nodeAdd = doc.CreateNode(XmlNodeType.Element, "add", string.Empty);
                nodeAppSettings.AppendChild(nodeAdd);
            }

            XmlElement nodeElem = (XmlElement)nodeAdd;
            nodeElem.SetAttribute("key", name);
            nodeElem.SetAttribute("value", value);
            doc.Save(configPath);

            try { ConfigurationManager.RefreshSection("appSettings"); } catch (Exception) { }
        }
        private static void OnConnectionStringsChanged(string name, string value)
        {
            string configPath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            if (!File.Exists(configPath))
            {
                const string content = @"<?xml version=""1.0""?><configuration></configuration>";
                File.WriteAllText(configPath, content, Encoding.UTF8);
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(configPath);

            XmlNode nodeConfiguration = doc.SelectSingleNode(@"configuration");
            if (nodeConfiguration == null)
            {
                nodeConfiguration = doc.CreateNode(XmlNodeType.Element, "configuration", string.Empty);
                doc.AppendChild(nodeConfiguration);
            }

            XmlNode nodeAppSettings = nodeConfiguration.SelectSingleNode(@"appSettings");
            XmlNode nodeConnectionStrings = nodeConfiguration.SelectSingleNode(@"connectionStrings");
            if (nodeConnectionStrings == null)
            {
                nodeConnectionStrings = doc.CreateNode(XmlNodeType.Element, "connectionStrings", string.Empty);
                if (!nodeConfiguration.HasChildNodes)
                    nodeConfiguration.AppendChild(nodeConnectionStrings);
                else
                {
                    //优先将 connectionStrings 放在 appSettings 后面
                    if (nodeAppSettings != null)
                        nodeConfiguration.InsertAfter(nodeConnectionStrings, nodeAppSettings);
                    else
                    {
                        //如果 没有 appSettings 节点, 则 configSections 必须放在 第一个, 所以得 避开 configSections
                        XmlNode firstNode = nodeConfiguration.ChildNodes[0];
                        bool firstNodeIsSections = string.Equals(firstNode.Name, "configSections", StringComparison.CurrentCultureIgnoreCase);

                        if (firstNodeIsSections)
                            nodeConfiguration.InsertAfter(nodeConnectionStrings, firstNode);
                        else
                            nodeConfiguration.InsertBefore(nodeConnectionStrings, firstNode);
                    }
                }
            }

            string xmlName = FormatXmlStr(name);
            XmlNode nodeAdd = nodeConnectionStrings.SelectSingleNode(@"add[@name='" + xmlName + "']");
            if (nodeAdd == null)
            {
                nodeAdd = doc.CreateNode(XmlNodeType.Element, "add", string.Empty);
                nodeConnectionStrings.AppendChild(nodeAdd);
            }

            XmlElement nodeElem = (XmlElement)nodeAdd;
            nodeElem.SetAttribute("name", name);
            nodeElem.SetAttribute("connectionString", value);
            doc.Save(configPath);

            try
            {
                ConfigurationManager.RefreshSection("connectionString");  //RefreshSection 无法刷新 connectionString 节点
                FieldInfo fieldInfo = typeof(ConfigurationManager).GetField("s_initState", BindingFlags.NonPublic | BindingFlags.Static);
                if (fieldInfo != null) fieldInfo.SetValue(null, 0);       //将配置文件 设置为: 未分析 状态, 配置文件 将会在下次读取 时 重新分析.
            }
            catch (Exception) { }
        }

        private static string FormatXmlStr(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            string result = value
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("&", "&amp;")
                .Replace("'", "&apos;")
                .Replace("\"", "&quot;");
            return result;
            //&lt; < 小于号 
            //&gt; > 大于号 
            //&amp; & 和 
            //&apos; ' 单引号 
            //&quot; " 双引号 
        }


        public class ConfigAppSetting
        {
            private readonly InnerIgnoreDict<string> m_Hash = new InnerIgnoreDict<string>();

            public string this[string name]
            {
                get
                {
                    string value = m_Hash[name];
                    if (string.IsNullOrWhiteSpace(value))
                    {
                        try { value = ConfigurationManager.AppSettings[name]; } catch (Exception) { }
                        m_Hash[name] = value;
                        return value;
                    }
                    return value;
                }
                set
                {
                    m_Hash[name] = value;
                    try { ConfigurationManager.AppSettings[name] = value; } catch (Exception) { }
                    if (AppSettingChanged != null) AppSettingChanged(name, value);
                }
            }
            public AppSettingValueChanged AppSettingChanged;

            public delegate void AppSettingValueChanged(string name, string value);
        }
        public class ConfigConnectionStrings
        {
            private readonly InnerIgnoreDict<ConnectionStringSettings> m_Hash = new InnerIgnoreDict<ConnectionStringSettings>();

            public string this[string name]
            {
                get
                {
                    ConnectionStringSettings value = m_Hash[name];
                    if (value == null || string.IsNullOrWhiteSpace(value.ConnectionString))
                    {
                        try { value = ConfigurationManager.ConnectionStrings[name]; } catch (Exception) { }
                        m_Hash[name] = value;
                        return value == null ? string.Empty : value.ConnectionString;
                    }
                    return value.ConnectionString;
                }
                set
                {

                    ConnectionStringSettings setting = new ConnectionStringSettings();
                    setting.Name = name;
                    setting.ConnectionString = value;
                    m_Hash[name] = setting;
                    //try { ConfigurationManager.ConnectionStrings[name] = setting; } catch (Exception) { }
                    if (ConnectionStringsChanged != null) ConnectionStringsChanged(name, value);
                }
            }
            public ConnectionStringsValueChanged ConnectionStringsChanged;

            public delegate void ConnectionStringsValueChanged(string name, string value);
        }





        private class InnerIgnoreDict<T> : Dictionary<string, T>
        {
            public InnerIgnoreDict() : base(StringComparer.CurrentCultureIgnoreCase)
            {
            }

            #if (!WindowsCE && !PocketPC)
            public InnerIgnoreDict(SerializationInfo info, StreamingContext context) : base(info, context) { }
            #endif

            private readonly object getSetLocker = new object();
            private static readonly T defaultValue = default(T);

            public new T this[string key]
            {
                get
                {
                    if (key == null) return defaultValue;
                    lock (getSetLocker) //为了 多线程的 高并发, 取值也 加上 线程锁
                    {
                        T record;
                        if (TryGetValue(key, out record)) return record;
                        else return defaultValue;
                    }
                }
                set
                {
                    try
                    {
                        if (key != null)
                        {
                            lock (getSetLocker)
                            {
                                //if (!value.Equals(default(T)))
                                //{
                                if (base.ContainsKey(key)) base[key] = value;
                                else base.Add(key, value);
                                //}
                                //else
                                //{
                                //    base.Remove(key);
                                //}
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
        }
    }
}
