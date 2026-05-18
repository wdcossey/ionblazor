# IonBlazor.StaticAssets.Ionic

> The [`@ionic/core`](https://www.npmjs.com/package/@ionic/core) JavaScript bundle and CSS, packaged as a Blazor static-web-asset library.

Use this package to ship Ionic's runtime with your app instead of loading it from a CDN. Files are served from `_content/IonBlazor/@ionic/core/`.

Most users get this transitively via [`IonBlazor`](https://www.nuget.org/packages/IonBlazor). Install it directly if you've referenced [`IonBlazor.Components`](https://www.nuget.org/packages/IonBlazor.Components) on its own and want the Ionic runtime bundled.

## Install

```bash
dotnet add package IonBlazor.StaticAssets.Ionic
```

## Setup

Reference the bundled assets from your host page (`wwwroot/index.html`, `_Host.cshtml`, or `App.razor`):

```html
<script type="module" src="_content/IonBlazor/@ionic/core/dist/ionic/ionic.esm.js"></script>

<link rel="stylesheet" href="_content/IonBlazor/@ionic/core/css/core.css" />
<link rel="stylesheet" href="_content/IonBlazor/@ionic/core/css/ionic.bundle.css" />
```

For icons, also install [`IonBlazor.StaticAssets.Ionic.Svg`](https://www.nuget.org/packages/IonBlazor.StaticAssets.Ionic.Svg).

## What's inside

The contents of `@ionic/core`'s `dist/ionic` (JavaScript) and `css` (stylesheets) directories, including:

- `_content/IonBlazor/@ionic/core/dist/ionic/ionic.esm.js`
- `_content/IonBlazor/@ionic/core/css/core.css`
- `_content/IonBlazor/@ionic/core/css/ionic.bundle.css`
- `_content/IonBlazor/@ionic/core/css/palettes/dark.class.css`

The Ionic version is pinned by the IonBlazor release — see the package version for the corresponding `@ionic/core` version.

## Supported frameworks

`net8.0`, `net9.0`, `net10.0`.

## Links

- Source: <https://github.com/wdcossey/ionblazor>
- Issues: <https://github.com/wdcossey/ionblazor/issues>
- Ionic Framework: <https://ionicframework.com/>

## License

MIT.