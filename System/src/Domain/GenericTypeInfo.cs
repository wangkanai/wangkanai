// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

public class GenericTypeInfo<BaseType>
{
	public string              TypeName    { get; private set; }
	public Type                Type        { get; private set; }
	public Type                MappedType  { get; set; }
	public ICollection<object> Services    { get; set; }
	public Func<BaseType>      Factory     { get; private set; }
	public Action<BaseType>    SetupAction { get; private set; }

	public GenericTypeInfo(Type type)
	{
		Services = new List<object>();
		Type     = type;
		TypeName = type.Name;
	}

	public T GetService<T>()
		=> Services.OfType<T>().FirstOrDefault();

	public bool IsAssignableTo(string typeName)
		=> Type.GetTypeInheritanceChainTo(typeof(BaseType))
		       .Concat(new[] { typeof(BaseType) })
		       .Any(t => typeName.EqualsInvariant(t.Name));

	public IEnumerable<Type> AllSubclasses
		=> Type.GetTypeInheritanceChainTo(typeof(BaseType))
		       .ToArray();

	public GenericTypeInfo<BaseType> WithService<T>(T service)
	{
		if (!Services.Contains(service))
			Services.Add(service);

		return this;
	}

	public GenericTypeInfo<BaseType> MapToType<T>()
	{
		MappedType = typeof(T);
		return this;
	}

	public GenericTypeInfo<BaseType> WithFactory(Func<BaseType> factory)
	{
		Factory = factory;
		return this;
	}

	public GenericTypeInfo<BaseType> WithSetupAction(Action<BaseType> setupAction)
	{
		SetupAction = setupAction;
		return this;
	}

	public GenericTypeInfo<BaseType> WithTypeName(string name)
	{
		TypeName = name;
		return this;
	}
}