namespace IonBlazor.Components;

public sealed record ActionSheetButtonHandlerEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public int? Index { get; internal init; }

    public IActionSheetButton? Button { get; internal init; }

    public TData? Data { get; internal init; }
}