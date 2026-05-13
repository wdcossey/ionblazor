namespace IonBlazor.Abstractions;

public abstract class IonComponent : ComponentBase, IIonComponent, IAsyncDisposable
{
    [Inject]
    internal IJSRuntime JsRuntime { get; init; } = null!;

    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? Attributes { get; init; }

    /// <summary>
    /// Reference to the Ionic (Html) component
    /// </summary>
    public ElementReference IonElement { get; protected set; }

    public async ValueTask AddEventListener<TArgs>(string eventName, DotNetObjectReference<TArgs> callback)
        where TArgs : class
    {
        await using IJSObjectReference jsModule = await JsRuntime.ImportAsync("common");
        await jsModule.InvokeVoidAsync("attachListener", eventName, IonElement, callback).AsTask();
    }

    public virtual async ValueTask DisposeAsync()
    {
        await ValueTask.CompletedTask;
        GC.SuppressFinalize(this);
    }
}