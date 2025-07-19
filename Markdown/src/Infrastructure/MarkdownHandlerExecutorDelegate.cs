// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Markdown.Infrastructure;

internal delegate Task<IActionResult?> MarkdownHandlerExecutorDelegate(object handler, object?[]? arguments);
