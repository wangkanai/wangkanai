// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

using System;
using System.Text;


using Xunit;

namespace Wangkanai.Universal.Models
{
    public class AnalyticTests
    {
        // [Fact]
        // public void TestUniversalAnalyticJavascriptLibraryScript()
        // {
        //     Analytic analytic        = Analytic.Instance;
        //     var      GeneratedScript = analytic.JsUniversalAnalyticJavascriptLibrary();
        //     var      ModelScript     = new StringBuilder();
        //     ModelScript.AppendLine("(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){");
        //     ModelScript.AppendLine("(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),");
        //     ModelScript.AppendLine("m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)");
        //     ModelScript.AppendLine("})(window,document,'script','//www.google-analytics.com/analytics.js','ga');");
        //     Assert.Equal(ModelScript.ToString(), analytic.JsUniversalAnalyticJavascriptLibrary());
        // }

        // [Fact]
        // public void TestCreateTrackerObjectWithoutCookie()
        // {
        //     var config = new Configuration();
        //     var create = new Create(config);
        //     Assert.Equal("ga('create', 'UA-XXXX-Y', 'auto');", create.ToString());
        // }

        // [Fact]
        // public void TestScriptBlockWrite()
        // {
        //     var analytic    = Analytic.Instance;
        //     var transaction = new Transaction("1234", "testing", 1.0, 1.0);
        //     transaction.Items.Add(new Item("product a", "a001", "fertilizer", 1.0, 1));
        //     transaction.Items.Add(new Item("product b", "b001", "fertilizer", 1.0, 1));
        //     var session = new Session();
        //     //session.Cookie = new Cookie("www.sathai.com", "testing", 30000);
        //     session.Events.Add(new Event("button", "click", "submit", "1"));
        //     session.Transaction = transaction;
        //     Console.WriteLine(analytic.Render(session));
        // }
    }
}