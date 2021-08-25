using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace demo
{
    class ExcelProduction
    {

        //定义配置文件路径
        static string dbConfigPath = System.Environment.CurrentDirectory + @"\config\DBConfig.xml";
        static XDocument xDoc = XDocument.Load(dbConfigPath);

        //获取配置文件中的连接元素
        static string address = xDoc.Element("configuration").Element("DBSetting").Element("address").Value;
        static string dbName = xDoc.Element("configuration").Element("DBSetting").Element("dbName").Value;
        static string username = xDoc.Element("configuration").Element("DBSetting").Element("username").Value;
        static string password = xDoc.Element("configuration").Element("DBSetting").Element("password").Value;
        static string inTableName = xDoc.Element("configuration").Element("DBSetting").Element("tableName").Element("inTableName").Value;
        static string outTableName = xDoc.Element("configuration").Element("DBSetting").Element("tableName").Element("outTableName").Value;


        //拼接连接字符串
        //设置超时时间
        //string conn_str = "Data Source=" + address + ";User Id=" + username + ";Password=" + password;
        string conn_str = "server =" + address + ";uid = sa;pwd = 123456;database = " + dbName + ";";
        //测试字符串
        //string conn_str = "server = 127.0.0.1;uid = sa;pwd = 123456;database = SA1;";


        public void importProductionExcel()
        {
            //打开文件
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            file.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);  //打开时初始位置设置为桌面
            file.RestoreDirectory = true;
            if (file.ShowDialog() == DialogResult.OK)
            {
                string path = file.FileName; //路径

                //判断文件是否符合格式
                string fileSuffix = System.IO.Path.GetExtension(path);  //扩展名
                if (string.IsNullOrEmpty(fileSuffix))
                    MessageBox.Show("所选文件没有后缀名,文件格式不符合哦");

                //存放读取到的excel数据
                DataSet ds = new DataSet();

                //判断Excel文件是2003版本还是2007版本
                string connString = "";
                if (fileSuffix == ".xls")
                {
                    //SystemLog.WriteLogLine("1");
                    connString = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                }
                else
                {
                    //SystemLog.WriteLogLine("2");
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";
                }

                string tableName = "";   //表名
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    //SystemLog.WriteLogLine("3");
                    conn.Open();
                    DataTable table = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
                    tableName = table.Rows[0]["Table_Name"].ToString();
                    //SystemLog.WriteLogLine("tableName:" + tableName);
                    string strExcel = "select * from " + "[" + tableName + "]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(strExcel, connString);
                    adapter.Fill(ds, tableName);
                    conn.Close();
                }

                //将ds插入到数据表中
                //readExcel(ds);
            }
        }

        
       




        /*
        /// <summary>
        /// 插入到产品数据表中
        /// </summary>
        /// <param name="productionList"></param>
        private void insertProductTable(List<Production> productionList)
        {
            string sql = "insert into [dbo].[TM_DY_Product](Product_ID,Product_Name,Product_Mark,Product_Type) values";
            for (int i = 0; i < productionList.Count; i++)
            {
                sql += ("('" + productionList[i].Product_ID + "','" + productionList[i].Product_Name + "','" + productionList[i].Product_Mark + "','" + productionList[i].Product_Type + "')");
                if (i < productionList.Count - 1)
                {
                    sql += ",";
                }
            }
            //SystemLog.WriteLogLine("插入到产品数据表中:" + sql);

            SqlConnection conn = new SqlConnection(conn_str);
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();
                //SystemLog.WriteLogLine("插入产品数据表成功!");
                MessageBox.Show("导入成功!");
            }
            catch (Exception ex)
            {
                //SystemLog.WriteLogLine("插入产品数据表异常" + ex);
                MessageBox.Show("导入失败" + ex);
            }
            finally
            {
                //如果连接没有关闭，先关闭再进行下面的查询操作
                if (conn != null || conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        */


        /*
        /// <summary>
        /// 清除数据和导入excel的事务方法
        /// </summary>
        /// <param name="productionList"></param>
        private void truncateAndInsertProductTableTran(List<Production> productionList)
        {
            string sql_truncate = "truncate table [dbo].[TM_DY_Product];";
            //SystemLog.WriteLogLine("清除数据表:" + sql_truncate);

            string sql_insert = "insert into [dbo].[TM_DY_Product](Product_ID,Product_Name,Product_Mark,Product_Type) values";
            for (int i = 0; i < productionList.Count; i++)
            {
                sql_insert += ("('" + productionList[i].Product_ID + "','" + productionList[i].Product_Name + "','" + productionList[i].Product_Mark + "','" + productionList[i].Product_Type + "')");
                if (i < productionList.Count - 1)
                {
                    sql_insert += ",";
                }
            }
            //SystemLog.WriteLogLine("插入到产品数据表中:" + sql_insert);

            string sql = sql_truncate + "\n" + sql_insert; //中间加个换行符

            using (SqlConnection conn = new SqlConnection(conn_str))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlTransaction MyTran = conn.BeginTransaction();  //开始事务

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = MyTran;
                    cmd.CommandText = sql;  //语句
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MyTran.Commit();   //提交事务
                        //SystemLog.WriteLogLine("插入产品数据表成功!");
                        MessageBox.Show("导入成功!");
                    }
                    catch (Exception ex)
                    {
                        MyTran.Rollback();
                        MessageBox.Show("导入失败,回滚事务!");
                        //SystemLog.WriteLogLine("导入失败,回滚事务:" + ex);
                    }
                }
                catch (Exception ex)
                {
                    //SystemLog.WriteLogLine("插入产品数据表异常" + ex);
                    MessageBox.Show("导入失败" + ex);
                }
                finally
                {
                    //如果连接没有关闭，先关闭再进行下面的查询操作
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
        */

        /// <summary>
        /// 清空产品表
        /// </summary>
        private void truncatProductionTable()
        {
            string sql = "truncate table [dbo].[TM_DY_Product];";
            SqlConnection conn = new SqlConnection(conn_str);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                //SystemLog.WriteLogLine("清空TM_DY_Product表成功");
            }
            catch (Exception ex)
            {
                //SystemLog.WriteLogLine("清空TM_DY_Product表成功异常:" + ex);
                MessageBox.Show("导入失败!" + ex);
            }
            finally
            {
                //如果连接没有关闭，先关闭再进行下面的查询操作
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        /*
        /// <summary>
        /// 查询产品数据表
        /// </summary>
        private List<Production> selectProductTable()
        {
            //表名列表
            //List<Production> productionList = new List<Production>();

            SqlConnection conn = new SqlConnection(conn_str);
            string sql = "select * from TM_DY_Product;";
            //SystemLog.WriteLogLine("查询产品数据表:" + sql);
            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    //SystemLog.WriteLogLine("查询到的产品:");
                    while (reader.Read())
                    {
                        //SystemLog.WriteLogLine(reader[0].ToString());
                        //SystemLog.WriteLogLine(reader[1].ToString());
                        //SystemLog.WriteLogLine(reader[2].ToString());
                        //SystemLog.WriteLogLine(reader[3].ToString());

                        //Production production = new Production();
                        //production.Product_ID = reader["Product_ID"].ToString();
                        //production.Product_Type = reader["Product_Type"].ToString();
                        //production.Product_Name = reader["Product_Name"].ToString();
                        //production.Product_Mark = reader["Product_Mark"].ToString();

                        //productionList.Add(production);
                    }
                    reader.Close();
                    return productionList;
                }
                return null;
            }
            catch (Exception ex)
            {
                //SystemLog.WriteLogLine("产品数据表异常:" + ex);
                return null;
            }
            finally
            {
                //如果连接没有关闭，先关闭再进行下面的查询操作
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
          */
    }

}



