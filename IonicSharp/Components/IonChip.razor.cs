namespace IonicSharp.Components;

public partial class IonChip : IonComponent, IIonModeComponent, IIonContentComponent, IIonColorComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter] 
    public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// If true, the user cannot interact with the <see cref="IonChip"/>.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// Display an outline style <see cref="IonChip"/>.
    /// </summary>
    [Parameter]
    public bool? Outline { get; set; }
}

public static class IonColor 
{
    public const string Danger = "danger";
    public const string Dark = "dark";
    public const string Light = "light";
    public const string Medium = "medium";
    public const string Primary = "primary";
    public const string Secondary = "secondary";
    public const string Success = "success";
    public const string Tertiary = "tertiary";
    public const string Warning = "warning";
}
