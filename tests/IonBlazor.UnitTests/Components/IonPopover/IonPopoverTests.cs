using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonPopoverTests : IonTestContext
{
    public IonPopoverTests()
    {
        SetupComponentModule<IonPopover>(module =>
        {
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
            module.SetupVoid("setIsOpen", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonPopoverRendersCorrectly()
    {
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
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

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.Animated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithArrow_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.Arrow, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithBackdropDismiss_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
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

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithTranslucent_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.Translucent, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.Trigger, "open-popover"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "popover" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>"));

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>"));

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
        var cut = Render<IonPopover>(parameters => parameters
            .AddChildContent("<p>Popover content</p>"));

        Assert.Equal(nameof(IonPopover), cut.Instance.JsImportName);
    }
}
