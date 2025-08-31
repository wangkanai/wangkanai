// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Domain.Models;

namespace Wangkanai.Domain.Tests;

public class EntityTests
{
   [Fact]
   public void NewGuidEntity_ShouldHaveId()
   {
      var entity = new GuidEntity();
      Assert.NotEqual(Guid.NewGuid(), entity.Id);
   }

   [Fact]
   public void GuidEntity_IsTransient_ShouldBeTrue()
   {
      var entity = new TransientGuidEntity();
      Assert.True(entity.IsTransient());
   }

   [Fact]
   public void GuidEntity_IsTransient_ShouldBeFalse()
   {
      var entity = new GuidEntity();
      Assert.False(entity.IsTransient());
   }

   [Fact]
   public void NewIntEntity_ShouldHaveId()
   {
      var entity = new IntEntity();
      Assert.NotEqual(0, entity.Id);
   }

   [Fact]
   public void IntEntity_IsTransient_ShouldBeFalse()
   {
      var entity = new IntEntity();
      Assert.False(entity.IsTransient());
   }

   [Fact]
   public void IntEntity_IsTransient_ShouldBeTrue()
   {
      var entity = new TransientIntEntity();
      Assert.True(entity.IsTransient());
   }

   [Fact]
   public void Entity_Transient_HashCode()
   {
      var entity = new IntEntity();
      Assert.Equal(entity.Id.GetHashCode(), entity.GetHashCode());
      entity.Id = default;
      Assert.NotEqual(entity.Id.GetHashCode(), entity.GetHashCode());
   }

   [Fact]
   public void Entity_Equals_ShouldBeTrue()
   {
      var entity = new IntEntity();
      var other  = new IntEntity();
      Assert.True(entity.Equals(other));
      Assert.True(entity == other);
   }

   [Fact]
   public void Entity_Equals_ShouldBeFalse()
   {
      var entity = new IntEntity();
      var other  = new TransientIntEntity();
      Assert.False(entity.Equals(other));
      Assert.False(entity == other);
   }
}