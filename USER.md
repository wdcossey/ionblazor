# Generator Verification Tasks

These are open questions the generator can't resolve automatically from the Stencil metadata.
Tick `[x]` when verified and I'll incorporate the findings.

---

## 1 — Base Class: `IonComponent` vs `IonContentComponent`

The generator defaults to `IonContentComponent` for everything. Components without ChildContent
must be in the `NoChildContentComponents` set in `Program.cs`.

My current set of 6 is incomplete. From searching the source, the following all extend `IonComponent`
directly (not `IonContentComponent`):

- [ ] `IonActionSheet` — overlays typically have no ChildContent
- [ ] `IonAlert`
- [ ] `IonBackButton`
- [ ] `IonBackdrop`
- [ ] `IonDateTimeButton`
- [ ] `IonIcon`
- [ ] `IonImg`
- [ ] `IonInfiniteScrollContent`
- [ ] `IonInputPasswordToggle`
- [ ] `IonLoading` *(not in grep output above — please confirm)*
- [ ] `IonMenuButton`
- [ ] `IonPickerLegacy`
- [ ] `IonProgressBar` *(confirm — not in grep output)*
- [ ] `IonRefresherContent`
- [ ] `IonRippleEffect` *(confirm)*
- [ ] `IonSearchbar`
- [ ] `IonSkeletonText`
- [ ] `IonSpinner`
- [ ] `IonToast`

**Task:** Confirm the list above is correct and complete. Are there any that *do* have ChildContent
and should NOT be in the list? Any missing from it?

---

## 2 — Method Signatures with Parameters

The generator currently emits `public ValueTask FooAsync()` with no C# parameters, but many
methods need them. The Stencil metadata gives JS parameter names and types, but the C# types
need verifying.

Below are all methods with parameters from `core.json`. For each one: confirm the C# method
signature (name, parameters, return type) matches the handwritten component, or note if it's
not implemented at all.

```
IonActionSheet.dismiss(data: any, role: string | undefined)         -> ValueTask<bool>
IonAlert.dismiss(data: any, role: string | undefined)               -> ValueTask<bool>
IonContent.scrollByPoint(x: number, y: number, duration: number)    -> ValueTask
IonContent.scrollToBottom(duration: number)                         -> ValueTask
IonContent.scrollToPoint(x, y, duration)                            -> ValueTask
IonContent.scrollToTop(duration: number)                            -> ValueTask
IonDatetime.cancel(closeOverlay: boolean)                           -> ValueTask
IonDatetime.confirm(closeOverlay: boolean)                          -> ValueTask
IonDatetime.reset(startDate: string | undefined)                    -> ValueTask
IonInputOtp.setFocus(index: number | undefined)                     -> ValueTask
IonItemSliding.open(side: Side | undefined)                         -> ValueTask
IonLoading.dismiss(data: any, role: string | undefined)             -> ValueTask<bool>
IonMenu.close(animated: boolean, role: string | undefined)          -> ValueTask<bool>
IonMenu.open(animated: boolean)                                     -> ValueTask<bool>
IonMenu.setOpen(shouldOpen: boolean, animated: boolean, role?)      -> ValueTask<bool>
IonMenu.toggle(animated: boolean)                                   -> ValueTask<bool>
IonModal.dismiss(data: any, role: string | undefined)               -> ValueTask<bool>
IonModal.setCurrentBreakpoint(breakpoint: number)                   -> ValueTask
IonPickerLegacy.dismiss(data: any, role: string | undefined)        -> ValueTask<bool>
IonPickerLegacy.getColumn(name: string)                             -> (unsupported return)
IonPopover.dismiss(data: any, role: string | undefined, dismissParentPopover: boolean) -> ValueTask<bool>
IonPopover.present(event: MouseEvent | PointerEvent | ...)          -> ValueTask
IonReorderGroup.complete(listOrReorder: boolean | any[] | undefined)-> (unsupported return)
IonRippleEffect.addRipple(x: number, y: number)                     -> (unsupported return Promise<()=>void>)
IonSelect.open(event: UIEvent | undefined)                          -> (unsupported return)
IonTabs.getTab(tab: string | HTMLIonTabElement)                     -> (unsupported return)
IonTabs.select(tab: string | HTMLIonTabElement)                     -> ValueTask<bool>
IonToast.dismiss(data: any, role: string | undefined)               -> ValueTask<bool>
```

Also, `IonApp.setFocus(elements: HTMLElement[])` — is this even implemented in the handwritten component?

**Task:** For each row, tick if the handwritten C# signature matches expectations, or note the
actual C# signature. Flag any that are missing or have a different name.

- [ ] `IonActionSheet.DismissAsync(object? data = null, string? role = null)`
- [ ] `IonAlert.DismissAsync(object? data = null, string? role = null)`
- [ ] `IonContent.ScrollByPointAsync(int x, int y, int duration)`
- [ ] `IonContent.ScrollToBottomAsync(int duration)`
- [ ] `IonContent.ScrollToPointAsync(int? x, int? y, int? duration)`
- [ ] `IonContent.ScrollToTopAsync(int? duration)`
- [ ] `IonDatetime.CancelAsync(bool? closeOverlay = null)`
- [ ] `IonDatetime.ConfirmAsync(bool? closeOverlay = null)`
- [ ] `IonDatetime.ResetAsync(string? startDate = null)`
- [ ] `IonInputOtp.SetFocusAsync(int? index = null)`
- [ ] `IonItemSliding.OpenAsync(string? side = null)`
- [ ] `IonLoading.DismissAsync(object? data = null, string? role = null)`
- [ ] `IonMenu.CloseAsync(bool? animated = null)` *(and role?)*
- [ ] `IonMenu.OpenAsync(bool? animated = null)`
- [ ] `IonMenu.SetOpenAsync(bool shouldOpen, bool? animated = null)`
- [ ] `IonMenu.ToggleAsync(bool? animated = null)`
- [ ] `IonModal.DismissAsync(object? data = null, string? role = null)`
- [ ] `IonModal.SetCurrentBreakpointAsync(double breakpoint)`
- [ ] `IonPickerLegacy.DismissAsync(object? data = null, string? role = null)`
- [ ] `IonPopover.DismissAsync(object? data = null, string? role = null, bool? dismissParentPopover = null)`
- [ ] `IonPopover.PresentAsync(...)` *(what does the handwritten take?)*
- [ ] `IonTabs.SelectAsync(string tab)`
- [ ] `IonToast.DismissAsync(object? data = null, string? role = null)`

---

## 3 — Default Values for String Properties ✅ RESOLVED

**Conclusion: The generator does not need to handle string property defaults at all.**

The key insight is that in IonBlazor, `Default = null` in constant classes is the "omit attribute"
sentinel — not the Ionic default value. Omitting the attribute and setting it to its Ionic default
are functionally identical:

```
<ion-accordion-group expand="compact"> == <ion-accordion-group>
```

So all `string?` properties should have `null` as their C# default (no explicit initializer).
The generator is already correct by not mapping Stencil `default` values.

**The `IonButtonFill` exception:**
`IonButtonFill.Default = "default"` is the *actual Ionic value* `"default"` (a valid fill option),
not the null sentinel. For this class, `Undefined = null` serves as the sentinel. The property
initializer in the handwritten component uses `= IonButtonFill.Undefined` (null). Same result —
no HTML attribute emitted. The generator treats it identically.

**Rare non-null `Default` cases** (where `Default` is a real value, not the null sentinel):
- `IonButtonFill.Default = "default"`
- `IonButtonSize.Default = "default"`
- `IonIconSize.Default = "default"`

All three use a separate `Undefined = null` constant as the "omit" sentinel.

**Naming resolved (Option A applied):** All null sentinels renamed from `Default = null` to
`Undefined = null` across all 71 constant classes. `IonIconSize.Null` was also renamed to
`Undefined` and moved to the bottom of the class. All component, sample, and test usages updated.
Tests: 939/939 passing.

---

## 4 — Non-Nullable `bool` vs `bool?`

The generator always emits `bool?` for boolean properties (since they're not `required` in
Stencil). But some handwritten components use non-nullable `bool` with a default.

**Pattern in Stencil metadata:** These properties have `"optional": false, "required": false`
with a non-null `"default"` value (e.g. `"true"` or `"false"`).

Handwritten examples found:
```
IonBackdrop.StopPropagation  bool = true
IonBackdrop.Tappable         bool = true
IonBackdrop.Visible          bool = true
IonPicker.Disabled           bool = false   (should this be bool??)
IonRadio.Disabled            bool = false   (should this be bool??)
IonReorderGroup.Disabled     bool = true
IonSearchbar.AutoCorrect     bool = false   (note: IonSearchbar.Animated is bool?)
IonSkeletonText.Animated     bool = false
```

**Task:** For each one below, confirm whether using non-nullable `bool` with the given default
is the correct pattern, or if `bool?` would be equally valid/preferred:

- [ ] `IonBackdrop` — `bool StopPropagation = true`, `bool Tappable = true`, `bool Visible = true` ✓
- [ ] `IonReorderGroup.Disabled` — `bool = true` (disabled by default, which is unusual — confirm)
- [ ] `IonSkeletonText.Animated` — `bool = false` (confirm `bool` not `bool?`)
- [ ] `IonPicker.Disabled` — `bool = false` (confirm this isn't just `bool?` omitted)
- [ ] `IonRadio.Disabled` — `bool = false` (same question)
- [ ] `IonSearchbar.AutoCorrect` — `bool = false` (confirm spelling matches Stencil attr `autocorrect`)

**Rule to implement:** If Stencil `optional == false && required == false && default != null`
→ use `bool` with the default value. Otherwise use `bool?`.
Is this rule correct? Are there counterexamples?

---

## 5 — Components NOT in `core.json` (Hand-Authored Variants)

These exist in `src/IonBlazor.Components/Components/` but have no entry in the Stencil
metadata. The generator should skip them (they're specializations or wrappers, not base Ionic elements).

- [ ] `IonListOf<TItem>` — generic wrapper over `IonList`
- [ ] `IonSelectOf<TItem>` — generic wrapper over `IonSelect`
- [ ] `IonSelectOptionOf` — typed wrapper (`IonSelectOptionBase<int>`)
- [ ] `IonSimplePickerLegacy` — inherits `IonPickerLegacy` unchanged
- [ ] `IonInputPasswordToggle` — confirm: is this in core.json or hand-authored?
- [ ] `IonIcon` — confirm: is `ion-icon` in core.json? (it's from `@ionic/core` not Ionicons)
- [ ] `IonDateTime` — the source folder is `IonDateTime` but core.json tag is `ion-datetime` (casing?)

---

## 6 — `CascadingParameter Parent` Pattern

Several components accept a `[CascadingParameter(Name = nameof(Parent))]` to know their
containing parent. This allows parent-aware behaviour (e.g. `IonAccordion` knowing its
`IonAccordionGroup`).

Components found with `CascadingParameter`:
`IonAccordion`, `IonBackButton`, `IonBreadcrumb`, `IonButton`, `IonButtons`, `IonCol`,
`IonHeader`, `IonInfiniteScrollContent`, `IonItem`, `IonItemDivider`, `IonItemOption`,
`IonItemOptions`, `IonItemSliding`, `IonList`, `IonListOf`, `IonPicker`, `IonPickerColumn`,
`IonPickerColumnOption`, `IonReorder`, `IonRow`, `IonSegmentButton`, `IonSegmentContent`,
`IonTab`, `IonTabButton`, `IonTitle`, `IonToolbar`

**Task:** This is NOT in the Stencil metadata — it's a custom IonBlazor pattern. Options are:
- [ ] Maintain a hardcoded list in the generator of which components need `[CascadingParameter]`
- [ ] Add it manually after generation (generator produces a base, human refines)
- [ ] Skip it entirely (it's an optional optimisation, not required for basic rendering)

Which approach do you prefer?

---

## 7 — `value` as a Computed Property

Some components expose `value` as a `ValueTask<T>` (async getter via JS interop) rather
than a simple `[Parameter]`. The generator would naively emit `[Parameter] public string? Value { get; set; }`.

Known cases:
- [ ] `IonAccordionGroup.Value` → `ValueTask<IEnumerable<string>>` (calls `getValue` JS method)
- [ ] `IonSelect.Value` → stored as `[Parameter]` but reading goes through JS?

**Task:** Confirm which components have `Value` as a non-parameter (computed via JS), and
what the return type should be. Should the generator skip the `value` property entirely for
these components and let the human add it manually?

---

## 8 — Events Generation (Stubs Only)

The current Events output is a stub with incomplete type info:
```csharp
private readonly DotNetObjectReference<IonicEventCallback<>> _ionChangeReference;
```
The `<>` is empty because the generator doesn't map the JS `detail` type to a C# type.

### The Two-Layer Architecture

Every event in IonBlazor has two layers:

**Layer 1 — Internal JS binding** (`IonicEventCallback<T>`):
Receives the raw JS `CustomEvent` object from `common.js`. This is always `JsonObject?`
(or no generic at all for events with no detail). The generator should consistently emit one of:

```csharp
// Events with detail
private readonly DotNetObjectReference<IonicEventCallback<JsonObject?>> _ionChangeReference;

// Events with no detail (blur, focus, willPresent, didPresent, willDismiss)
private readonly DotNetObjectReference<IonicEventCallback> _ionBlurReference;
```

The broken `IonicEventCallback<>` in the current stub → fix to `IonicEventCallback<JsonObject?>`.

**Layer 2 — Public Blazor parameter** (`EventCallback<T>`):
Typed EventArgs extracted from the `JsonObject?` and passed to Razor consumers. This always uses
a dedicated `sealed record IonXxxEventArgs`. `JsonObject?` is **not** the right type here.

```csharp
// Internal binding extracts from JsonObject?, then invokes the public callback
[Parameter] public EventCallback<IonCheckboxChangeEventArgs> IonChange { get; set; }
```

### EventArgs Patterns Found in Handwritten Components

All EventArgs are `sealed record` types with `Sender { get; internal init; }` + detail fields.

| JS Detail Type | C# EventArgs | Fields |
|---|---|---|
| `void` / no detail | `EventCallback` (no generic) | — |
| Sender-only (tap, no detail data) | `IonXxxEventArgs` | `Sender` only |
| `AccordionGroupChangeEventDetail` | `IonAccordionGroupIonChangeEventArgs` | `Sender`, `Value: string[]?` |
| `CheckboxChangeEventDetail` | `IonCheckboxChangeEventArgs` | `Sender`, `Checked: bool?`, `Value: string?` |
| `ToggleChangeEventDetail` | `IonToggleChangeEventArgs` | `Sender`, `Checked: bool?`, `Value: string?` |
| `InputChangeEventDetail` | `IonInputChangeEventArgs` | `Sender`, `Value: string?`, `Event: IonInputEvent` |
| `InputChangeEventDetail` (OTP) | `IonInputOtpChangeEventArgs` | `Sender`, `Value: string?`, `Event: IonInputEvent` |
| `RadioGroupChangeEventDetail` | `IonRadioGroupIonChangeEventArgs` | `Sender`, `Value: string?`, `Event: IonRadioGroupIonChangeEvent?` |
| `SegmentChangeEventDetail` | `IonSegmentIonChangeEventArgs` | `Sender`, `Value: string?` |
| `SearchbarChangeEventDetail` | `IonSearchbarChangeEventArgs` | `Sender`, `Value: string?`, `IsTrusted: bool?` |
| `TabsDidChangeEventDetail` | `IonTabsDidChangeEventArgs` | `Tab: string?` |
| `TabsWillChangeEventDetail` | `IonTabsWillChangeEventArgs` | `Tab: string?` |
| `OverlayEventDetail<T>` (modal/loading) | `IonModalDismissEventArgs` | `Sender`, `Role: string?`, `Data: object?` |
| `OverlayEventDetail<T>` (alert) | `IonAlertDismissEventArgs` | `Sender`, `Role: string?`, `Values: IAlertValues?` |
| `OverlayEventDetail<T>` (toast) | `IonToastDismissEventArgs` | `Sender`, `Role: string?` |
| `ScrollBaseDetail` | `IonContentScrollEventArgs` | `ScrollTop: double`, `ScrollLeft: double`, + 10 more double/bool fields |
| `SplitPaneVisibleEventDetail` | `IonSplitPaneVisibleEventArgs` | `Sender` only |
| `ItemReorderEventDetail` | `IonReorderGroupIonItemReorderEventArgs` | (TBD) |
| `RefresherEventDetail` | `IonRefresherIonRefreshEventArgs` | (TBD) |

`IonInputEvent` is a `struct { bool IsTrusted }` — the nested `event.isTrusted` from the JS detail.

### Special Cases

**CanDismiss on `IonModal`** — uses `IonicEventCallbackResult<bool>` (returns a value to JS):
```csharp
// Public parameter
[Parameter] public EventCallback<IonModalCanDismissEventArgs> CanDismiss { get; set; }

// Internal binding returns bool back to JS
_canDismissReference = IonicEventCallbackResult<bool>.Create(async () =>
{
    var args = new IonModalCanDismissEventArgs { Sender = this };
    await CanDismiss.InvokeAsync(args);
    return args.CanDismiss;   // mutable bool, default true
});
```

**Generic `IonSelect<TValue>`** — EventArgs carries the generic type:
```csharp
[Parameter] public EventCallback<IonSelectChangeEventArgs<TValue>> IonChange { get; set; }
```

**Builder events (IonAlert, IonActionSheet, IonToast buttons)** — use `IonicEventCallback<JsonObject?>` internally
to pluck an index, look up the button object, then invoke a per-button `Handler` delegate + a
component-level `EventCallback<AlertButtonHandlerEventArgs>`.

### Answer: Is `JsonObject?` Always the Right Fallback?

**For the internal `IonicEventCallback<T>`**: Yes — `JsonObject?` is always correct at this layer.
The generator should emit `IonicEventCallback<JsonObject?>` (with detail) or `IonicEventCallback`
(no detail). This fixes the broken `<>` stub immediately.

**For the public `EventCallback<T>`**: No — every event should have a typed `sealed record EventArgs`.
`JsonObject?` as a public parameter is poor DX. However, it is an acceptable temporary stub
while the proper EventArgs class is being hand-authored. The generator could emit:

```csharp
// TODO: replace JsonObject? with a typed IonXxxEventArgs once authored
[Parameter] public EventCallback<JsonObject?> IonChange { get; set; }
```

**EventArgs classes cannot be auto-generated from Stencil metadata alone.** The JS field names
are available in `complexType`, but the C# types for each field require judgement
(e.g. `detail.value` → `string?` vs `int?`, `detail.checked` → `bool?`).

### Generator Strategy Options

- [ ] **Option A — `JsonObject?` stub**: Generator emits `EventCallback<JsonObject?>` with a
  `// TODO` comment. Clean to generate, poor DX until replaced.
- [ ] **Option B — lookup table**: Maintain a hardcoded `Dictionary<string, string>` in
  `Program.cs` mapping known JS detail type names to their C# EventArgs class names.
  Generator emits the correct `EventCallback<IonXxxEventArgs>` for known types, falls back
  to `JsonObject?` for unknowns. Requires keeping the table in sync.
- [ ] **Option C — skip events entirely**: Generator emits no event properties. Human adds them
  manually. Simplest generator, most manual work.

Which strategy do you prefer?

### Checklist: Fix the Broken Stub

Regardless of public-parameter strategy, the internal binding type is unambiguous:

- [ ] `void` events → `IonicEventCallback` (no generic)
- [ ] `OverlayEventDetail<T>` → `IonicEventCallback<JsonObject?>` + public `EventCallback<IonXxxDismissEventArgs>`
- [ ] `AccordionGroupChangeEventDetail` → `IonicEventCallback<JsonObject?>` + public `EventCallback<IonAccordionGroupIonChangeEventArgs>`
- [ ] `CheckboxChangeEventDetail` → `IonicEventCallback<JsonObject?>` + public `EventCallback<IonCheckboxChangeEventArgs>`
- [ ] `InputChangeEventDetail` → `IonicEventCallback<JsonObject?>` + public `EventCallback<IonInputChangeEventArgs>`
