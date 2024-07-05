using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YMGS.Framework.Common
{
    public class KaiSaEncrypt
    {
        public static string Encrypt(string text,int key)
        {
            string encrptString = string.Empty;
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {

                    char newChar = (char)(c + key);
                    if (!char.IsLetter(newChar))
                    {
                        newChar -= (char)26;
                    }
                    encrptString += newChar;
                }
                else
                {
                    encrptString += c;
                }
            }
            return encrptString;
        }

        public static string Decrypt(string text,int key)
        {
            string decrptString = string.Empty;
            foreach (char c in text)
            {
                if (char.IsLetter(c))
                {

                    char newChar = (char)(c - key);
                    if (!char.IsLetter(newChar))
                    {
                        newChar += (char)26;
                    }
                    decrptString += newChar;
                }
                else
                {
                    decrptString += c;
                }
            }
            return decrptString;
        }
    }
}
