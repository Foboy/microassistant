
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MicroAssistant.Common
    {
    /// <summary>
    /// json序列化帮助类
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为json字符串
        /// </summary>
        /// <param name="value">需要序列化的对象</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string Serialize(object value)
        {
            Newtonsoft.Json.Converters.IsoDateTimeConverter time = new IsoDateTimeConverter() { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(value,time); 
        }
        /// <summary>
        /// 将json字符串反序列化为对象
        /// </summary>
        /// <typeparam name="T">对象的返回类型</typeparam>
        /// <param name="json">需要序列化的字符串</param>
        /// <returns></returns>
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}


