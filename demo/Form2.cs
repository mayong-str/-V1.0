using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace demo
{
   
    public partial class Form2 : Form
    {
        string order; //订单号
        string product; //产品号
        double target; //计划数
        double complete; //完成数
        string wanchenglv; //完成率
        int naduo; //码垛箱数
        Thread th;

        public Form2()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 实时显示日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            label_time.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm dddd");
        }
        /// <summary>
        /// 设备管理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_equipment_Click(object sender, EventArgs e)
        {
            panel_zhujiemian.Visible = true; 
            panel_equipment.Visible = true;
            panel_chanliang.Visible = false;
            panel_shujuselsect.Visible = false;
            label1_title.Visible = false;
            timer2.Enabled = false;
            label_yunshuduan.Visible=false;
            label_baozhuangduan.Visible=false;
            label_maduoduan.Visible=false;
            label1_yunshuduan.Visible = false;
            label1_baozhuangduan.Visible = false;
            label1_maduoduan.Visible = false;
        }
        /// <summary>
        /// 主界面按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_main_Click(object sender, EventArgs e)
        {
            panel_zhujiemian.Visible = true;
            panel_equipment.Visible = false;
            panel_chanliang.Visible = false;
            panel_shujuselsect.Visible = false;
            label1_title.Visible = false;
            timer2.Enabled = true;
            timer2.Interval = 10000;

            label_yunshuduan.Text = "●";
            label_baozhuangduan.Text = "●";
            label_maduoduan.Text = "●";
            if (th==null||!th.IsAlive)
            {
                label_yunshuduan.ForeColor = Color.Green;
                label_baozhuangduan.ForeColor = Color.Red;
                label_maduoduan.ForeColor = Color.Green;
            }
            else
            {
                label_yunshuduan.ForeColor = Color.Red;
                label_baozhuangduan.ForeColor = Color.Green;
                label_maduoduan.ForeColor = Color.Red;
            }
            string sql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
            dataGridView3.DataSource = db.dbHelper.sqlHelper(sql);

        }  
        private void Form2_Load(object sender, EventArgs e)
        {
            panel_zhujiemian.Visible = false; //主界面
            panel_equipment.Visible = false; //设备管理
            panel_chanliang.Visible = false; //产量管理
            panel_shujuselsect.Visible = false; //数据查询
            label1_title.Visible = true;  
            timer2.Enabled = false;      
        }
        /// <summary>
        /// 产量管理按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_chanliang_Click(object sender, EventArgs e)
        {
            panel_zhujiemian.Visible = true;
            panel_equipment.Visible = true;
            panel_chanliang.Visible = true;
            panel_shujuselsect.Visible = false;
            label1_title.Visible = false;
            timer2.Enabled = false;
            label_yunshuduan.Visible = false;
            label_baozhuangduan.Visible = false;
            label_maduoduan.Visible = false;
            label1_yunshuduan.Visible = false;
            label1_baozhuangduan.Visible = false;
            label1_maduoduan.Visible = false;
            //查询订单信息
            string sql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
            dataGridView1.DataSource = db.dbHelper.sqlHelper(sql);
        }
        /// <summary>
        /// 数据查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_shujuselect_Click(object sender, EventArgs e)
        {
            panel_zhujiemian.Visible = true;
            panel_equipment.Visible = true;
            panel_chanliang.Visible = true;
            panel_shujuselsect.Visible = true;
            label1_title.Visible = false;
            timer2.Enabled = false;
            label_yunshuduan.Visible = false;
            label_baozhuangduan.Visible = false;
            label_maduoduan.Visible = false;
            label1_yunshuduan.Visible = false;
            label1_baozhuangduan.Visible = false;
            label1_maduoduan.Visible = false;
        }
        /// <summary>
        /// 按时间查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_timeSelect_Click(object sender, EventArgs e)
        {
            //按时间段查询订单信息
            string sql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders where Date>='" + dateTimePicker_start.Text + "' and Date<='"+dateTimePicker_end.Text+"'";
            dataGridView2.DataSource = db.dbHelper.sqlHelper(sql);
        }
        /// <summary>
        /// 按订单号查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_orderSelect_Click(object sender, EventArgs e)
        {
            string order_no = textBox_orderno.Text;
            if (order_no=="")
            {
                MessageBox.Show("订单号不能为空！", "提示");
            }
            else
            {
                //按订单号查询              
                string sql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders where order_no='" + order_no + "'";
                dataGridView2.DataSource = db.dbHelper.sqlHelper(sql);
            }
        }
        /// <summary>
        /// 关闭子窗体时退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            //退出程序
            Application.Exit();
        }
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_insert_Click(object sender, EventArgs e)
        {
            bool flag=false;
            string Ordernosql = "select order_no from orders";
            DataTable dt= new DataTable();
            dt = db.dbHelper.sqlHelper(Ordernosql);
            if (dt.Rows.Count > 0)
            {
                for (int i=0;i<dt.Rows.Count;i++)
                {
                    if (textBox1_orderno.Text==dt.Rows[i][0].ToString().Trim())
                    {                   
                        flag = true;
                        break;
                    }                  
                }
            }
            if (flag==true)
            {
                MessageBox.Show("订单号已存在！","提示");
            }
            else
            {
                order = textBox1_orderno.Text;
                product = textBox1_productno.Text;
                target = int.Parse(textBox_target.Text);
                complete = 0;
                wanchenglv = ((complete / target) * 100).ToString() + '%';
                naduo = (int)complete / 4;
                if (complete <= target)
                {
                    //删除订单信息
                    string deletesql = "delete from orders where order_no='" + order + "'";
                    db.dbHelper.sqlOptionHelper(deletesql);
                    //新增订单信息
                    string insertsql = "insert into orders(order_no,product_no,target,complete,completion,number,type,Date)values('" + order + "', '" + product + "', '" + target + "', '" + complete + "', '" + wanchenglv + "', '" + naduo + "','" + false + "', '" + DateTime.Now.ToString("yyyy/MM/dd") + "')";
                    db.dbHelper.sqlOptionHelper(insertsql);
                    //查询订单信息
                    string selectsql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
                    dataGridView3.DataSource = db.dbHelper.sqlHelper(selectsql);
                }
                textBox1_complete.Text = "0";
            }            
        }
        /// <summary>
        /// 定时模拟完成数累加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            add();
            update();
        }
        /// <summary>
        /// 累加
        /// </summary>
        /// <returns></returns>
        private double add()
        {
            //定时10s完成数累加1
            target = int.Parse(textBox_target.Text);
            complete = int.Parse(textBox1_complete.Text);
            if (complete < target)
            {
                complete++;
                textBox1_complete.Text = complete.ToString().Trim();
                return complete;
            }
            return target;
        }
        /// <summary>
        /// 更新完成数、完成率、码垛数量
        /// </summary>
        public void update()
        {
            complete = 0;
            order = textBox1_orderno.Text;
            product = textBox1_productno.Text;
            target = int.Parse(textBox_target.Text);
            complete = add();
            wanchenglv = ((complete / target) * 100).ToString() + '%';
            naduo = (int)complete / 4;

            if (complete <= target)
            {
                //删除
                string deletesql = "delete from orders where order_no='" + order + "'";
                db.dbHelper.sqlOptionHelper(deletesql);

                //新增
                string insertsql = "insert into orders(order_no,product_no,target,complete,completion,number,type,Date)values('"+order+"', '"+product+"', '"+target+"', '"+complete+"', '"+wanchenglv+"', '"+naduo+"','"+false+"', '"+DateTime.Now.ToString("yyyy/MM/dd")+"')";
                db.dbHelper.sqlOptionHelper(insertsql);

                //查询
                string selectsql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
                dataGridView3.DataSource = db.dbHelper.sqlHelper(selectsql);
                
            }
        }
        private void panel_main_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel_zhujiemian_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_delete_Click(object sender, EventArgs e)
        {
            if (textBox_delete.Text=="")
            {
                MessageBox.Show("请输入要删除的订单号！","提示");
            }
            else
            {
                DialogResult result = MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result==DialogResult.Yes)
                {
                    string deletesql = "delete from orders where order_no='" + textBox_delete.Text + "'";
                    int n = db.dbHelper.sqlIntHelper(deletesql);
                    string selectsql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
                    dataGridView3.DataSource = db.dbHelper.sqlHelper(selectsql);
                    if (n != 0)
                    {
                        MessageBox.Show("删除成功！", "提示");
                    }
                    textBox1_complete.Text = "0";
                }
                else
                {
                    return;
                }
            }          
        }
        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_clear_Click(object sender, EventArgs e)
        {
            DialogResult result =MessageBox.Show("确定清除吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result==DialogResult.Yes)
            {
                string sql = "delete from orders";
                int n = db.dbHelper.sqlIntHelper(sql);
                string selectsql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
                dataGridView3.DataSource = db.dbHelper.sqlHelper(selectsql);
                if (n != 0)
                {
                    MessageBox.Show("清除成功！");
                }
                textBox1_complete.Text = "0";
            }
            else
            {
                return;
            }
            
        }
        /// <summary>
        /// 导出到Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_export_Click(object sender, EventArgs e)
        {
            string path = Folder.creatFolder()+"\\";
            if (path=="\\")
            {
                return;
            }
            else
            {
                string selectsql = "select order_no as '订单号',product_no as '产品型号',target as '计划数',complete as '完成数',completion as '完成率',number as '码垛箱数',Date as '日期' from orders";
                Excel.ExportExcel(db.dbHelper.sqlHelper(selectsql), path);
                MessageBox.Show("导出完成！", "提示");
            }
            
        }

        private void textBox_orderno_TextChanged(object sender, EventArgs e)
        {
            
        }


        public static void insertDifferenceExport(string currentsql, string lastcurrentsql)
        {
            bool flag=false;
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
                                if (int.Parse(ds.Tables[0].Rows[k][3].ToString()) - int.Parse(lastds.Tables[0].Rows[k][3].ToString()) == 0)
                                {
                                    flag = true;
                                }
                                else
                                {
                                    flag = false;
                                    break; 
                                }
                            }
                          
                            if (flag == true)
                            {
                                SQLiteTransaction tr = conn.BeginTransaction();
                                try
                                {
                                    SQLiteCommand cmd = new SQLiteCommand();
                                    for (int i = 0; i < count; i++)
                                    {
                                        string difference = (int.Parse(ds.Tables[0].Rows[i][5].ToString().Trim()) - int.Parse(lastds.Tables[0].Rows[i][5].ToString().Trim())).ToString().Trim();

                                        if (int.Parse(difference) < 0) 
                                        {
                                            difference = "0";
                                        }

                                        cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], difference, ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();
                                    }
                                    tr.Commit(); 
                                    conn.Close();
                                }
                                catch (Exception ex)
                                {
                                   
                                    tr.Rollback();
                                }
                            }
                            else
                            {
                                SQLiteTransaction tr = conn.BeginTransaction();
                                try
                                {
                                    SQLiteCommand cmd = new SQLiteCommand();
                                    for (int i = 0; i < count; i++)
                                    {
                                        cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                        cmd.Connection = conn;
                                        cmd.ExecuteNonQuery();
                                    }
                                    tr.Commit(); 
                                    conn.Close();
                                }
                                catch (Exception ex)
                                {
                                    
                                    tr.Rollback();
                                }
                               
                            }
                            conn.Clone();
                        }
                        else
                        {
                            SQLiteTransaction tr = conn.BeginTransaction();
                            try
                            {
                                SQLiteCommand cmd = new SQLiteCommand();
                                for (int i = 0; i < count; i++)
                                {
                                    cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                    cmd.Connection = conn;
                                    cmd.ExecuteNonQuery();
                                }
                                tr.Commit(); 
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                
                                tr.Rollback();
                            }
                          
                        }
                    }
                    else
                    {
                        SQLiteTransaction tr = conn.BeginTransaction();
                        try
                        {
                            SQLiteCommand cmd = new SQLiteCommand();
                            for (int i = 0; i < count; i++)
                            {
                                cmd.CommandText = string.Format("insert into  export (serialnumber,servername,localID,type,value,Date,requestDate,exportDate,RequestNumber,valueType) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')", ds.Tables[0].Rows[i][1], ds.Tables[0].Rows[i][2], ds.Tables[0].Rows[i][3], ds.Tables[0].Rows[i][4], ds.Tables[0].Rows[i][5], ds.Tables[0].Rows[i][6], ds.Tables[0].Rows[i][7], DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), ds.Tables[0].Rows[i][8], "1");
                                cmd.Connection = conn;
                                cmd.ExecuteNonQuery();
                            }
                            tr.Commit(); 
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            
                            tr.Rollback();
                        }
                        
                    }

                    conn.Clone();
                }
            }
            catch (Exception)
            {
                
            }
        }
     
        public void export()
        {
            if (dataGridView1.Rows.Count > 0)
            {

                string saveFileName = "";
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "xls";
                saveDialog.Filter = "Excel 文件(*.xlsx)|*.xlsx|Excel 文件(*.xls)|*.xls|所有文件(*.*)|*.*";
                saveDialog.RestoreDirectory = true;  //记忆上次打开的目录
                saveDialog.FileName = "page";   //默认文件名
                saveDialog.ShowDialog();
                saveFileName = saveDialog.FileName;
                if (saveFileName.IndexOf(":") < 0) return; //被点了取消
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel");
                    return;
                }
                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1

                //写入标题
                //从第二个列名开始,第一个列名是分页时的rownumber
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                //写入数值
                //从第二列开始,第一列是分页时的rownumber
                for (int r = 0; r < dataGridView1.Rows.Count; r++)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);

                        //获取输入的文件名(根据完整路径名获取文件名)
                        string name = Path.GetFileNameWithoutExtension(saveDialog.FileName);
                        //MessageBox.Show(name);
                        //获取扩展名
                        string extract = System.IO.Path.GetExtension(saveFileName);

                        MessageBox.Show(name + extract + "保存成功", "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }
                }
                xlApp.Quit();
                GC.Collect();//强行销毁
            }
            else
            {
                MessageBox.Show("无数据");
            }
        }

    }
}
