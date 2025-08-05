import WidgetHelper from '../../helper/WidgetHelper.js';
import BryntumWidgetAdapterRegister from './util/BryntumWidgetAdapterRegister.js';
import Mask from '../../widget/Mask.js';
import Menu from '../../widget/Menu.js';
import Popup from '../../widget/Popup.js';
import Toast from '../../widget/Toast.js';
import Tooltip from '../../widget/Tooltip.js';
import '../../widget/Button.js';
import '../../widget/ButtonGroup.js';
import '../../widget/Checkbox.js';
import '../../widget/Combo.js';
import '../../widget/Container.js';
import '../../widget/DateField.js';
import '../../widget/NumberField.js';
import '../../widget/Slider.js';
import '../../widget/TabPanel.js';
import '../../widget/TextAreaField.js';
import '../../widget/TextField.js';
import '../../widget/TimeField.js';
import Point from '../../helper/util/Point.js';
import ObjectHelper from '../../helper/ObjectHelper.js';

/**
 * @module Common/adapter/widget/BryntumWidgetAdapter
 */

/**
 * Adapter that allows {@link Common.helper.WidgetHelper} to use Bryntums widgets.
 * Not to be called directly.
 * @example
 * WidgetHelper.adapter = BryntumWidgetAdapter;
 */
export default class BryntumWidgetAdapter {
    // region Create, insert, append
    createWidget(config) {
        return BryntumWidgetAdapterRegister.createWidget(config.type, config);
    }

    getClass(type) {
        return BryntumWidgetAdapterRegister.getClass(type);
    }

    appendWidget(config) {
        return this.createWidget(config);
    }

    appendWidgets(configArray, config) {
        // Prototype chained objects may be passed, so use ObjectHelper.
        return configArray.map(item => this.appendWidget(ObjectHelper.assign({}, config || {}, item)));
    }

    //endregion

    //region Window & popup

    openPopup(element, config) {
        // Prototype chained objects may be passed, so use ObjectHelper.
        return new Popup(ObjectHelper.assign({
            forElement : element
        }, typeof config === 'string' ? {
            html : config
        } : config));
    }

    //endregion

    //region Menu

    showContextMenu(element, config) {
        const me = this;

        if (me.currentContextMenu) {
            me.currentContextMenu.destroy();
        }

        if (element instanceof HTMLElement) {
            config.forElement = element;
        }
        else if (Array.isArray(element)) {
            config.forElement = {
                target : new Point(...element)
            };
        }
        else if (element instanceof Point) {
            config.forElement = {
                target : element
            };
        }

        me.currentContextMenu = new Menu(config);

        me.currentContextMenu.on('destroy', () => {
            me.currentContextMenu = null;
        });

        return me.currentContextMenu;
    }

    //endregion

    //region Tooltip

    attachTooltip(element, configOrText) {
        if (typeof configOrText === 'string') configOrText = { html : configOrText };

        // TODO: refactor this
        // eslint-disable-next-line no-new
        new Tooltip(Object.assign({
            forElement : element
        }, configOrText));

        return element;
    }

    hasTooltipAttached(element) {
        return Tooltip.hasTooltipAttached(element);
    }

    destroyTooltipAttached(element) {
        return Tooltip.destroyTooltipAttached(element);
    }

    //endregion

    //region Mask

    mask(config, text = 'Loading') {
        if (config) {
            // Config object normalization
            if (config instanceof HTMLElement) {
                config = {
                    element : config,
                    text
                };
            }

            return Mask.mask(config, config.element);
        }
    }

    unmask(element, close = true) {
        if (element.mask) {
            if (close) {
                element.mask.close();
            }
            else {
                element.mask.hide();
            }
        }
    }

    //endregion

    //region Toast

    toast(msg) {
        return Toast.show(msg);
    }

    //endregion

    //region Style

    // moved from WidgetHelper to avoid circular reference

    set defaultStyle(style) {
        this._defaultStyle = style;
    }

    get defaultStyle() {
        return this._defaultStyle;
    }

    //endregion
}

if (!WidgetHelper._adapter) WidgetHelper.adapter = BryntumWidgetAdapter;
