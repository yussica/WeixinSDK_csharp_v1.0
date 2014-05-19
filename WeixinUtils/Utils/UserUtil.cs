using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class UserUtil : HttpUtil
    {
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="token"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public respmodel update(string token, string name)
        {
            var model = new { group = new { name = name } };
            var json = Model2Json(model);
            var url = string.Format("{0}groups/update?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        /// <summary>
        /// 获取分组
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public grouplist get(string token)
        {
            var url = string.Format("{0}groups/get?access_token={1}", base.urlBase, token);
            var content = base.DoGet(url);
            return JsonConvert.DeserializeObject<grouplist>(content);
        }

        /// <summary>
        /// 获取用户所在分组
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public grouplist getid(string token, string openid)
        {
            var model = new { openid = openid };
            var json = Model2Json(model);
            var url = string.Format("{0}groups/getid?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            return JsonConvert.DeserializeObject<grouplist>(content);
        }

        /// <summary>
        /// 更新分组
        /// </summary>
        /// <param name="token"></param>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public respmodel update2(string token, string id, string name)
        {
            var model = new { @group = new { id = id, name = name } };
            var json = Model2Json(model);
            var url = string.Format("{0}groups/update?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }

        /// <summary>
        /// 移动分组
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openid"></param>
        /// <param name="to_groupid"></param>
        /// <returns></returns>
        public respmodel move(string token, string openid, string to_groupid)
        {
            var model = new { openid = openid, to_groupid = to_groupid };
            var json = Model2Json(model);
            var url = string.Format("{0}groups/members/update?access_token={1}", base.urlBase, token);
            var content = base.DoPost(url, json);
            return JsonConvert.DeserializeObject<respmodel>(content);
        }
    }
}
