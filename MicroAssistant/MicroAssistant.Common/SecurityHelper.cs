using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using MicroAssistant.Cache;
using System.IO;

namespace MicroAssistant.Common
{
    public static class SecurityHelper
    {
       public static string GetToken(string userid)
       {
           string token = string.Empty;
           token=MD5(userid+System.DateTime.Now.Ticks.ToString());
           CacheManagerFactory.GetMemoryManager().Set(token, userid,new TimeSpan(1,0,0,0));
           return token;
       }
       ///// <summary>
       ///// MD5加密
       ///// </summary>
       ///// <param name="text">原文.</param>
       ///// <returns>返回加密结果的小写形式</returns>
       ///// <remarks></remarks>
       //public static string MD5(string text)
       //{
       //    return MD5(text, Encoding.Default);
       //}

       ///// <summary>
       ///// MD5加密
       ///// </summary>
       ///// <param name="text">原文.</param>
       ///// <param name="encode">字符编码格式.</param>
       ///// <returns>返回加密结果的小写形式</returns>
       ///// <remarks></remarks>
       //public static string MD5(string text, Encoding encode)
       //{
       //    MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
       //    return BitConverter.ToString(hashmd5.ComputeHash(encode.GetBytes(text))).Replace("-", "").ToLower();
       //}

       /// <summary>
       /// MD5函数
       /// </summary>
       /// <param name="input">原始字符串</param>
       /// <returns>MD5结果</returns>
       public static string MD5(string input)
       {
           byte[] bytes = Encoding.UTF8.GetBytes(input);
           bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
           string result = string.Empty;
           for (int i = 0; i < bytes.Length; i++)
               result += bytes[i].ToString("x").PadLeft(2, '0');

           return result;
       }

       /// <summary>
       /// SHA256函数
       /// </summary>
       /// /// <param name="input">原始字符串</param>
       /// <returns>SHA256结果</returns>
       public static string SHA256(string input)
       {
           byte[] data = Encoding.UTF8.GetBytes(input);
           SHA256Managed sha256 = new SHA256Managed();
           byte[] result = sha256.ComputeHash(data);
           return Convert.ToBase64String(result);  //返回长度为44字节的字符串
       }

       //默认密钥向量
       private static byte[] AESKeys = { 0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F };

       public static string AESEncode(string input, string key)
       {
           key = GetSubString(key, 32, string.Empty);
           key = key.PadRight(32, ' ');

           RijndaelManaged rijndaelProvider = new RijndaelManaged();
           rijndaelProvider.Key = Encoding.UTF8.GetBytes(key.Substring(0, 32));
           rijndaelProvider.IV = AESKeys;
           ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();

           byte[] inputData = Encoding.UTF8.GetBytes(input);
           byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);

           return Convert.ToBase64String(encryptedData);
       }

       public static string AESDecode(string input, string key)
       {
           try
           {
               key = GetSubString(key, 32, string.Empty);
               key = key.PadRight(32, ' ');

               RijndaelManaged rijndaelProvider = new RijndaelManaged();
               rijndaelProvider.Key = Encoding.UTF8.GetBytes(key);
               rijndaelProvider.IV = AESKeys;
               ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();

               byte[] inputData = Convert.FromBase64String(input);
               byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);

               return Encoding.UTF8.GetString(decryptedData);
           }
           catch
           {
               return string.Empty;
           }

       }

       //默认密钥向量
       private static byte[] DESKeys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

       /// <summary>
       /// DES加密字符串
       /// </summary>
       /// <param name="input">待加密的字符串</param>
       /// <param name="key">加密密钥,要求为8位</param>
       /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
       public static string DESEncode(string input, string key)
       {
           key = GetSubString(key, 8, string.Empty);
           key = key.PadRight(8, ' ');
           byte[] rgbKey = Encoding.UTF8.GetBytes(key.Substring(0, 8));
           byte[] rgbIV = DESKeys;
           byte[] inputByteArray = Encoding.UTF8.GetBytes(input);
           DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
           MemoryStream memory = new MemoryStream();
           CryptoStream stream = new CryptoStream(memory, provider.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
           stream.Write(inputByteArray, 0, inputByteArray.Length);
           stream.FlushFinalBlock();
           return Convert.ToBase64String(memory.ToArray());

       }

       /// <summary>
       /// DES解密字符串
       /// </summary>
       /// <param name="input">待解密的字符串</param>
       /// <param name="key">解密密钥,要求为8位,和加密密钥相同</param>
       /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
       public static string DESDecode(string input, string key)
       {
           try
           {
               key = GetSubString(key, 8, string.Empty);
               key = key.PadRight(8, ' ');
               byte[] rgbKey = Encoding.UTF8.GetBytes(key);
               byte[] rgbIV = DESKeys;
               byte[] inputByteArray = Convert.FromBase64String(input);
               DESCryptoServiceProvider provider = new DESCryptoServiceProvider();

               MemoryStream memory = new MemoryStream();
               CryptoStream stream = new CryptoStream(memory, provider.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
               stream.Write(inputByteArray, 0, inputByteArray.Length);
               stream.FlushFinalBlock();
               return Encoding.UTF8.GetString(memory.ToArray());
           }
           catch
           {
               return string.Empty;
           }
       }

       public static string GetSubString(string input, int length, string pad)
       {
           return GetSubString(input, 0, length, pad);
       }

       /// <summary>
       /// 取指定长度的字符串
       /// </summary>
       /// <param name="input">要检查的字符串</param>
       /// <param name="start">起始位置</param>
       /// <param name="length">指定长度</param>
       /// <param name="pad">用于替换的字符串</param>
       /// <returns>截取后的字符串</returns>
       public static string GetSubString(string input, int start, int length, string pad)
       {
           string result = input;

           Byte[] comments = Encoding.UTF8.GetBytes(input);
           foreach (char c in Encoding.UTF8.GetChars(comments))
           {    //当是日文或韩文时(注:中文的范围:\u4e00 - \u9fa5, 日文在\u0800 - \u4e00, 韩文为\xAC00-\xD7A3)
               if ((c > '\u0800' && c < '\u4e00') || (c > '\xAC00' && c < '\xD7A3'))
               {
                   //if (System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\u0800-\u4e00]+") || System.Text.RegularExpressions.Regex.IsMatch(p_SrcString, "[\xAC00-\xD7A3]+"))
                   //当截取的起始位置超出字段串长度时
                   if (start >= input.Length)
                       return string.Empty;
                   else
                       return input.Substring(start, ((length + start) > input.Length) ? (input.Length - start) : length);
               }
           }

           if (length >= 0)
           {
               byte[] bytes = Encoding.Default.GetBytes(input);

               //当字符串长度大于起始位置
               if (bytes.Length > start)
               {
                   int stopIndex = bytes.Length;

                   //当要截取的长度在字符串的有效长度范围内
                   if (bytes.Length > (start + length))
                   {
                       stopIndex = length + start;
                   }
                   else
                   {   //当不在有效范围内时,只取到字符串的结尾

                       length = bytes.Length - start;
                       pad = string.Empty;
                   }

                   int realLength = length;
                   int[] resultFlag = new int[length];
                   byte[] byteResult = null;

                   int flag = 0;
                   for (int i = start; i < stopIndex; i++)
                   {
                       if (bytes[i] > 127)
                       {
                           flag++;
                           if (flag == 3)
                               flag = 1;
                       }
                       else
                           flag = 0;

                       resultFlag[i] = flag;
                   }

                   if ((bytes[stopIndex - 1] > 127) && (resultFlag[length - 1] == 1))
                       realLength = length + 1;

                   byteResult = new byte[realLength];

                   Array.Copy(bytes, start, byteResult, 0, realLength);

                   result = Encoding.Default.GetString(byteResult);
                   result = result + pad;
               }
           }

           return result;
       }


    }
}
