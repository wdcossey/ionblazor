using IonBlazor.Components;
using Microsoft.JSInterop;
using NSubstitute;

namespace IonBlazor.UnitTests;

public abstract class IonTestContext : BunitContext
{
    protected IonTestContext()
    {
        JSInterop
            .SetupModule("./_content/IonBlazor/common.js")
            .SetupVoid("attachListeners", _ => true)
            .SetVoidResult();
    }

    protected void SetupComponentModule(string componentName, Action<BunitJSModuleInterop> configure)
    {
        BunitJSModuleInterop module = JSInterop
            .SetupModule($"./_content/IonBlazor/{componentName}.js");

        module.Mode = JSRuntimeMode.Loose;

        configure(module);
    }

    protected void SetupComponentModule<T>(Action<BunitJSModuleInterop> configure)
    {
        SetupComponentModule(typeof(T).Name, configure);
    }

    protected static Lazy<Task<IJSObjectReference>> CreateJsComponentMock(
        out IJSObjectReference mock)
    {
        mock = Substitute.For<IJSObjectReference>();
        IJSObjectReference? captured = mock;
        return new Lazy<Task<IJSObjectReference>>(() => Task.FromResult(captured));
    }

    /// <summary>
    /// Locates the <see cref="IonicEventCallback{TArgs}"/> registered for <paramref name="eventName"/>
    /// (via the <c>attachListeners</c> JS interop call) and invokes it with <paramref name="args"/>,
    /// simulating an event fired by the underlying Ionic web component.
    /// </summary>
    protected async Task InvokeIonEventAsync<TArgs>(string eventName, TArgs args)
        where TArgs : class
    {
        JSRuntimeInvocation invocation = JSInterop.Invocations["attachListeners"].Single();
        IEnumerable<IonEvent> events = (IEnumerable<IonEvent>)invocation.Arguments[0]!;
        IonEvent target = events.Single(e => e.Event == eventName);
        var dotNetRef = (DotNetObjectReference<IonicEventCallback<TArgs>>)target.Reference!;
        await dotNetRef.Value.OnCallbackEvent(args);
    }
}