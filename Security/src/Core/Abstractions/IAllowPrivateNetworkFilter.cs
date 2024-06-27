// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Mvc.Authorization;

/// <summary>
///     A filter that allows anonymous requests, disabling some <see cref="IAllowPrivateNetworkFilter" />s.
/// </summary>
public interface IAllowPrivateNetworkFilter : IFilterMetadata { }