using Markdig;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Threading.Tasks;

namespace CustomTagHelpers
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdownfile")]
    public class MarkdownTagHelper : TagHelper
    {
        private readonly IWebHostEnvironment _env;
        public MarkdownTagHelper(IWebHostEnvironment env) => _env = env;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string markdown;
            if (MarkdownFile is not null)
            {
                string filename = Path.Combine(_env.WebRootPath, MarkdownFile);
                markdown = File.ReadAllText(filename);
            }
            else
            {
                markdown = (await output.GetChildContentAsync()).GetContent();
            }

            string html = Markdown.ToHtml(markdown);
            output.Content.SetHtmlContent(html);
        }

        [HtmlAttributeName("markdownfile")]
        public string? MarkdownFile { get; set; }
    }
}
