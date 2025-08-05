import Dependencies from '../../feature/Dependencies.js';
import Override from '../../../Core/mixin/Override.js';
import { getLWCElement } from '../../../Core/override/salesforce/Init.js';

/*
 * This override replaces listener target (which is document) with LWC root element
 */
class DependenciesOverrideListenersTarget {
    static get target() {
        return {
            class : Dependencies
        };
    }

    get listenersTarget() {
        return getLWCElement();
    }
}

Override.apply(DependenciesOverrideListenersTarget);
