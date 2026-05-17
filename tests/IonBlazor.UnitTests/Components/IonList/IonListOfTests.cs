using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonListOfTests : IonTestContext
{
    public IonListOfTests()
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
    public async Task IonListOfRendersCorrectly()
    {
        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1", "item-2"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item)));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item))
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

        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item))
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

        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item))
            .Add(p => p.Lines, lines));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithEmptyTemplate_RendersCorrectly()
    {
        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, [])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item))
            .Add(p => p.EmptyTemplate, builder => builder.AddContent(0, "No items")));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item))
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
        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item)));

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
        var cut = Render<IonListOf<string>>(parameters => parameters
            .Add(p => p.ItemsSource, ["item-1"])
            .Add(p => p.ItemTemplate, item => builder => builder.AddContent(0, item)));

        Assert.Equal(nameof(IonList), cut.Instance.JsImportName);
    }
}
