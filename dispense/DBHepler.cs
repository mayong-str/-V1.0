using Log;
using NPOI.XSSF.UserModel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace dispense
{
    class DBHepler
    {
        static bool flag;
        /// <summary>
        /// 导出数据到csv
        /// </summary>
        /// <param name="airFileName"></param>
        /// <param name="sql1"></param>
        /// <param name="lastsql1"></param>
        public static void csvFile(string FileName, string sql1)
        {
            string a = "";
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql1, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                Console.WriteLine("V：" + v);
                for (int i = 0; i < v; i++)
                {
                    a += ds.Tables[0].Rows[i][0].ToString() + ',';
                }

                StreamWriter sw = new StreamWriter(FileName.ToString(), true, Encoding.Default);
                sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd") + ',' + DateTime.Now.ToString("HH:mm:ss") + ',' + a.ToString().Trim());
                sw.Flush();
                sw.Close();
                conn.Close();

            }
        }

        public static void csvFileValueType(string FileName, string sql1, string info, string value)
        {
            string sql = "select count(*) from config where type='" + info + "' and valueType=" + value + "";
            Console.WriteLine("输出查询总数：" + sql);
            int count = DBUtils.dbHelper(sql);
            Console.WriteLine("输出count：" + count);


            //创建文件  
            XSSFWorkbook wk = new XSSFWorkbook();

            //创建Excel工作表  
            var sheet = wk.CreateSheet("第一个Sheet"); //这个是工作簿名称

            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql1, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                Console.WriteLine("V：" + v);

                for (int k = 0; k < (v / count); k++)
                {
                    Console.WriteLine("输出k的值：" + k);
                    //创建单元格  
                    var row = sheet.CreateRow(k);//选择第1行
                    var cell = row.CreateCell(0);//选择第1列
                    cell.SetCellValue(DateTime.Now.ToString("yyyy/MM/dd"));
                    cell = row.CreateCell(1);//选择第2列
                    cell.SetCellValue(DateTime.Now.ToString("HH:mm:ss"));

                    for (int i = k * count; i < (k + 1) * count; i++)
                    {
                        Console.WriteLine("输出的值：" + ds.Tables[0].Rows[i][0].ToString().Trim());
                        cell = row.CreateCell(Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString().Trim()) + 1);//选择第1行
                        cell.SetCellValue(Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString().Trim()));//把0写进这个位置                                         
                    }
                }

                FileStream file = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None); //保存在这个路径，模式是创建                   
                wk.Write(file);
                file.Close();
                file.Dispose();
                conn.Close();
            }
        }



        public static void csvFileValue(string FileName, string sql1, string info)
        {
            string sql = "select count(*) from config where type='" + info + "'";
            Console.WriteLine("输出查询总数：" + sql);
            int count = DBUtils.dbHelper(sql);
            Console.WriteLine("输出count：" + count);
            //创建文件  
            XSSFWorkbook wk = new XSSFWorkbook();

            //创建Excel工作表  
            var sheet = wk.CreateSheet("第一个Sheet"); //这个是工作簿名称

            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql1, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                Console.WriteLine("V：" + v);

                for (int k = 0; k < (v / count); k++)
                {
                    Console.WriteLine("输出k的值：" + k);
                    //创建单元格  
                    var row = sheet.CreateRow(k);//选择第1行
                    var cell = row.CreateCell(0);//选择第1列
                    cell.SetCellValue(DateTime.Now.ToString("yyyy/MM/dd"));
                    cell = row.CreateCell(1);//选择第2列
                    cell.SetCellValue(DateTime.Now.ToString("HH:mm:ss"));

                    for (int i = k * count; i < (k + 1) * count; i++)
                    {
                        Console.WriteLine("输出的值：" + ds.Tables[0].Rows[i][0].ToString().Trim());
                        cell = row.CreateCell(Convert.ToInt32(ds.Tables[0].Rows[i][1].ToString().Trim()) + 1);//选择第1行
                        cell.SetCellValue(Convert.ToInt32(ds.Tables[0].Rows[i][0].ToString().Trim()));//把0写进这个位置                                         
                    }
                }

                FileStream file = new FileStream(FileName, FileMode.Create); //保存在这个路径，模式是创建                   
                wk.Write(file);
                file.Close();

                conn.Close();
            }
        }

        /// <summary>
        /// 每次导入配置文件时删除之前配置文件值
        /// </summary>
        /// <param name="sql"></param>
        public static void deleteConfigCsv(string sql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteReader();
                conn.Close();
            }
        }
        /// <summary>
        /// 从瞬时表将最近一次的记录插入到导出表
        /// </summary>
        /// <param name="sql">查询当前记录</param>
        public static void insertCurrentExport(string sql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = ds.Tables[0].Rows.Count;

                SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    for (int i = 0; i < count; i++)
                    {
                        cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "0");
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                    conn.Close();
                }
                catch (Exception ex)
                {
                    GlobalLog.WriteErrorLog(ex.ToString());
                    tr.Rollback();//回滚
                }
                conn.Clone();
            }
        }
        /// <summary>
        /// 从瞬时表将最近两次的记录差值插入到导出表
        /// </summary>
        /// <param name="currentsql">查询当前记录</param>
        /// <param name="lastcurrentsql">查询上次记录</param>
        public static void insertDifferenceExport(string currentsql, string lastcurrentsql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connstr))
                {
                    conn.Open();
                    SQLiteDataAdapter sda = new SQLiteDataAdapter(currentsql, conn);
                    SQLiteDataAdapter lastsda = new SQLiteDataAdapter(lastcurrentsql, conn);
                    DataSet ds = new DataSet();
                    DataSet lastds = new DataSet();
                    sda.Fill(ds);
                    lastsda.Fill(lastds);
                    int count = ds.Tables[0].Rows.Count;
                    int lastcount = lastds.Tables[0].Rows.Count;
                    if (lastcount != 0)
                    {
                        if (count == lastcount)
                        {
                            for (int k = 0; k < count; k++)
                            {
                                //判断localID是否修改
                                GlobalLog.WriteErrorLog("输出ds    的值：" + ds.Tables[0].Rows[k][3]);
                                GlobalLog.WriteErrorLog("输出lastds的值：" + lastds.Tables[0].Rows[k][3]);
                                if (int.Parse(ds.Tables[0].Rows[k][3].ToString()) - int.Parse(lastds.Tables[0].Rows[k][3].ToString()) == 0)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                    break; //跳出循环
                                }
                            }
                            GlobalLog.WriteErrorLog("输出flag的值：" + flag);
                            if (flag == true)
                            {
                                SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                                try
                                {
                                    SQLiteCommand cmd = new SQLiteCommand();
                                    for (int i = 0; i < count; i++)
                                    {
                                        string difference = (int.Parse(ds.Tables[0].Rows[i][5].ToString().Trim()) - int.Parse(lastds.Tables[0].Rows[i][5].ToString().Trim())).ToString().Trim();

                                        if (int.Parse(difference) < 0)  //流量计清零
                                        {
                                            difference = "0";
                                        }

                                        cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], difference, ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();
                                    }
                                    tr.Commit(); //把事务调用的更改保存到数据库中，事务结束
                                    conn.Close();
                                }
                                catch (Exception ex)
                                {
                                    GlobalLog.WriteErrorLog(ex.ToString());
                                    tr.Rollback();//回滚
                                }
                            }
                            else
                            {
                                SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                                try
                                {
                                    SQLiteCommand cmd = new SQLiteCommand();
                                    for (int i = 0; i < count; i++)
                                    {
                                        cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();
                                    }
                                    tr.Commit(); //把事务调用的更改保存到数据库中，事务结束
                                    conn.Close();
                                }
                                catch (Exception ex)
                                {
                                    GlobalLog.WriteErrorLog(ex.ToString());
                                    tr.Rollback();//回滚
                                }
                                GlobalLog.WriteInfoLog("前后两条记录的localID不一致！");
                            }
                            conn.Clone();
                        }
                        else
                        {
                            SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                            try
                            {
                                SQLiteCommand cmd = new SQLiteCommand();
                                for (int i = 0; i < count; i++)
                                {
                                    cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                    cmd.Connection = conn;
                                    cmd.ExecuteNonQuery();
                                }
                                tr.Commit(); //把事务调用的更改保存到数据库中，事务结束
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                GlobalLog.WriteErrorLog(ex.ToString());
                                tr.Rollback();//回滚
                            }
                            GlobalLog.WriteInfoLog("前后两条记录的长度不一致！");
                        }
                    }
                    else
                    {
                        SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                        try
                        {
                            SQLiteCommand cmd = new SQLiteCommand();
                            for (int i = 0; i < count; i++)
                            {
                                cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                            }
                            tr.Commit(); //把事务调用的更改保存到数据库中，事务结束
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            GlobalLog.WriteErrorLog(ex.ToString());
                            tr.Rollback();//回滚
                        }
                        GlobalLog.WriteInfoLog("查询的前一条记录不存在！");
                    }

                    conn.Clone();
                }
            }
            catch (Exception)
            {
                GlobalLog.WriteInfoLog("插入差值时异常！");
            }
        }
        /// <summary>
        /// 查询出配置表config中值类型valueType的值根据值类型导出瞬时值或差值
        /// </summary>
        /// <param name="sql">查询valueType的值</param>
        /// <returns></returns>
        public static string GetValeuType(string sql)
        {
            string str = "";
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int k = 0; k < v; k++)
                {
                    str = ds.Tables[0].Rows[0][0].ToString().Trim();

                }
                conn.Close();
                return str;

            }

        }

        public static string GetValeuType1(string sql)
        {
            string str1 = "";
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int k = 0; k < v; k++)
                {
                    str1 = ds.Tables[0].Rows[1][0].ToString().Trim();

                }
                conn.Close();
                return str1;

            }

        }
        /// <summary>
        /// 删除临时表export一月前记录
        /// </summary>
        /// <param name="sql"></param>
        public static void deleteExport(string sql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

}