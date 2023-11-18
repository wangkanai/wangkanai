// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq.Expressions;

namespace Wangkanai.Reflection;

public static class PropertyCopy<TTarget> where TTarget : class, new()
{
	public static TTarget CopyFrom<TSource>(TSource source)
		where TSource : class
		=> PropertyCopier<TSource>.Copy(source);

	private static class PropertyCopier<TSource> where TSource : class
	{
		private static Exception              _intialization;
		private static Func<TSource, TTarget> _copier;

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
			if (_intialization != null)
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
					throw new ArgumentException($"Property {sourceProperty.Name} is not found in {typeof(TTarget).FullName}");
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
