// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Wangkanai.Markdown.Infrastructure;

public abstract class MarkdownLoader
{
	public virtual Task<CompiledMarkdownActionDescriptor> LoadAsync(MarkdownActionDescriptor actionDescriptor, EndpointMetadataCollection endpointMetadata)
		=> throw new NotSupportedException();
}