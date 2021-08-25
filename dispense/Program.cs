using System;
using System.Threading;
using System.Windows.Forms;

namespace dispense
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createNew;
            /* public Mutex(bool initiallyOwned, string name, out bool createdNew); 方法
             * createdNew
             * 在此方法返回时，如果创建了局部互斥体（即，如果 name 为 null 或空字符串）或指定的命名系统互斥体，则包含布尔值 true；如果指定的命名系统互斥体已存在，则为
             * false。 此参数未经初始化即被传递。             
            */
            using (Mutex mutex = new Mutex(true, Application.ProductName, out createNew))
            {
                if (createNew)
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1());
                }
                else
                {
                    // 程序已经运行,显示提示后退出
                    MessageBox.Show("应用程序已经运行!");
                }
            }
        }
    }
}
