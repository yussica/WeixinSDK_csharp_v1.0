using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class BasicUtil : HttpUtil
    {
        private const string TOKEN = "test";

        /// <summary>
        /// 获取access_token（注：每日调用限额2000次，请不要超过2000次）
        /// </summary>
        /// <returns></returns>
        public token GetToken()
        {
            var url = string.Format("{0}token?grant_type=client_credential&appid={1}&secret={2}", this.urlBase,
                                    this.APPID, this.APPSECRET);
            var content = this.DoGet(url);
            var model = JsonConvert.DeserializeObject<token>(content);
            model.CreateDate = DateTime.Now;
            return model;
        }

        /// <summary>
        /// 验证消息真实性
        /// </summary>
        /// <param name="signature"></param>
        /// <param name="timestamp"></param>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public bool checkSignature(string signature, string timestamp, string nonce)
        {
            var token = TOKEN;
            var tmpArr = new List<string>() { token, timestamp, nonce };
            tmpArr.Sort();
            var tmpStr = string.Concat(tmpArr);
            tmpStr = sha1(tmpStr);
            if (tmpStr == signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// sha1加密
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string sha1(string text)
        {
            var encoding = Encoding.UTF8;
            var buffer = encoding.GetBytes(text);
            var cryptoTransformSHA1 =
                new SHA1CryptoServiceProvider();
            var hash = BitConverter.ToString(
                cryptoTransformSHA1.ComputeHash(buffer)).Replace("-", "");
            return hash.ToLower();
        }
    }
}
