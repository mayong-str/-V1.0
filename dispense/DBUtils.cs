using System.Configuration;
using System.Data;
using System.Data.SQLite;

namespace dispense
{
    class DBUtils
    {
        /// <summary>
        /// 执行sql语句返回总数count
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int dbHelper(string sql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;

            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int count = int.Parse(ds.Tables[0].Rows[0][0].ToString());
                conn.Close();
                return count;
            }

        }
    }
}
