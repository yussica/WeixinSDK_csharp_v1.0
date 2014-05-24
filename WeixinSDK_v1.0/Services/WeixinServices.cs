using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeixinSDK_v1._0.Services
{
    public class WeixinServices
    {
        public static string GetCookieValue(string name)
        {
            var value = string.Empty;
            var cookie = HttpContext.Current.Request.Cookies[name];
            //判断cookie
            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                value = HttpUtility.UrlDecode(cookie.Value);
            }
            return value;
        }

        public static void DeleteCookie(string name)
        {
            DateTime dt = DateTime.Now.AddSeconds(-1);
            HttpCookie cookiename = new HttpCookie(name);
            cookiename.Expires = dt;
            HttpContext.Current.Response.Cookies.Add(cookiename);
        }

        public static void AddCookie(string name, string value, DateTime expires)
        {
            HttpCookie cookiename = new HttpCookie(name);
            cookiename.Value = HttpUtility.UrlEncode(value);
            cookiename.Expires = expires;
            HttpContext.Current.Response.Cookies.Add(cookiename);
        }

        public static string AddOpenIdCookie(string openid)
        {
            DateTime dt = DateTime.Now.AddHours(6);
            AddCookie("openid", openid + "|" + dt.ToUnix().ToString(), dt);
            return openid;
        }

        public static void DeleteOpenIdCookie()
        {
            DeleteCookie("openid");
        }

        public static string GetOpenId()
        {
            var openid = GetCookieValue("openid");
            //判断cookie
            if (!string.IsNullOrEmpty(openid))
            {
                var nIndex = openid.IndexOf("|");
                if (nIndex > -1)
                {
                    openid = openid.Substring(0, nIndex);
                }
            }
            return openid;
        }
    }
    
    #region Unix时间戳

    public static class DateTimeExtension
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static int ToUnix(this DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                return -1;
            }
            TimeSpan span = (date - UnixEpoch);
            return (int)Math.Floor(span.TotalSeconds);
        }

        public static DateTime FromUnix(this int anInt)
        {
            if (anInt == -1)
            {
                return DateTime.MinValue;
            }
            return UnixEpoch.AddSeconds(anInt);
        }
    }

    #endregion
}