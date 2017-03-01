using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Land.OA.API
{
    public class CorsHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Method == HttpMethod.Options)
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
                return Task.FromResult(response);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
