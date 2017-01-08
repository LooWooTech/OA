using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Common
{
    public class HttpRequestHelper
    {
        public static string Request(string url, string method = "GET")
        {
            var result = string.Empty;
            try
            {
                var request = WebRequest.Create(url) as HttpWebRequest;
                request.ServicePoint.Expect100Continue = false;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.0)";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Method = method;

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        result = reader.ReadToEnd();
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }

        public static string PostData(string url, string data)
        {
            var result = string.Empty;

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            StreamReader sr = null;
            Stream requestStream = null;

            try
            {
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    request = WebRequest.Create(url) as HttpWebRequest;
                    request.ProtocolVersion = HttpVersion.Version10;
                }
                else
                {
                    request = WebRequest.Create(url) as HttpWebRequest;
                }

                request.Method = "POST";
                request.Timeout = 15000;
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
                request.CookieContainer = new CookieContainer();

                if (!string.IsNullOrWhiteSpace(data))
                {
                    var bytes = Encoding.UTF8.GetBytes(data);
                    request.ContentType = "application/json";
                    request.ContentLength = bytes.Length;
                    requestStream = request.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Flush();
                }

                response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    // gzip压缩传输的内容使用gzip读取响应流
                    if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        sr = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress), Encoding.UTF8);
                    }
                    // 普通内容直接读取响应流
                    else
                    {
                        sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    }

                    result = sr.ReadToEnd();
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response.Dispose();
                    response = null;
                }

                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                    sr = null;
                }

                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream.Dispose();
                    requestStream = null;
                }

                if (request != null)
                {
                    request = null;
                }
            }
            return result;
        }
    }
}
