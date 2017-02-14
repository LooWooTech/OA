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
            //var path = @"E:\output.txt";
            //_sw = new StreamWriter(path, true);
            //Console.SetOut(_sw);
            _webApp = WebApp.Start<Startup>(HOST_ADDRESS);
            Console.WriteLine("Web API started!");
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(HOST_ADDRESS);
            Console.WriteLine("HttpClient started!");
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
    }
}
