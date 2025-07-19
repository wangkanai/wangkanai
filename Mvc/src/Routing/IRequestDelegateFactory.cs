// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;

namespace Wangkanai.Mvc.Routing;

internal interface IRequestDelegateFactory
{
	RequestDelegate? CreateRequestDelegate(ActionDescriptor actionDescriptor, RouteValueDictionary? dataTokens);
}
