using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace ECommerceApp.UI.TagHelpers
{
    [HtmlTargetElement("product-list-pager")]
    public class PagingTagHelper:TagHelper
    {
        [HtmlAttributeName("page-size")]
        public int PageSize { get; set; }
        [HtmlAttributeName("page-count")]
        public int PageCount { get; set; }
        [HtmlAttributeName("current-page")]
        public int CurrentPage { get; set; }  
        [HtmlAttributeName("current-category")]
        public int CurrentCategory { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            var sb=new StringBuilder();
            if (PageCount > 1)
            {
                sb.Append("<ul class='pagination'>");

                sb.AppendFormat("<li class='page-item'>");

                sb.AppendFormat("<a class='page-link {0} {1}' tabindex='{2}' aria-disabled='{3}' href='/product/index?page={4}&category={5}'>Previous</a>", (CurrentPage == 1) ? "pe-none" : "", (CurrentPage == 1) ? "bg-secondary" : "", (CurrentPage == 1) ? "-1" : "", (CurrentPage == 1) ? "true" : "false", CurrentPage - 1, CurrentCategory);

                sb.AppendFormat("</li>");

                for (int i = 1; i <= PageCount; i++)
                {
                    sb.AppendFormat("<li class='{0}'>", (i == CurrentPage) ? "page-item active" : "page-item");

                    sb.AppendFormat("<a class='page-link' href='/product/index?page={0}&category={1}'>{2}</a>", i, CurrentCategory, i);

                    sb.AppendFormat("</li>");
                }

                sb.AppendFormat("<li class='page-item'>");

                sb.AppendFormat("<a class='page-link {0} {1}' tabindex='{2}' aria-disabled='{3}' href='/product/index?page={4}&category={5}'>Next</a>", (CurrentPage == PageCount) ? "pe-none" : "", (CurrentPage == PageCount) ? "bg-secondary" : "", (CurrentPage == PageCount) ? "-1" : "", (CurrentPage == PageCount) ? "true" : "false", CurrentPage + 1, CurrentCategory);

                sb.AppendFormat("</li>");

                sb.Append("</ul>");
            }

            output.Content.SetHtmlContent(sb.ToString());

        }

    }
}
