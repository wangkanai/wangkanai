// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class BrowserService(IUserAgentService userAgentService, IEngineService engineService)
	: IBrowserService
{
	private Browser? _browser;
	private Version? _version;

	public Browser Name    => _browser ??= GetBrowser();
	public Version Version => _version ??= GetVersion();

	private Browser GetBrowser()
	{
		var agent  = userAgentService.UserAgent.ToLower();
		var engine = engineService.Name;

		if (string.IsNullOrEmpty(agent))
			return Browser.Unknown;
		if (IsEdge(agent))
			return Browser.Edge;
		if (IsOpera(agent))
			return Browser.Opera;
		if (IsChrome(agent))
			return Browser.Chrome;
		if (IsInternetExplorer(agent, engine))
			return Browser.InternetExplorer;
		if (IsGoogleSearchApp(agent))
			return Browser.GoogleSearchApp;
		if (agent.ContainsMistake(Browser.Safari))
			return Browser.Safari;
		if (agent.ContainsMistake(Browser.Firefox))
			return Browser.Firefox;

		return Browser.Others;
	}

	private Version GetVersion()
	{
		var agent   = userAgentService.UserAgent.ToLower();
		var browser = Name;

		if (string.IsNullOrEmpty(agent))
			return new Version();

		if (browser == Browser.Edge && !agent.Contains("edge", StringComparison.Ordinal))
			return GetVersionCommon(agent.Replace("edg", "edge", StringComparison.Ordinal), browser);

		if (browser == Browser.GoogleSearchApp)
			return GetVersionGoogleSearchApp(agent);

		if (browser == Browser.Chrome && agent.Contains("crios", StringComparison.Ordinal))
			return GetVersionChrome(agent);

		if (browser == Browser.Safari && agent.Contains("version/", StringComparison.Ordinal))
			return GetVersionSafari(agent);

		if (browser == Browser.Opera)
			return GetVersionOpera(agent, browser);

		if (agent.Contains("rv:11.0", StringComparison.Ordinal) ||
		    agent.Contains("ie 11.0", StringComparison.Ordinal))
			return new Version(11, 0);
		if (agent.Contains("msie 10", StringComparison.Ordinal))
			return new Version(10, 0);
		if (agent.Contains("msie 9", StringComparison.Ordinal))
			return new Version(9, 0);

		return GetVersionCommon(agent, browser);
	}

	private static Version GetVersionOpera(string agent, Browser browser)
	{
		var indexOfOpera = agent.IndexOf("opr/", StringComparison.Ordinal);
		if (indexOfOpera != -1)
			return agent.Substring(indexOfOpera + "opr/".Length).ToVersion();

		var name           = browser.ToLowerString();
		var first          = agent.IndexOf(name, StringComparison.Ordinal);
		var cut            = agent.Length > first + name.Length + 1 ? agent.Substring(first + name.Length + 1) : agent.Substring(first + name.Length);
		var text           = "version/";
		var indexOfVersion = cut.IndexOf(text, StringComparison.Ordinal);
		var version        = indexOfVersion != -1 ? cut.Substring(indexOfVersion + text.Length) : cut;
		return version.ToVersion();
	}

	private static Version GetVersionGoogleSearchApp(string agent)
	{
		var version      = agent.Substring(agent.IndexOf("gsa/", StringComparison.Ordinal) + "gsa/".Length);
		var indexOfSpace = version.IndexOf(" ", StringComparison.Ordinal);

		if (indexOfSpace != -1)
			version = version.Substring(0, indexOfSpace);

		return version.ToVersion();
	}

	private static Version GetVersionSafari(string agent)
	{
		var version      = agent.Substring(agent.IndexOf("version/", StringComparison.Ordinal) + "version/".Length);
		var indexOfSpace = version.IndexOf(" ", StringComparison.Ordinal);

		if (indexOfSpace != -1)
			version = version.Substring(0, indexOfSpace);

		return version.ToVersion();
	}

	private static Version GetVersionChrome(string agent)
	{
		var version      = agent.Substring(agent.IndexOf("crios/", StringComparison.Ordinal) + "crios/".Length);
		var indexOfSpace = version.IndexOf(" ", StringComparison.Ordinal);

		if (indexOfSpace != -1)
			version = version.Substring(0, indexOfSpace);

		return version.ToVersion();
	}

	private static Version GetVersionCommon(string agent, Browser browser)
	{
		var name  = browser.ToLowerString();
		var first = agent.IndexOf(name, StringComparison.Ordinal);

		if (first < 0 || first + name.Length > agent.Length)
			return new Version();

		var cut = agent.Length > first + name.Length + 1 ? agent.Substring(first + name.Length + 1) : agent.Substring(first + name.Length);

		var indexOfSpace = cut.IndexOf(' ', StringComparison.Ordinal);
		var version      = indexOfSpace != -1 ? cut.Substring(0, indexOfSpace) : cut;
		return version.ToVersion();
	}


	private static bool IsEdge(string agent)
		=> agent.ContainsMistake(Browser.Edge) ||
		   agent.Contains("win64", StringComparison.Ordinal) &&
		   agent.Contains("edg", StringComparison.Ordinal);

	private static bool IsChrome(string agent)
		=> agent.ContainsMistake(Browser.Chrome) ||
		   agent.Contains("crios", StringComparison.Ordinal);

	private static bool IsInternetExplorer(string agent, Engine engine)
		=> engine == Engine.Trident ||
		   agent.Contains("msie", StringComparison.Ordinal) &&
		   !agent.ContainsMistake(Browser.Opera);

	private static bool IsOpera(string agent)
		=> agent.ContainsMistake(Browser.Opera) ||
		   agent.Contains("opr", StringComparison.Ordinal);

	private static bool IsGoogleSearchApp(string agent)
		=> agent.Contains("gsa", StringComparison.Ordinal);
}
