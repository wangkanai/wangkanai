// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.FileProviders;

namespace Wangkanai.Webmaster.TagHelpers;

[HtmlTargetElement(InlineImgAttributeName, Attributes = SrcWildcardAttributeName, TagStructure = TagStructure.WithoutEndTag)]
public class InlineImgTagHelper : UrlResolutionTagHelper
{
    private const string InlineImgAttributeName   = "inline-img";
    private const string SrcAttributeName         = "src";
    private const string SrcWildcardAttributeName = "[src^='~/']";

    private readonly IFileProvider _fileProvider;

    [ActivatorUtilitiesConstructor]
    public InlineImgTagHelper(IWebHostEnvironment webHostEnvironment, IUrlHelperFactory urlHelperFactory, HtmlEncoder htmlEncoder)
        : base(urlHelperFactory, htmlEncoder)
    {
        _fileProvider = webHostEnvironment.WebRootFileProvider;
    }

    [HtmlAttributeName(SrcAttributeName)]
    public string Src { get; set; }

    public override int Order => -1000;

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {
        var fileContent = await GetFileContentBase64Async();
        if (fileContent is null)
        {
            output.SuppressOutput();
            return;
        }

        var contentType       = GetFileContentType(Src);
        var srcAttributeValue = $"data:{contentType};base64,{fileContent}";

        output.TagName = "img";
        output.Attributes.RemoveAll("src");
        output.Attributes.Add(SrcAttributeName, srcAttributeValue);
        output.TagMode = TagMode.SelfClosing;
        output.Content.AppendHtml(fileContent);
    }

    private async Task<string> GetFileContentBase64Async()
    {
        await using var stream = GetFileInfo.CreateReadStream();
        await using var writer = new MemoryStream();

        await stream.CopyToAsync(writer);
        var offset = writer.Seek(0, SeekOrigin.Begin);

        return Convert.ToBase64String(writer.ToArray());
    }

    private IFileInfo GetFileInfo => _fileProvider.GetFileInfo(Src);

    private string GetFileContentType(string path)
    {
        if (path.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            return "image/jpeg";
        if (path.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            return "image/gif";
        if (path.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            return "image/png";
        if (path.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            return "image/svg+xml";
        throw new ArgumentException("Unknown file type");
    }
}