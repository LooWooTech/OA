using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace Loowoo.Land.OA.API
{
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = actionExecutedContext.Exception as HttpResponseException;
            var errorString = new
            {
                ex.Message,
                ex.StackTrace,
                Reason = ex.Response.ReasonPhrase,
                Data = ex.Data
            }.ToJson();
            LogWriter.Instance.WriteLog(errorString + "\r\n");

            var response = new HttpResponseMessage(ex.Response.StatusCode)
            {
                Content = new StringContent(errorString)
            };
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}