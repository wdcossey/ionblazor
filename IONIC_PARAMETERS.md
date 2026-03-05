# Ionic Parameters Audit

Comparing `node_modules/@ionic/docs/core.json` (Stencil metadata) against C# `[Parameter]`
implementations in `src/IonBlazor.Components/Components/`.

> Generated: 2026-03-05 · Ionic version: **8.8.0** (package.json pins `^8.7.2`)

---

## Legend

| Symbol | Meaning |
|--------|---------|
| ✅ | Correct — attr rendered in .razor and [Parameter] exists in .razor.cs |
| ⚙️ (builder) | Handled via an IonBlazor builder pattern instead of a plain [Parameter] |
| ⚠️ | Implemented but with issues (attr not rendered, parameter missing, etc.) |
| ❌ | Missing — Ionic prop not implemented in C# at all |
| — (JS fn) | JS function type — not applicable as a simple HTML attribute |

---

## Issues Summary

| Symbol | Count |
|--------|-------|
| ✅ Correct | 559 |
| ❌ Missing | 19 |
| ⚠️ Issues | 9 |

### ❌ Missing Parameters

- `ion-breadcrumb` → `routerDirection`
- `ion-checkbox` → `alignment`
- `ion-content` → `fixedSlotPlacement`
- `ion-datetime` → `formatOptions`
- `ion-input` → `clearInputIcon`
- `ion-item` → `routerDirection`
- `ion-popover` → `componentProps`
- `ion-popover` → `focusTrap`
- `ion-radio` → `alignment`
- `ion-refresher` → `mode`
- `ion-searchbar` → `autocapitalize`
- `ion-searchbar` → `maxlength`
- `ion-searchbar` → `minlength`
- `ion-searchbar` → `name`
- `ion-segment-view` → `swipeGesture`
- `ion-select` → `interfaceOptions`
- `ion-toast` → `cssClass`
- `ion-toast` → `swipeGesture`
- `ion-toggle` → `alignment`

### ⚠️ Parameter Issues

- `ion-accordion-group` → `value` — Attr `value` rendered in .razor but no [Parameter] found in .razor.cs
- `ion-button` → `routerDirection` — [Parameter] exists but attr `router-direction` not rendered in .razor
- `ion-card` → `routerDirection` — [Parameter] exists but attr `router-direction` not rendered in .razor
- `ion-modal` → `breakpoints` — [Parameter] exists but attr `—` not rendered in .razor
- `ion-modal` → `presentingElement` — [Parameter] exists but attr `—` not rendered in .razor
- `ion-popover` → `event` — [Parameter] exists but attr `event` not rendered in .razor
- `ion-select-option` → `disabled` — Attr `disabled` rendered in .razor but no [Parameter] found in .razor.cs
- `ion-select-option` → `value` — Attr `value` rendered in .razor but no [Parameter] found in .razor.cs
- `ion-spinner` → `duration` — [Parameter] exists but attr `duration` not rendered in .razor

---

## Version Delta: 8.7.2 (pinned) → 8.8.0 (audited)

The following changes were introduced between the pinned version and the audited version:

### 8.8.0

- `ion-segment-view` — new prop `swipeGesture` (`boolean`, default `true`) added
- `ion-modal.breakpoints` — `attr` field removed; prop is now JS-only (no HTML attribute binding)

---

## Full Component Parameter Map

### IonAccordion (`ion-accordion`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `toggleIcon` | `string` | `chevronDown` | `toggle-icon` | `ToggleIcon` | `string?` | ✅ |
| `toggleIconSlot` | `"end" | "start"` | `'end'` | `toggle-icon-slot` | `ToggleIconSlot` | `string?` | ✅ |
| `value` | `string` | ``ion-accordion-${accordionIds++}`` | `value` | `Value` | `string?` | ✅ |

### IonAccordionGroup (`ion-accordion-group`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `expand` | `"compact" | "inset"` | `'compact'` | `expand` | `Expand` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `multiple` | `boolean | undefined` | `—` | `multiple` | `Multiple` | `bool?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `value` | `null | string | string[] | undefined` | `—` | `value` | `Value` | `—` | ⚠️ _(Attr `value` rendered in .razor but no [Parameter] found in .razor.cs; Computed `ValueTask<IEnumerable<string>>` set via JS — not a plain [Parameter]; see `SetValueAsync`/`GetValueAsync`)_ |

### IonActionSheet (`ion-action-sheet`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `backdropDismiss` | `boolean` | `true` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `buttons` | `(string | ActionSheetButton<any>)[]` | `[]` | `—` | `Buttons` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `ButtonsBuilder` parameter))_ |
| `cssClass` | `string | string[] | undefined` | `—` | `css-class` | `CssClass` | `string?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `header` | `string | undefined` | `—` | `header` | `Header` | `string?` | ✅ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `—` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `subHeader` | `string | undefined` | `—` | `sub-header` | `SubHeader` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

> IonBlazor-specific: `ButtonsBuilder` (`ButtonBuilder?`) — no Ionic prop counterpart

### IonAlert (`ion-alert`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `backdropDismiss` | `boolean` | `true` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `buttons` | `(string | AlertButton)[]` | `[]` | `—` | `Buttons` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `ButtonsBuilder` parameter))_ |
| `cssClass` | `string | string[] | undefined` | `—` | `css-class` | `CssClass` | `string?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `header` | `string | undefined` | `—` | `header` | `Header` | `string?` | ✅ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `—` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `inputs` | `AlertInput[]` | `[]` | `—` | `Inputs` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `InputsBuilder` parameter))_ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `message` | `IonicSafeString | string | undefined` | `—` | `message` | `Message` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `subHeader` | `string | undefined` | `—` | `sub-header` | `SubHeader` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

> IonBlazor-specific: `ButtonsBuilder` (`ButtonBuilder?`) — no Ionic prop counterpart
> IonBlazor-specific: `InputsBuilder` (`InputBuilder?`) — no Ionic prop counterpart

### IonApp (`ion-app`)

_No props._

> IonBlazor-specific: `Mode` (`string?`) — no Ionic prop counterpart

### IonAvatar (`ion-avatar`)

_No props._

### IonBackButton (`ion-back-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `defaultHref` | `string | undefined` | `—` | `default-href` | `DefaultHref` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `icon` | `null | string | undefined` | `—` | `icon` | `Icon` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `text` | `null | string | undefined` | `—` | `text` | `Text` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string` | ✅ |

### IonBackdrop (`ion-backdrop`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `stopPropagation` | `boolean` | `true` | `stop-propagation` | `StopPropagation` | `bool` | ✅ |
| `tappable` | `boolean` | `true` | `tappable` | `Tappable` | `bool` | ✅ |
| `visible` | `boolean` | `true` | `visible` | `Visible` | `bool` | ✅ |

### IonBadge (`ion-badge`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonBreadcrumb (`ion-breadcrumb`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `active` | `boolean` | `false` | `active` | `Active` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `routerDirection` | `"back" | "forward" | "root"` | `'forward'` | `router-direction` | `RouterDirection` | `—` | ❌ |
| `separator` | `boolean | undefined` | `—` | `separator` | `Separator` | `bool?` | ✅ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |

### IonBreadcrumbs (`ion-breadcrumbs`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `itemsAfterCollapse` | `number` | `1` | `items-after-collapse` | `ItemsAfterCollapse` | `uint?` | ✅ |
| `itemsBeforeCollapse` | `number` | `1` | `items-before-collapse` | `ItemsBeforeCollapse` | `uint?` | ✅ |
| `maxItems` | `number | undefined` | `—` | `max-items` | `MaxItems` | `uint?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonButton (`ion-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `buttonType` | `string` | `'button'` | `button-type` | `ButtonType` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `expand` | `"block" | "full" | undefined` | `—` | `expand` | `Expand` | `string?` | ✅ |
| `fill` | `"clear" | "default" | "outline" | "solid" | und…` | `—` | `fill` | `Fill` | `string?` | ✅ |
| `form` | `HTMLFormElement | string | undefined` | `—` | `form` | `Form` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `routerDirection` | `"back" | "forward" | "root"` | `'forward'` | `router-direction` | `RouterDirection` | `string?` | ⚠️ _([Parameter] exists but attr `router-direction` not rendered in .razor; [Parameter] present but Blazor handles routing via `href`/`NavigationManager`; HTML attr likely intentionally omitted)_ |
| `shape` | `"round" | undefined` | `—` | `shape` | `Shape` | `string?` | ✅ |
| `size` | `"default" | "large" | "small" | undefined` | `—` | `size` | `Size` | `string?` | ✅ |
| `strong` | `boolean` | `false` | `strong` | `Strong` | `bool?` | ✅ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonButtons (`ion-buttons`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `collapse` | `boolean` | `false` | `collapse` | `Collapse` | `bool?` | ✅ |

### IonCard (`ion-card`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `button` | `boolean` | `false` | `button` | `Button` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `routerDirection` | `"back" | "forward" | "root"` | `'forward'` | `router-direction` | `RouterDirection` | `string?` | ⚠️ _([Parameter] exists but attr `router-direction` not rendered in .razor; [Parameter] present but Blazor handles routing via `href`/`NavigationManager`; HTML attr likely intentionally omitted)_ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonCardContent (`ion-card-content`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonCardHeader (`ion-card-header`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |

### IonCardSubtitle (`ion-card-subtitle`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonCardTitle (`ion-card-title`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonCheckbox (`ion-checkbox`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `alignment` | `"center" | "start" | undefined` | `—` | `alignment` | `Alignment` | `—` | ❌ |
| `checked` | `boolean` | `false` | `checked` | `Checked` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `indeterminate` | `boolean` | `false` | `indeterminate` | `Indeterminate` | `bool?` | ✅ |
| `justify` | `"end" | "space-between" | "start" | undefined` | `—` | `justify` | `Justify` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string` | ✅ |
| `required` | `boolean` | `false` | `required` | `Required` | `bool?` | ✅ |
| `value` | `any` | `'on'` | `value` | `Value` | `string?` | ✅ |

### IonChip (`ion-chip`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `outline` | `boolean` | `false` | `outline` | `Outline` | `bool?` | ✅ |

### IonCol (`ion-col`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `offset` | `string | undefined` | `—` | `offset` | `Offset` | `string?` | ✅ |
| `offsetLg` | `string | undefined` | `—` | `offset-lg` | `OffsetLg` | `string?` | ✅ |
| `offsetMd` | `string | undefined` | `—` | `offset-md` | `OffsetMd` | `string?` | ✅ |
| `offsetSm` | `string | undefined` | `—` | `offset-sm` | `OffsetSm` | `string?` | ✅ |
| `offsetXl` | `string | undefined` | `—` | `offset-xl` | `OffsetXl` | `string?` | ✅ |
| `offsetXs` | `string | undefined` | `—` | `offset-xs` | `OffsetXs` | `string?` | ✅ |
| `pull` | `string | undefined` | `—` | `pull` | `Pull` | `string?` | ✅ |
| `pullLg` | `string | undefined` | `—` | `pull-lg` | `PullLg` | `string?` | ✅ |
| `pullMd` | `string | undefined` | `—` | `pull-md` | `PullMd` | `string?` | ✅ |
| `pullSm` | `string | undefined` | `—` | `pull-sm` | `PullSm` | `string?` | ✅ |
| `pullXl` | `string | undefined` | `—` | `pull-xl` | `PullXl` | `string?` | ✅ |
| `pullXs` | `string | undefined` | `—` | `pull-xs` | `PullXs` | `string?` | ✅ |
| `push` | `string | undefined` | `—` | `push` | `Push` | `string?` | ✅ |
| `pushLg` | `string | undefined` | `—` | `push-lg` | `PushLg` | `string?` | ✅ |
| `pushMd` | `string | undefined` | `—` | `push-md` | `PushMd` | `string?` | ✅ |
| `pushSm` | `string | undefined` | `—` | `push-sm` | `PushSm` | `string?` | ✅ |
| `pushXl` | `string | undefined` | `—` | `push-xl` | `PushXl` | `string?` | ✅ |
| `pushXs` | `string | undefined` | `—` | `push-xs` | `PushXs` | `string?` | ✅ |
| `size` | `string | undefined` | `—` | `size` | `Size` | `string?` | ✅ |
| `sizeLg` | `string | undefined` | `—` | `size-lg` | `SizeLg` | `string?` | ✅ |
| `sizeMd` | `string | undefined` | `—` | `size-md` | `SizeMd` | `string?` | ✅ |
| `sizeSm` | `string | undefined` | `—` | `size-sm` | `SizeSm` | `string?` | ✅ |
| `sizeXl` | `string | undefined` | `—` | `size-xl` | `SizeXl` | `string?` | ✅ |
| `sizeXs` | `string | undefined` | `—` | `size-xs` | `SizeXs` | `string?` | ✅ |

### IonContent (`ion-content`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `fixedSlotPlacement` | `"after" | "before"` | `'after'` | `fixed-slot-placement` | `FixedSlotPlacement` | `—` | ❌ |
| `forceOverscroll` | `boolean | undefined` | `—` | `force-overscroll` | `ForceOverscroll` | `bool?` | ✅ |
| `fullscreen` | `boolean` | `false` | `fullscreen` | `Fullscreen` | `bool?` | ✅ |
| `scrollEvents` | `boolean` | `false` | `scroll-events` | `ScrollEvents` | `bool?` | ✅ |
| `scrollX` | `boolean` | `false` | `scroll-x` | `ScrollX` | `bool?` | ✅ |
| `scrollY` | `boolean` | `true` | `scroll-y` | `ScrollY` | `bool?` | ✅ |

### IonDateTime (`ion-datetime`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `cancelText` | `string` | `'Cancel'` | `cancel-text` | `CancelText` | `string?` | ✅ |
| `clearText` | `string` | `'Clear'` | `clear-text` | `ClearText` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `'primary'` | `color` | `Color` | `string?` | ✅ |
| `dayValues` | `number | number[] | string | undefined` | `—` | `day-values` | `DayValues` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `doneText` | `string` | `'Done'` | `done-text` | `DoneText` | `string?` | ✅ |
| `firstDayOfWeek` | `number` | `0` | `first-day-of-week` | `FirstDayOfWeek` | `int?` | ✅ |
| `formatOptions` | `undefined | { date: DateTimeFormatOptions; time…` | `—` | `—` | `FormatOptions` | `—` | ❌ |
| `highlightedDates` | `((dateIsoString: string) => DatetimeHighlightSt…` | `—` | `—` | `HighlightedDates` | `object?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `hourCycle` | `"h11" | "h12" | "h23" | "h24" | undefined` | `—` | `hour-cycle` | `HourCycle` | `string?` | ✅ |
| `hourValues` | `number | number[] | string | undefined` | `—` | `hour-values` | `HourValues` | `string?` | ✅ |
| `isDateEnabled` | `((dateIsoString: string) => boolean) | undefined` | `—` | `—` | `IsDateEnabled` | `Func<string>?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `locale` | `string` | `'default'` | `locale` | `Locale` | `string?` | ✅ |
| `max` | `string | undefined` | `—` | `max` | `Max` | `string?` | ✅ |
| `min` | `string | undefined` | `—` | `min` | `Min` | `string?` | ✅ |
| `minuteValues` | `number | number[] | string | undefined` | `—` | `minute-values` | `MinuteValues` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `monthValues` | `number | number[] | string | undefined` | `—` | `month-values` | `MonthValues` | `string?` | ✅ |
| `multiple` | `boolean` | `false` | `multiple` | `Multiple` | `bool?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `preferWheel` | `boolean` | `false` | `prefer-wheel` | `PreferWheel` | `bool?` | ✅ |
| `presentation` | `"date" | "date-time" | "month" | "month-year" |…` | `'date-time'` | `presentation` | `Presentation` | `string?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `showAdjacentDays` | `boolean` | `false` | `show-adjacent-days` | `ShowAdjacentDays` | `bool?` | ✅ |
| `showClearButton` | `boolean` | `false` | `show-clear-button` | `ShowClearButton` | `bool?` | ✅ |
| `showDefaultButtons` | `boolean` | `false` | `show-default-buttons` | `ShowDefaultButtons` | `bool?` | ✅ |
| `showDefaultTimeLabel` | `boolean` | `true` | `show-default-time-label` | `ShowDefaultTimeLabel` | `bool?` | ✅ |
| `showDefaultTitle` | `boolean` | `false` | `show-default-title` | `ShowDefaultTitle` | `bool?` | ✅ |
| `size` | `"cover" | "fixed"` | `'fixed'` | `size` | `Size` | `string?` | ✅ |
| `titleSelectedDatesFormatter` | `((selectedDates: string[]) => string) | undefined` | `—` | `—` | `TitleSelectedDatesFormatter` | `string` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `value` | `null | string | string[] | undefined` | `—` | `value` | `Value` | `string?` | ✅ |
| `yearValues` | `number | number[] | string | undefined` | `—` | `year-values` | `YearValues` | `string?` | ✅ |

### IonDateTimeButton (`ion-datetime-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `'primary'` | `color` | `Color` | `string?` | ✅ |
| `datetime` | `string | undefined` | `—` | `datetime` | `DateTime` | `string` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonFab (`ion-fab`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `activated` | `boolean` | `false` | `activated` | `Activated` | `bool` | ✅ |
| `edge` | `boolean` | `false` | `edge` | `Edge` | `bool` | ✅ |
| `horizontal` | `"center" | "end" | "start" | undefined` | `—` | `horizontal` | `Horizontal` | `string?` | ✅ |
| `vertical` | `"bottom" | "center" | "top" | undefined` | `—` | `vertical` | `Vertical` | `string?` | ✅ |

### IonFabButton (`ion-fab-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `activated` | `boolean` | `false` | `activated` | `Activated` | `bool` | ✅ |
| `closeIcon` | `string` | `close` | `close-icon` | `CloseIcon` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `routerDirection` | `"back" | "forward" | "root"` | `'forward'` | `router-direction` | `RouterDirection` | `string?` | ✅ |
| `show` | `boolean` | `false` | `show` | `Show` | `bool` | ✅ |
| `size` | `"small" | undefined` | `—` | `size` | `Size` | `string?` | ✅ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonFabList (`ion-fab-list`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `activated` | `boolean` | `false` | `activated` | `Activated` | `bool` | ✅ |
| `side` | `"bottom" | "end" | "start" | "top"` | `'bottom'` | `side` | `Side` | `string?` | ✅ |

### IonFooter (`ion-footer`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `collapse` | `"fade" | undefined` | `—` | `collapse` | `Collapse` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |

### IonGrid (`ion-grid`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `fixed` | `boolean` | `false` | `fixed` | `Fixed` | `bool?` | ✅ |

### IonHeader (`ion-header`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `collapse` | `"condense" | "fade" | undefined` | `—` | `collapse` | `Collapse` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |

### IonImg (`ion-img`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `alt` | `string | undefined` | `—` | `alt` | `Alt` | `string?` | ✅ |
| `src` | `string | undefined` | `—` | `src` | `Src` | `string?` | ✅ |

### IonInfiniteScroll (`ion-infinite-scroll`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `position` | `"bottom" | "top"` | `'bottom'` | `position` | `Position` | `string?` | ✅ |
| `threshold` | `string` | `'15%'` | `threshold` | `Threshold` | `string?` | ✅ |

### IonInfiniteScrollContent (`ion-infinite-scroll-content`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `loadingSpinner` | `"bubbles" | "circles" | "circular" | "crescent"…` | `—` | `loading-spinner` | `LoadingSpinner` | `string?` | ✅ |
| `loadingText` | `IonicSafeString | string | undefined` | `—` | `loading-text` | `LoadingText` | `string?` | ✅ |

### IonInput (`ion-input`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `autocapitalize` | `string` | `'off'` | `autocapitalize` | `Autocapitalize` | `string?` | ✅ |
| `autocomplete` | `"additional-name" | "address-level1" | "address…` | `'off'` | `autocomplete` | `Autocomplete` | `string?` | ✅ |
| `autocorrect` | `"off" | "on"` | `'off'` | `autocorrect` | `Autocorrect` | `string?` | ✅ |
| `autofocus` | `boolean` | `false` | `autofocus` | `Autofocus` | `bool?` | ✅ |
| `clearInput` | `boolean` | `false` | `clear-input` | `ClearInput` | `bool?` | ✅ |
| `clearInputIcon` | `string | undefined` | `—` | `clear-input-icon` | `ClearInputIcon` | `—` | ❌ |
| `clearOnEdit` | `boolean | undefined` | `—` | `clear-on-edit` | `ClearOnEdit` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `counter` | `boolean` | `false` | `counter` | `Counter` | `bool?` | ✅ |
| `counterFormatter` | `((inputLength: number, maxLength: number) => st…` | `—` | `—` | `CounterFormatter` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `debounce` | `number | undefined` | `—` | `debounce` | `Debounce` | `int?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `enterkeyhint` | `"done" | "enter" | "go" | "next" | "previous" |…` | `—` | `enterkeyhint` | `Enterkeyhint` | `string?` | ✅ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `fill` | `"outline" | "solid" | undefined` | `—` | `fill` | `Fill` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `inputmode` | `"decimal" | "email" | "none" | "numeric" | "sea…` | `—` | `inputmode` | `InputMode` | `string?` | ✅ |
| `label` | `string | undefined` | `—` | `label` | `Label` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "floating" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `max` | `number | string | undefined` | `—` | `max` | `Max` | `int?` | ✅ |
| `maxlength` | `number | undefined` | `—` | `maxlength` | `Maxlength` | `int?` | ✅ |
| `min` | `number | string | undefined` | `—` | `min` | `Min` | `int?` | ✅ |
| `minlength` | `number | undefined` | `—` | `minlength` | `Minlength` | `int?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `multiple` | `boolean | undefined` | `—` | `multiple` | `Multiple` | `bool?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `pattern` | `string | undefined` | `—` | `pattern` | `Pattern` | `string?` | ✅ |
| `placeholder` | `string | undefined` | `—` | `placeholder` | `Placeholder` | `string?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `required` | `boolean` | `false` | `required` | `Required` | `bool?` | ✅ |
| `shape` | `"round" | undefined` | `—` | `shape` | `Shape` | `string?` | ✅ |
| `spellcheck` | `boolean` | `false` | `spellcheck` | `Spellcheck` | `bool?` | ✅ |
| `step` | `string | undefined` | `—` | `step` | `Step` | `string?` | ✅ |
| `type` | `"date" | "datetime-local" | "email" | "month" |…` | `'text'` | `type` | `Type` | `string?` | ✅ |
| `value` | `null | number | string | undefined` | `''` | `value` | `Value` | `string?` | ✅ |

### IonInputOtp (`ion-input-otp`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `autocapitalize` | `string` | `'off'` | `autocapitalize` | `Autocapitalize` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `fill` | `"outline" | "solid" | undefined` | `'outline'` | `fill` | `Fill` | `string?` | ✅ |
| `inputmode` | `"decimal" | "email" | "none" | "numeric" | "sea…` | `—` | `inputmode` | `InputMode` | `string?` | ✅ |
| `length` | `number` | `4` | `length` | `Length` | `byte?` | ✅ |
| `pattern` | `string | undefined` | `—` | `pattern` | `Pattern` | `string?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `separators` | `number[] | string | undefined` | `—` | `separators` | `Separators` | `string?` | ✅ |
| `shape` | `"rectangular" | "round" | "soft"` | `'round'` | `shape` | `Shape` | `string?` | ✅ |
| `size` | `"large" | "medium" | "small"` | `'medium'` | `size` | `Size` | `string?` | ✅ |
| `type` | `"number" | "text"` | `'number'` | `type` | `Type` | `string?` | ✅ |
| `value` | `null | number | string | undefined` | `''` | `value` | `Value` | `string?` | ✅ |

### IonInputPasswordToggle (`ion-input-password-toggle`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `hideIcon` | `string | undefined` | `—` | `hide-icon` | `HideIcon` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `showIcon` | `string | undefined` | `—` | `show-icon` | `ShowIcon` | `string?` | ✅ |

### IonItem (`ion-item`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `button` | `boolean` | `false` | `button` | `Button` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `detail` | `boolean | undefined` | `—` | `detail` | `Detail` | `bool?` | ✅ |
| `detailIcon` | `string` | `chevronForward` | `detail-icon` | `DetailIcon` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `lines` | `"full" | "inset" | "none" | undefined` | `—` | `lines` | `Lines` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `routerAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `RouterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `routerDirection` | `"back" | "forward" | "root"` | `'forward'` | `router-direction` | `RouterDirection` | `—` | ❌ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonItemDivider (`ion-item-divider`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `sticky` | `boolean` | `false` | `sticky` | `Sticky` | `bool?` | ✅ |

### IonItemGroup (`ion-item-group`)

_No props._

### IonItemOption (`ion-item-option`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `expandable` | `boolean` | `false` | `expandable` | `Expandable` | `bool?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonItemOptions (`ion-item-options`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `side` | `"end" | "start"` | `'end'` | `side` | `Side` | `required string` | ✅ |

### IonItemSliding (`ion-item-sliding`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |

### IonLabel (`ion-label`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `position` | `"fixed" | "floating" | "stacked" | undefined` | `—` | `position` | `Position` | `string?` | ✅ |

### IonList (`ion-list`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `inset` | `boolean` | `false` | `inset` | `Inset` | `bool?` | ✅ |
| `lines` | `"full" | "inset" | "none" | undefined` | `—` | `lines` | `Lines` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonListHeader (`ion-list-header`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `lines` | `"full" | "inset" | "none" | undefined` | `—` | `lines` | `Lines` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonLoading (`ion-loading`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `backdropDismiss` | `boolean` | `false` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `cssClass` | `string | string[] | undefined` | `—` | `css-class` | `CssClass` | `string?` | ✅ |
| `duration` | `number` | `0` | `duration` | `Duration` | `int?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `—` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `message` | `IonicSafeString | string | undefined` | `—` | `message` | `Message` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `showBackdrop` | `boolean` | `true` | `show-backdrop` | `ShowBackdrop` | `bool?` | ✅ |
| `spinner` | `"bubbles" | "circles" | "circular" | "crescent"…` | `—` | `spinner` | `Spinner` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

### IonMenu (`ion-menu`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `contentId` | `string | undefined` | `—` | `content-id` | `ContentId` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `maxEdgeStart` | `number` | `50` | `max-edge-start` | `MaxEdgeStart` | `int?` | ✅ |
| `menuId` | `string | undefined` | `—` | `menu-id` | `MenuId` | `string?` | ✅ |
| `side` | `"end" | "start"` | `'start'` | `side` | `Side` | `string?` | ✅ |
| `swipeGesture` | `boolean` | `true` | `swipe-gesture` | `SwipeGesture` | `bool?` | ✅ |
| `type` | `"overlay" | "push" | "reveal" | undefined` | `—` | `type` | `Type` | `string?` | ✅ |

> IonBlazor-specific: `Mode` (`string?`) — no Ionic prop counterpart

### IonMenuButton (`ion-menu-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `autoHide` | `boolean` | `true` | `auto-hide` | `AutoHide` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `menu` | `string | undefined` | `—` | `menu` | `Menu` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string?` | ✅ |

### IonMenuToggle (`ion-menu-toggle`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `autoHide` | `boolean` | `true` | `auto-hide` | `AutoHide` | `bool?` | ✅ |
| `menu` | `string | undefined` | `—` | `menu` | `Menu` | `string?` | ✅ |

### IonModal (`ion-modal`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `backdropBreakpoint` | `number` | `0` | `backdrop-breakpoint` | `BackdropBreakpoint` | `double?` | ✅ |
| `backdropDismiss` | `boolean` | `true` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `breakpoints` | `number[] | undefined` | `—` | `—` | `Breakpoints` | `double[]?` | ⚠️ _([Parameter] exists but attr `—` not rendered in .razor; Array prop — no HTML attribute in 8.8.0 (attr removed in this version); must be set via JS interop; [Parameter] is correct but template rendering is N/A)_ |
| `canDismiss` | `((data?: any, role?: string | undefined) => Pro…` | `true` | `can-dismiss` | `CanDismiss` | `EventCallback<IonModalCanDismissEventArgs>` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `expandToScroll` | `boolean` | `true` | `expand-to-scroll` | `ExpandToScroll` | `bool?` | ✅ |
| `focusTrap` | `boolean` | `true` | `focus-trap` | `focusTrap` | `bool?` | ✅ |
| `handle` | `boolean | undefined` | `—` | `handle` | `Handle` | `bool?` | ✅ |
| `handleBehavior` | `"cycle" | "none" | undefined` | `'none'` | `handle-behavior` | `HandleBehavior` | `string?` | ✅ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `Dictionary<string, object>` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `initialBreakpoint` | `number | undefined` | `—` | `initial-breakpoint` | `InitialBreakpoint` | `double?` | ✅ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keepContentsMounted` | `boolean` | `false` | `keep-contents-mounted` | `KeepContentsMounted` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `object?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `presentingElement` | `HTMLElement | undefined` | `—` | `—` | `PresentingElement` | `object?` | ⚠️ _([Parameter] exists but attr `—` not rendered in .razor; DOM element reference — must be set via JS interop, not an HTML attribute; [Parameter] correct)_ |
| `showBackdrop` | `boolean` | `true` | `show-backdrop` | `ShowBackdrop` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

### IonNote (`ion-note`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonPicker (`ion-picker`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonPickerColumn (`ion-picker-column`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `'primary'` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `value` | `number | string | undefined` | `—` | `value` | `Value` | `string?` | ✅ |

### IonPickerColumnOption (`ion-picker-column-option`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `'primary'` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `value` | `any` | `—` | `value` | `Value` | `string?` | ✅ |

### IonPickerLegacy (`ion-picker-legacy`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `backdropDismiss` | `boolean` | `true` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `buttons` | `PickerButton[]` | `[]` | `—` | `Buttons` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `ButtonsBuilder` parameter))_ |
| `columns` | `PickerColumn[]` | `[]` | `—` | `Columns` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `ColumnsBuilder` parameter))_ |
| `cssClass` | `string | string[] | undefined` | `—` | `css-class` | `CssClass` | `string?` | ✅ |
| `duration` | `number` | `0` | `duration` | `Duration` | `int?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `object?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `object?` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `object?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `showBackdrop` | `boolean` | `true` | `show-backdrop` | `ShowBackdrop` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

> IonBlazor-specific: `ButtonsBuilder` (`ButtonBuilder?`) — no Ionic prop counterpart
> IonBlazor-specific: `ColumnsBuilder` (`ColumnBuilder?`) — no Ionic prop counterpart

### IonPopover (`ion-popover`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `alignment` | `"center" | "end" | "start" | undefined` | `—` | `alignment` | `Alignment` | `string?` | ✅ |
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `arrow` | `boolean` | `true` | `arrow` | `Arrow` | `bool?` | ✅ |
| `backdropDismiss` | `boolean` | `true` | `backdrop-dismiss` | `BackdropDismiss` | `bool?` | ✅ |
| `component` | `Function | HTMLElement | null | string | undefined` | `—` | `component` | `Component` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `componentProps` | `T | undefined` | `—` | `—` | `ComponentProps` | `—` | ❌ |
| `dismissOnSelect` | `boolean` | `false` | `dismiss-on-select` | `DismissOnSelect` | `bool?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `event` | `any` | `—` | `event` | `Event` | `object?` | ⚠️ _([Parameter] exists but attr `event` not rendered in .razor; MouseEvent object — must be set via JS interop, not an HTML attribute; [Parameter] correct)_ |
| `focusTrap` | `boolean` | `true` | `focus-trap` | `FocusTrap` | `—` | ❌ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `—` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keepContentsMounted` | `boolean` | `false` | `keep-contents-mounted` | `KeepContentsMounted` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `true` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `reference` | `"event" | "trigger"` | `'trigger'` | `reference` | `Reference` | `string?` | ✅ |
| `showBackdrop` | `boolean` | `true` | `show-backdrop` | `ShowBackdrop` | `bool?` | ✅ |
| `side` | `"bottom" | "end" | "left" | "right" | "start" |…` | `'bottom'` | `side` | `Side` | `string?` | ✅ |
| `size` | `"auto" | "cover"` | `'auto'` | `size` | `Size` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |
| `triggerAction` | `"click" | "context-menu" | "hover"` | `'click'` | `trigger-action` | `TriggerAction` | `string?` | ✅ |

### IonProgressBar (`ion-progress-bar`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `buffer` | `number` | `1` | `buffer` | `Buffer` | `double` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `reversed` | `boolean` | `false` | `reversed` | `Reversed` | `bool` | ✅ |
| `type` | `"determinate" | "indeterminate"` | `'determinate'` | `type` | `Type` | `string?` | ✅ |
| `value` | `number` | `0` | `value` | `Value` | `double` | ✅ |

### IonRadio (`ion-radio`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `alignment` | `"center" | "start" | undefined` | `—` | `alignment` | `Alignment` | `—` | ❌ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `justify` | `"end" | "space-between" | "start" | undefined` | `—` | `justify` | `Justify` | `string` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `value` | `any` | `—` | `value` | `Value` | `string?` | ✅ |

### IonRadioGroup (`ion-radio-group`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `allowEmptySelection` | `boolean` | `false` | `allow-empty-selection` | `AllowEmptySelection` | `bool` | ✅ |
| `compareWith` | `((currentValue: any, compareValue: any) => bool…` | `—` | `compare-with` | `CompareWith` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `value` | `any` | `—` | `value` | `Value` | `string?` | ✅ |

### IonRange (`ion-range`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `activeBarStart` | `number | undefined` | `—` | `active-bar-start` | `ActiveBarStart` | `int?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `debounce` | `number | undefined` | `—` | `debounce` | `Debounce` | `long?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `dualKnobs` | `boolean` | `false` | `dual-knobs` | `DualKnobs` | `bool?` | ✅ |
| `label` | `string | undefined` | `—` | `label` | `Label` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `max` | `number` | `100` | `max` | `Max` | `int?` | ✅ |
| `min` | `number` | `0` | `min` | `Min` | `int?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.rangeId` | `name` | `Name` | `string?` | ✅ |
| `pin` | `boolean` | `false` | `pin` | `Pin` | `bool?` | ✅ |
| `pinFormatter` | `(value: number) => string | number` | `(value: number): number => Math.round(value)` | `—` | `PinFormatter` | `Func<int,string>?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `snaps` | `boolean` | `false` | `snaps` | `Snaps` | `bool?` | ✅ |
| `step` | `number` | `1` | `step` | `Step` | `int?` | ✅ |
| `ticks` | `boolean` | `true` | `ticks` | `Ticks` | `bool?` | ✅ |
| `value` | `number | { lower: number; upper: number; }` | `0` | `value` | `Value` | `IRangeValue?` | ✅ |

### IonRefresher (`ion-refresher`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `closeDuration` | `string` | `'280ms'` | `close-duration` | `CloseDuration` | `string` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `—` | ❌ |
| `pullFactor` | `number` | `1` | `pull-factor` | `PullFactor` | `double?` | ✅ |
| `pullMax` | `number` | `this.pullMin + 60` | `pull-max` | `PullMax` | `int?` | ✅ |
| `pullMin` | `number` | `60` | `pull-min` | `PullMin` | `int?` | ✅ |
| `snapbackDuration` | `string` | `'280ms'` | `snapback-duration` | `SnapbackDuration` | `string` | ✅ |

### IonRefresherContent (`ion-refresher-content`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `pullingIcon` | `null | string | undefined` | `—` | `pulling-icon` | `PullingIcon` | `string?` | ✅ |
| `pullingText` | `IonicSafeString | string | undefined` | `—` | `pulling-text` | `PullingText` | `string?` | ✅ |
| `refreshingSpinner` | `"bubbles" | "circles" | "circular" | "crescent"…` | `—` | `refreshing-spinner` | `RefreshingSpinner` | `string?` | ✅ |
| `refreshingText` | `IonicSafeString | string | undefined` | `—` | `refreshing-text` | `RefreshingText` | `string?` | ✅ |

### IonReorder (`ion-reorder`)

_No props._

### IonReorderGroup (`ion-reorder-group`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `true` | `disabled` | `Disabled` | `bool` | ✅ |

### IonRippleEffect (`ion-ripple-effect`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `type` | `"bounded" | "unbounded"` | `'bounded'` | `type` | `Type` | `string?` | ✅ |

### IonRow (`ion-row`)

_No props._

### IonSearchbar (`ion-searchbar`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `false` | `animated` | `Animated` | `bool?` | ✅ |
| `autocapitalize` | `string` | `'off'` | `autocapitalize` | `Autocapitalize` | `—` | ❌ |
| `autocomplete` | `"additional-name" | "address-level1" | "address…` | `'off'` | `autocomplete` | `AutoComplete` | `string?` | ✅ |
| `autocorrect` | `"off" | "on"` | `'off'` | `autocorrect` | `AutoCorrect` | `bool` | ✅ |
| `cancelButtonIcon` | `string` | `config.get('backButtonIcon', arrowBackSharp) as string` | `cancel-button-icon` | `CancelButtonIcon` | `string?` | ✅ |
| `cancelButtonText` | `string` | `'Cancel'` | `cancel-button-text` | `CancelButtonText` | `string?` | ✅ |
| `clearIcon` | `string | undefined` | `—` | `clear-icon` | `ClearIcon` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `debounce` | `number | undefined` | `—` | `debounce` | `Debounce` | `int?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `enterkeyhint` | `"done" | "enter" | "go" | "next" | "previous" |…` | `—` | `enterkeyhint` | `EnterKeyHint` | `string?` | ✅ |
| `inputmode` | `"decimal" | "email" | "none" | "numeric" | "sea…` | `—` | `inputmode` | `InputMode` | `string?` | ✅ |
| `maxlength` | `number | undefined` | `—` | `maxlength` | `Maxlength` | `—` | ❌ |
| `minlength` | `number | undefined` | `—` | `minlength` | `Minlength` | `—` | ❌ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `—` | ❌ |
| `placeholder` | `string` | `'Search'` | `placeholder` | `Placeholder` | `string?` | ✅ |
| `searchIcon` | `string | undefined` | `—` | `search-icon` | `SearchIcon` | `string?` | ✅ |
| `showCancelButton` | `"always" | "focus" | "never"` | `'never'` | `show-cancel-button` | `ShowCancelButton` | `string?` | ✅ |
| `showClearButton` | `"always" | "focus" | "never"` | `'always'` | `show-clear-button` | `ShowClearButton` | `string?` | ✅ |
| `spellcheck` | `boolean` | `false` | `spellcheck` | `SpellCheck` | `bool?` | ✅ |
| `type` | `"email" | "number" | "password" | "search" | "t…` | `'search'` | `type` | `Type` | `string?` | ✅ |
| `value` | `null | string | undefined` | `''` | `value` | `Value` | `string?` | ✅ |

### IonSegment (`ion-segment`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `scrollable` | `boolean` | `false` | `scrollable` | `Scrollable` | `bool` | ✅ |
| `selectOnFocus` | `boolean` | `false` | `select-on-focus` | `SelectOnFocus` | `bool` | ✅ |
| `swipeGesture` | `boolean` | `true` | `swipe-gesture` | `SwipeGesture` | `bool` | ✅ |
| `value` | `number | string | undefined` | `—` | `value` | `Value` | `string?` | ✅ |

### IonSegmentButton (`ion-segment-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `contentId` | `string | undefined` | `—` | `content-id` | `ContentId` | `string` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `layout` | `"icon-bottom" | "icon-end" | "icon-hide" | "ico…` | `'icon-top'` | `layout` | `Layout` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `type` | `"button" | "reset" | "submit"` | `'button'` | `type` | `Type` | `string` | ✅ |
| `value` | `number | string` | `'ion-sb-' + ids++` | `value` | `Value` | `string` | ✅ |

### IonSegmentContent (`ion-segment-content`)

_No props._

### IonSegmentView (`ion-segment-view`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool` | ✅ |
| `swipeGesture` | `boolean` | `true` | `swipe-gesture` | `SwipeGesture` | `—` | ❌ |

### IonSelect (`ion-select`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `cancelText` | `string` | `'Cancel'` | `cancel-text` | `CancelText` | `string?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `compareWith` | `((currentValue: any, compareValue: any) => bool…` | `—` | `compare-with` | `CompareWith` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `expandedIcon` | `string | undefined` | `—` | `expanded-icon` | `ExpandedIcon` | `string?` | ✅ |
| `fill` | `"outline" | "solid" | undefined` | `—` | `fill` | `Fill` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `interface` | `"action-sheet" | "alert" | "modal" | "popover"` | `'alert'` | `interface` | `Interface` | `string?` | ✅ |
| `interfaceOptions` | `any` | `{}` | `interface-options` | `InterfaceOptions` | `—` | ❌ |
| `justify` | `"end" | "space-between" | "start" | undefined` | `—` | `justify` | `Justify` | `string?` | ✅ |
| `label` | `string | undefined` | `—` | `label` | `Label` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "floating" | "stacked" | "sta…` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `multiple` | `boolean` | `false` | `multiple` | `Multiple` | `bool?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `okText` | `string` | `'OK'` | `ok-text` | `OkText` | `string?` | ✅ |
| `placeholder` | `string | undefined` | `—` | `placeholder` | `Placeholder` | `string?` | ✅ |
| `required` | `boolean` | `false` | `required` | `Required` | `bool?` | ✅ |
| `selectedText` | `null | string | undefined` | `—` | `selected-text` | `SelectedText` | `string?` | ✅ |
| `shape` | `"round" | undefined` | `—` | `shape` | `Shape` | `string?` | ✅ |
| `toggleIcon` | `string | undefined` | `—` | `toggle-icon` | `ToggleIcon` | `string?` | ✅ |
| `value` | `any` | `—` | `value` | `Value` | `object?` | ✅ |

### IonSelectOption (`ion-select-option`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `—` | ⚠️ _(Attr `disabled` rendered in .razor but no [Parameter] found in .razor.cs; Defined in `IonSelectOptionBase<TValue>` base class — false positive (regex only scans direct .razor.cs))_ |
| `value` | `any` | `—` | `value` | `Value` | `—` | ⚠️ _(Attr `value` rendered in .razor but no [Parameter] found in .razor.cs; Defined as generic `TValue?` in `IonSelectOptionBase<TValue>` — false positive (regex only scans direct .razor.cs))_ |

### IonSkeletonText (`ion-skeleton-text`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `false` | `animated` | `Animated` | `bool` | ✅ |

### IonSpinner (`ion-spinner`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `duration` | `number | undefined` | `—` | `duration` | `Duration` | `int?` | ⚠️ _([Parameter] exists but attr `duration` not rendered in .razor; [Parameter] exists in .razor.cs but `duration` attr is not rendered in the .razor template — likely a bug)_ |
| `name` | `"bubbles" | "circles" | "circular" | "crescent"…` | `—` | `name` | `Name` | `string?` | ✅ |
| `paused` | `boolean` | `false` | `paused` | `Paused` | `bool` | ✅ |

### IonSplitPane (`ion-split-pane`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `contentId` | `string | undefined` | `—` | `content-id` | `ContentId` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `when` | `boolean | string` | `QUERY['lg']` | `when` | `When` | `string?` | ✅ |

### IonTab (`ion-tab`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `component` | `Function | HTMLElement | null | string | undefined` | `—` | `component` | `Component` | `string?` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `tab` | `string` | `—` | `tab` | `Tab` | `string` | ✅ |

### IonTabBar (`ion-tab-bar`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `selectedTab` | `string | undefined` | `—` | `selected-tab` | `SelectedTab` | `string?` | ✅ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |

### IonTabButton (`ion-tab-button`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `download` | `string | undefined` | `—` | `download` | `Download` | `string?` | ✅ |
| `href` | `string | undefined` | `—` | `href` | `Href` | `string?` | ✅ |
| `layout` | `"icon-bottom" | "icon-end" | "icon-hide" | "ico…` | `—` | `layout` | `Layout` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `rel` | `string | undefined` | `—` | `rel` | `Rel` | `string?` | ✅ |
| `selected` | `boolean` | `false` | `selected` | `Selected` | `bool?` | ✅ |
| `tab` | `string | undefined` | `—` | `tab` | `Tab` | `string?` | ✅ |
| `target` | `string | undefined` | `—` | `target` | `Target` | `string?` | ✅ |

### IonTabs (`ion-tabs`)

_No props._

### IonText (`ion-text`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

### IonTextarea (`ion-textarea`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `autoGrow` | `boolean` | `false` | `auto-grow` | `AutoGrow` | `bool?` | ✅ |
| `autocapitalize` | `string` | `'none'` | `autocapitalize` | `Autocapitalize` | `string?` | ✅ |
| `autofocus` | `boolean` | `false` | `autofocus` | `Autofocus` | `bool?` | ✅ |
| `clearOnEdit` | `boolean` | `false` | `clear-on-edit` | `ClearOnEdit` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `cols` | `number | undefined` | `—` | `cols` | `Cols` | `int?` | ✅ |
| `counter` | `boolean` | `false` | `counter` | `Counter` | `bool?` | ✅ |
| `counterFormatter` | `((inputLength: number, maxLength: number) => st…` | `—` | `—` | `CounterFormatter` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `debounce` | `number | undefined` | `—` | `debounce` | `Debounce` | `int?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `enterkeyhint` | `"done" | "enter" | "go" | "next" | "previous" |…` | `—` | `enterkeyhint` | `Enterkeyhint` | `string?` | ✅ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `fill` | `"outline" | "solid" | undefined` | `—` | `fill` | `Fill` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `inputmode` | `"decimal" | "email" | "none" | "numeric" | "sea…` | `—` | `inputmode` | `InputMode` | `string?` | ✅ |
| `label` | `string | undefined` | `—` | `label` | `Label` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "floating" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `maxlength` | `number | undefined` | `—` | `maxlength` | `Maxlength` | `int?` | ✅ |
| `minlength` | `number | undefined` | `—` | `minlength` | `Minlength` | `int?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `placeholder` | `string | undefined` | `—` | `placeholder` | `Placeholder` | `string?` | ✅ |
| `readonly` | `boolean` | `false` | `readonly` | `Readonly` | `bool?` | ✅ |
| `required` | `boolean` | `false` | `required` | `Required` | `bool?` | ✅ |
| `rows` | `number | undefined` | `—` | `rows` | `Rows` | `int?` | ✅ |
| `shape` | `"round" | undefined` | `—` | `shape` | `Shape` | `string?` | ✅ |
| `spellcheck` | `boolean` | `false` | `spellcheck` | `Spellcheck` | `bool?` | ✅ |
| `value` | `null | string | undefined` | `''` | `value` | `Value` | `string?` | ✅ |
| `wrap` | `"hard" | "off" | "soft" | undefined` | `—` | `wrap` | `Wrap` | `bool?` | ✅ |

> IonBlazor-specific: `CounterFormat` (`string?`) — no Ionic prop counterpart

### IonThumbnail (`ion-thumbnail`)

_No props._

### IonTitle (`ion-title`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `size` | `"large" | "small" | undefined` | `—` | `size` | `Size` | `string?` | ✅ |

### IonToast (`ion-toast`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `animated` | `boolean` | `true` | `animated` | `Animated` | `bool?` | ✅ |
| `buttons` | `(string | ToastButton)[] | undefined` | `—` | `—` | `Buttons` | `—` | ⚙️ (builder) _(Handled via IonBlazor builder pattern (see `ButtonsBuilder` parameter))_ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `cssClass` | `string | string[] | undefined` | `—` | `css-class` | `CssClass` | `—` | ❌ |
| `duration` | `number` | `config.getNumber('toastDuration', 0)` | `duration` | `Duration` | `int?` | ✅ |
| `enterAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `EnterAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `header` | `string | undefined` | `—` | `header` | `Header` | `string?` | ✅ |
| `htmlAttributes` | `undefined | { [key: string]: any; }` | `—` | `—` | `HtmlAttributes` | `—` | ✅ _(Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)]))_ |
| `icon` | `string | undefined` | `—` | `icon` | `Icon` | `string?` | ✅ |
| `isOpen` | `boolean` | `false` | `is-open` | `IsOpen` | `bool?` | ✅ |
| `keyboardClose` | `boolean` | `false` | `keyboard-close` | `KeyboardClose` | `bool?` | ✅ |
| `layout` | `"baseline" | "stacked"` | `'baseline'` | `layout` | `Layout` | `string?` | ✅ |
| `leaveAnimation` | `((baseEl: any, opts?: any) => Animation) | unde…` | `—` | `—` | `LeaveAnimation` | `—` | — (JS fn) _(JS function type — not applicable as HTML attribute)_ |
| `message` | `IonicSafeString | string | undefined` | `—` | `message` | `Message` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `position` | `"bottom" | "middle" | "top"` | `'bottom'` | `position` | `Position` | `string?` | ✅ |
| `positionAnchor` | `HTMLElement | string | undefined` | `—` | `position-anchor` | `PositionAnchor` | `string?` | ✅ |
| `swipeGesture` | `"vertical" | undefined` | `—` | `swipe-gesture` | `SwipeGesture` | `—` | ❌ |
| `translucent` | `boolean` | `false` | `translucent` | `Translucent` | `bool?` | ✅ |
| `trigger` | `string | undefined` | `—` | `trigger` | `Trigger` | `string?` | ✅ |

> IonBlazor-specific: `ButtonsBuilder` (`ButtonBuilder?`) — no Ionic prop counterpart

### IonToggle (`ion-toggle`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `alignment` | `"center" | "start" | undefined` | `—` | `alignment` | `Alignment` | `—` | ❌ |
| `checked` | `boolean` | `false` | `checked` | `Checked` | `bool?` | ✅ |
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `disabled` | `boolean` | `false` | `disabled` | `Disabled` | `bool?` | ✅ |
| `enableOnOffLabels` | `boolean | undefined` | `config.get('toggleOnOffLabels')` | `enable-on-off-labels` | `EnableOnOffLabels` | `bool?` | ✅ |
| `errorText` | `string | undefined` | `—` | `error-text` | `ErrorText` | `string?` | ✅ |
| `helperText` | `string | undefined` | `—` | `helper-text` | `HelperText` | `string?` | ✅ |
| `justify` | `"end" | "space-between" | "start" | undefined` | `—` | `justify` | `Justify` | `string?` | ✅ |
| `labelPlacement` | `"end" | "fixed" | "stacked" | "start"` | `'start'` | `label-placement` | `LabelPlacement` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |
| `name` | `string` | `this.inputId` | `name` | `Name` | `string?` | ✅ |
| `required` | `boolean` | `false` | `required` | `Required` | `bool?` | ✅ |
| `value` | `null | string | undefined` | `'on'` | `value` | `Value` | `string?` | ✅ |

### IonToolbar (`ion-toolbar`)

| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |
|------------|------------|---------|-----------|--------------|---------|--------|
| `color` | `"danger" | "dark" | "light" | "medium" | "prima…` | `—` | `color` | `Color` | `string?` | ✅ |
| `mode` | `"ios" | "md"` | `—` | `mode` | `Mode` | `string?` | ✅ |

---

## IonBlazor-Only Components

These components exist in IonBlazor but have no counterpart in Ionic's Stencil metadata:

- `IonListOf` (in `IonList/IonListOf`)
- `IonSelectOf` (in `IonSelect/IonSelectOf`)
- `IonSelectOptionOf` (in `IonSelect/IonSelectOptionOf`)
- `IonSimplePickerLegacy` (in `IonPickerLegacy/IonSimplePickerLegacy`)

---

## Ionic Components Without a Blazor Wrapper

| Ionic Tag | Reason |
|-----------|--------|
| `ion-nav` | Routing — handled by Blazor router, not wrapped |
| `ion-nav-link` | Routing — handled by Blazor router, not wrapped |
| `ion-route` | Routing — handled by Blazor router, not wrapped |
| `ion-route-redirect` | Routing — handled by Blazor router, not wrapped |
| `ion-router` | Routing — handled by Blazor router, not wrapped |
| `ion-router-link` | Routing — handled by Blazor router, not wrapped |
| `ion-router-outlet` | Routing — handled by Blazor router, not wrapped |
| `ion-select-modal` | Internal Ionic component — not intended for direct consumer use |
