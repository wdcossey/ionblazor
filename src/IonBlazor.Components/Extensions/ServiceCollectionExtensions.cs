using IonBlazor.Services;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers IonBlazor's injectable overlay services (<see cref="IonAlertService"/>,
    /// <see cref="IonActionSheetService"/>, <see cref="IonToastService"/>,
    /// <see cref="IonLoadingService"/>) as scoped dependencies. Call once during DI setup
    /// (e.g. in <c>Program.cs</c> or <c>MauiProgram.cs</c>).
    /// </summary>
    public static IServiceCollection AddIonBlazor(this IServiceCollection services)
    {
        services.AddScoped<IonAlertService>();
        services.AddScoped<IonActionSheetService>();
        services.AddScoped<IonToastService>();
        services.AddScoped<IonLoadingService>();
        return services;
    }
}
