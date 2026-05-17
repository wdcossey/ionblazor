using System.Collections.Immutable;

namespace IonBlazor.Services;

public sealed class IonActionSheetController : ComponentBase
{
    private static IJSRuntime _jsRuntime = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; init; } = null!;

    public static async ValueTask PresentAsync(Action<ActionSheetControllerOptions> configure)
    {
        ActionSheetControllerOptions options = new();
        configure(options);

        IReadOnlyList<IActionSheetButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (options.ButtonsBuilder is not null)
        {
            ActionSheetButtonBuilder buttonBuilder = new();
            options.ButtonsBuilder.Invoke(buttonBuilder);
            buttons = buttonBuilder.Build();

            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    IActionSheetButton? button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(button, index) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        var didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            options.OnDidDismiss?.Invoke(new ActionSheetControllerDismissEventArgs()
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
            });

            return Task.CompletedTask;
        });

        await using IJSObjectReference jsComponent = await CreateComponentAsync();
        await jsComponent.InvokeVoidAsync("present", options, buttons, buttonHandler, didDismissHandler);
    }

    private class DynamicButtonData : IActionSheetButtonData
    {
        public string? Action { get; set; }
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _jsRuntime = JsRuntime;
    }

    private static async Task<IJSObjectReference> CreateComponentAsync()
    {
        IJSObjectReference result = await _jsRuntime.ImportAsync(nameof(IonActionSheetController));

        if (result is null)
        {
            throw new InvalidOperationException($"{nameof(IonActionSheetController)} is not initialized");
        }

        return result;
    }
}