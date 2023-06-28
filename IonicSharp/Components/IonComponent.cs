namespace IonicSharp.Components;

public interface IIonComponent
{
    
}

public abstract class IonComponent : ComponentBase, IIonComponent
{
    [Inject]
    protected IJSRuntime JsRuntime { get; set; } = null!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public virtual string? Class { get; set; }
}

public interface IIonColorComponent
{
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    string? Color { get; set; }
}

public interface IIonModeComponent
{
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    string? Mode { get; set; }
}

public interface IIonContentComponent
{
    RenderFragment? ChildContent { get; set; }
}