namespace IonicSharp.Components;

public partial class IonTab : IonComponent, IIonContentComponent
{
    private ElementReference _self;
    
    public override ElementReference IonElement => _self;
    
    /// <inheritdoc/>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// The component to display inside of the tab.
    /// </summary>
    [Parameter]
    public string? Component { get; set; }

    /// <summary>
    /// A tab id must be provided for each <see cref="IonTab"/>.
    /// It's used internally to reference the selected tab or by the router to switch between them.
    /// </summary>
    [Parameter]
    public string? Tab { get; set; }

    /// <summary>
    /// Set the active component for the tab
    /// </summary>
    public async Task SetActiveAsync() => 
        await JsRuntime.InvokeVoidAsync("IonicSharp.IonTab.setActive", _self);
}