using Microsoft.Extensions.DependencyInjection;

namespace IonBlazor.Components;

public sealed partial class IonAccordionGroup : IonContentComponent, IIonModeComponent
{
    private DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeObjectReference = null!;

    protected override string JsImportName => nameof(IonAccordionGroup);

/*#if NET8_0_OR_GREATER
    [Inject(Key = nameof(IonAccordionGroup))]
    internal override Task<IJSObjectReference>? JsComponent { get; init; }
#else
    protected override string JsImportName => nameof(IonAccordionGroup);
#endif*/

    /// <summary>
    /// If true, all accordions inside of the accordion group will animate when expanding or collapsing.
    /// </summary>
    [Parameter]
    public bool? Animated { get; init; }

    /// <summary>
    /// If true, the accordion group cannot be interacted with.
    /// </summary>
    [Parameter]
    public bool? Disabled { get; init; }

    /// <summary>
    /// Describes the expansion behavior for each accordion.
    /// Possible values are <see cref="IonAccordionGroupExpand.Compact"/> and <see cref="IonAccordionGroupExpand.Inset"/>. Defaults to <see cref="IonAccordionGroupExpand.Compact"/>.
    /// </summary>
    [Parameter]
    public string? Expand { get; init; } = IonAccordionGroupExpand.Default;

    /// <inheritdoc/>
    [Parameter]
    public string? Mode { get; set; } = IonMode.Default;

    /// <summary>
    /// If true, the accordion group can have multiple accordion components expanded at the same time.
    /// </summary>
    [Parameter]
    public bool? Multiple { get; init; }

    /// <summary>
    /// If true, the accordion group cannot be interacted with, but does not alter the opacity.
    /// </summary>
    [Parameter]
    public bool? Readonly { get; init; }

    /// <summary>
    /// The value of the accordion group. This controls which accordions are expanded.
    /// This should be an array of strings only when multiple="true"
    /// </summary>
    public ValueTask<IEnumerable<string>> Value => GetValueAsync();

    /// <summary>
    /// Emitted when the value property has changed as a result of a user action such as a click.
    /// This event will not emit when programmatically setting the value property.
    /// </summary>
    [Parameter]
    public EventCallback<IonAccordionGroupIonChangeEventArgs> IonChange { get; init; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionChangeObjectReference = IonicEventCallback<JsonObject?>.Create(
            async args =>
            {
                JsonNode? tmpValue = args?["detail"]?["value"];
                var value = tmpValue switch
                {
                    null => [],
                    JsonArray => tmpValue.Deserialize<string[]>(),
                    _ => [ tmpValue.GetValue<string>() ]
                };

                await IonChange.InvokeAsync(new IonAccordionGroupIonChangeEventArgs { Sender = this, Value = value });
            });

        //Multiple is not true ? result?.FirstOrDefault() : result;
        //await SetValueAsync(Value);

        await this.AttachIonListenersAsync(IonElement, IonEvent.Set("ionChange", _ionChangeObjectReference));
    }

    public override async ValueTask DisposeAsync()
    {
        _ionChangeObjectReference.Dispose();
        await base.DisposeAsync();
    }

    public async ValueTask SetValueAsync(params string[]? value)
    {
        object? actualValue;
        if (value is null || value.Length <= 0)
            actualValue = null;
        else if (value.Length == 1)
            actualValue = value.First();
        else
            actualValue = value;

        await JsComponent.InvokeVoidAsync("setValue", IonElement, actualValue);
    }

    internal async ValueTask<IEnumerable<string>> GetValueAsync()
    {
        return await JsComponent.InvokeAsync<IEnumerable<string>>("getValue", IonElement);
    }

}