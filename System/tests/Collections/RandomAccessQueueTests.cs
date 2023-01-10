// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections;

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
		var queue = new StringQueue(StringQueue.DefaultCapacity + 5);

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

	[Fact]
	public void StronglyTypedClone()
	{
		var queue = new RandomAccessQueue<object>();

		var first  = new object();
		var second = new object();
		queue.Enqueue(first);
		queue.Enqueue(second);

		var clone = queue.Clone();

		Assert.Equal(queue.Count, clone.Count);

		Assert.Same(first, queue.Dequeue());
		Assert.Same(first, clone.Dequeue());
		Assert.Same(second, queue.Dequeue());
		Assert.Same(second, clone.Dequeue());

		Assert.NotSame(queue.SyncRoot, clone.SyncRoot);
	}

	[Fact]
	public void ICloneableClone()
	{
		var queue = new RandomAccessQueue<object>();

		var first  = new object();
		var second = new object();

		queue.Enqueue(first);
		queue.Enqueue(second);

		ICloneable cloneable = queue;
		var        clone     = (RandomAccessQueue<object>)cloneable.Clone();

		Assert.Equal(queue.Count, clone.Count);

		Assert.Same(first, queue.Dequeue());
		Assert.Same(first, clone.Dequeue());
		Assert.Same(second, queue.Dequeue());
		Assert.Same(second, clone.Dequeue());

		Assert.NotSame(queue.SyncRoot, clone.SyncRoot);
	}

	[Fact]
	public void Clear()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Clear();

		Assert.Equal(0, queue.Count);
	}

	[Fact]
	public void EnqueueWithIndex()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("3");
		queue.Enqueue("2", 1);

		Assert.Equal("1", queue.Dequeue());
		Assert.Equal("2", queue.Dequeue());
		Assert.Equal("3", queue.Dequeue());
	}

	[Fact]
	public void EnqueueWithNegativeIndex()
	{
		var queue = new StringQueue();
		queue.Enqueue("1");
		queue.Enqueue("3");

		Assert.Throws<ArgumentOutOfRangeException>(() => queue.Enqueue("2", -1));
	}

	[Fact]
	public void EnqueueWithExcessIndex()
	{
		var queue = new StringQueue();
		queue.Enqueue("1");
		queue.Enqueue("3");

		Assert.Throws<ArgumentOutOfRangeException>(() => queue.Enqueue("2", 3));
	}

	[Fact]
	public void EnqueueWithIndexEqualToCount()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("3");
		queue.Enqueue("2", 2);

		Assert.Equal("1", queue.Dequeue());
		Assert.Equal("3", queue.Dequeue());
		Assert.Equal("2", queue.Dequeue());
	}

	[Fact]
	public void StrongCopyTo()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		var array = new string[5];
		queue.CopyTo(array, 1);
		Assert.Null(array[0]);
		Assert.Equal("1", array[1]);
		Assert.Equal("2", array[2]);
		Assert.Equal("3", array[3]);
		Assert.Null(array[4]);
	}

	[Fact]
	public void StrongCopyToWithNullArray()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		Assert.Throws<ArgumentNullException>(() => queue.CopyTo((string[])null, 1));
	}

	[Fact]
	public void WeakCopyToNull()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		Assert.Throws<ArgumentNullException>(() => queue.CopyTo((Array)null, 0));
	}

	[Fact]
	public void CopyToWrongArrayType()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		Assert.Throws<ArgumentException>(() => queue.CopyTo(new object[5], 0));
	}

	[Fact]
	public void CopyToToShort()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		Assert.Throws<ArgumentException>(() => queue.CopyTo(new string[5], 3));
	}

	[Fact]
	public void CopyToNegativeIndex()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		Assert.Throws<ArgumentOutOfRangeException>(() => queue.CopyTo(new string[5], -1));
	}

	[Fact]
	public void WeakCopyTo()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		var array = new string[5];

		Array weak = array;
		queue.CopyTo(weak, 1);

		Assert.Null(array[0]);
		Assert.Equal("1", array[1]);
		Assert.Equal("2", array[2]);
		Assert.Equal("3", array[3]);
		Assert.Null(array[4]);
	}

	[Fact]
	public void ContainsNull()
	{
		var queue = new StringQueue();
		queue.Add("1");
		Assert.False(queue.Contains(null));
		queue.Add(null);
		Assert.True(queue.Contains(null));
	}

	[Fact]
	public void RemovePresentItem()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");

		Assert.True(queue.Remove("1"));
		Assert.False(queue.Contains("1"));
		Assert.Equal(1, queue.Count);
	}

	[Fact]
	public void RemoveMissingItem()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");

		Assert.False(queue.Remove("2"));
		Assert.Equal(1, queue.Count);
	}

	[Fact]
	public void RemoveMissingNull()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");

		Assert.False(queue.Remove(null));
		Assert.Equal(1, queue.Count);
	}

	[Fact]
	public void RemoveAtBeforeStart()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");

		Assert.Throws<ArgumentOutOfRangeException>(() => queue.RemoveAt(-1));
	}

	[Fact]
	public void RemoveAtAfterEnd()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");

		Assert.Throws<ArgumentOutOfRangeException>(() => queue.RemoveAt(2));
	}

	[Fact]
	public void RemoveAtStart()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");
		queue.RemoveAt(0);

		Assert.Equal(2, queue.Count);
		Assert.Equal("2", queue[0]);
		Assert.Equal("3", queue[1]);
	}

	[Fact]
	public void RemoveAtEnd()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");
		queue.RemoveAt(2);

		Assert.Equal(2, queue.Count);
		Assert.Equal("1", queue[0]);
		Assert.Equal("2", queue[1]);
	}

	[Fact]
	public void RemoveAtShuffleUp()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");
		queue.RemoveAt(1);

		Assert.Equal(2, queue.Count);
		Assert.Equal("1", queue[0]);
		Assert.Equal("3", queue[1]);
	}

	[Fact]
	public void RemoveAtShuffleDown()
	{
		var queue = new StringQueue();


		for (var i = 0; i < StringQueue.DefaultCapacity - 1; i++)
			queue.Enqueue(null);

		for (var i = 0; i < StringQueue.DefaultCapacity - 1; i++)
			Assert.Null(queue.Dequeue());

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");
		queue.RemoveAt(1);

		Assert.Equal(2, queue.Count);
		Assert.Equal("1", queue[0]);
		Assert.Equal("3", queue[1]);
	}

	[Fact]
	public void ContainMissingItem()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");

		Assert.True(queue.Contains("1"));
	}

	[Fact]
	public void ContainPresentItem()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");

		Assert.False(queue.Contains("2"));
	}

	[Fact]
	public void SimpleEnumeration()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");
		queue.Enqueue("3");

		var enumerator = queue.GetEnumerator();

		Assert.True(enumerator.MoveNext());
		Assert.Equal("1", enumerator.Current);
		Assert.True(enumerator.MoveNext());
		Assert.Equal("2", enumerator.Current);
		Assert.True(enumerator.MoveNext());
		Assert.Equal("3", enumerator.Current);
		Assert.False(enumerator.MoveNext());
	}

	[Fact]
	public void ChangeQueueDuringIterationThrowException()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");

		var enumerator = queue.GetEnumerator();
		Assert.True(enumerator.MoveNext());

		queue.Enqueue("3");

		Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
	}

	[Fact]
	public void ChangeQueueDuringIterationRemovingRemainingElement()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("2");

		var enumerator = queue.GetEnumerator();
		Assert.True(enumerator.MoveNext());
		Assert.True(enumerator.MoveNext());

		queue.Dequeue();
		queue.Dequeue();

		Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
	}

	[Fact]
	public void SyncRoot()
	{
		var queue = new StringQueue();

		var syncRoot = queue.SyncRoot;
		Assert.NotNull(syncRoot);

		queue.Enqueue("hi");
		Assert.Same(syncRoot, queue.SyncRoot);
	}

	[Fact]
	public void Add()
	{
		var queue = new StringQueue();

		Assert.Equal(0, queue.Count);
		queue.Add("1");
		queue.Add("2");
		Assert.Equal(2, queue.Count);

		Assert.Equal("1", queue.Dequeue());
		Assert.Equal("2", queue.Dequeue());
	}

	[Fact]
	public void AlwaysWritable()
	{
		var queue = new StringQueue();
		Assert.False(queue.IsReadOnly);
	}

	[Fact]
	public void NeverSynchronized()
	{
		var queue = new StringQueue();
		Assert.False(queue.IsSynchronized);
	}

	[Fact]
	public void NonGenericGetEnumerator()
	{
		var queue = new StringQueue();

		for (var i = 0; i < 5; i++)
			queue.Add(i.ToString());

		var count = 0;
		foreach (var x in (IEnumerable)queue)
		{
			Assert.Equal(count.ToString(), x);
			count++;
		}

		Assert.Equal(5, queue.Count);
		Assert.Equal(5, count);
	}

	[Fact]
	public void BinartySEarchComparableNull()
	{
		var queue = new StringQueue();

		Assert.Equal(~0, queue.BinarySearch(null));
		queue.Add("hi");
		Assert.Equal(~0, queue.BinarySearch(null));

		queue.Dequeue();
		queue.Add(null);
		Assert.Equal(0, queue.BinarySearch(null));
	}

	[Fact]
	public void BinarySearchIncomparable()
	{
		var queue = new RandomAccessQueue<object>();

		Assert.Throws<ArgumentException>(() => queue.BinarySearch(new object()));
	}

	[Fact]
	public void BinarySearchComparableEmpty()
	{
		var queue = new StringQueue();

		Assert.Equal(~0, queue.BinarySearch("1"));
	}

	[Fact]
	public void BinarySearchNullComparison()
	{
		var queue = new StringQueue();

		Assert.Throws<ArgumentNullException>(() => queue.BinarySearch("1", (Comparison<string>)null));
	}

	[Fact]
	public void BinarySearchComparisonEmpty()
	{
		var queue = new StringQueue();

		Assert.Equal(~0, queue.BinarySearch("1", (Comparison<string>)((x, y) => { throw new Exception("Don't expect to be called"); })));
	}

	[Fact]
	public void BinarySearchComparerNull()
	{
		var queue    = new StringQueue();
		var comparer = Comparer<string>.Default;

		Assert.Equal(~0, queue.BinarySearch(null, comparer));

		queue.Add("hi");
		Assert.Equal(~0, queue.BinarySearch(null, comparer));
		queue.Dequeue();
		queue.Add(null);

		Assert.Equal(0, queue.BinarySearch(null, comparer));
	}

	[Fact]
	public void BinarySearchNullComparer()
	{
		var queue = new StringQueue();

		Assert.Throws<ArgumentNullException>(() => queue.BinarySearch("1", (IComparer<string>)null));
	}

	[Fact]
	public void BinarySearchComparable()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("3");
		queue.Enqueue("5");

		Assert.Equal(0, queue.BinarySearch("1"));
		Assert.Equal(~1, queue.BinarySearch("2"));
		Assert.Equal(1, queue.BinarySearch("3"));
		Assert.Equal(~2, queue.BinarySearch("4"));
		Assert.Equal(2, queue.BinarySearch("5"));
		Assert.Equal(~3, queue.BinarySearch("6"));
	}

	[Fact]
	public void BinarySearchComparison()
	{
		var queue = new StringQueue();

		queue.Enqueue("1");
		queue.Enqueue("14");
		queue.Enqueue("50");
		queue.Enqueue("200");

		Comparison<string> comparison = delegate(string x, string y) {
			var first  = int.Parse(x);
			var second = int.Parse(y);

			return first.CompareTo(second);
		};

		Assert.Equal(0, queue.BinarySearch("1", comparison));
		Assert.Equal(~1, queue.BinarySearch("12", comparison));
		Assert.Equal(1, queue.BinarySearch("14", comparison));
		Assert.Equal(~2, queue.BinarySearch("45", comparison));
		Assert.Equal(2, queue.BinarySearch("50", comparison));
		Assert.Equal(~3, queue.BinarySearch("100", comparison));
		Assert.Equal(3, queue.BinarySearch("200", comparison));
		Assert.Equal(~4, queue.BinarySearch("500", comparison));
	}
}