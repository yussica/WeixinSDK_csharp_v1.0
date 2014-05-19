
namespace WeixinUtils.Models
{
    public class custommsg
    {
        /// <summary>
        /// 普通用户openid
        /// </summary>
        public string touser { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string msgtype { get; set; }
    }

    public class sendcustommsg : custommsg
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public sendcontent text { get; set; }
    }

    public class sendcontent
    {
        public string content { get; set; }
    }
}
