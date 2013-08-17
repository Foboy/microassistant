using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;

namespace MicroAssistant.Cache
{
    /// <summary>
    /// 缓存管理器创建工厂
    /// </summary>
    public class CacheManagerFactory : CachConfigHandle
    {
        private readonly static CacheManagerFactory obj = new CacheManagerFactory();
        private  static CacheManDepository _cacheManinstance = null;
        private  static MemoryCacheDepository _memoryCacheinstance = null;
        public CacheManagerFactory()
            : base()
        {
            
        }

        private static CacheManDepository CacheManinstance
        {
            get {
                if (_cacheManinstance == null)
                    _cacheManinstance = new CacheManDepository(servers);

                return _cacheManinstance;
            }
        }
        private static MemoryCacheDepository MemoryCacheinstance
        {
            get
            {
                if (_memoryCacheinstance == null)
                    _memoryCacheinstance = new MemoryCacheDepository();

                return _memoryCacheinstance;
            }
        }
        /// <summary>
        /// 获取内存缓存管理器
        /// </summary>
        /// <returns></returns>
        public static ICacheManager GetMemoryManager()
        {
            if (OpenCache)
            {
                if (UseCacheMan)
                {
                    return CacheManinstance;
                }
                else if (UseMemoryCache)
                {
                    return MemoryCacheinstance;
                }
                else //TODO:有待改进
                {
                    return MemoryCacheinstance;
                }

            }
            else//TODO:有待改进
            {
                return MemoryCacheinstance;
            }
        }

    }
}
