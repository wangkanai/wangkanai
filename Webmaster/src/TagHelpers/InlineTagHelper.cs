using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;

namespace Wangkanai.Webmaster.TagHelpers;

public class InlineTagHelper : UrlResolutionTagHelper
{
    private const string CacheKeyPrefix = "InlineTagHelper-";

    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IMemoryCache        _cache;

    [ActivatorUtilitiesConstructor]
    public InlineTagHelper(IWebHostEnvironment webHostEnvironment, IMemoryCache cache, HtmlEncoder htmlEncoder, IUrlHelperFactory urlHelperFactory) : base(urlHelperFactory, htmlEncoder)
    {
        _webHostEnvironment = webHostEnvironment;
        _cache              = cache;
    }

    private async Task<T> GetContentAsync<T>(ICacheEntry entry, string path, Func<IFileInfo, Task<T>> getContent)
    {
        var fileProvider = _webHostEnvironment.WebRootFileProvider;
        var changeToken  = fileProvider.Watch(path);

        entry.SetPriority(CacheItemPriority.NeverRemove);
        entry.AddExpirationToken(changeToken);

        var file = fileProvider.GetFileInfo(path);
        return file is not { Exists: true }
                   ? default
                   : await getContent(file);
    }

    protected Task<string> GetFileContentStringAsync(string path)
        => GetContentFuncAsync(path, GetFileContentAsStringAsync);

    protected Task<string> GetFileContentBase64Async(string path)
        => GetContentFuncAsync(path, GetFileContentAsBase64Async);

    private Task<string> GetContentFuncAsync(string path, Func<IFileInfo, Task<string>> getContent)
        => _cache.GetOrCreateAsync(
            CacheKeyPrefix + path,
            entry => GetContentAsync(entry, path, getContent));

    private static async Task<string> GetFileContentAsStringAsync(IFileInfo file)
    {
        await using var stream = file.CreateReadStream();
        using var       reader = new StreamReader(stream);

        return await reader.ReadToEndAsync();
    }

    private static async Task<string> GetFileContentAsBase64Async(IFileInfo file)
    {
        await using var stream = file.CreateReadStream();
        await using var writer = new MemoryStream();

        await stream.CopyToAsync(writer);
        var offset = writer.Seek(0, SeekOrigin.Begin);
        if (offset != 0)
            throw new FileLoadException("File error");

        return Convert.ToBase64String(writer.ToArray());
    }
}