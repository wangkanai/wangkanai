// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using SendGrid;
using SendGrid.Helpers.Mail;

namespace Wangkanai.SendGrid.Services;

public interface ISendGridService
{
	SendGridClient Client { get; }
	EmailAddress   From   { get; }
	Task<Response> SendEmailAsync(EmailAddress              to,  string subject, string plain, bool   importance            = false);
	Task<Response> SendEmailAsync(IEnumerable<EmailAddress> tos, string subject, string plain, bool   importance            = false);
	Task<Response> SendEmailAsync(EmailAddress              to,  string subject, string plain, string html, bool importance = false);
	Task<Response> SendEmailAsync(IEnumerable<EmailAddress> tos, string subject, string plain, string html, bool importance = false);
}