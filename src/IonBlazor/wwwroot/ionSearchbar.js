export async function getInputElement(element, callback) {
    let result = await element.getInputElement();
    return await callback.invokeMethodAsync('OnCallbackEvent', result);
}

export function setFocus(element) {
    return element.setFocus();
}

export function setValue(element, value) {
    return element.value = value;
}
