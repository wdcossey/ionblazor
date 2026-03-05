/**
 * IonBlazor Parameter Audit Script
 * Compares Ionic Stencil props (core.json) against C# [Parameter] implementations.
 * Run: node tools/compare-parameters.mjs
 * Output: IONIC_PARAMETERS.md
 */

import { readFileSync, writeFileSync, existsSync } from 'fs';

const COMPONENTS_DIR = './src/IonBlazor.Components/Components';
const CORE_JSON = './node_modules/@ionic/docs/core.json';
const OUTPUT_FILE = './IONIC_PARAMETERS.md';
const TODAY = new Date().toISOString().slice(0, 10);

// Detect Ionic version from installed @ionic/docs
let ionicVersion = 'unknown';
try {
  const docsPkg = JSON.parse(readFileSync('./node_modules/@ionic/docs/package.json', 'utf-8'));
  ionicVersion = docsPkg.version;
} catch { /* ignore */ }

// ─── Tag → relative razor path (relative to COMPONENTS_DIR) ─────────────────
const TAG_MAP = {
  'ion-accordion':               'IonAccordion/IonAccordion',
  'ion-accordion-group':         'IonAccordion/IonAccordionGroup',
  'ion-action-sheet':            'IonActionSheet/IonActionSheet',
  'ion-alert':                   'IonAlert/IonAlert',
  'ion-app':                     'IonApp/IonApp',
  'ion-avatar':                  'IonAvatar/IonAvatar',
  'ion-back-button':             'IonToolbar/IonBackButton',
  'ion-backdrop':                'IonBackdrop/IonBackdrop',
  'ion-badge':                   'IonBadge/IonBadge',
  'ion-breadcrumb':              'IonBreadcrumbs/IonBreadcrumb',
  'ion-breadcrumbs':             'IonBreadcrumbs/IonBreadcrumbs',
  'ion-button':                  'IonButton/IonButton',
  'ion-buttons':                 'IonToolbar/IonButtons',
  'ion-card':                    'IonCard/IonCard',
  'ion-card-content':            'IonCard/IonCardContent',
  'ion-card-header':             'IonCard/IonCardHeader',
  'ion-card-subtitle':           'IonCard/IonCardSubtitle',
  'ion-card-title':              'IonCard/IonCardTitle',
  'ion-checkbox':                'IonCheckbox/IonCheckbox',
  'ion-chip':                    'IonChip/IonChip',
  'ion-col':                     'IonGrid/IonCol',
  'ion-content':                 'IonContent/IonContent',
  'ion-datetime':                'IonDateTime/IonDateTime',
  'ion-datetime-button':         'IonDateTime/IonDateTimeButton',
  'ion-fab':                     'IonFab/IonFab',
  'ion-fab-button':              'IonFab/IonFabButton',
  'ion-fab-list':                'IonFab/IonFabList',
  'ion-footer':                  'IonFooter/IonFooter',
  'ion-grid':                    'IonGrid/IonGrid',
  'ion-header':                  'IonHeader/IonHeader',
  'ion-img':                     'IonImg/IonImg',
  'ion-infinite-scroll':         'IonInfiniteScroll/IonInfiniteScroll',
  'ion-infinite-scroll-content': 'IonInfiniteScroll/IonInfiniteScrollContent',
  'ion-input':                   'IonInput/IonInput',
  'ion-input-otp':               'IonInputOtp/IonInputOtp',
  'ion-input-password-toggle':   'IonInputPasswordToggle/IonInputPasswordToggle',
  'ion-item':                    'IonItem/IonItem',
  'ion-item-divider':            'IonItem/IonItemDivider',
  'ion-item-group':              'IonItem/IonItemGroup',
  'ion-item-option':             'IonItem/IonItemOption',
  'ion-item-options':            'IonItem/IonItemOptions',
  'ion-item-sliding':            'IonItem/IonItemSliding',
  'ion-label':                   'IonLabel/IonLabel',
  'ion-list':                    'IonList/IonList',
  'ion-list-header':             'IonList/IonListHeader',
  'ion-loading':                 'IonLoading/IonLoading',
  'ion-menu':                    'IonMenu/IonMenu',
  'ion-menu-button':             'IonMenu/IonMenuButton',
  'ion-menu-toggle':             'IonMenu/IonMenuToggle',
  'ion-modal':                   'IonModal/IonModal',
  'ion-note':                    'IonNote/IonNote',
  'ion-picker':                  'IonPicker/IonPicker',
  'ion-picker-column':           'IonPicker/IonPickerColumn',
  'ion-picker-column-option':    'IonPicker/IonPickerColumnOption',
  'ion-picker-legacy':           'IonPickerLegacy/IonPickerLegacy',
  'ion-popover':                 'IonPopover/IonPopover',
  'ion-progress-bar':            'IonProgressBar/IonProgressBar',
  'ion-radio':                   'IonRadio/IonRadio',
  'ion-radio-group':             'IonRadioGroup/IonRadioGroup',
  'ion-range':                   'IonRange/IonRange',
  'ion-refresher':               'IonRefresher/IonRefresher',
  'ion-refresher-content':       'IonRefresherContent/IonRefresherContent',
  'ion-reorder':                 'IonReorder/IonReorder',
  'ion-reorder-group':           'IonReorder/IonReorderGroup',
  'ion-ripple-effect':           'IonRippleEffect/IonRippleEffect',
  'ion-row':                     'IonGrid/IonRow',
  'ion-searchbar':               'IonSearchbar/IonSearchbar',
  'ion-segment':                 'IonSegment/IonSegment',
  'ion-segment-button':          'IonSegment/IonSegmentButton',
  'ion-segment-content':         'IonSegment/IonSegmentContent',
  'ion-segment-view':            'IonSegment/IonSegmentView',
  'ion-select':                  'IonSelect/IonSelect',
  'ion-select-option':           'IonSelect/IonSelectOption',
  'ion-skeleton-text':           'IonSkeletonText/IonSkeletonText',
  'ion-spinner':                 'IonSpinner/IonSpinner',
  'ion-split-pane':              'IonSplitPane/IonSplitPane',
  'ion-tab':                     'IonTabs/IonTab',
  'ion-tab-bar':                 'IonTabs/IonTabBar',
  'ion-tab-button':              'IonTabs/IonTabButton',
  'ion-tabs':                    'IonTabs/IonTabs',
  'ion-text':                    'IonText/IonText',
  'ion-textarea':                'IonTextarea/IonTextarea',
  'ion-thumbnail':               'IonThumbnail/IonThumbnail',
  'ion-title':                   'IonToolbar/IonTitle',
  'ion-toast':                   'IonToast/IonToast',
  'ion-toggle':                  'IonToggle/IonToggle',
  'ion-toolbar':                 'IonToolbar/IonToolbar',
  // Not wrapped - routing components (Ionic router is not used in Blazor)
  // 'ion-nav': null,
  // 'ion-nav-link': null,
  // 'ion-route': null,
  // 'ion-route-redirect': null,
  // 'ion-router': null,
  // 'ion-router-link': null,
  // 'ion-router-outlet': null,
  // 'ion-select-modal': null,  // internal Ionic component
};

// Tags intentionally not wrapped, with reasons
const UNWRAPPED_TAG_REASONS = {
  'ion-nav':            'Routing — handled by Blazor router, not wrapped',
  'ion-nav-link':       'Routing — handled by Blazor router, not wrapped',
  'ion-route':          'Routing — handled by Blazor router, not wrapped',
  'ion-route-redirect': 'Routing — handled by Blazor router, not wrapped',
  'ion-router':         'Routing — handled by Blazor router, not wrapped',
  'ion-router-link':    'Routing — handled by Blazor router, not wrapped',
  'ion-router-outlet':  'Routing — handled by Blazor router, not wrapped',
  'ion-select-modal':   'Internal Ionic component — not intended for direct consumer use',
};
const UNWRAPPED_TAGS = new Set(Object.keys(UNWRAPPED_TAG_REASONS));

// IonBlazor-only components (no Ionic counterpart)
const BLAZOR_ONLY_COMPONENTS = [
  'IonList/IonListOf',
  'IonSelect/IonSelectOf',
  'IonSelect/IonSelectOptionOf',
  'IonPickerLegacy/IonSimplePickerLegacy',
];

// Prop types that are JS functions / not serializable as HTML attributes
function isJsFunctionType(type) {
  return (
    type.includes('=>') ||
    type.includes('Animation') ||
    type.includes('Function') ||
    type.includes('AnimationBuilder')
  );
}

// Classify the Ionic type into a simple category
function classifyIonicType(prop) {
  const t = prop.complexType?.resolved ?? prop.type;
  if (isJsFunctionType(t)) return 'js-function';
  if (t.includes('boolean')) return 'boolean';
  if (t.includes('number')) return 'number';
  if (t.includes('string') || t.includes('"')) return 'string';
  if (t === 'any' || t.includes('any')) return 'any';
  return 'other';
}

// Convert kebab-case attr to PascalCase C# name
function attrToPascal(attr) {
  return attr
    .split('-')
    .map(s => s.charAt(0).toUpperCase() + s.slice(1))
    .join('');
}

// Read file safely
function readFile(path) {
  if (!existsSync(path)) return null;
  return readFileSync(path, 'utf-8');
}

// Convert camelCase to kebab-case (e.g. autoHide -> auto-hide)
function camelToKebab(str) {
  return str.replace(/([A-Z])/g, c => '-' + c.toLowerCase());
}

// Extract HTML attribute names rendered in a .razor template
// Handles: attr="@...", attr = "@...", attr='@...' (with or without spaces around =)
// Also normalises camelCase attrs (e.g. autoHide) to kebab-case (auto-hide)
function extractRazorAttrs(razorContent) {
  if (!razorContent) return new Set();
  const attrs = new Set();
  const re = /([\w][\w-]*)\s*=\s*["']@/g;
  let m;
  while ((m = re.exec(razorContent)) !== null) {
    const raw = m[1];
    attrs.add(raw.toLowerCase());
    // Also add kebab-case version of camelCase attrs
    const kebab = camelToKebab(raw).toLowerCase();
    if (kebab !== raw.toLowerCase()) attrs.add(kebab);
  }
  return attrs;
}

// Extract [Parameter] property names from a .razor.cs file
function extractCsParameters(csContent) {
  if (!csContent) return new Map(); // name -> full type string
  const params = new Map();
  // Match [Parameter] or [Parameter, EditorRequired] etc. (with possible other attributes)
  // followed by public TYPE? NAME { get; ... }
  const re = /\[Parameter[^\]]*\][^\[]*?public\s+([\w\?\[\]<>, .]+?)\s+([\w]+)\s*\{[^}]*?(?:get|set)/gs;
  let m;
  while ((m = re.exec(csContent)) !== null) {
    const type = m[1].trim();
    const name = m[2].trim();
    // Skip ChildContent and Attributes (framework params)
    if (name === 'ChildContent' || name === 'Attributes') continue;
    params.set(name, type);
  }
  return params;
}

// Known annotations for specific component+prop combinations — overrides/augments status notes
const KNOWN_NOTES = {
  'ion-accordion-group:value':    'Computed `ValueTask<IEnumerable<string>>` set via JS — not a plain [Parameter]; see `SetValueAsync`/`GetValueAsync`',
  'ion-button:routerDirection':   '[Parameter] present but Blazor handles routing via `href`/`NavigationManager`; HTML attr likely intentionally omitted',
  'ion-card:routerDirection':     '[Parameter] present but Blazor handles routing via `href`/`NavigationManager`; HTML attr likely intentionally omitted',
  'ion-modal:breakpoints':        'Array prop — no HTML attribute in 8.8.0 (attr removed in this version); must be set via JS interop; [Parameter] is correct but template rendering is N/A',
  'ion-modal:presentingElement':  'DOM element reference — must be set via JS interop, not an HTML attribute; [Parameter] correct',
  'ion-popover:event':            'MouseEvent object — must be set via JS interop, not an HTML attribute; [Parameter] correct',
  'ion-select-option:disabled':   'Defined in `IonSelectOptionBase<TValue>` base class — false positive (regex only scans direct .razor.cs)',
  'ion-select-option:value':      'Defined as generic `TValue?` in `IonSelectOptionBase<TValue>` — false positive (regex only scans direct .razor.cs)',
  'ion-spinner:duration':         '[Parameter] exists in .razor.cs but `duration` attr is not rendered in the .razor template — likely a bug',
};

// Check if a prop is handled via a Builder pattern (e.g. buttons -> ButtonsBuilder)
function hasBuilderPattern(csContent, propName) {
  if (!csContent) return false;
  // e.g. "buttons" -> look for "ButtonsBuilder" or similar
  const pascal = propName.charAt(0).toUpperCase() + propName.slice(1);
  // Strip trailing 's' to get singular form for builder lookup
  const singular = pascal.endsWith('s') ? pascal.slice(0, -1) : pascal;
  return csContent.includes(`${pascal}Builder`) || csContent.includes(`${singular}Builder`);
}

// Determine expected C# type for an Ionic prop
function expectedCsType(prop) {
  const category = classifyIonicType(prop);
  const isOptional = prop.optional || prop.type.includes('undefined');
  switch (category) {
    case 'boolean':  return isOptional ? 'bool?' : 'bool?'; // IonBlazor always uses bool?
    case 'number':   return 'double?' ;
    case 'string':   return 'string?';
    case 'js-function': return 'N/A (JS function)';
    default:         return 'object?';
  }
}

// Extract bool-specific [Parameter] info: name -> { type: 'bool'|'bool?', csDefault: 'true'|'false'|null }
// Captures optional explicit initialiser (= true / = false) after the property body
function extractBoolParameters(csContent) {
  if (!csContent) return new Map();
  const params = new Map();
  // Matches: [Parameter...] ... public bool?/bool NAME { get/set/init ... } [= true|false]
  const re = /\[Parameter[^\]]*\][^\[]*?public\s+(bool\??)\s+(\w+)\s*\{[^}]*\}\s*(?:=\s*(true|false))?/gs;
  let m;
  while ((m = re.exec(csContent)) !== null) {
    params.set(m[2].trim(), { type: m[1].trim(), csDefault: m[3] ?? null });
  }
  return params;
}

// ─── Boolean analysis ─────────────────────────────────────────────────────────
function generateBoolAnalysis() {
  const defaultTrueRows = [];  // non-optional Ionic booleans with default=true
  const optionalRows    = [];  // optional Ionic booleans (boolean | undefined)
  const bugRows         = [];  // Category D: bool without default where Ionic default=true

  for (const component of coreData.components) {
    const tag = component.tag;
    const relPath = TAG_MAP[tag];
    if (!relPath) continue; // unwrapped component

    const csContent    = readFile(`${COMPONENTS_DIR}/${relPath}.razor.cs`);
    const boolParams   = extractBoolParameters(csContent);
    const componentName = relPath.split('/').pop();

    for (const prop of (component.props ?? [])) {
      if (prop.name.startsWith('on')) continue;
      const ionicType = prop.complexType?.resolved ?? prop.type;
      if (!ionicType.includes('boolean')) continue;
      // Skip mixed function+boolean types (e.g. canDismiss, compareWith, isDateEnabled)
      if (isJsFunctionType(ionicType)) continue;

      const ionicDefault = prop.default
        ?? prop.docsTags?.find(t => t.name === 'default')?.text
        ?? null;
      const isOptional  = prop.optional || ionicType.includes('undefined');
      const propPascal  = prop.attr ? attrToPascal(prop.attr) : attrToPascal(prop.name);

      // Find C# param (case-insensitive)
      const csEntry = [...boolParams.entries()].find(
        ([k]) => k.toLowerCase() === propPascal.toLowerCase()
      );
      const csType   = csEntry?.[1].type    ?? '—';
      const csDef    = csEntry?.[1].csDefault ?? null;

      // Display value for C# default column
      let csDefDisplay;
      if (!csEntry) {
        csDefDisplay = '—';
      } else if (csType === 'bool?') {
        csDefDisplay = csDef ? `= ${csDef}` : '*(null)*';
      } else {
        // bool (non-nullable)
        csDefDisplay = csDef ? `= ${csDef}` : '*(implicit false)*';
      }

      // Classify
      let category, status;
      if (!csEntry) {
        category = '—'; status = '❌ not found';
      } else if (isOptional) {
        category = 'optional';
        status = csType === 'bool?' ? '✅' : '⚠️ should be `bool?`';
      } else if (ionicDefault === 'true') {
        if (csType === 'bool?') {
          // null → attr omitted → Ionic default true — correct but invisible in IDE
          category = 'B'; status = '✅ B';
        } else if (csDef === 'true') {
          // bool = true — explicit, gold standard
          category = 'C'; status = '✅ C';
        } else if (csDef === null) {
          // bool without default → implicit false → always renders "false" → overrides Ionic default
          category = 'D'; status = '❌ D — bug';
        } else {
          category = '?'; status = '⚠️';
        }
      } else {
        // default=false (or null/expression — non-true defaults)
        if (csType === 'bool') {
          category = 'A'; status = csDef === null ? '✅ A' : `✅ A (= ${csDef})`;
        } else {
          category = 'A'; status = '✅ A';
        }
      }

      const row = { tag, componentName, prop: prop.name, ionicDefault: ionicDefault ?? '—',
        csType, csDefDisplay, category, status };

      if (isOptional) {
        optionalRows.push(row);
      } else if (ionicDefault === 'true') {
        defaultTrueRows.push(row);
        if (category === 'D') bugRows.push(row);
      }
    }
  }

  // ─── Format tables ──────────────────────────────────────────────────────────
  const ROW_COLS = ['Component', 'Ionic Prop', 'Ionic Default', 'C# Type', 'C# Default', 'Cat', 'Status'];
  const SEP      = ROW_COLS.map(() => '---');
  const fmtRow   = r =>
    `| \`${r.componentName}\` | \`${r.prop}\` | \`${r.ionicDefault}\` | \`${r.csType}\` | ${r.csDefDisplay} | ${r.category} | ${r.status} |`;

  const header = [`| ${ROW_COLS.join(' | ')} |`, `| ${SEP.join(' | ')} |`];

  const bugSection = bugRows.length > 0 ? [
    '',
    `### ❌ Category D — Bugs (bool without default where Ionic default = \`true\`)`,
    '',
    'These C# `bool` parameters have no explicit default (`= true`), so the C# default of `false`',
    'is always rendered into the HTML attribute, overriding the Ionic component\'s own `true` default.',
    '',
    ...header,
    ...bugRows.map(fmtRow),
  ] : [
    '',
    `### ❌ Category D — Bugs`,
    '',
    '*None found.*',
  ];

  return [
    `## Boolean Parameter Analysis`,
    '',
    '### Background: `bool` vs `bool?` and Stencil attribute reflection',
    '',
    'Stencil boolean props work as follows:',
    '',
    '- **Attribute omitted** → Stencil uses the prop\'s declared `@Prop()` default',
    '- **Attribute = `"true"`** → Stencil converts to JS `true`',
    '- **Attribute = `"false"`** → Stencil converts to JS `false`',
    '',
    'Most Ionic props have `reflectToAttr: false` — the attribute is read once at initialisation',
    'and not written back. IonBlazor\'s `BooleanExtensions.AsString()` has two overloads:',
    '',
    '```',
    'bool.AsString()  → always "true" or "false"   (attribute always rendered)',
    'bool?.AsString() → null | "true" | "false"    (null = attribute omitted = Ionic default applies)',
    '```',
    '',
    '### Category key',
    '',
    '| Cat | Ionic type | Ionic default | C# type | Effect |',
    '|-----|-----------|--------------|---------|--------|',
    '| **A** | `boolean` | `false` | `bool` or `bool?` | Attr omitted or "false" — Ionic default matches ✓ |',
    '| **B** | `boolean` | `true` | `bool?` | Attr omitted → Ionic uses `true` — correct, but default is invisible in IDE |',
    '| **C** | `boolean` | `true` | `bool = true` | Attr always rendered as "true" — explicit, gold standard ✓ |',
    '| **D** | `boolean` | `true` | `bool` *(no default)* | Attr always rendered as "false" — **overrides Ionic default** ❌ |',
    '',
    `### Non-optional Ionic booleans with \`default = true\` (${defaultTrueRows.length} props)`,
    '',
    ...header,
    ...defaultTrueRows.map(fmtRow),
    ...bugSection,
    '',
    `### Optional Ionic booleans (\`boolean | undefined\`) (${optionalRows.length} props)`,
    '',
    '`bool?` is the correct C# type for all of these — the attribute should be omitted when null.',
    '',
    ...header,
    ...optionalRows.map(fmtRow),
    '',
  ].join('\n');
}




const coreData = JSON.parse(readFileSync(CORE_JSON, 'utf-8'));

let missingCount = 0;
let warningCount = 0;
let correctCount = 0;

const sections = [];
const missingList = [];
const warningList = [];

// Track which Ionic tags have no wrapper
const tagsMissingWrapper = [];

for (const component of coreData.components) {
  const tag = component.tag;
  const props = component.props ?? [];

  if (UNWRAPPED_TAGS.has(tag)) {
    tagsMissingWrapper.push({ tag, reason: UNWRAPPED_TAG_REASONS[tag] });
    continue;
  }

  const relPath = TAG_MAP[tag];
  if (relPath === undefined) {
    tagsMissingWrapper.push({ tag, reason: 'No wrapper found' });
    continue;
  }

  const razorPath = `${COMPONENTS_DIR}/${relPath}.razor`;
  const csPath    = `${COMPONENTS_DIR}/${relPath}.razor.cs`;

  const razorContent = readFile(razorPath);
  const csContent    = readFile(csPath);

  const razorAttrs  = extractRazorAttrs(razorContent);
  const csParams    = extractCsParameters(csContent);

  const componentName = relPath.split('/').pop();
  const rows = [];

  for (const prop of props) {
    // Skip event props (they are in IONIC_EVENTS.md)
    if (prop.name.startsWith('on') || prop.name === 'events') continue;

    const ionicPropName = prop.name;
    const ionicType     = prop.complexType?.resolved ?? prop.type;
    const ionicDefault  = prop.default ?? prop.docsTags?.find(t => t.name === 'default')?.text ?? '—';
    const htmlAttr      = prop.attr ?? '—';
    const category      = classifyIonicType(prop);

    // Derive expected C# name from the attr (kebab-case -> PascalCase)
    const expectedCsName = htmlAttr !== '—' ? attrToPascal(htmlAttr) : attrToPascal(ionicPropName);

    // Check presence
    const attrInRazor   = htmlAttr !== '—' && razorAttrs.has(htmlAttr.toLowerCase());
    // Look for the C# parameter by exact name or case-insensitive
    const csParamEntry  = [...csParams.entries()].find(
      ([k]) => k.toLowerCase() === expectedCsName.toLowerCase()
    );
    const inCsParams    = !!csParamEntry;
    const actualCsName  = csParamEntry?.[0] ?? expectedCsName;
    const actualCsType  = csParamEntry?.[1] ?? '—';

    let status;
    let note = '';

    if (category === 'js-function') {
      // JS-function types are intentionally not wrapped as simple attributes
      status = '— (JS fn)';
      note = 'JS function type — not applicable as HTML attribute';
    } else if (ionicPropName === 'htmlAttributes') {
      // htmlAttributes is always covered by AdditionalAttributes ([Parameter(CaptureUnmatchedValues=true)])
      status = '✅';
      note = 'Covered by `AdditionalAttributes` ([Parameter(CaptureUnmatchedValues = true)])';
      correctCount++;
    } else if (attrInRazor && inCsParams) {
      status = '✅';
      correctCount++;
    } else if (!attrInRazor && !inCsParams) {
      // Check for builder pattern handling complex array props
      if (hasBuilderPattern(csContent, ionicPropName)) {
        status = '⚙️ (builder)';
        note = `Handled via IonBlazor builder pattern (see \`${ionicPropName.charAt(0).toUpperCase() + ionicPropName.slice(1)}Builder\` parameter)`;
        correctCount++;
      } else {
        status = '❌';
        missingCount++;
        missingList.push(`- \`${tag}\` → \`${ionicPropName}\``);
      }
    } else {
      status = '⚠️';
      warningCount++;
      if (!attrInRazor && inCsParams) {
        note = `[Parameter] exists but attr \`${htmlAttr}\` not rendered in .razor`;
      } else if (attrInRazor && !inCsParams) {
        note = `Attr \`${htmlAttr}\` rendered in .razor but no [Parameter] found in .razor.cs`;
      }
      warningList.push(`- \`${tag}\` → \`${ionicPropName}\` — ${note || 'partial match'}`);
    }

    const ionicTypeShort = ionicType.length > 50 ? ionicType.slice(0, 47) + '…' : ionicType;
    // Append known contextual note if available
    const knownNote = KNOWN_NOTES[`${tag}:${ionicPropName}`];
    const finalNote = knownNote ? (note ? `${note}; ${knownNote}` : knownNote) : note;
    const noteCol = finalNote ? ` _(${finalNote})_` : '';

    rows.push(
      `| \`${ionicPropName}\` | \`${ionicTypeShort}\` | \`${ionicDefault}\` | \`${htmlAttr}\` | \`${actualCsName}\` | \`${actualCsType}\` | ${status}${noteCol} |`
    );
  }

  // Note any IonBlazor-specific [Parameter]s (not in Ionic props list)
  // Exclude event callbacks (EventCallback) — they're covered in IONIC_EVENTS.md
  const ionicPropNames = new Set(
    props.map(p => attrToPascal(p.attr ?? p.name).toLowerCase())
  );
  const extraParams = [...csParams.entries()].filter(([k, v]) => {
    if (ionicPropNames.has(k.toLowerCase())) return false;
    if (v.includes('EventCallback')) return false; // covered by IONIC_EVENTS.md
    if (v.includes('RenderFragment')) return false; // ChildContent variants
    return true;
  });
  const extraNotes = extraParams.map(
    ([k, v]) => `> IonBlazor-specific: \`${k}\` (\`${v}\`) — no Ionic prop counterpart`
  );

  const tableHeader = [
    `| Ionic Prop | Ionic Type | Default | HTML Attr | C# Parameter | C# Type | Status |`,
    `|------------|------------|---------|-----------|--------------|---------|--------|`,
  ];

  const block = [
    `### ${componentName} (\`${tag}\`)`,
    '',
    ...(rows.length > 0 ? [...tableHeader, ...rows] : ['_No props._']),
    ...(extraNotes.length > 0 ? ['', ...extraNotes] : []),
    '',
  ].join('\n');

  sections.push(block);
}

const boolAnalysisSection = generateBoolAnalysis();

// ─── Blazor-only component notes ──────────────────────────────────────────────
const blazorOnlySection = [
  `## IonBlazor-Only Components`,
  '',
  'These components exist in IonBlazor but have no counterpart in Ionic\'s Stencil metadata:',
  '',
  ...BLAZOR_ONLY_COMPONENTS.map(p => {
    const name = p.split('/').pop();
    return `- \`${name}\` (in \`${p}\`)`;
  }),
  '',
].join('\n');

// ─── Not-wrapped Ionic components note ────────────────────────────────────────
const notWrappedSection = [
  `## Ionic Components Without a Blazor Wrapper`,
  '',
  '| Ionic Tag | Reason |',
  '|-----------|--------|',
  ...tagsMissingWrapper.map(({ tag, reason }) => `| \`${tag}\` | ${reason} |`),
  '',
].join('\n');

// ─── Issues summary ───────────────────────────────────────────────────────────
const issuesSummary = [
  `## Issues Summary`,
  '',
  `| Symbol | Count |`,
  `|--------|-------|`,
  `| ✅ Correct | ${correctCount} |`,
  `| ❌ Missing | ${missingCount} |`,
  `| ⚠️ Issues | ${warningCount} |`,
  '',
  `### ❌ Missing Parameters`,
  '',
  missingList.length > 0 ? missingList.join('\n') : '*None.*',
  '',
  `### ⚠️ Parameter Issues`,
  '',
  warningList.length > 0 ? warningList.join('\n') : '*None.*',
  '',
].join('\n');

// ─── Version delta note ───────────────────────────────────────────────────────
// Hardcoded known differences between the pinned package.json version (8.7.2)
// and the latest audit version (detected from node_modules).
// Update this list whenever the audit is re-run against a new version.
const VERSION_DELTA_NOTES = [
  {
    version: '8.8.0',
    changes: [
      '`ion-segment-view` — new prop `swipeGesture` (`boolean`, default `true`) added',
      '`ion-modal.breakpoints` — `attr` field removed; prop is now JS-only (no HTML attribute binding)',
    ],
  },
];

const pinnedVersion = '8.7.2';
const deltaSection = VERSION_DELTA_NOTES.length > 0 && ionicVersion !== pinnedVersion ? [
  `## Version Delta: ${pinnedVersion} (pinned) → ${ionicVersion} (audited)`,
  '',
  'The following changes were introduced between the pinned version and the audited version:',
  '',
  ...VERSION_DELTA_NOTES.flatMap(({ version, changes }) => [
    `### ${version}`,
    '',
    ...changes.map(c => `- ${c}`),
    '',
  ]),
].join('\n') : '';

// ─── Full document ────────────────────────────────────────────────────────────
const doc = [
  `# Ionic Parameters Audit`,
  '',
  `Comparing \`node_modules/@ionic/docs/core.json\` (Stencil metadata) against C# \`[Parameter]\``,
  `implementations in \`src/IonBlazor.Components/Components/\`.`,
  '',
  `> Generated: ${TODAY} · Ionic version: **${ionicVersion}** (package.json pins \`^${pinnedVersion}\`)`,
  '',
  '---',
  '',
  `## Legend`,
  '',
  `| Symbol | Meaning |`,
  `|--------|---------|`,
  `| ✅ | Correct — attr rendered in .razor and [Parameter] exists in .razor.cs |`,
  `| ⚙️ (builder) | Handled via an IonBlazor builder pattern instead of a plain [Parameter] |`,
  `| ⚠️ | Implemented but with issues (attr not rendered, parameter missing, etc.) |`,
  `| ❌ | Missing — Ionic prop not implemented in C# at all |`,
  `| — (JS fn) | JS function type — not applicable as a simple HTML attribute |`,
  '',
  '---',
  '',
  issuesSummary,
  '---',
  '',
  ...(deltaSection ? [deltaSection, '---', ''] : []),
  `## Full Component Parameter Map`,
  '',
  ...sections,
  '---',
  '',
  blazorOnlySection,
  '---',
  '',
  notWrappedSection,
  '---',
  '',
  boolAnalysisSection,
].join('\n');

writeFileSync(OUTPUT_FILE, doc, 'utf-8');
console.log(`Written: ${OUTPUT_FILE}`);
console.log(`✅ ${correctCount}  ❌ ${missingCount}  ⚠️ ${warningCount}`);
