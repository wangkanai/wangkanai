// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using SendGrid.Helpers.Mail;

namespace Wangkanai.SendGrid.Options;

public class SendGridOptions
{
    public string       ApiKey { get; set; }
    public EmailAddress From   { get; set; }
}