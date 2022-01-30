using Microsoft.Extensions.FileProviders;

namespace Wangkanai.Extensions;

public static class FileInfoExtensions
{
    public static string ContentType(this IFileInfo fileInfo)
    {
        var name = fileInfo.Name;

        if (name.IsExtension("jpg"))
            return "image/jpeg";
        if (name.IsExtension("gif"))
            return "image/gif";
        if (name.IsExtension("png"))
            return "image/png";
        if (name.IsExtension("svg"))
            return "image/svg";

        return "application/octet-stream";
    }

    private static bool IsExtension(this string name, string extension)
        => name.EndsWith(extension, StringComparison.OrdinalIgnoreCase);
}