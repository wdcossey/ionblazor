namespace IonBlazor.Components;

public sealed record IonLoadingPresentEventArgs
{
    /// <summary>
    /// The <see cref="IIonLoading"/> that triggered the event.
    /// </summary>
    public IIonLoading? Sender { get; internal init; }

    /// <summary>
    /// The HTML attributes that were passed to the loader, used by <see cref="IonLoadingController"/>
    /// </summary>
    public Dictionary<string, string>? HtmlAttributes { get; internal init; }
}