// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wangkanai.EntityFramework;

public sealed class UtcDateTimeOffsetGenerator : ValueGenerator<DateTimeOffset>
{
	public override bool GeneratesTemporaryValues => false;

	public override DateTimeOffset Next(EntityEntry entry)
	{
		entry.ThrowIfNull();
		return DateTimeOffset.UtcNow;
	}
}
