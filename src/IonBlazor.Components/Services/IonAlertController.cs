using System.Collections.Immutable;

namespace IonBlazor.Services;

public sealed class IonAlertController: ComponentBase
{
    private static IJSRuntime _jsRuntime = null!;

    [Inject]
    private IJSRuntime JsRuntime { get; init; } = null!;

    public static async ValueTask PresentAsync(Action<AlertControllerOptions> configure)
    {
        AlertControllerOptions options = new();
        configure(options);

        IReadOnlyList<IAlertButton>? buttons = null;

        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (options.ButtonsBuilder is not null)
        {
            AlertButtonBuilder alertButtonBuilder = new();
            options.ButtonsBuilder.Invoke(alertButtonBuilder);
            buttons = alertButtonBuilder.Build();

            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    IAlertButton? button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new AlertButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        IReadOnlyList<IAlertInput>? inputs = null;

        if (options.InputsBuilder is not null)
        {
            AlertInputBuilder alertInputBuilder = new();
            options.InputsBuilder?.Invoke(alertInputBuilder);
            inputs = alertInputBuilder.Build();
        }

        var didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IAlertValues values = IonAlert.GetValues(args);

            options.OnDidDismiss?.Invoke(new IonAlertDismissEventArgs
            {
                Sender = null,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values
            });

            return Task.CompletedTask;
        });

        await using IJSObjectReference jsComponent = await CreateComponentAsync();
        await jsComponent.InvokeVoidAsync("presentAlert", options, buttons, inputs, buttonHandler, didDismissHandler);
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        _jsRuntime = JsRuntime;
    }

    private static async Task<IJSObjectReference> CreateComponentAsync()
    {
        IJSObjectReference result = await _jsRuntime.ImportAsync(nameof(IonAlertController));

        if (result is null)
        {
            throw new InvalidOperationException($"{nameof(IonAlertController)} is not initialized");
        }

        return result;
    }
}