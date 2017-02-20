using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Loowoo.Land.OA.API
{
    public class WebApiExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var errorString = string.Format("{0}——{1}：{2}——{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), actionExecutedContext.Exception.GetType().ToString(), actionExecutedContext.Exception.Message, actionExecutedContext.Exception.StackTrace);
            LogWriter.Instance.WriteLog2(errorString);
            HttpResponseMessage response = null;
            if (actionExecutedContext.Exception is NotImplementedException)
            {
                response=new HttpResponseMessage(System.Net.HttpStatusCode.NotImplemented);
            }else if(actionExecutedContext.Exception is TimeoutException)
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.RequestTimeout);
            }
            else
            {
                response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
            response.Content = new StringContent(errorString);
            actionExecutedContext.Response = response;
            base.OnException(actionExecutedContext);
        }
    }
}