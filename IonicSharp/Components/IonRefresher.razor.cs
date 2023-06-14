namespace IonicSharp.Components;

public partial class IonRefresher: IonSlotControl
{
    private ElementReference _self;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionPullReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionRefreshReference;
    private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>>? _ionStartReference;
    
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Time it takes to close the refresher.
    /// Does not apply when the refresher content uses a spinner, enabling the native refresher
    /// </summary>
    [Parameter] public string CloseDuration { get; set; } = "280ms";
    
    /// <summary>
    /// If <b>true</b>, the refresher will be hidden.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }
    
    /// <summary>
    /// How much to multiply the pull speed by. To slow the pull animation down, pass a number less than 1.
    /// To speed up the pull, pass a number greater than 1.
    /// The default value is 1 which is equal to the speed of the cursor.
    /// If a negative value is passed in, the factor will be 1 instead.
    /// <br/><br/>
    /// For example: If the value passed is 1.2 and the content is dragged by 10 pixels,
    /// instead of 10 pixels the content will be pulled by 12 pixels (an increase of 20 percent).
    /// If the value passed is 0.8, the dragged amount will be 8 pixels, less than the amount the cursor has moved.
    /// <br/><br/>
    /// Does not apply when the refresher content uses a spinner, enabling the native refresher.
    /// </summary>
    [Parameter] public double? PullFactor { get; set; } = 1d;
    
    /// <summary>
    /// The maximum distance of the pull until the refresher will automatically go into the refreshing state.
    /// Defaults to the result of pullMin + 60. Does not apply when the refresher content uses a spinner,
    /// enabling the native refresher.
    /// </summary>
    [Parameter] public int? PullMax { get; set; }
    
    /// <summary>
    /// The minimum distance the user must pull down until the refresher will go into the refreshing state.
    /// Does not apply when the refresher content uses a spinner, enabling the native refresher.
    /// </summary>
    [Parameter] public int? PullMin { get; set; } = 60;
    
    /// <summary>
    /// Time it takes the refresher to snap back to the refreshing state.
    /// Does not apply when the refresher content uses a spinner, enabling the native refresher.
    /// </summary>
    [Parameter] public string SnapbackDuration { get; set; } = "280ms";
    
    /// <summary>
    /// Emitted while the user is pulling down the content and exposing the refresher.
    /// </summary>
    [Parameter] public EventCallback<IonRefresherIonPullEventArgs> IonPull { get; set; }
    
    /// <summary>
    /// Emitted when the user lets go of the content and has pulled down further than the pullMin or
    /// pulls the content down and exceeds the pullMax.
    /// Updates the refresher state to refreshing.
    /// The complete() method should be called when the async operation has completed.
    /// </summary>
    [Parameter] public EventCallback<IonRefresherIonRefreshEventArgs> IonRefresh { get; set; }
    
    /// <summary>
    /// Emitted when the user begins to start pulling down.
    /// </summary>
    [Parameter] public EventCallback<IonRefresherIonStartEventArgs> IonStart { get; set; }

    public IonRefresher()
    {

        _ionPullReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            
            await IonPull.InvokeAsync(new IonRefresherIonPullEventArgs() { Sender  = this });
        }));

        _ionRefreshReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            
            await IonRefresh.InvokeAsync(new IonRefresherIonRefreshEventArgs() { Sender  = this });
        }));

        _ionStartReference = DotNetObjectReference.Create<IonicEventCallback<JsonObject?>>(new(async args =>
        {
            
            await IonStart.InvokeAsync(new IonRefresherIonStartEventArgs() { Sender  = this });
        }));
    }
    
    /// <summary>
    /// Changes the refresher's state from refreshing to cancelling.
    /// </summary>
    public async Task CancelAsync()
    {
        await JsRuntime.InvokeVoidAsync("cancelIonRefresher", _self);
    }
    
    /// <summary>
    /// Call complete() when your async operation has completed.
    /// For example, the refreshing state is while the app is performing an asynchronous operation,
    /// such as receiving more data from an AJAX request.
    /// Once the data has been received, you then call this method to signify that the refreshing has completed
    /// and to close the refresher. This method also changes the refresher's state from refreshing to completing.
    /// </summary>
    public async Task CompleteAsync()
    {
        await JsRuntime.InvokeVoidAsync("completeIonRefresher", _self);
    }
    
    /// <summary>
    /// A number representing how far down the user has pulled.
    /// The number 0 represents the user hasn't pulled down at all.
    /// The number 1, and anything greater than 1, represents that the user has pulled far enough down that when they
    /// let go then the refresh will happen.
    /// If they let go and the number is less than 1, then the refresh will not happen, and the content will return
    /// to it's original position.
    /// </summary>
    /// <returns></returns>
    public async Task<int> GetProgressAsync()
    {
        return await JsRuntime.InvokeAsync<int>("getProgressIonRefresher", _self);
    }
    
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;
        
        await JsRuntime.InvokeVoidAsync("attachIonEventListeners", new []
        {
            new { Event = "ionPull", Ref = _ionPullReference},
            new { Event = "ionRefresh", Ref = _ionRefreshReference},
            new { Event = "ionStart", Ref = _ionStartReference}
        }, _self);
    }
}

public class IonRefresherIonPullEventArgs : EventArgs
{
    public IonRefresher Sender { get; internal set; } = null!;
}

public class IonRefresherIonRefreshEventArgs : EventArgs
{
    public IonRefresher Sender { get; internal set; } = null!;
}

public class IonRefresherIonStartEventArgs : EventArgs
{
    public IonRefresher Sender { get; internal set; } = null!;
}