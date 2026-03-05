import { dotNetCallbackMethod } from './common.js';
import type { DotNetObjectReference } from './common.js';

export function maxItems(element: HTMLIonBreadcrumbsElement, value: number): void {
    element.maxItems = value;
}

export function attachIonCollapsedClickListener(element: HTMLIonBreadcrumbsElement, ref: DotNetObjectReference): void {
    element.addEventListener('ionCollapsedClick', (ev: Event) => {
        const customEv = ev as CustomEvent;
        ref.invokeMethodAsync(dotNetCallbackMethod, {
            tagName: (ev.target as HTMLElement).tagName,
            detail: customEv.detail.collapsedBreadcrumbs.map((obj: Record<string, unknown>) => ({
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
