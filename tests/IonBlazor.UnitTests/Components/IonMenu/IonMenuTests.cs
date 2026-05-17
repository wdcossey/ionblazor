using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonMenuTests : IonTestContext
{
    public IonMenuTests()
    {
        SetupComponentModule<IonMenu>(module =>
        {
            module.Setup<bool>("close", _ => true).SetResult(true);
            module.Setup<bool>("isActive", _ => true).SetResult(true);
            module.Setup<bool>("isOpen", _ => true).SetResult(true);
            module.Setup<bool>("open", _ => true).SetResult(true);
            module.Setup<bool>("setOpen", _ => true).SetResult(true);
            module.Setup<bool>("toggle", _ => true).SetResult(true);
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonMenuRendersCorrectly()
    {
        var cut = Render<IonMenu>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(IonMenuSide.Start)]
    [InlineData(IonMenuSide.End)]
    public async Task WithSide_RendersCorrectly(string side)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"side={side}");

        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.Side, side));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithSwipeGesture_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.SwipeGesture, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithContentId_RendersCorrectly()
    {
        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.ContentId, "main-content"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonMenu>(parameters => parameters
            .AddChildContent("<ion-content><p>Menu Content</p></ion-content>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonMenu>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "menu" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CloseAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.CloseAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["close"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task OpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.OpenAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["open"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task ToggleAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.ToggleAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["toggle"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task IsActiveAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.IsActiveAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["isActive"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task IsOpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.IsOpenAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["isOpen"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetOpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonMenu>();

        await cut.Instance.SetOpenAsync(shouldOpen: true);

        JSRuntimeInvocation invocation = JSInterop.Invocations["setOpen"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
        invocation.Arguments[1].Should().Be(true);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonMenu>();
        Assert.Equal(nameof(IonMenu), cut.Instance.JsImportName);
    }
}
