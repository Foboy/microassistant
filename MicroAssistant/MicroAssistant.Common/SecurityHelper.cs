using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using MicroAssistant.Cache;

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
       /// <summary>
       /// MD5加密
       /// </summary>
       /// <param name="text">原文.</param>
       /// <returns>返回加密结果的小写形式</returns>
       /// <remarks></remarks>
       public static string MD5(string text)
       {
           return MD5(text, Encoding.Default);
       }

       /// <summary>
       /// MD5加密
       /// </summary>
       /// <param name="text">原文.</param>
       /// <param name="encode">字符编码格式.</param>
       /// <returns>返回加密结果的小写形式</returns>
       /// <remarks></remarks>
       public static string MD5(string text, Encoding encode)
       {
           MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
           return BitConverter.ToString(hashmd5.ComputeHash(encode.GetBytes(text))).Replace("-", "").ToLower();
       }

    }
}
