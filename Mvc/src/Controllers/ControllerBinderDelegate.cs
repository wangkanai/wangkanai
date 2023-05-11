// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Routing.Controllers;

internal delegate Task ControllerBinderDelegate(
	ControllerContext           controllerContext,
	object                      controller,
	Dictionary<string, object?> arguments);