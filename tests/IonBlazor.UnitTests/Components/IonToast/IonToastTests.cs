using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonToastTests : IonTestContext
{
    public IonToastTests()
    {
        SetupComponentModule<IonToast>(module =>
        {
            module.SetupVoid("withButtons", _ => true).SetVoidResult();
            module.SetupVoid("dismiss", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonToastRendersCorrectly()
    {
        var cut = Render<IonToast>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonColor.Primary)]
    [InlineData(IonColor.Secondary)]
    [InlineData(IonColor.Danger)]
    public async Task WithColor_RendersCorrectly(string color)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"color={color}");

        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAnimated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Animated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIsOpen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithMessage_RendersCorrectly()
    {
        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Message, "Operation completed."));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithHeader_RendersCorrectly()
    {
        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Header, "Toast Header"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Trigger, "show-toast"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonToast>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "toast" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonToast>();

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
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
        var cut = Render<IonToast>();
        Assert.Equal(nameof(IonToast), cut.Instance.JsImportName);
    }
}
