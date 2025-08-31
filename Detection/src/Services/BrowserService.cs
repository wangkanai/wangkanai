// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

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
      {
         return Browser.Unknown;
      }

      if (IsEdge(agent))
      {
         return Browser.Edge;
      }

      if (IsOpera(agent))
      {
         return Browser.Opera;
      }

      if (IsSamsungInternetBrowser(agent))
      {
         return Browser.Samsung;
      }

      if (IsChrome(agent))
      {
         return Browser.Chrome;
      }

      if (IsInternetExplorer(agent, engine))
      {
         return Browser.InternetExplorer;
      }

      if (IsGoogleSearchApp(agent))
      {
         return Browser.GoogleSearchApp;
      }

      if (IsFirefox(agent))
      {
         return Browser.Firefox;
      }

      if (agent.ContainsLower(Browser.Safari))
      {
         return Browser.Safari;
      }

      return Browser.Others;
   }

   private Version GetVersion()
   {
      var agent   = userAgentService.UserAgent.ToLower();
      var browser = Name;

      if (browser == Browser.Edge && agent.Contains("EdgA", StringComparison.OrdinalIgnoreCase))
      {
         return GetVersionOf(agent, "edgA".ToLowerInvariant());
      }

      if (browser == Browser.Edge && agent.Contains("EdgiOS", StringComparison.OrdinalIgnoreCase))
      {
         return GetVersionOf(agent, "EdgiOS".ToLowerInvariant());
      }

      if (browser == Browser.Edge && !agent.Contains("edge", StringComparison.Ordinal))
      {
         return GetVersionOf(agent, "edg");
      }

      if (browser == Browser.Edge)
      {
         return GetVersionEdge(agent);
      }

      if (browser == Browser.GoogleSearchApp)
      {
         return GetVersionGoogleSearchApp(agent);
      }

      if (browser == Browser.Samsung)
      {
         return GetVersionSamsungInternetBrowser(agent);
      }

      if (browser == Browser.Chrome && agent.Contains("crios", StringComparison.Ordinal))
      {
         return GetVersionChromeOs(agent);
      }

      if (browser == Browser.Chrome)
      {
         return GetVersionChrome(agent);
      }

      if (browser == Browser.Safari && agent.Contains("version", StringComparison.Ordinal))
      {
         return GetVersionSafariVersion(agent);
      }

      if (browser == Browser.Safari)
      {
         return GetVersionSafariSafari(agent);
      }

      if (browser == Browser.Opera)
      {
         return GetVersionOpera(agent, browser);
      }

      if (agent.Contains("rv:11.0", StringComparison.Ordinal) || agent.Contains("ie 11.0", StringComparison.Ordinal))
      {
         return new(11, 0);
      }

      if (agent.Contains("msie 10", StringComparison.Ordinal))
      {
         return new(10, 0);
      }

      if (agent.Contains("msie 9", StringComparison.Ordinal))
      {
         return new(9, 0);
      }

      return GetVersionCommon(agent, browser);
   }

   private static Version GetVersionOpera(string agent, Browser browser)
   {
      var indexOfOpr = agent.IndexOf("opr/", StringComparison.Ordinal);
      if (indexOfOpr != -1)
      {
         return agent.Substring(indexOfOpr + "opr/".Length).ToVersion();
      }

      var indexOfOpera = agent.IndexOf("opera ", StringComparison.Ordinal);
      if (indexOfOpera != -1)
      {
         return agent.Substring(indexOfOpera + "opera ".Length).ToVersion();
      }

      var name           = browser.ToLowerString();
      var first          = agent.IndexOf(name, StringComparison.Ordinal);
      var cut            = agent.Length > first + name.Length + 1 ? agent.Substring(first + name.Length + 1) : agent.Substring(first + name.Length);
      var text           = "version/";
      var indexOfVersion = cut.IndexOf(text, StringComparison.Ordinal);
      var version        = indexOfVersion != -1 ? cut.Substring(indexOfVersion + text.Length) : cut;
      return version.ToVersion();
   }

   private static Version GetVersionOf(string agent, string value)
   {
      var version      = agent.Substring(agent.IndexOf($"{value}/", StringComparison.Ordinal) + $"{value}/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionEdge(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("edge/", StringComparison.Ordinal) + "edge/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionGoogleSearchApp(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("gsa/", StringComparison.Ordinal) + "gsa/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionSamsungInternetBrowser(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("SamsungBrowser/", StringComparison.OrdinalIgnoreCase) + "SamsungBrowser/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionSafariVersion(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("version/", StringComparison.Ordinal) + "version/".Length);
      var indexOfSpace = version.IndexOf(' ');
      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionSafariSafari(string agent)
   {
      var safari    = agent.Substring(agent.IndexOf("safari/", StringComparison.Ordinal) + "safari/".Length);
      var substring = safari.Substring(0);
      if (substring.Contains('.'))
      {
         return substring.ToVersion();
      }

      substring += ".0";

      return substring.ToVersion();
   }

   private static Version GetVersionChromeOs(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("crios/", StringComparison.Ordinal) + "crios/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionChrome(string agent)
   {
      var version      = agent.Substring(agent.IndexOf("chrome/", StringComparison.Ordinal) + "chrome/".Length);
      var indexOfSpace = version.IndexOf(' ');

      if (indexOfSpace != -1)
      {
         version = version.Substring(0, indexOfSpace);
      }

      return version.ToVersion();
   }

   private static Version GetVersionCommon(string agent, Browser browser)
   {
      var name  = browser.ToLowerString();
      var first = agent.IndexOf(name, StringComparison.Ordinal);

      if (first < 0 || first + name.Length > agent.Length)
      {
         return new();
      }

      var cut = agent.Length > first + name.Length + 1 ? agent.Substring(first + name.Length + 1) : agent.Substring(first + name.Length);

      var indexOfSpace = cut.IndexOf(' ');
      var version      = indexOfSpace != -1 ? cut.Substring(0, indexOfSpace) : cut;
      return version.ToVersion();
   }


   private static bool IsEdge(string agent)
      => agent.ContainsLower(Browser.Edge)                            ||
         agent.Contains("edgA",   StringComparison.OrdinalIgnoreCase) ||
         agent.Contains("EdgiOS", StringComparison.OrdinalIgnoreCase) ||
         agent.Contains("win64", StringComparison.Ordinal) &&
         agent.Contains("edg",   StringComparison.Ordinal);

   private static bool IsChrome(string agent)
      => agent.ContainsLower(Browser.Chrome) ||
         agent.Contains("crios", StringComparison.Ordinal);

   private static bool IsInternetExplorer(string agent, Engine engine)
      => engine == Engine.Trident ||
         agent.Contains("msie", StringComparison.Ordinal) &&
         !agent.ContainsLower(Browser.Opera);

   private static bool IsOpera(string agent)
      => agent.ContainsLower(Browser.Opera) ||
         agent.Contains("opr", StringComparison.Ordinal);

   private static bool IsGoogleSearchApp(string agent)
      => agent.Contains("gsa", StringComparison.Ordinal);

   private static bool IsSamsungInternetBrowser(string agent)
      => agent.Contains("SamsungBrowser", StringComparison.OrdinalIgnoreCase);

   private static bool IsFirefox(string agent)
      => agent.ContainsLower(Browser.Firefox) ||
         agent.Contains("FxiOS", StringComparison.OrdinalIgnoreCase);
}