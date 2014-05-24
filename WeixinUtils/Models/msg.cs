using System;
using System.Collections.Generic;

namespace WeixinUtils.Models
{
    #region 微信接收消息

    public class weixin_receivemsg
    {
        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class text_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class image_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 图片消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class voice_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 语音消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 语音识别结果，UTF8编码
        /// </summary>
        public string Recognition { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class video_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 视频消息媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息缩略图的媒体id，可以调用多媒体文件下载接口拉取数据。
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 地理位置消息
    /// </summary>
    public class location_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 地理位置维度
        /// </summary>
        public double Location_X { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Location_Y { get; set; }

        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale { get; set; }

        /// <summary>
        /// 地理位置信息
        /// </summary>
        public string Label { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class link_receivemsg : weixin_receivemsg
    {
        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 消息链接
        /// </summary>
        public string Url { get; set; }
    }

    /// <summary>
    /// 链接消息
    /// </summary>
    public class eventmsg : weixin_receivemsg
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值
        /// </summary>
        public string EventKey { get; set; }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }

        /// <summary>
        /// 订阅时触发的事件
        /// </summary>
        public const string Subscribe = "subscribe";

        /// <summary>
        /// 取消订阅时触发的事件
        /// </summary>
        public const string UnSubscribe = "unsubscribe";

        /// <summary>
        /// 菜单类型为click的菜单是触发，带有子菜单的不会收到次事件
        /// </summary>
        public const string Click = "CLICK";

        /// <summary>
        /// 菜单类型为view的菜单是触发，带有子菜单的不会收到次事件
        /// </summary>
        public const string View = "VIEW";

        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public const string Location = "LOCATION";

        /// <summary>
        /// 用户已关注时的事件推送（扫描二维码）
        /// </summary>
        public const string Scan = "SCAN";

        /// <summary>
        /// 群发结束事件
        /// </summary>
        public const string MASSSENDJOBFINISH = "MASSSENDJOBFINISH";
    }

    public class SendCallback : weixin_receivemsg
    {
        /// <summary>
        /// 事件信息，此处为MASSSENDJOBFINISH
        /// </summary>
        public string Event { get; set; }

        /// <summary>
        /// 群发的结构，为“send success”或“send fail”或“err(num)”。
        /// 但send success时，也有可能因用户拒收公众号的消息、系统错误等原因造成少量用户接收失败。
        /// err(num)是审核失败的具体原因，可能的情况如下：err(10001), //涉嫌广告 err(20001), //涉嫌政治 err(20004), //涉嫌社会 err(20002), //涉嫌色情 err(20006), //涉嫌违法犯罪 err(20008), //涉嫌欺诈 err(20013), //涉嫌版权 err(22000), //涉嫌互推(互相宣传) err(21000), //涉嫌其他
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// group_id下粉丝数；或者openid_list中的粉丝数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 过滤（过滤是指特定地区、性别的过滤、用户设置拒收的过滤，用户接收已超4条的过滤）后，准备发送的粉丝数，原则上，FilterCount = SentCount + ErrorCount
        /// </summary>
        public int FilterCount { get; set; }

        /// <summary>
        /// 发送成功的粉丝数
        /// </summary>
        public int SentCount { get; set; }

        /// <summary>
        /// 发送失败的粉丝数
        /// </summary>
        public int ErrorCount { get; set; }
    }

    #endregion

    #region 微信发送消息

    public class weixin_sendmsg
    {
        /// <summary>
        /// 接收方帐号（收到的OpenID）
        /// </summary>
        public string ToUserName { get; set; }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public int CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
    }

    /// <summary>
    /// 文本消息
    /// </summary>
    public class text_sendmsg : weixin_sendmsg
    {
        /// <summary>
        /// 回复的消息内容（换行：在content中能够换行，微信客户端就支持换行显示）
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 图片消息
    /// </summary>
    public class image_sendmsg : weixin_sendmsg
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 语音消息
    /// </summary>
    public class voice_sendmsg : weixin_sendmsg
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }
    }

    /// <summary>
    /// 视频消息
    /// </summary>
    public class video_sendmsg : weixin_sendmsg
    {
        /// <summary>
        /// 通过上传多媒体文件，得到的id。
        /// </summary>
        public string MediaId { get; set; }

        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 音乐消息
    /// </summary>
    public class music_sendmsg : weixin_sendmsg
    {
        /// <summary>
        /// 视频消息的标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 视频消息的描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 音乐链接
        /// </summary>
        public string MusicUrl { get; set; }

        /// <summary>
        /// 高质量音乐链接，WIFI环境优先使用该链接播放音乐
        /// </summary>
        public string HQMusicUrl { get; set; }

        /// <summary>
        /// 缩略图的媒体id，通过上传多媒体文件，得到的id
        /// </summary>
        public string ThumbMediaId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class articlemsg : weixin_sendmsg
    {
        /// <summary>
        /// 图文消息个数，限制为10条以内
        /// </summary>
        public int ArticleCount { get; set; }

        /// <summary>
        /// 多条图文消息信息，默认第一个item为大图,注意，如果图文数超过10，则将会无响应
        /// </summary>
        public IList<articleitem> Articles { get; set; }
    }

    public class articleitem
    {

        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图360*200，小图200*200
        /// </summary>
        public string PicUrl { get; set; }

        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url { get; set; }
    }

    #endregion
}
