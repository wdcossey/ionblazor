namespace IonBlazor.Components.Abstractions;

public interface IIonModalCanDismiss
{
    [JsonIgnore]
    Func<Task<bool>> Value { get; }
}

/*public class IonModalCanDismiss : IIonModalCanDismiss
{
    public IonModalCanDismiss(bool value)
    {
        Value = value;
    }

    public bool Value { get; }
}

public class IonModalCanDismissCallback : IIonModalCanDismiss
{
    public IonModalCanDismissCallback(Func<Task<bool>> callback)
    {
        Callback = callback;
    }

    public Func<Task<bool>> Callback { get; }
}*/