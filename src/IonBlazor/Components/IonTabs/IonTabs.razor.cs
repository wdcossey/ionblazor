namespace IonBlazor.Components;

public sealed partial class IonTabs : IonContentComponent
{
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionTabsDidChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionTabsWillChangeReference;

    protected override string JsImportName => nameof(IonTabs);

    /// <summary>
    /// Emitted when the navigation has finished transitioning to a new component.
    /// </summary>
    [Parameter]
    public EventCallback<IonTabsDidChangeEventArgs> IonTabsDidChange { get; set; }

    /// <summary>
    /// Emitted when the navigation is about to transition to a new component.
    /// </summary>
    [Parameter]
    public EventCallback<IonTabsWillChangeEventArgs> IonTabsWillChange { get; set; }

    public IonTabs()
    {
        _ionTabsDidChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var tab = args?["detail"]?["tab"]?.GetValue<string>();
            await IonTabsDidChange.InvokeAsync(new IonTabsDidChangeEventArgs() { Tab = tab });
        });

        _ionTabsWillChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var tab = args?["detail"]?["tab"]?.GetValue<string>();
            await IonTabsWillChange.InvokeAsync(new IonTabsWillChangeEventArgs() { Tab = tab });
        });
    }

    /// <summary>
    /// Get the currently selected tab.
    /// </summary>
    /// <returns></returns>
    public ValueTask<string> GetSelectedAsync() =>
        JsComponent.InvokeAsync<string>("getSelected", IonElement);

    /// <summary>
    /// Get a specific tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    public async ValueTask GetTabAsync(string tab)
    {
        var obj = await JsComponent.InvokeAsync<JsonObject>("getTab", IonElement, tab);
    }

    /// <summary>
    /// Select a tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> SelectAsync(string tab) =>
        JsComponent.InvokeAsync<bool>("select", IonElement, tab);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionTabsDidChange", _ionTabsDidChangeReference),
            IonEvent.Set("ionTabsWillChange", _ionTabsWillChangeReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionTabsDidChangeReference.Dispose();
        _ionTabsWillChangeReference.Dispose();
        await base.DisposeAsync();
    }
}