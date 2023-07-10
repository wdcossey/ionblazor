import { initialize } from '@ionic/core/components';

// import { IonAccordionGroup } from '@ionic/core/components/ion-accordion-group';
// import { IonAccordion } from '@ionic/core/components/ion-accordion';
// import { IonActionSheet } from '@ionic/core/components/ion-action-sheet';
import { IonAlert } from '@ionic/core/components/ion-alert';
import { IonApp } from '@ionic/core/components/ion-app';
// import { IonAvatar } from '@ionic/core/components/ion-avatar';
// import { IonBackButton } from '@ionic/core/components/ion-back-button';
// import { IonBackdrop } from '@ionic/core/components/ion-backdrop';
// import { IonBadge } from '@ionic/core/components/ion-badge';
// import { IonBreadcrumb } from '@ionic/core/components/ion-breadcrumb';
// import { IonBreadcrumbs } from '@ionic/core/components/ion-breadcrumbs';
import { IonButton } from '@ionic/core/components/ion-button';
import { IonButtons } from '@ionic/core/components/ion-buttons';
// import { IonCardContent } from '@ionic/core/components/ion-card-content';
// import { IonCardHeader } from '@ionic/core/components/ion-card-header';
// import { IonCard } from '@ionic/core/components/ion-card';
// import { IonCardSubtitle } from '@ionic/core/components/ion-card-subtitle';
// import { IonCardTitle } from '@ionic/core/components/ion-card-title';
// import { IonCheckbox } from '@ionic/core/components/ion-checkbox';
// import { IonChip } from '@ionic/core/components/ion-chip';
// import { IonCol } from '@ionic/core/components/ion-col';
import { IonContent } from '@ionic/core/components/ion-content';
// import { IonDatetime } from '@ionic/core/components/ion-datetime';
// import { IonFabButton } from '@ionic/core/components/ion-fab-button';
// import { IonFab } from '@ionic/core/components/ion-fab';
// import { IonFabList } from '@ionic/core/components/ion-fab-list';
// import { IonFooter } from '@ionic/core/components/ion-footer';
// import { IonGrid } from '@ionic/core/components/ion-grid';
import { IonHeader } from '@ionic/core/components/ion-header';
import { IonIcon } from '@ionic/core/components/ion-icon';
// import { IonImg } from '@ionic/core/components/ion-img';
// import { IonInfiniteScrollContent } from '@ionic/core/components/ion-infinite-scroll-content';
// import { IonInfiniteScroll } from '@ionic/core/components/ion-infinite-scroll';
import { IonInput } from '@ionic/core/components/ion-input';
import { IonItemDivider } from '@ionic/core/components/ion-item-divider';
import { IonItemGroup } from '@ionic/core/components/ion-item-group';
import { IonItem } from '@ionic/core/components/ion-item';
import { IonItemOption } from '@ionic/core/components/ion-item-option';
import { IonItemOptions } from '@ionic/core/components/ion-item-options';
import { IonItemSliding } from '@ionic/core/components/ion-item-sliding';
import { IonLabel } from '@ionic/core/components/ion-label';
import { IonListHeader } from '@ionic/core/components/ion-list-header';
import { IonList } from '@ionic/core/components/ion-list';
// import { IonLoading } from '@ionic/core/components/ion-loading';
// import { IonMenuButton } from '@ionic/core/components/ion-menu-button';
import { IonMenu } from '@ionic/core/components/ion-menu';
import { IonMenuToggle } from '@ionic/core/components/ion-menu-toggle';
// import { IonModal } from '@ionic/core/components/ion-modal';
// import { IonNav } from '@ionic/core/components/ion-nav';
// import { IonNavLink } from '@ionic/core/components/ion-nav-link';
// import { IonNote } from '@ionic/core/components/ion-note';
// import { IonPickerColumnInternal } from '@ionic/core/components/ion-picker-column-internal';
// import { IonPickerColumn } from '@ionic/core/components/ion-picker-column';
// import { IonPickerInternal } from '@ionic/core/components/ion-picker-internal';
// import { IonPicker } from '@ionic/core/components/ion-picker';
// import { IonPopover } from '@ionic/core/components/ion-popover';
// import { IonProgressBar } from '@ionic/core/components/ion-progress-bar';
// import { IonRadioGroup } from '@ionic/core/components/ion-radio-group';
// import { IonRadio } from '@ionic/core/components/ion-radio';
// import { IonRange } from '@ionic/core/components/ion-range';
// import { IonRefresherContent } from '@ionic/core/components/ion-refresher-content';
// import { IonRefresher } from '@ionic/core/components/ion-refresher';
// import { IonReorderGroup } from '@ionic/core/components/ion-reorder-group';
// import { IonReorder } from '@ionic/core/components/ion-reorder';
// import { IonRippleEffect } from '@ionic/core/components/ion-ripple-effect';
// import { IonRoute } from '@ionic/core/components/ion-route';
// import { IonRouteRedirect } from '@ionic/core/components/ion-route-redirect';
// import { IonRouter } from '@ionic/core/components/ion-router';
// import { IonRouterLink } from '@ionic/core/components/ion-router-link';
// import { IonRouterOutlet } from '@ionic/core/components/ion-router-outlet';
// import { IonRow } from '@ionic/core/components/ion-row';
// import { IonSearchbar } from '@ionic/core/components/ion-searchbar';
// import { IonSegmentButton } from '@ionic/core/components/ion-segment-button';
// import { IonSegment } from '@ionic/core/components/ion-segment';
// import { IonSelect } from '@ionic/core/components/ion-select';
// import { IonSelectOption } from '@ionic/core/components/ion-select-option';
// import { IonSelectPopover } from '@ionic/core/components/ion-select-popover';
// import { IonSkeletonText } from '@ionic/core/components/ion-skeleton-text';
// import { IonSlide } from '@ionic/core/components/ion-slide';
// import { IonSlides } from '@ionic/core/components/ion-slides';
// import { IonSpinner } from '@ionic/core/components/ion-spinner';
// import { IonSplitPane } from '@ionic/core/components/ion-split-pane';
// import { IonTabBar } from '@ionic/core/components/ion-tab-bar';
// import { IonTabButton } from '@ionic/core/components/ion-tab-button';
// import { IonTab } from '@ionic/core/components/ion-tab';
// import { IonTabs } from '@ionic/core/components/ion-tabs';
// import { IonTextarea } from '@ionic/core/components/ion-textarea';
// import { IonText } from '@ionic/core/components/ion-text';
// import { IonThumbnail } from '@ionic/core/components/ion-thumbnail';
import { IonTitle } from '@ionic/core/components/ion-title';
import { IonToast } from '@ionic/core/components/ion-toast';
// import { IonToggle } from '@ionic/core/components/ion-toggle';
import { IonToolbar } from '@ionic/core/components/ion-toolbar';
// import { IonVirtualScroll } from '@ionic/core/components/ion-virtual-scroll';

document.documentElement.classList.add('ion-ce');

function tryDefine(impl, tag) {
    try {
        customElements.define(tag, impl);
    } catch (error) { }
}

initialize();

// tryDefine(IonAccordionGroup, 'ion-accordion-group');
// tryDefine(IonAccordion, 'ion-accordion');
// tryDefine(IonActionSheet, 'ion-action-sheet');
tryDefine(IonAlert, 'ion-alert');
tryDefine(IonApp, 'ion-app');
// tryDefine(IonAvatar, 'ion-avatar');
// tryDefine(IonBackButton, 'ion-back-button');
// tryDefine(IonBackdrop, 'ion-backdrop');
// tryDefine(IonBadge, 'ion-badge');
// tryDefine(IonBreadcrumb, 'ion-breadcrumb');
// tryDefine(IonBreadcrumbs, 'ion-breadcrumbs');
tryDefine(IonButton, 'ion-button');
tryDefine(IonButtons, 'ion-buttons');
// tryDefine(IonCardContent, 'ion-card-content');
// tryDefine(IonCardHeader, 'ion-card-header');
// tryDefine(IonCard, 'ion-card');
// tryDefine(IonCardSubtitle, 'ion-card-subtitle');
// tryDefine(IonCardTitle, 'ion-card-title');
// tryDefine(IonCheckbox, 'ion-checkbox');
// tryDefine(IonChip, 'ion-chip');
// tryDefine(IonCol, 'ion-col');
tryDefine(IonContent, 'ion-content');
// tryDefine(IonDatetime, 'ion-datetime');
// tryDefine(IonFabButton, 'ion-fab-button');
// tryDefine(IonFab, 'ion-fab');
// tryDefine(IonFabList, 'ion-fab-list');
// tryDefine(IonFooter, 'ion-footer');
// tryDefine(IonGrid, 'ion-grid');
tryDefine(IonHeader, 'ion-header');
tryDefine(IonIcon, 'ion-icon');
// tryDefine(IonImg, 'ion-img');
// tryDefine(IonInfiniteScrollContent, 'ion-infinite-scroll-content');
// tryDefine(IonInfiniteScroll, 'ion-infinite-scroll');
tryDefine(IonInput, 'ion-input');
tryDefine(IonItemDivider, 'ion-item-divider');
tryDefine(IonItemGroup, 'ion-item-group');
tryDefine(IonItem, 'ion-item');
tryDefine(IonItemOption, 'ion-item-option');
tryDefine(IonItemOptions, 'ion-item-options');
tryDefine(IonItemSliding, 'ion-item-sliding');
tryDefine(IonLabel, 'ion-label');
tryDefine(IonListHeader, 'ion-list-header');
tryDefine(IonList, 'ion-list');
// tryDefine(IonLoading, 'ion-loading');
// tryDefine(IonMenuButton, 'ion-menu-button');
tryDefine(IonMenu, 'ion-menu');
tryDefine(IonMenuToggle, 'ion-menu-toggle');
// tryDefine(IonModal, 'ion-modal');
// tryDefine(IonNav, 'ion-nav');
// tryDefine(IonNavLink, 'ion-nav-link');
// tryDefine(IonNote, 'ion-note');
// tryDefine(IonPickerColumnInternal, 'ion-picker-column-internal');
// tryDefine(IonPickerColumn, 'ion-picker-column');
// tryDefine(IonPickerInternal, 'ion-picker-internal');
// tryDefine(IonPicker, 'ion-picker');
// tryDefine(IonPopover, 'ion-popover');
// tryDefine(IonProgressBar, 'ion-progress-bar');
// tryDefine(IonRadioGroup, 'ion-radio-group');
// tryDefine(IonRadio, 'ion-radio');
// tryDefine(IonRange, 'ion-range');
// tryDefine(IonRefresherContent, 'ion-refresher-content');
// tryDefine(IonRefresher, 'ion-refresher');
// tryDefine(IonReorderGroup, 'ion-reorder-group');
// tryDefine(IonReorder, 'ion-reorder');
// tryDefine(IonRippleEffect, 'ion-ripple-effect');
// tryDefine(IonRoute, 'ion-route');
// tryDefine(IonRouteRedirect, 'ion-route-redirect');
// tryDefine(IonRouter, 'ion-router');
// tryDefine(IonRouterLink, 'ion-router-link');
// tryDefine(IonRouterOutlet, 'ion-router-outlet');
// tryDefine(IonRow, 'ion-row');
// tryDefine(IonSearchbar, 'ion-searchbar');
// tryDefine(IonSegmentButton, 'ion-segment-button');
// tryDefine(IonSegment, 'ion-segment');
// tryDefine(IonSelect, 'ion-select');
// tryDefine(IonSelectOption, 'ion-select-option');
// tryDefine(IonSelectPopover, 'ion-select-popover');
// tryDefine(IonSkeletonText, 'ion-skeleton-text');
// tryDefine(IonSlide, 'ion-slide');
// tryDefine(IonSlides, 'ion-slides');
// tryDefine(IonSpinner, 'ion-spinner');
// tryDefine(IonSplitPane, 'ion-split-pane');
// tryDefine(IonTabBar, 'ion-tab-bar');
// tryDefine(IonTabButton, 'ion-tab-button');
// tryDefine(IonTab, 'ion-tab');
// tryDefine(IonTabs, 'ion-tabs');
// tryDefine(IonTextarea, 'ion-textarea');
// tryDefine(IonText, 'ion-text');
// tryDefine(IonThumbnail, 'ion-thumbnail');
tryDefine(IonTitle, 'ion-title');
tryDefine(IonToast, 'ion-toast');
// tryDefine(IonToggle, 'ion-toggle');
tryDefine(IonToolbar, 'ion-toolbar');
// tryDefine(IonVirtualScroll, 'ion-virtual-scroll');
