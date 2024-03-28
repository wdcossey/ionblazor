namespace IonicSharp.Components;

public partial class IonSegmentButton : IonComponent, IIonModeComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;

    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the segment button.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <summary>
    /// Set the layout of the text and icon in the segment.
    /// </summary>
    [Parameter]
    public string? Layout { get; set; } = IonSegmentButtonLayout.IconTop;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The type of the button.
    /// </summary>
    [Parameter]
    public string Type { get; set; } = IonSegmentButtonType.Button;

    /// <summary>
    /// the value of the segment.
    /// </summary>
    [Parameter]
    public string Value { get; set; } = null!;
}

public static class IonSegmentButtonLayout
{
    public const string IconBottom = "icon-bottom";
    public const string IconEnd = "icon-end";
    public const string IconHide = "icon-hide";
    public const string IconStart = "icon-start";
    public const string IconTop = "icon-top";
    public const string LabelHide = "label-hide";
}

public static class IonSegmentButtonType
{
    public const string Button = "button";
    public const string Reset = "reset";
    public const string Submit = "submit";
}