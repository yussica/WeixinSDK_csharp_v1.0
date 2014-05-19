using Newtonsoft.Json;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class MenuUtil : HttpUtil
    {
        /// <summary>
        /// 自定义菜单创建接口
        /// var buttons = new buttonbase[3];
        /// buttons[0] = new button() { type = ButtonType.view.ToString(), name = "每日刮奖", url = "http://test03.headin.cn/Book/GoScratchNow" };
        /// buttons[1] = new button() { type = ButtonType.view.ToString(), name = "发现", url = "http://test03.headin.cn/MemberShip/Index" }
        /// buttons[2] = new button() { type = ButtonType.view.ToString(), name = "我", url = "http://test03.headin.cn/BookAccount/Index" };
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="buttons"></param>
        /// <returns></returns>
        public respmodel create(string access_token, params button[] buttons)
        {
            var url = string.Format("{0}menu/create?access_token={1}", this.urlBase, access_token);
            var jsonsetting = new JsonSerializerSettings();
            jsonsetting.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(new menu() {button = buttons}, jsonsetting);
            var content = this.DoPost(url, json);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        /// <summary>
        /// 自定义菜单查询接口
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public menu get(string access_token)
        {
            var url = string.Format("{0}menu/get?access_token={1}", this.urlBase, access_token);
            var content = this.DoGet(url);
            var respmenu = new {menu = new menu()};

            var jsonsetting = new JsonSerializerSettings();
            jsonsetting.NullValueHandling = NullValueHandling.Ignore;
            var deserialized = JsonConvert.DeserializeAnonymousType(content, respmenu, jsonsetting);
            return deserialized.menu;
        }

        /// <summary>
        /// 自定义菜单删除接口
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public respmodel delete(string access_token)
        {
            var url = string.Format("{0}menu/delete?access_token={1}", this.urlBase, access_token);
            var content = this.DoGet(url);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }
    }
}
