// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using SendGrid.Helpers.Mail;

namespace Wangkanai.SendGrid.Extensions;

public static class MessageExtensions
{
	public static void SetImportance(this SendGridMessage email, bool importance = true)
	{
		if (!importance)
			return;

		email.Headers ??= new Dictionary<string, string>();

		email.Headers.Add("Importance", "high");
		email.Headers.Add("X-Priority", "1");
		email.Headers.Add("Priority", "Urgent");
	}
}