// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Linq;

using SendGrid;
using SendGrid.Helpers.Mail;

using Wangkanai.Extensions;
using Wangkanai.SendGrid.Extensions;
using Wangkanai.SendGrid.Options;

namespace Wangkanai.SendGrid.Services;

   public class SendGridService : ISendGridService
{
    public SendGridClient Client { get; }
    public EmailAddress From { get; }

    public SendGridService(SendGridOptions options)
    {
        options ??= new SendGridOptions();
        if (options.From == null)
            throw new ArgumentNullException(nameof(From));

        Client = new SendGridClient(options.ApiKey);
        From = options.From;
    }

    public async Task<Response> SendEmailAsync(EmailAddress to, string subject, string plain, bool importance = false)
    {
        var email = CreatePlainEmail(subject, plain, importance);
        email.AddTo(to);
        return await Client.SendEmailAsync(email).ConfigureAwait(false);
    }

    public async Task<Response> SendEmailAsync(IEnumerable<EmailAddress> tos, string subject, string plain, bool importance = false)
    {
        var email = CreatePlainEmail(subject, plain, importance);
        email.AddTos(tos.ToList());
        return await Client.SendEmailAsync(email).ConfigureAwait(false);
    }

    public async Task<Response> SendEmailAsync(EmailAddress to, string subject, string plain, string html, bool importance = false)
    {
        var email = CreateHtmlEmail(subject, plain, html, importance);
        email.AddTo(to);
        return await Client.SendEmailAsync(email).ConfigureAwait(false);
    }

    public async Task<Response> SendEmailAsync(IEnumerable<EmailAddress> tos, string subject, string plain, string html, bool importance = false)
    {
        var email = CreateHtmlEmail(subject, plain, html, importance);
        email.AddTos(tos.ToList());
        return await Client.SendEmailAsync(email).ConfigureAwait(false);
    }

    #region internal

    private SendGridMessage CreateHeaderEmail(string subject, bool importance = false)
    {
        var email = new SendGridMessage();
        email.SetFrom(From);
        email.SetSubject(subject);
        email.SetImportance(importance);
        return email;
    }

    private SendGridMessage CreatePlainEmail(string subject, string plain, bool importance = false)
    {
        var email = CreateHeaderEmail(subject, importance);

        if (!plain.IsNullOrWhiteSpace())
            email.AddContent(MimeType.Text, plain);

        return email;
    }

    private SendGridMessage CreateHtmlEmail(string subject, string plain, string html, bool importance = false)
    {
        var email = CreatePlainEmail(subject, plain, importance);

        if (!html.IsNullOrWhiteSpace())
            email.AddContent(MimeType.Html, html);

        return email;
    }

    #endregion
}