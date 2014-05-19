
namespace WeixinUtils.Models
{
    public enum UploadType
    {
        image,
        voice,
        video,
        thumb
    }

    public enum CustomMsgType
    {
        text,
        image,
        voice,
        video,
        music,
        news
    }

    public enum ButtonType
    {
        click,
        view
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        text,
        image,
        voice,
        video,
        location,
        link,
        Event,
        music,
        news
    }
}
