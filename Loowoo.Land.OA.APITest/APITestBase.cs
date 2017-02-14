using Loowoo.Land.OA.API;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Loowoo.Land.OA.APITest
{
    public abstract class APITestBase
    {
        public abstract string GetBaseAddress();

        private HttpMessageInvoker CreateMessageInvoker()
        {
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            var server = new HttpServer(config);
            var messageInvoker = new HttpMessageInvoker(server);
            return messageInvoker;
        }

        protected TResult InvokeGetRequest<TResult>(string api)
        {
            using (var invoker = CreateMessageInvoker())
            {
                using (var cts = new CancellationTokenSource())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, GetBaseAddress() + api);
                    using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                    {
                        var result = response.Content.ReadAsStringAsync().Result;
                        return JsonConvert.DeserializeObject<TResult>(result);
                    }
                }
            }
        }

        protected TResult InvokePostRequest<TResult, TArguemnt>(string api, TArguemnt arg)
        {
            var invoker = CreateMessageInvoker();
            using (var cts = new CancellationTokenSource())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, GetBaseAddress() + api);
                request.Content = new ObjectContent<TArguemnt>(arg, new JsonMediaTypeFormatter());
                using (HttpResponseMessage response = invoker.SendAsync(request, cts.Token).Result)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<TResult>(result);
                }
            }
        }
    }
}
