// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

using System.Diagnostics;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Primitives;

namespace Wangkanai.Mvc.Routing;

internal abstract class ActionEndpointDataSourceBase : EndpointDataSource, IDisposable
{
   private readonly IActionDescriptorCollectionProvider _actions;

   protected readonly List<Action<EndpointBuilder>> Conventions;
   protected readonly List<Action<EndpointBuilder>> FinallyConventions;

   protected readonly object                   Lock = new();
   private            CancellationTokenSource? _cancellationTokenSource;
   private            IChangeToken?            _changeToken;
   private            IDisposable?             _disposable;

   private List<Endpoint>? _endpoints;

   public ActionEndpointDataSourceBase(IActionDescriptorCollectionProvider actions)
   {
      _actions = actions;

      Conventions        = new();
      FinallyConventions = new();
   }

   public override IReadOnlyList<Endpoint> Endpoints
   {
      get
      {
         Initialize();
         Debug.Assert(_changeToken != null);
         Debug.Assert(_endpoints   != null);
         return _endpoints;
      }
   }

   public void Dispose()
   {
      _disposable?.Dispose();
      _disposable = null;
   }

   public override IReadOnlyList<Endpoint> GetGroupedEndpoints(RouteGroupContext context)
      => CreateEndpoints(
                         context.Prefix,
                         _actions.ActionDescriptors.Items,
                         Conventions,
                         context.Conventions,
                         FinallyConventions,
                         context.FinallyConventions);

   private void Initialize()
   {
      if (_endpoints == null)
      {
         lock (Lock)
         {
            if (_endpoints == null)
            {
               UpdateEndpoints();
            }
         }
      }
   }

   private void UpdateEndpoints()
   {
      lock (Lock)
      {
         var endpoints = CreateEndpoints(
                                         null,
                                         _actions.ActionDescriptors.Items,
                                         Conventions,
                                         Array.Empty<Action<EndpointBuilder>>(),
                                         FinallyConventions,
                                         Array.Empty<Action<EndpointBuilder>>());
         var oldCancellationTokenSource = _cancellationTokenSource;
         _endpoints               = endpoints;
         _cancellationTokenSource = new();
         _changeToken             = new CancellationChangeToken(_cancellationTokenSource.Token);

         oldCancellationTokenSource?.Cancel();
      }
   }

   protected abstract List<Endpoint> CreateEndpoints(
      RoutePattern?                          groupPrefix,
      IReadOnlyList<ActionDescriptor>        actions,
      IReadOnlyList<Action<EndpointBuilder>> conventions,
      IReadOnlyList<Action<EndpointBuilder>> groupConventions,
      IReadOnlyList<Action<EndpointBuilder>> finallyConventions,
      IReadOnlyList<Action<EndpointBuilder>> groupFinallyConventions);

   protected void Subscribe()
   {
      if (_actions is ActionDescriptorCollectionProvider collectionProviderWithChangeToken)
      {
         _disposable = ChangeToken.OnChange(() => collectionProviderWithChangeToken.GetChangeToken(), UpdateEndpoints);
      }
   }

   public override IChangeToken GetChangeToken()
   {
      Initialize();
      Debug.Assert(_changeToken != null);
      Debug.Assert(_endpoints   != null);
      return _changeToken;
   }
}