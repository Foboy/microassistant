using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MicroAssistant.Common
{
    public static class DBConvert
    {
        #region To Database Value

        public static object ToDBValue(object value)
        {
            object o;

            if (Object.Equals(value, null)) o = DBNull.Value;
            else
            {
                Type type = value.GetType();
                if (type.IsEnum)
                {
                    o = Convert.ToInt32(value);
                }
                else
                    throw new Exception("数据库类型转换失败:未知的数据类型");
            }
            return o;
        }

        public static object ToDBValue(string value)
        {
            if (String.IsNullOrEmpty(value)) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(int value)
        {
            if (value == -1) return DBNull.Value;
            else if (value == Int32.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(long value)
        {
            if (value == 0L) return DBNull.Value;
            else if (value == Int64.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(float value)
        {
            if (value == 0.0F) return DBNull.Value;
            else if (value == Single.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(double value)
        {
            if (value == 0.0) return DBNull.Value;
            else if (value == Double.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(decimal value)
        {
            if (value == 0.0M) return DBNull.Value;
            else if (value == Decimal.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(DateTime value)
        {
            if (value == DateTime.MinValue) return DBNull.Value;
            else return value;
        }

        public static object ToDBValue(bool value)
        {
            if (value) return 1;
            else return 0;
        }

        public static object ToDBValue(Guid value)
        {
            if (Object.Equals(value, Guid.Empty)) return DBNull.Value;
            else return ToString(value);
        }

        public static object ToDBValue(byte[] bytes)
        {
            return ToString(bytes);
        }

        #endregion

        #region To .NET Type

        public static string ToString(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return null;
                return Convert.ToString(value.Value);
            }

            if (Convert.IsDBNull(value)) return null;
            else if (Object.Equals(value, null)) return null;
            else if (value.GetType() == typeof(DateTime)) return ToDate(value);
            else return Convert.ToString(value);
        }

        public static string ToString(Guid value)
        {
            return value.ToString("N");
        }

        public static string ToString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            string p;
            foreach (byte b in bytes)
            {
                p = Convert.ToString(b, 16);
                if (p.Length == 2)
                    sb.Append(p);
                else
                {
                    sb.Append("0");
                    sb.Append(p);
                }

            }
            return sb.ToString();
        }

        public static string ToIP(object value)
        {
            string ip = ToString(value);
            if (!String.IsNullOrEmpty(ip))
                return Regex.Replace(ip, @"\d+$", "*");
            else
                return String.Empty;
        }

        public static byte ToByte(object value)
        {
            if (Convert.IsDBNull(value)) return Convert.ToByte(0);
            else return Convert.ToByte(value);
        }

        public static byte[] ToBypes(object value)
        {
            if (Convert.IsDBNull(value)) return new byte[] { };
            else
            {
                return (byte[])(value);
            }
        }

        public static byte[] ToBypes(string value)
        {
            List<byte> list = new List<byte>();
            string v = Convert.ToString(value);
            for (int index = 0; index < v.Length / 2; index++)
            {
                list.Add(Convert.ToByte(v.Substring(index * 2, 2), 16));
            }
            return list.ToArray();
        }

        public static int ToInt16(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0;
                return Convert.ToInt16(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0;
            else return Convert.ToInt16(value);
        }

        public static int ToInt32(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0;
                return Convert.ToInt32(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0;
            else return Convert.ToInt32(value);
        }

        public static long ToInt64(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0L;
                return Convert.ToInt64(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0L;
            else return Convert.ToInt64(value);
        }

        public static float ToSingle(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0.0F;
                return Convert.ToSingle(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0.0F;
            else return Convert.ToSingle(value);
        }

        public static double ToDouble(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0.0;
                return Convert.ToDouble(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0.0;
            else return Convert.ToDouble(value);
        }

        public static decimal ToDecimal(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return 0.0M;
                return Convert.ToDecimal(value.Value);
            }
            if (Convert.IsDBNull(value)) return 0.0M;
            else return Convert.ToDecimal(value);
        }

        public static DateTime ToDateTime(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return DateTime.MinValue;
                return Convert.ToDateTime(value.Value);
            }
            if (Convert.IsDBNull(value)) return DateTime.MinValue;
            else return Convert.ToDateTime(value);

        }

        /// <summary>
        /// 将数据库日期时间对象转换为{MM-dd HH:mm}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 今天 15:30</para>
        /// <para>如:2001-12-29 15:30:34 >> 昨天 15:30</para>
        /// <para>如:2001-12-28 15:30:34 >> 前天 15:30</para>
        /// <para>如:2001-12-27 15:30:34 >> 12-30 15:30</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTime(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return ToShortDateTime(Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{MM-dd HH:mm}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 今天 15:30</para>
        /// <para>如:2001-12-29 15:30:34 >> 昨天 15:30</para>
        /// <para>如:2001-12-28 15:30:34 >> 前天 15:30</para>
        /// <para>如:2001-12-27 15:30:34 >> 12-30 15:30</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDateTime(DateTime value)
        {
            if (String.Equals(value.Date, DateTime.Today)) return String.Format("今天 {0:HH:mm}", Convert.ToDateTime(value));
            if (String.Equals(value.Date, DateTime.Today.AddDays(-1))) return String.Format("昨天 {0:HH:mm}", Convert.ToDateTime(value));
            if (String.Equals(value.Date, DateTime.Today.AddDays(-2))) return String.Format("前天 {0:HH:mm}", Convert.ToDateTime(value));
            else return String.Format("{0:MM-dd HH:mm}", value);
        }


        /// <summary>
        /// 将数据库日期时间对象转换为{yyyy-MM-dd HH:mm:ss}格式字符串(完整格式)
        /// <para>如:2001-12-30 15:30:34 >> 2001-12-30 15:30:34</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLongDateTime(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:yyyy-MM-dd HH:mm:ss}", Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{yy-MM-dd HH:mm}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 01-12-30 15:30</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLocalDateTime(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:yy-MM-dd HH:mm}", Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{yyyy-MM-dd}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 2001-12-30</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToDate(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:yyyy-MM-dd}", Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{MM-dd}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 12-30</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortDate(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:MM-dd}", Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{HH:mm:ss}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 15:30:34</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToTime(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:HH:mm:ss}", Convert.ToDateTime(value));
        }

        /// <summary>
        /// 将数据库日期时间对象转换为{HH:mm:ss}格式字符串
        /// <para>如:2001-12-30 15:30:34 >> 15:30:34</para>
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToShortTime(object value)
        {
            if (Convert.IsDBNull(value)) return null;
            else if (Convert.ToDateTime(value) == DateTime.MinValue) return null;
            else return String.Format("{0:HH:mm}", Convert.ToDateTime(value));
        }

        public static bool ToBoolean(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return false;
                else return Convert.ToBoolean(value.Value);
            }

            if (Convert.IsDBNull(value)) return false;
            else return Convert.ToBoolean(value);
        }

        public static Guid ToGuid(dynamic value)
        {
            if (value.GetType().FullName.Contains("MySql.Data.Types"))
            {
                if (value.IsNull) return Guid.Empty;
                else return new Guid(Convert.ToString(value.Value));
            }
            if (Convert.IsDBNull(value)) return Guid.Empty;
            if (Object.Equals(value, null)) return Guid.Empty;
            else return new Guid(Convert.ToString(value));
        }

        #endregion
    }
}
