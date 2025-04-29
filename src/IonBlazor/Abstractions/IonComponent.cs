using System.Text;

namespace IonBlazor.Abstractions;

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

    internal readonly Lazy<Task<IJSObjectReference>> JsComponent;

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

    public virtual async ValueTask DisposeAsync()
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