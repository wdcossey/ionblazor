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

    /// <summary>
    /// Set once ion event listeners have been wired up for <see cref="IonElement"/>, so that
    /// <see cref="DisposeAsync"/> knows to remove them from the DOM. Removing the DOM listeners on
    /// disposal stops Ionic from invoking DotNetObjectReferences that have already been disposed
    /// (e.g. a teardown <c>ionBlur</c> fired while navigating away).
    /// </summary>
    private bool ListenersAttached { get; set; }

    public async ValueTask AddEventListener<TArgs>(string eventName, DotNetObjectReference<TArgs> callback)
        where TArgs : class
    {
        await using IJSObjectReference jsModule = await JsRuntime.ImportAsync("common");
        await jsModule.InvokeVoidAsync("attachListener", eventName, IonElement, callback).AsTask();
        ListenersAttached = true;
    }

    internal async ValueTask AttachIonListenersAsync(params IonEvent[]? args)
    {
        await JsRuntime.AttachIonListenersAsync(IonElement, args);
        ListenersAttached = true;
    }

    internal async ValueTask DetachIonListenersAsync()
    {
        if (ListenersAttached is not true)
            return;

        ListenersAttached = false;
        await JsRuntime.DetachIonListenersAsync(IonElement);
    }

    public virtual async ValueTask DisposeAsync()
    {
        await DetachIonListenersAsync();
        GC.SuppressFinalize(this);
    }
}