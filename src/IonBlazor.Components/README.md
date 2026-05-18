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