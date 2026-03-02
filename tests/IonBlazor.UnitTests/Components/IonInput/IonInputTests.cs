using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonInputTests : IonTestContext
{
    public IonInputTests()
    {
        SetupComponentModule<IonInput>(module =>
        {
            module.SetupVoid("counterFormatter", _ => true).SetVoidResult();
            module.SetupVoid("markInvalid", _ => true).SetVoidResult();
            module.SetupVoid("markTouched", _ => true).SetVoidResult();
            module.SetupVoid("markUnTouched", _ => true).SetVoidResult();
            module.SetupVoid("markValid", _ => true).SetVoidResult();
            module.SetupVoid("removeMarking", _ => true).SetVoidResult();
            module.SetupVoid("setFocus", _ => true).SetVoidResult();
            module.SetupVoid("setValue", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonInputRendersCorrectly()
    {
        var cut = Render<IonInput>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonInput>(parameters => parameters
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

        var cut = Render<IonInput>(parameters => parameters
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

        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonInputType.Text)]
    [InlineData(IonInputType.Password)]
    [InlineData(IonInputType.Email)]
    [InlineData(IonInputType.Number)]
    public async Task WithType_RendersCorrectly(string type)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"type={type}");

        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Type, type));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonInputFill.Solid)]
    [InlineData(IonInputFill.Outline)]
    public async Task WithFill_RendersCorrectly(string fill)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"fill={fill}");

        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Fill, fill));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithPlaceholder_RendersCorrectly()
    {
        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Placeholder, "Enter value"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithLabel_RendersCorrectly()
    {
        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Label, "My Label"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonInput>(parameters => parameters
            .AddChildContent("<ion-input-password-toggle></ion-input-password-toggle>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonInput>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "input" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetFocusAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonInput>();

        await cut.Instance.SetFocusAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["setFocus"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetValueAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonInput>();

        await cut.Instance.SetValueAsync("new value");

        JSRuntimeInvocation invocation = JSInterop.Invocations["setValue"].Single();
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
        var cut = Render<IonInput>();
        Assert.Equal(nameof(IonInput), cut.Instance.JsImportName);
    }
}
