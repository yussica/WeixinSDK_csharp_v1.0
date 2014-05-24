using Newtonsoft.Json;
using WeixinUtils.Models;
using System.Collections.Specialized;

namespace WeixinUtils.Utils
{
    public class OAuthUtil : HttpUtil
    {
        public string snsapi_base(string REDIRECT_URI)
        {
            var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect",
                                    this.APPID, this.Callback, REDIRECT_URI);

            return url;
        }

        public string snsapi_userinfo(string REDIRECT_URI)
        {
            var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_userinfo&state={2}#wechat_redirect",
                                    this.APPID, this.Callback, REDIRECT_URI);

            return url;
        }

        public AccessToken access_token(string code)
        {
            var url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                this.APPID, this.APPSECRET, code);
            var content = base.DoGet(url);

            var deserialized = JsonConvert.DeserializeObject<AccessToken>(content);
            return deserialized;
        }

        public UserInfo userinfo(string hash)
        {
            if (!string.IsNullOrEmpty(hash))
            {
                hash = hash.Replace("#", string.Empty);
                var keys = hash.Split('&');
                StringDictionary dict = new StringDictionary();
                int index = 0;
                foreach (string key in keys)
                {
                    index = key.IndexOf("=");
                    if (dict.ContainsKey(key.Substring(0, index)))
                    {
                        dict[key.Substring(0, index)] = key.Substring(index + 1, key.Length - index - 1);
                    }
                    else
                    {
                        dict.Add(key.Substring(0, index), key.Substring(index + 1, key.Length - index - 1));
                    }
                }
                string access_token = dict["access_token"];
                string openid = dict["openid"];
                long expires_in;
                long.TryParse(dict["expires_in"], out expires_in);
                var response = base.DoGet(string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN", access_token, openid));
                var model = JsonConvert.DeserializeObject<UserInfo>(response);
                model.ReturnUrl = dict["state"];
                return model;
            }
            return null;
        }
    }
}
