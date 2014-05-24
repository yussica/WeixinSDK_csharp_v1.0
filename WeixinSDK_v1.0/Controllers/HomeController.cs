using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeixinUtils.Utils;
using WeixinUtils.Models;
using Newtonsoft.Json;
using WeixinSDK_v1._0.Services;
using WeixinSDK_v1._0.ActionFilters;

namespace WeixinSDK_v1._0.Controllers
{
    public class HomeController : Controller
    {
        private static token _token = null;

        token GetToken
        {
            get
            {
                if (null == _token)
                {
                    var basic = new BasicUtil();
                    _token = basic.GetToken();
                }
                if (DateTime.Now >= _token.CreateDate.AddSeconds(_token.expires_in))
                {
                    var basic = new BasicUtil();
                    _token = basic.GetToken();
                }
                return _token;
            }
        }

        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            return Content("Weixin SDK For C#");
        }

        /// <summary>
        /// 微信接口Url
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <param name="echostr"></param>
        /// <returns></returns>
        public ActionResult WeixinMsg(string signature, string timestamp, string nonce, string echostr)
        {
            var basic = new BasicUtil();
            if (basic.checkSignature(signature, timestamp, nonce))
            {
                var msg = new ReceiveMsgUtil();
                var send = new SendMsgUtil();

                var model = msg.ReceiveMsg(Request.InputStream);
                if (null != model)
                {
                    switch (model.MsgType)
                    {
                        case MsgType.text:
                            // 处理接收到的图片信息
                            // 根据用户的信息回复用户一条信息
                            var sendText = new text_sendmsg()
                                {
                                    ToUserName = model.FromUserName,
                                    FromUserName = model.ToUserName,
                                    Content = "http://115.239.15.16/Home/Auth"
                                };
                            echostr = send.SendText(sendText);
                            break;
                        case MsgType.image:
                            // 处理接收到的图片信息
                            // (model as image_receivemsg).MediaId 可用于下载图片资源

                            // 发送一条图片信息
                            var sendImage = new image_sendmsg()
                                {
                                    ToUserName = model.FromUserName,
                                    FromUserName = model.ToUserName,
                                    MediaId = "R8SSBkFKd0z5O_7ul6PjZ-Gom_qW7UEEZz8Zgp4YFFRBHwmdnBeD4YHGQejaVSOs"
                                };
                            echostr = send.SendImage(sendImage);
                            break;
                        case MsgType.voice:
                            // 处理接收到的语音信息
                            // (model as voice_receivemsg).MediaId) 可用于下载图片资源
                            // (model as voice_receivemsg).Recognition 识别到的语音转换后的文字，需要在服务号里面开启识别才有效

                            break;
                        case MsgType.video:
                            // 处理接收到的视频信息
                            // (model as video_receivemsg).MediaId) 可用于下载图片资源
                            break;
                        case MsgType.location:
                            // 处理接收到的地理位置信息
                            // (model as location_receivemsg).Label 当前位置
                            // (model as location_receivemsg).Location_X, (model as location_receivemsg).Location_Y, (model as location_receivemsg).Scale
                            break;
                        case MsgType.link:
                            // 处理接收到的链接信息
                            // (model as link_receivemsg).Url 
                            break;
                        case MsgType.Event:
                            {
                                var eventModel = model as eventmsg;
                                if (eventModel.Event == eventmsg.Subscribe)
                                {
                                    if (string.IsNullOrEmpty(eventModel.Ticket))
                                    {
                                        // 用户关注了服务号，可以推送一条欢迎消息给用户
                                    }
                                    else
                                    {
                                        // 用户通过扫描二维码关注了服务号，可以推送一条欢迎消息给用户
                                        var sendText2 = new text_sendmsg()
                                            {
                                                ToUserName = model.FromUserName,
                                                FromUserName = model.ToUserName,
                                                Content = "感谢您使用二维码！"
                                            };
                                        echostr = send.SendText(sendText2);
                                    }
                                }
                                else if (eventModel.Event == eventmsg.UnSubscribe)
                                {
                                    // 用户取消关注时收到的事件
                                }
                                else if (eventModel.Event == eventmsg.View)
                                {
                                    // 点击类型为View的菜单触发的事件，可以统计用户点击菜单的点击次数（如果菜单有子菜单则不触发）
                                }
                                else if (eventModel.Event == eventmsg.Click)
                                {
                                    // 点击类型为Click的菜单触发的事件，可以统计用户点击菜单的点击次数（如果菜单有子菜单则不触发）
                                }
                                else if (eventModel.Event == eventmsg.Location)
                                {
                                    // 当开启位置跟踪，且用户同意发送位置信息时，每次进入服务号或每隔5s以上，上报一次位置信息
                                    // string.Format("event :: {0} :: 经度：{1} :: 纬度：{2} :: 精度：{3}", eventmsg.Click, eventModel.Latitude, eventModel.Longitude, eventModel.Precision);
                                }
                                else if (eventModel.Event == eventmsg.Scan)
                                {
                                    // 用户已经关注，且扫描二维码进入服务号时触发
                                    var sendText2 = new text_sendmsg()
                                        {
                                            ToUserName = model.FromUserName,
                                            FromUserName = model.ToUserName,
                                            Content = "感谢您使用二维码！"
                                        };
                                    echostr = send.SendText(sendText2);
                                }
                                else if (eventModel.Event == eventmsg.MASSSENDJOBFINISH)
                                {
                                }
                            }
                            break;
                        default:
                            // 微信未知的出发事件
                            break;
                    }
                }
                return Content(echostr);
            }
            // 验证签名失败
            var parm = string.Format("false:{0} :: {1} :: {2} :: {3}", signature, timestamp, nonce, echostr);

            return Content(echostr);
        }

        public ActionResult CreateMenu()
        {
            var buttons = new button[3];
            buttons[0] = new button() { type = ButtonType.view.ToString(), name = "聊天", url = "http://im.qq.com" };
            buttons[1] = new button() { type = ButtonType.view.ToString(), name = "发现", url = "http://weixin.qq.com" };
            buttons[2] = new button() { type = ButtonType.view.ToString(), name = "我", url = "http://qq.com" };

            var menu = new MenuUtil();
            var result = menu.create(_token.access_token, buttons);
            return Content("OK");
        }

        /// <summary>
        /// 推送客服消息
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMsg(string openid)
        {
            var custom = new CustomUtil();
            return Content(custom.send(_token.access_token, new sendcustommsg() { touser = openid, text = new sendcontent() { content = "欢迎" } }));
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <returns></returns>
        public ActionResult Groups()
        {
            var user = new UserUtil();
            user.update(_token.access_token, "软件开发");
            user.update(_token.access_token, "UI设计");
            user.update(_token.access_token, "产品经理");
            var groups = user.get(_token.access_token);
            return Content(JsonConvert.SerializeObject(groups));
        }

        public ActionResult UploadMedia()
        {
            var media = new MediaUtil();
            var dir = @"C:\Download\1.jpg";
            string mediaid;
            media.upload(_token.access_token, UploadType.image, dir, out mediaid);
            return Content("OK");
        }

        public ActionResult DownloadMedia(string mediaid)
        {
            var media = new MediaUtil();
            var dir = @"C:\Download";
            media.get(_token.access_token, mediaid, dir);
            return Content("OK");
        }

        [WeixinFilter]
        public ActionResult Auth()
        {
            var openid = WeixinServices.GetOpenId();
            return Content(openid);
        }
    }
}
