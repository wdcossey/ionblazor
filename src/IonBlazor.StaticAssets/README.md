# IonBlazor.StaticAssets

> JavaScript interop modules used by [`IonBlazor.Components`](https://www.nuget.org/packages/IonBlazor.Components).

Ships the per-component JavaScript glue (e.g. `IonAccordionGroup.js`, `IonAlert.js`, `common.js`) that the C# wrappers invoke at runtime. These files are served from `_content/IonBlazor/` to your Blazor host.

Most users don't install this directly — it comes in transitively via [`IonBlazor`](https://www.nuget.org/packages/IonBlazor) (the bundle) or as an explicit dependency alongside [`IonBlazor.Components`](https://www.nuget.org/packages/IonBlazor.Components).

## Install

```bash
dotnet add package IonBlazor.StaticAssets
```

## When you need this package

You're installing [`IonBlazor.Components`](https://www.nuget.org/packages/IonBlazor.Components) directly (rather than the [`IonBlazor`](https://www.nuget.org/packages/IonBlazor) bundle). The component wrappers can't talk to Ionic without these interop modules.

You will likely also want:

- [`IonBlazor.StaticAssets.Ionic`](https://www.nuget.org/packages/IonBlazor.StaticAssets.Ionic) — the Ionic JS bundle and CSS.
- [`IonBlazor.StaticAssets.Ionic.Svg`](https://www.nuget.org/packages/IonBlazor.StaticAssets.Ionic.Svg) — Ionic SVG icons.

## What's inside

A static-web-asset Razor class library. After install, files are available at:

```
_content/IonBlazor/IonAccordionGroup.js
_content/IonBlazor/IonAlert.js
_content/IonBlazor/common.js
...
```

You normally don't reference them directly — the C# component types in `IonBlazor.Components` import them through Blazor's JS interop.

## Supported frameworks

`net8.0`, `net9.0`, `net10.0`.

## Links

- Source: <https://github.com/wdcossey/ionblazor>
- Issues: <https://github.com/wdcossey/ionblazor/issues>

## License

MIT.