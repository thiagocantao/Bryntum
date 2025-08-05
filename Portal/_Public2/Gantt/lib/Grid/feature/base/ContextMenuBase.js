import InstancePlugin from '../../../Core/mixin/InstancePlugin.js';
import WidgetHelper from '../../../Core/helper/WidgetHelper.js';
import ObjectHelper from '../../../Core/helper/ObjectHelper.js';
import StringHelper from '../../../Core/helper/StringHelper.js';
import Rectangle from '../../../Core/helper/util/Rectangle.js';
import '../../../Core/widget/Menu.js';

/**
 * @module Grid/feature/base/ContextMenuBase
 */

/**
 * Abstract base class used by other context menu features.
 * @extends Core/mixin/InstancePlugin
 * @abstract
 */
export default class ContextMenuBase extends InstancePlugin {
    static get $name() {
        return 'ContextMenuBase';
    }

    //region Config

    static get defaultConfig() {
        return {
            /**
             * This is a type of the context menu used to generate correct names for methods and events.
             * Should be in camel case. Required to be set in subclass.
             * @config {String}
             * @readonly
             */
            type : null,

            /**
             * A config which will be applied for Menu component
             * @config {Object}
             */
            menuConfig : null,

            /**
             * {@link Core.widget.Menu Menu} items object containing named child menu items to apply to the feature's
             * provided context menu.
             *
             * This may add extra items as below, but may also remove any of the default items by configuring the name
             * of the item as `false`
             *
             * ```javascript
             * features : {
             *     cellMenu : {
             *         // This object is applied to the Feature's predefined default items
             *         items : {
             *             switchToDog : {
             *                 text : 'Dog',
             *                 icon : 'b-fa b-fa-fw b-fa-dog',
             *                 onItem({contextRecord}) {
             *                     contextRecord.dog = true;
             *                     contextRecord.cat = false;
             *                 },
             *                 weight : 500     // Make this second from end
             *             },
             *             switchToCat : {
             *                 text : 'Cat',
             *                 icon : 'b-fa b-fa-fw b-fa-cat',
             *                 onItem({contextRecord}) {
             *                     contextRecord.dog = false;
             *                     contextRecord.cat = true;
             *                 },
             *                 weight : 510     // Make this sink to end
             *             },
             *             add : false // We do not want the "Add" submenu to be available
             *         }
             *     }
             * }
             * ```
             *
             * @config {Object|Object[]}
             */
            items : null,

            /**
             * A function called before displaying the menu that allows manipulations of its items.
             * Called with a single parameter with format { contextRecord, eventElement, items }.
             * Returning `false` from this function prevents the menu from being shown.
             *
             * ```javascript
             * features : {
             *     cellMenu : {
             *         processItems({contextRecord, items}) {
             *             // Add or remove items here as needed
             *             if (contextRecord.type === 'Meeting') {
             *                 items.cancel = {
             *                     text   : 'Cancel',
             *                     icon   : 'b-fa b-fa-fw b-fa-ban',
             *                     weight : 200 // Move to end
             *                 };
             *             }
             *
             *             // Hide delete for parents
             *             items.deleteTask.hidden = contextRecord.isParent;
             *         }
             *     }
             * }
             * ```
             *
             * @config {Function}
             * @preventable
             */
            processItems : null,

            /**
             * Event which is used to show context menu.
             * Available options are: 'contextmenu', 'click', 'dblclick'.
             * Default value is used from {@link Grid/view/GridBase#config-contextMenuTriggerEvent}
             * @config {String}
             */
            triggerEvent : null
        };
    }

    // Plugin configuration. This plugin chains some of the functions in Grid.
    // The contextmenu event is emulated from a taphold gesture on touch platforms.
    static get pluginConfig() {
        return {
            assign : ['showContextMenu'],
            chain  : [
                'onElementContextMenu',
                'onElementClick',
                'onElementDblClick',
                'onElementKeyDown'
            ]
        };
    }

    //endregion

    //region Init

    construct() {
        super.construct(...arguments);

        if (!this.type || !this.type.length) {
            throw new Error(`Config 'type' is required to be specified for context menu`);
        }
    }

    doDestroy() {
        this.menu?.destroy();

        super.doDestroy();
    }

    //endregion

    //region Events

    /**
     * Fired from grid when an item is selected in the context menu.
     * @event contextMenuItem
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} item Selected menu item
     */

    /**
     * Fired from grid when a check item is toggled in the context menu.
     * @event contextMenuToggleItem
     * @param {Grid.view.Grid} source The grid
     * @param {Core.widget.Menu} menu The menu
     * @param {Object} item Selected menu item
     * @param {Boolean} checked Checked or not
     */

    onElementContextMenu(event) {
        this.triggerEvent === 'contextmenu' && this.internalShowContextMenu(event);
    }

    onElementClick(event) {
        this.triggerEvent === 'click' && this.internalShowContextMenu(event);
    }

    onElementDblClick(event) {
        this.triggerEvent === 'dblclick' && this.internalShowContextMenu(event);
    }

    onElementKeyDown(event) {
    }

    //endregion

    //region Menu handlers

    internalShowContextMenu(event) {
        const me = this;

        if (me.disabled) {
            return;
        }

        const data = me.getDataFromEvent(event);

        if (me.shouldShowMenu(data)) {
            me.showContextMenu(data);
        }
    }

    getDataFromEvent(event) {
        return {
            event,
            targetElement : this.getTargetElementFromEvent(event)
        };
    }

    getTargetElementFromEvent(event) {
        return event.target;
    }

    /**
     * Shows the context menu
     * @param {Object} eventParams
     * @param {MouseEvent} eventParams.event
     * @param {HTMLElement} eventParams.targetElement
     * @internal
     */
    showContextMenu(eventParams) {
        const me = this;

        if (me.disabled) {
            return;
        }

        const
            {
                type,
                client,
                processItems,
                items
            }                    = me,
            {
                event,
                targetElement
            }                    = eventParams,
            point                = event ? [event.clientX + 1, event.clientY + 1] : Rectangle.from(targetElement).center;

        ObjectHelper.assign(eventParams, {
            items     : {},
            selection : client.selectedRecords
        });

        // Call the chainable method which other features use to add their own menu items.
        me.callChainablePopulateMenuMethod(eventParams);

        // Filter out `true` values
        for (const key in items) {
            if (items[key] === true) {
                Reflect.deleteProperty(items, key);
            }
        }

        // Merge with user defined items
        ObjectHelper.merge(eventParams.items, items);

        // Allow user a chance at processing the items and preventing the menu from showing
        if ((!processItems || processItems(eventParams) !== false) && me.hasActiveMenuItems(eventParams)) {
            me.populateItemsWithData(eventParams);
            me.preventDefaultEvent(eventParams);

            // beforeContextMenuShow is a lifecycle method which may be implemented in subclasses to preprocess the event.
            if (me.beforeContextMenuShow(eventParams) !== false) {
                // Trigger event that allows preventing menu or manipulating its items
                if (client.trigger(`${type}MenuBeforeShow`, eventParams) !== false &&
                    // TODO: cellContextMenuBeforeShow and headerContextMenuBeforeShow events are deprecated in ContextMenu feature
                    client.trigger(`${type}ContextMenuBeforeShow`, eventParams) !== false
                ) {
                    me.menu = WidgetHelper.showContextMenu(point, ObjectHelper.merge({
                        items        : eventParams.items,
                        owner        : client,
                        scrollAction : 'hide',
                        constrainTo  : window,
                        // Load up the item event with the contextual info
                        onBeforeItem(itemEvent) {
                            Object.assign(itemEvent, eventParams);
                        },
                        onItem(itemEvent) {
                            client.trigger(`${type}MenuItem`, itemEvent);
                            // TODO: contextMenuItem event is deprecated in ContextMenu feature
                            client.trigger('contextMenuItem', itemEvent);
                            // TODO: eventContextMenuItem event and schedulerContextMenuItem event are deprecated
                            client.trigger(`${type}ContextMenuItem`, itemEvent);
                        },
                        onToggle(itemEvent) {
                            client.trigger(`${type}MenuToggleItem`, itemEvent);
                            // TODO: contextMenuToggleItem event is deprecated in ContextMenu feature
                            client.trigger('contextMenuToggleItem', itemEvent);
                        },
                        onDestroy() {
                            me.menu = null;
                        },
                        onShow({ source : menu }) {
                            eventParams.menu = menu;
                            client.trigger(`${type}MenuShow`, eventParams);
                            // TODO: cellContextMenuShow and headerContextMenuShow events are deprecated in ContextMenu feature
                            client.trigger(`${type}ContextMenuShow`, eventParams);
                        }
                    }, me.menuConfig));
                }
            }
        }
    }

    /**
     * Hides the context menu
     * @internal
     */
    hideContextMenu(animate) {
        this.menu?.hide(animate);
    }

    callChainablePopulateMenuMethod(eventParams) {
        // For example `populateCellMenu`
        const getItemsMethod = `populate${StringHelper.capitalize(this.type)}Menu`;

        if (this.client[getItemsMethod]) {
            this.client[getItemsMethod](eventParams);
        }
    }

    createContextMenuEventForElement(targetElement) {
        const
            targetPoint      = Rectangle.from(targetElement).center,
            contextmenuEvent = new MouseEvent(this.triggerEvent, {
                clientX : targetPoint.x,
                clientY : targetPoint.y
            });

        Object.defineProperty(contextmenuEvent, 'target', {
            get() {
                return targetElement;
            }
        });

        return contextmenuEvent;
    }

    hasActiveMenuItems(eventParams) {
        return Object.values(eventParams.items).some(item => item);
    }

    /**
     * Override this function and return `false` to prevent the context menu from being shown. Returns `true` by default.
     * @return {Boolean}
     * @internal
     */
    shouldShowMenu() {
        return true;
    }

    beforeContextMenuShow(eventParams) {}

    populateItemsWithData(eventParams) {}

    preventDefaultEvent(eventParams) {
        eventParams.event?.preventDefault();
    }

    //endregion

    //region Getters/Setters

    set triggerEvent(value) {
        this._triggerEvent = value;
    }

    get triggerEvent() {
        return this._triggerEvent || this.client.contextMenuTriggerEvent;
    }

    set type(value) {
        this._type = value;
    }

    get type() {
        return this._type;
    }

    set menuConfig(value) {
        this._menuConfig = value;
    }

    get menuConfig() {
        return this._menuConfig;
    }

    //endregion

}
