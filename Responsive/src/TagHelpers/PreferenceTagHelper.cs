// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Wangkanai.Extensions;
using Wangkanai.Responsive.Services;

namespace Microsoft.AspNetCore.Mvc.TagHelpers;

[HtmlTargetElement(ElementName, Attributes = OnlyAttributeName, TagStructure = TagStructure.NormalOrSelfClosing)]
public class PreferenceTagHelper : TagHelper
{
   private const    string             ElementName       = "preference";
   private const    string             OnlyAttributeName = "only";
   private readonly IDeviceService     _device;
   private readonly IResponsiveService _responsive;

   public PreferenceTagHelper(IHtmlGenerator generator, IResponsiveService responsive, IDeviceService device)
   {
      Generator   = generator  ?? throw new ArgumentNullException(nameof(generator));
      _responsive = responsive ?? throw new ArgumentNullException(nameof(responsive));
      _device     = device     ?? throw new ArgumentNullException(nameof(device));
   }

   protected IHtmlGenerator Generator { get; }

   [HtmlAttributeName(OnlyAttributeName)]
   public string? Only { get; set; }

   private bool DisplayOnlyDevice => _device.Type == OnlyDevice;

   private Device OnlyDevice => EnumValues<Device>.Parse(Only ?? "desktop", true);

   public override void Process(TagHelperContext context, TagHelperOutput output)
   {
      if (context is null)
      {
         throw new ArgumentNullException(nameof(context));
      }

      if (output is null)
      {
         throw new ArgumentNullException(nameof(output));
      }

      output.TagName = null;

      if (string.IsNullOrWhiteSpace(Only))
      {
         return;
      }

      if (_responsive.HasPreferred())
      {
         return;
      }

      if (DisplayOnlyDevice)
      {
         return;
      }

      output.SuppressOutput();
   }
}