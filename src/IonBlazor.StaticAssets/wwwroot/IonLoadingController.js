import { dotNetCallbackMethod } from './common.js';
export async function create(options, didDismissHandler, didPresentHandler) {
    const loading = document.createElement('ion-loading');
    if (options.id) {
        loading.id = options.id;
    }
    loading.spinner = options.spinner ?? undefined;
    loading.message = options.message;
    loading.cssClass = options.cssClass ?? undefined;
    if (options.showBackdrop !== undefined)
        loading.showBackdrop = options.showBackdrop;
    if (options.duration !== undefined)
        loading.duration = options.duration;
    if (options.translucent !== undefined)
        loading.translucent = options.translucent;
    if (options.animated !== undefined)
        loading.animated = options.animated;
    if (options.backdropDismiss !== undefined)
        loading.backdropDismiss = options.backdropDismiss;
    loading.mode = options.mode ?? undefined;
    if (options.keyboardClose !== undefined)
        loading.keyboardClose = options.keyboardClose;
    loading.htmlAttributes = options.htmlAttributes ?? undefined;
    document.body.appendChild(loading);
    if (didDismissHandler) {
        loading.addEventListener('didDismiss', (ev) => {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id,
            });
        });
    }
    if (didPresentHandler) {
        loading.addEventListener('didPresent', (ev) => {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id,
            });
        });
    }
    return options.id ?? loading.id;
}
export async function present(id) {
    const loading = getElement(id);
    if (loading) {
        await loading.present();
    }
}
export async function presentWithMessage(id, message) {
    const loading = getElement(id);
    if (loading) {
        loading.message = message;
        await loading.present();
    }
}
export function updateMessage(id, message) {
    const loading = getElement(id);
    if (loading) {
        loading.message = message;
    }
}
export function updateDuration(id, duration) {
    const loading = getElement(id);
    if (loading) {
        loading.duration = duration;
    }
}
export function dismiss(id, data, role) {
    const loading = getElement(id);
    return loading?.dismiss(data, role);
}
export function remove(id) {
    const loading = getElement(id);
    loading?.dismiss();
    loading?.remove();
}
function getElement(id) {
    return document.querySelector(`ion-loading#${id}`);
}
