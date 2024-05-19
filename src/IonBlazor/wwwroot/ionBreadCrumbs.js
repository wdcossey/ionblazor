export function attachIonCollapsedClickListener(element, ref) {
    element.addEventListener('ionCollapsedClick', (ev) => {
        ref.invokeMethodAsync(dotNetCallbackMethod, {
            tagName: ev.target.tagName,
            detail: ev.detail.collapsedBreadcrumbs.map(obj => {
                return { href: obj.href, textContent: obj.textContent }
            })
        });
    });
}