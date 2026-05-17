namespace IonBlazor.Components;

public sealed record ActionSheetControllerDismissEventArgs
{
    public string? Role { get; internal init; }

    public JsonElement? Data { get; internal init; }
}