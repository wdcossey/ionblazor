namespace IonBlazor.Components;

public sealed partial class IonImg : IonComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionErrorReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionImgDidLoadReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionImgWillLoadReference;

    /// <summary>
    /// This attribute defines the alternative text describing the image.
    /// Users will see this text displayed if the image URL is wrong,
    /// the image is not in one of the supported formats, or if the image is not yet downloaded.
    /// </summary>
    [Parameter] public string? Alt { get; set; }

    /// <summary>
    /// The image URL. This attribute is mandatory for the &lt;img&gt; element.
    /// </summary>
    [Parameter, EditorRequired] public string? Src { get; init; }

    /// <summary>
    /// Emitted when the img fails to load.
    /// </summary>
    [Parameter] public EventCallback<IonImg> IonError { get; set; }

    /// <summary>
    /// Emitted when the image has finished loading.
    /// </summary>
    [Parameter] public EventCallback<IonImg> IonImgDidLoad { get; set; }

    /// <summary>
    /// Emitted when the img src has been set.
    /// </summary>
    [Parameter] public EventCallback<IonImg> IonImgWillLoad { get; set; }

    public IonImg()
    {
        _ionErrorReference = IonicEventCallback.Create(async () => await IonError.InvokeAsync(this));
        _ionImgDidLoadReference = IonicEventCallback.Create(async () => await IonImgDidLoad.InvokeAsync(this));
        _ionImgWillLoadReference = IonicEventCallback.Create(async () => await IonImgWillLoad.InvokeAsync(this));
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionError", _ionErrorReference),
            IonEvent.Set("ionImgDidLoad", _ionImgDidLoadReference),
            IonEvent.Set("ionImgWillLoad", _ionImgWillLoadReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionErrorReference.Dispose();
        _ionImgDidLoadReference.Dispose();
        _ionImgWillLoadReference.Dispose();
        await base.DisposeAsync();
    }
}