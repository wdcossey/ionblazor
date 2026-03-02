using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonInfiniteScrollTests : IonTestContext
{
    public IonInfiniteScrollTests()
    {
        SetupComponentModule<IonInfiniteScroll>(module =>
        {
            module.SetupVoid("complete", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonInfiniteScrollRendersCorrectly()
    {
        var cut = Render<IonInfiniteScroll>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonInfiniteScroll>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonInfiniteScrollPosition.Top)]
    [InlineData(IonInfiniteScrollPosition.Bottom)]
    public async Task WithPosition_RendersCorrectly(string position)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"position={position}");

        var cut = Render<IonInfiniteScroll>(parameters => parameters
            .Add(p => p.Position, position));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithThreshold_RendersCorrectly()
    {
        var cut = Render<IonInfiniteScroll>(parameters => parameters
            .Add(p => p.Threshold, "100px"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonInfiniteScroll>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "infinite-scroll" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CompleteAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonInfiniteScroll>();

        await cut.Instance.CompleteAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["complete"].Single();
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
        var cut = Render<IonInfiniteScroll>();
        Assert.Equal(nameof(IonInfiniteScroll), cut.Instance.JsImportName);
    }
}
