// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Validations;

public interface IResourceOwnerPasswordValidator
{
	Task ValidateAsync(ResourceOwnerPasswordValidationContext context);
}
