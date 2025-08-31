// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Markdown.Infrastructure;

internal delegate Task MarkdownHandlerBinderDelegate(MarkdownContext pageContext, IDictionary<string, object?> arguments);