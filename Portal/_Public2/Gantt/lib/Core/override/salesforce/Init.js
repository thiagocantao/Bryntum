import Widget from '../../widget/Widget.js';
import { createTooltip } from '../../widget/Tooltip.js';
import { setupFocusListeners } from '../../GlobalEvents.js';
import EventHelper from '../../helper/EventHelper.js';

let lwcElement;

export default function init(element) {
    lwcElement = element;

    // Destroy default tooltip. It is instantiated before we know which element to listen to
    Widget.tooltip?.destroy();

    setupFocusListeners(element.firstChild, EventHelper, true);

    createTooltip();
}

// This is the key method which allows our code to know which element to use as root
export function getLWCElement() {
    return lwcElement;
}
