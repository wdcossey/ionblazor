namespace IonBlazor.Components;

public class AlertValuesDictionary : IAlertValues<IDictionary<string, string>>
{
    public IDictionary<string, string>? Values { get; internal init; }
}