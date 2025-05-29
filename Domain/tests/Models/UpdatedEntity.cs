// filepath: /Users/wangkanai/Sources/wangkanai/Domain/tests/Models/UpdatedEntity.cs
// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Models;

public class UpdatedEntity : KeyGuidEntity, IUpdatedEntity
{
    public DateTime? Updated { get; set; }
}
