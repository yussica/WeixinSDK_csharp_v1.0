using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace WeixinUtils.Utils
{
    public class HttpUtil
    {
        private HttpWebRequest request;
        private Encoding encoding;
        private int timeOut;
        private string appid;
        private string appsecret;
        private string callback;
        protected string urlBase;

        public HttpUtil()
        {
            encoding = Encoding.UTF8;
            timeOut = 1000*30;
            this.appid = ConfigurationManager.AppSettings["APPID"];
            this.appsecret = ConfigurationManager.AppSettings["APPSECRET"];
            this.callback = ConfigurationManager.AppSettings["Callback"];
            urlBase = "https://api.weixin.qq.com/cgi-bin/";
        }

        public string APPID
        {
            get { return this.appid; }
        }

        public string APPSECRET
        {
            get { return this.appsecret; }
        }

        public string Callback
        {
            get { return this.callback; }
        }

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <param name="inputStream">Request.InputStream</param>
        /// <returns></returns>
        protected string Receive(Stream inputStream)
        {
            string postBody = string.Empty;
            byte[] buffer = new byte[inputStream.Length];
            inputStream.Read(buffer, 0, (int) inputStream.Length);
            postBody = Encoding.UTF8.GetString(buffer);
            return postBody;
        }

        /// <summary>
        /// HTTP Get
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        protected string DoGet(string url)
        {
            try
            {
                request = (HttpWebRequest) HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = timeOut;

                var resp = (HttpWebResponse) request.GetResponse();
                resp = (HttpWebResponse) request.GetResponse();
                var respStream = resp.GetResponseStream();
                var sr = new StreamReader(respStream);
                var respString = sr.ReadToEnd();
                return respString;
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        protected string DoGet(string url, string savePath)
        {
            try
            {
                request = (HttpWebRequest) HttpWebRequest.Create(url);
                request.Method = "GET";
                request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request.Timeout = timeOut;

                var resp = (HttpWebResponse) request.GetResponse();
                resp = (HttpWebResponse) request.GetResponse();
                var respStream = resp.GetResponseStream();
                var img = Image.FromStream(respStream);
                img.Save(savePath);
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        /// <summary>
        /// HTTP Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string DoPost(string url, string data)
        {
            try
            {
                var request = (HttpWebRequest) HttpWebRequest.Create(url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded;";

                // Encode the data
                var buffer = encoding.GetBytes(data);
                request.ContentLength = buffer.Length;
                request.Timeout = timeOut;

                // Write encoded data into request stream
                var reqStream = request.GetRequestStream();
                reqStream.Write(buffer, 0, buffer.Length);
                reqStream.Close();

                var resp = (HttpWebResponse) request.GetResponse();
                resp = (HttpWebResponse) request.GetResponse();
                var respStream = resp.GetResponseStream();
                var sr = new StreamReader(respStream);
                var respString = sr.ReadToEnd();
                return respString;
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        /// <summary>
        /// HTTP Post
        /// </summary>
        /// <param name="url"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected string DoPostFile(string url, string filePath)
        {
            try
            {
                var request = (HttpWebRequest) HttpWebRequest.Create(url);

                string boundaryString = "----WebKitFormBoundary" + RandomCode(16);
                request.AllowWriteStreamBuffering = false;

                request.Method = "POST";
                request.ContentType = "multipart/form-data; boundary=" + boundaryString;
                request.KeepAlive = false;

                ASCIIEncoding ascii = new ASCIIEncoding();
                string boundaryStringLine = "\r\n--" + boundaryString + "\r\n";
                byte[] boundaryStringLineBytes = ascii.GetBytes(boundaryStringLine);

                string lastBoundaryStringLine = "\r\n--" + boundaryString + "--\r\n";
                byte[] lastBoundaryStringLineBytes = ascii.GetBytes(lastBoundaryStringLine);

                // Get the byte array of the myFileDescription content disposition
                string myFileDescriptionContentDisposition = String.Format(
                    "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}",
                    "myFileDescription",
                    "A sample file description");
                byte[] myFileDescriptionContentDispositionBytes
                    = ascii.GetBytes(myFileDescriptionContentDisposition);

                // Get the byte array of the string part of the myFile content
                // disposition
                string myFileContentDisposition = String.Format(
                    "Content-Disposition: form-data;name=\"{0}\"; "
                    + "filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n",
                    "myFile", Path.GetFileName(filePath), Path.GetExtension(filePath));
                byte[] myFileContentDispositionBytes =
                    ascii.GetBytes(myFileContentDisposition);

                FileInfo fileInfo = new FileInfo(filePath);

                // Calculate the total size of the HTTP request
                long totalRequestBodySize = boundaryStringLineBytes.Length*2
                                            + lastBoundaryStringLineBytes.Length
                                            + myFileDescriptionContentDispositionBytes.Length
                                            + myFileContentDispositionBytes.Length
                                            + fileInfo.Length;
                request.ContentLength = totalRequestBodySize;
                request.Timeout = timeOut;

                using (Stream s = request.GetRequestStream())
                {
                    // Send the file description content disposition over to the server
                    s.Write(boundaryStringLineBytes, 0, boundaryStringLineBytes.Length);
                    s.Write(myFileDescriptionContentDispositionBytes, 0,
                            myFileDescriptionContentDisposition.Length);

                    // Send the file content disposition over to the server
                    s.Write(boundaryStringLineBytes, 0, boundaryStringLineBytes.Length);
                    s.Write(myFileContentDispositionBytes, 0,
                            myFileContentDispositionBytes.Length);

                    // Send the file binaries over to the server, in 1024 bytes chunk
                    FileStream fileStream = new FileStream(filePath, FileMode.Open,
                                                           FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;
                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        s.Write(buffer, 0, bytesRead);
                    } // end while
                    fileStream.Close();

                    // Send the last part of the HTTP request body
                    s.Write(lastBoundaryStringLineBytes, 0, lastBoundaryStringLineBytes.Length);
                }

                var resp = (HttpWebResponse) request.GetResponse();
                resp = (HttpWebResponse) request.GetResponse();
                var respStream = resp.GetResponseStream();
                var sr = new StreamReader(respStream);
                var respString = sr.ReadToEnd();
                return respString;
            }
            catch (Exception ex)
            {

            }
            return string.Empty;
        }

        private string RandomCode(int length)
        {
            var code = new StringBuilder();
            var chars = new char[]
                {
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K'
                    , 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e'
                    , 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y'
                    , 'z'
                };
            var seed = (int) DateTime.Now.Ticks;
            RNGCryptoServiceProvider Gen;
            var randomNumber = new byte[1];
            for (var i = 0; i < length; i++)
            {
                Gen = new RNGCryptoServiceProvider();
                Gen.GetBytes(randomNumber);
                var random = new Random(randomNumber[0]);
                code.Append(chars[random.Next(0, chars.Length)]);
            }
            return code.ToString();
        }

        /// <summary>
        /// DateTime转Unix时间戳
        /// </summary>
        /// <returns></returns>
        public int DateTime2Unix()
        {
            return DateTime2Unix(DateTime.UtcNow);
        }

        /// <summary>
        /// DateTime转Unix时间戳
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        public int DateTime2Unix(DateTime now)
        {
            return (int) (now.AddHours(-8).Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        /// <summary>
        /// Unix时间戳转DateTime
        /// </summary>
        /// <param name="ts"></param>
        /// <returns></returns>
        public DateTime Unix2DateTime(int ts)
        {
            return new DateTime(1970, 1, 1).AddSeconds(ts).AddHours(8);
        }

        protected string Model2Json(object o)
        {
            var jsonsetting = new JsonSerializerSettings();
            jsonsetting.NullValueHandling = NullValueHandling.Ignore;
            var json = JsonConvert.SerializeObject(o, jsonsetting);
            return json;
        }
    }
}
