namespace IonBlazor.Components;

public sealed partial class IonPicker : IonContentComponent, IIonModeComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; }
}