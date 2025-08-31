// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

using Wangkanai.Mvc.Infrastructure;
using Wangkanai.Mvc.Routing;

namespace Wangkanai.Markdown.Infrastructure;

internal sealed class DynamicMarkdownEndpointSelector : IDisposable
{
   private readonly DataSourceDependentCache<ActionSelectionTable<Endpoint>> _cache;

   public DynamicMarkdownEndpointSelector(EndpointDataSource dataSource)
   {
      dataSource.ThrowIfNull();

      DataSource = dataSource;
      _cache     = new(dataSource, Initialize);
   }

   public EndpointDataSource DataSource
   {
      get;
   }

   private ActionSelectionTable<Endpoint> Table
      => _cache.EnsureInitialized();

   public void Dispose() => _cache.Dispose();

   private static ActionSelectionTable<Endpoint> Initialize(IReadOnlyList<Endpoint> endpoints)
      => ActionSelectionTable<Endpoint>.Create(endpoints);

   public IReadOnlyList<Endpoint> SelectEndpoints(RouteValueDictionary values)
   {
      values.ThrowIfNull();

      var table   = Table;
      var matches = table.Select(values);
      return matches;
   }
}