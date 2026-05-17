using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonSelectOfTests : IonTestContext
{
    public IonSelectOfTests()
    {
        SetupComponentModule(nameof(IonSelect<string>), module =>
        {
            module.SetupVoid("open", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonSelectOfRendersCorrectly()
    {
        var cut = Render<IonSelectOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["option-a", "option-b"])
            .Add(p => p.ItemTemplate, kvp => builder => builder.AddContent(0, kvp.Value)));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonSelectOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["option-a"])
            .Add(p => p.ItemTemplate, kvp => builder => builder.AddContent(0, kvp.Value))
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "select" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task OpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonSelectOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["option-a"])
            .Add(p => p.ItemTemplate, kvp => builder => builder.AddContent(0, kvp.Value)));

        await cut.Instance.OpenAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["open"].Single();
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
        var cut = Render<IonSelectOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["option-a"])
            .Add(p => p.ItemTemplate, kvp => builder => builder.AddContent(0, kvp.Value)));

        Assert.Equal(nameof(IonSelect<string>), cut.Instance.JsImportName);
    }
}
