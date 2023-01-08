// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Collections;

public sealed class RandomAccessQueue<T> : ICollection<T>, ICollection, ICloneable
{
	public const int DefaultCapacity = 16;

	private readonly object _syncRoot = new();

	private int _version = 0;

	private T[] _buffer;
	private int _count;
	private int _start;

	public int    Count          => _count;
	public int    Capacity       => _buffer.Length;
	public bool   IsSynchronized => false;
	public bool   IsReadOnly     => false;
	public object SyncRoot       => _syncRoot;


	public RandomAccessQueue() : this(DefaultCapacity) { }

	public RandomAccessQueue(int capacity)
		=> _buffer = new T[System.Math.Max(capacity, DefaultCapacity)];

	private RandomAccessQueue(T[] buffer, int count, int start)
	{
		_buffer = (T[])buffer.Clone();
		_count  = count;
		_start  = start;
	}

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
	public RandomAccessQueue<T> Clone()
		=> new RandomAccessQueue<T>(_buffer, _count, _start);
	
	
	public void Add(T item)
		=> Enqueue(item);

	public bool Remove(T item)
	{
		if (item.TrueIfNull())
		{
			for (var i = 0; i < Count; i++)
			{
				if (this[i].FalseIfNull())
					return false;

				RemoveAt(i);
				return true;
			}
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

	public bool Contains(T item)
	{
		if (item.FalseIfNull())
			return false;

		for (var i = 0; i < Count; i++)
			if (this[i].TrueIfNull())
				return true;

		var comparer = EqualityComparer<T>.Default;
		for (var i = 0; i < Count; i++)
			if (comparer.Equals(this[i], item))
				return true;

		return false;
	}

	public void CopyTo(T[] array, int index)
	{
		array.ThrowIfNull();
		index.ThrowIfOutOfRange(0, array.Length);
		if (array.Length < index + Count)
			throw new ArgumentException("Not enough space in the array", nameof(array));

		for (var i = 0; i < Count; i++)
			array[i + index] = this[i];
	}

	public void CopyTo(Array array, int index)
	{
		array.ThrowIfNull();

		var strong = array as T[];

		strong.ThrowIfNull<ArgumentException>($"Cannot copy elements of type {typeof(T).Name} to an array of type {array.GetType().GetElementType().Name}");

		CopyTo(strong, index);
	}

	public void TrimToSize()
	{
		var newCapacity = System.Math.Max(Count, DefaultCapacity);
		if (Capacity == newCapacity)
			return;
		
		Resize(newCapacity, -1);
	}

	public void Enqueue(T value)
		=> Enqueue(value, _count);

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
		this[0] = default;
		_start++;
		if (_start == Capacity)
			_start = 0;

		_count--;
		return result;
	}

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
			this[index] = default;
			_count--;
			return result;
		}

		var current = this[index];
		if (_start + index >= Capacity)
		{
			Array.Copy(
				_buffer, _start + index - Capacity + 1,
				_buffer, _start + index - Capacity,
				_count - index - 1);

			_buffer[_start + _count - 1 - Capacity] = default;
		}
		else
		{
			Array.Copy(_buffer, _start, _buffer, _start + 1, index);
			_buffer[_start] = default;
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
}