// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Wangkanai.EntityFramework.Generators;

public sealed class DateTimeNowGenerator : ValueGenerator<DateTime>
{
   public override bool GeneratesTemporaryValues => false;

   public override DateTime Next(EntityEntry entry)
   {
      entry.ThrowIfNull();
      return DateTime.Now;
   }
}