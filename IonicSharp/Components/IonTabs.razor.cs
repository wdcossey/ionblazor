namespace IonicSharp.Components;

public partial class IonTabs: IonComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionTabsDidChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionTabsWillChangeReference;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Emitted when the navigation has finished transitioning to a new component.
    /// </summary>
    [Parameter] public EventCallback<IonTabsDidChangeEventArgs> IonTabsDidChange { get; set; }
    
    /// <summary>
    /// Emitted when the navigation is about to transition to a new component.
    /// </summary>
    [Parameter] public EventCallback<IonTabsWillChangeEventArgs> IonTabsWillChange { get; set; }
    
    public IonTabs()
    {
        _ionTabsDidChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            var tab = args?["detail"]?["tab"]?.GetValue<string>();
            await IonTabsDidChange.InvokeAsync(new IonTabsDidChangeEventArgs() { Tab = tab });
        }));
        
        _ionTabsWillChangeReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new (async args =>
        {
            var tab = args?["detail"]?["tab"]?.GetValue<string>();
            await IonTabsWillChange.InvokeAsync(new IonTabsWillChangeEventArgs() { Tab = tab } );
        }));
    }
    
    /// <summary>
    /// Get the currently selected tab.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task GetSelectedAsync()
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Get a specific tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task GetTabAsync(string tab)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Select a tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task SelectAsync(string tab)
    {
        throw new NotImplementedException();
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "ionTabsDidChange", Ref = _ionTabsDidChangeReference},
            new { Event = "ionTabsWillChange", Ref = _ionTabsWillChangeReference}
        }, _self);
    }
}

public class IonTabsDidChangeEventArgs : EventArgs
{
    public string? Tab { get; internal set; }
}

public class IonTabsWillChangeEventArgs : EventArgs
{
    public string? Tab { get; internal set; }
}