using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;

namespace MicroAssistant.Cache
{
    public class CachConfigHandle
    {
        protected static IPEndPoint[] servers = { };
        protected static bool OpenCache = false;
        protected static bool UseCacheMan = false;
        protected static bool UseMemoryCache = false;
        protected static XmlDocument xml = null;
        public CachConfigHandle()
        {
            ReadCacheConfig();
        }
        /// <summary>
        /// 读取cacheman配置
        /// </summary>
        /// <returns></returns>
        private void ReadCacheConfig()
        {
            if (xml == null)
            {
                // ----------------------------------------------------     修改前： Config/CacheMan.config      ------------------------
                string filePath = System.AppDomain.CurrentDomain.BaseDirectory + "Config/Cache.config";
                if (System.IO.File.Exists(filePath) == false) throw new FileNotFoundException("Cache配置文件没有找到", filePath);
                string cacheConfig = System.IO.File.ReadAllText(filePath);
                xml = new XmlDocument();
                xml.LoadXml(cacheConfig);
            }
            try
            {
                XmlNode modenode = xml.SelectSingleNode("//Cache");

                if (modenode.Attributes["OpenCache"].Value == "on")
                {
                    OpenCache = true;

                    XmlNode cnode = xml.SelectSingleNode("//Cache/CacheMan");
                    if (cnode.Attributes["OpenCache"].Value == "on")
                    {
                        UseCacheMan = true;
                        foreach (XmlNode node in xml.SelectNodes("//server"))
                        {
                            List<IPEndPoint> server = new List<IPEndPoint>();
                            var ip = node.InnerText.Split(':');
                            if (ip.Length == 2)
                            {
                                server.Add(new IPEndPoint(IPAddress.Parse(ip[0]), int.Parse(ip[1])));
                            }
                            else
                            {
                                server.Add(new IPEndPoint(IPAddress.Parse(ip[0]), 16180));
                            }
                            servers = server.ToArray();
                        }
                    }

                    XmlNode mnode = xml.SelectSingleNode("//Cache/MemoryCache");
                    if (modenode.Attributes["OpenCache"].Value == "on")
                    {
                        UseMemoryCache = true;
                    }

                }


            }
            catch
            {
                throw new InvalidDataException("Cache配置文件内容不正确");
            }
        }
    }
}
