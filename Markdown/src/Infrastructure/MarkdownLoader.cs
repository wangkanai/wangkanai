// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Markdown.Infrastructure;

public abstract class MarkdownLoader
{
   public virtual Task<CompiledMarkdownActionDescriptor> LoadAsync(MarkdownActionDescriptor actionDescriptor, EndpointMetadataCollection endpointMetadata)
      => throw new NotSupportedException();
}