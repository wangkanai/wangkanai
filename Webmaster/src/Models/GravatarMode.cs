// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel;

namespace Wangkanai.Webmaster.Models;

public enum GravatarMode
{
   [Display(Name = "404")]
   NotFound,

   [Display(Name = "Mp")]
   Mp,

   [Display(Name = "Identicon")]
   Identicon,

   [Display(Name = "Monsterid")]
   Monsterid,

   [Display(Name = "Wavatar")]
   Wavatar,

   [Display(Name = "Retro")]
   Retro,

   [Display(Name = "Blank")]
   Blank,

   [Browsable(false)]
   [EditorBrowsable(EditorBrowsableState.Never)]
   Default
}

public static class GravatarModeExtensions
{
   public static string Value(this GravatarMode mode)
      => mode == GravatarMode.NotFound
         ? "404"
         : mode.ToString().ToLower();
}