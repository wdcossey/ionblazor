# IonBlazor

> Ionic Framework components for Blazor, MAUI Hybrid, and ASP.NET Core Razor.

The all-in-one package: pulls in the component wrappers **and** every static asset bundle so a fresh project can render Ionic UI without any extra setup.

## Install

```bash
dotnet add package IonBlazor
```

## Setup

Add the Ionic script and stylesheets to your host page — `wwwroot/index.html` for Blazor WebAssembly, `Pages/_Host.cshtml` (or `App.razor` in .NET 8+) for Blazor Server, `wwwroot/index.html` for MAUI Hybrid:

```html
<script type="module" src="_content/IonBlazor/@ionic/core/dist/ionic/ionic.esm.js"></script>

<link rel="stylesheet" href="_content/IonBlazor/@ionic/core/css/core.css" />
<link rel="stylesheet" href="_content/IonBlazor/@ionic/core/css/ionic.bundle.css" />
```

Add the namespaces to `_Imports.razor`:

```razor
@using IonBlazor.Components
@using IonBlazor.Components.Abstractions
@using IonBlazor.Services
```

## Use

```razor
<IonApp>
    <IonContent>
        <IonButton OnClick="@HandleClick">Tap me</IonButton>
    </IonContent>
</IonApp>

@code {
    void HandleClick() { /* ... */ }
}
```

## Overlay services (Controllers → Services)

The four overlay controllers (`IonAlertController`, `IonActionSheetController`, `IonToastController`,
`IonLoadingController`) have been replaced by **scoped DI services** — `IonAlertService`,
`IonActionSheetService`, `IonToastService`, `IonLoadingService`. The old controllers are now
`[Obsolete(error: true)]` stubs and will fail the build with a pointer to the migration recipe.

Register all four services with one call in `Program.cs` / `MauiProgram.cs`:

```csharp
builder.Services.AddIonBlazor();
```

Then inject and call:

```razor
@inject IonAlertService AlertService

@code {
    private async Task Show()
    {
        await AlertService.PresentAsync(options =>
        {
            options.Header = "Heads up";
            options.Message = "Hello.";
        });
    }
}
```

Remove every `<IonAlertController/>`, `<IonActionSheetController/>`, `<IonToastController/>`, and
`<IonLoadingController/>` tag from your root layout after switching. See the
[IonBlazor.Components README](https://www.nuget.org/packages/IonBlazor.Components) for full details
including the per-service migration mapping and the dropped `IonLoadingController.Create` sync wrapper.

## Package family

| Package                            | Use when                                                                    |
|------------------------------------|-----------------------------------------------------------------------------|
| **IonBlazor**                      | You want everything in one reference. Recommended for most apps.            |
| IonBlazor.Components               | You want only the C# wrappers and will provide Ionic JS/CSS yourself.       |
| IonBlazor.StaticAssets             | JavaScript interop modules used by the wrappers (transitively required).    |
| IonBlazor.StaticAssets.Ionic       | The `@ionic/core` JavaScript bundle and CSS, served from `_content/IonBlazor`. |
| IonBlazor.StaticAssets.Ionic.Svg   | Ionic SVG icon set, served from `_content/IonBlazor`.                       |

### MAUI Hybrid multi-project caveat

In a MAUI Hybrid solution where multiple projects (e.g. a `Core` Razor class library and a `MAUI` host) all reference an asset-carrying package, MAUI's static-asset packaging fails with a duplicate-asset error.

Workaround: only **one** project ships the static assets. That project can use either:

- the bundle — `IonBlazor`, **or**
- the granular packages — `IonBlazor.StaticAssets` + `IonBlazor.StaticAssets.Ionic` (+ `IonBlazor.StaticAssets.Ionic.Svg` for icons), optionally with `IonBlazor.Components`.

Every other project references only [`IonBlazor.Components`](https://www.nuget.org/packages/IonBlazor.Components), which has no static assets. The component wrappers stay available everywhere; the assets ship exactly once.

## Supported frameworks

`net8.0`, `net9.0`, `net10.0`.

## Links

- Source: <https://github.com/wdcossey/ionblazor>
- Issues: <https://github.com/wdcossey/ionblazor/issues>
- Ionic Framework: <https://ionicframework.com/>

## License

MIT.