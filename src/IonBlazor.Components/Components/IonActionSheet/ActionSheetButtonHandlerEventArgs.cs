namespace IonBlazor.Components;

public sealed record ActionSheetButtonHandlerEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public int? Index { get; internal init; }

    public ActionSheetButton<TData>? Button { get; internal init; }
}