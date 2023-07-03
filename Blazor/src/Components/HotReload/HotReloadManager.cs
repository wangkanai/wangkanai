// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection.Metadata;

namespace Wangkanai.Blazor.Components.HotReload;

internal sealed class HotReloadManager
{
	public static readonly HotReloadManager Default = new();

	public bool MetadataUpdateSupported { get; set; } = MetadataUpdater.IsSupported;

	/// <summary>
	///     Gets a value that determines if OnDeltaApplied is subscribed to.
	/// </summary>
	public bool IsSubscribedTo => OnDeltaApplied is not null;

	public event Action? OnDeltaApplied;

	/// <summary>
	///     MetadataUpdateHandler event. This is invoked by the hot reload host via reflection.
	/// </summary>
	public static void UpdateApplication(Type[]? _)
	{
		Default.OnDeltaApplied?.Invoke();
	}
}