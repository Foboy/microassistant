/**
 * @author chenrui
 * @email:mailcray@gmail.com
 * @date: 2012/7/10 11:14:25
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MicroAssistant.Common
{
    /// <summary>
    /// Cookie操作类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="CookieName">要获取键值对的键值</param>
        /// <returns></returns>
        public static string Get(string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cookie != null)
                return cookie.Value;
            else
                return null;
        }

        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="ParentName">Cookie名称</param>
        /// <param name="CookieName">要获取键值对的键值</param>
        /// <returns></returns>
        public static string Get(string ParentName, string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[ParentName];
            if (cookie != null)
                return HttpContext.Current.Request.Cookies[ParentName][CookieName];
            else
                return null;
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="CookieName">要设置键值对的键值</param>
        /// <param name="CookieValue">要设置键值对的值</param>
        /// <param name="ExpiresTime">过期时间，若传入值为 DateTime.MinValue 将不设置过期时间</param>
        public static void Set(string CookieName, string CookieValue, DateTime ExpiresTime)
        {
            HttpCookie cookie = new HttpCookie(CookieName);
            cookie.Value = CookieValue;
            if (ExpiresTime > DateTime.MinValue)
                cookie.Expires = ExpiresTime;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="ParentName">Cookie名称</param>
        /// <param name="CookieName">要设置键值对的键值</param>
        /// <param name="CookieValue">要设置键值对的值</param>
        /// <param name="ExpiresTime">过期时间，若传入值为 DateTime.MinValue 将不设置过期时间</param>
        public static void Set(string ParentName, string CookieName, string CookieValue, DateTime ExpiresTime)
        {
            HttpCookie cookie;
            if (Get(ParentName) == null)
                cookie = new HttpCookie(CookieName);
            else
                cookie = HttpContext.Current.Request.Cookies[ParentName];
            cookie.Values[CookieName] = CookieValue;
            if (ExpiresTime > DateTime.MinValue)
                cookie.Expires = ExpiresTime;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="CookieName">要删除键值对的键值</param>
        public static void Remove(string CookieName)
        {
            System.Web.HttpContext.Current.Response.Cookies[CookieName].Value = null;
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="ParentName">Cookie名称</param>
        /// <param name="CookieName">要删除键值对的键值</param>
        public static void Remove(string ParentName, string CookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[ParentName];
            cookie.Values.Remove(CookieName);
        }
    }
}

