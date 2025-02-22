namespace IonBlazor.Components.Abstractions;

public interface IIonLoading : IAsyncDisposable
{
    ValueTask<bool> DismissAsync<TData>(TData? data = null, string? role = null) where TData : class;
    ValueTask<bool> DismissAsync(string? role = null);
    ValueTask PresentAsync();
    ValueTask PresentWithMessageAsync(string? message);
    ValueTask UpdateMessageAsync(string? message);
}