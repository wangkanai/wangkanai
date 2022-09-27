// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System.IO;

using Microsoft.Extensions.FileProviders;

using Moq;

using Xunit;

namespace Wangkanai.Extensions;

public class FileInfoExtensionTests
{
    [Fact]
    public void GetFileExtensions()
    {
        var txt = Mock.Of<IFileInfo>(f => f.Name == "test.txt");
        Assert.Equal(".txt", txt.Extension());
    }
    
    [Fact]
    public void PngImageExtensions()
    {
        var png = Mock.Of<IFileInfo>(f => f.Name == "test.png");
        Assert.Equal(".png", png.Extension());
        Assert.Equal(ImageExtension.Png, png.GetImageExtension());
        Assert.Equal("image/png", png.ContentType());
    }
    
    [Fact]
    public void SvgImageExtensions()
    {
        var svg = Mock.Of<IFileInfo>(f => f.Name == "test.svg");
        Assert.Equal(".svg", svg.Extension());
        Assert.Equal(ImageExtension.Svg, svg.GetImageExtension());
        Assert.Equal("image/svg", svg.ContentType());
    }
    
    [Fact]
    public void UnknownImageExtensions()
    {
        var webp = Mock.Of<IFileInfo>(f => f.Name == "test.webp");
        Assert.Equal(".webp", webp.Extension());
        Assert.Equal(ImageExtension.Unknown, webp.GetImageExtension());
        Assert.Equal("application/octet-stream", webp.ContentType());
    }
}