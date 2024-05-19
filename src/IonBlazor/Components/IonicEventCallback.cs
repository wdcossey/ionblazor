namespace IonBlazor.Components;

public class IonicEventCallback
{
    private readonly Func<Task> _callback;
    
    private IonicEventCallback(Func<Task> callback) => _callback = callback;
    
    public static DotNetObjectReference<IonicEventCallback> Create(Func<Task> callback) => 
        DotNetObjectReference.Create(new IonicEventCallback(callback));
    
    [JSInvokable]
    public Task OnCallbackEvent() => _callback();
}

public class IonicEventCallback<TArgs>
{
    private readonly Func<TArgs, Task> _callback;
    
    private IonicEventCallback(Func<TArgs, Task> callback) => _callback = callback;
    
    public static DotNetObjectReference<IonicEventCallback<TArgs>> Create(Func<TArgs, Task> callback) => 
        DotNetObjectReference.Create(new IonicEventCallback<TArgs>(callback));
    
    [JSInvokable]
    public Task OnCallbackEvent(TArgs args) => _callback(args);
}

public class IonicEventCallbackResult<TResult>
{
    private readonly Func<Task<TResult>> _callback;

    private IonicEventCallbackResult(Func<Task<TResult>> callback) => _callback = callback;

    public static DotNetObjectReference<IonicEventCallbackResult<TResult>> Create(Func<Task<TResult>> callback) => 
        DotNetObjectReference.Create(new IonicEventCallbackResult<TResult>(callback));

    [JSInvokable]
    public Task<TResult> OnCallbackEvent() => _callback();
}

public class IonicEventCallbackResult<TArgs, TResult>
{
    private readonly Func<TArgs, Task<TResult>> _callback;

    private IonicEventCallbackResult(Func<TArgs, Task<TResult>> callback) => _callback = callback;

    public static DotNetObjectReference<IonicEventCallbackResult<TArgs, TResult>> Create(Func<TArgs, Task<TResult>> callback) => 
        DotNetObjectReference.Create(new IonicEventCallbackResult<TArgs, TResult>(callback));
    
    [JSInvokable]
    public Task<TResult> OnCallbackEvent(TArgs args) => _callback(args);
}