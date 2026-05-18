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

This project spans C# and JS. The npm side compiles the JS interop modules from `src/IonBlazor.StaticAssets/Typescript/*.ts` to `src/IonBlazor.StaticAssets/wwwroot/*.js` via `tsc` (`npm run build`).

The bundled `@ionic/core` JS and CSS that ship in `IonBlazor.StaticAssets.Ionic` come straight from `node_modules/@ionic/core/` — copied by an MSBuild target in that csproj, no JS bundler involved. End users of the NuGet packages can opt out and reference Ionic via `<script>` tags themselves (CDN or otherwise) by skipping the `IonBlazor.StaticAssets.Ionic*` packages.

- npm scripts and Ionic pin: `package.json` at the repo root
- TypeScript sources: `src/IonBlazor.StaticAssets/Typescript/` (compiled outputs are gitignored)
- Target frameworks: `net6.0;net7.0;net8.0;net9.0;net10.0`
  - `net6.0` — EOL November 12, 2024 — remove in a future revision
  - `net7.0` — EOL May 14, 2024 — remove in a future revision
  - `net8.0` — LTS, EOL November 10, 2026
  - `net9.0` — STS, EOL November 10, 2026
  - `net10.0` — LTS, EOL ~November 2027
- **Build order: `npm install && npm run build` MUST run before `dotnet build`.** `IonBlazor.StaticAssets.csproj` has an MSBuild guard (`EnsureTypeScriptCompiled`) that fails early with a clear message if `wwwroot/common.js` is missing.

## Component Design Principles

- Components are hand-authored Razor components, one per Ionic component
- Parameters closely mirror their Ionic/JS counterparts, tweaked where necessary for Razor conventions
- **Do not add redundant event handlers** — e.g. no `OnClick`, Razor already has this
- `AdditionalAttributes` (`[Parameter(CaptureUnmatchedValues = true)]`) is intentional on all components — users can pass arbitrary HTML attributes
- Almost all Ionic event handlers are implemented as `EventCallback` or `EventCallback<T>` parameters
- Nullable booleans (`bool?`) use `.AsString()` extension to render as `"true"`/`"false"` or omit the attribute entirely when null
- `Mode` defaults to `IonMode.Default` where applicable

## Base Classes

The hierarchy is a 4-way split. JS plumbing and `ChildContent` are orthogonal — pick the one that
combines what the component actually needs. Components that don't need JS pay no cost for the
`Lazy<Task<IJSObjectReference>>` field.

```
IonComponent                          // IJSRuntime, IonElement, AddEventListener, DisposeAsync
├── IonContentComponent               // + ChildContent (RenderFragment)
└── IonJsComponent                    // + abstract JsImportName, lazy JsComponent module
    └── IonJsContentComponent         // + ChildContent
```

- `IonComponent` — base for everything. Handles `IJSRuntime` injection, `IonElement`
  (`ElementReference`), `AddEventListener`, and `DisposeAsync`. No JS module loading.
- `IonContentComponent` — extends `IonComponent` with `ChildContent (RenderFragment)`. For
  components that take child content but never call JS methods.
- `IonJsComponent` — extends `IonComponent` with an abstract `JsImportName` (must be overridden)
  and a lazy `JsComponent` (`Lazy<Task<IJSObjectReference>>`). For overlays and other components
  that invoke JS methods but have no `ChildContent` (e.g. `IonAlert`, `IonToast`, `IonActionSheet`,
  `IonSearchbar`).
- `IonJsContentComponent` — both JS plumbing **and** `ChildContent`. The most common case for
  interactive components (`IonInput`, `IonModal`, `IonAccordionGroup`, `IonSelect`, …).
- Interfaces: `IIonComponent`, `IIonContentComponent`, `IIonModeComponent`, `IIonColorComponent`.

## JS Interop Pattern

`IonJsComponent` (and `IonJsContentComponent`) exposes a lazy module reference. Each subclass
**must** override `JsImportName` — it's abstract:

```csharp
internal override string JsImportName => nameof(IonAlert);  // resolves to "IonAlert"
```

This imports `./_content/IonBlazor/IonAlert.js` — **the JS filename must match the PascalCase component name exactly**. All files in `IonBlazor.StaticAssets/wwwroot/` use PascalCase (e.g. `IonAlert.js`, `IonAccordionGroup.js`). This was a known cross-platform casing issue (Windows is forgiving, Linux is not) — now resolved.

Event listeners are wired in `OnAfterRenderAsync` via `AttachIonListenersAsync`, which imports `common.js` and calls `attachListeners`. This works from any `IonComponent` subclass (it goes through `JsRuntime` directly, not `JsComponent`), so components that listen for Ionic events without invoking JS methods can still use `IonComponent` / `IonContentComponent`.

JS methods are invoked directly via `JsComponent` (only available on `IonJsComponent` subclasses):

```csharp
await JsComponent.InvokeVoidAsync("present", IonElement);
await JsComponent.InvokeAsync<bool>("dismiss", IonElement);
```

## Two-Way Binding (`@bind-Value` / `@bind-Checked`)

Several form-style components support Blazor `@bind`. The pattern is a parallel `…Changed` /
`…Input` `EventCallback` that fires *alongside* the existing `IonChange` / `IonInput`:

- `ValueChanged` (and `CheckedChanged` for `IonToggle` / `IonCheckbox`) fires on commit — i.e.
  whenever `IonChange` fires. Enables plain `@bind-Value` / `@bind-Checked`.
- `ValueInput` fires on every keystroke / drag — i.e. whenever `IonInput` fires. Enables
  `@bind-Value:event="ValueInput"` for live two-way binding on `IonInput`, `IonTextarea`,
  `IonSearchbar`, `IonRange`.

The callback handler sets the local `Value` / `Checked` *before* invoking the callback so external
bind targets see the up-to-date state:

```csharp
Value = value;
await ValueChanged.InvokeAsync(value);
await IonChange.InvokeAsync(new IonInputChangeEventArgs { Sender = this, Value = value, ... });
```

Components currently wired for `@bind`: `IonInput`, `IonTextarea`, `IonSearchbar`, `IonRange`,
`IonDateTime`, `IonRadioGroup`, `IonSelect<T>`, `IonCheckbox`, `IonToggle`, `IonPickerColumn`.

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
- Base class: `IonContentComponent` by default; `IonComponent` for components in `NoChildContentComponents` set (`IonBackdrop`, `IonProgressBar`, `IonRefresherContent`, `IonSkeletonText`, `IonSpinner`, `IonRippleEffect`). **Note:** this predates the JS/no-JS split — the generator still picks between only the two non-JS bases. Handwritten components that use JS now derive from `IonJsComponent` / `IonJsContentComponent` instead, so generator output for JS-backed components requires manual base-class adjustment until the generator is taught the 4-way choice.
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
- `InvokeIonEventAsync<TArgs>(string eventName, TArgs args)` — locates the `DotNetObjectReference` registered for an event via the `attachListeners` JS interop call and invokes its callback. Use this to simulate a fired Ionic event end-to-end (e.g. `ionChange`, `ionInput`) and assert that the C# side dispatches both `IonChange` *and* the parallel `ValueChanged` / `CheckedChanged` callback.

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

**`@bind-Value` / `@bind-Checked` — parallel callback test via `InvokeIonEventAsync`:**
```csharp
[Fact]
public async Task IonChange_FiresBoth_ValueChangedAndIonChange()
{
    string? capturedValue = null;
    IonInputChangeEventArgs? capturedArgs = null;

    var cut = Render<IonInput>(parameters => parameters
        .Add(p => p.ValueChanged, v => capturedValue = v)
        .Add(p => p.IonChange, args => capturedArgs = args));

    var payload = new JsonObject
    {
        ["detail"] = new JsonObject
        {
            ["value"] = "hello",
            ["event"] = new JsonObject { ["isTrusted"] = true }
        }
    };
    await InvokeIonEventAsync("ionChange", payload);

    capturedValue.Should().Be("hello");
    capturedArgs!.Value.Should().Be("hello");
    cut.Instance.Value.Should().Be("hello");
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
- `JsImportName` assertion (only on `IonJsComponent`-derived components)
- Builder patterns where present (e.g. `ButtonsBuilder`, `InputsBuilder`)
- `@bind-Value` / `@bind-Checked` callbacks where present — assert that `ValueChanged` /
  `ValueInput` / `CheckedChanged` fires alongside `IonChange` / `IonInput`, and that the
  component's `Value` / `Checked` property is updated **before** the callback fires

### Reference Test Files

- `IonAccordionTests.cs` — clean baseline, no JS interop
- `IonAccordionGroupTests.cs` — JS interop methods pattern
- `IonAlertTests.cs` — full pattern including builders and lifecycle JS calls
- `IonBlazor.KitchenSink` — real-world usage examples for each component
