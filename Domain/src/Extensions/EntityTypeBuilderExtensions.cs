﻿// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Numerics;

using Wangkanai.Domain;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
	public static void NewGuidOnAdd<T>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity<Guid>
		=> builder.Property(x => x.Id)
		          .ValueGeneratedOnAdd();

	public static void NewKeyOnAdd<T>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity<int>
		=> builder.Property(x => x.Id)
		          .ValueGeneratedOnAdd();

	public static void HasDefaultCreated<T>(this EntityTypeBuilder<T> builder)
		where T : class, ICreatedEntity
		=> builder.Property(x => x.Created)
		          .HasDefaultValue(DateTime.Now)
		          .ValueGeneratedOnAdd();

	public static void HasDefaultCreatedAndUpdated<T>(this EntityTypeBuilder<T> builder)
		where T : class, ICreatedEntity, IUpdatedEntity
	{
		builder.HasDefaultCreated();

		builder.Property(x => x.Updated)
		       .HasDefaultValue(DateTime.Now)
		       .ValueGeneratedOnAddOrUpdate();
	}
}