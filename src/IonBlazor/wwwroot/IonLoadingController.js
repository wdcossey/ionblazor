import { dotNetCallbackMethod } from './common.js';

export async function create(id, message, duration, htmlAttributes, didDismissHandler, didPresentHandler) {

    const loading = document.createElement('ion-loading');

    if (id) {
        loading.setAttribute("id", id);
    }

    loading.message = message;

    if (duration) {
        loading.duration = duration;
    }

    loading.htmlAttributes = htmlAttributes;

    document.body.appendChild(loading);

    if (didDismissHandler) {
        loading.addEventListener('didDismiss', (ev) => {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });

        });
    }

    if (didPresentHandler) {
        loading.addEventListener('didPresent', (ev) => {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes,
                id: ev.target.id
            });
        });
    }

    return id ?? loading.id;
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
    return loading?.remove();
}

function getElement(id) {
    return document.querySelector(`ion-loading#${id}`);
}