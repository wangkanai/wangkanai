// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

#nullable enable

using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;

using Wangkanai.System.Collections;

namespace Wangkanai.Mvc.Infrastructure;

internal sealed class ActionSelectionTable<TItem>
{
	public  int                               Version                  { get; }
	private string[]                          RouteKeys                { get; }
	private Dictionary<string[], List<TItem>> OrdinalEntries           { get; }
	private Dictionary<string[], List<TItem>> OrdinalIgnoreCaseEntries { get; }

	private ActionSelectionTable(
		int                               version,
		string[]                          routeKeys,
		Dictionary<string[], List<TItem>> ordinalEntries,
		Dictionary<string[], List<TItem>> ordinalIgnoreCaseEntries)
	{
		Version                  = version;
		RouteKeys                = routeKeys;
		OrdinalEntries           = ordinalEntries;
		OrdinalIgnoreCaseEntries = ordinalIgnoreCaseEntries;
	}

	public static ActionSelectionTable<ActionDescriptor> Create(ActionDescriptorCollection actions)
	{
		return CreateCore<ActionDescriptor>(
			actions.Version,
			actions.Items.Where(a => a.AttributeRouteInfo == null),
			a => a.RouteValues?.Keys,
			(a, key) => {
				string? value = null;
				a.RouteValues?.TryGetValue(key, out value);
				return value ?? string.Empty;
			});
	}

	private static ActionSelectionTable<T> CreateCore<T>(
		int                           version,
		IEnumerable<T>                items,
		Func<T, IEnumerable<string>?> getRouteKeys,
		Func<T, string, string>       getRouteValue)
	{
		var ordinalEntries           = new Dictionary<string[], List<T>>(StringArrayComparer.Ordinal);
		var ordinalIgnoreCaseEntries = new Dictionary<string[], List<T>>(StringArrayComparer.OrdinalIgnoreCase);
		var routeKeys                = new SortedSet<string>(StringComparer.OrdinalIgnoreCase);

		// Working conditional
		foreach (var item in items)
		{
			var keys = getRouteKeys(item);
			if (keys == null) continue;

			foreach (var key in keys)
				routeKeys.Add(key);
		}

		foreach (var item in items)
		{
			var index       = 0;
			var routeValues = new string[routeKeys.Count];
			foreach (var key in routeKeys)
			{
				var value = getRouteValue(item, key);
				routeValues[index++] = value;
			}

			if (!ordinalIgnoreCaseEntries.TryGetValue(routeValues, out var entries))
			{
				entries = new List<T>();
				ordinalIgnoreCaseEntries.Add(routeValues, entries);
			}

			entries.Add(item);

			if (!ordinalEntries.ContainsKey(routeValues))
				ordinalEntries.Add(routeValues, entries);
		}

		return new ActionSelectionTable<T>(version, routeKeys.ToArray(), ordinalEntries, ordinalIgnoreCaseEntries);
	}
}