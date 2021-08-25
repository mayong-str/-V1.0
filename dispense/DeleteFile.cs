using System;
using System.IO;

namespace dispense
{
    class DeleteFile
    {
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
