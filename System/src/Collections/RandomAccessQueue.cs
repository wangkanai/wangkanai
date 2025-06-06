// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

/// <summary>Represents a generic collection that provides random access to elements and supports queue-like operations.</summary>
/// <typeparam name="T">The type of elements in the queue.</typeparam>
public sealed class RandomAccessQueue<T> : ICollection<T>, ICollection, ICloneable
{
	/// <summary>The default initial capacity of the queue when no capacity is explicitly specified.</summary>
	public const int DefaultCapacity = 16;

	private int _version;
	private T[] _buffer;
	private int _start;

	/// <summary>The number of items in the queue</summary>
	public int Count { get; private set; }

	/// <summary>Current capacity of the queue - the size of the buffer</summary>
	public int Capacity => _buffer.Length;

	/// <summary>Returns false to indicate that the queue is not read-only</summary>
	public bool IsReadOnly => false;

	/// <summary>Returns false to indicate that the queue is not synchronized</summary>
	public bool IsSynchronized => false;

	/// <summary>
	/// An object reference to synchronize on when using the queue from multiple threads.
	/// This reference is not used for anywhere in the class itself.
	/// The same reference will always be returned for the same queue, and this will never be the same as the reference returned for a different queue, even a clone.
	/// </summary>
	public object SyncRoot { get; } = new();

	/// <summary>Initializes a new instance of the <see cref="RandomAccessQueue{T}"/> class that is empty and has the default initial capacity.</summary>
	public RandomAccessQueue() : this(DefaultCapacity) { }

	/// <summary>Initializes a new instance of the <see cref="RandomAccessQueue{T}"/> class that is empty and has the specified initial capacity (or the default capacity if that is higher.</summary>
	/// <param name="capacity">The initial capacity of the queue</param>
	public RandomAccessQueue(int capacity)
		=> _buffer = new T[System.Math.Max(capacity, DefaultCapacity)];

	/// <summary>Private constructor for cloning</summary>
	/// <param name="buffer">The buffer to clone for use in this queue</param>
	/// <param name="count">The number of "valid" elements in the buffer</param>
	/// <param name="start">The first valid element in the queue</param>
	private RandomAccessQueue(T[] buffer, int count, int start)
	{
		_buffer = (T[])buffer.Clone();
		Count   = count;
		_start  = start;
	}

	/// <summary>Indexer for the queue, allowing items to be accessed by index and replaced.</summary>
	/// <param name="index">The index in the queue</param>
	public T this[int index]
	{
		get
		{
			index.ThrowIfOutOfRange(0, Count);
			return _buffer[(_start + index) % Capacity];
		}
		set
		{
			index.ThrowIfOutOfRange(0, Count);
			_version++;
			_buffer[(_start + index) % Capacity] = value;
		}
	}

	/// <summary>Clear the queue without resizing the buffer</summary>
	public void Clear()
	{
		_start = 0;
		Count  = 0;
		((IList)_buffer).Clear();
	}

	/// <summary>
	/// Creates a new queue with the same contents as this queue.
	/// The queues are separate, however - adding an item to the returned queue doesn't affect this queue or vice versa.
	/// </summary>
	/// <returns></returns>
	object ICloneable.Clone() => Clone();

	/// <summary>Strongly type version if <see cref="ICloneable.Clone"/> Create a new queue with the same contents as the queue.</summary>
	/// <returns>A clone of the current queue</returns>
	public RandomAccessQueue<T> Clone() => new(_buffer, Count, _start);

	/// <summary>Adds an item to the queue.</summary>
	/// <param name="item">The item to add</param>
	public void Add(T item) => Enqueue(item);

	/// <summary>Removes the given item from the queue if it exists. The first equal value is removed.</summary>
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

	/// <summary>Returns whether the queue contains the given item, using the default equality comparer for the item to find is non-null.</summary>
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

	/// <summary>Copies the elements of the queue to the given array, starting at the given index in the array.</summary>
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

	/// <summary>Resizes the buffer to just fit the current number of items in the queue. The buffer size is never set to less than the default capacity.</summary>
	public void TrimToSize()
	{
		var newCapacity = System.Math.Max(Count, DefaultCapacity);
		if (Capacity == newCapacity)
			return;

		Resize(newCapacity, -1);
	}

	/// <summary>Adds an item to the end of the queue.</summary>
	/// <param name="value">The item to add to the queue.</param>
	public void Enqueue(T value)
		=> Enqueue(value, Count);

	/// <summary>Adds an object at the specified index.</summary>
	/// <param name="value">The item to add to the queue</param>
	/// <param name="index">The index of the newly added item</param>
	public void Enqueue(T value, int index)
	{
		if (Count == Capacity)
		{
			Resize(Count * 2, index);
			Count++;
		}
		else
		{
			Count++;
			// How to make this more efficient? foreach?
			for (int i = Count - 2; i >= index; i--)
				this[i + 1] = this[i];
		}

		this[index] = value;
	}

	/// <summary>Removes on T from the start of the queue, returning it.</summary>
	/// <returns>The item at the head of the queue</returns>
	/// <exception cref="InvalidOperationException"></exception>
	public T Dequeue()
	{
		if (Count == 0)
			throw new InvalidOperationException("Dequeue called from an empty queue");

		var result = this[0];
		this[0] = default!;

		_start++;
		if (_start == Capacity)
			_start = 0;

		Count--;
		return result;
	}

	/// <summary>Resizes the queue to a new capacity, optionally leaving at the given logical index so that a new item can be added in with further copying.</summary>
	/// <param name="newCapacity">The new capacity</param>
	/// <param name="gap">The logical index at which in insert a gap, or -1 for no gap</param>
	private void Resize(int newCapacity, int gap)
	{
		var newBuffer = new T[newCapacity];
		if (gap == -1)
		{
			int first;
			int second;
			if (_buffer.Length - _start >= Count)
			{
				first  = Count;
				second = 0;
			}
			else
			{
				first  = _buffer.Length - _start;
				second = Count - first;
			}

			Array.Copy(_buffer, _start, newBuffer, 0, first);
			Array.Copy(_buffer, 0, newBuffer, first, second);
		}
		else
		{
			var outIndex = 0;
			var inIndex  = _start;

			for (var i = 0; i < Count; i++)
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

	/// <summary>Removes an item from the queue at the specified index.</summary>
	/// <param name="index">The index of the item to remove</param>
	/// <returns>The item which has been removed from the</returns>
	public T RemoveAt(int index)
	{
		index.ThrowIfOutOfRange(0, Count);

		if (index == 0)
			return Dequeue();

		var result = this[index];

		if (index == Count - 1)
		{
			this[index] = default!;
			Count--;
			return result;
		}

		if (_start + index >= Capacity)
		{
			Array.Copy(
				_buffer, _start + index - Capacity + 1,
				_buffer, _start + index - Capacity,
				Count - index - 1);

			_buffer[_start + Count - 1 - Capacity] = default!;
		}
		else
		{
			Array.Copy(_buffer, _start, _buffer, _start + 1, index);
			_buffer[_start] = default!;
			_start++;
		}

		Count--;
		_version++;
		return result;
	}

	IEnumerator IEnumerable.GetEnumerator()
		=> GetEnumerator();

	/// <summary>Returns an enumerator that iterates through the elements of the <see cref="RandomAccessQueue{T}"/>.</summary>
	/// <returns>An enumerator that can be used to iterate through the collection.</returns>
	/// <exception cref="InvalidOperationException">Thrown if the collection is modified after the enumerator is created.</exception>
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

	/// <summary>Searches for the specified object within the queue and returns the zero-based index of the object.</summary>
	/// <param name="value">The object to locate in the queue. The value must implement <see cref="IComparable"/>.</param>
	/// <returns>The zero-based index of the value in the queue if found; otherwise, a negative number indicating the bitwise complement of the index where the specified value could be inserted.</returns>
	/// <exception cref="ArgumentException">Thrown when the <paramref name="value"/> does not implement <see cref="IComparable"/>.</exception>
	public int BinarySearch(T value)
	{
		if (value is null)
			if (Count == 0 || _buffer[_start].NotNull())
				return ~0;
			else
				return 0;

		var comparable = value as IComparable;
		comparable.ThrowIfNull<ArgumentException>($"{nameof(value)} does not implement {nameof(IComparable)}");

		if (Count == 0)
			return ~0;

		return BinarySearch(test => {
			var element = this[test];
			return element is null ? 1 : comparable!.CompareTo(element);
		});
	}

	/// <summary>
	/// Searches the queue for a given item and returns the index of the item if found.
	/// The search is performed using a binary search algorithm, and the comparison logic is determined by the specified comparer.
	/// </summary>
	/// <param name="value">The item to locate in the queue.</param>
	/// <param name="comparison">An <see cref="IComparer{T}"/> implementation to use for comparing elements during the search.</param>
	/// <returns>
	/// The zero-based index of the specified item in the queue, if found;
	/// otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than the value,
	/// or, if there is no larger element, the bitwise complement of <see cref="Count"/>.
	/// </returns>
	public int BinarySearch(T value, Comparison<T> comparison)
		=> BinarySearch(value, new ComparisonComparer<T>(comparison));

	/// <summary>Performs a binary search for the specified value and returns the index of the value within the collection.</summary>
	/// <param name="value">The value to locate within the queue.</param>
	/// <param name="projection">A projection function to apply to each element in the queue.</param>
	/// <returns>
	/// The zero-based index of the specified value if it is found, or a negative number which is the bitwise complement
	/// of the index of the next element that is larger than the specified value (or if there is no larger element, the bitwise complement of <see cref="Count"/>).
	/// </returns>
	public int BinarySearch(T value, Func<T, T, int> projection)
		=> BinarySearch(value, new ComparisonComparer<T>((x, y) => projection(x, y)));

	/// <summary>Searches for the specified value in the <see cref="RandomAccessQueue{T}"/> using the specified comparer and returns the zero-based index of the value.</summary>
	/// <param name="value">The value to locate in the queue.</param>
	/// <param name="comparer">The comparer used to compare values in the queue. Cannot be null.</param>
	/// <returns>The zero-based index of the specified value in the queue if found; otherwise, a negative number that is the bitwise complement of the index of the next element that is larger than <paramref name="value"/> or, if no such element exists, the bitwise complement of <see cref="Count"/>. </returns>
	public int BinarySearch(T value, IComparer<T> comparer)
	{
		comparer.ThrowIfNull();

		if (Count == 0)
			return ~0;

		return BinarySearch(test => comparer.Compare(value, this[test]));
	}

	private int BinarySearch(Func<int, int> comparer)
	{
		var min = 0;
		var max = Count - 1;

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
