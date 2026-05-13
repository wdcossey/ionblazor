# Generator Verification Tasks

These are open questions the generator can't resolve automatically from the Stencil metadata.
Tick `[x]` when verified and I'll incorporate the findings.

> **Heads-up (post base-class split):** Section 1 was written when the runtime hierarchy was
> 2-way (`IonComponent` / `IonContentComponent`). The hierarchy is now 4-way — see
> [CLAUDE.md → Base Classes](CLAUDE.md). When teaching the generator about base-class
> selection, the choice is now a matrix of `{has-JS, no-JS} × {has-ChildContent, no-ChildContent}`,
> not a single boolean. The list below is still useful as the "no ChildContent" axis.

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

**Task:** For the most common event detail types, what should the C# type be?

- [ ] `OverlayEventDetail<T>` → `JsonObject?` (used on dismiss events)
- [ ] `AccordionGroupChangeEventDetail` → `JsonObject?`
- [ ] `CheckboxChangeEventDetail` → `JsonObject?`
- [ ] `InputChangeEventDetail` → `JsonObject?`
- [ ] `void` → `JsonObject?` (events with no detail)

Is `JsonObject?` always the right fallback, or do some events warrant a typed `EventArgs` class?
