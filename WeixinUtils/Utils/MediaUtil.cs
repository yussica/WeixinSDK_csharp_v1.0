using System.IO;
using Newtonsoft.Json;
using WeixinUtils.Models;

namespace WeixinUtils.Utils
{
    public class MediaUtil : HttpUtil
    {
        /// <summary>
        /// 上传媒体文件
        /// </summary>
        /// <param name="token"></param>
        /// <param name="type"></param>
        /// <param name="filePath">本地路径</param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public bool upload(string token, UploadType type, string filePath, out string media_id)
        {
            media_id = string.Empty;
            string url = string.Format("{0}media/upload?access_token={1}&type={2}", this.urlBase, token, type.ToString());
            var result = DoPostFile(url, filePath);
            if (string.IsNullOrEmpty(result))
                return false;
            var resp = JsonConvert.DeserializeObject<uploadrespmodel>(result);
            media_id = resp.media_id;
            return true;
        }

        /// <summary>
        /// 下载媒体资源
        /// </summary>
        /// <param name="token"></param>
        /// <param name="media_id"></param>
        /// <returns></returns>
        public void get(string token, string media_id, string saveDir)
        {
            string url = string.Format("{0}media/get?access_token={1}&media_id={2}", this.urlBase, token, media_id);
            string savePath = Path.Combine(saveDir, media_id + ".jpg");
            var result = DoGet(url, savePath);
        }
    }
}
