using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace demo
{
    class Folder
    {
        /// <summary>
        /// 选择保存Excel路径
        /// </summary>
        /// <returns></returns>
        public static string  creatFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string foldPath = dialog.SelectedPath;
                DirectoryInfo theFolder = new DirectoryInfo(foldPath);
                return theFolder.FullName.ToString().Trim();
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 删除一月前文件
        /// </summary>
        /// <param name="str"></param>
        public static void Delete(string str)
        {
            DirectoryInfo directory = new DirectoryInfo(str);
            FileInfo[] fileInfos = directory.GetFiles();
            foreach (FileInfo file in fileInfos)
            {
                if (file.LastWriteTime < DateTime.Now.AddMonths(-1))
                {
                    file.Delete();
                }
            }
        }
    }
}
