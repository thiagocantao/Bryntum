import Field from './Field.js';
import BryntumWidgetAdapterRegister from '../adapter/widget/util/BryntumWidgetAdapterRegister.js';
import TemplateHelper from '../helper/TemplateHelper.js';
import DomHelper from '../helper/DomHelper.js';

//TODO: label should be own element

/**
 * @module Common/widget/TextField
 */

/**
 * Textfield widget. Wraps native &lt;input type="text"&gt;
 *
 * @extends Common/widget/Field
 *
 * @example
 * let textField = new TextField({
 *   placeholder: 'Enter some text'
 * });
 *
 * @classType textfield
 * @externalexample widget/TextField.js
 */
export default class TextField extends Field {
    inputTemplate() {
        const
            me = this,
            style = 'inputWidth' in me ? `style="width:${DomHelper.setLength(me.inputWidth)}"` : '';

        return TemplateHelper.tpl`<input type="${me.inputType || 'text'}"
            reference="input"
            class="${me.inputCls || ''}"
            placeholder="${me.placeholder}" 
            autocomplete="${me.autoComplete}"
            name="${me.name || me.id}"
            id="${me.id + '_input'}"
            ${style}/>`;
    }

    set value(value) {
        super.value = (this.$name === 'TextField' && value == null) ? '' : value;
    }

    get value() {
        return super.value;
    }
}

BryntumWidgetAdapterRegister.register('textfield', TextField);
BryntumWidgetAdapterRegister.register('text', TextField);
