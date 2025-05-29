// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using Wangkanai.Domain.Common;

namespace Wangkanai.Domain;

/// <summary>
/// Represents an interface for entities that can convert to and from models, and allows for the patching of entity instances.
/// This interface is commonly used to manage bidirectional transformations between entity and model representations in a domain.
/// </summary>
/// <typeparam name="TEntity">The type of the entity that implements this interface.</typeparam>
/// <typeparam name="TModel">The type of the model to which the entity can be transformed.</typeparam>
public interface IDataEntity<TEntity, TModel>
{
	/// <summary>Converts the current entity instance to the specified model representation.</summary>
	/// <param name="model">The model instance into which the entity data will be converted.</param>
	/// <returns>The converted model representation of the current entity.</returns>
	public TModel ToModel(TModel model);

	/// <summary>Populates the current entity instance with data from the specified model.</summary>
	/// <param name="model">The model instance containing data to be applied to the entity.</param>
	/// <param name="pkMap">The primary key resolving map used to manage entity relationships during the transformation.</param>
	/// <returns>The updated entity instance populated with data from the model.</returns>
	public TEntity FromModel(TModel model, PrimaryKeyResolvingMap pkMap);

	/// <summary>Applies updates from the current entity instance to the target entity.</summary>
	/// <param name="target">The target entity that will be updated with the changes from the current instance.</param>
	public void Patch(TEntity target);
}
