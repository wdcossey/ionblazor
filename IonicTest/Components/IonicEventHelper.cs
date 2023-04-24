namespace IonicTest.Components;

public class IonicEventCallback<TArgs>
{
    private readonly Func<TArgs, Task> _callback;

    public IonicEventCallback(Func<TArgs, Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}