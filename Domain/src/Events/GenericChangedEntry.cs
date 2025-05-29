// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain.Events;

/// <summary>
/// Represents a generic entry that tracks changes within a domain event system.
/// This class is a value object and encapsulates the logic for working with domain changes.
/// </summary>
/// <typeparam name="T">The type of object that is being tracked or changed.</typeparam>
public class GenericChangedEntry<T> : IValueObject;
