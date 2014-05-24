using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using WeixinUtils.Models;
using System.IO;

namespace WeixinUtils.Utils
{
    public class ReceiveMsgUtil : HttpUtil
    {
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public string ReceiveContent(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            return content;
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public weixin_receivemsg ReceiveMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            if (string.IsNullOrEmpty(content))
                return null;
            var model = Xml2Model<weixin_receivemsg>(content);
            switch (model.MsgType)
            {
                case MsgType.text:
                    model = Xml2Model<text_receivemsg>(content);
                    break;
                case MsgType.image:
                    model = Xml2Model<image_receivemsg>(content);
                    break;
                case MsgType.voice:
                    model = Xml2Model<voice_receivemsg>(content);
                    break;
                case MsgType.video:
                    model = Xml2Model<video_receivemsg>(content);
                    break;
                case MsgType.location:
                    model = Xml2Model<location_receivemsg>(content);
                    break;
                case MsgType.link:
                    model = Xml2Model<link_receivemsg>(content);
                    break;
                case MsgType.Event:
                    if (content.IndexOf("MASSSENDJOBFINISH") == -1)
                        model = Xml2Model<eventmsg>(content);
                    else
                        model = Xml2Model<SendCallback>(content);
                    break;
            }
            return model;
        }

        /// <summary>
        /// 接收文本消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public text_receivemsg ReceiveTextMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<text_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// 接收图片消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public image_receivemsg ReceivePhotoMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<image_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// 接收语音消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public voice_receivemsg ReceiveVoiceMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<voice_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// 接收视频消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public video_receivemsg ReceiveVideoMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<video_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// 接收位置消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public location_receivemsg ReceiveGeoMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<location_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// 接收链接消息
        /// </summary>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public link_receivemsg ReceiveLinkMsg(Stream inputStream)
        {
            var content = base.Receive(inputStream);
            var model = Xml2Model<link_receivemsg>(content);
            return model;
        }

        /// <summary>
        /// xml转实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public T Xml2Model<T>(string content) where T : new()
        {
            var model = new T();
            var type = typeof(T);
            var props = type.GetProperties();
            var xdoc = new XmlDocument();
            xdoc.LoadXml(content);
            var root = xdoc.DocumentElement;
            foreach (XmlNode node in root.ChildNodes)
            {
                foreach (var p in props)
                {
                    if (node.Name.ToLower() == p.Name.ToLower())
                    {
                        if ("createtime" == p.Name.ToLower())
                            p.SetValue(model, Unix2DateTime(int.Parse(node.InnerText)), null);
                        else if ("msgtype" == p.Name.ToLower())
                        {
                            if (node.InnerText == "event")
                                node.InnerText = "Event";
                            p.SetValue(model, (MsgType)Enum.Parse(typeof(MsgType), node.InnerText), null);
                        }
                        else
                            p.SetValue(model, Convert.ChangeType(node.InnerText, p.PropertyType), null);
                    }
                }
            }
            return model;
        }
    }
}
