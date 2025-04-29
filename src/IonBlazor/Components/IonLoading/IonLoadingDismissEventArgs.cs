namespace IonBlazor.Components;

public sealed record IonLoadingDismissEventArgs
{
    /// <summary>
    /// The <see cref="IonLoading"/> that triggered the event. This will be null if created by <see cref="IonLoadingController"/>
    /// </summary>
    public IonLoading? Sender { get; internal init; } = null!;

    public string? Role { get; internal init; }

    public object? Data { get; internal init; }

    /// <summary>
    /// The HTML attributes that were passed to the loader, used by <see cref="IonLoadingController"/>
    /// </summary>
    public Dictionary<string, string>? HtmlAttributes { get; internal init; }
}