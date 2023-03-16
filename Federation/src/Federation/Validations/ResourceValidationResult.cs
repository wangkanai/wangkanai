// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Validations;

public class ResourceValidationResult
{
	public FederationResources Resources { get; }
	public ResourceValidationResult() { }

	public ResourceValidationResult(FederationResources resources)
	{
		Resources = resources;
	}
}