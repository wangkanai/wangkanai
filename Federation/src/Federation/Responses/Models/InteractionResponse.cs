// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Federation.Responses;

public class InteractionResponse
{
	public bool IsLogin { get; set; }
	public bool IsConsent { get; set; }
	public bool IsCreateAccount { get; set; }
	public string Error { get; set; }
	public string Description { get; set; }
	public string RedirectUrl { get; set; }

	public bool IsError => Error != null;
	public bool IsRedirect => RedirectUrl.IsExist();

	public InteractionResponseType ResponseType
		=> IsError ? InteractionResponseType.Error :
		   IsLogin || IsConsent || IsCreateAccount || IsRedirect ? InteractionResponseType.UserInteraction : InteractionResponseType.None;
}
