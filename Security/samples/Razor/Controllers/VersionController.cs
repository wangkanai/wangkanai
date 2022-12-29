// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Wangkanai.Security.RazorApp.Controllers;

[AllowPrivateNetwork]
[ApiController]
[Route("api/[controller]")]
public class VersionController : ControllerBase
{
    [HttpGet]
    public async Task<string> Get()
    {
        return await Task.FromResult(typeof(Program).GetVersion());
    }
}