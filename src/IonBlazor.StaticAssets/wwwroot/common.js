export function attachListener(type, element, ref) {
    element.addEventListener(type, (ev) => {
        const customEv = ev;
        ref.invokeMethodAsync(dotNetCallbackMethod, { tagName: ev.target.tagName, detail: customEv.detail });
    });
}
export function attachListeners(configs, element) {
    configs.forEach(function (config) {
        attachListener(config.event, element, config.ref);
    });
}
export const dotNetCallbackMethod = 'OnCallbackEvent';
