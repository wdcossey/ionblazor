namespace IonBlazor.Abstractions;

public abstract class IonJsComponent : IonComponent, IAsyncDisposable
{
    internal abstract string JsImportName { get; }

    internal Lazy<Task<IJSObjectReference>> JsComponent { get; set; } = null!;

    protected override void OnInitialized() => ConfigureJsComponent();

    public override async ValueTask DisposeAsync()
    {
        try
        {
            if (JsComponent.IsValueCreated is true)
                await (await JsComponent.Value).DisposeAsync();
        }
        finally
        {
            await base.DisposeAsync();
        }
    }

    private void ConfigureJsComponent() => JsComponent = new Lazy<Task<IJSObjectReference>>(() => JsRuntime.ImportAsync(JsImportName));
}