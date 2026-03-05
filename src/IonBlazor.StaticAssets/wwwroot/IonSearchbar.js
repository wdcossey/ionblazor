export async function getInputElement(element, callback) {
    const result = await element.getInputElement();
    return callback.invokeMethodAsync('OnCallbackEvent', result);
}
export function setFocus(element) {
    return element.setFocus();
}
export function setValue(element, value) {
    return (element.value = value);
}
