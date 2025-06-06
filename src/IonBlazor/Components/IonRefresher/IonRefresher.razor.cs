﻿namespace IonBlazor.Components;

public sealed partial class IonRefresher: IonContentComponent
{
    private readonly DotNetObjectReference<IonicEventCallback> _ionPullReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionRefreshReference;
    private readonly DotNetObjectReference<IonicEventCallback> _ionStartReference;

    protected override string JsImportName => nameof(IonRefresher);

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
        _ionPullReference = IonicEventCallback.Create(async () => await IonPull.InvokeAsync(new IonRefresherIonPullEventArgs { Sender  = this }));

        _ionRefreshReference = IonicEventCallback.Create(async () => await IonRefresh.InvokeAsync(new IonRefresherIonRefreshEventArgs { Sender  = this }));

        _ionStartReference = IonicEventCallback.Create(async () => await IonStart.InvokeAsync(new IonRefresherIonStartEventArgs { Sender  = this }));
    }

    /// <summary>
    /// Changes the refresher's state from refreshing to cancelling.
    /// </summary>
    public async ValueTask CancelAsync()
    {
        await JsComponent.InvokeVoidAsync("cancel", IonElement);
    }

    /// <summary>
    /// Call complete() when your async operation has completed.
    /// For example, the refreshing state is while the app is performing an asynchronous operation,
    /// such as receiving more data from an AJAX request.
    /// Once the data has been received, you then call this method to signify that the refreshing has completed
    /// and to close the refresher. This method also changes the refresher's state from refreshing to completing.
    /// </summary>
    public async ValueTask CompleteAsync()
    {
        await JsComponent.InvokeVoidAsync("complete", IonElement);
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
    public async ValueTask<int> GetProgressAsync()
    {
        return await JsComponent.InvokeAsync<int>("getProgress", IonElement);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        await this.AttachIonListenersAsync(
            IonElement,
            IonEvent.Set("ionPull", _ionPullReference),
            IonEvent.Set("ionRefresh", _ionRefreshReference),
            IonEvent.Set("ionStart", _ionStartReference)
        );
    }

    public override async ValueTask DisposeAsync()
    {
        _ionPullReference.Dispose();
        _ionRefreshReference.Dispose();
        _ionStartReference.Dispose();
        await base.DisposeAsync();
    }
}