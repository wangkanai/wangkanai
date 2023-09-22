// Copyright (c) 2014-2023 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Threading;

namespace Wangkanai.Domain.Events;

public static class EventSuppressor
{
	private sealed class DisposableActionGuard : IDisposable
	{
		private readonly Action _action;

		private static readonly AsyncLocal<bool> EventsSuppressedStorage = new();

		public static bool EventsSuppressed => EventsSuppressedStorage.Value;

		public DisposableActionGuard(Action action) => _action = action;

		public void Dispose()
			=> Dispose(true);

		private void Dispose(bool disposing)
		{
			if (disposing)
				_action();
		}

		public static IDisposable SuppressEvents()
		{
			EventsSuppressedStorage.Value = true;
			return new DisposableActionGuard(() => EventsSuppressedStorage.Value = false);
		}
	}
}