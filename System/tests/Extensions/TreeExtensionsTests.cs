// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Extensions;

public class TreeExtensionsTests
{
	[Fact]
	public void Traverse()
	{
		var root = new Node<int>(1)
		           {
			           Children = new List<Node<int>>
			                      {
				                      new Node<int>(2)
				                      {
					                      Children = new List<Node<int>>
					                                 {
						                                 new Node<int>(3)
					                                 }
				                      }
			                      }
		           };

		var result = root.Traverse(n => n.Children).ToList();
		Assert.Equal(3, result.Count);
		Assert.Equal(1, result[0].Value);
		Assert.Equal(2, result[1].Value);
		Assert.Equal(3, result[2].Value);
	}
}

public class Node<T>(T value)
	where T : struct
{
	public T             Value    { get; set; } = value;
	public List<Node<T>> Children { get; set; } = new();
}
