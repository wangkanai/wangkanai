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
        type.IfNullThrow();

        var result = _typeInfos.FirstOrDefault(x => x.AllSubclasses.Contains(type));
        if (result != null)
            return result;

        result = new TypeInfo<BaseType>(type);
        _typeInfos.Add(result);

        return result;
    }

    /// <summary>
    /// Override already registered  type to new 
    /// </summary>
    /// <returns>TypeInfo instance to continue configuration through fluent syntax</returns>
    public static TypeInfo<BaseType> OverrideType<OldType, NewType>() where NewType : BaseType
    {
        var oldType       = typeof(OldType);
        var newType       = typeof(NewType);
        var existTypeInfo = _typeInfos.FirstOrDefault(x => x.Type == oldType);
        var newTypeInfo   = new TypeInfo<BaseType>(newType);
        if (existTypeInfo != null)
            _typeInfos.Remove(existTypeInfo);

        _typeInfos.Add(newTypeInfo);
        return newTypeInfo;
    }

    /// <summary>
    /// Create BaseType instance considering type mapping information
    /// </summary>
    public static BaseType TryCreateInstance()
        => TryCreateInstance(typeof(BaseType).Name);

    /// <summary>
    /// Create derived from BaseType  specified type instance considering type mapping information
    /// </summary>
    public static T TryCreateInstance<T>() where T : BaseType
        => (T)TryCreateInstance(typeof(T).Name);

    public static BaseType TryCreateInstance(string typeName, BaseType defaultObj)
    {
        var result   = defaultObj;
        var typeInfo = FindTypeInfoByName(typeName);
        if (typeInfo != null)
            result = TryCreateInstance(typeName);

        return result;
    }

    public static BaseType TryCreateInstance(string typeName)
    {
        BaseType result;
        var      typeInfo = FindTypeInfoByName(typeName);
        if (typeInfo != null)
        {
            if (typeInfo.Factory != null)
                result = typeInfo.Factory();
            else
                result = (BaseType)Activator.CreateInstance(typeInfo.Type);
            typeInfo.SetupAction?.Invoke(result);
        }
        else
        {
            var baseType = typeof(BaseType);
            if (baseType.IsAbstract)
                throw new OperationCanceledException($"A type with {typeName} name is not registered in the AbstractFactory, you cannot create an instance of an abstract class {baseType.Name} because it does not have a complete implementation");

            result = (BaseType)Activator.CreateInstance(typeof(BaseType));
        }

        return result;
    }

    public static TypeInfo<BaseType> FindTypeInfoByName(string typeName)
    {
        // Try find first direct type match from registered types
        var result = _typeInfos.FirstOrDefault(x => x.TypeName.EqualsInvariant(typeName));
        // Then need to find in inheritance chain from registered types
        if (result == null)
            result = _typeInfos.FirstOrDefault(x => x.IsAssignableTo(typeName));

        return result;
    }
}