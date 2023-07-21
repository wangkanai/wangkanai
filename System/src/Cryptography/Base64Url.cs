// Copyright (c) 2014-2024 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Cryptography;

public static class Base64Url
{
	public static string Encode(byte[] arg)
	{
		arg.ThrowIfNull();
		arg.ThrowIfEmpty();
		
		var str = Convert.ToBase64String(arg);
		str = str.Split('=')[0];
		str = str.Replace('+', '-');
		str = str.Replace('/', '_');
		return str;
	}

	public static byte[] Decode(string arg)
	{
		arg.ThrowIfNull();
		arg.ThrowIfEmpty();
		
		var str = arg;
		str = str.Replace('-', '+');
		str = str.Replace('_', '/');

		str = (str.Length % 4) switch
		{
			0 => str,
			2 => str + "==",
			3 => str + "=",
			_ => throw new Exception("Illegal base64url string!")
		};
		
		return Convert.FromBase64String(str);
	}
}