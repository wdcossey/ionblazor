using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonInputOtpTests : IonTestContext
{
    public IonInputOtpTests()
    {
        SetupComponentModule<IonInputOtp>(module =>
        {
            module.SetupVoid("setFocus", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonInputOtpRendersCorrectly()
    {
        var cut = Render<IonInputOtp>();
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

        var cut = Render<IonInputOtp>(parameters => parameters
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

        var cut = Render<IonInputOtp>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithReadonly_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonInputOtp>(parameters => parameters
            .Add(p => p.Readonly, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithLength_RendersCorrectly()
    {
        var cut = Render<IonInputOtp>(parameters => parameters
            .Add(p => p.Length, (byte)6));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonInputOtp>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "input-otp" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetFocusAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonInputOtp>();

        await cut.Instance.SetFocusAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["setFocus"].Single();
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
        var cut = Render<IonInputOtp>();
        Assert.Equal(nameof(IonInputOtp), cut.Instance.JsImportName);
    }
}
