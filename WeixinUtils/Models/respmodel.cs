using System.Collections.Generic;

namespace WeixinUtils.Models
{
    public class respmodel
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }

        public string this[int errcode]
        {
            get
            {
                var dict = new Dictionary<int, string>();
                dict.Add(-1, "系统繁忙");
                dict.Add(0, "请求成功");
                dict.Add(40001, "获取access_token时AppSecret错误，或者access_token无效");
                dict.Add(40002, "不合法的凭证类型");
                dict.Add(40003, "不合法的OpenID");
                dict.Add(40004, "不合法的媒体文件类型");
                dict.Add(40005, "不合法的文件类型");
                dict.Add(40006, "不合法的文件大小");
                dict.Add(40007, "不合法的媒体文件id");
                dict.Add(40008, "不合法的消息类型");
                dict.Add(40009, "不合法的图片文件大小");
                dict.Add(40010, "不合法的语音文件大小");
                dict.Add(40011, "不合法的视频文件大小");
                dict.Add(40012, "不合法的缩略图文件大小");
                dict.Add(40013, "不合法的APPID");
                dict.Add(40014, "不合法的access_token");
                dict.Add(40015, "不合法的菜单类型");
                dict.Add(40016, "不合法的按钮个数");
                dict.Add(40017, "不合法的按钮个数");
                dict.Add(40018, "不合法的按钮名字长度");
                dict.Add(40019, "不合法的按钮KEY长度");
                dict.Add(40020, "不合法的按钮URL长度");
                dict.Add(40021, "不合法的菜单版本号");
                dict.Add(40022, "不合法的子菜单级数");
                dict.Add(40023, "不合法的子菜单按钮个数");
                dict.Add(40024, "不合法的子菜单按钮类型");
                dict.Add(40025, "不合法的子菜单按钮名字长度");
                dict.Add(40026, "不合法的子菜单按钮KEY长度");
                dict.Add(40027, "不合法的子菜单按钮URL长度");
                dict.Add(40028, "不合法的自定义菜单使用用户");
                dict.Add(40029, "不合法的oauth_code");
                dict.Add(40030, "不合法的refresh_token");
                dict.Add(40031, "不合法的openid列表");
                dict.Add(40032, "不合法的openid列表长度");
                dict.Add(40033, @"不合法的请求字符，不能包含\uxxxx格式的字符");
                dict.Add(40035, "不合法的参数");
                dict.Add(40038, "不合法的请求格式");
                dict.Add(40039, "不合法的URL长度");
                dict.Add(40050, "不合法的分组id");
                dict.Add(40051, "分组名字不合法");
                dict.Add(41001, "缺少access_token参数");
                dict.Add(41002, "缺少appid参数");
                dict.Add(41003, "缺少refresh_token参数");
                dict.Add(41004, "缺少secret参数");
                dict.Add(41005, "缺少多媒体文件数据");
                dict.Add(41006, "缺少media_id参数");
                dict.Add(41007, "缺少子菜单数据");
                dict.Add(41008, "缺少oauth code");
                dict.Add(41009, "缺少openid");
                dict.Add(42001, "access_token超时");
                dict.Add(42002, "refresh_token超时");
                dict.Add(42003, "oauth_code超时");
                dict.Add(43001, "需要GET请求");
                dict.Add(43002, "需要POST请求");
                dict.Add(43003, "需要HTTPS请求");
                dict.Add(43004, "需要接收者关注");
                dict.Add(43005, "需要好友关系");
                dict.Add(44001, "多媒体文件为空");
                dict.Add(44002, "POST的数据包为空");
                dict.Add(44003, "图文消息内容为空");
                dict.Add(44004, "文本消息内容为空");
                dict.Add(45001, "多媒体文件大小超过限制");
                dict.Add(45002, "消息内容超过限制");
                dict.Add(45003, "标题字段超过限制");
                dict.Add(45004, "描述字段超过限制");
                dict.Add(45005, "链接字段超过限制");
                dict.Add(45006, "图片链接字段超过限制");
                dict.Add(45007, "语音播放时间超过限制");
                dict.Add(45008, "图文消息超过限制");
                dict.Add(45009, "接口调用超过限制");
                dict.Add(45010, "创建菜单个数超过限制");
                dict.Add(45015, "回复时间超过限制");
                dict.Add(45016, "系统分组，不允许修改");
                dict.Add(45017, "分组名字过长");
                dict.Add(45018, "分组数量超过上限");
                dict.Add(46001, "不存在媒体数据");
                dict.Add(46002, "不存在的菜单版本");
                dict.Add(46003, "不存在的菜单数据");
                dict.Add(46004, "不存在的用户");
                dict.Add(47001, "解析JSON/XML内容错误");
                dict.Add(48001, "api功能未授权");
                dict.Add(50001, "用户未授权该api");
                if (dict.ContainsKey(errcode))
                    return dict[errcode];
                return "微信未定义错误";
            }
        }
    }

    public class uploadrespmodel
    {
        public string type { get; set; }

        public string media_id { get; set; }

        public int created_at { get; set; }
    }
}
