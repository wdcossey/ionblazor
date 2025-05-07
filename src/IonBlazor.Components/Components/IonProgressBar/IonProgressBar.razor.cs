namespace IonBlazor.Components;

public sealed partial class IonProgressBar : IonContentComponent, IIonModeComponent, IIonColorComponent
{
    /// <summary>
    /// If the buffer and value are smaller than 1, the buffer circles will show.
    /// The buffer should be between [0.0, 1.0].
    /// </summary>
    [Parameter]
    public double Buffer { get; set; } = 1.0d;

    /// <inheritdoc/>
    [Parameter]
    public string? Color { get; init; }

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If true, reverse the progress bar direction.
    /// </summary>
    [Parameter]
    public bool Reversed { get; set; }

    /// <summary>
    /// The state of the progress bar, based on if the time the process takes is known or not.
    /// Default options are: <see cref="IonProgressBarType.Determinate"/> (no animation),
    /// <see cref="IonProgressBarType.Indeterminate"/> (animate from left to right).
    /// </summary>
    [Parameter]
    public string? Type { get; set; } = IonProgressBarType.Default;

    /// <summary>
    /// The value determines how much of the active bar should display when the type is "determinate".
    /// The value should be between [0.0, 1.0].
    /// </summary>
    [Parameter]
    public double Value { get; set; } = 0.0d;

    /// <summary>
    /// The value should be between [0.0, 1.0]
    /// </summary>
    /// <returns></returns>
    public IonProgressBar SetBuffer(double value)
    {
        InvokeAsync(() =>
        {
            Buffer = value;
            StateHasChanged();
        });
        return this;
    }

    /// <summary>
    /// The value should be between [0.0, 1.0]
    /// </summary>
    /// <returns></returns>
    public IonProgressBar SetValue(double value)
    {
        InvokeAsync(() =>
        {
            Value = value;
            StateHasChanged();
        });
        return this;
    }
}