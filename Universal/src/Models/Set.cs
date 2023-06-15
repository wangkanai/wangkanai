// Copyright (c) 2014-2022 Sarin Na Wangkanai, All Rights Reserved.Apache License, Version 2.0

namespace Wangkanai.Universal.Models;

public class Set : Field
{
	public Set() { format = "ga('set','{0}','{1}');"; }

    #region Hit

    /// <summary>
    ///     Specifies that a hit be considered non-interactive
    /// </summary>
    public string NonInteraction { get; set; }

    #endregion

    #region General

    /// <summary>
    ///     When present, the IP address of the sender will be anonymized
    /// </summary>
    internal bool AnonymizeIp { get; set; }

    /// <summary>
    ///     By default, tracking beacons sent from https pages will be sent using https while beacons sent from http pages will
    ///     be sent using http. Setting forceSSL to true will force http pages to also send all beacons using https
    /// </summary>
    internal bool ForceSSL { get; set; }

    #endregion

    #region Traffic Sources

    /// <summary>
    ///     Specifies which referral source brought traffic to a website. This value is also used to compute the traffic
    ///     source. The format of this value is a URL. This field is initialized by the create command and is only set when the
    ///     current hostname differs from the referrer hostname, unless the 'alwaysSendReferrer' field is set to true
    /// </summary>
    public string Referrer { get; set; }

    /// <summary>
    ///     Specifies the campaign name
    /// </summary>
    public string CampaignName { get; set; }

    /// <summary>
    ///     Specifies the campaign source
    /// </summary>
    public string CampaignSource { get; set; }

    /// <summary>
    ///     Specifies the campaign medium
    /// </summary>
    public string CampaignMedium { get; set; }

    /// <summary>
    ///     Specifies the campaign keyword
    /// </summary>
    public string CampaignKeyword { get; set; }

    /// <summary>
    ///     Specifies the campaign content
    /// </summary>
    public string CampaignContent { get; set; }

    /// <summary>
    ///     Specifies the campaign ID
    /// </summary>
    public string CampaignId { get; set; }

    #endregion

    #region System Info

    /// <summary>
    ///     Specifies the screen resolution. This field is initialized by the create command
    /// </summary>
    public string ScreenResolution { get; set; }

    /// <summary>
    ///     Specifies the viewable area of the browser / device. This field is initialized by the create command
    /// </summary>
    public string ViewportSize { get; set; }

    /// <summary>
    ///     Specifies the character set used to encode the page / document. This field is initialized by the create command
    /// </summary>
    public string Encoding { get; set; }

    /// <summary>
    ///     Specifies the screen color depth. This field is initialized by the create command
    /// </summary>
    public string ScreenColors { get; set; }

    /// <summary>
    ///     Specifies the language. This field is initialized by the create command
    /// </summary>
    public string Language { get; set; }

    /// <summary>
    ///     Specifies whether Java was enabled. This field is initialized by the create command
    /// </summary>
    public bool JavaEnabled { get; set; }

    /// <summary>
    ///     Specifies the flash version. This field is initialized by the create command
    /// </summary>
    public string FlashVersion { get; set; }

    #endregion

    #region Content Information

    /// <summary>
    ///     Specifies the full URL (excluding anchor) of the page. This field is initialized by the create command
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    ///     Specifies the hostname from which content was hosted
    /// </summary>
    public string Hostname { get; set; }

    /// <summary>
    ///     The path portion of the page URL. Should begin with '/'. Used to specify virtual page paths
    /// </summary>
    public string Page { get; set; }

    /// <summary>
    ///     The title of the page / document. Defaults to document.title
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     The ID of a clicked DOM element, used to disambiguate multiple links to the same URL in In-Page Universal
    ///     reports when Enhanced Link Attribution is enabled for the property
    /// </summary>
    public string LinkId { get; set; }

    #endregion

    #region App Tracking

    /// <summary>
    ///     Specifies the application name. Only visible in app views (profiles)
    /// </summary>
    public string AppName { get; set; }

    /// <summary>
    ///     Specifies the application version. Only visible in app views (profiles)
    /// </summary>
    public string AppVersion { get; set; }

    #endregion

    #region Custom Dimension Metric

    /// <summary>
    ///     Each custom dimension has an associated index. There is a maximum of 20 custom dimensions (200 for Premium
    ///     accounts). The name suffix must be a positive integer between 1 and 200, inclusive
    /// </summary>
    public string Dimension { get; set; }

    /// <summary>
    ///     Each custom metric has an associated index. There is a maximum of 20 custom metrics (200 for Premium accounts). The
    ///     name suffix must be a positive integer between 1 and 200, inclusive
    /// </summary>
    public string Metric { get; set; }

    #endregion

    #region Content Experiments

    /// <summary>
    ///     This parameter specifies that this visitor has been exposed to an experiment with the given ID. It should be sent
    ///     in conjunction with the Experiment Variant parameter
    /// </summary>
    public string ExpId { get; set; }

    /// <summary>
    ///     This parameter specifies that this visitor has been exposed to a particular variation of an experiment. It should
    ///     be sent in conjunction with the Experiment ID parameter
    /// </summary>
    public string ExpVar { get; set; }

    #endregion
}