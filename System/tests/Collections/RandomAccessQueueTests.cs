// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Xunit;

using StringQueue = Wangkanai.Collections.RandomAccessQueue<string>;

namespace Wangkanai.Collections;

public class RandomAccessQueueTests
{
	[Fact]
	public void ConstructorWithCapacity()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity + 5);

		Assert.Equal(StringQueue.DefaultCapacity + 5, queue.Capacity);
	}

	[Fact]
	public void ConstructorWithSmallerThanDefaultCapacity()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity - 1);

		Assert.Equal(StringQueue.DefaultCapacity, queue.Capacity);
	}

	[Fact]
	public void SimpleAccess()
	{
		var queue = new StringQueue();

		Assert.Equal(0, queue.Count);
		queue.Enqueue("1");
		queue.Enqueue("2");
		Assert.Equal(2, queue.Count);

		Assert.Equal("1", queue.Dequeue());
		Assert.Equal("2", queue.Dequeue());

		Assert.Equal(0, queue.Count);
	}

	[Fact]
	public void EnqueueAndDequeuePastInitialCapacity()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity);

		for (var i = 0; i < StringQueue.DefaultCapacity + 5; i++)
			queue.Enqueue(i.ToString());

		for (var i = 0; i < StringQueue.DefaultCapacity + 5; i++)
			Assert.Equal(i.ToString(), queue.Dequeue());
	}
	
	[Fact]
	public void EnqueueAndDequeueAtStartPastInitialCapacity()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity);

		for (var i = 0; i < StringQueue.DefaultCapacity + 5; i++)
			queue.Enqueue(i.ToString(), 0);

		for (var i = StringQueue.DefaultCapacity + 4; i >= 0; i--)
			Assert.Equal(i.ToString(), queue.Dequeue());
	}

	[Fact]
	public void TrimToSize()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity);

		for (var i = 0; i < StringQueue.DefaultCapacity + 5; i++) 
			queue.Enqueue(i.ToString());
		
		Assert.NotEqual(queue.Capacity, queue.Count);
		
		queue.TrimToSize();
		
		Assert.Equal(queue.Capacity, queue.Count);

		for (var i = 0; i < StringQueue.DefaultCapacity + 5; i++) 
			Assert.Equal(i.ToString(), queue.Dequeue());
	}

	[Fact]
	public void TrimToSizeNoOp()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity);

		for (var i = 0; i < queue.Capacity; i++) 
			queue.Enqueue(i.ToString());
		
		Assert.Equal(queue.Capacity, queue.Count);

		queue.TrimToSize();
		
		Assert.Equal(queue.Capacity, queue.Count);

		for (var i = 0; i < StringQueue.DefaultCapacity; i++) 
			Assert.Equal(i.ToString(), queue.Dequeue());
	}

	[Fact]
	public void TrimToSizeWithWrap()
	{
		var queue = new StringQueue(StringQueue.DefaultCapacity+5);

		for (var i = 0; i < 5; i++) 
			queue.Enqueue("Ignore me");

		for (var i = 0; i < 5; i++) 
			queue.Dequeue();

		for (var i = 0; i < StringQueue.DefaultCapacity + 2; i++)
			queue.Enqueue(i.ToString());
		
		Assert.NotEqual(queue.Capacity, queue.Count);
		
		queue.TrimToSize();
		
		Assert.Equal(queue.Capacity, queue.Count);

		for (var i = 0; i < StringQueue.DefaultCapacity + 2; i++) 
			Assert.Equal(i.ToString(), queue.Dequeue());
	}
	
	[Fact]
	public void DequeueOnEmptyIsInvalid()
	{
		var queue = new StringQueue();

		Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
	}
	
	// [Fact]
	// public void 
}