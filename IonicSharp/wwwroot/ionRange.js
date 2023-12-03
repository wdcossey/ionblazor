export function setValue(element, value) {
    element.value = value;
}

export function setUpperLowerValue(element, lower, upper) {
    element.value = { lower: lower, upper: upper };
}

export function pinFormatter(element, callback) {
    element.pinFormatter = async (value) => {
        let result = await callback.invokeMethodAsync(dotNetCallbackMethod, value);
        return `${result}`;
    }
}