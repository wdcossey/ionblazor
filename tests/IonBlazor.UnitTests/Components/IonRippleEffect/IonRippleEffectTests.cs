using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonRippleEffectTests : IonTestContext
{
    public IonRippleEffectTests()
    {
        SetupComponentModule<IonRippleEffect>(module =>
        {
            module.SetupVoid("addRipple", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRippleEffectRendersCorrectly()
    {
        var cut = Render<IonRippleEffect>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonRippleEffectType.Bounded)]
    [InlineData(IonRippleEffectType.Unbounded)]
    public async Task WithType_RendersCorrectly(string type)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"type={type}");

        var cut = Render<IonRippleEffect>(parameters => parameters
            .Add(p => p.Type, type));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonRippleEffect>(parameters => parameters
            .AddChildContent("<span>Ripple</span>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRippleEffect>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "ripple-effect" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task AddRippleAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRippleEffect>();

        await cut.Instance.AddRippleAsync(10, 20);

        JSRuntimeInvocation invocation = JSInterop.Invocations["addRipple"].Single();
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
        var cut = Render<IonRippleEffect>();
        Assert.Equal(nameof(IonRippleEffect), cut.Instance.JsImportName);
    }
}
