// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc.Filters;

namespace Microsoft.AspNetCore.Mvc.Authorization;

/// <summary>
///     A filter that allows anonymous requests, disabling some <see cref="IAllowPrivateNetworkFilter" />s.
/// </summary>
public interface IAllowPrivateNetworkFilter : IFilterMetadata { }
