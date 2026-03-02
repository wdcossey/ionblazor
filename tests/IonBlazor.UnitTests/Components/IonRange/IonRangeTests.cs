using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonRangeTests : IonTestContext
{
    public IonRangeTests()
    {
        SetupComponentModule<IonRange>(module =>
        {
            module.SetupVoid("setValue", _ => true).SetVoidResult();
            module.SetupVoid("setUpperLowerValue", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRangeRendersCorrectly()
    {
        var cut = Render<IonRange>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonRange>(parameters => parameters
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

        var cut = Render<IonRange>(parameters => parameters
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

        var cut = Render<IonRange>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDualKnobs_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonRange>(parameters => parameters
            .Add(p => p.DualKnobs, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithPin_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonRange>(parameters => parameters
            .Add(p => p.Pin, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithLabel_RendersCorrectly()
    {
        var cut = Render<IonRange>(parameters => parameters
            .Add(p => p.Label, "Volume"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonRange>(parameters => parameters
            .AddChildContent("<ion-icon slot=\"start\" name=\"volume-low\"></ion-icon>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRange>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "range" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetValueAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRange>();

        await cut.Instance.SetValueAsync(50);

        JSRuntimeInvocation invocation = JSInterop.Invocations["setValue"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetValueAsync_WithUpperLower_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRange>();

        await cut.Instance.SetValueAsync(20, 80);

        JSRuntimeInvocation invocation = JSInterop.Invocations["setUpperLowerValue"].Single();
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
        var cut = Render<IonRange>();
        Assert.Equal(nameof(IonRange), cut.Instance.JsImportName);
    }
}
