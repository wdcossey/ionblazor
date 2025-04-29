namespace IonBlazor.Components.Abstractions;

public interface IAlertValues<out TData> : IAlertValues
{
    TData? Values { get; }
}