// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.System.Extensions;

namespace Wangkanai.System.Domain;

public class TypeInfo<BaseType>
{
    public string              TypeName    { get; private set; }
    public Type                Type        { get; private set; }
    public Type                MappedType  { get; set; }
    public ICollection<object> Services    { get; set; }
    public Func<BaseType>      Factory     { get; private set; }
    public Action<BaseType>    SetupAction { get; private set; }

    public TypeInfo(Type type)
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

    public TypeInfo<BaseType> WithService<T>(T service)
    {
        if (!Services.Contains(service))
            Services.Add(service);

        return this;
    }

    public TypeInfo<BaseType> MapToType<T>()
    {
        MappedType = typeof(T);
        return this;
    }

    public TypeInfo<BaseType> WithFactory(Func<BaseType> factory)
    {
        Factory = factory;
        return this;
    }

    public TypeInfo<BaseType> WithSetupAction(Action<BaseType> setupAction)
    {
        SetupAction = setupAction;
        return this;
    }

    public TypeInfo<BaseType> WithTypeName(string name)
    {
        TypeName = name;
        return this;
    }
}