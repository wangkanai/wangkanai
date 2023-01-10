// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.IO;

using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Wangkanai.Responsive.Mocks;

internal class MockPageRouteModel
{
	private static readonly string            IndexFileName = "Index" + RazorViewEngine.ViewExtension;
	private readonly        string            _normalizedAreaRootDirectory;
	private readonly        string            _normalizedRootDirectory;
	private readonly        RazorPagesOptions _options;

	public MockPageRouteModel(RazorPagesOptions options)
	{
		_options = options ?? throw new ArgumentNullException(nameof(options));

		_normalizedRootDirectory     = NormalizeDirectory(options.RootDirectory);
		_normalizedAreaRootDirectory = "/Areas/";
	}

	public PageRouteModel CreateRouteModel(string relativePath, string routeTemplate)
	{
		var viewEnginePath = GetViewEnginePath(_normalizedRootDirectory, relativePath);
		var model          = new PageRouteModel(relativePath, viewEnginePath);

		PopulateRouteModel(model, viewEnginePath, routeTemplate);

		return model;
	}

	public PageRouteModel CreateAreaRouteModel(string relativePath, string routeTemplate)
	{
		if (!TryParseAreaPath(relativePath, out var areaResult))
			return null!;

		var model  = new PageRouteModel(relativePath, areaResult.viewEnginePath, areaResult.areaName);
		var prefix = CreateAreaRoute(areaResult.areaName, areaResult.viewEnginePath);
		PopulateRouteModel(model, prefix, routeTemplate);
		model.RouteValues["area"] = areaResult.areaName;

		return model;
	}


	private static void PopulateRouteModel(PageRouteModel model, string pageRoute, string routeTemplate)
	{
		model.RouteValues.Add("page", model.ViewEnginePath);

		var selectorModel = CreateSelectorModel(pageRoute, routeTemplate);
		model.Selectors.Add(selectorModel);

		var fileName = Path.GetFileName(model.RelativePath);
		if (AttributeRouteModel.IsOverridePattern(routeTemplate) && string.Equals(IndexFileName, fileName, StringComparison.OrdinalIgnoreCase))
		{
			// For pages without on override route, and ending in /Index.cshtml
			// I want ot allow incoming routing, but force outgoing routes to match to the path sans /Index.
			selectorModel.AttributeRouteModel.SuppressLinkGeneration = true;

			var index               = pageRoute.LastIndexOf('/');
			var parentDirectoryPath = index == -1 ? string.Empty : pageRoute.Substring(0, index);
			model.Selectors.Add(CreateSelectorModel(parentDirectoryPath, routeTemplate));
		}
	}

	private static SelectorModel CreateSelectorModel(string prefix, string routeTemplate)
	{
		return new SelectorModel
		{
			AttributeRouteModel = new AttributeRouteModel
			{
				Template = AttributeRouteModel.CombineTemplates(prefix, routeTemplate)
			},
			EndpointMetadata = { new PageRouteMetadata(prefix, routeTemplate) }
		};
	}

	private bool TryParseAreaPath(string relativePath, out (string areaName, string viewEnginePath) result)
	{
		// path = "/Areas/Admin/Pages/Manage/Home.cshtml"
		// Result ("Admin", "/Manage/Home")
		const string areaPagesRoot = "/Pages/";

		result = default;
		// Parse the area root directory
		var areaRootEndIndex = relativePath.IndexOf('/', 1);
		if (areaRootEndIndex == -1 || areaRootEndIndex >= relativePath.Length - 1 || !relativePath.StartsWith(_normalizedAreaRootDirectory, StringComparison.OrdinalIgnoreCase))
			return false;

		// The first directory that follows the area root is the area name
		var areaEndIndex = relativePath.IndexOf('/', areaRootEndIndex + 1);
		if (areaEndIndex == -1 || areaEndIndex == relativePath.Length)
			return false;

		// Ensire the next token is the "Pages" directory
		var areaName = relativePath.Substring(areaRootEndIndex + 1, areaEndIndex - areaRootEndIndex - 1);
		if (string.Compare(relativePath, areaEndIndex, areaPagesRoot, 0, areaPagesRoot.Length, StringComparison.OrdinalIgnoreCase) != 0)
			return false;

		// Include the trailing slash of the root directory at the start of the viewEnginePath
		var pageNameIndex  = areaEndIndex + areaPagesRoot.Length - 1;
		var viewEnginePath = relativePath.Substring(pageNameIndex, relativePath.Length - pageNameIndex - RazorViewEngine.ViewExtension.Length);

		result = (areaName, viewEnginePath);
		return true;
	}

	private string GetViewEnginePath(string rootDirectory, string path)
	{
		if (rootDirectory is null)
			throw new ArgumentNullException(nameof(rootDirectory));
		if (path is null)
			throw new ArgumentNullException(nameof(path));

		var start = rootDirectory.Length - 1;
		var end   = path.Length - RazorViewEngine.ViewExtension.Length;

		return path.Substring(start, end - start);
	}

	private static string CreateAreaRoute(string areaName, string viewEnginePath)
	{
		return string.Create(1 + areaName.Length + viewEnginePath.Length, (areaName, viewEnginePath), (span, tuple) => {
			var (areaNameValue, viewEnginePathValue) = tuple;

			span[0] = '/';
			span    = span.Slice(1);

			areaNameValue.AsSpan().CopyTo(span);
			span = span.Slice(areaNameValue.Length);

			viewEnginePathValue.AsSpan().CopyTo(span);
		});
	}

	private static string NormalizeDirectory(string directory)
	{
		return directory.Length > 1
		       && !directory.EndsWith("/", StringComparison.Ordinal)
			       ? directory + "/"
			       : directory;
	}
}