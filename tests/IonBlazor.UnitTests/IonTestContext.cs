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
}