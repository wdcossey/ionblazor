using IonicSharp.Extensions;

namespace IonicSharp.Components;

public partial class IonTabs : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionTabsDidChangeReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionTabsWillChangeReference;
    
    private readonly Lazy<ValueTask<IJSObjectReference>> _lazyIonComponentJs;
    private readonly Func<ValueTask<string>> _getSelectedJsWrapper;
    private readonly Func<string,ValueTask<JsonObject>> _getTabJsWrapper;
    private readonly Func<string, ValueTask<bool>> _selectJsWrapper;

    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

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
        _lazyIonComponentJs = new Lazy<ValueTask<IJSObjectReference>>(() => JsRuntime.ImportAsync("ionTabs"));
        _getSelectedJsWrapper = async () => await (await _lazyIonComponentJs.Value).InvokeAsync<string>("getSelected", _self);
        _getTabJsWrapper = async tab => await (await _lazyIonComponentJs.Value).InvokeAsync<JsonObject>("getTab", _self, tab);
        _selectJsWrapper = async tab => await (await _lazyIonComponentJs.Value).InvokeAsync<bool>("select", _self, tab);
        
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
    public ValueTask<string> GetSelectedAsync() => _getSelectedJsWrapper();

    /// <summary>
    /// Get a specific tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    public async ValueTask GetTabAsync(string tab)
    {
        var obj = await _getTabJsWrapper(tab);
    }

    /// <summary>
    /// Select a tab by the value of its tab property or an element reference.
    /// </summary>
    /// <returns></returns>
    public ValueTask<bool> SelectAsync(string tab) => _selectJsWrapper(tab);

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await this.AttachIonListenersAsync(_self, new[]
        {
            IonEvent.Set("ionTabsDidChange" , _ionTabsDidChangeReference ),
            IonEvent.Set("ionTabsWillChange", _ionTabsWillChangeReference)
        });
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