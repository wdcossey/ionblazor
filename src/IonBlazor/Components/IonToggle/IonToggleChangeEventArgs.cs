namespace IonBlazor.Components;

public sealed record IonToggleChangeEventArgs
{
    public IonToggle? Sender { get; internal init; }

    public bool? Checked { get; internal init; }

    /// <summary>
    /// The value of the <see cref="IonToggle"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that. <p/>
    /// The value of a <see cref="IonToggle"/> is analogous to the value of a &lt;input type="checkbox"&gt;,
    /// it's only used when the <see cref="IonToggle"/> participates in a native &lt;form&gt;.
    /// </summary>
    public string? Value { get; internal init; }
}