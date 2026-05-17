using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonActionSheetTests : IonTestContext
{
    public IonActionSheetTests()
    {
        SetupComponentModule(nameof(IonActionSheet<ActionSheetButtonData>), module =>
        {
            module.SetupVoid("addButtons", _ => true).SetVoidResult();
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonActionSheetRendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
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

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
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

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.BackdropDismiss, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithCssClass_RendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.CssClass, "my-action-sheet"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithHeader_RendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.Header, "Action Sheet Header"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithSubHeader_RendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.SubHeader, "Action Sheet Sub Header"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIsOpen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithKeyboardClose_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.KeyboardClose, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithTranslucent_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.Translucent, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.Trigger, "open-action-sheet"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "action-sheet" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>();

        await cut.Instance.DismissAsync(null, null);

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>();

        await cut.Instance.PresentAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public void WithButtonsBuilder_InvokesAddButtons_WhenRendered()
    {
        Render<IonActionSheet<ActionSheetButtonData>>(parameters => parameters
            .Add(p => p.ButtonsBuilder, builder => builder
                .Add(new BasicActionSheetButton { Text = "OK", Role = "confirm" })
                .Add(new BasicActionSheetButton { Text = "Cancel", Role = "cancel" })));

        JSRuntimeInvocation invocation = JSInterop.Invocations["addButtons"].Single();
        invocation.Arguments[1].Should().BeAssignableTo<IReadOnlyList<IActionSheetButton>>()
            .Which.Count.Should().Be(2);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonActionSheet<ActionSheetButtonData>>();
        Assert.Equal(nameof(IonActionSheet<ActionSheetButtonData>), cut.Instance.JsImportName);
    }
}
