
/*
 * 创建者: yangchao   创建时间:2011-10-11
 * 文件描述:实现缓存数据存储器接口方法的类
 * 修改历史:
 * 2011-10-11 author  description
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using CachemanAPI;

namespace MicroAssistant.Cache
{
    public class CacheManDepository : ICacheManager
    {
        private CachemanClient _cachemanClient = null;

        public CacheManDepository(IPEndPoint[] servers)
        {
            _cachemanClient = new CachemanClient(servers);
            _cachemanClient.Connect();
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">数据</param>
        public void Set(string key, object value)
        {
            _cachemanClient.Set(key, value, -1);
        }
        /// <summary>
        /// 存储数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">数据</param>
        public void Set(string key, object value, TimeSpan cacheTime)
        {
            _cachemanClient.Set(key, value, cacheTime.Seconds);
        }

        /// <summary>
        /// 是否包含某个键
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            bool flag=false;
            try
            {
                if (Get(key) != null)
                    flag = true;
            }
            catch
            {
            }
            return flag;
        }

        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="key">键</param>
        public void Remove(string key)
        {
            _cachemanClient.Delete(key);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public object Get(string key)
        {
            return _cachemanClient.Get(key);
        }
        public T Get<T>(string key)
        {
            return (T)(_cachemanClient.Get(key));
        }

    }
}
