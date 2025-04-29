namespace IonBlazor.Components;

public sealed partial class IonToolbar : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
}