import './Tool.js';
import Container from './Container.js';
import ObjectHelper from '../helper/ObjectHelper.js';
import EventHelper from '../helper/EventHelper.js';
import DomHelper from '../helper/DomHelper.js';
import Toolbar from './Toolbar.js';

/**
 * @module Core/widget/Panel
 */

const
    acceptNode = e => !e.classList.contains('b-focus-trap') && DomHelper.isFocusable(e) ? NodeFilter.FILTER_ACCEPT : NodeFilter.FILTER_SKIP,
    byWeight = (l, r) => (l.weight || 0) - (r.weight || 0),
    byWeightReverse = (l, r) => (r.weight || 0) - (l.weight || 0);

// https://github.com/webcomponents/webcomponentsjs/issues/556
// Work around Internet Explorer wanting a function instead of an object.
// IE also *requires* this argument where other browsers don't.
acceptNode.acceptNode = acceptNode;

/**
 * Panel widget. A general pupose container which may be used to contain child {@link Core.widget.Container#config-items}
 * or {@link Core.widget.Widget#config-html}.
 *
 * Also may dock a {@link #config-header} and {@link #config-footer} either at top/bottom or left/right
 *
 *
 * @example
 * let panel = new Panel({
 *   title   : 'A Test Panel',
 *   items : [
 *     { type : 'text', placeholder: 'Text' },
 *   ],
 *   bbar : [{
 *     text : 'Proceed',
 *     onClick : () => {
 *       alert('Proceeding!');
 *     }
 *   }]
 * });
 *
 * @classType panel
 *
 * @extends Core/widget/Container
 * @externalexample widget/Panel.js
 */
export default class Panel extends Container {
    //region Config
    static get $name() {
        return 'Panel';
    }

    // Factoryable type name
    static get type() {
        return 'panel';
    }

    static get configurable() {
        return {
            localizableProperties : ['title'],

            /**
             * By default, tabbing within a Panel is not contained, ie you can TAB out of the Panel
             * forwards or backwards.
             * Configure this as `true` to disallow tabbing out of the Panel, and make tabbing circular within this Panel.
             * @config {Boolean}
             * @default false
             */
            trapFocus : null,

            /**
             * A title to display in the header. Causes creation and docking of a header
             * to the top if no header is configured.
             *
             * If specified, overrides any title configured within the {@link #config-header} configuration.
             * @default
             * @config {String}
             */
            title : null,

            /**
             * Config object of a header. May contain a `title`, `titleAlign`, `dock`
             * and a `cls` property. If passed as a string, the value is used
             * as the title, dock is defaulted to `'top', and titleAlign` is defaulted to `start`.
             *
             * The `dock` property may be `top`, `right`, `bottom` or `left`.
             * The `titleAlign` property may be `start`, `center` or `end`.
             * @default
             * @config {Object|String}
             */
            header : null,

            /**
             * Config object of a footer. May contain a `dock`
             * and a `cls` property.
             *
             * The `dock` property may be `top`, `right`, `bottom` or `left`.
             * @default
             * @config {Object|String}
             */
            footer : null,

            /**
             * The tools to add either before or after the title in the Panel header.
             * Each property name is the reference by which an instantiated Tool Widget
             * may be retrieved from the live `{@link #property-tools}` property.
             * Each tool may have the following properties:
             * * `cls` The CSS class to apply.
             * * `handler` A method in the field to call upon click
             * * `align` `'start'` or `'end'` which end of the header the tool should go.
             * * `weight` (Optional) Heigher weighted tools gravitate towards the centre.
             * @config {Object}
             */
            tools : {
                $config : {
                    nullify : true
                },
                value : null
            },

            /**
             * A Config object representing the configuration of a {@link Core.widget.Toolbar},
             * or array of config objects representing the child items of a Toolbar.
             *
             * This creates a toolbar docked to the top of the panel immediately below the header.
             * @config {Object[]|Object}
             */
            tbar : {
                $config : {
                    nullify : true,
                    merge   : 'objects'
                },
                value : null
            },

            /**
             * A Config object representing the configuration of a {@link Core.widget.Toolbar},
             * or array of config objects representing the child items of a Toolbar.
             *
             * This creates a toolbar docked to the bottom of the panel immediately above the footer.
             * @config {Object[]|Object}
             */
            bbar : {
                $config : {
                    nullify : true,
                    merge   : 'objects'
                },
                value : null
            }
        };
    }

    //endregion

    /**
     * The tool Widgets as specified by the {@link #config-tools} configuration
     * (and the {@link Core.widget.Popup#config-closable} configuration in the Popup subclass).
     * Each is a {@link Core.widget.Widget Widget} instance which may be hidden, shown and observed and styled just like any other widget.
     * @member {Object} tools
     */

    /**
     * Get toolbar {@link Core.widget.Toolbar} docked to the top of the panel,
     * @member {Core.widget.Toolbar} tbar
     * @readonly
     * @typings Toolbar
     */

    /**
     * Get toolbar {@link Core.widget.Toolbar} docked to the bottom of the panel,
     * @member {Core.widget.Toolbar} bbar
     * @readonly
     * @typings Toolbar
     */

    /**
     * A header {@link #config-tools tool} has been clicked.
     * @event toolclick
     * @param {Core.widget.Tool} source - This Panel.
     * @param {Core.widget.Tool} tool - The tool which is being clicked.
     */

    //region Init & destroy

    set element(element) {
        const me = this,
            headerFromTitle = me.title && (!me.parent || !me.parent.suppressChildHeaders),
            {
                tools,
                footer,
                tbar
            }           = me,
            // If tools exist, we need to create the header infrastructure
            header      = me.header || (tools || headerFromTitle ? {} : null),
            bbar = me.bbar || me.buttons,
            children = [{
                reference : 'topFocusTrap',
                className : 'b-focus-trap',
                tabIndex  : 0
            }],
            startTools = [],
            endTools = [],
            result = {
                children
            };

        if ((me.hasItems && me.focusable !== false) || me.focusable) {
            result.tabIndex = 0;
        }

        if (header) {
            const
                title       = (typeof header === 'string' ? header : me.title || header.title),
                titleAlign  = (header.titleAlign || 'start'),
                headerClass = me.classHierarchy(Panel).reduce((prev, cls) => {
                    prev[`b-${cls.$$name.toLowerCase()}-header`] = 1;
                    return prev;
                }, typeof header.cls === 'string' ? {
                    [header.cls] : 1
                } : header.cls || {}),
                headerChildren = [{
                    reference : 'titleElement',
                    className : `b-header-title b-align-${titleAlign}`,
                    html      : title
                }];

            for (const toolRef in tools) {
                const tool = tools[toolRef];
                if (tool.align === 'start') {
                    startTools.unshift(tool);
                }
                else {
                    endTools.push(tool);
                }
            }

            // The tools at each end are sorted "gravitationally".
            // Higher weight sorts towards the center which is the title element.
            startTools.sort(byWeight);
            endTools.sort(byWeightReverse);
            headerChildren.unshift(...startTools.map(t => t.element));
            headerChildren.push(...endTools.map(t => t.element));

            // Hide the header if no title and all tools are hidden
            headerClass['b-hide-display'] = !title && (!tools || !Object.values(tools).some(t => !t.hidden));
            headerClass[`b-dock-${header.dock || 'top'}`] = 1;
            children.push({
                tag       : 'header',
                reference : 'headerElement',
                className : headerClass,
                children  : headerChildren
            });
        }

        if (tbar) {
            tbar.layout.renderChildren();
            children.push(tbar.element);
        }

        children.push(me.bodyConfig);

        if (bbar) {
            bbar.layout.renderChildren();
            children.push(bbar.element);
        }

        if (footer) {
            children.push({
                tag       : 'footer',
                reference : 'footerElement',
                className : `b-dock-${footer.dock || 'bottom'} ${footer.cls || ''}`
            });
        }

        children.push({
            reference : 'bottomFocusTrap',
            className : 'b-focus-trap',
            tabIndex  : 0
        });

        super.element = result;
    }

    set bodyConfig(bodyConfig) {
        this._bodyConfig = bodyConfig;
    }

    get bodyConfig() {
        return ObjectHelper.merge({}, this._bodyConfig, {
            reference : 'bodyElement',
            className : this.classHierarchy(Panel).reduce((prev, cls) => {
                prev[`b-${cls.$$name.toLowerCase()}-content`] = 1;
                return prev;
            }, {}),
            html : this.html
        });
    }

    get element() {
        return super.element;
    }

    changeTbar(tbar, oldTbar) {
        if (tbar) {
            if (Array.isArray(tbar)) {
                tbar = {
                    items : tbar
                };
            }

            if (tbar.isWidget) {
                tbar.parent = this;
            }
            else {
                tbar = Toolbar.reconfigure(oldTbar, Toolbar.mergeConfigs({
                    type   : 'toolbar',
                    parent : this
                }, tbar));
            }

            // Allow Panel subclasses with tbar configured into them to have tbar configured away
            if (tbar) {
                this.hasItems = this.hasItems || tbar.hasItems;
                DomHelper.addClasses(tbar.element, ['b-top-toolbar', `b-dock-${tbar.dock || 'top'}`]);
            }
        }
        else if (oldTbar) {
            oldTbar.destroy();
        }

        return tbar;
    }

    changeBbar(bbar, oldTbar) {
        if (bbar) {
            if (Array.isArray(bbar)) {
                bbar = {
                    items : bbar
                };
            }

            if (bbar.isWidget) {
                bbar.parent = this;
            }
            else {
                bbar = Toolbar.reconfigure(oldTbar, Toolbar.mergeConfigs({
                    type   : 'toolbar',
                    parent : this
                }, bbar));
            }

            // Allow Panel subclasses with bbar configured into them to have bbar configured away
            if (bbar) {
                this.hasItems = this.hasItems || bbar.hasItems;
                DomHelper.addClasses(bbar.element, ['b-bottom-toolbar', `b-dock-${bbar.dock || 'bottom'}`]);
            }
        }
        else if (oldTbar) {
            oldTbar.destroy();
        }

        return bbar;
    }

    updateTitle(title) {
        const { tools } = this;

        if (this.headerElement) {
            this.titleElement.innerHTML = title;

            this.headerElement.classList[!title && (!tools || !Object.values(tools).some(t => !t.hidden)) ? 'add' : 'remove']('b-hide-display');
        }
    }

    // Override to iterate docked Toolbars in the correct order around contained widgets.
    get childItems() {
        const
            me = this,
            items = me.items ? me.items.slice() : [];

        if (me.tools) {
            items.unshift(...Object.values(me.tools));
        }
        if (me.tbar) {
            items.unshift(me.tbar);
        }
        if (me.bbar) {
            items.push(me.bbar);
        }

        return items;
    }

    changeTools(tools, oldTools) {
        const
            me      = this,
            myTools = {};

        if (tools) {
            for (const toolRef in tools) {
                // Allow excluding tools by configuring them as `false`
                if (tools[toolRef]) {
                    const tool = myTools[toolRef] = Panel.create(ObjectHelper.assign({
                        type   : 'tool',
                        ref    : toolRef,
                        parent : me
                    }, tools[toolRef]), me.defaultToolType || 'tool');

                    me.onChildAdd(tool);
                }
            }
        }
        else if (oldTools) {
            for (const t of Object.values(oldTools)) {
                t.destroy();
            }
        }
        return myTools;
    }

    updateTrapFocus(trapFocus) {
        const me = this;

        me.element.classList[trapFocus ? 'add' : 'remove']('b-focus-trapped');
        if (trapFocus) {
            me.focusTrapListener = EventHelper.on({
                element  : me.element,
                focusin  : 'onFocusTrapped',
                delegate : '.b-focus-trap',
                thisObj  : me
            });

            // Create a TreeWalker which visits focusable elements.
            if (!me.treeWalker) {
                me.treeWalker = this.setupTreeWalker(me.element, NodeFilter.SHOW_ELEMENT, acceptNode, false);
            }
        }
        else {
            if (me.focusTrapListener) {
                me.focusTrapListener();
                me.focusTrapListener = null;
            }
        }
    }

    setupTreeWalker(root, whatToShow, filter, entityReferenceExpansion) {
        return document.createTreeWalker(root, whatToShow, filter, entityReferenceExpansion);
    }

    onFocusTrapped(e) {
        const me = this,
            treeWalker = me.treeWalker;

        // The only way of focusing these invisible elements is by TABbing to them.
        // If we hit the bottom one, wrap to the top.
        if (e.target === me.bottomFocusTrap) {
            treeWalker.currentNode = me.topFocusTrap;
            treeWalker.nextNode();
        }
        // If we hit the top one, wrap to the bottom.
        else {
            treeWalker.currentNode = me.bottomFocusTrap;
            treeWalker.previousNode();
        }

        me.requestAnimationFrame(() => treeWalker.currentNode.focus());
    }

    get focusElement() {
        // Either use our Containerness to yield the focus element of
        // a descendant or fall back to the encapsulating element.
        return this.hasItems && (super.focusElement || this.element);
    }

    get contentElement() {
        return this.element && this.bodyElement;
    }

    get widgetClassList() {
        const me = this,
            result = super.widgetClassList,
            header = me.header;

        if (header || me.title || me.tools) {
            result.push(`b-panel-has-header b-header-dock-${header && header.dock || 'top'}`);
        }

        if (me.tbar) {
            result.push(`b-panel-has-top-toolbar`);
        }

        if (me.bbar) {
            result.push(`b-panel-has-bottom-toolbar`);
        }

        return result;
    }

    //endregion
}

// Register this widget type with its Factory
Panel.initClass();
