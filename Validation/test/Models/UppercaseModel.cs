// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.ComponentModel.DataAnnotations;

namespace Wangkanai.Validation.Models;

public class UppercaseModel : BaseModel<UppercaseModel>
{
    [RequireUppercase]
    public string Password { get; set; }
}