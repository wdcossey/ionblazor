using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonDateTimeTests : IonTestContext
{
    public IonDateTimeTests()
    {
        SetupComponentModule<IonDateTime>(module =>
        {
            module.SetupVoid("cancel", _ => true).SetVoidResult();
            module.SetupVoid("confirm", _ => true).SetVoidResult();
            module.SetupVoid("isDateEnabled", _ => true).SetVoidResult();
            module.SetupVoid("reset", _ => true).SetVoidResult();
            module.SetupVoid("setValue", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonDateTimeRendersCorrectly()
    {
        var cut = Render<IonDateTime>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonDateTime>(parameters => parameters
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

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithMultiple_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Multiple, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithPreferWheel_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.PreferWheel, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonDateTimePresentation.Date)]
    [InlineData(IonDateTimePresentation.Time)]
    [InlineData(IonDateTimePresentation.DateTime)]
    public async Task WithPresentation_RendersCorrectly(string presentation)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"presentation={presentation}");

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Presentation, presentation));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithReadonly_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Readonly, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonDateTime>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "datetime" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CancelAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonDateTime>();

        await cut.Instance.CancelAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["cancel"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ConfirmAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonDateTime>();

        await cut.Instance.ConfirmAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["confirm"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ResetAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonDateTime>();

        await cut.Instance.ResetAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["reset"].Single();
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
        var cut = Render<IonDateTime>();
        Assert.Equal(nameof(IonDateTime), cut.Instance.JsImportName);
    }
}
