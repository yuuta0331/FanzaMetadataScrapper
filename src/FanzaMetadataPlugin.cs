﻿using System;
using System.Collections.Generic;
using System.Windows.Controls;
using JetBrains.Annotations;
using Playnite.SDK;
using Playnite.SDK.Plugins;

namespace FanzaMetadata;

[UsedImplicitly]
public class FanzaMetadataPlugin : MetadataPlugin
{
    private readonly IPlayniteAPI _playniteApi;

    public FanzaMetadataPlugin(IPlayniteAPI api) : base(api)
    {
        _playniteApi = api;
        Settings = new FanzaMetadataSettingsViewModel(this);
        Properties = new MetadataPluginProperties
        {
            HasSettings = true
        };
    }

    private FanzaMetadataSettingsViewModel Settings { get; }

    public override Guid Id { get; } = Guid.Parse("7ed11394-1d65-432f-b9dc-1f2cbd9f91bb");

    public override List<MetadataField> SupportedFields { get; } =
    [
        MetadataField.Name,
        MetadataField.AgeRating,
        MetadataField.Description,
        MetadataField.Genres,
        MetadataField.Developers,
        MetadataField.Links,
        MetadataField.CommunityScore,
        MetadataField.CommunityScore,
        MetadataField.ReleaseDate,
        MetadataField.Series,
        MetadataField.CoverImage,
        MetadataField.BackgroundImage
    ];

    public override string Name => "Fanza";

    public override OnDemandMetadataProvider GetMetadataProvider(MetadataRequestOptions options)
    {
        return new FanzaMetadataProvider(_playniteApi, options, Settings.Settings);
    }

    public override ISettings GetSettings(bool firstRunSettings)
    {
        return Settings;
    }

    public override UserControl GetSettingsView(bool firstRunSettings)
    {
        return new FanzaMetadataSettingsView();
    }
}