namespace IonBlazor.Components;

public class AlertValuesArray : IAlertValues<string[]>
{
    public string[]? Values { get; internal init; }
}