using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonTabsTests : IonTestContext
{
    public IonTabsTests()
    {
        SetupComponentModule<IonTabs>(module =>
        {
            module.Setup<string>("getSelected", _ => true).SetResult("home");
            module.Setup<bool>("select", _ => true).SetResult(true);
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonTabsRendersCorrectly()
    {
        var cut = Render<IonTabs>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonTabs>(parameters => parameters
            .AddChildContent("<ion-tab tab=\"home\"></ion-tab>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonTabs>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "tabs" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task GetSelectedAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonTabs>();

        await cut.Instance.GetSelectedAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["getSelected"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SelectAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonTabs>();

        await cut.Instance.SelectAsync("home");

        JSRuntimeInvocation invocation = JSInterop.Invocations["select"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonTabs>();
        Assert.Equal(nameof(IonTabs), cut.Instance.JsImportName);
    }
}
