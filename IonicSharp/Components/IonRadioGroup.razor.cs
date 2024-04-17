namespace IonicSharp.Components;

public partial class IonRadioGroup : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// If <b>true</b>, the radios can be deselected.
    /// </summary>
    [Parameter]
    public bool AllowEmptySelection { get; set; }

    /// <summary>
    /// The name of the control, which is submitted with the form data.
    /// </summary>
    [Parameter]
    public string? Name { get; set; }

    /// <summary>
    /// the value of the radio group.
    /// </summary>
    [Parameter]
    public string? Value { get; set; }

    /// <summary>
    /// Emitted when the value has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonRadioGroupIonChangeEventArgs> IonChange { get; set; }

    public IonRadioGroup()
    {

        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            var isTrusted = args?["detail"]?["event"]?["isTrusted"]?.GetValue<bool>();
            Value = value;

            await IonChange.InvokeAsync(new IonRadioGroupIonChangeEventArgs
                { Sender = this, Value = value, IsTrusted = isTrusted });
        });

    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(_self, IonEvent.Set("ionChange", _ionChangeReference));
    }
}

public class IonRadioGroupIonChangeEventArgs : EventArgs
{
    public IonRadioGroup Sender { get; internal set; } = null!;
    
    public string? Value { get; internal set; }
    
    public bool? IsTrusted { get; internal set; }
}