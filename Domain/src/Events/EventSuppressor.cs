// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

public static class EventSuppressor
{
	private sealed class DisposableActionGuard(Action action) : IDisposable
	{
		private static readonly AsyncLocal<bool> EventsSuppressedStorage = new();

		public static bool EventsSuppressed => EventsSuppressedStorage.Value;

		public void Dispose() => Dispose(true);

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
