# Ionic Events Audit

Comparing `node_modules/@ionic/docs/core.json` (Stencil metadata) against C# implementations in
`src/IonBlazor.Components/Components/`.

> Generated: 2026-03-04

---

## Legend

| Symbol | Meaning |
|--------|---------|
| ✅ | Correct — event and detail type match |
| ⚠️ | Implemented but with issues (detail missing, wrong type, naming) |
| ❌ | Missing — event not implemented at all |

---

## Issues Summary

### ❌ Missing Events

*None.*

### ⚠️ Wrong EventCallback Type

*None.*

---

## Full Component Event Map

### IonAccordionGroup (`ion-accordion-group`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionChange` | `AccordionGroupChangeEventDetail<any>` | `value: string[]` | `IonChange` | `IonAccordionGroupIonChangeEventArgs` | ✅ |

### IonActionSheet (`ion-action-sheet`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `ActionSheetDismissEventArgs<T>` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `ActionSheetEventArgs<T>` | ✅ |
| `ionActionSheetDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonActionSheetDidDismiss` | `ActionSheetDismissEventArgs<T>` | ✅ |
| `ionActionSheetDidPresent` | `void` | — | `IonActionSheetDidPresent` | `ActionSheetEventArgs<T>` | ✅ |
| `ionActionSheetWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonActionSheetWillDismiss` | `ActionSheetDismissEventArgs<T>` | ✅ |
| `ionActionSheetWillPresent` | `void` | — | `IonActionSheetWillPresent` | `ActionSheetEventArgs<T>` | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `ActionSheetDismissEventArgs<T>` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `ActionSheetEventArgs<T>` | ✅ |

> Note: `ButtonHandler` is a non-Ionic extra event added for the button handler JS callback — not in Stencil metadata.

### IonAlert (`ion-alert`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonAlertDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `IonAlertDidPresentEventArgs` | ✅ |
| `ionAlertDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonAlertDidDismiss` | `IonAlertDismissEventArgs` | ✅ |
| `ionAlertDidPresent` | `void` | — | `IonAlertDidPresent` | `IonAlertIonAlertDidPresentEventArgs` | ✅ |
| `ionAlertWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonAlertWillDismiss` | `IonAlertDismissEventArgs` | ✅ |
| `ionAlertWillPresent` | `void` | — | `IonAlertWillPresent` | `IonAlertIonAlertWillPresentEventArgs` | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonAlertDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `IonAlertWillPresentEventArgs` | ✅ |

### IonBackdrop (`ion-backdrop`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBackdropTap` | `void` | — | `IonBackdropTap` | `EventCallback<IonBackdrop>` | ✅ |

### IonBreadcrumb (`ion-breadcrumb`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ *(renamed from `OnBlur`)* |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ *(renamed from `OnFocus`)* |

### IonBreadcrumbs (`ion-breadcrumbs`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionCollapsedClick` | `BreadcrumbCollapsedClickEventDetail` | `collapsedBreadcrumbs?: HTMLIonBreadcrumbElement[]` | `IonCollapsedClick` | `IonBreadcrumbsCollapsedClickEventArgs` | ✅ JS serializes `active`, `collapsed`, `disabled`, `download`, `href`, `last`, `textContent` per breadcrumb |

### IonButton (`ion-button`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ *(renamed from `OnBlur`)* |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ *(renamed from `OnFocus`)* |

### IonCheckbox (`ion-checkbox`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionChange` | `CheckboxChangeEventDetail<any>` | `checked: bool`, `value: any` | `IonChange` | `IonCheckboxChangeEventArgs` | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

### IonContent (`ion-content`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionScroll` | `ScrollDetail` | `scrollTop`, `scrollLeft`, `type`, `velocityX/Y`, `deltaX/Y`, etc. | `IonScroll` | `IonContentScrollEventArgs` | ✅ |
| `ionScrollEnd` | `ScrollBaseDetail` | `isScrolling: bool` | `IonScrollEnd` | `IonScrollEndEventArgs` | ✅ |
| `ionScrollStart` | `ScrollBaseDetail` | `isScrolling: bool` | `IonScrollStart` | `IonScrollStartEventArgs` | ✅ |

### IonDatetime (`ion-datetime`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionCancel` | `void` | — | `IonCancel` | — | ✅ |
| `ionChange` | `DatetimeChangeEventDetail` | `value: string \| string[]` | `IonChange` | `IonDateTimeChangeEventArgs` | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

### IonFabButton (`ion-fab-button`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

### IonImg (`ion-img`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionError` | `void` | — | `IonError` | `EventCallback<IonImg>` | ✅ |
| `ionImgDidLoad` | `void` | — | `IonImgDidLoad` | `EventCallback<IonImg>` | ✅ |
| `ionImgWillLoad` | `void` | — | `IonImgWillLoad` | `EventCallback<IonImg>` | ✅ |

### IonInfiniteScroll (`ion-infinite-scroll`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionInfinite` | `void` | — | `IonInfinite` | `IonInfiniteEventArgs` | ✅ |

### IonInput (`ion-input`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `FocusEvent` | DOM FocusEvent | `IonBlur` | — (void) | ✅ |
| `ionChange` | `InputChangeEventDetail` | `value: string \| null \| undefined` | `IonChange` | `IonInputChangeEventArgs` | ✅ |
| `ionFocus` | `FocusEvent` | DOM FocusEvent | `IonFocus` | — (void) | ✅ |
| `ionInput` | `InputInputEventDetail` | `value: string \| null \| undefined`, `event: InputEvent` | `IonInputEvent` | `IonInputInputEventArgs` | ✅ |

### IonInputOtp (`ion-input-otp`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `FocusEvent` | DOM FocusEvent | `IonBlur` | `EventCallback<IonInputOtp>` | ✅ |
| `ionChange` | `InputOtpChangeEventDetail` | `value: string` | `IonChange` | `IonInputOtpChangeEventArgs` | ✅ |
| `ionComplete` | `InputOtpCompleteEventDetail` | `value: string` | `IonComplete` | `IonInputOtpCompleteEventArgs` | ✅ |
| `ionFocus` | `FocusEvent` | DOM FocusEvent | `IonFocus` | `EventCallback<IonInputOtp>` | ✅ |
| `ionInput` | `InputOtpInputEventDetail` | `value: string \| null \| undefined` | `IonInputEvent` | `IonInputOtpInputEventArgs` | ✅ |

### IonItemOptions (`ion-item-options`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionSwipe` | `any` | untyped | `IonSwipe` | `IonItemOptionsSwipeEventArgs` | ✅ (detail is `any` in Ionic) |

### IonItemSliding (`ion-item-sliding`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionDrag` | `any` | untyped | `IonDrag` | `IonDragEventArgs` | ✅ (detail is `any` in Ionic) |

### IonLoading (`ion-loading`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonLoadingDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `IonLoadingPresentEventArgs` | ✅ |
| `ionLoadingDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonLoadingDidDismiss` | `IonLoadingDismissEventArgs` | ✅ |
| `ionLoadingDidPresent` | `void` | — | `IonLoadingDidPresent` | `IonLoadingPresentEventArgs` | ✅ |
| `ionLoadingWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonLoadingWillDismiss` | `IonLoadingDismissEventArgs` | ✅ |
| `ionLoadingWillPresent` | `void` | — | `IonLoadingWillPresent` | `IonLoadingPresentEventArgs` | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonLoadingDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `IonLoadingPresentEventArgs` | ✅ |

### IonMenu (`ion-menu`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionDidClose` | `MenuCloseEventDetail` | `role?: string` | `IonDidClose` | `IonMenuCloseEventArgs` | ✅ |
| `ionDidOpen` | `void` | — | `IonDidOpen` | `EventCallback<IonMenu>` | ✅ |
| `ionWillClose` | `MenuCloseEventDetail` | `role?: string` | `IonWillClose` | `IonMenuCloseEventArgs` | ✅ |
| `ionWillOpen` | `void` | — | `IonWillOpen` | `EventCallback<IonMenu>` | ✅ |

### IonModal (`ion-modal`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonModalDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `EventCallback<IonModal>` | ✅ |
| `ionBreakpointDidChange` | `ModalBreakpointChangeEventDetail` | `breakpoint: number` | `IonBreakpointDidChange` | `IonModalBreakpointDidChangeEventArgs` | ✅ |
| `ionModalDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonModalDidDismiss` | `IonModalDismissEventArgs` | ✅ |
| `ionModalDidPresent` | `void` | — | `IonModalDidPresent` | `EventCallback<IonModal>` | ✅ |
| `ionModalWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonModalWillDismiss` | `IonModalDismissEventArgs` | ✅ |
| `ionModalWillPresent` | `void` | — | `IonModalWillPresent` | `EventCallback<IonModal>` | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonModalDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `EventCallback<IonModal>` | ✅ |

### IonNav (`ion-nav`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionNavDidChange` | `void` | — | `IonNavDidChange` | `IonNavDidChangeEventArgs` | ✅ |
| `ionNavWillChange` | `void` | — | `IonNavWillChange` | `IonNavWillChangeEventArgs` | ✅ |

### IonPickerColumn (`ion-picker-column`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionChange` | `PickerColumnChangeEventDetail` | `value: string \| number \| undefined` | `IonChange` | `IonPickerColumnIonChangeEventArgs` | ✅ |

### IonPickerLegacy (`ion-picker-legacy`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonPickerLegacyDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `EventCallback` (void) | ✅ |
| `ionPickerDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonPickerDidDismiss` | `IonPickerLegacyDismissEventArgs` | ✅ |
| `ionPickerDidPresent` | `void` | — | `IonPickerDidPresent` | `EventCallback` (void) | ✅ |
| `ionPickerWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonPickerWillDismiss` | `IonPickerLegacyDismissEventArgs` | ✅ |
| `ionPickerWillPresent` | `void` | — | `IonPickerWillPresent` | `EventCallback` (void) | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonPickerLegacyDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `EventCallback` (void) | ✅ |

### IonPopover (`ion-popover`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonModalDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `EventCallback` (void) | ✅ |
| `ionPopoverDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonPopoverDidDismiss` | `IonModalDismissEventArgs` | ✅ |
| `ionPopoverDidPresent` | `void` | — | `IonPopoverDidPresent` | `EventCallback` (void) | ✅ |
| `ionPopoverWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonPopoverWillDismiss` | `IonModalDismissEventArgs` | ✅ |
| `ionPopoverWillPresent` | `void` | — | `IonPopoverWillPresent` | `EventCallback` (void) | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonModalDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `EventCallback` (void) | ✅ |

### IonRadio (`ion-radio`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

### IonRadioGroup (`ion-radio-group`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionChange` | `RadioGroupChangeEventDetail<any>` | `value: any` | `IonChange` | `IonRadioGroupIonChangeEventArgs` | ✅ |

### IonRange (`ion-range`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionChange` | `RangeChangeEventDetail` | `value: number \| { lower, upper }` | `IonChange` | `RangeChangeEventArgs` | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |
| `ionInput` | `RangeChangeEventDetail` | `value: number \| { lower, upper }` | `IonInput` | `RangeChangeEventArgs` | ✅ |
| `ionKnobMoveEnd` | `RangeKnobMoveEndEventDetail` | `value: number \| { lower, upper }` | `IonKnobMoveEnd` | `RangeChangeEventArgs` | ✅ |
| `ionKnobMoveStart` | `RangeKnobMoveStart` | `value: number \| { lower, upper }` | `IonKnobMoveStart` | `RangeChangeEventArgs` | ✅ |

### IonRefresher (`ion-refresher`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionPull` | `void` | — | `IonPull` | `IonRefresherIonPullEventArgs` | ✅ |
| `ionRefresh` | `RefresherEventDetail` | `complete(): void` | `IonRefresh` | `IonRefresherIonRefreshEventArgs` | ✅ |
| `ionStart` | `void` | — | `IonStart` | `IonRefresherIonStartEventArgs` | ✅ |

### IonReorderGroup (`ion-reorder-group`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionItemReorder` | `ItemReorderEventDetail` | `from: int`, `to: int`, `complete: fn` | `IonItemReorder` | `IonReorderGroupIonItemReorderEventArgs` | ✅ |
| `ionReorderEnd` | `ReorderEndEventDetail` | `from: int`, `to: int`, `complete: fn` | `IonReorderEnd` | `IonReorderGroupIonReorderEndEventArgs` | ✅ |
| `ionReorderMove` | `ReorderMoveEventDetail` | `from: int`, `to: int` | `IonReorderMove` | `IonReorderGroupIonReorderMoveEventArgs` | ✅ |
| `ionReorderStart` | `void` | — | `IonReorderStart` | `EventCallback<IonReorderGroup>` | ✅ |

### IonSearchbar (`ion-searchbar`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionCancel` | `void` | — | `IonCancel` | — | ✅ |
| `ionChange` | `SearchbarChangeEventDetail` | `value?: string`, `isTrusted: bool` | `IonChange` | `IonSearchbarChangeEventArgs` | ✅ |
| `ionClear` | `void` | — | `IonClear` | — | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |
| `ionInput` | `SearchbarInputEventDetail` | `value?: string` | `IonInput` | `IonSearchbarInputEventArgs` | ✅ |

### IonSegment (`ion-segment`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionChange` | `SegmentChangeEventDetail` | `value: string` | `IonChange` | `IonSegmentIonChangeEventArgs` | ✅ |

### IonSegmentView (`ion-segment-view`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionSegmentViewScroll` | `SegmentViewScrollEvent` | `scrollRatio: number`, `isManualScroll: bool` | `IonSegmentViewScroll` | `IonSegmentViewScrollEvent` | ✅ |

### IonSelect (`ion-select`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionCancel` | `void` | — | `IonCancel` | — | ✅ |
| `ionChange` | `SelectChangeEventDetail<any>` | `value: any` | `IonChange` | `IonSelectChangeEventArgs<T>` | ✅ |
| `ionDismiss` | `void` | — | `IonDismiss` | — | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

### IonSplitPane (`ion-split-pane`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionSplitPaneVisible` | `{ visible: boolean }` | `visible: bool` | `IonSplitPaneVisible` | `IonSplitPaneVisibleEventArgs` | ✅ |

### IonTabs (`ion-tabs`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionTabsDidChange` | `{ tab: string }` | `tab: string` | `IonTabsDidChange` | `IonTabsDidChangeEventArgs` | ✅ |
| `ionTabsWillChange` | `{ tab: string }` | `tab: string` | `IonTabsWillChange` | `IonTabsWillChangeEventArgs` | ✅ |

### IonTextarea (`ion-textarea`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `FocusEvent` | DOM FocusEvent | `IonBlur` | `EventCallback<IonTextarea>` | ✅ |
| `ionChange` | `TextareaChangeEventDetail` | `value: string \| null \| undefined` | `IonChange` | `IonTextareaChangeEventArgs` | ✅ |
| `ionFocus` | `FocusEvent` | DOM FocusEvent | `IonFocus` | `EventCallback<IonTextarea>` | ✅ |
| `ionInput` | `TextareaInputEventDetail` | `value: string \| null \| undefined`, `event: InputEvent` | `IonInput` | `IonTextareaInputEventArgs` | ✅ |

### IonToast (`ion-toast`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `didDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `DidDismiss` | `IonToastDismissEventArgs` | ✅ |
| `didPresent` | `void` | — | `DidPresent` | `EventCallback` (void) | ✅ |
| `ionToastDidDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonToastDidDismiss` | `IonToastDismissEventArgs` | ✅ |
| `ionToastDidPresent` | `void` | — | `IonToastDidPresent` | `EventCallback` (void) | ✅ |
| `ionToastWillDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `IonToastWillDismiss` | `IonToastDismissEventArgs` | ✅ |
| `ionToastWillPresent` | `void` | — | `IonToastWillPresent` | `EventCallback` (void) | ✅ |
| `willDismiss` | `OverlayEventDetail<any>` | `role?, data?` | `WillDismiss` | `IonToastDismissEventArgs` | ✅ |
| `willPresent` | `void` | — | `WillPresent` | `EventCallback` (void) | ✅ |

### IonToggle (`ion-toggle`)

| Ionic Event | Detail Type | Properties | C# Parameter | C# EventArgs | Status |
|-------------|-------------|-----------|--------------|-------------|--------|
| `ionBlur` | `void` | — | `IonBlur` | — | ✅ |
| `ionChange` | `ToggleChangeEventDetail<any>` | `checked: bool`, `value: any` | `IonChange` | `IonToggleChangeEventArgs` | ✅ |
| `ionFocus` | `void` | — | `IonFocus` | — | ✅ |

---

## Components Not Wrapped (events excluded from audit)

These Ionic components have events in `core.json` but have no corresponding C# wrapper in this library
(router components are intentionally excluded as they are Vanilla Ionic / not applicable to Blazor):

| Ionic Tag | Events | Reason |
|-----------|--------|--------|
| `ion-route` | `ionRouteDataChanged` | Router component — not applicable |
| `ion-route-redirect` | `ionRouteRedirectChanged` | Router component — not applicable |
| `ion-router` | `ionRouteDidChange`, `ionRouteWillChange` | Router component — not applicable |

---

## Stats

| Category | Count |
|----------|-------|
| Total Ionic events (wrapped components) | ~115 |
| ✅ Correctly implemented | ~115 |
| ❌ Missing events | 0 |
| ⚠️ Misconfigured / wrong type | 0 |
