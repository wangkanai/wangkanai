// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Extensions;

namespace Wangkanai.Domain;

public static class AbstractTypeFactory<BaseType>
{
    private static readonly List<TypeInfo<BaseType>> _typeInfos = new();

    public static IEnumerable<TypeInfo<BaseType>> AllTypeInfos
        => _typeInfos;

    public static bool HasOverrides
        => _typeInfos.Count > 0;

    public static TypeInfo<BaseType> RegisterType<T>() where T : BaseType
        => RegisterType(typeof(T));

    public static TypeInfo<BaseType> RegisterType(Type type)
    {
        Check.NotNull(type);

        var result = _typeInfos.FirstOrDefault(x => x.AllSubclasses.Contains(type));
        if (result != null) 
            return result;
        
        result = new TypeInfo<BaseType>(type);
        _typeInfos.Add(result);

        return result;
    }
}

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