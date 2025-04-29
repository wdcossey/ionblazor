namespace IonBlazor.Components;

public sealed partial class IonIcon : IonComponent, IIonColorComponent
{
    [Parameter]
    public string? Name { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; } = IonColor.Default;

    [Parameter]
    public string? Size { get; set; } = IonIconSize.Null;

    public void Configure(Action<IonIcon> configure)
    {
        configure(this);
        _ = InvokeAsync(StateHasChanged);
    }
}