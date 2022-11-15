// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using Microsoft.AspNetCore.Components;

using Wangkanai.Internal;

namespace Wangkanai.Blazor.Components.Reflection;

internal static class ComponentProperties
{
    internal const BindingFlags BindablePropertyFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase;

    private static readonly ConcurrentDictionary<Type, WritersForType> _cachedWritersByType = new();

    public static void ClearCache()
    {
        _cachedWritersByType.Clear();
    }

    public static void SetProperties(in ParameterView parameters, object target)
    {
        if (target == null)
            throw new ArgumentNullException(nameof(target));

        var targetType = target.GetType();
        if (!_cachedWritersByType.TryGetValue(targetType, out var writers))
        {
            writers                          = new WritersForType(targetType);
            _cachedWritersByType[targetType] = writers;
        }

        if (writers.CaptureUnmatchedValuesWriter == null)
        {
            foreach (var parameter in parameters)
            {
                var parameterName = parameter.Name;
                if (!writers.TryGetValue(parameterName, out var writer))
                {
                    ThrowForUnknownIncomingParameterName(targetType, parameterName);
                    throw null; // Unreachable
                }

                if (writer.Cascading && !parameter.Cascading)
                {
                    ThrowForSettingCascadingParameterWithNonCascadingValue(targetType, parameterName);
                    throw null; // Unreachable
                }

                if (!writer.Cascading && parameter.Cascading)
                {
                    ThrowForSettingParameterWithCascadingValue(targetType, parameterName);
                    throw null; // Unreachable
                }

                SetProperty(target, writer, parameterName, parameter.Value);
            }
        }
        else
        {
            // Logic with components with a CaptureUnmatchedValues parameter
            var                         isCaptureUnmatchedValuesParameterSetExplicitly = false;
            Dictionary<string, object>? unmatched                                      = null;
            foreach (var parameter in parameters)
            {
                var parameterName = parameter.Name;
                if (string.Equals(parameterName, writers.CaptureUnmatchedValuesPropertyName, StringComparison.OrdinalIgnoreCase))
                    isCaptureUnmatchedValuesParameterSetExplicitly = true;

                if (writers.TryGetValue(parameterName, out var writer))
                {
                    if (!writer.Cascading && parameter.Cascading)
                    {
                        ThrowForSettingParameterWithCascadingValue(targetType, parameterName);
                        throw null; // Unreachable
                    }

                    if (writer.Cascading && !parameter.Cascading)
                    {
                        unmatched                ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                        unmatched[parameterName] =   parameter.Value;
                    }
                    else
                    {
                        SetProperty(target, writer, parameterName, parameter.Value);
                    }
                }
                else
                {
                    if (parameter.Cascading)
                    {
                        // Don't allow an "extra" cascading value to be collected - or don't allow a non-cascading
                        // parameter to be set with a cascading value.
                        //
                        // This is likely a bug in our infrastructure or an attempt to deliberately do something unsupported.
                        ThrowForSettingParameterWithCascadingValue(targetType, parameterName);
                        throw null; // Unreachable
                    }

                    unmatched                ??= new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                    unmatched[parameterName] =   parameter.Value;
                }
            }

            if (unmatched != null && isCaptureUnmatchedValuesParameterSetExplicitly)
            {
                ThrowForCaptureUnmatchedValuesConflict(targetType, writers.CaptureUnmatchedValuesPropertyName!, unmatched);
                throw null; // Unreachable
            }

            if (unmatched != null)
            {
                SetProperty(target, writers.CaptureUnmatchedValuesWriter, writers.CaptureUnmatchedValuesPropertyName!, unmatched);
            }
        }

        static void SetProperty(object target, PropertySetter writer, string parameterName, object value)
        {
            try
            {
                writer.SetValue(target, value);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"Unable to set property '{parameterName}' on object of " +
                    $"type '{target.GetType().FullName}'. The error was: {ex.Message}", ex);
            }
        }
    }

    internal static IEnumerable<PropertyInfo> GetCandidateBindableProperties([DynamicallyAccessedMembers(LinkerFlags.Component)] Type targetType)
    {
        return MemberAssignment.GetPropertiesIncludingInherited(targetType, BindablePropertyFlags);
    }

    [DoesNotReturn]
    private static void ThrowForUnknownIncomingParameterName([DynamicallyAccessedMembers(LinkerFlags.Component)] Type targetType,
        string                                                                                                        parameterName)
    {
        var propertyInfo = targetType.GetProperty(parameterName, BindablePropertyFlags);
        if (propertyInfo != null)
            if (!propertyInfo.IsDefined(typeof(ParameterAttribute)) && !propertyInfo.IsDefined(typeof(CascadingParameterAttribute)))
                throw new InvalidOperationException(
                    $"Object of type '{targetType.FullName}' has a property matching the name '{parameterName}', " +
                    $"but it does not have [{nameof(ParameterAttribute)}] or [{nameof(CascadingParameterAttribute)}] applied.");
            else
                throw new InvalidOperationException(
                    $"No writer was cached for the property '{propertyInfo.Name}' on type '{targetType.FullName}'.");
        throw new InvalidOperationException(
            $"Object of type '{targetType.FullName}' does not have a property " +
            $"matching the name '{parameterName}'.");
    }

    [DoesNotReturn]
    private static void ThrowForSettingCascadingParameterWithNonCascadingValue(Type targetType, string parameterName)
    {
        throw new InvalidOperationException(
            $"Object of type '{targetType.FullName}' has a property matching the name '{parameterName}', " +
            $"but it does not have [{nameof(ParameterAttribute)}] applied.");
    }

    [DoesNotReturn]
    private static void ThrowForSettingParameterWithCascadingValue(Type targetType, string parameterName)
    {
        throw new InvalidOperationException(
            $"The property '{parameterName}' on component type '{targetType.FullName}' cannot be set " +
            "using a cascading value.");
    }

    [DoesNotReturn]
    private static void ThrowForCaptureUnmatchedValuesConflict(Type targetType, string parameterName, Dictionary<string, object> unmatched)
    {
        throw new InvalidOperationException(
            $"The property '{parameterName}' on component type '{targetType.FullName}' cannot be set explicitly " +
            "when also used to capture unmatched values. Unmatched values:" + Environment.NewLine +
            string.Join(Environment.NewLine, unmatched.Keys));
    }

    [DoesNotReturn]
    private static void ThrowForMultipleCaptureUnmatchedValuesParameters([DynamicallyAccessedMembers(LinkerFlags.Component)] Type targetType)
    {
        var propertyNames = new List<string>();
        foreach (var property in targetType.GetProperties(BindablePropertyFlags))
            if (property.GetCustomAttribute<ParameterAttribute>()?.CaptureUnmatchedValues == true)
                propertyNames.Add(property.Name);

        propertyNames.Sort(StringComparer.Ordinal);

        throw new InvalidOperationException(
            $"Multiple properties were found on component type '{targetType.FullName}' with " +
            $"'{nameof(ParameterAttribute)}.{nameof(ParameterAttribute.CaptureUnmatchedValues)}'. Only a single property " +
            $"per type can use '{nameof(ParameterAttribute)}.{nameof(ParameterAttribute.CaptureUnmatchedValues)}'. Properties:" + Environment.NewLine +
            string.Join(Environment.NewLine, propertyNames));
    }

    [DoesNotReturn]
    private static void ThrowForInvalidCaptureUnmatchedValuesParameterType(Type targetType, PropertyInfo propertyInfo)
    {
        throw new InvalidOperationException(
            $"The property '{propertyInfo.Name}' on component type '{targetType.FullName}' cannot be used " +
            $"with '{nameof(ParameterAttribute)}.{nameof(ParameterAttribute.CaptureUnmatchedValues)}' because it has the wrong type. " +
            "The property must be assignable from 'Dictionary<string, object>'.");
    }

    private sealed class WritersForType
    {
        private const    int                                           MaxCachedWriterLookups = 100;
        private readonly ConcurrentDictionary<string, PropertySetter?> _referenceEqualityWritersCache;
        private readonly Dictionary<string, PropertySetter>            _underlyingWriters;

        public WritersForType([DynamicallyAccessedMembers(LinkerFlags.Component)] Type targetType)
        {
            _underlyingWriters             = new Dictionary<string, PropertySetter>(StringComparer.OrdinalIgnoreCase);
            _referenceEqualityWritersCache = new ConcurrentDictionary<string, PropertySetter?>(ReferenceEqualityComparer.Instance);

            foreach (var propertyInfo in GetCandidateBindableProperties(targetType))
            {
                var parameterAttribute          = propertyInfo.GetCustomAttribute<ParameterAttribute>();
                var cascadingParameterAttribute = propertyInfo.GetCustomAttribute<CascadingParameterAttribute>();
                var isParameter                 = parameterAttribute != null || cascadingParameterAttribute != null;
                if (!isParameter)
                    continue;

                var propertyName = propertyInfo.Name;
                if (parameterAttribute != null && (propertyInfo.SetMethod == null || !propertyInfo.SetMethod.IsPublic))
                    throw new InvalidOperationException(
                        $"The type '{targetType.FullName}' declares a parameter matching the name '{propertyName}' that is not public. Parameters must be public.");

                var propertySetter = new PropertySetter(targetType, propertyInfo)
                {
                    Cascading = cascadingParameterAttribute != null
                };

                if (_underlyingWriters.ContainsKey(propertyName))
                    throw new InvalidOperationException(
                        $"The type '{targetType.FullName}' declares more than one parameter matching the " +
                        $"name '{propertyName.ToLowerInvariant()}'. Parameter names are case-insensitive and must be unique.");

                _underlyingWriters.Add(propertyName, propertySetter);

                if (parameterAttribute != null && parameterAttribute.CaptureUnmatchedValues)
                {
                    if (CaptureUnmatchedValuesWriter != null)
                        ThrowForMultipleCaptureUnmatchedValuesParameters(targetType);

                    if (!propertyInfo.PropertyType.IsAssignableFrom(typeof(Dictionary<string, object>)))
                        ThrowForInvalidCaptureUnmatchedValuesParameterType(targetType, propertyInfo);

                    CaptureUnmatchedValuesWriter       = new PropertySetter(targetType, propertyInfo);
                    CaptureUnmatchedValuesPropertyName = propertyInfo.Name;
                }
            }
        }

        public PropertySetter? CaptureUnmatchedValuesWriter       { get; }
        public string?         CaptureUnmatchedValuesPropertyName { get; }

        public bool TryGetValue(string parameterName, [MaybeNullWhen(false)] out PropertySetter writer)
        {
            if (!_referenceEqualityWritersCache.TryGetValue(parameterName, out writer))
            {
                _underlyingWriters.TryGetValue(parameterName, out writer);

                if (_referenceEqualityWritersCache.Count < MaxCachedWriterLookups)
                    _referenceEqualityWritersCache.TryAdd(parameterName, writer);
            }

            return writer != null;
        }
    }
}