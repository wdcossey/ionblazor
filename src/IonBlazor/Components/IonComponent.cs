using System.Text;

namespace IonBlazor.Components;

public interface IIonComponent
{
    ElementReference IonElement { get; }
}

public abstract class IonComponent : ComponentBase, IIonComponent, IAsyncDisposable
{
    [Inject]
    internal IJSRuntime JsRuntime { get; init; } = null!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; set; }

    [Parameter]
    public virtual string? Class { get; set; }

    /// <summary>
    /// Reference to the Ionic (Html) component
    /// </summary>
    public abstract ElementReference IonElement { get; }

    protected readonly Lazy<Task<IJSObjectReference>> JsComponent;

    protected IonComponent()
    {
        JsComponent = new Lazy<Task<IJSObjectReference>>(() =>
        {
            Type type = GetType();

            StringBuilder nameBuilder = type.IsGenericType ? new StringBuilder(type.Name[..^2]) : new StringBuilder(type.Name);

            if (!char.IsLower(nameBuilder[0]))
                nameBuilder[0] = char.ToLowerInvariant(nameBuilder[0]);

            return JsRuntime.ImportAsync(nameBuilder.ToString());
        });
    }

    public async ValueTask DisposeAsync()
    {
        try
        {
            if (JsComponent.IsValueCreated)
                await (await JsComponent.Value).DisposeAsync();
        }
        finally
        {
            GC.SuppressFinalize(this);
        }
    }
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
    /// <see cref="IonMode"/>
    /// </summary>
    string? Mode { get; set; }
}

public interface IIonContentComponent
{
    RenderFragment? ChildContent { get; set; }
}