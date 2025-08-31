// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an auditable entity that includes information about the user responsible for creation and updates.</summary>
/// <remarks>This interface extends the <see cref="ICreatedEntity"/> and <see cref="IUpdatedEntity"/> interfaces to include properties for tracking the user who created and last updated the entity. It is intended for use in ensuring accountability and auditing in systems where user actions need to be recorded.</remarks>
public interface IUserAuditable : ICreatedEntity, IUpdatedEntity
{
   /// <summary>Gets or sets the identifier of the user who created the entity.</summary>
   /// <remarks>The <c>CreatedBy</c> property is used to track the user responsible for creating this entity. This is useful for audit purposes and ensuring accountability in systems where user actions are monitored. </remarks>
   string? CreatedBy { get; set; }

   /// <summary>Gets or sets the identifier of the user who last updated the entity.</summary>
   /// <remarks>The <c>UpdatedBy</c> property is used to track the user responsible for the most recent update to the entity. This is essential for auditing purposes and ensuring traceability in systems that monitor changes to entities.</remarks>
   string? UpdatedBy { get; set; }
}