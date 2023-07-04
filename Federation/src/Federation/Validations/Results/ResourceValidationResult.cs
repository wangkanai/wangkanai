// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Federation.Models;

namespace Wangkanai.Federation.Validations;

public class ResourceValidationResult
{
	public ResourceValidationResult() { }

	public ResourceValidationResult(FederationResources resources)
	{
		Resources    = resources;
		ParsedScopes = resources.ToScopeNames().Select(x => new ParsedScopeValue(x)).ToList();
	}

	public ResourceValidationResult(FederationResources resources, IEnumerable<ParsedScopeValue> parsedScopeValues)
	{
		Resources    = resources;
		ParsedScopes = parsedScopeValues.ToList();
	}

	public bool Succeed
		=> ParsedScopes.Any()   &&
		   !InvalidScopes.Any() &&
		   !InvalidResourceIndicators.Any();

	public IEnumerable<string> RawScopeValues
		=> ParsedScopes.Select(x => x.Value);

	public FederationResources Resources { get; set; } = new();

	public ICollection<ParsedScopeValue> ParsedScopes { get; set; } = new HashSet<ParsedScopeValue>();

	public ICollection<string> InvalidScopes             { get; set; } = new HashSet<string>();
	public ICollection<string> InvalidResources          { get; set; } = new HashSet<string>();
	public ICollection<string> InvalidResourceIndicators { get; set; } = new HashSet<string>();
}