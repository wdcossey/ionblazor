namespace IonBlazor.Components;

public sealed partial class IonSplitPane : IonContentComponent
{
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionSplitPaneVisibleReference;

    /// <summary>
    /// The <b>id</b> of the main content. When using a router this is typically <b>ion-router-outlet</b>.
    /// When not using a router, this is typically your main view's <b>ion-content</b>.
    /// This is not the id of the <b>ion-content</b> inside of your <b>ion-menu</b>.
    /// </summary>
    [Parameter] public string? ContentId { get; set; }

    /// <summary>
    /// If <b>true</b>, the split pane will be hidden.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }

    /// <summary>
    /// When the split-pane should be shown.
    /// Can be a CSS media query expression, or a shortcut expression. Can also be a boolean expression.
    /// </summary>
    [Parameter] public string? When { get; set; }

    /// <summary>
    /// Emitted when the backdrop is tapped.
    /// </summary>
    [Parameter]
    public EventCallback IonSplitPaneVisible { get; set; }

    public IonSplitPane()
    {
        _ionSplitPaneVisibleReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                await IonSplitPaneVisible.InvokeAsync(new IonSplitPaneVisibleEventArgs { Sender = this });
            });
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionSplitPaneVisible", _ionSplitPaneVisibleReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionSplitPaneVisibleReference.Dispose();
        await base.DisposeAsync();
    }
}