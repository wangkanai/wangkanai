// Copyright (c) 2014-2025 Sarin Na Wangkanai, All Rights Reserved.

namespace Wangkanai.Federation.Hosting;

public class Endpoint
{
	public string Name { get; }
	public string Path { get; }
	public Type Handler { get; }

	public Endpoint(string name, string path, Type handler)
	{
		Name = name;
		Path = path;
		Handler = handler;
	}
}
