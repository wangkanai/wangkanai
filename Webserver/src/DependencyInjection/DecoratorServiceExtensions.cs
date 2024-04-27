// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0


namespace Microsoft.Extensions.DependencyInjection;

public static class DecoratorServiceExtensions
{
	public static void AddTransientDecorator<TService, TImplementation>(this IServiceCollection services)
		where TService : class
		where TImplementation : class, TService
	{
		services.AddDecorator<TService>();
		services.AddTransient<TService, TImplementation>();
	}

	public static void AddDecorator<TService>(this IServiceCollection services)
	{
		var registration = services.LastOrDefault(x => x.ServiceType == typeof(TService));

		registration.ThrowIfNull<InvalidOperationException>("Service type: " + typeof(TService).Name + " not registered.");

		if (services.Any(x => x.ServiceType == typeof(Decorator<TService>)))
			throw new InvalidOperationException("Decorator already registered for type: " + typeof(TService).Name + ".");

		services.Remove(registration);

		if (registration.ImplementationInstance != null)
		{
			var type  = registration.ImplementationInstance.GetType();
			var inner = typeof(Decorator<,>).MakeGenericType(typeof(TService), type);

			services.Add(new ServiceDescriptor(typeof(Decorator<TService>), inner, ServiceLifetime.Transient));
			services.Add(new ServiceDescriptor(type, registration.ImplementationInstance));
		}
		else if (registration.ImplementationFactory != null)
		{
			services.Add(new ServiceDescriptor(typeof(Decorator<TService>), p => new DisposableDecorator<TService>((TService)registration.ImplementationFactory(p)), registration.Lifetime));
		}
		else
		{
			var type  = registration.ImplementationType;
			var inner = typeof(Decorator<,>).MakeGenericType(typeof(TService), registration.ImplementationType);

			services.Add(new ServiceDescriptor(typeof(Decorator<TService>), inner, ServiceLifetime.Transient));
			services.Add(new ServiceDescriptor(type, type, registration.Lifetime));
		}
	}
}
