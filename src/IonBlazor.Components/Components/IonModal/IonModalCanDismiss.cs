namespace IonBlazor.Components;

public class IonModalCanDismiss : IIonModalCanDismiss
{
    private IonModalCanDismiss(Func<Task<bool>> value)
    {
        Value = value;
    }

    public static IonModalCanDismiss Create(bool value)
    {
        return new IonModalCanDismiss(() => Task.FromResult(value));
    }

    public static IonModalCanDismiss Create(Func<Task<bool>> callback)
    {
        return new IonModalCanDismiss(callback);
    }

    public Func<Task<bool>> Value { get; }
}