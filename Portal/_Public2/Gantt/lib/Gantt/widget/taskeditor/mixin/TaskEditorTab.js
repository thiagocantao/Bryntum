/**
 * @module Gantt/widget/taskeditor/mixin/TaskEditorTab
 */

/**
 * Mixin class for task editor tabs which processes common tab configs. Like `extraWidgets`
 *
 * @mixin
 */

export default Target => class extends Target {
    startConfigure(config) {
        if (config.extraItems && config.items) {
            config.items.push(...config.extraItems);
            config.items.sort((widgetA, widgetB) => (widgetA.index | 0) - (widgetB.index - 0));
        }
        super.afterConfigure();
    }
};
