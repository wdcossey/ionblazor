namespace IonBlazor.Components;

public sealed partial class IonItemOptions : IonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionSwipeReference;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// The side the option button should be on. Possible values: <see cref="IonItemOptionsSide.Start"/> and
    /// <see cref="IonItemOptionsSide.End"/>.
    /// If you have multiple <see cref="IonItemOptions"/>, a side must be provided for each.
    /// </summary>
    [Parameter, EditorRequired]
#if NET7_0_OR_GREATER
    public required string Side { get; init; } = IonItemOptionsSide.End;
#else
    public string Side { get; init; } = IonItemOptionsSide.End;
#endif

    /// <summary>
    /// Emitted when the item has been fully swiped.
    /// </summary>
    [Parameter] public EventCallback<IonItemOptionsSwipeEventArgs> IonSwipe { get; set; }

    public IonItemOptions()
    {
        _ionSwipeReference = IonicEventCallback<JsonObject?>.Create(async args =>
            {
                await IonSwipe.InvokeAsync(new IonItemOptionsSwipeEventArgs
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

    public override async ValueTask DisposeAsync()
    {
        _ionSwipeReference.Dispose();
        await base.DisposeAsync();
    }
}