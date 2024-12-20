namespace IonBlazor.Components;

public sealed partial class IonTab : IonContentComponent
{
    private ElementReference _self;

    /// <inheritdoc/>
    public override ElementReference IonElement => _self;

    [CascadingParameter(Name = nameof(Parent))] public IIonComponent? Parent { get; init; }

    /// <summary>
    /// The component to display inside of the tab.
    /// </summary>
    [Parameter] public string? Component { get; set; }

    /// <summary>
    /// A tab id must be provided for each <see cref="IonTab"/>.
    /// It's used internally to reference the selected tab or by the router to switch between them.
    /// </summary>
    [Parameter, EditorRequired] public string Tab { get; init; } = null!;

    /// <summary>
    /// Set the active component for the tab
    /// </summary>
    public async Task SetActiveAsync() =>
        await JsComponent.InvokeVoidAsync("setActive", _self);
}