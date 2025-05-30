// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Represents an entry that tracks changes to an entity within a particular state.</summary>
public class TrackingEntry
{
	/// <summary>Gets or sets the entity being tracked within the tracking entry. This represents the specific object whose state is monitored for changes.</summary>
	public required object Entity { get; set; }

	/// <summary>Gets or sets the state of the entity within the tracking entry. This indicates the current status of the entity, such as whether it is unchanged, modified, added, deleted, or detached.</summary>
	public EntryState EntryState { get; set; } = EntryState.Unchanged;

	/// <summary>Determines whether the entity associated with the tracking entry is subscribed to change notifications or events within the context.</summary>
	internal bool IsSubscribed { get; set; }

	/// <summary>Returns a string that represents the current TrackingEntry instance, including its entity and state.</summary>
	/// <returns>A string representation of the TrackingEntry in the format "Entity EntryState".</returns>
	public override string ToString()
		=> $"{Entity ?? "null"} {EntryState}";
}
