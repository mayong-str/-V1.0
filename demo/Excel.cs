using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    class Excel
    {
        public static void ExportExcel(DataTable dt,string path)
        {
            //设置导出文件路径     
            //string path = Application.StartupPath + "\\excel\\";
            //设置新建文件路径及名称
            string savePath = path + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".xls";
            Console.WriteLine("保存Excel路径"+savePath);
            //创建文件
            FileStream file = new FileStream(savePath, FileMode.CreateNew, FileAccess.Write);

            //以指定的字符编码向指定的流写入字符
            StreamWriter sw = new StreamWriter(file, Encoding.GetEncoding("GB2312"));

            StringBuilder strbu = new StringBuilder();

            //写入标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                strbu.Append(dt.Columns[i].ColumnName.ToString() + "\t");
            }
            //加入换行字符串
            strbu.Append(Environment.NewLine);

            //写入内容
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    strbu.Append(dt.Rows[i][j].ToString() + "\t");
                }
                strbu.Append(Environment.NewLine);
            }

            sw.Write(strbu.ToString());
            sw.Flush(); //刷新缓冲区
            file.Flush();

            sw.Close();
            sw.Dispose(); //释放使用的资源

            file.Close();
            file.Dispose();
        }
    }
}
