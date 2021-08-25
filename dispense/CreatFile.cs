using System;
using System.IO;

namespace dispense
{
    class CreatFile
    {
        /// <summary>
        /// 手动创建Air/Gas/Power/Water的csv
        /// </summary>
        /// <param name="file">AIR/GAS/POWER/WATER远程文件夹路径</param>
        /// <param name="defaultfile">本地备份路径</param>
        /// <param name="sql">查询当前记录</param>
        /// <param name="defaultSql">上一条记录</param>
        public static void shoudongCreatFile(string file, string defaultfile, string sql)
        {
            //string date = "00:00:00"; //设置基准时间
            int hour = Convert.ToInt32(DateTime.Now.Hour.ToString());
            int minute = Convert.ToInt32(DateTime.Now.Minute.ToString());
            string info = new DirectoryInfo(file).Name; //获取当前路径最后一级文件夹名称
            string defaultinfo = new DirectoryInfo(defaultfile).Name;
            if (hour < 10 && minute < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);

                stream.Close();
                DBHepler.csvFile(stream.Name, sql);

                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + "0" + hour + "" + "0" + minute + "00"));
                FileStream defaultstream = File.Create(defaultfileName);

                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (hour < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);

                stream.Close();
                DBHepler.csvFile(stream.Name, sql);

                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + "0" + hour + "" + minute + "00"));
                FileStream defaultstream = File.Create(defaultfileName);

                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (minute < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);

                stream.Close();
                DBHepler.csvFile(stream.Name, sql);

                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + hour + "" + "0" + minute + "00"));
                FileStream defaultstream = File.Create(defaultfileName);

                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (hour >= 10 && minute >= 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);

                stream.Close();
                DBHepler.csvFile(stream.Name, sql);

                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + hour + "" + minute + "00"));
                FileStream defaultstream = File.Create(defaultfileName);

                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }

        }
        /// <summary>
        /// 定时导出配置表对应值
        /// </summary>
        /// <param name="file">远程路径</param>
        /// <param name="defaultfile">本地路径</param>
        /// <param name="sql">查询语句</param>
        public static void dingshiCreatFile(string file, string defaultfile, string sql)
        {
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            int M = minute;

            if (M < 10)
            {
                M = 0;
            }
            else
            {
                M = DateTime.Now.Minute / 10;
            }

            string info = new DirectoryInfo(file).Name; //获取当前路径最后一级文件夹名称
            string defaultinfo = new DirectoryInfo(defaultfile).Name;
            if (hour < 10 && minute < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + "0" + M * 10 + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFile(stream.Name, sql);


                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + "0" + hour + "" + "0" + M * 10 + "00"));
                FileStream defaultstream = File.Create(defaultfileName);
                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (hour < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + M * 10 + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFile(stream.Name, sql);


                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + "0" + hour + "" + M * 10 + "00"));
                FileStream defaultstream = File.Create(defaultfileName);
                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (minute < 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + "0" + M * 10 + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFile(stream.Name, sql);


                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + hour + "" + "0" + M * 10 + "00"));
                FileStream defaultstream = File.Create(defaultfileName);
                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
            else if (hour >= 10 && minute >= 10)
            {
                string fileName = string.Format(@"" + file + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + M * 10 + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFile(stream.Name, sql);


                string defaultfileName = string.Format(@"" + defaultfile + "\\{0}.csv", DateTime.Now.ToString("" + defaultinfo + "_yyyyMMdd_" + hour + "" + M * 10 + "00"));
                FileStream defaultstream = File.Create(defaultfileName);
                defaultstream.Close();
                DBHepler.csvFile(defaultstream.Name, sql);
            }
        }
        /// <summary>
        /// 汇总导出瞬时值(0)或差值(1)
        /// </summary>
        /// <param name="path">保存文件路径</param>
        /// <param name="sql">查询语句</param>
        /// <param name="value">瞬时值(0)或差值(1)</param>
        public static void OutRecordValueType(string path, string sql, string value)
        {
            //string date = "00:00:00"; //设置基准时间
            int hour = Convert.ToInt32(DateTime.Now.Hour.ToString());
            int minute = Convert.ToInt32(DateTime.Now.Minute.ToString());
            string info = new DirectoryInfo(path).Name; //获取当前路径最后一级文件夹名称           
            if (hour < 10 && minute < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValueType(stream.Name, sql, info, value);

            }
            else if (hour < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValueType(stream.Name, sql, info, value);

            }
            else if (minute < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValueType(stream.Name, sql, info, value);

            }
            else if (hour >= 10 && minute >= 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValueType(stream.Name, sql, info, value);

            }
        }
        /// <summary>
        /// 汇总导出瞬时值和差值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="sql"></param>
        public static void outRecord(string path, string sql)
        {
            //string date = "00:00:00"; //设置基准时间
            int hour = Convert.ToInt32(DateTime.Now.Hour.ToString());
            int minute = Convert.ToInt32(DateTime.Now.Minute.ToString());
            string info = new DirectoryInfo(path).Name; //获取当前路径最后一级文件夹名称  
            Console.WriteLine("输出文件夹类型：" + info);
            if (hour < 10 && minute < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValue(stream.Name, sql, info);

            }
            else if (hour < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + "0" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValue(stream.Name, sql, info);

            }
            else if (minute < 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + "0" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValue(stream.Name, sql, info);

            }
            else if (hour >= 10 && minute >= 10)
            {
                string fileName = string.Format(@"" + path + "\\{0}.csv", DateTime.Now.ToString("" + info + "_yyyyMMdd_" + hour + "" + minute + "00"));
                FileStream stream = File.Create(fileName);
                stream.Close();
                DBHepler.csvFileValue(stream.Name, sql, info);

            }
        }
    }



}
