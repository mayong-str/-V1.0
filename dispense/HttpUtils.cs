using Log;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace dispense
{
    class HttpUtils
    {

        /// <summary>
        /// 根据发送的http请求返回对应字符串
        /// </summary>
        /// <param name="Url">IP地址</param>
        /// <returns></returns>
        public static string Get(string Url)
        {
            System.GC.Collect();
            /*超时中断解决方法
             * 1、 request.KeepAlive = false;关闭长连接
             * 2、关闭request\response\myStream\mystreamReader流
             * 3、request.Timeout = 5 * 60 * 1000; 网路响应慢导致超时
             * 4、defaultconnectionlimit修改默认值
             * 5、垃圾回收
             */
            GlobalLog.WriteInfoLog("Get请求实现开始------");
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.Proxy = null;
            request.KeepAlive = false;  //长连接
            request.ContentType = "application/x-www-form-urlencoded";
            request.AutomaticDecompression = DecompressionMethods.GZip;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myStream = response.GetResponseStream();

            StreamReader mystreamReader = new StreamReader(myStream, Encoding.UTF8);

            string str = mystreamReader.ReadLine();
            mystreamReader.Close();
            myStream.Close(); //关闭流

            if (response != null)
            {
                response.Close();
            }

            if (request != null)
            {
                request.Abort();
            }
            return str;

        }
        /// <summary>
        /// 转化json字符串格式
        /// </summary>
        /// <param name="str">json字符串</param>
        /// <returns></returns>
        public static string ConvertJsonString(string str)
        {
            GlobalLog.WriteInfoLog("转化Json字符串开始------");
            JsonSerializer serializer = new JsonSerializer();
            TextReader reader = new StringReader(str);
            JsonTextReader jsonreader = new JsonTextReader(reader);
            object ob = serializer.Deserialize(jsonreader);
            if (ob != null)
            {
                StringWriter writer = new StringWriter();
                JsonTextWriter jsonWriter = new JsonTextWriter(writer)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 4,  //缩进
                    IndentChar = ' '  //空格
                };
                serializer.Serialize(jsonWriter, ob);
                return writer.ToString();
            }
            return str;

        }
        /// <summary>
        /// 从json对象中获取指定localID的dataValue值 
        /// </summary>
        /// <param name="strjson"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string GetJsonValue(string strjson, int i)
        {
            GlobalLog.WriteInfoLog("从json字符串获值开始------");
            string value1 = "";
            JArray array = new JArray();
            JObject jo = JObject.Parse(strjson);
            for (int k = 0; k < i; k++)
            {
                //测量点为""
                string measureData = jo["body"]["point"][k]["measureData"].ToString().Trim();
                if (measureData == "")
                {
                    string value3 = Convert.ToString("0");
                    array.Add(value3);

                }
                else
                {
                    //测量点的值为""
                    string dataValue = jo["body"]["point"][k]["measureData"]["dataValue"].ToString().Trim();
                    if (dataValue == "")
                    {
                        string value3 = Convert.ToString("0");
                        array.Add(value3);
                    }
                    else
                    {
                        string value3 = jo["body"]["point"][k]["measureData"]["dataValue"].ToString().Trim();

                        array.Add(value3);
                    }
                }
            }
            foreach (string v in array)
            {
                value1 = value1 + v.ToString() + ',';
            }
            return value1;
        }

        public static string GetJsonValue1(string str, int i)
        {
            string value4 = "";
            JObject jo = JObject.Parse(str);
            for (int b = 0; b < i; b++)
            {
                JArray array = new JArray();
                int value2 = Convert.ToInt32(jo["body"]["point"][b]["measureData"]["dataValue"].ToString().Trim());

                if (value2 == 0)
                {
                    value2 = 0;
                    array.Add(value2);
                }
                else
                {
                    string value3 = jo["body"]["point"][b]["measureData"]["dataValue"].ToString().Trim();
                    array.Add(value3);
                }

                foreach (string v in array)
                {
                    value4 = value4 + v.ToString() + ',';
                }
            }
            return value4;
        }

        /// <summary>
        /// 发送类似'http://192.168.1.20'判断是否连接上服务器
        /// </summary>
        /// <param name="Url">IP地址</param>
        /// <returns></returns>
        public static bool GetConnection(string Url)
        {
            System.GC.Collect();
            System.Net.ServicePointManager.DefaultConnectionLimit = 200;
            GlobalLog.WriteInfoLog("服务器响应前检测时间" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            //request.Timeout = 6000; 超时时间

            request.Proxy = null;
            request.KeepAlive = false;
            request.ContentType = "application/x-www-form-urlencoded";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();

            if (resp != null)
            {
                resp.Close();
            }

            if (request != null)
            {
                request.Abort();
            }

            if (resp.StatusDescription.ToString().ToUpper() == "OK") //如果服务器响应
            {
                Console.WriteLine("服务器响应状态值：" + resp.StatusDescription.ToString().ToUpper());
                GlobalLog.WriteInfoLog("服务器响应成功后检测时间" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                return true;
            }
            else
            {
                GlobalLog.WriteInfoLog("服务器响应未成功后检测时间" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                return false;
            }
        }
    }
}




