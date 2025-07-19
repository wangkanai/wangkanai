// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Threading.Tasks;

namespace Wangkanai.Markdown.Infrastructure;

internal delegate Task MarkdownHandlerBinderDelegate(MarkdownContext pageContext, IDictionary<string, object?> arguments);
