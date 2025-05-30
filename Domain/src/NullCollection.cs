// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

using System.Collections.ObjectModel;

namespace Wangkanai.Domain;

/// <summary>
/// Represents a collection of objects of a specified type that does not hold any elements.
/// This class is intended to provide a collection-like behavior which is effectively empty,
/// and can be used as a placeholder or default implementation where an empty collection might be needed.
/// </summary>
/// <typeparam name="T">The type of elements contained in the collection.</typeparam>
public class NullCollection<T> : ObservableCollection<T>;
