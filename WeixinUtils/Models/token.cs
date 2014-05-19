
using System;
namespace WeixinUtils.Models
{
    public class token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
