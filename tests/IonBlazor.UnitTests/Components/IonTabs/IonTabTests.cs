using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonTabTests : IonTestContext
{
    public IonTabTests()
    {
        SetupComponentModule<IonTab>(module =>
        {
            module.SetupVoid("setActive", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonTabRendersCorrectly()
    {
        var cut = Render<IonTab>(parameters => parameters
            .Add(p => p.Tab, "home"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonTab>(parameters => parameters
            .Add(p => p.Tab, "home")
            .AddChildContent("<ion-content><p>Tab Content</p></ion-content>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonTab>(parameters => parameters
            .Add(p => p.Tab, "home")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "tab" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetActiveAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonTab>(parameters => parameters
            .Add(p => p.Tab, "home"));

        await cut.Instance.SetActiveAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["setActive"].Single();
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
        var cut = Render<IonTab>(parameters => parameters
            .Add(p => p.Tab, "home"));

        Assert.Equal(nameof(IonTab), cut.Instance.JsImportName);
    }
}
