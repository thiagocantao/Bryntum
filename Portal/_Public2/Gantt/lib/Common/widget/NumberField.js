import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import TemplateHelper from '../helper/TemplateHelper.js';
import Field from './Field.js';
import BrowserHelper from '../helper/BrowserHelper.js';

/**
 * @module Common/widget/NumberField
 */

/**
 * Number field widget. Wraps native `<input type="number">`
 *
 * @extends Common/widget/Field
 *
 * @example
 * let number = new NumberField({
 *   min: 1,
 *   max: 5,
 *   value: 3
 * });
 *
 * @classType numberfield
 * @externalexample widget/NumberField.js
 */
export default class NumberField extends Field {
    //region Config

    static get defaultConfig() {
        return {
            /**
             * Min value
             * @config {Number}
             */
            min : null,

            /**
             * Max value
             * @config {Number}
             */
            max : null,

            /**
             * Step size. Use a decimal value to allow decimal input
             * @config {Number}
             */
            step : 1,

            /**
             * Initial value
             * @config {Number}
             */
            value : 0,

            triggers : {
                spin : {
                    type : 'spintrigger'
                }
            }
        };
    }

    internalOnKeyPress(e) {
        if (BrowserHelper.isEdge && e.type === 'keydown') {
            if (e.key === 'ArrowUp') {
                this.doSpinUp();
                e.preventDefault();
            }
            else if (e.key === 'ArrowDown') {
                this.doSpinDown();
                e.preventDefault();
            }
        }
        super.internalOnKeyPress(e);
    }

    //endregion

    inputTemplate() {
        const me = this,
            style = 'inputWidth' in me ? `style="width:${me.inputWidth}${typeof me.inputWidth === 'number' ? 'px' : ''}"` : '';

        return TemplateHelper.tpl`
            <input type="number"
                reference="input"
                min="${me.min}"
                max="${me.max}"
                step="any"
                value="${me._value}"
                autocomplete="${me.autoComplete}"
                placeholder="${me.placeholder}"
                name="${me.name || me.id}"
                id="${me.id}_input"
                ${style}/>
            `;
    }

    set step(step) {
        this.element.classList[step ? 'remove' : 'add']('b-hide-spinner');
        this._step = step;
    }

    get step() {
        return this._step;
    }

    /**
     * Min value
     * @property {Number}
     */
    set min(min) {
        this._min = min;

        if (this.input) {
            this.input.min = min;
        }
    }

    get min() {
        return this._min;
    }

    /**
     * Max value
     * @property {Number}
     */
    set max(max) {
        this._max = max;

        if (this.input) {
            this.input.max = max;
        }
    }

    get max() {
        return this._max;
    }

    doSpinUp() {
        let newValue = (this.value || 0) + this.step,
            { min, max } = this;

        if (!isNaN(min) && newValue < min) {
            newValue = min;

        }
        if (isNaN(max) || newValue <= max) {
            this._isUserAction = true;
            this.value = newValue;
            this._isUserAction = false;
        }
    }

    doSpinDown() {
        let newValue = (this.value || 0) - this.step,
            { min, max } = this;

        if (!isNaN(max) && newValue > max) {
            newValue = max;

        }
        if (isNaN(min) || newValue >= min) {
            this._isUserAction = true;
            this.value = newValue;
            this._isUserAction = false;
        }
    }

    get value() {
        return super.value;
    }

    set value(value) {
        if (value || value === 0) {
            // We insist on a number as the value
            if (typeof value !== 'number') {
                value = (typeof value === 'string') ? parseFloat(value) : Number(value);

                if (isNaN(value)) {
                    value = '';
                }
            }
        }
        else {
            value = this.clearable ? undefined : 0;
        }

        // Reject non-changes & not interested in non-number values
        if (this.value !== value) {
            super.value = value;
        }
    }
}

BryntumWidgetAdapterRegister.register('numberfield', NumberField);
BryntumWidgetAdapterRegister.register('number', NumberField);
