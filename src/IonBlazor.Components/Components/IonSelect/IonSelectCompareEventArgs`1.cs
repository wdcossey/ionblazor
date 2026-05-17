namespace IonBlazor.Components;

public sealed record IonSelectCompareEventArgs<TItem> where TItem : notnull
{
    public delegate bool ComparisonDelegate(int[] indexes, KeyValuePair<int, TItem> item);

    public IonSelect<TItem>? Sender { get; internal init; }

    public ComparisonDelegate Compare { get; set; } = (ints, item) => ints.Contains(item.Key);
}