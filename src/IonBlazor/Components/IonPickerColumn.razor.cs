namespace IonBlazor.Components;

public partial class IonPickerColumn : IonComponent, IIonModeComponent, IIonColorComponent, IIonContentComponent
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;

    public override ElementReference IonElement => _self;

    /// <summary>
    /// The parent <see cref="IonPicker"/>.
    /// </summary>
    [CascadingParameter(Name = "ion-picker")] public IonPicker? Parent { get; init; }

    /// <inheritdoc/>
    [Parameter] public RenderFragment? ChildContent { get; set; }

    /// <inheritdoc/>
    [Parameter] public string? Color { get; set; }

    /// <summary>
    /// If <b>true</b>, the user cannot interact with the picker.
    /// </summary>
    [Parameter] public bool Disabled { get; set; } = false;

    /// <inheritdoc/>
    [Parameter] public string? Mode { get; set; }

    /// <summary>
    /// The selected option in the picker.
    /// </summary>
    [Parameter] public string? Value { get; set; }

    /// <summary>
    /// Emitted when the value has changed.
    /// </summary>
    [Parameter]
    public EventCallback<IonPickerColumnIonChangeEventArgs> IonChange { get; set; }

    /// <summary>
    /// Sets focus on the scrollable container within the picker column.
    /// Use this method instead of the global pickerColumn.focus().
    /// </summary>
    public async ValueTask SetFocusAsync() => await JsComponent.InvokeAsync<string>("setFocus", _self);

    public IonPickerColumn()
    {
        _ionChangeReference = IonicEventCallback<JsonObject?>.Create(async args =>
        {
            var value = args?["detail"]?["value"]?.GetValue<string>();
            Value = value;

            await IonChange.InvokeAsync(
                new IonPickerColumnIonChangeEventArgs
                {
                    Sender = this,
                    Value = value,
                });
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

public class IonPickerColumnIonChangeEventArgs : EventArgs
{
    public IonPickerColumn Sender { get; internal set; } = null!;

    public string? Value { get; internal set; }
}