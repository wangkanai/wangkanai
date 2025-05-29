// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a utility class designed to manage the suppression of events in the domain.
/// Specifically, used to temporarily disable or suppress event handling
/// to prevent unintended processing within a given context or operation.
/// </summary>
public static class EventSuppressor
{
	/// <summary>
	/// Represents a utility class that encapsulates an action to be executed
	/// upon the disposal of the object. Specifically, useful for managing cleanup
	/// or restoring state after an operation is completed.
	/// </summary>
	private sealed class DisposableActionGuard(Action action) : IDisposable
	{
		private static readonly AsyncLocal<bool> EventsSuppressedStorage = new();

		/// <summary>
		/// Indicates whether events are currently suppressed within the context of the event suppressor.
		/// When this property is set to true, event handling is temporarily disabled to prevent
		/// unintended processing.
		/// </summary>
		public static bool EventsSuppressed => EventsSuppressedStorage.Value;

		public void Dispose()
			=> Dispose(true);

		private void Dispose(bool disposing)
		{
			if (disposing)
				action();
		}

		public static IDisposable SuppressEvents()
		{
			EventsSuppressedStorage.Value = true;
			return new DisposableActionGuard(() => EventsSuppressedStorage.Value = false);
		}
	}
}
