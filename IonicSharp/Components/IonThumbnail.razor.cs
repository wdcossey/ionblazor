﻿namespace IonicSharp.Components;

public partial class IonThumbnail : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }
}