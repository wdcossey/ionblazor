﻿namespace IonBlazor.Components;

public partial class IonNote : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}