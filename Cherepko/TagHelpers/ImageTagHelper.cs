using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace Cherepko.TagHelpers
{
    [HtmlTargetElement(tag: "img", Attributes = "img-action, img-controller")]
    public class ImageTagHelper : TagHelper
    {
        public string ImgAction { get; set; }
        public string ImgController { get; set; }
        LinkGenerator linkGenerator;
        public ImageTagHelper(LinkGenerator LinkGenerator)
        {
            linkGenerator = LinkGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var uri = linkGenerator.GetPathByAction(ImgAction, ImgController);
            output.Attributes.Add("src", uri);
        }
    }

}
