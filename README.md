# IonBlazor

[Ionic Framework](https://ionicframework.com/) web components wrapped as Blazor / Razor components. Works in Blazor WebAssembly, Blazor Server, MAUI Hybrid, ASP.NET Core, Photino.Blazor, and anything else that hosts Razor components.

End-user documentation lives on each NuGet package's listing page; the per-package READMEs are kept alongside each csproj (e.g. `src/IonBlazor/README.md`). The file you're reading now is for contributors working on the repo itself.

## Project layout

```
ionblazor/
├── src/
│   ├── IonBlazor/                       Bundle package — pulls in Components + StaticAssets
│   ├── IonBlazor.Components/            Hand-authored Razor wrappers for each Ionic web component
│   ├── IonBlazor.StaticAssets/          JS interop modules (compiled from TypeScript)
│   ├── IonBlazor.StaticAssets.Ionic/    Bundled @ionic/core CSS + JavaScript
│   ├── IonBlazor.StaticAssets.Ionic.Svg/ Bundled Ionic SVG icons
│   └── IonBlazor.Mcp/                   MCP server CLI tool (.NET tool: ionblazor-mcp)
├── samples/
│   ├── IonBlazor.KitchenSink/           MAUI Hybrid demo (Windows-only TFM in the matrix)
│   ├── IonBlazor.Barebones/             MAUI Hybrid minimal sample
│   └── IonBlazor.Wasm/                  Blazor WebAssembly demo
├── tests/
│   └── IonBlazor.UnitTests/             bUnit + Verify + NSubstitute
├── tools/
│   └── IonBlazor.Generator/             One-shot codegen from @ionic/docs metadata
└── assets/                              Logos used as PackageIcon
```

## Prerequisites

- Node.js 22+ and npm
- .NET SDK 10.x (pinned in `global.json`)

`IonBlazor.KitchenSink` and `IonBlazor.Barebones` are MAUI Hybrid and only build on Windows with the MAUI workload — see [MAUI samples](#maui-samples) below.

## First build

```bash
npm install            # installs @ionic/core, @ionic/docs, @maskito/core, typescript
npm run build          # tsc → src/IonBlazor.StaticAssets/wwwroot/*.js
dotnet restore IonBlazor.slnx
dotnet build IonBlazor.slnx
```

The npm step **must** run before `dotnet build`. The JS interop files in `src/IonBlazor.StaticAssets/wwwroot/` are produced from TypeScript sources by `tsc` and are not committed (they're gitignored). `IonBlazor.StaticAssets.csproj` has an MSBuild guard that fails the build with a clear message if you forget.

## Solution layout

- `IonBlazor.slnx` — full solution, used in Visual Studio / Rider / `dotnet build` locally.
- `IonBlazor.CI.slnf` — solution filter consumed by CI. Excludes the MAUI samples so CI runners (Ubuntu, no MAUI workload) can build.

## Tests

```bash
dotnet test IonBlazor.slnx
```

Test framework: bUnit 2.6.x + Verify.XunitV3 + NSubstitute + FluentAssertions. New tests go in `tests/IonBlazor.UnitTests/Components/<IonComponent>/`.

## Packaging

CI publishes via `.github/workflows/nuget-publish.yml` — `workflow_dispatch` only, with a `publish` checkbox that defaults to false (dry run uploads artifacts; tick the box to push to NuGet.org).

Version is derived from `@ionic/core` in `package.json`:

> `0.{ionicMajor}{ionicMinor}{ionicPatch}.{revision}` — e.g. `@ionic/core@8.8.7` → `0.887.0`.

Override with the `version` workflow input for a one-off release.

To pack locally:

```bash
npm run build
dotnet pack src/IonBlazor.Components/IonBlazor.Components.csproj -c Release --output nupkgs
# ...and so on for each src project. Mcp tool packs via PackAsTool=true.
```

## CI

- `.github/workflows/ci.yml` — runs on every PR / push to `master`. npm build → `dotnet restore/build/test` against `IonBlazor.CI.slnf`.
- `.github/workflows/nuget-publish.yml` — manual dispatch only. Packs every shipping project and optionally publishes.

## MAUI samples

`IonBlazor.KitchenSink` and `IonBlazor.Barebones` use `<UseMaui>true</UseMaui>` with a `<TargetFrameworks>;</TargetFrameworks>` trick that only resolves to a real TFM (`net9.0-windows10.0.19041.0`) on Windows. On Linux they have no buildable TFM, which trips an SDK 10.0.300 bug in `GetAllRuntimeIdentifiers`. CI excludes them via `IonBlazor.CI.slnf`. To build them locally:

```bash
dotnet workload install maui
dotnet build samples/IonBlazor.KitchenSink/IonBlazor.KitchenSink.csproj
```

## Generator

`tools/IonBlazor.Generator/` is a one-shot codegen driven by `node_modules/@ionic/docs/core.json`. Hand-authored components remain the reference; the generator is a work-in-progress aid.

```bash
dotnet run --project tools/IonBlazor.Generator/
# Output lands in tools/IonBlazor.Generator/bin/Debug/net10.0/<ComponentName>/
```

## Useful files

- `CLAUDE.md` — orientation for AI agents working in this repo (component design rules, test patterns, base class hierarchy, gotchas).
- `IONIC_EVENTS.md` — reference for the Ionic event names each component listens for.
- `CommonNuget.props` — shared MSBuild properties for the Razor-class-library packages.

## License

MIT. See `LICENSE`.
