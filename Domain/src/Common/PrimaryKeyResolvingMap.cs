// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain.Common;

/// <summary>Represents a mapping mechanism to resolve the primary key of transient entities and update them with the corresponding persistent entities during the persistence process.</summary>
public class PrimaryKeyResolvingMap
{
   private readonly Dictionary<IKeyGuidEntity, IKeyGuidEntity> _resolvingMap = new();

   /// <summary>Adds a mapping pair to the resolving map, associating a transient entity with its corresponding persistent entity.</summary>
   /// <param name="transient">The transient entity that requires a persistent reference.</param>
   /// <param name="persistent">The persistent entity associated with the transient entity.</param>
   public void AddPair(IKeyGuidEntity transient, IKeyGuidEntity persistent)
      => _resolvingMap[transient] = persistent;

   /// <summary>Resolves the primary keys of transient entities in the resolving map by updating them with the corresponding primary keys of persistent entities. Additionally, updates audit metadata such as creation and update details for entities implementing the IUserAuditable interface.</summary>
   public void ResolvePrimaryKeys()
   {
      foreach (var pair in _resolvingMap)
      {
         if (pair.Key.Id == Guid.Empty && pair.Value.Id != Guid.Empty)
         {
            pair.Key.Id = pair.Value.Id;

            if (pair is { Key: IUserAuditable transient, Value: IUserAuditable presistent })
            {
               transient.CreatedBy = presistent.CreatedBy;
               transient.Created   = presistent.Created;
               transient.UpdatedBy = presistent.UpdatedBy;
               transient.Updated   = presistent.Updated;
            }
         }
      }
   }
}