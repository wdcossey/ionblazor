export function isDateEnabled(element, callback) {

    //element.isDateEnabled = async (dateIsoString) => {
    //    const isEnabled = await callback.invokeMethodAsync('OnCallbackEvent', {dateIsoString});
    //    console.log(`${dateIsoString}: ${isEnabled}`);
    //    return isEnabled;
    //}

    element.isDateEnabled = (dateIsoString) => {
        const date = new Date(dateIsoString);
        const utcDay = date.getUTCDay();

        /**
         * Date will be enabled if it is not
         * Sunday or Saturday
         */
        return utcDay !== 0 && utcDay !== 6;
    }
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