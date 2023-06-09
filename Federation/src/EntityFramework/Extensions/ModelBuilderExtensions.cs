// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Extensions;
using Wangkanai.Federation.Options;

namespace Wangkanai.Federation.Extensions;

public static class ModelBuilderExtensions
{
	private static EntityTypeBuilder<TEntity> ToTable<TEntity>(this EntityTypeBuilder<TEntity> builder, TableConfiguration config)
		where TEntity : class
		=> config.Schema.IsNullOrWhiteSpace()
			   ? builder.ToTable(config.Name)
			   : builder.ToTable(config.Name, config.Schema);

	public static void ConfigureClientContext(this ModelBuilder builder, ConfigurationStoreOption options)
	{
		if (!options.DefaultSchema.IsNullOrWhiteSpace())
			builder.HasDefaultSchema(options.DefaultSchema);

	}
}