using System.Text.Json.Nodes;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonRefresherTests : IonTestContext
{
    public IonRefresherTests()
    {
        SetupComponentModule<IonRefresher>(module =>
        {
            module.SetupVoid("cancel", _ => true).SetVoidResult();
            module.SetupVoid("complete", _ => true).SetVoidResult();
            module.Setup<int>("getProgress", _ => true).SetResult(0);
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonRefresherRendersCorrectly()
    {
        var cut = Render<IonRefresher>();
        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithDisabled_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonRefresher>(parameters => parameters
            .Add(p => p.Disabled, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithChildContent_RendersCorrectly()
    {
        var cut = Render<IonRefresher>(parameters => parameters
            .AddChildContent("<ion-refresher-content></ion-refresher-content>"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonRefresher>(parameters => parameters
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "refresher" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task CancelAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRefresher>();

        await cut.Instance.CancelAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["cancel"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task CompleteAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRefresher>();

        await cut.Instance.CompleteAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["complete"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task GetProgressAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonRefresher>();

        await cut.Instance.GetProgressAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["getProgress"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    // ---------------------------------------------------------------------------
    // Pull events (ionPullStart / ionPullEnd)
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonPullStart_Fires_WithSender()
    {
        IonRefresherIonPullEventArgs? captured = null;

        var cut = Render<IonRefresher>(parameters => parameters
            .Add(p => p.IonPullStart, args => captured = args));

        await InvokeIonEventAsync("ionPullStart");

        captured.Should().NotBeNull();
        captured!.Sender.Should().Be(cut.Instance);
    }

    [Theory]
    [InlineData("complete", RefresherPullEndEventDetailReason.Complete)]
    [InlineData("cancel", RefresherPullEndEventDetailReason.Cancel)]
    [InlineData("something-else", RefresherPullEndEventDetailReason.Undefined)]
    public async Task IonPullEnd_Fires_WithMappedReason(string reason, RefresherPullEndEventDetailReason expected)
    {
        IonRefresherIonPullEndEventArgs? captured = null;

        var cut = Render<IonRefresher>(parameters => parameters
            .Add(p => p.IonPullEnd, args => captured = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject { ["reason"] = reason }
        };
        await InvokeIonEventAsync("ionPullEnd", payload);

        captured.Should().NotBeNull();
        captured!.Sender.Should().Be(cut.Instance);
        captured.Reason.Should().Be(expected);
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonRefresher>();
        Assert.Equal(nameof(IonRefresher), cut.Instance.JsImportName);
    }
}
