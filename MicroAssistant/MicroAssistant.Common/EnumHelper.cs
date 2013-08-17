using System;
using System.Reflection;
using System.ComponentModel;

namespace MicroAssistant.Common
{
    public class EnumHelper
    {
        public static string GetEnumDescription(object enumSubitem)
        {
            enumSubitem = (Enum)enumSubitem;
            string strValue = enumSubitem.ToString();
            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            if (fieldinfo != null)
            {

                Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (objs == null || objs.Length == 0)
                {
                    return strValue;
                }
                else
                {
                    DescriptionAttribute da = (DescriptionAttribute)objs[0];
                    return da.Description;
                }
            }
            else
                return "未知错误";
        }
    }
}
