// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Wangkanai.Extensions.Internal;

internal static class TypeNameHelper
{
	/// <summary>
	/// Pretty print a type name.
	/// </summary>
	/// <param name="item">The <see cref="object"/>.</param>
	/// <param name="fullName"><c>true</c> to print a full qualified name.</param>
	/// <returns>The pretty printed type name.</returns>
	[return: NotNullIfNotNull("item")]
	public static string? GetTypeDisplayName(this object? item, bool fullName = false)
		=> item == null ? null : GetTypeDisplayName(item.GetType(), fullName);

	/// <summary>
	/// Pretty print a type name.
	/// </summary>
	/// <param name="type">The <see cref="Type"/>.</param>
	/// <param name="fullName"><c>true</c> to print a fully qualified name.</param>
	/// <param name="includeGenericParameterNames"><c>true</c> to include generic parameter names.</param>
	/// <param name="includeGenericParameters"><c>true</c> to include generic parameters.</param>
	/// <param name="nestedTypeDelimiter">Character to use as a delimiter in nested type names</param>
	/// <returns>The pretty printed type name.</returns>
	public static string GetTypeDisplayName(this Type type, bool fullName = false, bool includeGenericParameterNames = false, bool includeGenericParameters = true, char nestedTypeDelimiter = DefaultNestedTypeDelimiter)
	{
		var builder = new StringBuilder();
		var option  = new DisplayNameOptions(fullName, includeGenericParameterNames, includeGenericParameters, nestedTypeDelimiter);
		builder.ProcessType(type, option);
		return builder.ToString();
	}

	internal static void ProcessType(this StringBuilder builder, Type type, in DisplayNameOptions options)
	{
		if (type.IsGenericType)
		{
			var genericArguments = type.GetGenericArguments();
			builder.ProcessGenericType(type, genericArguments, genericArguments.Length, options);
		}
		else if (type.IsArray)
			builder.ProcessArrayType(type, options);
		else if (_builtInTypeNames.TryGetValue(type, out var builtInName))
			builder.Append(builtInName);
		else if (type.IsGenericParameter)
		{
			if (options.IncludeGenericParameterNames)
				builder.Append(type.Name);
		}
		else
		{
			var name = options.FullName ? type.FullName! : type.Name;
			builder.Append(name);

			if (options.NestedTypeDelimiter != DefaultNestedTypeDelimiter)
				builder.Replace(DefaultNestedTypeDelimiter, options.NestedTypeDelimiter, builder.Length - name.Length, name.Length);
		}
	}

	internal static void ProcessArrayType(this StringBuilder builder, Type type, in DisplayNameOptions options)
	{
		var innerType = type;
		while (innerType.IsArray)
			innerType = innerType.GetElementType()!;

		builder.ProcessType(innerType, options);

		while (type.IsArray)
		{
			builder.Append('[');
			builder.Append(',', type.GetArrayRank() - 1);
			builder.Append(']');
			type = type.GetElementType()!;
		}
	}

	internal static void ProcessGenericType(this StringBuilder builder, Type type, Type[] genericArguments, int length, in DisplayNameOptions options)
	{
		var offset = 0;
		if (type.IsNested)
			offset = type.DeclaringType!.GetGenericArguments().Length;

		if (options.FullName)
		{
			if (type.IsNested)
			{
				builder.ProcessGenericType(type.DeclaringType!, genericArguments, offset, options);
				builder.Append(options.NestedTypeDelimiter);
			}
			else if (!type.Namespace.IsNullOrEmpty())
			{
				builder.Append(type.Namespace);
				builder.Append('.');
			}
		}

		var genericPartIndex = type.Name.IndexOf('`');
		if (genericPartIndex <= 0)
		{
			builder.Append(type.Name);
			return;
		}

		builder.Append(type.Name, 0, genericPartIndex);

		if (!options.IncludeGenericParameters)
			return;

		builder.Append('<');
		for (var i = offset; i < length; i++)
		{
			builder.ProcessType(genericArguments[i], options);
			if (i + 1 == length)
				continue;
			builder.Append(',');
			if (options.IncludeGenericParameterNames || !genericArguments[i + 1].IsGenericParameter)
				builder.Append(' ');
		}

		builder.Append('>');
	}

	private const char DefaultNestedTypeDelimiter = '+';

	private static readonly Dictionary<Type, string> _builtInTypeNames = new()
	{
		{ typeof(void), "void" },
		{ typeof(bool), "bool" },
		{ typeof(byte), "byte" },
		{ typeof(char), "char" },
		{ typeof(decimal), "decimal" },
		{ typeof(double), "double" },
		{ typeof(float), "float" },
		{ typeof(int), "int" },
		{ typeof(long), "long" },
		{ typeof(object), "object" },
		{ typeof(sbyte), "sbyte" },
		{ typeof(short), "short" },
		{ typeof(string), "string" },
		{ typeof(uint), "uint" },
		{ typeof(ulong), "ulong" },
		{ typeof(ushort), "ushort" }
	};

	internal readonly struct DisplayNameOptions(
		bool fullName = false, 
		bool includeGenericParameterNames = false, 
		bool includeGenericParameters = false, 
		char nestedTypeDelimiter = '.')
	{
		public bool FullName                     { get; } = fullName;
		public bool IncludeGenericParameters     { get; } = includeGenericParameters;
		public bool IncludeGenericParameterNames { get; } = includeGenericParameterNames;
		public char NestedTypeDelimiter          { get; } = nestedTypeDelimiter;
	}
}