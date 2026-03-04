# Ionic Events — Full Detail Type Inventory

Generated from `node_modules/@ionic/docs/core.json` (Ionic 8.7.2).

**Legend:** `[✓]` implemented · `[✗]` missing

---

## IonAccordionGroup `ion-accordion-group`

### `[✓]` `IonChange` — `AccordionGroupChangeEventDetail`

```ts
export interface AccordionGroupChangeEventDetail<T = any> {
  value: T;
}
```

**C#:** `EventCallback<IonAccordionGroupIonChangeEventArgs>`

---

## IonActionSheet `ion-action-sheet`

### `[✗]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** *(not implemented)*

### `[✗]` `DidPresent` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonActionSheetDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonActionSheetDidPresent` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonActionSheetWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonActionSheetWillPresent` — `void`

**C#:** *(not implemented)*

### `[✗]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** *(not implemented)*

### `[✗]` `WillPresent` — `void`

**C#:** *(not implemented)*

---

## IonAlert `ion-alert`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonAlertDismissEventArgs>`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback<IonAlertDidPresentEventArgs>`

### `[✓]` `IonAlertDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonAlertDismissEventArgs>`

### `[✓]` `IonAlertDidPresent` — `void`

**C#:** `EventCallback<IonAlertIonAlertDidPresentEventArgs>`

### `[✓]` `IonAlertWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonAlertDismissEventArgs>`

### `[✓]` `IonAlertWillPresent` — `void`

**C#:** `EventCallback<IonAlertIonAlertWillPresentEventArgs>`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonAlertDismissEventArgs>`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback<IonAlertWillPresentEventArgs>`

---

## IonBackdrop `ion-backdrop`

### `[✓]` `IonBackdropTap` — `void`

**C#:** `EventCallback`

---

## IonBreadcrumb `ion-breadcrumb`

### `[✗]` `IonBlur` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonFocus` — `void`

**C#:** *(not implemented)*

---

## IonBreadcrumbs `ion-breadcrumbs`

### `[✗]` `IonCollapsedClick` — `BreadcrumbCollapsedClickEventDetail`

```ts
export interface BreadcrumbCollapsedClickEventDetail {
  ionShadowTarget?: HTMLElement;
  collapsedBreadcrumbs?: HTMLIonBreadcrumbElement[];
}
```

**C#:** *(not implemented)*

---

## IonButton `ion-button`

### `[✗]` `IonBlur` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonFocus` — `void`

**C#:** *(not implemented)*

---

## IonCheckbox `ion-checkbox`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonChange` — `CheckboxChangeEventDetail`

```ts
export interface CheckboxChangeEventDetail<T = any> {
  value: T;
  checked: boolean;
}
```

**C#:** `EventCallback<IonCheckboxChangeEventArgs>`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

---

## IonContent `ion-content`

### `[✓]` `IonScroll` — `ScrollDetail`

```ts
export interface ScrollDetail extends GestureDetail, ScrollBaseDetail {
  scrollTop: number;
  scrollLeft: number;
}
```

**C#:** `EventCallback<IonContentScrollEventArgs?>`

### `[✓]` `IonScrollEnd` — `ScrollBaseDetail`

```ts
export interface ScrollBaseDetail {
  isScrolling: boolean;
}
```

**C#:** `EventCallback<IonScrollEndEventArgs?>`

### `[✓]` `IonScrollStart` — `ScrollBaseDetail`

```ts
export interface ScrollBaseDetail {
  isScrolling: boolean;
}
```

**C#:** `EventCallback<IonScrollStartEventArgs?>`

---

## IonDatetime `ion-datetime`

### `[✗]` `IonBlur` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonCancel` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonChange` — `DatetimeChangeEventDetail`

```ts
export interface DatetimeChangeEventDetail {
  value?: string | string[] | null;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonFocus` — `void`

**C#:** *(not implemented)*

---

## IonFabButton `ion-fab-button`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

---

## IonImg `ion-img`

### `[✗]` `IonError` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonImgDidLoad` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonImgWillLoad` — `void`

**C#:** *(not implemented)*

---

## IonInfiniteScroll `ion-infinite-scroll`

### `[✓]` `IonInfinite` — `void`

**C#:** `EventCallback<IonInfiniteEventArgs>`

---

## IonInput `ion-input`

### `[✓]` `IonBlur` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonInput>`

### `[✓]` `IonChange` — `InputChangeEventDetail`

```ts
export interface InputChangeEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonInputChangeEventArgs>`

### `[✓]` `IonFocus` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonInput>`

### `[✗]` `IonInput` — `InputInputEventDetail`

```ts
export interface InputInputEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** *(not implemented)*

---

## IonInputOtp `ion-input-otp`

### `[✓]` `IonBlur` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonInputOtp>`

### `[✓]` `IonChange` — `InputOtpChangeEventDetail`

```ts
export interface InputOtpChangeEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonInputOtpChangeEventArgs>`

### `[✓]` `IonComplete` — `InputOtpCompleteEventDetail`

```ts
export interface InputOtpCompleteEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonInputOtpCompleteEventArgs>`

### `[✓]` `IonFocus` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonInputOtp>`

### `[✗]` `IonInput` — `InputOtpInputEventDetail`

```ts
export interface InputOtpInputEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** *(not implemented)*

---

## IonItemOptions `ion-item-options`

### `[✓]` `IonSwipe` — `any`

```ts
any
```

**C#:** `EventCallback<IonItemOptionsSwipeEventArgs>`

---

## IonItemSliding `ion-item-sliding`

### `[✓]` `IonDrag` — `any`

```ts
any
```

**C#:** `EventCallback<IonDragEventArgs>`

---

## IonLoading `ion-loading`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonLoadingDismissEventArgs>`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback<IonLoadingPresentEventArgs>`

### `[✓]` `IonLoadingDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonLoadingDismissEventArgs>`

### `[✓]` `IonLoadingDidPresent` — `void`

**C#:** `EventCallback<IonLoadingPresentEventArgs>`

### `[✓]` `IonLoadingWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonLoadingDismissEventArgs>`

### `[✓]` `IonLoadingWillPresent` — `void`

**C#:** `EventCallback<IonLoadingPresentEventArgs>`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonLoadingDismissEventArgs>`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback<IonLoadingPresentEventArgs>`

---

## IonMenu `ion-menu`

### `[✓]` `IonDidClose` — `MenuCloseEventDetail`

```ts
export interface MenuCloseEventDetail {
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `IonDidOpen` — `void`

**C#:** `EventCallback`

### `[✓]` `IonWillClose` — `MenuCloseEventDetail`

```ts
export interface MenuCloseEventDetail {
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `IonWillOpen` — `void`

**C#:** `EventCallback`

---

## IonModal `ion-modal`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonModalDismissEventArgs>`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonBreakpointDidChange` — `ModalBreakpointChangeEventDetail`

```ts
export interface ModalBreakpointChangeEventDetail {
  breakpoint: number;
}
```

**C#:** `EventCallback`

### `[✓]` `IonModalDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonModalDismissEventArgs>`

### `[✓]` `IonModalDidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonModalWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonModalDismissEventArgs>`

### `[✓]` `IonModalWillPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonModalDismissEventArgs>`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback`

---

## IonNav `ion-nav`

### `[✗]` `IonNavDidChange` — `void`

**C#:** *(not implemented)*

### `[✗]` `IonNavWillChange` — `void`

**C#:** *(not implemented)*

---

## IonPickerColumn `ion-picker-column`

### `[✓]` `IonChange` — `PickerColumnChangeEventDetail`

```ts
export interface PickerColumnChangeEventDetail {
  value: PickerColumnValue;
}
```

**C#:** `EventCallback<IonPickerColumnIonChangeEventArgs>`

---

## IonPickerLegacy `ion-picker-legacy`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonPickerLegacyDismissEventArgs>`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonPickerDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonPickerLegacyDismissEventArgs>`

### `[✓]` `IonPickerDidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonPickerWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonPickerLegacyDismissEventArgs>`

### `[✓]` `IonPickerWillPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonPickerLegacyDismissEventArgs>`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback`

---

## IonPopover `ion-popover`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonPopoverDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `IonPopoverDidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonPopoverWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `IonPopoverWillPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback`

---

## IonRadio `ion-radio`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

---

## IonRadioGroup `ion-radio-group`

### `[✓]` `IonChange` — `RadioGroupChangeEventDetail`

```ts
export interface RadioGroupChangeEventDetail<T = any> {
  value: T;
  event?: Event;
}
```

**C#:** `EventCallback<IonRadioGroupIonChangeEventArgs>`

---

## IonRange `ion-range`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonChange` — `RangeChangeEventDetail`

```ts
export interface RangeChangeEventDetail {
  value: RangeValue;
}
```

**C#:** `EventCallback<IRangeChangeEventArgs>`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

### `[✓]` `IonInput` — `RangeChangeEventDetail`

```ts
export interface RangeChangeEventDetail {
  value: RangeValue;
}
```

**C#:** `EventCallback<RangeChangeEventArgs>`

### `[✓]` `IonKnobMoveEnd` — `RangeKnobMoveEndEventDetail`

```ts
export interface RangeKnobMoveEndEventDetail {
  value: RangeValue;
}
```

**C#:** `EventCallback<RangeChangeEventArgs>`

### `[✓]` `IonKnobMoveStart` — `RangeKnobMoveStartEventDetail`

```ts
export interface RangeKnobMoveStartEventDetail {
  value: RangeValue;
}
```

**C#:** `EventCallback<RangeChangeEventArgs>`

---

## IonRefresher `ion-refresher`

### `[✓]` `IonPull` — `void`

**C#:** `EventCallback<IonRefresherIonPullEventArgs>`

### `[✓]` `IonRefresh` — `RefresherEventDetail`

```ts
export interface RefresherEventDetail {
  complete(): void;
}
```

**C#:** `EventCallback<IonRefresherIonRefreshEventArgs>`

### `[✓]` `IonStart` — `void`

**C#:** `EventCallback<IonRefresherIonStartEventArgs>`

---

## IonReorderGroup `ion-reorder-group`

### `[✓]` `IonItemReorder` — `ItemReorderEventDetail`

```ts
export interface ItemReorderEventDetail {
  from: number;
  to: number;
  complete: (data?: boolean | any[]) => any;
}
```

**C#:** `EventCallback<IonReorderGroupIonItemReorderEventArgs>`

### `[✗]` `IonReorderEnd` — `ReorderEndEventDetail`

```ts
export interface ReorderEndEventDetail {
  from: number;
  to: number;
  complete: (data?: boolean | any[]) => any;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonReorderMove` — `ReorderMoveEventDetail`

```ts
export interface ReorderMoveEventDetail {
  from: number;
  to: number;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonReorderStart` — `void`

**C#:** *(not implemented)*

---

## IonRoute `ion-route`

### `[✗]` `IonRouteDataChanged` — `any`

```ts
any
```

**C#:** *(not implemented)*

---

## IonRouteRedirect `ion-route-redirect`

### `[✗]` `IonRouteRedirectChanged` — `any`

```ts
any
```

**C#:** *(not implemented)*

---

## IonRouter `ion-router`

### `[✗]` `IonRouteDidChange` — `RouterEventDetail`

```ts
export interface RouterEventDetail {
  from: string | null;
  redirectedFrom: string | null;
  to: string;
}
```

**C#:** *(not implemented)*

### `[✗]` `IonRouteWillChange` — `RouterEventDetail`

```ts
export interface RouterEventDetail {
  from: string | null;
  redirectedFrom: string | null;
  to: string;
}
```

**C#:** *(not implemented)*

---

## IonSearchbar `ion-searchbar`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonCancel` — `void`

**C#:** `EventCallback`

### `[✓]` `IonChange` — `SearchbarChangeEventDetail`

```ts
export interface SearchbarChangeEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonSearchbarChangeEventArgs>`

### `[✓]` `IonClear` — `void`

**C#:** `EventCallback`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

### `[✓]` `IonInput` — `SearchbarInputEventDetail`

```ts
export interface SearchbarInputEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonSearchbarInputEventArgs>`

---

## IonSegment `ion-segment`

### `[✓]` `IonChange` — `SegmentChangeEventDetail`

```ts
export interface SegmentChangeEventDetail {
  value?: SegmentValue;
}
```

**C#:** `EventCallback<IonSegmentIonChangeEventArgs>`

---

## IonSegmentView `ion-segment-view`

### `[✓]` `IonSegmentViewScroll` — `SegmentViewScrollEvent`

```ts
export interface SegmentViewScrollEvent {
  scrollRatio: number;
  isManualScroll: boolean;
}
```

**C#:** `EventCallback<IonSegmentViewScrollEvent>`

---

## IonSelect `ion-select`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonCancel` — `void`

**C#:** `EventCallback`

### `[✗]` `IonChange` — `SelectChangeEventDetail`

```ts
export interface SelectChangeEventDetail<T = any> {
  value: T;
}
```

**C#:** *(not implemented)*

### `[✓]` `IonDismiss` — `void`

**C#:** `EventCallback`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

---

## IonSplitPane `ion-split-pane`

### `[✓]` `IonSplitPaneVisible` — `{ visible: boolean }`

**C#:** `EventCallback`

---

## IonTabs `ion-tabs`

### `[✓]` `IonTabsDidChange` — `{ tab: string }`

**C#:** `EventCallback<IonTabsDidChangeEventArgs>`

### `[✓]` `IonTabsWillChange` — `{ tab: string }`

**C#:** `EventCallback<IonTabsWillChangeEventArgs>`

---

## IonTextarea `ion-textarea`

### `[✓]` `IonBlur` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonTextarea>`

### `[✓]` `IonChange` — `TextareaChangeEventDetail`

```ts
export interface TextareaChangeEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonTextareaChangeEventArgs>`

### `[✓]` `IonFocus` — `FocusEvent`

```ts
Browser FocusEvent
```

**C#:** `EventCallback<IonTextarea>`

### `[✓]` `IonInput` — `TextareaInputEventDetail`

```ts
export interface TextareaInputEventDetail {
  value?: string | null;
  event?: Event;
}
```

**C#:** `EventCallback<IonTextareaInputEventArgs>`

---

## IonToast `ion-toast`

### `[✓]` `DidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonToastDismissEventArgs>`

### `[✓]` `DidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonToastDidDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonToastDismissEventArgs>`

### `[✓]` `IonToastDidPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `IonToastWillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonToastDismissEventArgs>`

### `[✓]` `IonToastWillPresent` — `void`

**C#:** `EventCallback`

### `[✓]` `WillDismiss` — `OverlayEventDetail`

```ts
export interface OverlayEventDetail<T = any> {
  data?: T;
  role?: string;
}
```

**C#:** `EventCallback<IonToastDismissEventArgs>`

### `[✓]` `WillPresent` — `void`

**C#:** `EventCallback`

---

## IonToggle `ion-toggle`

### `[✓]` `IonBlur` — `void`

**C#:** `EventCallback`

### `[✓]` `IonChange` — `ToggleChangeEventDetail`

```ts
export interface ToggleChangeEventDetail<T = any> {
  value: T;
  checked: boolean;
}
```

**C#:** `EventCallback<IonToggleChangeEventArgs>`

### `[✓]` `IonFocus` — `void`

**C#:** `EventCallback`

---

## Summary

**109 / 141** events implemented. **32** missing.

### Missing events

| Component | Blazor Parameter | JS Detail Type |
|-----------|-----------------|----------------|
| `IonActionSheet` | `DidDismiss` | `OverlayEventDetail` — export interface OverlayEventDetail<T = any> { data?: T; role?: string; } |
| `IonActionSheet` | `DidPresent` | `void` |
| `IonActionSheet` | `IonActionSheetDidDismiss` | `OverlayEventDetail` — export interface OverlayEventDetail<T = any> { data?: T; role?: string; } |
| `IonActionSheet` | `IonActionSheetDidPresent` | `void` |
| `IonActionSheet` | `IonActionSheetWillDismiss` | `OverlayEventDetail` — export interface OverlayEventDetail<T = any> { data?: T; role?: string; } |
| `IonActionSheet` | `IonActionSheetWillPresent` | `void` |
| `IonActionSheet` | `WillDismiss` | `OverlayEventDetail` — export interface OverlayEventDetail<T = any> { data?: T; role?: string; } |
| `IonActionSheet` | `WillPresent` | `void` |
| `IonBreadcrumb` | `IonBlur` | `void` |
| `IonBreadcrumb` | `IonFocus` | `void` |
| `IonBreadcrumbs` | `IonCollapsedClick` | `BreadcrumbCollapsedClickEventDetail` — export interface BreadcrumbCollapsedClickEventDetail { ionShadowTarget?: HTMLEle |
| `IonButton` | `IonBlur` | `void` |
| `IonButton` | `IonFocus` | `void` |
| `IonDatetime` | `IonBlur` | `void` |
| `IonDatetime` | `IonCancel` | `void` |
| `IonDatetime` | `IonChange` | `DatetimeChangeEventDetail` — export interface DatetimeChangeEventDetail { value?: string | string[] | null; } |
| `IonDatetime` | `IonFocus` | `void` |
| `IonImg` | `IonError` | `void` |
| `IonImg` | `IonImgDidLoad` | `void` |
| `IonImg` | `IonImgWillLoad` | `void` |
| `IonInput` | `IonInput` | `InputInputEventDetail` — export interface InputInputEventDetail { value?: string | null; event?: Event; } |
| `IonInputOtp` | `IonInput` | `InputOtpInputEventDetail` — export interface InputOtpInputEventDetail { value?: string | null; event?: Event |
| `IonNav` | `IonNavDidChange` | `void` |
| `IonNav` | `IonNavWillChange` | `void` |
| `IonReorderGroup` | `IonReorderEnd` | `ReorderEndEventDetail` — export interface ReorderEndEventDetail { from: number; to: number; complete: (da |
| `IonReorderGroup` | `IonReorderMove` | `ReorderMoveEventDetail` — export interface ReorderMoveEventDetail { from: number; to: number; } |
| `IonReorderGroup` | `IonReorderStart` | `void` |
| `IonRoute` | `IonRouteDataChanged` | `any` |
| `IonRouteRedirect` | `IonRouteRedirectChanged` | `any` |
| `IonRouter` | `IonRouteDidChange` | `RouterEventDetail` — export interface RouterEventDetail { from: string | null; redirectedFrom: string |
| `IonRouter` | `IonRouteWillChange` | `RouterEventDetail` — export interface RouterEventDetail { from: string | null; redirectedFrom: string |
| `IonSelect` | `IonChange` | `SelectChangeEventDetail` — export interface SelectChangeEventDetail<T = any> { value: T; } |