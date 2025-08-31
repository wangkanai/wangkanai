// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Wangkanai.Detection.Extensions;
using Wangkanai.Detection.Models;
using Wangkanai.Extensions;

namespace Wangkanai.Detection.Services;

public sealed class CrawlerService : ICrawlerService
{
   private static readonly Dictionary<string, Crawler> Crawlers
      = EnumValues<Crawler>.GetValues().Select(x => (x.ToLowerString(), x)).ToDictionary(x => x.Item1, x => x.Item2);

   private static readonly IPrefixTrie CrawlerTrie
      = Crawlers.Select(x => x.Key).BuildSearchTrie();

   private readonly DetectionOptions  _options;
   private readonly IUserAgentService _useragent;

   private Crawler? _name;
   private Version? _version;

   public CrawlerService(IUserAgentService useragent, DetectionOptions options)
   {
      _useragent = useragent;
      _options   = options;
   }

   public bool    IsCrawler => Name != Crawler.Unknown;
   public Crawler Name      => _name ??= GetCrawler();
   public Version Version   => _version ??= GetVersion();


   private Crawler GetCrawler()
   {
      var agent = _useragent.UserAgent.ToLower();

      if (string.IsNullOrEmpty(agent))
      {
         return Crawler.Unknown;
      }

      foreach (var crawler in Crawlers)
         if (agent.Contains(crawler.Key))
         {
            return crawler.Value;
         }

      return HasOthers(agent, _options.Crawler.Others)
         ? Crawler.Others
         : Crawler.Unknown;
   }

   private Version GetVersion()
   {
      var agent = _useragent.UserAgent.ToLower();
      var bot   = FindBot(agent);
      if (string.IsNullOrEmpty(bot))
      {
         return new();
      }

      var index = bot.IndexOf('/');
      if (index < 0)
      {
         index = bot.IndexOf(';');
      }

      var version = string.Empty;
      if (index > 0 && bot.Length > index + 1)
      {
         version = bot.Substring(index + 1).TrimEnd(';');
      }

      return version.ToVersion();
   }

   private bool HasOthers(string agent, IEnumerable<string> others)
      => agent.Contains("bot", StringComparison.Ordinal) ||
         others.Any(x => agent.Contains(x, StringComparison.Ordinal));

   private string FindBot(string agent)
      => agent.Split(' ')
              .FirstOrDefault(x => x.SearchContains(CrawlerTrie)) ?? string.Empty;
}