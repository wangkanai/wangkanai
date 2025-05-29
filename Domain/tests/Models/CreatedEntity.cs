// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Models;

public class CreatedEntity : KeyGuidEntity, ICreatedEntity
{
    public DateTime? Created { get; set; }
}
