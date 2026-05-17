namespace IonBlazor.Components;

public sealed partial class IonBackdrop : IonComponent, IIonComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionBackdropTapReference;

    /// <summary>
    /// If <b>true</b>, the backdrop will stop propagation on tap.
    /// </summary>
    [Parameter]
    public bool StopPropagation { get; init; } = true;

    /// <summary>
    /// If <b>true</b>, the backdrop can be clicked and will emit the ionBackdropTap event.
    /// </summary>
    [Parameter]
    public bool Tappable { get; init; } = true;

    /// <summary>
    /// If <b>true</b>, the backdrop will be visible.
    /// </summary>
    [Parameter]
    public bool Visible { get; init; } = true;

    /// <summary>
    /// Emitted when the backdrop is tapped.
    /// </summary>
    [Parameter]
    public EventCallback<IonBackdrop> IonBackdropTap { get; set; }

    public IonBackdrop()
    {
        _ionBackdropTapReference = IonicEventCallback.Create(async () => await IonBackdropTap.InvokeAsync(this));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionBackdropTap", _ionBackdropTapReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionBackdropTapReference.Dispose();
        await base.DisposeAsync();
    }
}