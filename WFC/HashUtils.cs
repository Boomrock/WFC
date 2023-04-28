using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace WFC
{
    internal static class HashUtils
    {
        static private MD5CryptoServiceProvider _MD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        public static byte[] ObjectToByteArray(Object obj)
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
        public static byte[] GetHashObject(object obj)
        {
            return _MD5CryptoServiceProvider.ComputeHash(HashUtils.ObjectToByteArray(obj));
        }

        private static string ByteArrayToString(byte[] arrInput)
        {
            int i;
            StringBuilder sOutput = new StringBuilder(arrInput.Length);
            for (i = 0; i < arrInput.Length - 1; i++)
            {
                sOutput.Append(arrInput[i].ToString("X2"));
            }
            return sOutput.ToString();
        }
        public static int  ByteToInt(byte[]  bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

    }
}