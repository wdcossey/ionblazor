namespace IonBlazor.Components;

public sealed class AlertValuesArray : IAlertValues<string[]>
{
    public string[]? Values { get; internal init; }
}