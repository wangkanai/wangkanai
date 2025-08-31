// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Domain;

/// <summary>Represents an entity that contains language-related functionality.</summary>
public interface IHasLanguage
{
   /// <summary>Gets the ISO 639-1 language code representing the language associated with the entity.</summary>
   string LanguageCode { get; }
}