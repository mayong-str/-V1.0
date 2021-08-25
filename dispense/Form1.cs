using Log;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace dispense
{
    public partial class Form1 : Form
    {
        static string fipath = writerpath();
        static string path = fipath;
        static string airpath = "" + path + "\\AIR";
        static string gaspath = "" + path + "\\GAS";
        static string powerpath = "" + path + "\\POWER";
        static string waterpath = "" + path + "\\WATER";
        static int t = Convert.ToInt32(writertime());

        static string defaultfipath = defaultwriterpath();
        static string defaultpath = defaultfipath;
        static string defaultairpath = "" + defaultpath + "\\AIR";
        static string defaultgaspath = "" + defaultpath + "\\GAS";
        static string defaultpowerpath = "" + defaultpath + "\\POWER";
        static string defaultwaterpath = "" + defaultpath + "\\WATER";
        public Form1()
        {
            InitializeComponent();
            //跨线程问题
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;//设置该属性 为false
            groupBox1.Paint += groupBox1_Paint;
            groupBox2.Paint += groupBox2_Paint;
            label4.Text = "程序已启动！";
            label3.Text = "采集未开始！";
            this.textBox_Value.Visible = true;
            textBox_Date.Visible = false;
            textBox_servername.Visible = false;
            textBox_serialnumber.Visible = false;
            textBox_type.Visible = false;
            textBox_localID.Visible = false;
            textBox_ID.Visible = false;
            this.textBox_Value.Visible = false;
            textBox1.Visible = false;
            textBox1_LocalID.Visible = false;
            textBox_GetValue.Visible = false;
            textBox_GetNull.Visible = false;
            textBox_fuwuqi.Visible = false;
            this.button_Clear.Visible = false;
            this.textBox_Time.Text = Convert.ToString(t);
            textBox_valueType.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            groupBox2.Visible = false;
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gBox = (GroupBox)sender;

            e.Graphics.Clear(gBox.BackColor);
            e.Graphics.DrawString(gBox.Text, gBox.Font, Brushes.Black, 10, 1);
            var vSize = e.Graphics.MeasureString(gBox.Text, gBox.Font);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 8, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, vSize.Width + 8, vSize.Height / 2, gBox.Width - 2, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 1, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, gBox.Height - 2, gBox.Width - 2, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, gBox.Width - 2, vSize.Height / 2, gBox.Width - 2, gBox.Height - 2);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            GroupBox gBox = (GroupBox)sender;

            e.Graphics.Clear(gBox.BackColor);
            e.Graphics.DrawString(gBox.Text, gBox.Font, Brushes.Black, 10, 1);
            var vSize = e.Graphics.MeasureString(gBox.Text, gBox.Font);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 8, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, vSize.Width + 8, vSize.Height / 2, gBox.Width - 2, vSize.Height / 2);
            e.Graphics.DrawLine(Pens.Black, 1, vSize.Height / 2, 1, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, gBox.Height - 2, gBox.Width - 2, gBox.Height - 2);
            e.Graphics.DrawLine(Pens.Black, gBox.Width - 2, vSize.Height / 2, gBox.Width - 2, gBox.Height - 2);
        }

        /// <summary>
        /// 导入csv格式配置文件插入数据库并显示在DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Open_Click(object sender, EventArgs e)
        {
            string sql = "delete from config";  //导入新的配置文件时删除以前的配置文件
            DBHepler.deleteConfigCsv(sql);
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;

            dialog.Title = "请选择文件夹";
            dialog.Filter = "所有文件(*.csv)|*.csv";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string file = dialog.FileName;
                FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //新建一个datatable用于保存读入的数据 
                DataTable dt = new DataTable();
                //给datatable添加列  
                dt.Columns.Add("输出文件序列 ", typeof(String));
                dt.Columns.Add("设备类型 ", typeof(String));
                dt.Columns.Add("EcoServer_IP ", typeof(String));
                dt.Columns.Add("EcoServer点位 ", typeof(String));
                dt.Columns.Add("点位内容 ", typeof(String));
                dt.Columns.Add("值类型  0为取瞬时值 1为取差值", typeof(string));
                //读入文件
                using (StreamReader reader = new StreamReader(stream, Encoding.Default))
                {
                    //循环读取所有行
                    while (!reader.EndOfStream)
                    {
                        //将每行数据，用,分割
                        String[] data = reader.ReadLine().Split(',');
                        //新建一行，并将读出的数据分别存入对应的列中，有几列建立几个
                        //DataRow da = dt.NewRow();
                        DataRow dr = dt.NewRow();
                        dr[0] = data[0];
                        dr[1] = data[1];
                        dr[2] = data[2];
                        dr[3] = data[3];
                        dr[4] = data[4];
                        dr[5] = data[5];

                        dt.Rows.Add(dr);
                    }
                }
                string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                using (SQLiteConnection conn = new SQLiteConnection(connstr))
                {
                    conn.Open();
                    SQLiteTransaction tr = conn.BeginTransaction();//事务开始
                    try
                    {
                        SQLiteCommand cmd = new SQLiteCommand();
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            cmd.CommandText = string.Format("insert into  config (serialnumber,type,servername,localID,remarks,Date,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", dt.Rows[k][0], dt.Rows[k][1], dt.Rows[k][2], dt.Rows[k][3], dt.Rows[k][4], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), dt.Rows[k][5]);
                            cmd.Connection = conn;
                            cmd.ExecuteNonQuery();
                        }
                        tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        GlobalLog.WriteErrorLog(ex.Message);
                        tr.Rollback();//回滚
                    }
                }
                this.dataGridView1.DataSource = dt;
                MessageBox.Show("导入成功！");
            }
        }
        /// <summary>
        /// 查询导入的配置文件中Air类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Air_Click(object sender, EventArgs e)
        {
            String sql = "select distinct serialnumber as 输出文件序列,type as 设备类型, servername as EcoServer_IP ,localID as EcoServer点位,remarks as 点位内容,valueType as '值类型  0为取瞬时值 1为取差值' from config where type ='AIR'";
            GetSql(sql);
        }
        /// <summary>
        ///  查询导入的配置文件中Gas类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Gas_Click(object sender, EventArgs e)
        {
            String sql = "select distinct serialnumber as 输出文件序列,type as 设备类型, servername as EcoServer_IP ,localID as EcoServer点位,remarks as 点位内容,valueType as '值类型  0为取瞬时值 1为取差值' from config where type ='GAS'";
            GetSql(sql);
        }
        /// <summary>
        ///  查询导入的配置文件中Power类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Power_Click(object sender, EventArgs e)
        {
            String sql = "select distinct serialnumber as 输出文件序列,type as 设备类型, servername as EcoServer_IP ,localID as EcoServer点位,remarks as 点位内容,valueType as '值类型  0为取瞬时值 1为取差值' from config where type ='POWER'";
            GetSql(sql);
        }
        /// <summary>
        ///  查询导入的配置文件中Water类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Water_Click(object sender, EventArgs e)
        {
            String sql = "select distinct serialnumber as 输出文件序列,type as 设备类型, servername as EcoServer_IP ,localID as EcoServer点位,remarks as 点位内容,valueType as '值类型  0为取瞬时值 1为取差值' from config where type ='WATER'";
            GetSql(sql);
        }
        /// <summary>
        /// 连接数据库,执行sql语句
        /// </summary>
        /// <param name="str"></param>
        public void GetSql(string sql)
        {
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                conn.Close();
            }
        }
        /// <summary>
        /// 初始化时加载debug/ini.txt中的变量c
        /// </summary>
        /// <returns></returns>
        public static string writer()
        {
            string ini = Application.StartupPath + "\\ini.txt";
            using (StreamReader sr = new StreamReader(ini, Encoding.Default))
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
        /// <summary>
        /// 初始化时加载debug/path.xt中指定的存放csv路径
        /// </summary>
        /// <returns></returns>
        public static string writerpath()
        {
            string path = Application.StartupPath + "\\path.txt";
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
        /// <summary>
        /// 初始化时加载debug/defaultpath.txt中指定的本地存放csv的路径
        /// </summary>
        /// <returns></returns>
        public static string defaultwriterpath()
        {
            string defaultpath = Application.StartupPath + "\\defaultpath.txt";
            using (StreamReader sr = new StreamReader(defaultpath, Encoding.Default))
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
        /// <summary>
        /// 初始化时加载debug/time.txt中指定的定时间隔
        /// </summary>
        /// <returns></returns>
        public static string writertime()
        {
            string path = Application.StartupPath + "\\time.txt";
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
        /// <summary>
        /// 定时请求服务器获取json的值
        /// </summary>
        public static void Get()
        {
            textBox_fuwuqi.Text = "";  //将从数据库查询到的不同的ip存放到 textBox_fuwuqi
            string time = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"); //该方法执行时的时间
            GlobalLog.WriteInfoLog("每次获取的时间：" + time);
            int c = Convert.ToInt32(writer());   //插入数据库时变量
            string sql2 = "SELECT  DISTINCT servername FROM config ORDER BY servername ASC";
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql2, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox_fuwuqi.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                }
                conn.Close();
            }
            string[] j = textBox_fuwuqi.Text.Split(new char[] { ',' });  //textBox_fuwuqi 转换成数组
            for (int k = 0; k < j.Length - 1; k++)  //for循环将IP作为变量传递到fuwuqi_1方法
            {
                fuwuqidingshi_1(time, c, j[k]);
                Console.WriteLine("服务器" + j[k] + "执行完毕!");
            }
            c++;    //将变量c写入到debug/ini.txt
            string fi = Application.StartupPath + "\\ini.txt";
            using (StreamWriter sw = new StreamWriter(fi.ToString(), false, Encoding.Default))
            {
                sw.WriteLine(c);
                sw.Flush();  //刷新缓冲区(覆盖)
                sw.Close();
            }
            GlobalLog.WriteInfoLog("button_Get执行完毕！");
        }

        /// <summary>
        /// / 发送http请求获取json字符串中指定值插入数据库
        /// </summary>
        /// <param name="time">发送请求的时间</param>
        /// <param name="c">变量c</param>
        /// <param name="urlpath">IP地址</param>
        private static void fuwuqidingshi_1(string time, int c, string urlpath)
        {
            string Url = urlpath;
            GlobalLog.WriteInfoLog("Url：" + Url);
            textBox1_LocalID.Text = "";
            textBox_GetValue.Text = "";
            textBox_servername.Text = "";
            textBox_localID.Text = "";
            textBox_type.Text = "";
            textBox_serialnumber.Text = "";
            textBox_Date.Text = "";
            textBox_valueType.Text = "";
            string sql11 = "select localID,servername,type,serialnumber,Date,valueType from config where servername='" + Url + "'";
            string connstr1 = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr1))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql11, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox1_LocalID.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                    textBox_servername.Text += ds.Tables[0].Rows[i][1].ToString() + ',';
                    textBox_localID.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                    textBox_type.Text += ds.Tables[0].Rows[i][2].ToString() + ',';
                    textBox_serialnumber.Text += ds.Tables[0].Rows[i][3].ToString() + ',';
                    textBox_Date.Text += ds.Tables[0].Rows[i][4].ToString() + ',';
                    textBox_valueType.Text += ds.Tables[0].Rows[i][5].ToString() + ',';
                }
                conn.Close();
            }
            string[] a = textBox1_LocalID.Text.Split(new char[] { ',' });
            string Url1 = "http://" + Url + "/";
            GlobalLog.WriteInfoLog("Url1：" + Url1);
            //判断IP是否存在
            if (Url1 != "")
            {
                try
                {
                    GlobalLog.WriteInfoLog("第一次发送请求开始------");
                    //读取服务器将从json字符串中取到的值写入数据库
                    dingshiConnectionUrl(Url, Url1, time, c, a);
                }
                catch (Exception)
                {
                    //如果再次请求超时失败 赋值null
                    try
                    {
                        GlobalLog.WriteInfoLog("再次发送请求开始------");
                        dingshiConnectionUrl(Url, Url1, time, c, a);
                    }
                    catch (Exception)
                    {
                        GlobalLog.WriteInfoLog("再次发送请求失败开始------");
                        textBox_GetNull.Text = "";
                        string[] t = textBox_servername.Text.Split(new char[] { ',' });
                        string[] x = textBox_localID.Text.Split(new char[] { ',' });
                        string[] y = textBox_type.Text.Split(new char[] { ',' });
                        string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                        string[] w = textBox_Date.Text.Split(new char[] { ',' });
                        string[] v = textBox_valueType.Text.Split(new char[] { ',' });
                        for (int i = 0; i < x.Length - 1; i++)
                        {
                            x[i] = "0";
                            textBox_GetNull.Text += x[i] + ',';
                        }
                        string[] s = textBox_GetNull.Text.Split(new char[] { ',' });
                        string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                        SQLiteConnection con = new SQLiteConnection(connectionString);//***
                        con.Open();
                        GlobalLog.WriteInfoLog("断线事务" + Url + "开启之前------");
                        SQLiteTransaction tr = con.BeginTransaction();//事务开始
                        try
                        {
                            SQLiteCommand cmd = new SQLiteCommand();
                            for (int l = 0; l < a.Length - 1; l++)
                            {
                                cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), a[l].ToString().Trim(), y[l].ToString().Trim(), s[l].ToString().Trim(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), time, c.ToString().Trim(), v[l].ToString().Trim());
                                GlobalLog.WriteInfoLog("执行Sql：" + cmd.CommandText);
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                            }
                            tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                            con.Close();
                            GlobalLog.WriteInfoLog("断线" + Url + "事务执行完毕--------");
                        }
                        catch (Exception ex)
                        {
                            GlobalLog.WriteErrorLog(ex.Message);
                            tr.Rollback();//回滚
                        }
                        con.Close();
                    }
                }
            }
            else
            {
                GlobalLog.WriteErrorLog("服务器" + Url + "为空！");
            }
        }
        /// <summary>
        /// 发送http请求获取json字符串中指定值插入数据库
        /// </summary>
        /// <param name="Url">IP地址</param>
        /// <param name="Url1">http+IP</param>
        /// <param name="time">发送请求时间</param>
        /// <param name="c">变量c</param>
        /// <param name="a">测量点localID</param>
        private static void dingshiConnectionUrl(string Url, string Url1, string time, int c, string[] a)
        {
            bool flag1 = HttpUtils.GetConnection(Url1);  //服务器是否响应
            if (flag1 == true)
            {
                GlobalLog.WriteInfoLog("读取服务器" + Url + "中json开始的时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                for (int r = 0; r < a.Length - 1; r += 10)
                {
                    if ((a.Length - 1 - r) >= 10) //数组剩余长度大于10
                    {
                        string[] q = a.Skip(r).Take(10).ToArray();   //截取数组长度10
                        textBox_ID.Text = "";
                        for (int i = 0; i < q.Length; i++)
                        {
                            textBox_ID.Text += q[i].ToString() + ',';
                        }
                        string url = "http://" + Url + "/ecoVM/ecoVMJSON?req=1001&id=" + textBox_ID.Text + "&diffType=0";
                        GlobalLog.WriteInfoLog("url：" + url);
                        //发送http请求
                        string json = HttpUtils.Get(url);
                        GlobalLog.WriteInfoLog("json：" + json);
                        //转换json字符串格式
                        string convertjson = HttpUtils.ConvertJsonString(json);
                        //congjson字符串中取出指定值
                        string value = HttpUtils.GetJsonValue(convertjson, q.Length);
                        GlobalLog.WriteInfoLog("value：" + value);
                        textBox_GetValue.Text += value;
                    }
                    else  //数组剩余长度小于10
                    {
                        string[] q = a.Skip(r).Take((a.Length - 1 - r)).ToArray();
                        textBox_ID.Text = "";
                        for (int i = 0; i < q.Length; i++)
                        {
                            textBox_ID.Text += q[i].ToString() + ',';
                        }
                        string url = "http://" + Url + "/ecoVM/ecoVMJSON?req=1001&id=" + textBox_ID.Text + "&diffType=0";
                        GlobalLog.WriteInfoLog("url：" + url);
                        string json = HttpUtils.Get(url);
                        GlobalLog.WriteInfoLog("json：" + json);
                        string convertjson = HttpUtils.ConvertJsonString(json);
                        string value = HttpUtils.GetJsonValue(convertjson, q.Length);
                        GlobalLog.WriteInfoLog("value：" + value);
                        textBox_GetValue.Text += value;
                    }
                }
                GlobalLog.WriteInfoLog("读取服务器" + Url + "中json结束的时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                string[] s = textBox_GetValue.Text.Split(new char[] { ',' });//将从json中获取的值存在数组里
                string[] t = textBox_servername.Text.Split(new char[] { ',' });
                string[] x = textBox_localID.Text.Split(new char[] { ',' });
                string[] y = textBox_type.Text.Split(new char[] { ',' });
                string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                string[] w = textBox_Date.Text.Split(new char[] { ',' });
                string[] v = textBox_valueType.Text.Split(new char[] { ',' });
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                SQLiteConnection con = new SQLiteConnection(connectionString);//***
                con.Open();
                if (ConnectionState.Open == con.State)
                {
                    GlobalLog.WriteInfoLog("数据库已打开");
                }
                else
                {
                    GlobalLog.WriteInfoLog("数据库未打开");
                }
                GlobalLog.WriteInfoLog("开启" + Url + "事务之前------");
                SQLiteTransaction tr = con.BeginTransaction();//事务开始
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    for (int l = 0; l < s.Length - 1; l++)
                    {
                        //字符串转double  double转int  int转字符串
                        string e = Convert.ToString(Convert.ToInt32(Convert.ToDouble(s[l])));
                        cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), x[l].ToString().Trim(), y[l].ToString().Trim(), e, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), time, c.ToString().Trim(), v[l].ToString().Trim());
                        GlobalLog.WriteInfoLog("执行Sql：" + cmd.CommandText);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                    con.Close();
                    GlobalLog.WriteInfoLog("事务" + Url + "执行完毕--------");
                }
                catch (Exception ex)
                {
                    GlobalLog.WriteErrorLog(ex.ToString());
                    tr.Rollback();//回滚
                }
                con.Close();
            }
            else
            {
                textBox_GetNull.Text = "";
                string[] t = textBox_servername.Text.Split(new char[] { ',' });
                string[] x = textBox_localID.Text.Split(new char[] { ',' });
                string[] y = textBox_type.Text.Split(new char[] { ',' });
                string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                string[] w = textBox_Date.Text.Split(new char[] { ',' });
                string[] v = textBox_valueType.Text.Split(new char[] { '.' });
                for (int i = 0; i < x.Length - 1; i++)
                {
                    x[i] = "0";   //服务器断线该服务器上测量点为'0'
                    textBox_GetNull.Text += x[i] + ',';
                }
                string[] s = textBox_GetNull.Text.Split(new char[] { ',' });
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                SQLiteConnection con = new SQLiteConnection(connectionString);//***
                con.Open();
                GlobalLog.WriteInfoLog("开启断线" + Url + "事务之前------");
                SQLiteTransaction tr = con.BeginTransaction();//事务开始
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    for (int l = 0; l < a.Length - 1; l++)
                    {
                        cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), a[l].ToString().Trim(), y[l].ToString().Trim(), s[l].ToString().Trim(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), time, c.ToString().Trim(), v[l].ToString().Trim());
                        GlobalLog.WriteInfoLog("执行Sql：" + cmd.CommandText);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                    con.Close();
                    GlobalLog.WriteInfoLog("事务" + Url + "断线执行完毕--------");
                }
                catch (Exception ex)
                {
                    GlobalLog.WriteErrorLog(ex.Message);
                    tr.Rollback();//回滚
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {         
            //DES加密、解密
            string str = SericalNumber();
            string key = "fd09ok46";
            string dateTimeStr = Encryption.DesDecrypt(str, key);
            GlobalLog.WriteInfoLog("输出dateTimeStr：" + dateTimeStr);
            string dateTimeEnd = DateTime.Now.ToString("yyyyMMdd");
            GlobalLog.WriteInfoLog("输出dateTimeEnd：" + dateTimeEnd);

            if (Convert.ToInt32(dateTimeEnd) - Convert.ToInt32(dateTimeStr) > 511)
            {
                Application.Exit();
                MessageBox.Show("软件试用期已到,请联系管理员！");
            }


            GlobalLog.WriteInfoLog("系统启动");
            this.timer1.Enabled = true;
            this.timer1.Interval = 60000;  //一分钟
        }

        public static string SericalNumber()
        {
            string path = Application.StartupPath + "\\serical.txt";
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

        /// <summary>
        /// 定时执行请求和导出方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void getout()
        {
            Get();
            Out();
        }
        /// <summary>
        /// 定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            label3.Text = "采集未开始！";
            int hour = DateTime.Now.Hour;
            int minute = DateTime.Now.Minute;
            
            int time = int.Parse(textBox_Time.Text);
            GlobalLog.WriteInfoLog("小时：" + hour);
            GlobalLog.WriteInfoLog("分钟：" + minute);

            if ((hour*60+minute)%(time)==0)
            {
                Console.WriteLine("开始采集：");
                label3.Text = "采集中！";
                ThreadStart threadStart = new ThreadStart(dingshistart);
                Thread thread = new Thread(threadStart);
                thread.IsBackground = true; //主线程结束该线程也随之结束
                thread.Start();
                
                GlobalLog.WriteInfoLog("完成！");
            }
            DeleteFile.Delete(CreatFolder.CreatDefaultAIRFolder(defaultpath));
            DeleteFile.Delete(CreatFolder.CreatDefaultGASFolder(defaultpath));
            DeleteFile.Delete(CreatFolder.CreatDefaultPOWERFolder(defaultpath));
            DeleteFile.Delete(CreatFolder.CreatDefaultWATERFolder(defaultpath));

            //DES加密、解密
            string str = SericalNumber();
            string key = "fd09ok46";
            string dateTimeStr = Encryption.DesDecrypt(str, key); 
            GlobalLog.WriteInfoLog("输出dateTimeStr：" + dateTimeStr);
            string dateTimeEnd = DateTime.Now.ToString("yyyyMMdd");
            GlobalLog.WriteInfoLog("输出dateTimeEnd：" + dateTimeEnd);

            if (Convert.ToInt32(dateTimeEnd) - Convert.ToInt32(dateTimeStr) > 511)
            {                
                Application.Exit();
                MessageBox.Show("软件试用期已到,请联系管理员！");
            }

            //button_Clear_Click(sender, e);
        }
        /// <summary>
        /// 后台线程运行请求服务器、导出csvs
        /// </summary>
        private void dingshistart()
        {
            getout();
        }
        /// <summary>
        /// 定时到处AIR/GAS/POWER/WATER的值到相应的csv
        /// </summary>
        public static void Out()
        {
            textBox1.Text = ""; //清空文本框  
            string sql = "select distinct valueType from config ";     //取出配置文件中值类型(0、1)     
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox1.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                }
                conn.Close();
            }
            string[] j = textBox1.Text.Split(new char[] { ',' });  //textBox1 配置文件中值类型 转换成数组
            for (int k = 0; k < j.Length - 1; k++)   //for循环将valueType作为变量传递
            {
                Console.WriteLine("j[k]：" + j[k]);
                if (j[k] == "0")
                {
                    string currentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) FROM value) and valueType=0 ORDER BY CAST(serialnumber as signed integer)";
                    DBHepler.insertCurrentExport(currentSql);  //将瞬时表最近一次记录插入导出表
                }
                if (j[k] == "1")
                {
                    string currentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) FROM value) and valueType=1 ORDER BY CAST(serialnumber as signed integer)";
                    string lastcurrentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) from value where requestDate < (select MAX(requestDate) FROM value)) and valueType=1  ORDER BY CAST(serialnumber as signed integer)";
                    DBHepler.insertDifferenceExport(currentSql, lastcurrentSql);  //将差值存入导出表
                }
            }
            string airfile = CreatFolder.CreatAirFolder(path);
            string defaultairfile = CreatFolder.CreatDefaultAIRFolder(defaultpath);
            string airSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='AIR'  ORDER BY CAST(serialnumber as signed integer)";
            CreatFile.dingshiCreatFile(airfile, defaultairfile, airSql);
            GlobalLog.WriteInfoLog("导出AIR.csv完成！");


            string gasfile = CreatFolder.CreatGasFolder(path);
            string defaultgasfile = CreatFolder.CreatDefaultGASFolder(defaultpath);
            string gasSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='GAS'   ORDER BY CAST(serialnumber as signed integer)";
            CreatFile.dingshiCreatFile(gasfile, defaultgasfile, gasSql);
            GlobalLog.WriteInfoLog("导出GAS.csv完成！");


            string powerfile = CreatFolder.CreatPowerFolder(path);
            string defaultpowerfile = CreatFolder.CreatDefaultPOWERFolder(defaultpath);
            string powerSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='POWER'    ORDER BY CAST(serialnumber as signed integer)";
            CreatFile.dingshiCreatFile(powerfile, defaultpowerfile, powerSql);
            GlobalLog.WriteInfoLog("导出POWER.csv完成！");


            string waterfile = CreatFolder.CreatWaterFolder(path);
            string defaultwaterfile = CreatFolder.CreatDefaultWATERFolder(defaultpath);
            string waterSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='WATER'   ORDER BY CAST(serialnumber as signed integer)";
            CreatFile.dingshiCreatFile(waterfile, defaultwaterfile, waterSql);
            GlobalLog.WriteInfoLog("导出WATER.csv完成！");
            label3.Text = "采集完成！";
        }

        /// <summary>
        /// 查询出配置文件中所有信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_All_Click(object sender, EventArgs e)
        {
            String sql = "select distinct serialnumber as 输出文件序列,type as 设备类型, servername as EcoServer_IP ,localID as EcoServer点位,remarks as 点位内容,valueType as '值类型  0为取瞬时值 1为取差值' from config";
            GetSql(sql);
        }
        /// <summary>
        /// 将修改的定时间隔写入debug/time.txt 在初始化时读取 重启程序后不必收到填写
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox_Time_TextChanged(object sender, EventArgs e)
        {
            string fitime = Application.StartupPath + "\\time.txt";
            using (StreamWriter sw = new StreamWriter(fitime.ToString(), false, Encoding.Default))
            {
                sw.WriteLine(this.textBox_Time.Text);
                sw.Flush();
                sw.Close();
            }
        }
        /// <summary>
        /// winform 点击按钮选择文件保存的路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Folder_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                DirectoryInfo theFolder = new DirectoryInfo(foldPath);
                airpath = CreatFolder.CreatAirFolder(theFolder.FullName.ToString().Trim());
                gaspath = CreatFolder.CreatGasFolder(theFolder.FullName.ToString().Trim());
                powerpath = CreatFolder.CreatPowerFolder(theFolder.FullName.ToString().Trim());
                waterpath = CreatFolder.CreatWaterFolder(theFolder.FullName.ToString().Trim());
                path = theFolder.FullName.ToString().Trim();
                string fipath = Application.StartupPath + "\\path.txt";
                using (StreamWriter sw = new StreamWriter(fipath.ToString(), false, Encoding.Default))
                {
                    sw.WriteLine(path);
                    sw.Flush();
                    sw.Close();
                }
                GlobalLog.WriteInfoLog("path：" + path);
            }
        }
        /// <summary>
        /// 定时清除一月前export中的记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Clear_Click(object sender, EventArgs e)
        {
            string sql = "delete from export where requestDate <'" + DateTime.Now.AddMonths(-1) + "'";
            Console.WriteLine("输出删除export表sql：" + sql);
            DBHepler.deleteExport(sql);
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 手动请求服务器获取json值
        /// </summary>
        public static void shoudongGet()
        {
            textBox_fuwuqi.Text = ""; //将从数据库查询到的不同的ip存放到 textBox_fuwuqi
            string shoudongtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");//该方法执行时的时间
            GlobalLog.WriteInfoLog("每次获取的时间：" + shoudongtime);
            int c = Convert.ToInt32(writer());  //插入数据库时变量
            string sql2 = "SELECT  DISTINCT servername FROM config ORDER BY servername ASC";
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql2, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox_fuwuqi.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                }
                conn.Close();
            }
            string[] j = textBox_fuwuqi.Text.Split(new char[] { ',' });  //textBox_fuwuqi 转换成数组                                                                   
            for (int k = 0; k < j.Length - 1; k++)   //for循环将IP作为变量传递到fuwuqi_1方法
            {
                fuwuqi_1(shoudongtime, c, j[k]);
                Console.WriteLine("服务器" + j[k] + "执行完毕!");
                Console.WriteLine(k);
            }
            //将变量c写入到debug/ini.txt
            c++;
            string fi = Application.StartupPath + "\\ini.txt";
            using (StreamWriter sw = new StreamWriter(fi.ToString(), false, Encoding.Default))
            {
                sw.WriteLine(c);
                sw.Flush(); //刷新缓冲区(覆盖)
                sw.Close();
            }
            GlobalLog.WriteInfoLog("button_Get执行完毕！");
        }


        /// <summary>
        /// 发送http请求从返回的json字符串中获取指定值,插入数据库
        /// </summary>
        /// <param name="shoudongtime">发送请求的时间</param>
        /// <param name="c">变量的增量</param>
        /// <param name="urlpath">服务器IP</param>
        private static void fuwuqi_1(string shoudongtime, int c, string urlpath)
        {
            string Url = urlpath;
            GlobalLog.WriteInfoLog("Url：" + Url);
            textBox1_LocalID.Text = "";
            textBox_GetValue.Text = "";
            textBox_servername.Text = "";
            textBox_localID.Text = "";
            textBox_type.Text = "";
            textBox_serialnumber.Text = "";
            textBox_Date.Text = "";
            textBox_valueType.Text = "";
            string sql11 = "select localID,servername,type,serialnumber,Date,valueType from config where servername='" + Url + "'";
            string connstr1 = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr1))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql11, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox1_LocalID.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                    textBox_servername.Text += ds.Tables[0].Rows[i][1].ToString() + ',';
                    textBox_localID.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                    textBox_type.Text += ds.Tables[0].Rows[i][2].ToString() + ',';
                    textBox_serialnumber.Text += ds.Tables[0].Rows[i][3].ToString() + ',';
                    textBox_Date.Text += ds.Tables[0].Rows[i][4].ToString() + ',';
                    textBox_valueType.Text += ds.Tables[0].Rows[i][5].ToString() + ',';
                }
                conn.Close();
            }
            string[] a = textBox1_LocalID.Text.Split(new char[] { ',' });
            string Url1 = "http://" + Url + "/";
            GlobalLog.WriteInfoLog("Url1：" + Url1);
            //判断IP是否存在
            if (Url1 != "")
            {
                try
                {
                    GlobalLog.WriteInfoLog("第一次发送请求开始------");
                    //读取服务器将从json字符串中取到的值写入数据库
                    ConnectionUrl(Url, Url1, shoudongtime, c, a);
                }
                catch (Exception)
                {
                    //如果再次请求超时失败 赋值null
                    try
                    {
                        GlobalLog.WriteInfoLog("再次发送请求开始------");
                        ConnectionUrl(Url, Url1, shoudongtime, c, a);
                    }
                    catch (Exception)
                    {
                        GlobalLog.WriteInfoLog("再次发送请求失败开始------");
                        textBox_GetNull.Text = "";
                        string[] t = textBox_servername.Text.Split(new char[] { ',' });
                        string[] x = textBox_localID.Text.Split(new char[] { ',' });
                        string[] y = textBox_type.Text.Split(new char[] { ',' });
                        string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                        string[] w = textBox_Date.Text.Split(new char[] { ',' });
                        string[] v = textBox_valueType.Text.Split(new char[] { ',' });
                        for (int i = 0; i < x.Length - 1; i++)
                        {
                            x[i] = "0";
                            textBox_GetNull.Text += x[i] + ',';
                        }
                        string[] s = textBox_GetNull.Text.Split(new char[] { ',' });
                        string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                        SQLiteConnection con = new SQLiteConnection(connectionString);//***
                        con.Open();
                        GlobalLog.WriteInfoLog("断线事务" + Url + "开启之前------");
                        SQLiteTransaction tr = con.BeginTransaction();//事务开始
                        try
                        {
                            SQLiteCommand cmd = new SQLiteCommand();
                            for (int l = 0; l < a.Length - 1; l++)
                            {
                                cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), a[l].ToString().Trim(), y[l].ToString().Trim(), s[l].ToString().Trim(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), shoudongtime, c.ToString().Trim(), v[l].ToString().Trim());
                                GlobalLog.WriteInfoLog("执行Sql：" + cmd.CommandText);
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                            }
                            tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                            con.Close();
                            GlobalLog.WriteInfoLog("断线" + Url + "事务执行完毕--------");
                        }
                        catch (Exception ex)
                        {
                            GlobalLog.WriteErrorLog(ex.Message);
                            tr.Rollback();//回滚
                        }
                        con.Close();
                    }
                }
            }
            else
            {
                GlobalLog.WriteErrorLog("服务器" + Url + "为空！");
            }
        }
        /// <summary>
        /// 发送http请求获取json字符串中指定值插入数据库
        /// </summary>
        /// <param name="Url">服务器IP地址</param>
        /// <param name="Url1">http+IP的路径</param>
        /// <param name="shoudongtime">http请求时的时间</param>
        /// <param name="c">变量C</param>
        /// <param name="a">测量点localID</param>
        private static void ConnectionUrl(string Url, string Url1, string shoudongtime, int c, string[] a)
        {
            bool flag1 = HttpUtils.GetConnection(Url1);//发送的请求是否响应
            if (flag1 == true)
            {
                GlobalLog.WriteInfoLog("读取服务器" + Url + "中json开始的时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                for (int r = 0; r < a.Length - 1; r += 10)
                {
                    if ((a.Length - 1 - r) >= 10) //数组剩余长度大于10
                    {
                        string[] q = a.Skip(r).Take(10).ToArray(); //截取localID长度为10
                        textBox_ID.Text = "";

                        for (int i = 0; i < q.Length; i++)
                        {
                            textBox_ID.Text += q[i].ToString() + ',';
                        }
                        //发送http请求
                        string url = "http://" + Url + "/ecoVM/ecoVMJSON?req=1001&id=" + textBox_ID.Text + "&diffType=0";
                        GlobalLog.WriteInfoLog("url：" + url);
                        //返回json字符串
                        string json = HttpUtils.Get(url);
                        Console.WriteLine("json：" + json);
                        GlobalLog.WriteInfoLog("json：" + json);
                        //转化json字符串
                        string convertjson = HttpUtils.ConvertJsonString(json);
                        //从json字符串取值
                        string value = HttpUtils.GetJsonValue(convertjson, q.Length);
                        GlobalLog.WriteInfoLog("value：" + value);
                        textBox_GetValue.Text += value;
                    }
                    else  //数组剩余长度小于10
                    {
                        string[] q = a.Skip(r).Take((a.Length - 1 - r)).ToArray();
                        textBox_ID.Text = "";
                        for (int i = 0; i < q.Length; i++)
                        {
                            textBox_ID.Text += q[i].ToString() + ',';
                        }
                        string url = "http://" + Url + "/ecoVM/ecoVMJSON?req=1001&id=" + textBox_ID.Text + "&diffType=0";
                        GlobalLog.WriteInfoLog("url：" + url);
                        string json = HttpUtils.Get(url);
                        Console.WriteLine("json：" + json);
                        GlobalLog.WriteInfoLog("json：" + json);
                        string convertjson = HttpUtils.ConvertJsonString(json);
                        string value = HttpUtils.GetJsonValue(convertjson, q.Length);
                        GlobalLog.WriteInfoLog("value：" + value);
                        textBox_GetValue.Text += value;
                    }
                }
                GlobalLog.WriteInfoLog("读取服务器" + Url + "中json结束的时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                string[] s = textBox_GetValue.Text.Split(new char[] { ',' });//将从json中获取的值存在数组里
                string[] t = textBox_servername.Text.Split(new char[] { ',' });
                string[] x = textBox_localID.Text.Split(new char[] { ',' });
                string[] y = textBox_type.Text.Split(new char[] { ',' });
                string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                string[] w = textBox_Date.Text.Split(new char[] { ',' });
                string[] v = textBox_valueType.Text.Split(new char[] { ',' });
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                SQLiteConnection con = new SQLiteConnection(connectionString);//***
                con.Open();
                if (ConnectionState.Open == con.State)
                {
                    GlobalLog.WriteInfoLog("数据库已打开");
                }
                else
                {
                    GlobalLog.WriteInfoLog("数据库未打开");
                }
                GlobalLog.WriteInfoLog("" + Url + "事务开启之前------");
                SQLiteTransaction tr = con.BeginTransaction();//事务开始
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    for (int l = 0; l < s.Length - 1; l++)
                    {
                        //字符串转double  double转int  int转字符串
                        string e = Convert.ToString(Convert.ToInt32(Convert.ToDouble(s[l])));
                        cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), x[l].ToString().Trim(), y[l].ToString().Trim(), e, DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), shoudongtime, c.ToString().Trim(), v[l].ToString().Trim());
                        GlobalLog.WriteInfoLog("执行插入Sql：" + cmd.CommandText);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                    con.Close();
                    GlobalLog.WriteInfoLog("" + Url + "事务执行完毕--------");
                }
                catch (Exception ex)
                {
                    GlobalLog.WriteErrorLog(ex.ToString());
                    tr.Rollback();//回滚
                }
                con.Close();
            }
            else
            {
                GlobalLog.WriteInfoLog("服务器" + Url + "断线开始------");
                textBox_GetNull.Text = "";
                string[] t = textBox_servername.Text.Split(new char[] { ',' });
                string[] x = textBox_localID.Text.Split(new char[] { ',' });
                string[] y = textBox_type.Text.Split(new char[] { ',' });
                string[] z = textBox_serialnumber.Text.Split(new char[] { ',' });
                string[] w = textBox_Date.Text.Split(new char[] { ',' });
                string[] v = textBox_valueType.Text.Split(new char[] { ',' });
                for (int i = 0; i < x.Length - 1; i++)
                {
                    x[i] = "0";  //服务器在断线时该服务器上测量点的值为0
                    textBox_GetNull.Text += x[i] + ',';
                }
                string[] s = textBox_GetNull.Text.Split(new char[] { ',' });
                string connectionString = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
                SQLiteConnection con = new SQLiteConnection(connectionString);//***
                con.Open();
                GlobalLog.WriteInfoLog("断线事务" + Url + "开启之前------");
                SQLiteTransaction tr = con.BeginTransaction();//事务开始
                try
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    for (int l = 0; l < a.Length - 1; l++)
                    {
                        cmd.CommandText = string.Format("insert into  value (serialnumber,servername,localID,type,value,Date,requestDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", z[l].ToString().Trim(), t[l].ToString().Trim(), a[l].ToString().Trim(), y[l].ToString().Trim(), s[l].ToString().Trim(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), shoudongtime, c.ToString().Trim(), v[l].ToString().Trim());
                        GlobalLog.WriteInfoLog("执行插入Sql：" + cmd.CommandText);
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();
                    }
                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                    con.Close();
                    GlobalLog.WriteInfoLog("断线" + Url + "事务执行完毕--------");
                }
                catch (Exception ex)
                {
                    GlobalLog.WriteErrorLog(ex.Message);
                    tr.Rollback();//回滚
                }
                con.Close();
            }
        }
        /// <summary>
        /// 手动导出csv
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_shoudongOut_Click(object sender, EventArgs e)
        {
            label3.Text = "采集中！";
            //label3.Image = Image.FromFile(Application.StartupPath + "\\pic.gif");
            ThreadStart threadStart = new ThreadStart(start);
            Thread thread = new Thread(threadStart);
            thread.IsBackground = true; //前台线程关闭 该后台线程随之关闭
            thread.Start();

        }
        private void start()
        {
            shoudongGet();
            textBox1.Text = ""; //清空文本框 存放值类型
            //valueType 0为瞬时值 1为差值
            string sql = "select distinct valueType from config ";  //取出配置表值类型
            //string str = DBHepler.GetValeuType(sql);            
            string connstr = ConfigurationManager.ConnectionStrings["myconn"].ConnectionString;
            using (SQLiteConnection conn = new SQLiteConnection(connstr))
            {
                conn.Open();
                SQLiteDataAdapter sda = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                int v = ds.Tables[0].Rows.Count;
                for (int i = 0; i < v; i++)
                {
                    textBox1.Text += ds.Tables[0].Rows[i][0].ToString() + ',';
                }
                conn.Close();
            }
            string[] j = textBox1.Text.Split(new char[] { ',' });  //textBox1 转换成数组

            for (int k = 0; k < j.Length - 1; k++)   //for循环将valueType作为变量传递
            {
                Console.WriteLine("j[k]：" + j[k]);
                if (j[k] == "0")
                {
                    string currentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) FROM value) and valueType=0 ORDER BY CAST(serialnumber as signed integer)";
                    DBHepler.insertCurrentExport(currentSql);  //将瞬时表最近一次记录插入导出表              
                }
                if (j[k] == "1")
                {
                    string currentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) FROM value) and valueType=1 ORDER BY CAST(serialnumber as signed integer)";
                    string lastcurrentSql = "SELECT * FROM value WHERE requestDate = (select MAX(requestDate) from value where requestDate < (select MAX(requestDate) FROM value)) and valueType=1  ORDER BY CAST(serialnumber as signed integer)";
                    DBHepler.insertDifferenceExport(currentSql, lastcurrentSql); //将差值存入导出表                   
                }
            }
            string airfile = CreatFolder.CreatAirFolder(path);
            string airSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='AIR'  ORDER BY CAST(serialnumber as signed integer)";
            string defaultairfile = CreatFolder.CreatDefaultAIRFolder(defaultpath);
            CreatFile.shoudongCreatFile(airfile, defaultairfile, airSql);
            GlobalLog.WriteInfoLog("导出AIR.csv完成！");

            string gasfile = CreatFolder.CreatGasFolder(path);
            string gasSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='GAS'  ORDER BY CAST(serialnumber as signed integer)";
            string defaultgasfile = CreatFolder.CreatDefaultGASFolder(defaultpath);
            CreatFile.shoudongCreatFile(gasfile, defaultgasfile, gasSql);
            GlobalLog.WriteInfoLog("导出GAS.csv完成！");

            string powerfile = CreatFolder.CreatPowerFolder(path);
            string powerSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='POWER'  ORDER BY CAST(serialnumber as signed integer)";
            string defaultpowerfile = CreatFolder.CreatDefaultPOWERFolder(defaultpath);
            CreatFile.shoudongCreatFile(powerfile, defaultpowerfile, powerSql);
            GlobalLog.WriteInfoLog("导出POWER.csv完成！");

            string waterfile = CreatFolder.CreatWaterFolder(path);
            string waterSql = "SELECT value,serialnumber FROM export WHERE requestDate = (select MAX(requestDate) FROM export) AND type='WATER'  ORDER BY CAST(serialnumber as signed integer)";
            string defaultwaterfile = CreatFolder.CreatDefaultWATERFolder(defaultpath);
            CreatFile.shoudongCreatFile(waterfile, defaultwaterfile, waterSql);
            GlobalLog.WriteInfoLog("导出WATER.csv完成！");

            label3.Text = "采集完成！";
            MessageBox.Show("手动导出Excel完成！");
        }

        /// <summary>
        /// 指定本地存放csv文件的路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_DefaultFolder_Path_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string defaultfoldPath = dialog.SelectedPath;
                DirectoryInfo defaulttheFolder = new DirectoryInfo(defaultfoldPath);
                defaultairpath = CreatFolder.CreatDefaultAIRFolder(defaulttheFolder.FullName.ToString().Trim());
                defaultgaspath = CreatFolder.CreatDefaultGASFolder(defaulttheFolder.FullName.ToString().Trim());
                defaultpowerpath = CreatFolder.CreatDefaultPOWERFolder(defaulttheFolder.FullName.ToString().Trim());
                defaultwaterpath = CreatFolder.CreatDefaultWATERFolder(defaulttheFolder.FullName.ToString().Trim());

                defaultpath = defaulttheFolder.FullName.ToString().Trim();
                string defaultfipath = Application.StartupPath + "\\defaultpath.txt";
                using (StreamWriter sw = new StreamWriter(defaultfipath.ToString(), false, Encoding.Default))
                {
                    sw.WriteLine(defaultpath);
                    sw.Flush();
                    sw.Close();
                }
                GlobalLog.WriteInfoLog("defaultpath：" + defaultpath);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 汇总时间段内数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_summary_Click(object sender, EventArgs e)
        {
            Console.WriteLine("输出valueType的值：" + comboBox_valueType.Text);
            if (comboBox_valueType.Text != "")
            {
                string airfile = CreatFolder.CreatAirFolder(path);
                string airSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "' and valueType=" + comboBox_valueType.Text + " and type='AIR' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + airSql);
                CreatFile.OutRecordValueType(airfile, airSql, comboBox_valueType.Text);
                GlobalLog.WriteInfoLog("导出AIR.csv完成！");

                string gasfile = CreatFolder.CreatGasFolder(path);
                string gasSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "' and valueType=" + comboBox_valueType.Text + " and type='GAS' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + gasSql);
                CreatFile.OutRecordValueType(gasfile, gasSql, comboBox_valueType.Text);
                GlobalLog.WriteInfoLog("导出GAS.csv完成！");

                string powerfile = CreatFolder.CreatPowerFolder(path);
                string powerSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "' and valueType=" + comboBox_valueType.Text + " and type='POWER' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + powerSql);
                CreatFile.OutRecordValueType(powerfile, powerSql, comboBox_valueType.Text);
                GlobalLog.WriteInfoLog("导出POWER.csv完成！");

                string waterfile = CreatFolder.CreatWaterFolder(path);
                string waterSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "' and valueType=" + comboBox_valueType.Text + " and type='WATER' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + waterSql);
                CreatFile.OutRecordValueType(waterfile, waterSql, comboBox_valueType.Text);
                GlobalLog.WriteInfoLog("导出WATER.csv完成！");
            }
            else
            {
                string airfile = CreatFolder.CreatAirFolder(path);
                string airSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "'  and type='AIR' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + airSql);
                CreatFile.outRecord(airfile, airSql);
                GlobalLog.WriteInfoLog("导出AIR.csv完成！");

                string gasfile = CreatFolder.CreatGasFolder(path);
                string gasSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "'  and type='GAS' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + gasSql);
                CreatFile.outRecord(gasfile, gasSql);
                GlobalLog.WriteInfoLog("导出GAS.csv完成！");

                string powerfile = CreatFolder.CreatPowerFolder(path);
                string powerSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "'  and type='POWER' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + powerSql);
                CreatFile.outRecord(powerfile, powerSql);
                GlobalLog.WriteInfoLog("导出POWER.csv完成！");

                string waterfile = CreatFolder.CreatWaterFolder(path);
                string waterSql = "select value,serialnumber,requestDate from export where requestDate >= '" + dateTimePicker_Start.Text + "' and requestDate<='" + dateTimePicker_End.Text + "'  and type='WATER' order by cast(requestDate as signed integer)";
                Console.WriteLine("输出sql：" + waterSql);
                CreatFile.outRecord(waterfile, waterSql);
                GlobalLog.WriteInfoLog("导出WATER.csv完成！");
            }
            MessageBox.Show("汇总导出完成！");
        }
    }
}