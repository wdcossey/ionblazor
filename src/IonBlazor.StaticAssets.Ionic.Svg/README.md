# IonBlazor.StaticAssets.Ionic.Svg

> Ionic Framework's SVG icon set, packaged as a Blazor static-web-asset library.

Ships the SVGs that `<IonIcon>` references so icons render without an internet connection. Files are served from `_content/IonBlazor/@ionic/core/dist/ionic/svg/`.

Most users get this transitively via [`IonBlazor`](https://www.nuget.org/packages/IonBlazor). Install it directly if you've assembled the package set yourself and want bundled icons rather than CDN-hosted ones.

## Install

```bash
dotnet add package IonBlazor.StaticAssets.Ionic.Svg
```

## Use

`<IonIcon Name="…" />` picks up the SVGs automatically once the package is referenced — no host-page changes required.

## Supported frameworks

`net6.0`, `net7.0`, `net8.0`, `net9.0`, `net10.0`.

## Links

- Source: <https://github.com/wdcossey/ionblazor>
- Issues: <https://github.com/wdcossey/ionblazor/issues>
- Ionicons: <https://ionic.io/ionicons>

## License

MIT.
