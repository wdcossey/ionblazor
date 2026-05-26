namespace IonBlazor.Services;

/// <summary>
/// Scoped DI service for programmatically presenting an Ionic action sheet.
/// Register via <c>services.AddIonBlazor()</c> and inject with <c>@inject IonActionSheetService</c>.
/// </summary>
public sealed class IonActionSheetService(IJSRuntime jsRuntime)
{
    /// <summary>
    /// Presents an action sheet configured by the supplied <see cref="ActionSheetControllerOptions"/> action.
    /// </summary>
    public async ValueTask PresentAsync(Action<ActionSheetControllerOptions> configure)
    {
        ActionSheetControllerOptions options = new();
        configure(options);

        IReadOnlyList<IActionSheetButton>? buttons = null;
        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null;

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
            options.OnDidDismiss?.Invoke(new ActionSheetControllerDismissEventArgs
            {
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Data = args?["detail"]?["data"]?.Deserialize<JsonElement>(),
            });

            return Task.CompletedTask;
        });

        await using IJSObjectReference jsComponent = await jsRuntime.ImportAsync(nameof(IonActionSheetService));
        await jsComponent.InvokeVoidAsync("present", options, buttons, buttonHandler, didDismissHandler);
    }
}
