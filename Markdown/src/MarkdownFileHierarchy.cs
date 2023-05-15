// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Text;

namespace Wangkanai.Markdown;

internal static class MarkdownFileHierarchy
{
	private const string ViewStartFileName = "_ViewStart.md";

	public static IEnumerable<string> GetViewStartPaths(string path)
	{
		if (string.IsNullOrEmpty(path))
			throw new ArgumentException(Resources.ArgumentCannotBeNullOrEmpty, nameof(path));

		if (path[0] != '/')
			throw new ArgumentException(Resources.MarkdownProject_PathMustStartWithForwardSlash, nameof(path));

		if (path.Length == 1)
			yield break;

		var builder       = new StringBuilder(path);
		var maxIterations = 255;
		var index         = path.Length;
		while (maxIterations-- > 0 && index > 1 && (index = path.LastIndexOf('/', index - 1)) != -1)
		{
			builder.Length = index + 1;
			builder.Append(ViewStartFileName);

			var itemPath = builder.ToString();
			yield return itemPath;
		}
	}
}
