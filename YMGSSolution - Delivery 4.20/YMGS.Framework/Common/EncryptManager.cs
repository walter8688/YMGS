using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace YMGS.Framework
{
    /// <summary>
    /// 加密管理类
    /// </summary>
    [SerializableAttribute]
    public class EncryptManager
    {
        /// <summary>
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="strold">原字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetEncryString(string strold)
        {
            if (strold.Trim() != "")
            {
                ////采用MD5加密
                //MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
                //UnicodeEncoding _ascii = new UnicodeEncoding();

                ////获取该字符串的Byte[]值
                //Byte[] _byte = _ascii.GetBytes(strold);

                ////加密
                //Byte[] _byteEncrypt = _md5.ComputeHash(_byte);
                //string _strReturn = BitConverter.ToString(_byteEncrypt);

                ////返回该加密后的字符串
                //return _strReturn;

                MD5CryptoServiceProvider hashmd5;
                hashmd5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(strold))).Replace("-", "").ToUpper();
            }
            else
            {
                //如果为空，返回空字符串
                return "";
            }
        }

        /// <summary>
        /// 通过DES算法加密一个字符串(不支持中文)
        /// </summary>
        /// <param name="ValueToEnCrypt">需要被加密的值</param>
        public static String DESEnCrypt(String ValueToEnCrypt)
        {
            DESCryptoServiceProvider DesProv = new DESCryptoServiceProvider();
            return EnCrypt(DesProv, ValueToEnCrypt);
        }

        /// <summary>
        /// 通过DES算法解密一个字符串(不支持中文)
        /// </summary>
        /// <param name="ValueToDeCrypt">需要被解密的值</param>
        public static String DESDeCrypt(String ValueToDeCrypt)
        {
            DESCryptoServiceProvider DesProv = new DESCryptoServiceProvider();
            return DeCrypt(DesProv, ValueToDeCrypt);
        }

        /// <summary>
        /// 指定整个Palau平台的密匙
        /// </summary>
        private static String EncryptionKey = typeof(System.IO.BinaryReader).ToString() + "-w9" +
            typeof(System.Xml.NameTable).ToString() + "sdf3f" + typeof(Random).ToString() + "jsow23j235ay2s" +
            "LFramework.Common.CommonFunction" + "a2skwp230a" + typeof(System.Collections.Queue).ToString() + "黶dadjm" +
            typeof(System.NullReferenceException).ToString();

        /// <summary>
        /// 通过制定的算法模式来加密一个字符串(不支持中文)
        /// </summary>
        /// <param name="Algorithm">加密的算法</param>
        /// <param name="ValueToEnCrypt">将要被加密的值</param>
        private static String EnCrypt(SymmetricAlgorithm Algorithm, String ValueToEnCrypt)
        {
            // 将字符串保存到字节数组中
            Byte[] InputByteArray = Encoding.UTF8.GetBytes(ValueToEnCrypt);

            // 创建一个key.
            Byte[] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
            Algorithm.Key = (Byte[])ReDim(Key, Algorithm.Key.Length);
            Algorithm.IV = (Byte[])ReDim(Key, Algorithm.IV.Length);

            MemoryStream MemStream = new MemoryStream();
            CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateEncryptor(), CryptoStreamMode.Write);

            // Write the byte array into the crypto stream( It will end up in the memory stream).
            CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
            CrypStream.FlushFinalBlock();

            // Get the data back from the memory stream, and into a string.
            StringBuilder StringBuilder = new StringBuilder();
            for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
            {
                Byte ActualByte = MemStream.ToArray()[i];

                // Format the actual byte as HEX.
                StringBuilder.AppendFormat("{0:X2}", ActualByte);
            }

            return StringBuilder.ToString();
        }

        /// <summary>
        /// 通过制定的算法模式来加密一个字符串(不支持中文)
        /// </summary>
        /// <param name="Algorithm">解密的算法</param>
        /// <param name="ValueToDeCrypt">将要被解密的值</param>
        private static String DeCrypt(SymmetricAlgorithm Algorithm, String ValueToDeCrypt)
        {
            // Put the input string into the byte array.
            Byte[] InputByteArray = new Byte[ValueToDeCrypt.Length / 2];

            for (Int32 i = 0; i < ValueToDeCrypt.Length / 2; i++)
            {
                Int32 Value = (Convert.ToInt32(ValueToDeCrypt.Substring(i * 2, 2), 16));
                InputByteArray[i] = (Byte)Value;
            }

            // Create the key.
            Byte[] Key = ASCIIEncoding.ASCII.GetBytes(EncryptionKey);
            Algorithm.Key = (Byte[])ReDim(Key, Algorithm.Key.Length);
            Algorithm.IV = (Byte[])ReDim(Key, Algorithm.IV.Length);

            MemoryStream MemStream = new MemoryStream();
            CryptoStream CrypStream = new CryptoStream(MemStream, Algorithm.CreateDecryptor(), CryptoStreamMode.Write);

            // Flush the data through the crypto stream into the memory stream.
            CrypStream.Write(InputByteArray, 0, InputByteArray.Length);
            CrypStream.FlushFinalBlock();

            // Get the decrypted data back from the memory stream.
            StringBuilder StringBuilder = new StringBuilder();

            for (Int32 i = 0; i < MemStream.ToArray().Length; i++)
            {
                StringBuilder.Append((Char)MemStream.ToArray()[i]);
            }

            return StringBuilder.ToString();
        }


        /// <summary>
        /// 重新定义一个数组列表
        /// </summary>
        /// <param name="OriginalArray">需要被重定义的数组</param>
        /// <param name="NewSize">这个数组的新大小</param>
        private static Array ReDim(Array OriginalArray, Int32 NewSize)
        {
            Type ArrayElementsType = OriginalArray.GetType().GetElementType();
            Array newArray = Array.CreateInstance(ArrayElementsType, NewSize);
            Array.Copy(OriginalArray, 0, newArray, 0, Math.Min(OriginalArray.Length, NewSize));
            return newArray;
        }

    }
}
