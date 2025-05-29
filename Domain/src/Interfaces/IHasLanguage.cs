// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved. Apache License, Version 2.0

namespace Wangkanai.Domain;

/// <summary>Represents an entity that contains language-related functionality.</summary>
public interface IHasLanguage
{
	/// <summary>Gets the ISO 639-1 language code representing the language associated with the entity.</summary>
	string LanguageCode { get; }
}
