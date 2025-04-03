// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.RegularExpressions;

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class PlatformService : IPlatformService
{
	private readonly IUserAgentService _userAgentService;

	private Platform?  _name;
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

		if (agent.ContainsLower(Platform.Android))
			return Platform.Android;
		if (agent.ContainsLower(Platform.Windows))
			return Platform.Windows;
		if (IsiPadOS(agent))
			return Platform.iPadOS;
		if (IsiOS(agent))
			return Platform.iOS;
		if (agent.ContainsLower(Platform.Mac))
			return Platform.Mac;
		if (agent.ContainsLower(Platform.Linux))
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
		=> agent.SearchContains(X86DeviceTrie);

	private static bool IsX64(string agent)
		=> agent.SearchContains(X64DeviceTrie);

	private static bool IsiOS(string agent)
		=> agent.SearchContains(IosDeviceTrie) && agent.SearchContains(AppleWebKitTrie);

	private static bool IsiPadOS(string agent)
		=> agent.SearchContains(PadosDeviceTrie) && agent.SearchContains(AppleWebKitTrie);

	private static bool IsChromeOS(string agent)
		=> agent.SearchContains(ChromeOsTrie);

	private static bool IsArm(string agent, Platform os)
		=> agent.ContainsLower(Processor.ARM) ||
		   agent.ContainsLower(Platform.Android) ||
		   os is Platform.iOS or Platform.iPadOS;

	private static bool IsPowerPC(string agent, Platform os)
		=> os == Platform.Mac &&
		   !agent.Contains("ppc", StringComparison.Ordinal);

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
		                .FirstOrDefault()
		                ?
		                .Value
		                .RemoveAll(" ", "(", ")")
		                .Split(';')
		                .FirstOrDefault(x => x.StartsWith(prefix, StringComparison.Ordinal));


	private static readonly string[]    X86DeviceList    = { "i86", "i686", Processor.x86.ToString() };
	private static readonly IPrefixTrie X86DeviceTrie    = X86DeviceList.BuildSearchTrie();
	private static readonly string[]    X64DeviceList    = { "x86_64", "wow64", "win64", Processor.x64.ToLowerString() };
	private static readonly IPrefixTrie X64DeviceTrie    = X64DeviceList.BuildSearchTrie();
	private static readonly string[]    IosDeviceList    = { "iphone", "ipod", Platform.iOS.ToLowerString() };
	private static readonly IPrefixTrie IosDeviceTrie    = IosDeviceList.BuildSearchTrie();
	private static readonly string[]    IPadosDeviceList = { "ipad", Platform.iPadOS.ToLowerString() };

	private static readonly IPrefixTrie PadosDeviceTrie = IPadosDeviceList.BuildSearchTrie();
	private static readonly string[]    ChromeOSList    = { "cros" };
	private static readonly IPrefixTrie ChromeOsTrie    = ChromeOSList.BuildSearchTrie();
	private static readonly string[]    AppleWebKitList = { "applewebkit", "webkit" };
	private static readonly IPrefixTrie AppleWebKitTrie = AppleWebKitList.BuildSearchTrie();

	#endregion
}
