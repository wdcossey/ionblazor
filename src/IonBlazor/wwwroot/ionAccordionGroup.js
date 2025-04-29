export function setValue(element, value) {
    element.value = value;
}

export function getValue(element) {
    const value = element.value;
    return Array.isArray(value) ? value : [ value ];
}