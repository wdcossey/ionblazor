import { dotNetCallbackMethod } from './common.js';

export async function presentLoading(message, duration, htmlAttributes, didDismissHandler, didPresentHandler) {
    const loading = document.createElement('ion-loading');
    loading.message = message;
    loading.duration = duration;
    loading.htmlAttributes = htmlAttributes;

    document.body.appendChild(loading);

    loading.addEventListener('didDismiss', (ev) => {
        if (didDismissHandler) {
            didDismissHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes
            });
        }

        setTimeout(function(){ loading.remove() }, 2000);
    });

    loading.addEventListener('didPresent', (ev) => {
        if (didPresentHandler) {
            didPresentHandler.invokeMethodAsync(dotNetCallbackMethod, {
                tagName: ev.target.tagName,
                detail: ev.detail,
                htmlAttributes: ev.target.htmlAttributes
            });
        }
    });

    await loading.present();
}