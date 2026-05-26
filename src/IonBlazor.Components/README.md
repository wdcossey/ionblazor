# IonBlazor.Components

> Razor / Blazor wrappers for [Ionic Framework](https://ionicframework.com/) web components.

C# component types only. Does **not** include Ionic's JavaScript bundle, CSS, or icons. Use this package when you want to control how Ionic itself is loaded — typically a CDN reference, or in a MAUI Hybrid solution where only one project is allowed to ship the static assets.

For the all-in-one experience (wrappers + every static asset), install [`IonBlazor`](https://www.nuget.org/packages/IonBlazor) instead.

## Install

```bash
dotnet add package IonBlazor.Components
```

## Setup

You're responsible for loading Ionic's JS and CSS. Two common approaches:

### Option 1 — Load Ionic from a CDN

In your host page (`wwwroot/index.html`, `_Host.cshtml`, or `App.razor`):

```html
<script type="module" src="https://cdn.jsdelivr.net/npm/@ionic/core/dist/ionic/ionic.esm.js"></script>

<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@ionic/core/css/core.css" />
<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/@ionic/core/css/ionic.bundle.css" />
```

### Option 2 — Add the asset packages explicitly

```bash
dotnet add package IonBlazor.StaticAssets         # JS interop modules (required)
dotnet add package IonBlazor.StaticAssets.Ionic   # Ionic JS bundle + CSS
dotnet add package IonBlazor.StaticAssets.Ionic.Svg  # Ionic SVG icons (optional)
```

Then point your host page at `_content/IonBlazor/...` — see the [IonBlazor package README](https://www.nuget.org/packages/IonBlazor) for the snippet.

Add the namespaces to `_Imports.razor`:

```razor
@using IonBlazor.Components
@using IonBlazor.Components.Abstractions
@using IonBlazor.Services
```

## Controllers → Services migration

The four overlay controllers (`IonAlertController`, `IonActionSheetController`, `IonToastController`,
`IonLoadingController`) have been replaced by **scoped DI services** (`IonAlertService`,
`IonActionSheetService`, `IonToastService`, `IonLoadingService`) with instance methods. Each old
controller is retained as an `[Obsolete(error: true)]` stub so existing call sites fail the build
with a pointer to this section.

### Why

The legacy controllers worked by being rendered once at the app root (`<IonAlertController/>` in
`App.razor` / `MainLayout.razor`) so they could `[Inject]` `IJSRuntime` and stash it in a `static`
field that every `PresentAsync` static call then read.

On Blazor WebAssembly there's one circuit per browser tab, so this happens to work. On **Blazor
Server**, `IJSRuntime` is per-circuit — the static field gets overwritten by whichever circuit
renders last, and every other connected user's calls then dispatch through the wrong circuit. The
DI version eliminates the static state entirely.

### Register the services

```csharp
// Program.cs / MauiProgram.cs
builder.Services.AddIonBlazor();
```

`AddIonBlazor()` registers all four services scoped — one call covers the lot.

### Migration — same shape for all four

The pattern is identical for every controller. Pick the appropriate row:

| Legacy controller          | Injected service          |
|----------------------------|---------------------------|
| `IonAlertController`       | `IonAlertService`         |
| `IonActionSheetController` | `IonActionSheetService`   |
| `IonToastController`       | `IonToastService`         |
| `IonLoadingController`     | `IonLoadingService`       |

### Before — `IonAlertController` (legacy, deprecated)

```razor
@* Main.razor or MainLayout.razor *@
<IonAlertController />

@* Some component *@
@code {
    private async Task Show()
    {
        await IonAlertController.PresentAsync(options =>
        {
            options.Header = "Heads up";
            options.Message = "Hello.";
        });
    }
}
```

### After — `IonAlertService` (injected)

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

Then **remove** every `<IonAlertController/>`, `<IonActionSheetController/>`, `<IonToastController/>`,
and `<IonLoadingController/>` tag from your root layout / `App.razor`.

### `IonLoadingController.Create` (sync) was removed

The synchronous `Create(...)` wrapper on `IonLoadingController` (which internally called
`CreateAsync(...).GetAwaiter().GetResult()`) has been dropped. Call `IonLoadingService.CreateAsync(...)`
directly — it returns the same `IonLoadingReference` type as before.

### Compile-time signal

Every `Ion*Controller` is now an `[Obsolete(error: true)]` stub. Old markup tags and static calls
fail the build with a message pointing at this section. Migrate, register `AddIonBlazor()`, and
delete the tags from your root layout.

## MAUI Hybrid

In a MAUI Hybrid solution with multiple Razor projects, only **one** project can ship the static assets — otherwise MAUI's packaging fails with a duplicate-static-asset error.

Pick one project to carry the assets (typically the MAUI host) and have it reference either:

- the bundle — [`IonBlazor`](https://www.nuget.org/packages/IonBlazor), **or**
- the granular packages — [`IonBlazor.StaticAssets`](https://www.nuget.org/packages/IonBlazor.StaticAssets) + [`IonBlazor.StaticAssets.Ionic`](https://www.nuget.org/packages/IonBlazor.StaticAssets.Ionic) (+ [`IonBlazor.StaticAssets.Ionic.Svg`](https://www.nuget.org/packages/IonBlazor.StaticAssets.Ionic.Svg) for icons), plus this package for the wrappers.

Every other project in the solution references only `IonBlazor.Components` — no assets, just the C# types — so the wrappers stay available everywhere while the assets ship exactly once.

## Supported frameworks

`net8.0`, `net9.0`, `net10.0`.

## Links

- Source: <https://github.com/wdcossey/ionblazor>
- Issues: <https://github.com/wdcossey/ionblazor/issues>

## License

MIT.