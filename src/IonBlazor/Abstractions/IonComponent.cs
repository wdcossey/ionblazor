namespace IonBlazor.Abstractions;

public abstract class IonComponent : ComponentBase, IIonComponent, IAsyncDisposable
{
    private readonly IJSRuntime _jsRuntime = null!;

    [Inject]
    internal IJSRuntime JsRuntime
    {
        get => _jsRuntime;
        init
        {
            _jsRuntime = value;
            ConfigureJsComponent();
        }
    }

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; init; }

    protected virtual string? JsImportName => null;

/*#if NET8_0_OR_GREATER
    internal virtual Task<IJSObjectReference>? JsComponent { get; init; }
#else
    internal Lazy<Task<IJSObjectReference>> JsComponent { get; private set; } = null!;
#endif*/

    internal Lazy<Task<IJSObjectReference>> JsComponent { get; set; } = null!;

    /// <summary>
    /// Reference to the Ionic (Html) component
    /// </summary>
    public abstract ElementReference IonElement { get; }



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

    private void ConfigureJsComponent()
    {
        JsComponent = new Lazy<Task<IJSObjectReference>>(() =>
            _jsRuntime.ImportAsync(JsImportName!));
    }
}