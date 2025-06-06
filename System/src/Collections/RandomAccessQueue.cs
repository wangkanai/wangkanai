// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class RandomAccessQueue<T> : ICollection<T>, ICollection, ICloneable
{
	public const int DefaultCapacity = 16;

	private int _version;
	private T[] _buffer;
	private int _count;
	private int _start;

	/// <summary>
	/// The number of items in the queue
	/// </summary>
	public int Count => _count;

	/// <summary>
	/// Current capacity of the queue - the size of the buffer
	/// </summary>
	public int Capacity => _buffer.Length;

	/// <summary>
	/// Returns false, to indicate that the queue is not read-only
	/// </summary>
	public bool IsReadOnly => false;

	/// <summary>
	/// Returns false, to in indicate that the queue is not synchronized
	/// </summary>
	public bool IsSynchronized => false;

	/// <summary>
	/// An object reference to synchronize on when using the queue from multiple threads.
	/// This reference is not used for anywhere in the class itself.
	/// The same reference will always be returned for the same queue, and this will never be the same as the reference returned for a different queue, even a clone.
	/// </summary>
	public object SyncRoot => _syncRoot;

	private readonly object _syncRoot = new();

	/// <summary>
	/// Initializes a new instance of the <see cref="RandomAccessQueue{T}"/> class that is empty and has the default initial capacity.
	/// </summary>
	public RandomAccessQueue() : this(DefaultCapacity) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="RandomAccessQueue{T}"/> class that is empty and has the specified initial capacity (or the default capacity if that is higher.
	/// </summary>
	/// <param name="capacity">The initial capacity of the queue</param>
	public RandomAccessQueue(int capacity)
		=> _buffer = new T[System.Math.Max(capacity, DefaultCapacity)];

	/// <summary>
	/// Private constructor for cloning
	/// </summary>
	/// <param name="buffer">The buffer to clone for use in this queue</param>
	/// <param name="count">The number of "valid" elements in the buffer</param>
	/// <param name="start">The first valid element in the queue</param>
	private RandomAccessQueue(T[] buffer, int count, int start)
	{
		_buffer = (T[])buffer.Clone();
		_count  = count;
		_start  = start;
	}

	/// <summary>
	/// Indexer for the queue, allowing items to be accessed by index and replaced.
	/// </summary>
	/// <param name="index">The index in the queue</param>
	public T this[int index]
	{
		get
		{
			index.ThrowIfOutOfRange(0, _count);
			return _buffer[(_start + index) % Capacity];
		}
		set
		{
			index.ThrowIfOutOfRange(0, _count);
			_version++;
			_buffer[(_start + index) % Capacity] = value;
		}
	}

	/// <summary>
	/// Clear the queue without resizing the buffer
	/// </summary>
	public void Clear()
	{
		_start = 0;
		_count = 0;
		((IList)_buffer).Clear();
	}

	/// <summary>
	/// Creates a new queue with the same contents as this queue.
	/// The queues are separate, however - adding an item to the returned queue doesn't affect this queue or vice versa.
	/// </summary>
	/// <returns></returns>
	object ICloneable.Clone() => Clone();

	/// <summary>
	/// Strongly type version if <see cref="ICloneable.Clone"/>
	/// Create a new queue with the same contents as the queue.
	/// </summary>
	/// <returns>A clone of the current queue</returns>
	public RandomAccessQueue<T> Clone() => new(_buffer, _count, _start);


	/// <summary>
	/// Adds an item to the queue.
	/// </summary>
	/// <param name="item">The item to add</param>
	public void Add(T item) => Enqueue(item);

	/// <summary>
	/// Removes the given item from the queue, if it exists. The first equal value is removed.
	/// </summary>
	/// <param name="item">The item to remove</param>
	public bool Remove(T item)
	{
		if (item.TrueIfNull())
		{
			for (var i = 0; i < Count; i++)
			{
				if (this[i] is not null)
					return false;
				RemoveAt(i);
			}

			return true;
		}

		var comparer = EqualityComparer<T>.Default;
		for (var i = 0; i < Count; i++)
		{
			if (!comparer.Equals(this[i], item))
				continue;
			RemoveAt(i);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Returns whether the queue contains the given item,
	/// using the default equality comparer for the item to find is non-null.
	/// </summary>
	public bool Contains(T item)
	{
		if (item is null)
		{
			for (var i = 0; i < Count; i++)
				if (this[i] is null)
					return true;
			return false;
		}

		var comparer = EqualityComparer<T>.Default;
		for (var i = 0; i < Count; i++)
			if (comparer.Equals(this[i], item))
				return true;
		return false;
	}

	/// <summary>
	/// Copies the elements of the queue to the given array, starting at the given index in the array.
	/// </summary>
	/// <param name="array">The array to copy the contents of the queue into</param>
	/// <param name="arrayIndex">The zero-based index in array at which coping begins</param>
	/// <exception cref="ArgumentException">When not enough space in the array</exception>
	public void CopyTo(T[] array, int arrayIndex)
	{
		array.ThrowIfNull();
		arrayIndex.ThrowIfOutOfRange(0, array.Length);

		if (array.Length < arrayIndex + Count)
			throw new ArgumentException("Not enough space in the array", nameof(array));

		for (var i = 0; i < Count; i++)
			array[i + arrayIndex] = this[i];
	}

	/// <summary>
	/// Copies the elements of the queue to the given array, beginning at the given index.
	/// </summary>
	/// <param name="array">The array to copy to the contents of the queue into</param>
	/// <param name="index">The zero-based index in array at which coping begins</param>
	public void CopyTo(Array array, int index)
	{
		array.ThrowIfNull();

		var strong = array as T[];
		var name   = array.GetType().GetElementType()!.Name;
		strong.ThrowIfNull<ArgumentException>($"Cannot copy elements of type {typeof(T).Name} to an array of type {name}");
		CopyTo(strong!, index);
	}

	/// <summary>
	/// Resizes the buffer to just fit the current number of items in the queue.
	/// The buffer size is never set to less than the default capacity.
	/// </summary>
	public void TrimToSize()
	{
		var newCapacity = System.Math.Max(Count, DefaultCapacity);
		if (Capacity == newCapacity)
			return;

		Resize(newCapacity, -1);
	}

	/// <summary>
	/// Adds an item to the end of the queue
	/// </summary>
	/// <param name="value">The item to add to the queue.</param>
	public void Enqueue(T value)
		=> Enqueue(value, _count);

	/// <summary>
	/// Adds an object at the specified index
	/// </summary>
	/// <param name="value">The item to add to the queue</param>
	/// <param name="index">The index of the newly added item</param>
	public void Enqueue(T value, int index)
	{
		if (_count == Capacity)
		{
			Resize(_count * 2, index);
			_count++;
		}
		else
		{
			_count++;
			// How to make this more efficient? foreach?
			for (int i = _count - 2; i >= index; i--)
				this[i + 1] = this[i];
		}

		this[index] = value;
	}

	/// <summary>
	/// Removes on T from the start of the queue, returning it.
	/// </summary>
	/// <returns>The item at the head of the queue</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public T Dequeue()
	{
		if (_count == 0)
			throw new InvalidOperationException("Dequeue called from an empty queue");

		var result = this[0];
		this[0] = default!;

		_start++;
		if (_start == Capacity)
			_start = 0;

		_count--;
		return result;
	}

	/// <summary>
	/// Resizes the queue to a new capacity, optionally leaving a at the given logical index so that a new item can be added in with further copying.
	/// </summary>
	/// <param name="newCapacity">The new capacity</param>
	/// <param name="gap">The logical index at which in insert a gap, or -1 for no gap</param>
	private void Resize(int newCapacity, int gap)
	{
		var newBuffer = new T[newCapacity];
		if (gap == -1)
		{
			int first;
			int second;
			if (_buffer.Length - _start >= _count)
			{
				first  = _count;
				second = 0;
			}
			else
			{
				first  = _buffer.Length - _start;
				second = _count - first;
			}

			Array.Copy(_buffer, _start, newBuffer, 0, first);
			Array.Copy(_buffer, 0, newBuffer, first, second);
		}
		else
		{
			var outIndex = 0;
			var inIndex  = _start;

			for (var i = 0; i < _count; i++)
			{
				if (i == gap)
					outIndex++;

				newBuffer[outIndex] = _buffer[inIndex];
				outIndex++;
				inIndex++;
				if (inIndex == _buffer.Length)
					inIndex = 0;
			}
		}

		_buffer = newBuffer;
		_start  = 0;
	}

	/// <summary>
	/// Removes an item from the queue at the specified index.
	/// </summary>
	/// <param name="index">The index of the item to remove</param>
	/// <returns>The item which has been removed from the</returns>
	public T RemoveAt(int index)
	{
		index.ThrowIfOutOfRange(0, _count);

		if (index == 0)
			return Dequeue();

		var result = this[index];

		if (index == _count - 1)
		{
			this[index] = default!;
			_count--;
			return result;
		}

		if (_start + index >= Capacity)
		{
			Array.Copy(
				_buffer, _start + index - Capacity + 1,
				_buffer, _start + index - Capacity,
				_count - index - 1);

			_buffer[_start + _count - 1 - Capacity] = default!;
		}
		else
		{
			Array.Copy(_buffer, _start, _buffer, _start + 1, index);
			_buffer[_start] = default!;
			_start++;
		}

		_count--;
		_version++;
		return result;
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	public IEnumerator<T> GetEnumerator()
	{
		var original = _version;

		for (var i = 0; i < Count; i++)
		{
			yield return this[i];

			if (_version != original)
				throw new InvalidOperationException("Collection was modified after the enumerator was created");
		}
	}

	#region Binary Search

	public int BinarySearch(T value)
	{
		if (value is null)
			if (_count == 0 || !EqualityComparer<T>.Default.Equals(_buffer[_start], default))
				return ~0;
			else
				return 0;

		var comparable = value as IComparable;
		comparable.ThrowIfNull<ArgumentException>($"{nameof(value)} does not implement {nameof(IComparable)}");

		if (_count == 0)
			return ~0;

		return BinarySearch(test => {
			var element = this[test];
			return element is null ? 1 : comparable!.CompareTo(element);
		});
	}

	public int BinarySearch(T value, Comparison<T> comparison)
		=> BinarySearch(value, new ComparisonComparer<T>(comparison));

	public int BinarySearch(T value, Func<T, T, int> projection)
		=> BinarySearch(value, new ComparisonComparer<T>((x, y) => projection(x, y)));

	public int BinarySearch(T value, IComparer<T> comparer)
	{
		comparer.ThrowIfNull();

		if (_count == 0)
			return ~0;

		return BinarySearch(test => comparer.Compare(value, this[test]));
	}

	private int BinarySearch(Func<int, int> comparer)
	{
		var min = 0;
		var max = _count - 1;

		while (min <= max)
		{
			var test   = (min + max) / 2;
			var result = comparer(test);

			if (result == 0)
				return test;
			if (result < 0)
				max = test - 1;
			if (result > 0)
				min = test + 1;
		}

		return ~min;
	}

	#endregion
}
