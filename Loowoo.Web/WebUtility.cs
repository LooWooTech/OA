using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Loowoo.Web
{
    public static class WebUtility
    {
        public static HtmlString Pagination(this HtmlHelper html, PageParameter page, string attributes = null)
        {
            return new PageView(html.ViewContext.HttpContext, page) { LinkAttributes = attributes }.GetHtml();
        }
    }
}