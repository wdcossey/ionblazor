using System.Collections.Immutable;

namespace IonBlazor.Services;

public sealed class IonAlertController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference? _jsComponent;

    public static async ValueTask PresentAsync(Action<AlertControllerOptions> configure)
    {
        AlertControllerOptions options = new();
        configure(options);

        IImmutableList<IAlertButton>? buttons = null;

        AlertInputBuilder alertInputBuilder = new();
        options.InputsBuilder?.Invoke(alertInputBuilder);
        IEnumerable<IAlertInput> inputs = alertInputBuilder.Build();

        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (options.ButtonsBuilder is not null)
        {
            AlertButtonBuilder toastButtonBuilder = new();
            options.ButtonsBuilder.Invoke(toastButtonBuilder);
            buttons = toastButtonBuilder.Build();

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


        await (_jsComponent?.InvokeVoidAsync("presentAlert", options, buttons, inputs, buttonHandler, didDismissHandler) ?? ValueTask.CompletedTask);
    }

    public async ValueTask DisposeAsync()
    {
        await (_jsComponent?.DisposeAsync() ?? ValueTask.CompletedTask);
        _jsComponent = null;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);

        if (!firstRender)
            return;

        _jsComponent = await JsRuntime.ImportAsync(nameof(IonAlertController));
    }
}