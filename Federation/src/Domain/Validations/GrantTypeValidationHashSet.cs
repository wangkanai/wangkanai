// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Validations;

internal class GrantTypeValidationHashSet : ICollection<string>
{
	private readonly ICollection<string> _inner;

	public int Count => _inner.Count;
	public bool IsReadOnly => _inner.IsReadOnly;

	public GrantTypeValidationHashSet()
		=> _inner = new HashSet<string>();

	public GrantTypeValidationHashSet(IEnumerable<string> values)
		=> _inner = new HashSet<string>(values);

	public void Add(string item)
	{
		item.ThrowIfNull();
		item.ThrowIfEmpty();
		CloneWith(item).ValidateGrantTypes();
		_inner.Add(item);
	}

	public bool Remove(string item) => _inner.Remove(item);
	public void Clear() => _inner.Clear();
	public bool Contains(string item) => _inner.Contains(item);
	public void CopyTo(string[] array, int arrayIndex) => _inner.CopyTo(array, arrayIndex);

	public IEnumerator<string> GetEnumerator() => _inner.GetEnumerator();
	IEnumerator IEnumerable.GetEnumerator() => _inner.GetEnumerator();

	private ICollection<string> Clone() => new HashSet<string>(this);

	private ICollection<string> CloneWith(params string[] values)
	{
		var clone = Clone();
		foreach (var value in values) clone.Add(value);
		return clone;
	}
}
