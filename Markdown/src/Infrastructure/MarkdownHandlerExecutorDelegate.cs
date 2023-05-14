// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Markdown.Infrastructure;

internal delegate Task<IActionResult?> MarkdownHandlerExecutorDelegate(object handler, object?[]? arguments);