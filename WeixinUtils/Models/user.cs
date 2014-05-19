using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeixinUtils.Models
{
    public class group
    {
        /// <summary>
        /// 分组id，由微信分配
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 分组名字，UTF8编码
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 分组内用户数量
        /// </summary>
        public int count { get; set; }
    }

    public class grouplist
    {
        /// <summary>
        /// 公众平台分组信息列表
        /// </summary>
        public IList<group> groups { get; set; }
    }
}
