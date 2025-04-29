namespace IonBlazor.Components;

public sealed partial class IonGrid : IonContentComponent
{
    /// <summary>
    /// If <b>true</b>, the grid will have a fixed width based on the screen size.
    /// </summary>
    [Parameter]
    public bool? Fixed { get; set; }
}