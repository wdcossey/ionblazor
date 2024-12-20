using System.Collections.ObjectModel;

namespace IonBlazor.Components;

public sealed record IonSelectChangeEventArgs<TValue> where TValue : notnull
{
    [JsonIgnore]
    public IonSelect<TValue>? Sender { get; internal init; }

    [JsonPropertyName("value")]
    public IonSelectValue<TValue> Value { get; internal init; }
}

public sealed class IonSelectValue<TValue> : ReadOnlyCollection<TValue> where TValue : notnull
{
    internal IonSelectValue(IList<TValue> list) : base(list) { }

    public override string ToString() => string.Join(",", this);
}