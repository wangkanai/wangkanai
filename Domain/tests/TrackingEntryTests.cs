// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Tests;

public class TrackingEntryTests
{
	[Fact]
	public void Constructor_WithEntity_SetsEntityProperty()
	{
		// Arrange
		var entity = new TestEntity();

		// Act
		var trackingEntry = new TrackingEntry { Entity = entity };

		// Assert
		Assert.Equal(entity, trackingEntry.Entity);
	}

	[Fact]
	public void EntryState_DefaultValue_IsUnchanged()
	{
		// Arrange & Act
		var trackingEntry = new TrackingEntry { Entity = new TestEntity() };

		// Assert
		Assert.Equal(EntryState.Unchanged, trackingEntry.EntryState);
	}

	[Fact]
	public void EntryState_CanBeSet_AndRetrieved()
	{
		// Arrange
		var trackingEntry = new TrackingEntry { Entity = new TestEntity() };

		// Act
		trackingEntry.EntryState = EntryState.Added;

		// Assert
		Assert.Equal(EntryState.Added, trackingEntry.EntryState);
	}

	[Fact]
	public void IsSubscribed_DefaultValue_IsFalse()
	{
		// Arrange & Act
		var trackingEntry = new TrackingEntry { Entity = new TestEntity() };

		// Assert
		Assert.False(trackingEntry.IsSubscribed);
	}

	[Fact]
	public void IsSubscribed_CanBeSet_AndRetrieved()
	{
		// Arrange
		var trackingEntry = new TrackingEntry { Entity = new TestEntity() };

		// Act
		trackingEntry.IsSubscribed = true;

		// Assert
		Assert.True(trackingEntry.IsSubscribed);
	}

	[Fact]
	public void ToString_WithEntity_ReturnsEntityAndState()
	{
		// Arrange
		var entity = new TestEntity();
		var trackingEntry = new TrackingEntry
		{
			Entity = entity,
			EntryState = EntryState.Modified
		};

		// Act
		var result = trackingEntry.ToString();

		// Assert
		Assert.Equal($"{entity} Modified", result);
	}

	[Fact]
	public void ToString_WithNullEntity_ReturnsNullAndState()
	{
		// Arrange
		var trackingEntry = new TrackingEntry
		{
			Entity = new TestEntity(),
			EntryState = EntryState.Deleted
		};

		// Use reflection to set Entity to null after initialization
		typeof(TrackingEntry).GetProperty("Entity")!.SetValue(trackingEntry, null);

		// Act
		var result = trackingEntry.ToString();

		// Assert
		Assert.Equal("null Deleted", result);
	}

	private class TestEntity
	{
		public override string ToString() => "TestEntity";
	}
}
