using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonSimplePickerLegacyTests : IonTestContext
{
    public IonSimplePickerLegacyTests()
    {
        SetupComponentModule<IonPickerLegacy>(module =>
        {
            module.SetupVoid("withColumns", _ => true).SetVoidResult();
            module.SetupVoid("withButtons", _ => true).SetVoidResult();
            module.SetupVoid("dismiss", _ => true).SetVoidResult();
            module.SetupVoid("present", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSimplePickerLegacyRendersCorrectly()
    {
        var cut = Render<IonSimplePickerLegacy>();
        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSimplePickerLegacy>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "simple-picker-legacy" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonSimplePickerLegacy>();

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonSimplePickerLegacy>();

        await cut.Instance.PresentAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
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
        var cut = Render<IonSimplePickerLegacy>();
        Assert.Equal(nameof(IonPickerLegacy), cut.Instance.JsImportName);
    }
}
