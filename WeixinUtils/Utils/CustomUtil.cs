using Newtonsoft.Json;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class CustomUtil : HttpUtil
    {
        public string send(string token, sendcustommsg model)
        {
            var url = string.Format("{0}message/custom/send?access_token={1}", this.urlBase, token);
            model.msgtype = CustomMsgType.text.ToString();
            var content = JsonConvert.SerializeObject(model);
            var result = this.DoPost(url, content);
            return result;
        }
    }
}
