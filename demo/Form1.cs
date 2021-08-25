using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_login_Click(object sender, EventArgs e)
        {
            string username = textBox_username.Text;
            string password = textBox_password.Text;
            //string username = "admin";
            //string password = "123456";

            string sql = "select * from user where username='" + username + "' and password='" + password + "'";
            int n = db.dbHelper.sqlIntHelper(sql);
            if (username == "" || password == "")
            {
                MessageBox.Show("用户名或密码不能为空！", "提示");
            }
            else if (n != 0)
            {
                //MessageBox.Show("登录成功！", "提示");
                this.Hide();
                Form2 f2 = new Form2();
                f2.ShowDialog();
            }
            else
            {
                MessageBox.Show("用户名或密码错误，请重新输入！", "提示");
                textBox_username.Text = "";
                textBox_password.Text = "";
                textBox_username.Focus();
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_clear_Click(object sender, EventArgs e)
        {
            textBox_username.Text = "";
            textBox_password.Text = "";

        }

        public void register(string username, string password)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                string sql = "update user set uaername='" + username + "' and password='" + password + "'";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

       

        public void writerIni()
        {
            int c=0;
            string fi = Application.StartupPath + "\\ini.txt";
            using (StreamWriter sw = new StreamWriter(fi.ToString(), false, Encoding.Default))
            {
                sw.WriteLine(c);
                sw.Flush(); //刷新缓冲区(覆盖)
                sw.Close();
            }
        }


        public  string readIni()
        {
            string path = Application.StartupPath + "\\ini.txt";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    string li = line.ToString();
                    return li;
                }
                sr.Close();
                Console.ReadLine();
            }
            return null;
        }

        private void textBox_password_TextChanged(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar=true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
