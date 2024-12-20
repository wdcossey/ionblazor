namespace IonBlazor.Components;

public sealed partial class IonItemDivider : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// When it's set to <b>true</b>, the item-divider will stay visible when it reaches the top of the viewport
    /// until the next <b>ion-item-divider</b> replaces it.
    /// This feature relies in position:sticky: https://caniuse.com/#feat=css-sticky
    /// </summary>
    [Parameter]
    public bool? Sticky { get; set; }
}