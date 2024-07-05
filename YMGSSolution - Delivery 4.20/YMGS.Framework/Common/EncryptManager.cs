using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Web;

namespace YMGS.Framework
{
    /// <summary>
    /// ���ܹ�����
    /// </summary>
    [SerializableAttribute]
    public class EncryptManager
    {
        /// <summary>
        /// ��ȡ���ܺ���ַ���
        /// </summary>
        /// <param name="strold">ԭ�ַ���</param>
        /// <returns>���ܺ���ַ���</returns>
        public static string GetEncryString(string strold)
        {
            if (strold.Trim() != "")
            {
                ////����MD5����
                //MD5CryptoServiceProvider _md5 = new MD5CryptoServiceProvider();
                //UnicodeEncoding _ascii = new UnicodeEncoding();

                ////��ȡ���ַ�����Byte[]ֵ
                //Byte[] _byte = _ascii.GetBytes(strold);

                ////����
                //Byte[] _byteEncrypt = _md5.ComputeHash(_byte);
                //string _strReturn = BitConverter.ToString(_byteEncrypt);

                ////���ظü��ܺ���ַ���
                //return _strReturn;

                MD5CryptoServiceProvider hashmd5;
                hashmd5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(strold))).Replace("-", "").ToUpper();
            }
            else
            {
                //���Ϊ�գ����ؿ��ַ���
                return "";
            }
        }

        /// <summary>
        /// ͨ��DES�㷨����һ���ַ���(��֧������)
        /// </summary>
        /// <param name="ValueToEnCrypt">��Ҫ�����ܵ�ֵ</param>
        public static String DESEnCrypt(String ValueToEnCrypt)
        {
            DESCryptoServiceProvider DesProv = new DESCryptoServiceProvider();
            return EnCrypt(DesProv, ValueToEnCrypt);
        }

        /// <summary>
        /// ͨ��DES�㷨����һ���ַ���(��֧������)
        /// </summary>
        /// <param name="ValueToDeCrypt">��Ҫ�����ܵ�ֵ</param>
        public static String DESDeCrypt(String ValueToDeCrypt)
        {
            DESCryptoServiceProvider DesProv = new DESCryptoServiceProvider();
            return DeCrypt(DesProv, ValueToDeCrypt);
        }

        /// <summary>
        /// ָ������Palauƽ̨���ܳ�
        /// </summary>
        private static String EncryptionKey = typeof(System.IO.BinaryReader).ToString() + "-w9" +
            typeof(System.Xml.NameTable).ToString() + "sdf3f" + typeof(Random).ToString() + "jsow23j235ay2s" +
            "LFramework.Common.CommonFunction" + "a2skwp230a" + typeof(System.Collections.Queue).ToString() + "�sdadjm" +
            typeof(System.NullReferenceException).ToString();

        /// <summary>
        /// ͨ���ƶ����㷨ģʽ������һ���ַ���(��֧������)
        /// </summary>
        /// <param name="Algorithm">���ܵ��㷨</param>
        /// <param name="ValueToEnCrypt">��Ҫ�����ܵ�ֵ</param>
        private static String EnCrypt(SymmetricAlgorithm Algorithm, String ValueToEnCrypt)
        {
            // ���ַ������浽�ֽ�������
            Byte[] InputByteArray = Encoding.UTF8.GetBytes(ValueToEnCrypt);

            // ����һ��key.
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
        /// ͨ���ƶ����㷨ģʽ������һ���ַ���(��֧������)
        /// </summary>
        /// <param name="Algorithm">���ܵ��㷨</param>
        /// <param name="ValueToDeCrypt">��Ҫ�����ܵ�ֵ</param>
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
        /// ���¶���һ�������б�
        /// </summary>
        /// <param name="OriginalArray">��Ҫ���ض��������</param>
        /// <param name="NewSize">���������´�С</param>
        private static Array ReDim(Array OriginalArray, Int32 NewSize)
        {
            Type ArrayElementsType = OriginalArray.GetType().GetElementType();
            Array newArray = Array.CreateInstance(ArrayElementsType, NewSize);
            Array.Copy(OriginalArray, 0, newArray, 0, Math.Min(OriginalArray.Length, NewSize));
            return newArray;
        }

    }
}
