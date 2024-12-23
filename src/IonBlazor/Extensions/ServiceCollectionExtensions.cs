// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIonBlazor(this IServiceCollection services)
    {
/*#if NET8_0_OR_GREATER
        services.AddKeyedScoped<Task<IJSObjectReference>>(nameof(IonAccordionGroup), async (provider, key) =>
        {
            IJSRuntime jsRuntime = provider.GetRequiredService<IJSRuntime>();
            return await jsRuntime.ImportAsync((string)key!);
        });
#endif*/

        return services;
    }
}