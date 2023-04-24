using System.Text.Json;
using System.Text.Json.Nodes;

namespace IonicTest.Components;

public partial class IonAccordionGroup : IonControl
{
    private ElementReference _self;
    private DotNetObjectReference<AccordionGroupEventHelper<JsonObject?>> _ionChangeObjectReference = null!;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// If true, all accordions inside of the accordion group will animate when expanding or collapsing.
    /// </summary>
    [Parameter] public bool? Animated { get; set; }
    
    /// <summary>
    /// If true, the accordion group cannot be interacted with.
    /// </summary>
    [Parameter] public bool? Disabled { get; set; }

    /// <summary>
    /// Describes the expansion behavior for each accordion.
    /// Possible values are <see cref="AccordionExpand.Compact"/> and <see cref="AccordionExpand.Inset"/>. Defaults to <see cref="AccordionExpand.Compact"/>.
    /// </summary>
    [Parameter] public AccordionExpand? Expand { get; set; }
    
    /// <summary>
    /// The mode determines which platform styles to use.
    /// </summary>
    [Parameter] public IonicStyleMode? Mode { get; set; }

    /// <summary>
    /// If true, the accordion group can have multiple accordion components expanded at the same time.
    /// </summary>
    [Parameter] public bool? Multiple { get; set; }

    /// <summary>
    /// If true, the accordion group cannot be interacted with, but does not alter the opacity.
    /// </summary>
    [Parameter] public bool? Readonly { get; set; }

    /// <summary>
    /// The value of the accordion group. This controls which accordions are expanded.
    /// This should be an array of strings only when multiple="true"
    /// </summary>
    [Parameter]
    public string[]? Value { get; set; }

    public async Task<IonAccordionGroup> SetValue(string[]? value)
    {
        await JsRuntime.InvokeVoidAsync("setAccordionGroupValue", _self, value ?? Array.Empty<string>());
        Value = value;
        return this;
    }
    
    /// <summary>
    /// Emitted when the value property has changed as a result of a user action such as a click.
    /// This event will not emit when programmatically setting the value property.
    /// </summary>
    [Parameter] public EventCallback<AccordionGroupIonChangeEventArgs> OnChange { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _ionChangeObjectReference = DotNetObjectReference.Create(new AccordionGroupEventHelper<JsonObject?>(
            async args =>
            {
                //if (args?["detail"]?["value"]?.AsObject().)
                var value = args?["detail"]?["value"];
                Value = value is null ? Array.Empty<string>() : value is JsonArray ? value.Deserialize<string[]>() : new [] { value.GetValue<string>() };

                await OnChange.InvokeAsync(new AccordionGroupIonChangeEventArgs() { Value = Value });
            }));

        //Multiple is not true ? result?.FirstOrDefault() : result;
        await SetValue(Value);
        await JsRuntime.InvokeVoidAsync("attachIonEventListener", "ionChange", _self, _ionChangeObjectReference);
    }

    public class AccordionGroupEventHelper<TArgs>
    {
        private readonly Func<TArgs, Task> _callback;

        public AccordionGroupEventHelper(Func<TArgs, Task> callback) => _callback = callback;

        [JSInvokable]
        public Task OnCallbackEvent(TArgs args) => _callback(args);
    }
}

public class AccordionGroupIonChangeEventArgs : EventArgs
{
    public string[]? Value { get; internal set; }
}

public enum AccordionExpand
{
    Compact,
    Inset
}