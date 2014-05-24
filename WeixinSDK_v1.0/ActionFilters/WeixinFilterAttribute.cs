using System;
using System.Text;
using System.Web.Mvc;
using WeixinSDK_v1._0.Services;

namespace WeixinSDK_v1._0.ActionFilters
{
    public class WeixinFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var openid = WeixinServices.GetOpenId();
            if (string.IsNullOrEmpty(openid))
            {
                var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
                var qopenid = filterContext.HttpContext.Request["openid"];
                if (!string.IsNullOrEmpty(qopenid))
                {
                    url = url.Replace("openid=" + qopenid, "");
                }
                var refresh = filterContext.HttpContext.Request["refresh"];
                if (!string.IsNullOrEmpty(refresh))
                {
                    url = url.Replace("refresh=" + refresh, "");
                }
                url = url.Replace("?&", "?").Replace("&&", "&");
                if (url.EndsWith("?"))
                    url = url.Remove(url.Length - 1);
                if (url.EndsWith("&"))
                    url = url.Remove(url.Length - 1);
                var buffer = Encoding.UTF8.GetBytes(url);
                var base64 = Convert.ToBase64String(buffer).Replace("/", "-");
                var result = new RedirectResult(string.Format("/OpenAPI/WeixinLogin?CurrentUrl=weixin{0}", base64), false);
                filterContext.Result = result;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class WeixinPhotoFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var openid = WeixinServices.GetOpenId();
            if (string.IsNullOrEmpty(openid))
            {
                var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
                var qopenid = filterContext.HttpContext.Request["openid"];
                if (!string.IsNullOrEmpty(qopenid))
                {
                    url = url.Replace("openid=" + qopenid, "");
                }
                var refresh = filterContext.HttpContext.Request["refresh"];
                if (!string.IsNullOrEmpty(refresh))
                {
                    url = url.Replace("refresh=" + refresh, "");
                }
                url = url.Replace("?&", "?").Replace("&&", "&");
                if (url.EndsWith("?"))
                    url = url.Remove(url.Length - 1);
                if (url.EndsWith("&"))
                    url = url.Remove(url.Length - 1);
                var buffer = Encoding.UTF8.GetBytes(url);
                var base64 = Convert.ToBase64String(buffer).Replace("/", "-");
                var result = new RedirectResult(string.Format("/OpenAPI/WeixinLogin2?CurrentUrl=weixin{0}", base64), false);
                filterContext.Result = result;
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}