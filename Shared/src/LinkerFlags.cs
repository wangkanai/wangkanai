// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Diagnostics.CodeAnalysis;

namespace Wangkanai.Internal;

internal static class LinkerFlags
{
	/// <summary>
	///     Flags for a member that is JSON (de)serialized.
	/// </summary>
	public const DynamicallyAccessedMemberTypes JsonSerialized = DynamicallyAccessedMemberTypes.PublicConstructors |
																 DynamicallyAccessedMemberTypes.PublicFields |
																 DynamicallyAccessedMemberTypes.PublicProperties;

	/// <summary>
	///     Flags for a component
	/// </summary>
	public const DynamicallyAccessedMemberTypes Component = DynamicallyAccessedMemberTypes.All;

	/// <summary>
	///     Flags for a JSInvokable type.
	/// </summary>
	public const DynamicallyAccessedMemberTypes JSInvokable = DynamicallyAccessedMemberTypes.PublicMethods;
}
