// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Wangkanai.Federation;

internal static class FederationSerializer
{
	private static readonly JsonSerializerOptions Options = new()
	{
		DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
	};

	public static string ToString(object o)
		=> JsonSerializer.Serialize(o, Options);

	public static T FromString<T>(string value)
		=> JsonSerializer.Deserialize<T>(value, Options);
}
