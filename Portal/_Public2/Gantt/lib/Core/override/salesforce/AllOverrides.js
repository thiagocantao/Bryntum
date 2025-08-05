// import createElementFromTemplate override first, as one of the patches below will rely on it
import './DomHelperOverride.js';

import './BrowserHelperOverridePointerEvents.js';
import './ButtonOverrideMenuContainerElement.js';
import './DomHelperOverrideActiveElement.js';
import './DomHelperOverrideElementFromPoint.js';
import './DomHelperOverrideForEachSelector.js';
import './DomHelperOverrideGetCommonAncestor.js';
import './DomHelperOverrideIsVisible.js';
import './DragHelperOverrideListenersTarget.js';
import './EventHelperOverride.js';
import './FieldOverrideAutocomplete.js';
import './NavigatorOverride.js';
import './ObjectsOverrideIsObject.js';
import './PanelOverride.js';
import './TooltipOverrideListenersTarget.js';
import './WidgetOverrideFloatRoot.js';
import './WidgetOverrideFromElement.js';
import './WidgetOverrideSetDragImage.js';
import './WidgetOverrideToFront.js';

import init, { getLWCElement } from './Init.js';

export default init;

export { getLWCElement };
