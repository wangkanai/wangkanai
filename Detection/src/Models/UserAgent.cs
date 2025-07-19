// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;

namespace Wangkanai.Detection.Models;

public class UserAgent
{
	private readonly int _length;
	private readonly string _lower;
	private readonly string _original;

	public UserAgent()
	{
		_original = string.Empty;
		_lower = string.Empty;
		_length = 0;
	}

	public UserAgent(string useragent) : this()
	{
		_original = useragent ?? string.Empty;
		_lower = _original.ToLower();
		_length = _original.Length;
	}

	[DebuggerStepThrough]
	public override string ToString()
	{
		return _original;
	}

	[DebuggerStepThrough]
	public string ToLower()
	{
		return _lower;
	}

	[DebuggerStepThrough]
	public int Length()
	{
		return _length;
	}
}
