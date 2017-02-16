using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Loowoo.Land.OA.APITest
{
    public class WebApiTest:IDisposable
    {
        private const string HOST_ADDRESS = "http://localhost:61709";
        private HttpClient _httpClient { get; set; }
        private IDisposable _webApp { get; set; }
        private StreamWriter _sw { get; set; }
        public WebApiTest()
        {
            _webApp = WebApp.Start<Startup>(HOST_ADDRESS);
            Console.WriteLine("Web API started!");
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HOST_ADDRESS);
            Console.WriteLine("HttpClient started!");
        }
        public string GetAddress()
        {
            return HOST_ADDRESS;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
            _webApp.Dispose();
        }

        public async Task Test(string url)
        {
            Console.WriteLine($"开始测试地址：{url}");
            var response = await _httpClient.GetAsync(url);
            Console.WriteLine(response.StatusCode);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }

        public void Post(string url,string postData)
        {
            FunBase(url, postData, "post");
        }

        public void Put(string url,string putData)
        {
            FunBase(url, putData, "put");
        }

        public void FunBase(string url,string data,string method = "get")
        {
            url = HOST_ADDRESS + url;
            Console.WriteLine(url);
            WebRequest request = WebRequest.Create(url);
            request.Method = method;
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            Console.WriteLine("成功设置request的MIME类型及内容长度");


            Stream dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            Console.WriteLine("打开request字符流");

            WebResponse response = null;
            try
            {
                response = request.GetResponse();
            }
            catch (WebException ex)
            {
                response = ex.Response;
            }
            Console.WriteLine(((HttpWebResponse)response).StatusDescription);

            dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            Console.WriteLine($"完成{method}");
        }
    }
}
