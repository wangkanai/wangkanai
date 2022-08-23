// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Graph.Providers;

internal class HttpClientHttpProvider : IHttpProvider
{
    private readonly HttpClient http;

    public ISerializer Serializer     { get; }      = new Serializer();
    public TimeSpan    OverallTimeout { get; set; } = TimeSpan.FromSeconds(300);

    public HttpClientHttpProvider(HttpClient http)
        => this.http = http;

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        => http.SendAsync(request);

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage   request,
                                               HttpCompletionOption completionOption,
                                               CancellationToken    cancellationToken)
        => http.SendAsync(request, completionOption, cancellationToken);

    public void Dispose()
    {
    }
}