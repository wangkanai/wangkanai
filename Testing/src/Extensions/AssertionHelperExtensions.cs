// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using Microsoft.Extensions.DependencyInjection;

namespace Wangkanai.Testing;

public static class AssertionHelperExtensions
{
   public static void AssertServices(this List<ServiceDescriptor> descriptors, IServiceCollection services)
   {
      for (var i = 0; i < descriptors.Count; i++)
      {
         Assert.Equal(descriptors[i].ServiceType,            services[i].ServiceType);
         Assert.Equal(descriptors[i].ImplementationInstance, services[i].ImplementationInstance);
         Assert.Equal(descriptors[i].Lifetime,               services[i].Lifetime);
      }
   }
}