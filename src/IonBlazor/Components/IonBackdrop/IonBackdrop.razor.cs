namespace IonBlazor.Components;

public partial class IonBackdrop : IonComponent, IIonComponent
{
    private ElementReference _self;
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionBackdropTapReference = null!;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// If <b>true</b>, the backdrop will stop propagation on tap.
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; set; } = true;

    /// <summary>
    /// If <b>true</b>, the backdrop can be clicked and will emit the ionBackdropTap event.
    /// </summary>
    [Parameter]
    public bool Tappable { get; set; } = true;

    /// <summary>
    /// If <b>true</b>, the backdrop will be visible.
    /// </summary>
    [Parameter]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Emitted when the backdrop is tapped.
    /// </summary>
    [Parameter]
    public EventCallback IonBackdropTap { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionBackdropTapReference = IonicEventCallback<JsonObject?>.Create(
            async _ =>
            {
                await IonBackdropTap.InvokeAsync(new IonBackdropIonChangeEventArgs { Sender = this });
            });

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionBackdropTap", _ionBackdropTapReference));
    }

}

public class IonBackdropIonChangeEventArgs : EventArgs
{
    public IonBackdrop Sender { get; internal init; } = null!;
}