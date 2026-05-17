using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonItemSlidingTests : IonTestContext
{
    public IonItemSlidingTests()
    {
        SetupComponentModule<IonItemSliding>(module =>
        {
            module.SetupVoid("close", _ => true).SetVoidResult();
            module.SetupVoid("closeOpened", _ => true).SetVoidResult();
            module.Setup<double>("getOpenAmount", _ => true).SetResult(0d);
            module.Setup<double>("getSlidingRatio", _ => true).SetResult(0d);
            module.SetupVoid("open", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonItemSlidingRendersCorrectly()
    {
        var cut = Render<IonItemSliding>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonItemSliding>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonItemSliding>(parameters => parameters
            .AddChildContent("<ion-item><ion-label>Item</ion-label></ion-item>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonItemSliding>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "item-sliding" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CloseAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.CloseAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["close"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task CloseOpenedAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.CloseOpenedAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["closeOpened"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task GetOpenAmountAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.GetOpenAmountAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["getOpenAmount"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task GetSlidingRatioAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.GetSlidingRatioAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["getSlidingRatio"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task OpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.OpenAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["open"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task OpenAsync_WithSide_PassesSideArgument()
    {
        var cut = Render<IonItemSliding>();

        await cut.Instance.OpenAsync("end");

        JSRuntimeInvocation invocation = JSInterop.Invocations["open"].Single();
        invocation.Arguments[1].Should().Be("end");
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonItemSliding>();
        Assert.Equal(nameof(IonItemSliding), cut.Instance.JsImportName);
    }
}
