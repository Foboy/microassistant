using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MicroAssistant.Common
{
    public class HtmlFormatHelper
    {
        public static string GeneratePagingHtml(int pageIndex,int pageSize,int recordCount,string pageLink, string nextPageText="下一页", string prevPageText="上一页", string homePageText="首页", string lastPageText="末页", string displayMsg="")
        {
            string dots = "<span class='gray-font'>…</span>";
            string action ="<span class='black-font'>{0}</span>";

             if (pageSize < 1) { pageSize = 1; }
             if (pageIndex < 1) { pageIndex = 1; }
             //开始
             string html = string.Empty;
             string htmlLeftPage = string.Empty;
             string htmlRightPage = string.Empty;
             //开始计算
             long pageCount = 0;
             if (recordCount % pageSize == 0)
             {
                 pageCount = recordCount / pageSize;
             }
             else
             {
                 pageCount = (recordCount / pageSize) + 1;
             }

             if (pageCount <= 1)
                 return string.Empty;

             int leftIndex = 0;
             int rightIndex = 0;

             for (int i = 2; i >= 1; i--)
             {
                 if (pageIndex - i >= 1)
                 {
                     leftIndex = pageIndex - i;
                     htmlLeftPage += string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, (pageIndex - i)), pageIndex - i);
                 }
             }
             for (int j = 1; j <= 2; j++)
             {
                 if (pageIndex + j <= pageCount)
                 {
                     rightIndex = pageIndex + j;
                     htmlRightPage += string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, (pageIndex + j)), pageIndex + j);
                 }
             }
             if (rightIndex < pageIndex)
                 rightIndex = pageIndex;
             long prevPage = pageIndex - 1;
             if (prevPage < 1) { prevPage = 1; }
             long nextPage = pageIndex + 1;
             if (nextPage > pageCount) { nextPage = pageCount; }
             //最后处理
             if (nextPage < 1) { nextPage = 1; }
             if (pageCount < 1) { pageCount = 1; }

             if (leftIndex > 3)
             {
                 htmlLeftPage =
                     string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, 1), 1) + 
                     dots + 
                     htmlLeftPage;
             }
             else if(leftIndex>2)
             {
                 htmlLeftPage =
                    string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, 1), 1) +
                    htmlLeftPage;
             }
             if (rightIndex < pageCount - 1)
             {
                 htmlRightPage =
                     htmlRightPage +
                     dots +
                     string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, pageCount), pageCount);
             }
             else if (rightIndex < pageCount)
             {
                 htmlRightPage =
                     htmlRightPage+
                    string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, pageCount), pageCount);
             }

             if (pageIndex > 1)
                 htmlLeftPage = string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, prevPage), prevPageText) + htmlLeftPage;
            if(pageIndex<pageCount)
                htmlRightPage=htmlRightPage+string.Format("<a href=\"{0}\">{1}</a>", string.Format(pageLink, nextPage), nextPageText);
             html = 
                 htmlLeftPage +
                 string.Format(action, pageIndex) +
                 htmlRightPage;

             //计算结束
             return html;
        }


    }
}
