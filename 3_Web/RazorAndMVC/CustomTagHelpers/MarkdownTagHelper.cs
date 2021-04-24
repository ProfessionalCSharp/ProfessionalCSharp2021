using Markdig;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace CustomTagHelpers
{
    [HtmlTargetElement("markdown", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement(Attributes = "markdownfile")]
    public class MarkdownTagHelper : TagHelper
    {
        private readonly IHostEnvironment _env;
        public MarkdownTagHelper(IHostEnvironment env) => _env = env;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string markdown;
            if (MarkdownFile is not null)
            {
                string filename = Path.Combine(_env.ContentRootPath, MarkdownFile);
                markdown = File.ReadAllText(filename);
            }
            else
            {
                markdown = (await output.GetChildContentAsync()).GetContent();
            }
            output.Content.SetHtmlContent(Markdown.ToHtml(markdown));
        }

        [HtmlAttributeName("markdownfile")]
        public string? MarkdownFile { get; set; }
    }
}
