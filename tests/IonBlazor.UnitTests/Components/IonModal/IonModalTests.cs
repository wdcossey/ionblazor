using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonModalTests : IonTestContext
{
    public IonModalTests()
    {
        SetupComponentModule<IonModal>(module =>
        {
            module.SetupVoid("canDismissCallback", _ => true).SetVoidResult();
            module.SetupVoid("enterAnimation", _ => true).SetVoidResult();
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.Setup<int>("getCurrentBreakpoint", _ => true).SetResult(0);
            module.Setup<bool>("isOpen", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
            module.SetupVoid("setCurrentBreakpoint", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonModalRendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
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

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
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

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
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

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithHandle_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Handle, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Trigger, "open-modal"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "modal" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

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
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        Assert.Equal(nameof(IonModal), cut.Instance.JsImportName);
    }
}
