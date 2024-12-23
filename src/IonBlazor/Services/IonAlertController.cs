namespace IonBlazor.Services;

public sealed class IonAlertController: ComponentBase, IAsyncDisposable
{
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

    private static IJSObjectReference? _jsComponent;

    public static async ValueTask PresentAsync(
        string? header = null,
        string? subHeader = null,
        string? message = null,
        Func<IEnumerable<AlertButton>>? buttonsFunc = null,
        Func<IEnumerable<AlertInput>>? inputsFunc = null,
        IDictionary<string, string>? htmlAttributes = null,
        Action<IonAlertDismissEventArgs>? onDidDismiss = null!)
    {
        //AlertInput[]?
        //[Parameter] public Func<AlertInput[]>? Inputs { get; set; }
        //      if (_inputs?.Length > 0)
        //await JsComponent.InvokeVoidAsync("addInputs", _self, _inputs);

        IEnumerable<AlertButton>? buttons = null;
        IEnumerable<AlertInput>? inputs = inputsFunc?.Invoke() ?? Array.Empty<AlertInput>();

        DotNetObjectReference<IonicEventCallback<JsonObject?>>? buttonHandler = null!;

        if (buttonsFunc is not null)
        {
            buttons = buttonsFunc?.Invoke();
            buttonHandler = IonicEventCallback<JsonObject?>.Create(
                async args =>
                {
                    var index = args?["index"]?.GetValue<int?>();
                    var button = buttons?.ElementAtOrDefault(index ?? -1);
                    await (button?.Handler?.Invoke(new AlertButtonEventArgs() { Sender = null, Button = button, Index = index}) ?? ValueTask.CompletedTask);
                    // ReSharper disable once AccessToModifiedClosure
                    buttonHandler?.Dispose();
                });
        }

        var didDismissHandler = IonicEventCallback<JsonObject?>.Create(args =>
        {
            IAlertValues values = IonAlert.GetValues(args);

            onDidDismiss?.Invoke(new IonAlertDismissEventArgs
            {
                Sender = null,
                Role = args?["detail"]?["role"]?.GetValue<string>(),
                Values = values
            });

            return Task.CompletedTask;
        });


        await (_jsComponent?.InvokeVoidAsync("presentAlert", header, subHeader, message, buttons, inputs, buttonHandler, didDismissHandler, htmlAttributes) ?? ValueTask.CompletedTask);
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