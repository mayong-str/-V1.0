using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace demo.db
{
    class dbHelper
    {
        static string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
        /// <summary>
        /// 查询结果,返回缓存表
        /// </summary>
        /// <param name="sql">执行查询的sql语句</param>
        /// <returns></returns>
        public static DataTable sqlHelper(string sql)
        {         
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                conn.Close();
                return ds.Tables[0];
            }        
        }

        /// <summary>
        /// 查询结果,返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int sqlIntHelper(string sql)
        {
            int n;           
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                n=sda.Fill(ds);
                conn.Close();
                return n;
            }
        }
        /// <summary>
        /// 新增、删除、修改操作
        /// </summary>
        /// <param name="sql"></param>
        public static void sqlOptionHelper(string sql)
        {
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
