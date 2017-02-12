using Loowoo.Land.OA.API.Controllers;
using Microsoft.Owin.Hosting;
using System;
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

        public CommentsWebApiTest()
        {
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
            var postId = 1;
            Console.WriteLine("start......");
            var response = await _httpClient.GetAsync($"/blogposts/{postId}/comments");
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            Xunit.Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var comments = await response.Content.ReadAsAsync<Comment[]>();
            Xunit.Assert.NotEmpty(comments);
            Xunit.Assert.Equal(postId, comments[0].PostId);
            Xunit.Assert.Equal("Coding changes the world", comments[0].Body);
        }
    }
}
