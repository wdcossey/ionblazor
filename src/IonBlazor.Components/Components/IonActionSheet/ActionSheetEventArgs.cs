namespace IonBlazor.Components;

public record ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public IonActionSheet<TData>? Sender { get; internal init; }
}