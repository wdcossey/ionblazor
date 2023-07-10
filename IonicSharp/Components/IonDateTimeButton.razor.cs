namespace IonicSharp.Components;

public partial class IonDateTimeButton : IonComponent, IIonModeComponent, IIonColorComponent
{
    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; set; }

    /// <summary>
    /// The ID of the ion-datetime instance associated with the datetime button.
    /// </summary>
    [Parameter, EditorRequired]
    public string? DateTime { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the button.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;
}