using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class SendMsgUtil : HttpUtil
    {
        public string SendText(text_sendmsg model)
        {
            model.MsgType = MsgType.text.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();
            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.FromUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[text]]></MsgType>");
            lpBuilder.AppendFormat("<Content><![CDATA[{0}]]></Content>", model.Content);
            lpBuilder.Append("</xml>");

            return lpBuilder.ToString();
        }

        public string SendImage(image_sendmsg model)
        {
            model.MsgType = MsgType.image.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();
            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.FromUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[image]]></MsgType>");
            lpBuilder.Append("<Image>");
            lpBuilder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", model.MediaId);
            lpBuilder.Append("</Image>");
            lpBuilder.Append("</xml>");

            return lpBuilder.ToString();
        }

        public string SendVoice(voice_sendmsg model)
        {
            model.MsgType = MsgType.voice.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();
            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.FromUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[voice]]></MsgType>");
            lpBuilder.Append("<Voice>");
            lpBuilder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", model.MediaId);
            lpBuilder.Append("</Voice>");
            lpBuilder.Append("</xml>");

            return lpBuilder.ToString();
        }

        public string SendVideo(video_sendmsg model)
        {
            model.MsgType = MsgType.video.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();
            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.FromUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[video]]></MsgType>");
            lpBuilder.Append("<Video>");
            lpBuilder.AppendFormat("<MediaId><![CDATA[{0}]]></MediaId>", model.MediaId);
            lpBuilder.AppendFormat("<Title><![CDATA[{0}]]></Title>", model.Title);
            lpBuilder.AppendFormat("<Description><![CDATA[{0}]]></Description>", model.Description);
            lpBuilder.Append("</Video> ");
            lpBuilder.Append("</xml>");

            return lpBuilder.ToString();
        }

        public string SendMusic(music_sendmsg model)
        {
            model.MsgType = MsgType.music.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();
            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[music]]></MsgType>");
            lpBuilder.AppendFormat("<Music>");
            lpBuilder.AppendFormat("<Title><![CDATA[{0}]]></Title>", model.Title);
            lpBuilder.AppendFormat("<Description><![CDATA[{0}]]></Description>", model.Description);
            lpBuilder.AppendFormat("<MusicUrl><![CDATA[{0}]]></MusicUrl>", model.MusicUrl);
            lpBuilder.AppendFormat("<HQMusicUrl><![CDATA[{0}]]></HQMusicUrl>", model.HQMusicUrl);
            lpBuilder.AppendFormat("<ThumbMediaId><![CDATA[{0}]]></ThumbMediaId>", model.ThumbMediaId);
            lpBuilder.Append("</Music>");
            lpBuilder.Append("</xml>");

            return lpBuilder.ToString();
        }

        public string SendArticle(articlemsg model)
        {
            model.MsgType = MsgType.news.ToString();
            model.CreateTime = base.DateTime2Unix();
            var lpBuilder = new StringBuilder();

            lpBuilder.Append("<xml>");
            lpBuilder.AppendFormat("<ToUserName><![CDATA[{0}]]></ToUserName>", model.ToUserName);
            lpBuilder.AppendFormat("<FromUserName><![CDATA[{0}]]></FromUserName>", model.FromUserName);
            lpBuilder.AppendFormat("<CreateTime>{0}</CreateTime>", model.CreateTime);
            lpBuilder.Append("<MsgType><![CDATA[news]]></MsgType>");
            lpBuilder.AppendFormat("<ArticleCount>{0}</ArticleCount>", model.Articles.Count);
            lpBuilder.Append("<Articles>");
            foreach (var item in model.Articles)
            {
                lpBuilder.Append("<item>");
                lpBuilder.AppendFormat("<Title><![CDATA[{0}]]></Title> ", item.Title);
                lpBuilder.AppendFormat("<Description><![CDATA[{0}]]></Description>", item.Description);
                lpBuilder.AppendFormat("<PicUrl><![CDATA[{0}]]></PicUrl>", item.PicUrl);
                lpBuilder.AppendFormat("<Url><![CDATA[{0}]]></Url>", item.Url);
                lpBuilder.Append("</item>");
            }
            lpBuilder.Append("</Articles>");
            lpBuilder.Append("</xml> ");

            return lpBuilder.ToString();
        }
    }
}
