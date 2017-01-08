using Loowoo.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Loowoo.Web
{
    public class PageView
    {
        public PageView(HttpContextBase context, PageParameter page, string linkFormart = null, string currentPageClass = null)
        {
            _context = context;
            Page = page;
            if (string.IsNullOrEmpty(linkFormart))
            {
                LinkFormat = _context.Request.Path + "?page={0}&rows={1}";
            }
            else
            {
                LinkFormat = linkFormart;
            }


            if (string.IsNullOrWhiteSpace(currentPageClass))
            {
                CurrentPageClass = "disabled";
            }
            else
            {
                CurrentPageClass = currentPageClass;
            }
        }

        private HttpContextBase _context;

        public PageParameter Page { get; set; }

        /// <summary>
        /// 链接的Html附加属性
        /// </summary>
        public string LinkAttributes { get; set; }

        /// <summary>
        /// [RequestPath]?page={0}&rows{1}
        /// </summary>
        public string LinkFormat { get; set; }

        /// <summary>
        /// 当前页码的css类名
        /// </summary>
        public string CurrentPageClass { get; set; }

        public string GetPageLink(int pageIndex)
        {
            var link = string.Format(LinkFormat, pageIndex, Page.PageSize);
            var queryString = string.Empty;
            foreach (var key in _context.Request.QueryString.AllKeys)
            {
                if(!link.Contains(key+"="))
                {
                    queryString += "&" + key + "=" + HttpUtility.UrlEncode(_context.Request.QueryString[key]);
                }
            }
            if (!link.Contains("?"))
            {
                link += "?";
            }
            return link + queryString;
        }

        public HtmlString GetHtml()
        {
            if (Page == null)
            {
                return null;
            }

            if (Page.PageSize == 0)
            {
                Page.PageSize = 10;
            }

            if (Page.PageCount < 2)
            {
                return null;
            }

            var queryString = HttpContext.Current.Request.QueryString;

            var listSize = 5;
            var startIndex = Page.PageIndex - Page.PageIndex % listSize + 1;
            //如果正好是list的尾页，则不翻到下一批
            if (Page.PageIndex % listSize == 0)
            {
                startIndex -= listSize;
            }
            var endIndex = startIndex + listSize - 1;
            endIndex = endIndex >= Page.PageCount ? Page.PageCount : endIndex;
            var sb = new StringBuilder();
            sb.AppendLine("<ul class=\"pagination\">");
            if (Page.PageIndex > 1)
            {
                sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>&lt;&lt; 上一页</a></li>", GetPageLink(Page.PageIndex - 1), LinkAttributes));
            }
            if (startIndex > 1)
            {
                sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>1</a></li>", GetPageLink(1), LinkAttributes));
            }
            if (startIndex / listSize > 1)
            {
                sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>...</a></li>", GetPageLink(startIndex - listSize), LinkAttributes));
            }
            for (int i = startIndex; i <= endIndex; i++)
            {
                if (i == Page.PageIndex)
                {
                    sb.AppendLine(string.Format("<li class=\"{1}\"><a>{0}</a></li>", i, CurrentPageClass));
                }
                else
                {
                    sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>{2}</a></li>", GetPageLink(i), LinkAttributes, i));
                }
            }
            if (endIndex + 1 < Page.PageCount)
            {
                sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>...</a></li>", GetPageLink(endIndex + 1), LinkAttributes));
            }
            if (Page.PageIndex < Page.PageCount - 1)
            {
                sb.AppendLine(string.Format("<li><a href=\"{0}\" {1}>&gt;&gt; 下一页</a></li>", GetPageLink(Page.PageIndex + 1), LinkAttributes));
            }

            sb.AppendLine(string.Format("<li class=\"disabled\"><a>{0}/{1}</a></li>", Page.PageIndex, Page.PageCount));

            return new HtmlString(sb.ToString());
        }
    }
}
