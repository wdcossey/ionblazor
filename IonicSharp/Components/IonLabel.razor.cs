namespace IonicSharp.Components;

public partial class IonLabel : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// The position determines where and how the label behaves inside an item.
    /// </summary>
    [Parameter]
    public string? Position { get; set; } = IonLabelPosition.Default;
}

public static class IonLabelPosition
{
    public const string? Default = null;
    public const string Fixed = "fixed";
    public const string Floating = "floating";
    public const string Stacked = "stacked";
}