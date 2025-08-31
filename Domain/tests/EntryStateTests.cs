// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Tests;

public class EntryStateTests
{
   [Fact]
   public void EntryState_HasFlagsAttribute()
   {
      // Verify that EntryState is decorated with the [Flags] attribute
      var attributes = typeof(EntryState).GetCustomAttributes(typeof(FlagsAttribute), false);
      Assert.Single(attributes);
   }

   [Fact]
   public void EntryState_ValuesAreUnique()
   {
      // Each flag value should be a unique power of 2 for proper bitwise operations
      Assert.Equal(1 << 0, (int)EntryState.Detached);
      Assert.Equal(1 << 1, (int)EntryState.Unchanged);
      Assert.Equal(1 << 2, (int)EntryState.Added);
      Assert.Equal(1 << 3, (int)EntryState.Deleted);
      Assert.Equal(1 << 4, (int)EntryState.Modified);
   }

   [Fact]
   public void EntryState_BitwiseCombinations()
   {
      // Test combining flags
      var addedAndModified = EntryState.Added | EntryState.Modified;

      Assert.True(addedAndModified.HasFlag(EntryState.Added));
      Assert.True(addedAndModified.HasFlag(EntryState.Modified));
      Assert.False(addedAndModified.HasFlag(EntryState.Deleted));
      Assert.False(addedAndModified.HasFlag(EntryState.Unchanged));
      Assert.False(addedAndModified.HasFlag(EntryState.Detached));

      // Test a complex combination
      var combination = EntryState.Detached | EntryState.Modified | EntryState.Deleted;
      Assert.True(combination.HasFlag(EntryState.Detached));
      Assert.True(combination.HasFlag(EntryState.Modified));
      Assert.True(combination.HasFlag(EntryState.Deleted));
      Assert.False(combination.HasFlag(EntryState.Added));
      Assert.False(combination.HasFlag(EntryState.Unchanged));
   }

   [Fact]
   public void EntryState_BitwiseOperations()
   {
      // Test adding and removing flags
      var state = EntryState.Added;
      state |= EntryState.Modified;
      Assert.Equal(EntryState.Added | EntryState.Modified, state);

      // Remove a flag
      state &= ~EntryState.Added;
      Assert.Equal(EntryState.Modified, state);

      // XOR operation
      var initialState = EntryState.Added | EntryState.Modified;
      var toggledState = initialState ^ EntryState.Modified;
      Assert.Equal(EntryState.Added, toggledState);
   }

   [Fact]
   public void EntryState_ToString()
   {
      // Test string representation
      Assert.Equal("Detached", EntryState.Detached.ToString());
      Assert.Equal("Added",    EntryState.Added.ToString());

      // Check combined flags string representation
      var combined = EntryState.Added | EntryState.Modified;
      Assert.Equal("Added, Modified", combined.ToString());
   }
}