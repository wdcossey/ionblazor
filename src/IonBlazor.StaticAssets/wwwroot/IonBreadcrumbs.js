import { dotNetCallbackMethod } from './common.js';
export function maxItems(element, value) {
    element.maxItems = value;
}
export function attachIonCollapsedClickListener(element, ref) {
    element.addEventListener('ionCollapsedClick', (ev) => {
        const customEv = ev;
        ref.invokeMethodAsync(dotNetCallbackMethod, {
            tagName: ev.target.tagName,
            detail: customEv.detail.collapsedBreadcrumbs.map((obj) => ({
                active: obj['active'],
                collapsed: obj['collapsed'],
                disabled: obj['disabled'],
                download: obj['download'],
                href: obj['href'],
                last: obj['last'],
                textContent: obj['textContent'],
            })),
        });
    });
}
