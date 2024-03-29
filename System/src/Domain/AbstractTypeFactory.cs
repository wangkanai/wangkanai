﻿// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

public static class AbstractTypeFactory<TBaseType>
{
	private static readonly List<GenericTypeInfo<TBaseType>> TypeInfos = new();

	public static IEnumerable<GenericTypeInfo<TBaseType>> AllTypeInfos
		=> TypeInfos;

	public static bool HasOverrides
		=> TypeInfos.Count > 0;

	public static GenericTypeInfo<TBaseType> RegisterType<T>()
		where T : TBaseType
		=> RegisterType(typeof(T));

	public static GenericTypeInfo<TBaseType> RegisterType(Type type)
	{
		type.ThrowIfNull();

		var result = TypeInfos.FirstOrDefault(x => x.GetAllSubclasses().Contains(type));
		if (result != null)
			return result;

		result = new GenericTypeInfo<TBaseType>(type);
		TypeInfos.Add(result);

		return result;
	}

	/// <summary>
	/// Override already registered  type to new
	/// </summary>
	/// <returns>TypeInfo instance to continue configuration through fluent syntax</returns>
	public static GenericTypeInfo<TBaseType> OverrideType<TOldType, TNewType>()
		where TNewType : TBaseType
	{
		var oldType       = typeof(TOldType);
		var newType       = typeof(TNewType);
		var existTypeInfo = TypeInfos.Find(x => x.Type == oldType);
		var newTypeInfo   = new GenericTypeInfo<TBaseType>(newType);
		if (existTypeInfo != null)
			TypeInfos.Remove(existTypeInfo);

		TypeInfos.Add(newTypeInfo);
		return newTypeInfo;
	}

	/// <summary>
	/// Create BaseType instance considering type mapping information
	/// </summary>
	public static TBaseType TryCreateInstance()
		=> TryCreateInstance(typeof(TBaseType).Name);

	/// <summary>
	/// Create derived from BaseType  specified type instance considering type mapping information
	/// </summary>
	public static T TryCreateInstance<T>()
		where T : TBaseType
		=> (T)TryCreateInstance(typeof(T).Name)!;

	public static TBaseType TryCreateInstance(string typeName, TBaseType defaultObj)
	{
		TBaseType result   = defaultObj;
		var       typeInfo = FindTypeInfoByName(typeName);
		if (typeInfo.FalseIfNull())
			result = TryCreateInstance(typeName);

		return result;
	}

	public static TBaseType TryCreateInstance(string typeName)
	{
		TBaseType result;
		var       typeInfo = FindTypeInfoByName(typeName);
		if (typeInfo.FalseIfNull())
		{
			if (typeInfo.Factory != null)
				result = typeInfo.Factory();
			else
				result = (TBaseType)Activator.CreateInstance(typeInfo.Type)!;

			typeInfo.SetupAction?.Invoke(result);
		}
		else
		{
			var baseType = typeof(TBaseType);
			if (baseType.IsAbstract)
				throw new OperationCanceledException($"A type with {typeName} name is not registered in the AbstractFactory, you cannot create an instance of an abstract class {baseType.Name} because it does not have a complete implementation");

			result = (TBaseType)Activator.CreateInstance(typeof(TBaseType))!;
		}

		return result;
	}

	public static GenericTypeInfo<TBaseType> FindTypeInfoByName(string typeName)
	{
		// Try find first direct type match from registered types
		var result = TypeInfos.Find(x => x.TypeName.EqualsInvariant(typeName));
		// Then need to find in inheritance chain from registered types
		if (result.TrueIfNull())
			result = TypeInfos.First(x => x.IsAssignableTo(typeName));

		return result!;
	}
}
