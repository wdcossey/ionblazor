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

    internal Lazy<Task<IJSObjectReference>> JsComponent { get; set; } = null!;

    /// <summary>
    /// Reference to the Ionic (Html) component
    /// </summary>
    public ElementReference IonElement { get; protected set; }

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

    public async ValueTask AddEventListener<TArgs>(string eventName, DotNetObjectReference<TArgs> callback)
        where TArgs : class
    {
        await using var jsModule = await JsRuntime.ImportAsync("common");
        await jsModule.InvokeVoidAsync("attachListener", eventName, IonElement, callback).AsTask();
    }
}