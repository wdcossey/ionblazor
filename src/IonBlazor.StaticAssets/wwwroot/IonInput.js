import { dotNetCallbackMethod } from './common.js';

export function counterFormatter(element, expression) {
    if (!expression || expression.length === 0) {
        element.counterFormatter = undefined;
        return;
    }

    element.counterFormatter = eval(expression);
}

export function setValue(element, value) {
    element.value = value;
    return element.value;
}

export async function getInputElement(element, callback) {
    let result = await element.getInputElement();
    return await callback.invokeMethodAsync(dotNetCallbackMethod, {result});
}

export function setFocus(element) {
    element.setFocus();
}

export function markTouched(element) {
    if (element == null)
        return;

    element.classList.add('ion-touched');
}

export function markUnTouched(element) {
    if (element == null)
        return;

    element.classList.remove('ion-touched');
}

export function markInvalid(element) {
    if (element == null)
        return;

    removeMarking(element);
    element.classList.add('ion-invalid');
}

export function markValid(element) {
    if (element == null)
        return;

    removeMarking(element);
    element.classList.add('ion-valid');
}

export function removeMarking(element) {
    if (element == null)
        return;

    element.classList.remove('ion-invalid');
    element.classList.remove('ion-valid');
}