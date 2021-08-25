using HslCommunication.LogNet;
using System.Windows.Forms;

namespace Log
{
    class GlobalLog
    {
        private static ILogNet logNet = new LogNetDateTime(Application.StartupPath + "\\Logs\\", GenerateMode.ByEveryDay);

        static public void WriteInfoLog(string strMsg)
        {
            logNet.WriteInfo(strMsg);
        }
        static public void WriteWarningLog(string strMsg)
        {
            logNet.WriteWarn(strMsg);
        }
        static public void WriteErrorLog(string strMsg)
        {
            logNet.WriteError(strMsg);
        }
        static public void WriteErrorLog(string strMsg, string strExMsg)
        {
            logNet.WriteError(strMsg + strExMsg);
        }
    }
}
