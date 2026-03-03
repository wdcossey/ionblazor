# IonBlazor

A C# Razor component library that wraps [Ionic Framework](https://ionicframework.com/) web components for use with Blazor, MAUI Hybrid, ASP.NET Core, Photino.Blazor, and anything else that supports Razor components.

## Project Structure

```
ionblazor/
├── src/
│   ├── IonBlazor.Components/        # Core Razor components (.razor + .razor.cs)
│   └── IonBlazor.StaticAssets/      # JS interop files and bundled Ionic static assets (wwwroot/)
├── tests/
│   └── IonBlazor.UnitTests/         # bUnit tests
└── samples/
    └── IonBlazor.KitchenSink/       # Mirror of Ionic sample site, used as reference for tests
```

## Two Ecosystems

This project spans C# and JS. The npm side exists **only** to bundle Ionic's static assets into the NuGet package. It is not a requirement — users can opt out and reference Ionic via `<script>` tags themselves, pointing to any Ionic version they choose.

- npm/bundling: managed in `IonBlazor.StaticAssets`
- Ionic version is pinned in `package.json`
- Target frameworks: `net6.0;net7.0;net8.0;net9.0;net10.0`
  - `net6.0` — EOL November 12, 2024 — remove in a future revision
  - `net7.0` — EOL May 14, 2024 — remove in a future revision
  - `net8.0` — LTS, EOL November 10, 2026
  - `net9.0` — STS, EOL November 10, 2026
  - `net10.0` — LTS, EOL ~November 2027
- Build order: npm assets do **not** need to build before the C# project for development

## Component Design Principles

- Components are hand-authored Razor components, one per Ionic component
- Parameters closely mirror their Ionic/JS counterparts, tweaked where necessary for Razor conventions
- **Do not add redundant event handlers** — e.g. no `OnClick`, Razor already has this
- `AdditionalAttributes` (`[Parameter(CaptureUnmatchedValues = true)]`) is intentional on all components — users can pass arbitrary HTML attributes
- Almost all Ionic event handlers are implemented as `EventCallback` or `EventCallback<T>` parameters
- Nullable booleans (`bool?`) use `.AsString()` extension to render as `"true"`/`"false"` or omit the attribute entirely when null
- `Mode` defaults to `IonMode.Default` where applicable

## Base Classes

- `IonComponent` — base for all components. Handles `IJSRuntime` injection, `JsComponent` lazy module loading, `IonElement` (`ElementReference`), and `DisposeAsync`
- `IonContentComponent` — extends `IonComponent` with `ChildContent (RenderFragment)`
- Interfaces: `IIonComponent`, `IIonContentComponent`, `IIonModeComponent`, `IIonColorComponent`

## JS Interop Pattern

JS modules are loaded lazily per component:

```csharp
internal override string JsImportName => nameof(IonAlert);  // resolves to "IonAlert"
```

This imports `./_content/IonBlazor/IonAlert.js` — **the JS filename must match the PascalCase component name exactly**. All files in `IonBlazor.StaticAssets/wwwroot/` use PascalCase (e.g. `IonAlert.js`, `IonAccordionGroup.js`). This was a known cross-platform casing issue (Windows is forgiving, Linux is not) — now resolved.

Event listeners are wired in `OnAfterRenderAsync` via `AttachIonListenersAsync`, which imports `common.js` and calls `attachListeners`.

JS methods are invoked directly via `JsComponent`:

```csharp
await JsComponent.InvokeVoidAsync("present", IonElement);
await JsComponent.InvokeAsync<bool>("dismiss", IonElement);
```

## Generator

`tools/IonBlazor.Generator/` — one-shot `dotnet run` tool. Reads `node_modules/@ionic/docs/core.json` (Stencil metadata), outputs to `bin/Debug/net10.0/{ComponentName}/`.

**To run:** `dotnet run --project tools/IonBlazor.Generator/`

### Output files per component
- `{ComponentName}.razor` — template with `@inherits`, `@ChildContent`, `.AsString()` for `bool?`
- `{ComponentName}.Properties.razor.cs` — `public sealed partial class` with base class + shared interfaces
- `{ComponentName}.Methods.razor.cs` — only emitted if the component has supported JS methods
- `{ComponentName}.Methods.js` — only emitted if the component has any methods
- `{ComponentName}.Events.razor.cs` — only emitted if the component has events (stub only — events need manual implementation)
- `I{ComponentName}.cs` — **skipped** (commented out); handwritten code uses shared `IIonModeComponent`/`IIonColorComponent` instead

### Generator design decisions (as of current iteration)
- Base class: `IonContentComponent` by default; `IonComponent` for components in `NoChildContentComponents` set (`IonBackdrop`, `IonProgressBar`, `IonRefresherContent`, `IonSkeletonText`, `IonSpinner`, `IonRippleEffect`)
- Shared interfaces: auto-detected — `mode` prop → `IIonModeComponent`, `color` prop → `IIonColorComponent`
- `bool?` props: rendered as `attribute="@PropName.AsString()"` in the razor template
- `Mode` property: always `get; set; } = IonMode.Default` (mutable + default)
- `JsImportName`: emitted in `.Properties.razor.cs` when any supported methods exist
- Events generation: stubs only (incomplete type info for `IonicEventCallback<>`); `OnAfterRenderAsync`, `DisposeAsync`, and `AttachIonListenersAsync` wiring must be added manually

### Known gaps (still manual)
- `bool` (non-nullable) non-optional params (e.g., `IonBackdrop.StopPropagation = true`) — Stencil marks these as non-optional but they have JS defaults; needs `bool` not `bool?`
- Default string values (e.g., `ToggleIconSlot = IonAccordionToggleIconSlot.Default`) — not in metadata
- `CascadingParameter Parent` for child components — not in metadata
- Event callback wiring (`OnAfterRenderAsync`, `DisposeAsync`) — too complex to auto-generate fully
- Method parameter types in C# signature — parameters names pass through but types are not mapped yet
- The `value` attribute on some components is a computed property (`ValueTask<T>`) not a simple `[Parameter]`
- Razor attribute alignment style — generator uses 4-space indent; handwritten aligns to `@ref` position (cosmetic)

## Future Direction

- **Generator**: The goal is to generate components from Ionic's Stencil metadata. The hand-authored components are the reference implementation and gold standard.
  - Stencil metadata source: `node_modules/@ionic/docs/core.json`
  - Parser: `System.Text.Json`
  - The manifest is versioned with Ionic and updates automatically when the Ionic version in `package.json` is bumped
  - Generator type: one-shot `.cs` script (runnable via `dotnet run`)
- **bUnit tests**: Tests should eventually be generated alongside components. The existing tests define the patterns to follow.

## Testing

- **bUnit**: `2.6.2`
- **Verify**: `Verify.XunitV3`
- **NSubstitute**: `5.3.0`
- **FluentAssertions**: `8.8.0`

### Test Infrastructure

**`GlobalSetup.cs`** — assembly-wide `[ModuleInitializer]` that scrubs `blazor:elementReference` GUIDs from Verify snapshots. Applied once for the whole assembly, no need to add to individual test classes.

**`IonTestContext`** — base class for all test classes, extends `BunitContext`. Handles:
- `common.js` module setup with `attachListeners`
- `SetupComponentModule(string, Action<BunitJSModuleInterop>)` helper
- `SetupComponentModule<T>(Action<BunitJSModuleInterop>)` generic helper (uses `typeof(T).Name` — consistent with `nameof()` in components)
- `CreateJsComponentMock(out IJSObjectReference)` helper for NSubstitute-based JS method tests

### Test Class Structure

```csharp
public class IonAlertTests : IonTestContext
{
    public IonAlertTests()
    {
        SetupComponentModule<IonAlert>(module =>
        {
            module.SetupVoid("addButtons", _ => true).SetVoidResult();
            module.SetupVoid("addInputs", _ => true).SetVoidResult();
            module.Setup<bool>("dismiss", _ => true);
            module.SetupVoid("present", _ => true).SetVoidResult();
        });
    }
}
```

### bUnit JSInterop Notes (v2.6.2)

- Use `SetupVoid(...).SetVoidResult()` — the `SetVoidResult()` call is required in bUnit 2.6.x, the handler will hang indefinitely without it
- `common.js` is imported via `await using` in `AttachIonListenersAsync` — set `module.Mode = JSRuntimeMode.Loose` to prevent the dispose from throwing in tests (handled automatically by `IonTestContext.SetupComponentModule`)
- The `await using` on the `common.js` module reference will silently kill `OnAfterRenderAsync` if disposal isn't handled — this manifests as JS lifecycle calls (e.g. `addButtons`) never being invoked

### Test Patterns

**Default render — snapshot via Verify:**
```csharp
[Fact]
public async Task IonAlertRendersCorrectly()
{
    var cut = Render<IonAlert>();
    await Verify(cut.Markup);
}
```

**Boolean parameter:**
```csharp
[Theory]
[InlineData(true)]
[InlineData(false)]
public async Task WithAnimated_RendersCorrectly(bool value)
{
    VerifySettings settings = new();
    settings.UseTextForParameters($"value={value}");
    var cut = Render<IonAlert>(parameters => parameters
        .Add(p => p.Animated, value));
    await Verify(cut.Markup, settings);
}
```

**String/enum parameter:**
```csharp
[Theory]
[InlineData(IonMode.iOS)]
[InlineData(IonMode.MaterialDesign)]
public async Task WithMode_RendersCorrectly(string mode)
{
    VerifySettings settings = new();
    settings.UseTextForParameters($"mode={mode}");
    var cut = Render<IonAlert>(parameters => parameters
        .Add(p => p.Mode, mode));
    await Verify(cut.Markup, settings);
}
```

**JS interop method test — bUnit invocation tracking + FluentAssertions:**
```csharp
[Fact]
public async Task PresentAsync_InvokesJsMethod_WhenCalled()
{
    var cut = Render<IonAlert>();

    await cut.Instance.PresentAsync();

    JSRuntimeInvocation invocation = JSInterop.Invocations["present"].Single();
    invocation.Arguments[0]
        .Should().BeAssignableTo<ElementReference>()
        .Which.Should().Be(cut.Instance.IonElement);
}
```

**JS lifecycle test — called in `OnAfterRenderAsync`, same invocation tracking:**
```csharp
[Fact]
public void WithButtonsBuilder_InvokesAddButtons_WhenRendered()
{
    Render<IonAlert>(parameters => parameters
        .Add(p => p.ButtonsBuilder, builder => builder
            .Add(new AlertButton { Text = "OK", Role = "confirm" })));

    JSRuntimeInvocation invocation = JSInterop.Invocations["addButtons"].Single();
    invocation.Arguments[1].Should().BeAssignableTo<IReadOnlyList<IAlertButton>>()
        .Which.Count.Should().Be(1);
}
```

**JsImportName assertion:**
```csharp
[Fact]
public void Assert_JsImportName()
{
    var cut = Render<IonAlert>();
    Assert.Equal(nameof(IonAlert), cut.Instance.JsImportName);
}
```

### What to Test Per Component

For every component, cover:
- Default render (no parameters)
- Each boolean parameter (`true` and `false`)
- Each string/enum parameter (`[Theory]` + `[InlineData]`)
- `AdditionalAttributes` passthrough
- `ChildContent` (if applicable)
- Each JS method (`InvokeVoidAsync` / `InvokeAsync<T>`) via `JSInterop.Invocations` + FluentAssertions
- `JsImportName` assertion
- Builder patterns where present (e.g. `ButtonsBuilder`, `InputsBuilder`)

### Reference Test Files

- `IonAccordionTests.cs` — clean baseline, no JS interop
- `IonAccordionGroupTests.cs` — JS interop methods pattern
- `IonAlertTests.cs` — full pattern including builders and lifecycle JS calls
- `IonBlazor.KitchenSink` — real-world usage examples for each component
