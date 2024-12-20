namespace IonBlazor.Components;

public sealed partial class IonToolbar : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}