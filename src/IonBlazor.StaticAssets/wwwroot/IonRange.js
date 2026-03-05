import { dotNetCallbackMethod } from './common.js';
export function setValue(element, value) {
    element.value = value;
}
export function setUpperLowerValue(element, lower, upper) {
    element.value = { lower, upper };
}
export function pinFormatter(element, callback) {
    element.pinFormatter = (async (value) => {
        const result = await callback.invokeMethodAsync(dotNetCallbackMethod, value);
        return `${result}`;
    });
}
