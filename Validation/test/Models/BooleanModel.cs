// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

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
}