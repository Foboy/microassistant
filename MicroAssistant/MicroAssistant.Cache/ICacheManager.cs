/*
 * 创建者: yangchao   创建时间:2011-10-11
 * 文件描述:缓存管理器
 * 修改历史:
 * 2011-10-11 author  description
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.Cache
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 添加缓存 默认保存30分钟
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        void Set(string key, object value);
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="value">缓存值</param>
        /// <param name="cacheTime">缓存时间</param>
        void Set(string key, object value, TimeSpan cacheTime);
        /// <summary>
        /// 获取 缓存值
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        object Get(string key);
        /// <summary>
        /// 是否包含某个键
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        bool Contains(string key);
        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 移除缓存项
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);
    }
}
