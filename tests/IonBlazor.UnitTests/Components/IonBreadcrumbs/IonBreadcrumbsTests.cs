using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonBreadcrumbsTests : IonTestContext
{
    public IonBreadcrumbsTests()
    {
        SetupComponentModule<IonBreadcrumbs>(module =>
        {
            module.SetupVoid("attachIonCollapsedClickListener", _ => true).SetVoidResult();
            module.SetupVoid("maxItems", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonBreadcrumbsRendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonBreadcrumbs>(parameters => parameters
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

        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .Add(p => p.Color, color));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithItemsAfterCollapse_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .Add(p => p.ItemsAfterCollapse, 2u));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithItemsBeforeCollapse_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .Add(p => p.ItemsBeforeCollapse, 1u));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithMaxItems_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .Add(p => p.MaxItems, 4u));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .AddChildContent("<ion-breadcrumb>Home</ion-breadcrumb>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonBreadcrumbs>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "breadcrumbs" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task SetMaxItemsAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonBreadcrumbs>();

        await cut.Instance.SetMaxItemsAsync(5u);

        JSRuntimeInvocation invocation = JSInterop.Invocations["maxItems"].Single();
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
        var cut = Render<IonBreadcrumbs>();
        Assert.Equal(nameof(IonBreadcrumbs), cut.Instance.JsImportName);
    }
}
