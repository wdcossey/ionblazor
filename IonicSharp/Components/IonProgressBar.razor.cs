namespace IonicSharp.Components;

public partial class IonProgressBar : IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If the buffer and value are smaller than 1, the buffer circles will show.
    /// The buffer should be between [0.0, 1.0].
    /// </summary>
    [Parameter] public double Buffer { get; set; } = 1.0d;
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <p/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public string? Mode { get; set; } = IonMode.Default;
    
    /// <summary>
    /// If true, reverse the progress bar direction.
    /// </summary>
    [Parameter] public bool Reversed { get; set; }

    /// <summary>
    /// The state of the progress bar, based on if the time the process takes is known or not.
    /// Default options are: <see cref="IonProgressBarType.Determinate"/> (no animation),
    /// <see cref="IonProgressBarType.Indeterminate"/> (animate from left to right).
    /// </summary>
    [Parameter] public string Type { get; set; } = IonProgressBarType.Determinate;
    
    /// <summary>
    /// The value determines how much of the active bar should display when the type is "determinate".
    /// The value should be between [0.0, 1.0].
    /// </summary>
    [Parameter] public double Value { get; set; } = 0.0d;

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

public static class IonProgressBarType
{
    public const string Determinate = "determinate";
    public const string Indeterminate = "indeterminate";
}
