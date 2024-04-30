// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.ObjectModel;

namespace Wangkanai.Extensions;

public class ObservableCollectionExtensionsTests
{
	[Fact]
	public void Observe_Add()
	{
		// Arrange
		var collection = new ObservableCollection<int>();
		var result     = 0;
		collection.Observe(x => result += x, x => result -= x);

		// Act
		collection.Add(1);
		collection.Add(2);
		collection.Add(3);

		// Assert
		Assert.Equal(6, result);
	}

	[Fact]
	public void Observe_Remove()
	{
		// Arrange
		var collection = new ObservableCollection<int> { 1, 2, 3 };
		var result     = 6;
		collection.Observe(x => result += x, x => result -= x);

		// Act
		collection.Remove(1);
		collection.Remove(2);
		collection.Remove(3);

		// Assert
		Assert.Equal(0, result);
	}

	[Fact]
	public void Observe_Add_Remove()
	{
		// Arrange
		var collection = new ObservableCollection<int> { 1, 2, 3 };
		var result     = 6;
		collection.Observe(x => result += x, x => result -= x);

		// Act
		collection.Add(4);
		collection.Remove(1);
		collection.Add(5);
		collection.Remove(2);
		collection.Add(6);
		collection.Remove(3);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void Observe_Remove_Add()
	{
		// Arrange
		var collection = new ObservableCollection<int> { 1, 2, 3 };
		var result     = 6;
		collection.Observe(x => result += x, x => result -= x);

		// Act
		collection.Remove(1);
		collection.Add(4);
		collection.Remove(2);
		collection.Add(5);
		collection.Remove(3);
		collection.Add(6);

		// Assert
		Assert.Equal(15, result);
	}

	[Fact]
	public void Observe_Remove_Add_Remove()
	{
		// Arrange
		var collection = new ObservableCollection<int> { 1, 2, 3 };
		var result     = 6;
		collection.Observe(x => result += x, x => result -= x);

		// Act
		collection.Remove(1);
		collection.Add(4);
		collection.Remove(2);
		collection.Add(5);
		collection.Remove(3);
		collection.Add(6);
		collection.Remove(4);
		collection.Add(7);
		collection.Remove(5);
		collection.Add(8);
		collection.Remove(6);
		collection.Add(9);

		// Assert
		Assert.Equal(24, result);
	}
}
