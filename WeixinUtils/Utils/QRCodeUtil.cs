using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WeixinUtils.Utils
{
    public class QRCodeUtil : HttpUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="scene_id"></param>
        /// <returns></returns>
        public string create(string token, int scene_id)
        {
            var model = new { expire_seconds = 1800, action_name = "QR_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            var json = Model2Json(model);
            var url = string.Format("{0}qrcode/create?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            var respmenu = new { ticket = "", expire_seconds = 1800 };
            var deserialized = JsonConvert.DeserializeAnonymousType(content, respmenu);
            if (null != deserialized)
                return deserialized.ticket;
            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="scene_id">1--100000</param>
        /// <returns></returns>
        public string createforever(string token, int scene_id)
        {
            var model = new { action_name = "QR_LIMIT_SCENE", action_info = new { scene = new { scene_id = scene_id } } };
            var json = Model2Json(model);
            var url = string.Format("{0}qrcode/create?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            var respmenu = new { ticket = "", expire_seconds = 1800 };
            var deserialized = JsonConvert.DeserializeAnonymousType(content, respmenu);
            if (null != deserialized)
                return deserialized.ticket;
            return string.Empty;
        }

        /// <summary>
        /// 通过ticket换取二维码。获取二维码ticket后，开发者可用ticket换取二维码图片。
        /// </summary>
        /// <param name="ticket"></param>
        /// <returns></returns>
        public string showqrcode(string ticket)
        {
            return string.Format("{0}showqrcode?ticket={1}", this.urlBase, ticket);
        }
    }
}
