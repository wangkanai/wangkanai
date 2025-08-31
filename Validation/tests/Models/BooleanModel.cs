// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class BooleanModel : BaseModel<BooleanModel>
{
   [Required]
   [RequireTrue]
   public bool WannaTrue { get; set; }

   [Required]
   [RequireFalse]
   public bool WannaFalse { get; set; }

   [Required]
   [RequireTrue]
   public bool? NullableTrue { get; set; }

   [Required]
   [RequireFalse]
   public bool? NullableFalse { get; set; }
}