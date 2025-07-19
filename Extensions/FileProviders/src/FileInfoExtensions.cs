// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using Microsoft.Extensions.FileProviders;

namespace Wangkanai.Extensions.FileProviders;

public static class FileInfoExtensions
{
	public static string ContentType(this IFileInfo file)
		=> file.GetImageExtension() switch
		{
			ImageExtension.Jpg => "image/jpeg",
			ImageExtension.Gif => "image/gif",
			ImageExtension.Png => "image/png",
			ImageExtension.Svg => "image/svg",
			_ => "application/octet-stream",
		};

	public static ImageExtension GetImageExtension(this IFileInfo file)
		=> file.Extension() switch
		{
			".jpg" => ImageExtension.Jpg,
			".gif" => ImageExtension.Gif,
			".png" => ImageExtension.Png,
			".svg" => ImageExtension.Svg,
			_ => ImageExtension.Unknown
		};

	public static string Extension(this IFileInfo file)
		=> file.Name.Substring(file.Name.LastIndexOf('.')).ToLower();
}
