namespace IonicSharp.Components;

public partial class IonListHeader : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// How the bottom border should be displayed on the list header.
    /// </summary> 
    [Parameter]
    public string? Lines { get; set; } = IonListHeaderLines.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}

public static class IonListHeaderLines
{
    public const string? Default = null;
    public const string Full = "full";
    public const string Inset = "inset";
    public const string None = "none";
}