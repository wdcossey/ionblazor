namespace IonicSharp.Components;

public partial class IonItemOptions : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionSwipeReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The side the option button should be on. Possible values: <see cref="IonItemOptionsSide.Start"/> and
    /// <see cref="IonItemOptionsSide.End"/>.
    /// If you have multiple <see cref="IonItemOptions"/>, a side must be provided for each.
    /// </summary>
    [Parameter]
    public string Side { get; set; } = IonItemOptionsSide.End;
    
    /// <summary>
    /// Emitted when the item has been fully swiped.
    /// </summary>
    [Parameter] public EventCallback<IonSwipeEventArgs> IonSwipe { get; set; }
    
    public IonItemOptions()
    {
        _ionSwipeReference = IonicEventCallback<JsonObject?>.Create(async args =>
            {
                await IonSwipe.InvokeAsync(new IonSwipeEventArgs
                {
                    Side = args?["detail"]?["side"]?.GetValue<string>()
                });
            });
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionSwipe", _ionSwipeReference));
    }
}

public static class IonItemOptionsSide
{
    public const string End = "end";
    public const string Start = "start";
}

public class IonSwipeEventArgs : EventArgs
{
    public string? Side { get; internal set; }
}