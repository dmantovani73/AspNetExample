using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text;

namespace Example.WebApplication.TagHelpers;

public class SystemInfoTagHelper : TagHelper
{
    private readonly HtmlEncoder _htmlEncoder;
    public SystemInfoTagHelper(HtmlEncoder htmlEncoder)
    {
        _htmlEncoder = htmlEncoder;
    }

    [HtmlAttributeName("machine")]
    public bool IncludeMachine { get; set; } = true;

    [HtmlAttributeName("os")]
    public bool IncludeOS { get; set; } = true;

    public override void Process(
        TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "span";
        output.TagMode = TagMode.StartTagAndEndTag;
        var sb = new StringBuilder();

        if (IncludeMachine)
        {
            sb.Append(" <strong>Machine</strong> ");
            sb.Append(_htmlEncoder.Encode(Environment.MachineName));
        }

        if (IncludeOS)
        {
            sb.Append(" <strong>OS</strong> ");
            sb.Append(
               _htmlEncoder.Encode(RuntimeInformation.OSDescription));
        }
        output.Content.SetHtmlContent(sb.ToString());
    }
}
