namespace IonicSharp.Components;

public partial class IonContent : IonControl
{
    private ElementReference _self;
    //private DotNetObjectReference<AccordionGroupEventHelper<JsonObject?>> _ionChangeObjectReference = null!;

    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// The color to use from your application's color palette.
    /// Default options are:
    /// <see cref="IonColor.Primary"/>, <see cref="IonColor.Secondary"/>,
    /// <see cref="IonColor.Tertiary"/>, <see cref="IonColor.Success"/>,
    /// <see cref="IonColor.Warning"/>, <see cref="IonColor.Danger"/>,
    /// <see cref="IonColor.Light"/>, <see cref="IonColor.Medium"/>,
    /// and <see cref="IonColor.Dark"/>. <br/>
    /// For more information on colors, see theming.
    /// </summary>
    [Parameter] public string? Color { get; set; }
    
    /// <summary>
    /// If true and the content does not cause an overflow scroll, the scroll interaction will cause a bounce.
    /// If the content exceeds the bounds of ionContent, nothing will change.
    /// Note, this does not disable the system bounce on iOS. That is an OS level setting.
    /// </summary>
    [Parameter] public bool? ForceOverscroll { get; set; }
    
    /// <summary>
    /// If true, the content will scroll behind the headers and footers.
    /// This effect can easily be seen by setting the toolbar to transparent.
    /// </summary>
    [Parameter] public bool? Fullscreen { get; set; }
    
    /// <summary>
    /// Because of performance reasons, ionScroll events are disabled by default,
    /// in order to enable them and start listening from (ionScroll), set this property to true.
    /// </summary>
    [Parameter] public bool? ScrollEvents { get; set; }
    
    /// <summary>
    /// If you want to enable the content scrolling in the X axis, set this property to true.
    /// </summary>
    [Parameter] public bool? ScrollX { get; set; }
    
    /// <summary>
    /// If you want to disable the content scrolling in the Y axis, set this property to false.
    /// </summary>
    [Parameter] public bool? ScrollY { get; set; }

    /// <summary>
    /// Get the element where the actual scrolling takes place.
    /// This element can be used to subscribe to scroll events or manually modify scrollTop.
    /// However, it's recommended to use the API provided by ion-content: <br/>
    /// i.e. Using ionScroll, ionScrollStart, ionScrollEnd for scrolling events and scrollToPoint()
    /// to scroll the content into a certain point.
    /// </summary>
    public object? GetScrollElement()
    {
        throw new NotImplementedException();
        //JsRuntime.InvokeAsync<IJSObjectReference>()
    }

    /// <summary>
    /// Scroll by a specified X/Y distance in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public void ScrollByPoint(int x, int y, int duration)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Scroll to the bottom of the component.
    /// </summary>
    /// <param name="duration"></param>
    public void ScrollToBottom(int duration)
    {
        throw new NotImplementedException();
    }
    
    /// <summary>
    /// Scroll to a specified X/Y location in the component.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="duration"></param>
    public void ScrollToPoint(int? x = null, int? y = null, int? duration = null)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Scroll to the top of the component.
    /// </summary>
    /// <param name="duration"></param>
    public void ScrollToTop(int duration)
    {
        throw new NotImplementedException();
    }
}