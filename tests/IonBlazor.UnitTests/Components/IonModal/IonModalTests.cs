using System.Text.Json.Nodes;
using FluentAssertions;
using Microsoft.AspNetCore.Components;

namespace IonBlazor.UnitTests.Components;

public class IonModalTests : IonTestContext
{
    public IonModalTests()
    {
        SetupComponentModule<IonModal>(module =>
        {
            module.SetupVoid("breakpoints", _ => true).SetVoidResult();
            module.SetupVoid("canDismissCallback", _ => true).SetVoidResult();
            module.SetupVoid("enterAnimation", _ => true).SetVoidResult();
            module.Setup<bool>("dismiss", _ => true).SetResult(true);
            module.Setup<int>("getCurrentBreakpoint", _ => true).SetResult(0);
            module.SetupVoid("initialBreakpoint", _ => true).SetVoidResult();
            module.Setup<bool>("isOpen", _ => true).SetResult(true);
            module.SetupVoid("present", _ => true).SetVoidResult();
            module.SetupVoid("setCurrentBreakpoint", _ => true).SetVoidResult();
        });
    }

    // ---------------------------------------------------------------------------
    // Render tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonModalRendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await Verify(cut.Markup);
    }

    [Theory]
    [InlineData(IonMode.iOS)]
    [InlineData(IonMode.MaterialDesign)]
    public async Task WithMode_RendersCorrectly(string mode)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"mode={mode}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Mode, mode));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithAnimated_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Animated, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithBackdropDismiss_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.BackdropDismiss, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithIsOpen_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IsOpen, value));

        await Verify(cut.Markup, settings);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task WithHandle_RendersCorrectly(bool value)
    {
        VerifySettings settings = new();
        settings.UseTextForParameters($"value={value}");

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Handle, value));

        await Verify(cut.Markup, settings);
    }

    [Fact]
    public async Task WithTrigger_RendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Trigger, "open-modal"));

        await Verify(cut.Markup);
    }

    [Fact]
    public async Task WithAttributes_RendersCorrectly()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.Attributes, new Dictionary<string, object>
            {
                { "id", "modal" }
            }));

        await Verify(cut.Markup);
    }

    // ---------------------------------------------------------------------------
    // JS interop tests
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task DismissAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.DismissAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["dismiss"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task PresentAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.PresentAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task GetCurrentBreakpointAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.GetCurrentBreakpointAsync();

        JSRuntimeInvocation invocation = JSInterop.Invocations["getCurrentBreakpoint"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetCurrentBreakpointAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.SetCurrentBreakpointAsync(0.5);

        JSRuntimeInvocation invocation = JSInterop.Invocations["setCurrentBreakpoint"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
        invocation.Arguments[1].Should().Be(0.5);
    }

    [Fact]
    public async Task SetIsOpenAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.SetIsOpenAsync(true);

        JSRuntimeInvocation invocation = JSInterop.Invocations["isOpen"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
        invocation.Arguments[1].Should().Be(true);
    }

    [Fact]
    public async Task SetBreakpointsAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.SetBreakpointsAsync(0, 0.25, 0.5, 1);

        JSRuntimeInvocation invocation = JSInterop.Invocations["breakpoints"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
    }

    [Fact]
    public async Task SetInitialBreakpointAsync_InvokesJsMethod_WhenCalled()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        await cut.Instance.SetInitialBreakpointAsync(0.5);

        JSRuntimeInvocation invocation = JSInterop.Invocations["initialBreakpoint"].Single();
        invocation.Arguments[0]
            .Should().BeAssignableTo<ElementReference>()
            .Which.Should().Be(cut.Instance.IonElement);
        invocation.Arguments[1].Should().Be(0.5d);
    }

    // ---------------------------------------------------------------------------
    // Drag events (sheet / card modal gestures)
    // ---------------------------------------------------------------------------

    [Fact]
    public async Task IonDragStart_Fires_WithSender()
    {
        IonModalDragStartEventArgs? captured = null;

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IonDragStart, args => captured = args));

        await InvokeIonEventAsync("ionDragStart", new JsonObject());

        captured.Should().NotBeNull();
        captured!.Sender.Should().Be(cut.Instance);
    }

    [Fact]
    public async Task IonDragMove_Fires_WithFlattenedDetail()
    {
        IonModalDragMoveEventArgs? captured = null;

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IonDragMove, args => captured = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject
            {
                ["currentY"] = 120,
                ["deltaY"] = 12,
                ["velocityY"] = 0.5,
                ["progress"] = 0.75,
                ["snapBreakpoint"] = 0.5
            }
        };
        await InvokeIonEventAsync("ionDragMove", payload);

        captured.Should().NotBeNull();
        captured!.Sender.Should().Be(cut.Instance);
        captured.CurrentY.Should().Be(120m);
        captured.DeltaY.Should().Be(12m);
        captured.VelocityY.Should().Be(0.5m);
        captured.Progress.Should().Be(0.75m);
        captured.SnapBreakpoint.Should().Be(0.5m);
    }

    [Fact]
    public async Task IonDragEnd_Fires_WithFlattenedDetail()
    {
        IonModalDragEndEventArgs? captured = null;

        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IonDragEnd, args => captured = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject
            {
                ["currentY"] = 200,
                ["deltaY"] = -25,
                ["velocityY"] = 1.2,
                ["progress"] = 1.0,
                ["snapBreakpoint"] = 1.0
            }
        };
        await InvokeIonEventAsync("ionDragEnd", payload);

        captured.Should().NotBeNull();
        captured!.Sender.Should().Be(cut.Instance);
        captured.CurrentY.Should().Be(200m);
        captured.DeltaY.Should().Be(-25m);
        captured.VelocityY.Should().Be(1.2m);
        captured.Progress.Should().Be(1.0m);
        captured.SnapBreakpoint.Should().Be(1.0m);
    }

    [Fact]
    public async Task IonDragEnd_Fires_WithNullSnapBreakpoint_ForCardModal()
    {
        IonModalDragEndEventArgs? captured = null;

        Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>")
            .Add(p => p.IonDragEnd, args => captured = args));

        var payload = new JsonObject
        {
            ["detail"] = new JsonObject
            {
                ["currentY"] = 50,
                ["deltaY"] = -5,
                ["velocityY"] = 0.1,
                ["progress"] = 0.2
            }
        };
        await InvokeIonEventAsync("ionDragEnd", payload);

        captured.Should().NotBeNull();
        captured!.SnapBreakpoint.Should().BeNull();
    }

    // ---------------------------------------------------------------------------
    // JsImportName
    // ---------------------------------------------------------------------------

    [Fact]
    public void Assert_JsImportName()
    {
        var cut = Render<IonModal>(parameters => parameters
            .AddChildContent("<p>Modal content</p>"));

        Assert.Equal(nameof(IonModal), cut.Instance.JsImportName);
    }
}
