// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

/// <summary>
/// Represents generic type information.
/// </summary>
/// <typeparam name="TBaseType">The base type of the generic type.</typeparam>
public class GenericTypeInfo<TBaseType>(Type type)
{
	public ICollection<object> Services { get; private set; } = new List<object>();
	public Type MappedType { get; private set; } = typeof(Type);
	public string TypeName { get; private set; } = type.Name;
	public Type Type { get; private set; } = type;
	public Func<TBaseType>? Factory { get; private set; }
	public Action<TBaseType>? SetupAction { get; private set; }

	/// <summary>
	/// Sets the type name for the generic type.
	/// </summary>
	/// <param name="name">The name of the type.</param>
	/// <returns>The modified GenericTypeInfo instance.</returns>
	public GenericTypeInfo<TBaseType> WithTypeName(string name)
	{
		TypeName = name;
		return this;
	}

	/// <summary>
	/// Returns the service of type T from the collection of services.
	/// </summary>
	/// <typeparam name="T">The type of the service to retrieve.</typeparam>
	/// <returns>The service of type T if it exists in the collection, otherwise null.</returns>
	public T? GetService<T>()
		=> Services.OfType<T>().FirstOrDefault();

	/// <summary>
	/// Retrieves all subclasses of the generic type.
	/// </summary>
	/// <typeparam name="TBaseType">The base type of the generic type.</typeparam>
	/// <returns>An IEnumerable of Type objects representing the subclasses of the generic type.</returns>
	public IEnumerable<Type> GetAllSubclasses()
		=> (Type[])Type.GetTypeInheritanceChainTo(typeof(TBaseType)).Clone();

	/// <summary>
	/// Determines whether the current GenericTypeInfo is assignable to a given type name.
	/// </summary>
	/// <param name="typeName">The type name to check.</param>
	/// <returns>True if the GenericTypeInfo is assignable to the given type name; otherwise, false.</returns>
	public bool IsAssignableTo(string typeName)
		=> Type.GetTypeInheritanceChainTo(typeof(TBaseType))
			   .Concat(new[] { typeof(TBaseType) })
			   .Any(t => typeName.EqualsInvariant(t.Name));

	/// <summary>
	/// Adds a service to the GenericTypeInfo instance.
	/// </summary>
	/// <typeparam name="T">The type of the service.</typeparam>
	/// <param name="service">The service instance to be added.</param>
	/// <returns>The modified GenericTypeInfo instance.</returns>
	public GenericTypeInfo<TBaseType> WithService<T>(T service)
	{
		service.ThrowIfNull();
		if (!Services.Contains(service!))
			Services.Add(service!);
		return this;
	}

	/// <summary>
	/// Maps the generic type to a specified type.
	/// </summary>
	/// <typeparam name="T">The type to map the generic type to.</typeparam>
	/// <returns>The modified GenericTypeInfo instance.</returns>
	public GenericTypeInfo<TBaseType> MapToType<T>()
	{
		MappedType = typeof(T);
		return this;
	}

	/// <summary>
	/// Sets a factory method for creating an instance of the generic type.
	/// </summary>
	/// <param name="factory">The factory method.</param>
	/// <returns>The modified GenericTypeInfo instance.</returns>
	public GenericTypeInfo<TBaseType> WithFactory(Func<TBaseType> factory)
	{
		Factory = factory;
		return this;
	}

	/// <summary>
	/// Sets the setup action for the generic type.
	/// </summary>
	/// <param name="setupAction">The setup action to be invoked when setting up the generic type.</param>
	/// <returns>The modified GenericTypeInfo instance.</returns>
	public GenericTypeInfo<TBaseType> WithSetupAction(Action<TBaseType> setupAction)
	{
		SetupAction = setupAction;
		return this;
	}
}
