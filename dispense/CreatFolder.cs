using System.IO;

namespace dispense
{
    class CreatFolder
    {
        /// <summary>
        /// 在指定共享路径文件夹下生成AIR文件夹
        /// </summary>
        /// <param name="str">指定的共享路径</param>
        /// <returns></returns>
        public static string CreatAirFolder(string str)
        {
            string air = str + "\\AIR";
            if (!Directory.Exists(air))
            {
                Directory.CreateDirectory(air);
            }
            return air;
        }
        /// <summary>
        /// 在指定共享路径文件夹下生成GAS文件夹
        /// </summary>
        /// <param name="str">指定的共享路径</param>
        /// <returns></returns>
        public static string CreatGasFolder(string str)
        {
            string gas = str + "\\GAS";
            if (!Directory.Exists(gas))
            {
                Directory.CreateDirectory(gas);
            }
            return gas;
        }
        /// <summary>
        /// 在指定共享路径文件夹下生成POWER文件夹
        /// </summary>
        /// <param name="str">指定的共享路径</param>
        /// <returns></returns>
        public static string CreatPowerFolder(string str)
        {
            string power = str + "\\POWER";
            if (!Directory.Exists(power))
            {
                Directory.CreateDirectory(power);
            }
            return power;
        }
        /// <summary>
        /// 在指定共享路径文件夹下生成WATER文件夹
        /// </summary>
        /// <param name="str">指定的共享路径</param>
        /// <returns></returns>
        public static string CreatWaterFolder(string str)
        {
            string water = str + "\\WATER";
            if (!Directory.Exists(water))
            {
                Directory.CreateDirectory(water);
            }
            return water;
        }



        /// <summary>
        /// 在指定本地路径文件夹下生成AIR文件夹
        /// </summary>
        /// <param name="defaultstr">指定的本地路径</param>
        /// <returns></returns>
        public static string CreatDefaultAIRFolder(string defaultstr)
        {
            string defaultair = defaultstr + "\\AIR";
            if (!Directory.Exists(defaultair))
            {
                Directory.CreateDirectory(defaultair);
            }
            return defaultair;
        }
        /// <summary>
        /// 在指定本地路径文件夹下生成GAS文件夹
        /// </summary>
        /// <param name="defaultstr">指定的本地路径</param>
        /// <returns></returns>
        public static string CreatDefaultGASFolder(string defaultstr)
        {
            string defaultgas = defaultstr + "\\GAS";
            if (!Directory.Exists(defaultgas))
            {
                Directory.CreateDirectory(defaultgas);
            }
            return defaultgas;
        }
        /// <summary>
        /// 在指定本地路径文件夹下生成POWER文件夹
        /// </summary>
        /// <param name="defaultstr">指定的本地路径</param>
        /// <returns></returns>
        public static string CreatDefaultPOWERFolder(string defaultstr)
        {
            string defaultpower = defaultstr + "\\POWER";

            if (!Directory.Exists(defaultpower))
            {
                Directory.CreateDirectory(defaultpower);
            }
            return defaultpower;
        }
        /// <summary>
        /// 在指定本地路径文件夹下生成WATER文件夹
        /// </summary>
        /// <param name="defaultstr">指定的本地路径</param>
        /// <returns></returns>
        public static string CreatDefaultWATERFolder(string defaultstr)
        {
            string defaultwater = defaultstr + "\\WATER";
            if (!Directory.Exists(defaultwater))
            {
                Directory.CreateDirectory(defaultwater);
            }
            return defaultwater;
        }
    }
}
