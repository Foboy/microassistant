/*
 * 创建者: yangchao   创建时间:2011-10-11
 * 文件描述:内存缓存数据存储器
 * 修改历史:
 * 2011-10-11 author  description
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Caching;

namespace MicroAssistant.Cache
{
    /// <summary>
    /// 内存缓存数据存储器
    /// </summary>
    public class MemoryCacheDepository : ICacheManager
    {
        //private static Dictionary<string, object> _depository = new Dictionary<string, object>();

        private MemoryCache _depository = MemoryCache.Default;
        

        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">数据</param>
        /// <remarks></remarks>
        public void Set(string key, object value)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = new TimeSpan(1, 0, 0);

            if (!string.IsNullOrEmpty(key))
            {
            if (!Contains(key))
                _depository.Add(key, value, policy);
            else
                _depository[key] = value;
            }
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.SlidingExpiration = cacheTime;

            if (!string.IsNullOrEmpty(key))
            {
                if (!Contains(key))
                    _depository.Add(key, value, policy);
                else
                    _depository[key] = value;
            }
        }

        /// <summary>
        /// 是否包含某个键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns><c>true</c> if [contains] [the specified key]; otherwise, <c>false</c>.</returns>
        /// <remarks></remarks>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            else
            {
                return _depository.Contains(key);
            }
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="key">键</param>
        /// <remarks></remarks>
        public void Remove(string key)
        {
            if (Contains(key))
                _depository.Remove(key);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public object Get(string key)
        {
            //object value;

            return _depository.Get(key);
        }


        public T Get<T>(string key)
        {
            return (T)_depository.Get(key);
        }



    }
}
