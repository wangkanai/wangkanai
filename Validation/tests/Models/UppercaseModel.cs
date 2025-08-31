// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class UppercaseModel : BaseModel<UppercaseModel>
{
   [RequireUppercase]
   public string Password { get; set; } = string.Empty;
}