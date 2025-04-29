export function isDateEnabled(element, script) {
    element.isDateEnabled = eval(script);
}

export function setValue(element, value) {
    element.value = value;
}

export function cancel(element, closeOverlay) {
    element.cancel(closeOverlay);
}

export function confirm(element, closeOverlay) {
    element.confirm(closeOverlay);
}

export function reset(element, startDate) {
    element.reset(startDate);
}