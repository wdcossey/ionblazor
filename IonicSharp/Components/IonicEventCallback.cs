namespace IonicSharp.Components;

public class IonicEventCallback
{
    private readonly Func<Task> _callback;

    public IonicEventCallback(Func<Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent() => _callback();
}

public class IonicEventCallback<TArgs>
{
    private readonly Func<TArgs, Task> _callback;

    public IonicEventCallback(Func<TArgs, Task> callback) => _callback = callback;

    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}