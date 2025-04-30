namespace IonBlazor.Components;

public sealed class AlertValuesDictionary : IAlertValues<IDictionary<string, string>>
{
    public IDictionary<string, string>? Values { get; internal init; }
}