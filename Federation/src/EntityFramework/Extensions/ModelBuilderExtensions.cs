// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Wangkanai.Federation.Extensions;

public static class ModelBuilderExtensions
{
	private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> entityTypeBuilder, string name)
		where TEntity : class
		=> entityTypeBuilder.ToTable(name);
}