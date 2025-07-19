// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

/// <summary>
/// The AbstractTypeFactory class provides a way to register and create instances of derived types of a base type.
/// </summary>
/// <typeparam name="TBaseType">The base type for which the derived types will be registered and created.</typeparam>
public static class AbstractTypeFactory<TBaseType>
{
	private static readonly List<GenericTypeInfo<TBaseType>> TypeInfos = new();

	/// <summary>
	/// The AllTypeInfos property provides access to all registered generic type information for a specified base type.
	/// </summary>
	/// <typeparam name="TBaseType">The base type for which the generic type information is registered.</typeparam>
	/// <returns>An IEnumerable of GenericTypeInfo objects representing the registered generic type information.</returns>
	public static IEnumerable<GenericTypeInfo<TBaseType>> AllTypeInfos
		=> TypeInfos;

	/// <summary>
	/// The HasOverrides property indicates whether there are any overridden types registered for the base type.
	/// </summary>
	/// <typeparam name="TBaseType">The base type for which the overridden types are registered.</typeparam>
	/// <returns>True if there are overridden types registered, otherwise false.</returns>
	public static bool HasOverrides
		=> TypeInfos.Count > 0;

	/// <summary>
	/// Register a derived type of the base type to be created and used by the AbstractTypeFactory.
	/// </summary>
	/// <typeparam name="T">The derived type to be registered.</typeparam>
	/// <returns>A GenericTypeInfo instance representing the registered type, allowing further configuration through fluent syntax.</returns>
	public static GenericTypeInfo<TBaseType> RegisterType<T>()
		where T : TBaseType
		=> RegisterType(typeof(T));

	/// <summary>
	/// Register a derived type of the base type to be created and used by the AbstractTypeFactory.
	/// </summary>
	/// <param name="type">The derived type to be registered.</param>
	/// <returns>A GenericTypeInfo instance representing the registered type, allowing further configuration through fluent syntax.</returns>
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
		var oldType = typeof(TOldType);
		var newType = typeof(TNewType);
		var existTypeInfo = TypeInfos.Find(x => x.Type == oldType);
		var newTypeInfo = new GenericTypeInfo<TBaseType>(newType);
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


	/// <summary>
	/// Create BaseType instance considering type mapping information.
	/// </summary>
	/// <returns>An instance of TBaseType created using the type mapping information.</returns>
	public static TBaseType TryCreateInstance(string typeName)
	{
		TBaseType result;
		GenericTypeInfo<TBaseType> typeInfo = FindTypeInfoByName(typeName);
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


	/// <summary>
	/// Finds the GenericTypeInfo object for a given type name.
	/// </summary>
	/// <param name="typeName">The name of the type to search for.</param>
	/// <returns>The GenericTypeInfo object representing the type, if found; otherwise, null.</returns>
	public static GenericTypeInfo<TBaseType> FindTypeInfoByName(string typeName)
	{
		// Try find first direct type match from registered types
		var result = TypeInfos.Find(x => x.TypeName.EqualsInvariant(typeName));
		// Then need to find in the inheritance chain from registered types
		if (result != null)
			result = TypeInfos.First(x => x.IsAssignableTo(typeName));

		return result!;
	}
}
