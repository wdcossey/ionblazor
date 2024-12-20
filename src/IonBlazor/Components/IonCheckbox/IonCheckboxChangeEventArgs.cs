namespace IonBlazor.Components;

public sealed record IonCheckboxChangeEventArgs
{
    public IonCheckbox? Sender { get; internal init; }

    public bool? Checked { get; internal init; }

    /// <summary>
    /// The value of the <see cref="IonCheckbox"/> does not mean if it's checked or not,
    /// use the <see cref="Checked"/> property for that.
    /// <br/><br/>
    /// The value of a <see cref="IonCheckbox"/> is analogous to the value of an &lt;input type="checkbox"&gt;,
    /// it's only used when the checkbox participates in a native &lt;form&gt;.
    /// </summary>
    public string? Value { get; internal init; }
}