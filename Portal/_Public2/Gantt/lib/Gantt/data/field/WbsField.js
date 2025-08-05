import DataField from '../../../Core/data/field/DataField.js';

/**
 * @module Gantt/data/field/WbsField
 */

/**
 * This class is used for a WBS (Work Breakdown Structure) field. These fields hold an array of integers describing
 * their breakdown (e.g., `[1, 2, 1]`) and are printed in string form joined by "." (e.g., '1.2.1').
 *
 * @extends Core/data/field/DataField
 */
export default class WbsField extends DataField {
    static get type() {
        return 'wbs';
    }

    static parseValue(value) {
        // https://jsbench.me/fckhc0cs0n/1 (typeof is slightly faster than s?.charAt in Chrome...by about 3%)
        if (typeof value === 'string') {
            value = value.split('.');

            for (let i = value.length; i-- > 0; /* empty */) {
                value[i] = parseInt(value[i], 10);
            }
        }

        return value;
    }

    compare(a, b) {
        if (a === b) {
            return 0;
        }

        if (!a || !b) {
            return a ? 1 : (b ? -1 : 0);
        }

        // https://jsbench.me/sekhc0ro9s/1 (slightly faster to just call fn that has test vs test before call):
        a = WbsField.parseValue(a);
        b = WbsField.parseValue(b);

        const
            lenA = a.length,
            lenB = b.length,
            n = (lenA < lenB) ? lenA : lenB;

        for (let c, i = 0; i < n; ++i) {
            c = a[i] - b[i];

            if (c) {
                return c;
            }
        }

        return lenA - lenB;
    }

    printValue(value) {
        // we are never called w/ value == null
        return (typeof value === 'string') ? value : value.join('.');
    }
}

WbsField.initClass();
