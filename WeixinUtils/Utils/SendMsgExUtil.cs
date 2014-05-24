using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeixinUtils.Models;
using Newtonsoft.Json;

namespace WeixinUtils.Utils
{
    public class SendMsgExUtil : HttpUtil
    {
        /// <summary>
        /// 上传图文消息素材
        /// </summary>
        /// <param name="token"></param>
        /// <param name="articles"></param>
        public uploadrespmodel uploadnews(string token, params Article[] articles)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new { articles = articles });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<uploadrespmodel>(content);
        }

        public respmodel sendallnews(string token, int group_id, string media_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                filter = new { group_id = group_id.ToString() },
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendalltext(string token, int group_id, string text)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                filter = new { group_id = group_id.ToString() },
                text = new { content = text },
                msgtype = "text"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendallvoice(string token, int group_id, string media_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                filter = new { group_id = group_id.ToString() },
                voice = new { media_id = media_id },
                msgtype = "voice"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendallphoto(string token, int group_id, string media_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                filter = new { group_id = group_id.ToString() },
                image = new { media_id = media_id },
                msgtype = "image"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendallvideo(string token, int group_id, string media_id)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                filter = new { group_id = group_id.ToString() },
                mpvideo = new { media_id = media_id },
                msgtype = "mpvideo"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendnews(string token, string media_id, params string[] openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                touser = openid,
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendtext(string token, string text, params string[] openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                touser = openid,
                text = new { content = text },
                msgtype = "text"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendvoice(string token, string media_id, params string[] openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                touser = openid,
                voice = new { media_id = media_id },
                msgtype = "voice"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendphoto(string token, string media_id, params string[] openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                touser = openid,
                image = new { media_id = media_id },
                msgtype = "image"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel sendvideo(string token, string media_id, string title, string description, params string[] openid)
        {
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                touser = openid,
                video = new { media_id = media_id, title = title, description = description },
                msgtype = "video"
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        public respmodel delete(string token, string msgid)
        {
            var url = string.Format("https://api.weixin.qq.com//cgi-bin/message/mass/delete?access_token={0}", token);
            var data = JsonConvert.SerializeObject(new
            {
                msgid = msgid
            });
            var content = base.DoPost(url, data);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }
    }
}
