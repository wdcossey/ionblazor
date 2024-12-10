namespace IonBlazor.Components;

public partial class IonSplitPane : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionSplitPaneVisibleReference = null!;

    /// <inheritdoc />
    public override ElementReference IonElement => _self;

    /// <inheritdoc />
    [Parameter] public RenderFragment? ChildContent { get; set; }

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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionSplitPaneVisibleReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                await IonSplitPaneVisible.InvokeAsync(new IonSplitPaneVisibleEventArgs { Sender = this });
            });

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionSplitPaneVisible", _ionSplitPaneVisibleReference));
    }
}