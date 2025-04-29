namespace IonBlazor.Components;

public sealed record AlertButtonHandlerEventArgs
{
    /// <summary>
    /// The <see cref="IonAlert" /> that this event occurred on.
    /// </summary>
    public IonAlert? Sender { get; internal init; }

    /// <summary>
    /// The index of the button that was clicked.
    /// </summary>
    public int? Index { get; internal init; }

    /// <summary>
    /// The <see cref="IAlertButton" /> that was clicked.
    /// </summary>
    public IAlertButton? Button { get; internal init; }
}