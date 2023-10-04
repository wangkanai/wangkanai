// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

public class GenericTypeInfo<TBaseType>
{
	public ICollection<object> Services { get; set; } = new List<object>();

	public Type              MappedType  { get; private set; } = typeof(Type);
	public string            TypeName    { get; private set; }
	public Type              Type        { get; private set; }
	public Func<TBaseType>   Factory     { get; private set; }
	public Action<TBaseType> SetupAction { get; private set; }

	public GenericTypeInfo(Type type)
	{
		Type     = type;
		TypeName = type.Name;
	}

	public GenericTypeInfo<TBaseType> WithTypeName(string name)
	{
		TypeName = name;
		return this;
	}

	public T GetService<T>()
		=> Services.OfType<T>().FirstOrDefault();

	public bool IsAssignableTo(string typeName)
		=> Type.GetTypeInheritanceChainTo(typeof(TBaseType))
		       .Concat(new[] { typeof(TBaseType) })
		       .Any(t => typeName.EqualsInvariant(t.Name));

	public IEnumerable<Type> GetAllSubclasses() 
		=> (Type[])Type.GetTypeInheritanceChainTo(typeof(TBaseType)).Clone();

	public GenericTypeInfo<TBaseType> WithService<T>(T service)
	{
		if (!Services.Contains(service))
			Services.Add(service);

		return this;
	}

	public GenericTypeInfo<TBaseType> MapToType<T>()
	{
		MappedType = typeof(T);
		return this;
	}

	public GenericTypeInfo<TBaseType> WithFactory(Func<TBaseType> factory)
	{
		Factory = factory;
		return this;
	}

	public GenericTypeInfo<TBaseType> WithSetupAction(Action<TBaseType> setupAction)
	{
		SetupAction = setupAction;
		return this;
	}
}