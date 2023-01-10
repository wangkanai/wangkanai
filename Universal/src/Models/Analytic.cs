// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Models;

/// <summary>
///     This is a singleton service class that will interact with Google Wangkanai.Universal
/// </summary>
public class Analytic
{
	// private static volatile Analytic instance = new Analytic();
	// public static Analytic Instance { get { return instance; } }
	// private static readonly Configuration config;
	// private Analytic() { }
	// static Analytic()
	// {
	//     config = Configuration.GetConfiguration();
	// }
	/// <summary>
	/// The method render the javascript block to html for use in the header tag of the webpage
	/// </summary>
	/// <param name="session">[Nullable] Configuration of Cookie, Pageview, Event, Ecommence, Social, Experiemnts, Dimension, Matric</param>
	/// <returns></returns>
	// public string Render(Session session)
	// {
	//     if (session == null) session = new Session();           
	//     var js = new StringBuilder();
	//     js.AppendLine("<!-- Google Wangkanai.Universal --><script>");
	//     js.AppendLine(JsUniversalAnalyticJavascriptLibrary());
	//
	//     // Create method
	//     var create = new Create(config, new ConfigOption(config, session));
	//     js.AppendLine(create.ToString());
	//
	//     // Require method
	//     js.AppendLine((new DisplayFeatures(config)).ToString());
	//     js.AppendLine((new EnhancedLink(config)).ToString());
	//
	//     // Set method
	//     js.AppendLine((new Set(config)).ToString());
	//
	//     // Send method
	//     //js.AppendLine(new Send(session).ToString());
	//
	//     js.AppendLine("</script><!-- End Google Wangkanai.Universal -->");
	//     return js.ToString();
	// }
	//
	// internal string JsUniversalAnalyticJavascriptLibrary()
	// {
	//     StringBuilder js = new StringBuilder();
	//     js.AppendLine("(function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){");
	//     js.AppendLine("(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o),");
	//     js.AppendLine("m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)");
	//     js.AppendLine("})(window,document,'script','//www.google-analytics.com/analytics.js','ga');");
	//     return js.ToString();
	// }
}