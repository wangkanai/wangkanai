// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Wangkanai.Nation.Seeds;

namespace Wangkanai.Nation.Configurations;

public sealed class CountryConfiguration : IEntityTypeConfiguration<Models.Country>
{
	public void Configure(EntityTypeBuilder<Models.Country> builder)
	{
		builder.Property(x => x.Iso)
			   .HasMaxLength(2)
			   .IsRequired();

		builder.Property(x => x.Name)
			   .HasMaxLength(100)
			   .IsRequired();

		builder.Property(x => x.Native)
			   .HasMaxLength(100)
			   .IsUnicode()
			   .IsRequired();

		builder.Property(x => x.Population);

		builder.HasData(CountrySeed.Dataset);
	}
}
