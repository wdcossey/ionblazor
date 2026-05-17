namespace IonBlazor.Components;

public sealed class AlertValues : IAlertValues<object>
{
    public object? Values { get; internal init; }
}