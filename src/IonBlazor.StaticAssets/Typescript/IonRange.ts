import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function setValue(element: HTMLIonRangeElement, value: RangeValue): void {
    element.value = value;
}

export function setUpperLowerValue(element: HTMLIonRangeElement, lower: number, upper: number): void {
    element.value = { lower, upper };
}

export function pinFormatter(element: HTMLIonRangeElement, callback: DotNetObjectReference): void {
    element.pinFormatter = (async (value: number) => {
        const result = await callback.invokeMethodAsync(dotNetCallbackMethod, value);
        return `${result}`;
    }) as unknown as PinFormatter;
}
