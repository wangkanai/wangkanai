// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.RegularExpressions;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class PlatformService : IPlatformService
{
	private readonly IUserAgentService _userAgentService;
	private          Platform?         _name;

	private Processor? _processor;
	private Version?   _version;

	public PlatformService(IUserAgentService userAgentService)
	{
		_userAgentService = userAgentService;
	}

	public Processor Processor => _processor ??= GetProcessor();
	public Platform  Name      => _name ??= GetPlatform();
	public Version   Version   => _version ??= GetVersion();


	private Platform GetPlatform()
	{
		var agent = _userAgentService.UserAgent.ToLower();

		if (string.IsNullOrEmpty(agent))
			return Platform.Unknown;

		if (agent.Contains(Platform.Android))
			return Platform.Android;
		if (agent.Contains(Platform.Windows))
			return Platform.Windows;
		if (IsiPadOS(agent))
			return Platform.iPadOS;
		if (IsiOS(agent))
			return Platform.iOS;
		if (agent.Contains(Platform.Mac))
			return Platform.Mac;
		if (agent.Contains(Platform.Linux))
			return Platform.Linux;
		if (IsChromeOS(agent))
			return Platform.ChromeOS;

		return Platform.Others;
	}

	private Version GetVersion()
	{
		var agent    = _userAgentService.UserAgent.ToLower();
		var platform = Name;
		return platform switch
		{
			Platform.Unknown => new Version(),
			Platform.Others  => new Version(),
			Platform.Windows => ParseOsVersion(agent, "windowsnt"),
			Platform.Android => ParseOsVersion(agent, "android"),
			Platform.Mac     => ParseOsVersion(agent, "intelmacosx"),
			Platform.iOS     => ParseOsVersion(agent, "cpuiphoneos"),
			Platform.iPadOS  => ParseOsVersion(agent, "cpuos"),
			Platform.Linux   => ParseOsVersion(agent, "rv:"),
			_                => new Version()
		};
	}

	private Processor GetProcessor()
	{
		var agent = _userAgentService.UserAgent.ToLower();
		var os    = Name;

		if (IsArm(agent, os))
			return Processor.ARM;
		if (IsX64(agent))
			return Processor.x64;
		if (IsX86(agent))
			return Processor.x86;
		if (IsPowerPC(agent, os))
			return Processor.x64;

		return Processor.Others;
	}

	private static bool IsX86(string agent)
	{
		return agent.SearchContains(X86DeviceIndex);
	}

	private static bool IsX64(string agent)
	{
		return agent.SearchContains(X64DeviceIndex);
	}

	private static bool IsiOS(string agent)
	{
		return agent.SearchContains(IosDeviceIndex) && agent.SearchContains(AppleWebKitIndex);
	}

	private static bool IsiPadOS(string agent)
	{
		return agent.SearchContains(IPadosDeviceIndex) && agent.SearchContains(AppleWebKitIndex);
	}

	private static bool IsChromeOS(string agent)
	{
		return agent.SearchContains(ChromeOSIndex);
	}

	private static bool IsArm(string agent, Platform os)
	{
		return agent.Contains(Processor.ARM)
		       || agent.Contains(Platform.Android)
		       || os is Platform.iOS or Platform.iPadOS;
	}

	private static bool IsPowerPC(string agent, Platform os)
	{
		return os == Platform.Mac
		       && !agent.Contains("ppc", StringComparison.Ordinal);
	}

	#region Internal

	private static readonly Regex _osStartRegex = new(@"\(([^\)]+)\)", RegexOptions.Compiled, Constants.RegexTimeout);
	private static readonly Regex _osParseRegex = new(@"(?:(\d+)\.)?(?:(\d+)\.)?(?:(\d+)\.\d+)", RegexOptions.Compiled, Constants.RegexTimeout);

	private static Version ParseOsVersion(string agent, string prefix)
	{
		return _osParseRegex.RegexMatch(ReplaceUnderscore(AgentSourceParse(agent, prefix)))
		                    .Value
		                    .ToVersion();
	}

	private static string ReplaceUnderscore(string value)
		=> value.Replace("_", ".");

	private static string AgentSourceParse(string agent, string prefix)
		=> AgentSourceStart(agent, prefix) ?? string.Empty;

	private static string AgentSourceStart(string agent, string prefix)
		=> _osStartRegex.RegexMatch(agent)
		                .Captures
		                .FirstOrDefault()?
		                .Value
		                .RemoveAll(" ", "(", ")")
		                .Split(';')
		                .FirstOrDefault(x => x.StartsWith(prefix, StringComparison.Ordinal));


	private static readonly string[]  X86DeviceList     = { "i86", "i686", Processor.x86.ToStringInvariant() };
	private static readonly IndexTree X86DeviceIndex    = X86DeviceList.BuildIndexTree();
	private static readonly string[]  X64DeviceList     = { "x86_64", "wow64", Processor.x64.ToStringInvariant() };
	private static readonly IndexTree X64DeviceIndex    = X64DeviceList.BuildIndexTree();
	private static readonly string[]  IosDeviceList     = { "iphone", "ipod", Platform.iOS.ToStringInvariant() };
	private static readonly IndexTree IosDeviceIndex    = IosDeviceList.BuildIndexTree();
	private static readonly string[]  IPadosDeviceList  = { "ipad", Platform.iPadOS.ToStringInvariant() };
	private static readonly IndexTree IPadosDeviceIndex = IPadosDeviceList.BuildIndexTree();
	private static readonly string[]  ChromeOSList      = { "cros" };
	private static readonly IndexTree ChromeOSIndex     = ChromeOSList.BuildIndexTree();
	private static readonly string[]  AppleWebKitList   = { "applewebkit", "webkit" };
	private static readonly IndexTree AppleWebKitIndex  = AppleWebKitList.BuildIndexTree();

	#endregion
}
