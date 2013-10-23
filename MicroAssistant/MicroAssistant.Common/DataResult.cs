using System.Runtime.Serialization;
using System.ComponentModel;
using System;
using MicroAssistant.DataStructure;
using System.Collections.Generic;

namespace MicroAssistant.Common
{
    [DataContract(Namespace = "http://MicroAssistant.com/services")]
    public class RespResult
    { 
        [DataMember]
        public AppError Error { get; set; }

        /// <summary>
        /// 新增ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        public String ExMessage {get;set;}

        [DataMember(Name="ErrMsg")]
        public string ErrorMessage
        {
            get 
            {
                if (String.IsNullOrEmpty(ExMessage))
                    return EnumHelper.GetEnumDescription(Error);
                else
                    return ExMessage;
            }
            set { ExMessage = value; }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public RespResult()
        {
            Error = AppError.ERROR_FAILED;
        }

    }
    [DataContract(Namespace = "http://MicroAssistant.com/services")]
    public class AdvancedResult<T> : RespResult
    {
        [DataMember(Name = "Data")]
        public T Data { get; set; } 
    } 


    [Serializable]
    public class PageEntity<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public List<T> Items { get; set; }
        public int RecordsCount { get; set; }

        public PageEntity() 
        {
            this.Items = new List<T>();
        }
    } 

}
