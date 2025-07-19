// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Routing.Controllers;

internal delegate Task ControllerBinderDelegate(
	ControllerContext controllerContext,
	object controller,
	Dictionary<string, object?> arguments);
