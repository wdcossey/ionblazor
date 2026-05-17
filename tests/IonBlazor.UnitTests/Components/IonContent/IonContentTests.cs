using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonContentTests : IonTestContext
{
    public IonContentTests()
    {
        SetupComponentModule<IonContent>(module =>
        {
            module.SetupVoid("scrollByPoint", _ => true).SetVoidResult();
            module.SetupVoid("scrollToBottom", _ => true).SetVoidResult();
            module.SetupVoid("scrollToPoint", _ => true).SetVoidResult();
            module.SetupVoid("scrollToTop", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonContentRendersCorrectly()
    {
        var cut = Render<IonContent>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonColor.Primary)]
    [InlineData(IonColor.Secondary)]
    [InlineData(IonColor.Danger)]
    public async Task WithColor_RendersCorrectly(string color)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"color={color}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithForceOverscroll_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.ForceOverscroll, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithFullscreen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.Fullscreen, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithScrollEvents_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.ScrollEvents, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithScrollX_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.ScrollX, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithScrollY_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.ScrollY, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonContent>(parameters => parameters
            .AddChildContent("<p>Content</p>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonContent>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "content" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task ScrollByPointAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonContent>();

        await cut.Instance.ScrollByPointAsync(0, 100, 300);

        JSRuntimeInvocation invocation = JSInterop.Invocations["scrollByPoint"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ScrollToBottomAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonContent>();

        await cut.Instance.ScrollToBottomAsync(300);

        JSRuntimeInvocation invocation = JSInterop.Invocations["scrollToBottom"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ScrollToPointAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonContent>();

        await cut.Instance.ScrollToPointAsync(0, 200, 300);

        JSRuntimeInvocation invocation = JSInterop.Invocations["scrollToPoint"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ScrollToTopAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonContent>();

        await cut.Instance.ScrollToTopAsync(300);

        JSRuntimeInvocation invocation = JSInterop.Invocations["scrollToTop"].Single();
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
        var cut = Render<IonContent>();
        Assert.Equal(nameof(IonContent), cut.Instance.JsImportName);
    }
}
