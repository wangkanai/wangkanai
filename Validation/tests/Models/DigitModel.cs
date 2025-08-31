// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class DigitModel : BaseModel<DigitModel>, IPasswordModel
{
   [RequireDigit]
   public string Password { get; set; } = string.Empty;
}