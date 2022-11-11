// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.Logging;
using Wangkanai.Blazor.Components.Rendering;

namespace Wangkanai.Blazor.Components.RenderTree;

public abstract partial class Renderer
{
    internal static partial class Log
    {
        [LoggerMessage(1, LogLevel.Debug, "Initializing component {ComponentId} ({ComponentType}) as child of {ParentComponentId} ({ParentComponentType})", EventName = "InitializingChildComponent", SkipEnabledCheck = true)]
        private static partial void InitializingChildComponent(ILogger logger, int componentId, Type componentType, int parentComponentId, Type parentComponentType);

        [LoggerMessage(2, LogLevel.Debug, "Initializing root component {ComponentId} ({ComponentType})", EventName = "InitializingRootComponent", SkipEnabledCheck = true)]
        private static partial void InitializingRootComponent(ILogger logger, int componentId, Type componentType);

        public static void InitializingComponent(ILogger logger, ComponentState componentState, ComponentState parentComponentState)
        {
            if (logger.IsEnabled(LogLevel.Debug)) // This is almost always false, so skip the evaluations
            {
                if (parentComponentState == null)
                    InitializingRootComponent(logger, componentState.ComponentId, componentState.Component.GetType());
                else
                    InitializingChildComponent(logger, componentState.ComponentId, componentState.Component.GetType(), parentComponentState.ComponentId, parentComponentState.Component.GetType());
            }
        }

        [LoggerMessage(3, LogLevel.Debug, "Rendering component {ComponentId} of type {ComponentType}", EventName = "RenderingComponent", SkipEnabledCheck = true)]
        private static partial void RenderingComponent(ILogger logger, int componentId, Type componentType);

        public static void RenderingComponent(ILogger logger, ComponentState componentState)
        {
            if (logger.IsEnabled(LogLevel.Debug)) // This is almost always false, so skip the evaluations
                RenderingComponent(logger, componentState.ComponentId, componentState.Component.GetType());
        }

        [LoggerMessage(4, LogLevel.Debug, "Disposing component {ComponentId} of type {ComponentType}", EventName = "DisposingComponent", SkipEnabledCheck = true)]
        private static partial void DisposingComponent(ILogger<Renderer> logger, int componentId, Type componentType);

        public static void DisposingComponent(ILogger<Renderer> logger, ComponentState componentState)
        {
            if (logger.IsEnabled(LogLevel.Debug)) // This is almost always false, so skip the evaluations
                DisposingComponent(logger, componentState.ComponentId, componentState.Component.GetType());
        }

        [LoggerMessage(5, LogLevel.Debug, "Handling event {EventId} of type '{EventType}'", EventName = "HandlingEvent", SkipEnabledCheck = true)]
        public static partial void HandlingEvent(ILogger<Renderer> logger, ulong eventId, string eventType);

        public static void HandlingEvent(ILogger<Renderer> logger, ulong eventHandlerId, EventArgs? eventArgs)
        {
            if (logger.IsEnabled(LogLevel.Debug)) // This is almost always false, so skip the evaluations
                HandlingEvent(logger, eventHandlerId, eventArgs?.GetType().Name ?? "null");
        }
    }
}
