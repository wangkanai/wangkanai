// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Domain;

namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

public static class EntityTypeBuilderExtensions
{
	public static void KeyGuidNewOnAdd<T>(this EntityTypeBuilder<T> builder)
		where T : class, IEntity
		=> builder.Property(x => x.Id)
		          .ValueGeneratedOnAdd();
	
	public static void HasDefaultCreated<T>(this EntityTypeBuilder<T> builder)
		where T : class, ICreatedEntity
		=> builder.Property(x => x.Created)
		          .HasDefaultValue(DateTime.Now)
		          .ValueGeneratedOnAdd();
}