using Loowoo.Land.OA.API.Controllers;
using Microsoft.Owin.Hosting;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Loowoo.Land.OA.APITest
{
    public class CommentsWebApiTest:IDisposable
    {
        private const string HOST_ADDRESS = "http://localhost:61709";
        private HttpClient _httpClient { get; set; }
        private IDisposable _webApp { get; set; }
        private StreamWriter _sw { get; set; }

        public CommentsWebApiTest()
        {
            var path = @"E:\output.txt";
            _sw = new StreamWriter(path,true);
            Console.SetOut(_sw);
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
        [Fact]
        public async Task GetComments()
        {
            //var postId = 1;
            //Console.WriteLine("start......");
            //var response = await _httpClient.GetAsync($"/blogposts/{postId}/comments");
            //if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //{
            //    Console.WriteLine(await response.Content.ReadAsStringAsync());
            //}
            //Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            //var comments = await response.Content.ReadAsAsync<Comment[]>();
            //Xunit.Assert.NotEmpty(comments);
            //Xunit.Assert.Equal(postId, comments[0].PostId);
            //Xunit.Assert.Equal("Coding changes the world", comments[0].Body);
        }

        public async Task Test(string url)
        {
            Console.WriteLine($"开始测试地址：{url}");
            var response = await _httpClient.GetAsync(url);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }

        }

        //[Fact]
        //public async Task Test(string url)
        //{
        //    Console.WriteLine($"开始测试地址：{url}");
        //    var response = await _httpClient.GetAsync(url);
        //    Console.WriteLine(response.StatusCode);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        Console.WriteLine(await response.Content.ReadAsStreamAsync());
        //    }
        //    //Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        //}

        //public async Task User()
        //{
        //    var page = 1;
        //    var rows = 20;
        //    await Test($"api/user/getlist?page={page}&&rows={rows}");
        //}
    }
}
