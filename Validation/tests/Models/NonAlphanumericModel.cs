// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class NonAlphanumericModel : BaseModel<NonAlphanumericModel>, IPasswordModel
{
   [Required]
   [RequireNonAlphanumeric]
   public string Password { get; set; } = string.Empty;
}