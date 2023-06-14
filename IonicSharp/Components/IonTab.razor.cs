namespace IonicSharp.Components;

public partial class IonTab: IonControl
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The component to display inside of the tab.
    /// </summary>
    [Parameter] public string? Component { get; set; }

    /// <summary>
    /// A tab id must be provided for each ion-tab.
    /// It's used internally to reference the selected tab or by the router to switch between them.
    /// </summary>
    [Parameter] public string? Tab { get; set; }

    public Task SetActiveAsync()
    {
        throw new NotImplementedException();
    }
}