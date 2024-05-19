export function counterFormatter(element, callback) {
    element.counterFormatter = async (inputLength, maxLength)=> {
        return await callback.invokeMethodAsync('OnCallbackEvent', {inputLength, maxLength});
    };
}

export function counterFormat(element, format) {

    if (format == null)
        return;

    element.counterFormatter = (inputLength, maxLength) => {
        return eval(format);
    };
}

export function setValue(element, value) {
    element.value = value;
    return element.value;
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
