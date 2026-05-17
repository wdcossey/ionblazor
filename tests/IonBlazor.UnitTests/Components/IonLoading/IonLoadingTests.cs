using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonLoadingTests : IonTestContext
{
    public IonLoadingTests()
    {
        SetupComponentModule<IonLoading>(module =>
        {
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
            module.SetupVoid("updateMessage", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonLoadingRendersCorrectly()
    {
        var cut = Render<IonLoading>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAnimated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.Animated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithBackdropDismiss_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.BackdropDismiss, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIsOpen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithMessage_RendersCorrectly()
    {
        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.Message, "Loading..."));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonLoadingSpinner.Circular)]
    [InlineData(IonLoadingSpinner.Dots)]
    [InlineData(IonLoadingSpinner.Lines)]
    public async Task WithSpinner_RendersCorrectly(string spinner)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"spinner={spinner}");

        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.Spinner, spinner));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonLoading>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "loading" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonLoading>();

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonLoading>();

        await cut.Instance.PresentAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task UpdateMessageAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonLoading>();

        await cut.Instance.UpdateMessageAsync("Updated message");

        JSRuntimeInvocation invocation = JSInterop.Invocations["updateMessage"].Single();
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
        var cut = Render<IonLoading>();
        Assert.Equal(nameof(IonLoading), cut.Instance.JsImportName);
    }
}
