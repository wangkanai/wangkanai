// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.Blazor.Components;

internal sealed class DefaultComponentActivator : IComponentActivator
{
    public static IComponentActivator Instance { get; } = new DefaultComponentActivator();
    
    public IComponent CreateInstance([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] Type componentType)
    {
        if (!typeof(IComponent).IsAssignableFrom(componentType))
            throw new ArgumentException($"The type {componentType.FullName} does not implement {nameof(IComponent)}.", nameof(componentType));

        return (IComponent)Activator.CreateInstance(componentType)!;
    }
}