// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Models;

/// <summary>
///     Event tracking allows you to measure how users interact with the content of your website. For example, you
///     might want to measure how many times a button was pressed, or how many times a particular item was used in a web
///     game.
/// </summary>
public class Event
{
	public Event() { }

	public Event(string category)
		: this()
	{
		Category = category;
	}

	public Event(string category, string action)
		: this(category)
	{
		Action = action;
	}

	public Event(string category, string action, string label)
		: this(category, action)
	{
		Label = label;
	}

	/// <param name="category">Typically the object that was interacted with (e.g. button)</param>
	/// <param name="action">The type of interaction (e.g. click)</param>
	/// <param name="label">Useful for categorizing events (e.g. nav buttons)</param>
	/// <param name="value">Values must be non-negative. Useful to pass counts (e.g. 4 times)</param>
	public Event(string category, string action, string label, string value)
		: this(category, action, label)
	{
		Value = value;
	}

	public string Category { get; set; }
	public string Action   { get; set; }
	public string Label    { get; set; }
	public string Value    { get; set; }

	public override string ToString()
	{
		var js = "'event'";
		js += "," + FormatJsValue(Category) ?? "";
		js += "," + FormatJsValue(Action) ?? "";
		js += "," + FormatJsValue(Label) ?? "";
		js += "," + FormatJsValue(Value) ?? "";
		return js;
	}

	private string FormatJsValue(string value)
	{
		return Exist(value) ? "'" + value + "'" : "";
	}

	private bool Exist(object value)
	{
		if (value == null) return false;
		if (value is string)
		{
			if ((string)value == "")
				return false;
		}

		return true;
	}
}