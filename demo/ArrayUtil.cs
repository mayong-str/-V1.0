using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo
{
    class ArrayUtil
    {
        /// <summary>Int16数组强制类型转float
        /// /Int16[0],[1]/
        /// </summary>
        /// <param name="array">Int16数组</param>
        /// <returns>Int16数组转float</returns>
        public static float I16ToFloat(Int16[] array)
        {
            float[] f = new float[1];
            Buffer.BlockCopy(array, 0, f, 0, 4);    //4个字节
            return f[0];
        }
        /// <summary>float强制类型转Int16数组
        /// 
        /// </summary>
        /// <param name="f">float</param>
        /// <returns>Int16数组</returns>
        public static Int16[] FloatToI16(float f)
        {
            Int16[] i16 = new Int16[2];
            Buffer.BlockCopy(new float[] { f }, 0, i16, 0, 4);  //4个字节
            return i16;
        }
        /// <summary>Int16数组强制类型转Int32
        /// /Int16[0],[1]/
        /// </summary>
        /// <param name="array">Int16数组</param>
        /// <returns>Int32</returns>
        public static Int32 I16ToI32(Int16[] array)
        {
            Int32[] i32 = new Int32[1];
            Buffer.BlockCopy(array, 0, i32, 0, 4);  //4个字节
            return i32[0];
        }
        /// <summary>Int32强制类型转Int16数组
        /// 
        /// </summary>
        /// <param name="i32">Int32</param>
        /// <returns>Int16数组</returns>
        public static Int16[] I32ToI16(Int32 i32)
        {
            Int16[] i16 = new Int16[2];
            Buffer.BlockCopy(new Int32[] { i32 }, 0, i16, 0, 4);    //4个字节
            return i16;
        }
        /// <summary>Int16数组强制类型转Double
        /// /Int16[0],[1],[2],[3]/
        /// </summary>
        /// <param name="array">Int16数组</param>
        /// <returns>Double</returns>
        public static double I16ToDouble(Int16[] array)
        {
            double[] d = new double[1];
            Buffer.BlockCopy(array, 0, d, 0, 8);    //8个字节
            return d[0];
        }
        /// <summary>Int16数组强制类型转String
        /// 
        /// </summary>
        /// <param name="array">Int16数组</param>
        /// <returns>string</returns>
        public static string I16ToString(Int16[] array)
        {
            return I16ToString(array, array.Length * 2);
        }
        /// <summary>Int16数组强制类型转String，
        /// 指定字节数。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string I16ToString(Int16[] array, int length)
        {
            byte[] ba = new byte[length];
            Buffer.BlockCopy(array, 0, ba, 0, length);
            return Encoding.ASCII.GetString(ba);
        }
        /// <summary>String强制类型转Int16数组
        /// 
        /// </summary>
        /// <param name="msg">String</param>
        /// <returns>Int16数组</returns>
        public static Int16[] StringToI16Array(string msg)
        {
            return StringToI16Array(msg, msg.Length);
        }
        /// <summary>String强制类型转Int16数组，
        /// 指定要转换的字节数。
        /// </summary>
        /// <param name="msg">String</param>
        /// <param name="length">字节数</param>
        /// <returns>Int16数组</returns>
        public static Int16[] StringToI16Array(string msg, int length)
        {
            byte[] ba = Encoding.ASCII.GetBytes(msg);
            int len = (int)Math.Ceiling(length / 2.0);
            Int16[] i16a = new Int16[len];
            Buffer.BlockCopy(ba, 0, i16a, 0, length);
            return i16a;
        }

        /// <summary>截取Int16数组
        /// 
        /// </summary>
        /// <param name="srcArray">Int16数组</param>
        /// <param name="index">起点</param>
        /// <param name="size">元素个数</param>
        /// <returns>子Int16数组</returns>
        public static Int16[] CutOut(Int16[] srcArray, int index, int size)
        {
            Int16[] i16 = new Int16[size];
            Array.Copy(srcArray, index, i16, 0, size);
            return i16;
        }


        /// <summary>
        /// 将一个地址值转换成16位
        /// </summary>
        /// <param name="ui16"></param>
        /// <returns></returns>
        public static bool[] GetBitsByInt16(Int16 ui16)
        {
            bool[] result = new bool[16];
            Int16 mask = 1;
            for (int element = 0; element < 16; element++)
            {
                result[element] = ((ui16 & mask) != 0);
                mask <<= 1;
            }
            return result;
        }
    }
}
