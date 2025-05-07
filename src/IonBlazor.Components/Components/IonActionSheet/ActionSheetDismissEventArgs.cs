namespace IonBlazor.Components;

public sealed record ActionSheetDismissEventArgs<TData> : ActionSheetEventArgs<TData>
    where TData: class, IActionSheetButtonData
{
    public string? Role { get; internal init; }

    public TData? Data { get; internal init; }
}