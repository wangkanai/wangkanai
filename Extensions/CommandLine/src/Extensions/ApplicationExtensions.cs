// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.Reflection;

namespace Wangkanai.System.Extensions.CommandLine;

internal static class ApplicationExtensions
{
    public static CommandOption HelpOption(this CommandLineApplication app)
        => app.HelpOption("-?|-h|--help");

    public static CommandOption VerboseOption(this CommandLineApplication app)
        => app.Option("-v|--verbose", "Show verbose output", CommandOptionType.NoValue, inherited: true);

    public static void OnExecute(this CommandLineApplication app, Action action)
        => app.OnExecute(() =>
        {
            action();
            return 0;
        });

    public static CommandOption Option(this CommandLineApplication app, string template, string description)
        => app.Option(
            template,
            description,
            template.IndexOf("<", StringComparison.Ordinal) != -1
                ? template.EndsWith(">...", StringComparison.Ordinal)
                      ? CommandOptionType.MultipleValue
                      : CommandOptionType.SingleValue
                : CommandOptionType.NoValue);

    public static void VersionOptionFromAssemblyAttributes(this CommandLineApplication app)
        => app.VersionOptionFromAssemblyAttributes(typeof(ApplicationExtensions).Assembly);

    public static void VersionOptionFromAssemblyAttributes(this CommandLineApplication app, Assembly assembly)
        => app.VersionOption("--version", GetInformationalVersion(assembly));

    private static string GetInformationalVersion(Assembly assembly)
    {
        var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

        var versionAttribute = attribute == null
                                   ? assembly.GetName().Version?.ToString()
                                   : attribute.InformationalVersion;

        return versionAttribute.ThrowIfNull() ?? throw new InvalidOperationException();
    }
}