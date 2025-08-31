// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class UniqueModel : BaseModel<UniqueModel>
{
   [RequireUniqueChar]
   public string Unique1 { get; set; } = string.Empty;

   [RequireUniqueChar(2)]
   public string Unique2 { get; set; } = string.Empty;

   [RequireUniqueChar(3)]
   public string Unique3 { get; set; } = string.Empty;

   [RequireUniqueChar(4)]
   public string Unique4 { get; set; } = string.Empty;
}