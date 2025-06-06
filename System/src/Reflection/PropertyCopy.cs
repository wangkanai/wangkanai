// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq.Expressions;

using ArgumentException = System.ArgumentException;

namespace Wangkanai.Reflection;

/// <summary>A utility class for copying properties from one object to another object of a specified target type.</summary>
/// <typeparam name="TTarget">The target type to which properties are copied. This must be a class with a parameterless constructor.</typeparam>
public static class PropertyCopy<TTarget> where TTarget : class, new()
{
	/// <summary>Copies properties from a source object to a new instance of the target type.</summary>
	/// <typeparam name="TSource">The type of the source object from which properties are copied. This must be a class.</typeparam>
	/// <param name="source">The source object from which properties are copied. Cannot be null.</param>
	/// <returns>A new instance of the target type with properties copied from the source object.</returns>
	/// <exception cref="ArgumentNullException">Thrown if the source object is null.</exception>
	/// <exception cref="ArgumentException">Thrown if a property exists in the source object that cannot be copied to the target type due to mismatched names, types, or accessibility.</exception>
	public static TTarget CopyFrom<TSource>(TSource source)
		where TSource : class
		=> PropertyCopier<TSource>.Copy(source);

	private static class PropertyCopier<TSource> where TSource : class
	{
		private static Func<TSource, TTarget> _copier;
		private static Exception              _intialization;

		static PropertyCopier()
		{
			try
			{
				_copier        = BuildCopier();
				_intialization = null!;
			}
			catch (Exception ex)
			{
				_copier        = null!;
				_intialization = ex;
			}
		}

		internal static TTarget Copy(TSource source)
		{
			if (_intialization is not null)
				throw _intialization;

			source.ThrowIfNull();

			return _copier(source);
		}

		private static Func<TSource, TTarget> BuildCopier()
		{
			var sourceParameter = Expression.Parameter(typeof(TSource), "source");
			var bindings        = new List<MemberBinding>();

			foreach (var sourceProperty in typeof(TSource).GetProperties())
			{
				if (!sourceProperty.CanRead)
					continue;

				var targetProperty = typeof(TTarget).GetProperty(sourceProperty.Name);

				if (targetProperty is null)
					throw new ArgumentNullException($"Property {sourceProperty.Name} is not found in {typeof(TTarget).FullName}");
				if (!targetProperty.CanWrite)
					throw new ArgumentException($"Property {sourceProperty.Name} is not writable in {typeof(TTarget).FullName}");
				if (!targetProperty.PropertyType.IsAssignableFrom(sourceProperty.PropertyType))
					throw new ArgumentException($"Property {sourceProperty.Name} is not assignable type in {targetProperty.PropertyType.FullName}");

				bindings.Add(Expression.Bind(targetProperty, Expression.Property(sourceParameter, sourceProperty)));
			}

			var initializer = Expression.MemberInit(Expression.New(typeof(TTarget)), bindings);

			return Expression.Lambda<Func<TSource, TTarget>>(initializer, sourceParameter).Compile();
		}
	}
}
