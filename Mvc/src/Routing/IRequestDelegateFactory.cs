// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Mvc.Routing;

internal interface IRequestDelegateFactory
{
	RequestDelegate? CreateRequestDelegate(ActionDescriptor actionDescriptor, RouteValueDictionary? dataTokens);
}
