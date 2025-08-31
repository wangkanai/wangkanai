// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Globalization;

using Microsoft.AspNetCore.Mvc;

namespace Wangkanai.Mvc.Routing;

internal static class NormalizedRouteValue
{
   public static string? GetNormalizedRouteValue(ActionContext context, string key)
   {
      context.ThrowIfNull();
      key.ThrowIfNull();

      if (!context.RouteData.Values.TryGetValue(key, out var routeValue))
      {
         return null;
      }

      var     actionDescriptor = context.ActionDescriptor;
      string? normalizedValue  = null;

      if (actionDescriptor.RouteValues.TryGetValue(key, out var value) && !string.IsNullOrEmpty(value))
      {
         normalizedValue = value;
      }

      var stringRouteValue = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
      if (string.Equals(normalizedValue, stringRouteValue, StringComparison.OrdinalIgnoreCase))
      {
         return normalizedValue;
      }

      return stringRouteValue;
   }
}