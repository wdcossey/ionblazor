namespace IonBlazor.Components;

public sealed partial class IonSegmentButton : IonContentComponent, IIonModeComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

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