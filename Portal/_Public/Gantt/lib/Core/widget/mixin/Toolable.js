import Widget from '../Widget.js';
import ObjectHelper from '../../helper/ObjectHelper.js';
import FunctionHelper from '../../helper/FunctionHelper.js';
import DynamicObject from '../../util/DynamicObject.js';

import '../Tool.js';

/**
 * @module Core/widget/mixin/Toolable
 */

const
    emptyArray = [],
    emptyObject = {},
    toolConfigs = {
        align  : 1,
        // hidden : 1,
        weight : 1
    };

/**
 * A mixin that manages {@link #config-tools}.
 *
 * @mixin
 * @mixinbase Widget
 */
export default Target => class Toolable extends (Target || Widget) {
    static get $name() {
        return 'Toolable';
    }

    static get configurable() {
        return {
            /**
             * The {@link Core.widget.Tool tools} as specified by the {@link #config-tools} configuration. Each is a
             * {@link Core.widget.Tool} instance which may be hidden, shown and observed and styled just like any other
             * widget.
             * @member {Object<String,Core.widget.Tool>} tools
             * @category Content
             */
            /**
             * The {@link Core.widget.Tool tools} to add either before or after the `title` in the Panel header. Each
             * property name is the reference by which an instantiated tool may be retrieved from the live
             * `{@link #property-tools}` property.
             * @config {Object<String,ToolConfig>}
             * @category Content
             */
            tools : {
                value   : null,
                $config : {
                    nullify : true
                }
            },

            /**
             * An object containing config defaults for corresponding {@link #config-tools} objects with a matching name.
             *
             * This object contains a key named `'*'` with default config properties to apply to all tools. This
             * object provides the default `type` (`'tool').
             * @config {Object} toolDefaults
             * @private
             */
            toolDefaults : {
                '*' : {
                    type  : 'tool',
                    align : 'end'
                }
            }
        };
    }

    byWeightSortFn(a, b) {
        return (a.weight || 0) - (b.weight || 0);
    }

    byWeightReverseSortFn(a, b) {
        return (b.weight || 0) - (a.weight || 0);
    }

    gatherTools({ align, alt, refs } = emptyObject) {
        const
            { collapsed, tools } = this,
            options = { collapsed, alt };

        let ret = [],
            alignment, key, i, item, tool;

        for (key in tools) {
            tool = tools[key];

            // Tools redefine "align" config to be a simple string, but other widgets promote align config to an align
            // spec object
            alignment = tool?.align?.align ?? tool?.align ?? 'end';

            if (alignment === align && tool.isCollapsified(options)) {
                ret.push(tool);
            }
        }

        ret.sort(this[(align === 'end') ? 'byWeightReverseSortFn' : 'byWeightSortFn']);

        if (refs) {
            const
                asWidget = refs === 'widget',
                asRefs = {};

            for (i = 0; i < ret.length; ++i) {
                item = ret[i];

                asRefs[item.ref] = asWidget ? item : item.element;
            }

            ret = asRefs;
        }

        return ret;
    }

    getEndTools({ alt, refs } = emptyObject) {
        return this.gatherTools({ align : 'end', alt, refs });
    }

    getStartTools({ alt, refs } = emptyObject) {
        return this.gatherTools({ align : 'start', alt, refs });
    }

    get childItems() {
        return [
            ...this.getStartTools(),
            ...(this._items || emptyArray),
            ...this.getEndTools()
        ];
    }

    changeTools(tools, oldTools) {
        const
            me      = this,
            manager = me.$tools || (me.$tools = new DynamicObject({
                configName : 'tools',
                factory    : Widget,
                inferType  : false,  // the name of a tool in the tools object is not its type
                owner      : me,

                created(instance) {
                    instance.innerItem = false;
                    instance.syncRotationToDock?.(me.header?.dock);

                    FunctionHelper.after(instance, 'onConfigChange', (ret, { name }) => {
                        if (toolConfigs[name]) {
                            me.onConfigChange({
                                name  : 'tools',
                                value : manager.target
                            });
                        }
                    });

                    me.onChildAdd(instance);
                },

                setup(config, name) {
                    config = ObjectHelper.merge({}, me.toolDefaults['*'], me.toolDefaults[name], config);

                    config.parent = me;  // so parent can be accessed during construction
                    config.ref    = name;

                    return config;
                }
            }));

        manager.update(tools);

        if (!oldTools) {
            // Only return the target once. Further calls are processed above so we need to return undefined to ensure
            // onConfigChange is called. By returning the same target on 2nd+ call, it passes the === test and won't
            // trigger onConfigChange.
            return manager.target;
        }
    }

    get widgetClass() {}
};
