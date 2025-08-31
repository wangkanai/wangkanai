// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.AspNetCore.Routing;

using Wangkanai.Webmaster.Routing;

namespace Microsoft.Extensions.DependencyInjection;

public static class RoutingCollectionExtensions
{
   public static IWebmasterBuilder AddRoutingConstraint(this IWebmasterBuilder builder)
   {
      builder.ThrowIfNull();

      builder.Services.Configure<RouteOptions>(options =>
      {
         options.ConstraintMap.Add("thai",    typeof(ThaiLanguageRouteConstraint));
         options.ConstraintMap.Add("lao",     typeof(LaoLanguageRouteConstraint));
         options.ConstraintMap.Add("myanmar", typeof(MyanmarLanguageRouteConstraint));
      });

      return builder;
   }
}