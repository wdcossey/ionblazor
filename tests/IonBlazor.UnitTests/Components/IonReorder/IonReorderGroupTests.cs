using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonReorderGroupTests : IonTestContext
{
    public IonReorderGroupTests()
    {
        SetupComponentModule<IonReorderGroup>(module =>
        {
            module.SetupVoid("complete", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonReorderGroupRendersCorrectly()
    {
        var cut = Render<IonReorderGroup>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonReorderGroup>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonReorderGroup>(parameters => parameters
            .AddChildContent("<ion-item><ion-label>Item 1</ion-label><ion-reorder slot=\"end\"></ion-reorder></ion-item>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonReorderGroup>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "reorder-group" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CompleteAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonReorderGroup>();

        await cut.Instance.CompleteAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["complete"].Single();
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
        var cut = Render<IonReorderGroup>();
        Assert.Equal(nameof(IonReorderGroup), cut.Instance.JsImportName);
    }
}
