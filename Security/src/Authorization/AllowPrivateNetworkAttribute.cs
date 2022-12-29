// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Microsoft.AspNetCore.Authorization;

/// <summary>
///     Specifies that the class or method that this attribute is applied to does not require authorization for private secured network.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AllowPrivateNetworkAttribute : Attribute, IAllowPrivateNetwork { }