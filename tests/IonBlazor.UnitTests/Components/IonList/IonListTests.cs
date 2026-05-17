using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonListTests : IonTestContext
{
    public IonListTests()
    {
        SetupComponentModule<IonList>(module =>
        {
            module.Setup<bool>("closeSlidingItems", _ => true).SetResult(true);
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonListRendersCorrectly()
    {
        var cut = Render<IonList>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonList>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithInset_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonList>(parameters => parameters
            .Add(p => p.Inset, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonListLines.Full)]
    [InlineData(IonListLines.Inset)]
    [InlineData(IonListLines.None)]
    public async Task WithLines_RendersCorrectly(string lines)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"lines={lines}");

        var cut = Render<IonList>(parameters => parameters
            .Add(p => p.Lines, lines));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonList>(parameters => parameters
            .AddChildContent("<ion-item><ion-label>Item</ion-label></ion-item>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonList>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "list" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CloseSlidingItemsAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonList>();

        await cut.Instance.CloseSlidingItemsAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["closeSlidingItems"].Single();
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
        var cut = Render<IonList>();
        Assert.Equal(nameof(IonList), cut.Instance.JsImportName);
    }
}
