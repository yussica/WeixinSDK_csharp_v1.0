using System.Configuration;
using System.Web.Mvc;
using WeixinUtils.Utils;
using System;
using System.Text;
using System.IO;
using System.Net;

namespace WeixinSDK_v1._0.Controllers
{
    public class OpenAPIController : Controller
    {
        OAuthUtil oauth = new OAuthUtil();

        public ActionResult WeixinLogin(string CurrentUrl)
        {
            if (string.IsNullOrEmpty(CurrentUrl))
                CurrentUrl = "/Home/Index";
            var oauth = new OAuthUtil();
            string url = oauth.snsapi_base(Server.UrlEncode(CurrentUrl));
            return RedirectPermanent(url);
        }

        public ActionResult WeixinLogin2(string CurrentUrl)
        {
            if (string.IsNullOrEmpty(CurrentUrl))
                CurrentUrl = "/Home/Index";
            string url = oauth.snsapi_userinfo(Server.UrlEncode(CurrentUrl));
            return RedirectPermanent(url);
        }

        public ActionResult WeixinCallback()
        {
            if (!string.IsNullOrEmpty(Request["code"]))
            {
                // 获取AccessToken参数
                var param = oauth.access_token(Request["code"]);
                string url = string.Format("{0}#access_token={1}&openid={2}&expires_in={3}&state={4}", ConfigurationManager.AppSettings["Callback"], param.access_token, param.openid, param.expires_in, Server.UrlDecode(Request.QueryString["state"]));
                //重新跳转到回调页面，保持腾讯登录相同风格
                return Redirect(url);
            }
            return View();
        }

        [HttpPost]
        public ActionResult WeixinSignin(string hash)
        {
            var openModel = oauth.userinfo(hash);
            if (openModel == null)
                RedirectToAction("Home", "Index");

            var starts = "weixinphoto";
            // 自动跳转到原始url（头像）
            if (openModel.ReturnUrl.StartsWith(starts))
            {
                var avatar = string.Empty;
                if (!string.IsNullOrEmpty(openModel.headimgurl))
                {
                    string oriName = DownloadImage(openModel.openid, openModel.headimgurl);
                    GetBigImg(oriName, openModel.openid);
                    GetSmallImg(oriName, openModel.openid);
                    avatar = oriName;
                }

                var args = openModel.ReturnUrl.Remove(0, starts.Length);
                var buffer = Convert.FromBase64String(args.Replace("-", "/"));
                var url = Encoding.UTF8.GetString(buffer);
                var uri = new Uri(url);
                url = string.Format("{0}://{1}/Account/Weixin2/{2}?openid={3}&avatar={4}&refresh=1", uri.Scheme, uri.Host, args, openModel.openid, avatar);
                return RedirectPermanent(url);
            }

            starts = "weixin";
            // 自动跳转到原始url
            if (openModel.ReturnUrl.StartsWith(starts))
            {
                var args = openModel.ReturnUrl.Remove(0, starts.Length);
                var buffer = Convert.FromBase64String(args.Replace("-", "/"));
                var url = Encoding.UTF8.GetString(buffer);
                var uri = new Uri(url);
                url = string.Format("{0}://{1}/Account/Weixin/{2}?openid={3}&refresh=1", uri.Scheme, uri.Host, args, openModel.openid);

                return RedirectPermanent(url);
            }

            return Content("ERROR");
        }

        #region 下载头像

        //大头像
        private string GetBigImg(string oriName, string uid)
        {
            string s_img = "big_" + oriName;
            string sourceFolder = ConfigurationManager.AppSettings["sourceFolder"];
            string uploadPath = ConfigurationManager.AppSettings["uploadsFolder"];
            string sourcePath = Path.Combine(sourceFolder, uid, oriName);
            string destPath = Path.Combine(uploadPath, s_img);
            //ImageHelper.MakeThumbnail(sourcePath, destPath, 100, 100);

            return s_img;
        }

        //小头像
        private string GetSmallImg(string oriName, string uid)
        {
            string s_img = "small_" + oriName;
            string sourceFolder = ConfigurationManager.AppSettings["sourceFolder"];
            string uploadPath = ConfigurationManager.AppSettings["uploadsFolder"];
            string sourcePath = Path.Combine(sourceFolder, uid, oriName);
            string destPath = Path.Combine(uploadPath, s_img);
            //ImageHelper.MakeThumbnail(sourcePath, destPath, 80, 80);

            return s_img;
        }

        //下载图片，并按照ID创建目录
        private string DownloadImage(string uid, string url)
        {
            string sourceFolder = ConfigurationManager.AppSettings["sourceFolder"];
            string srcPath = Path.Combine(sourceFolder, uid);

            if (!Directory.Exists(srcPath))
                Directory.CreateDirectory(srcPath);
            string filename = Guid.NewGuid().ToString() + ".jpg";
            string srcFilePath = Path.Combine(srcPath, filename);

            var WC = new WebClient();
            WC.DownloadFile(url, srcFilePath);
            WC.Dispose();

            return filename;
        }

        #endregion
    }
}
