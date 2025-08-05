import Combo from '../../Core/widget/Combo.js';

/**
 * @module SchedulerPro/widget/SchedulingDirectionPicker
 */

/**
 * Combo box preconfigured with possible [scheduling direction](https://bryntum.com/products/gantt/docs/api/Gantt/model/TaskModel#field-direction) values.
 * This picker doesn't support {@link Core/widget/Combo#config-multiSelect}.
 *
 * This field can be used as an editor for a {@link Grid/column/Column}.
 *
 * @extends Core/widget/Combo
 * @classType schedulingdirectionpicker
 * @inputfield
 */
export default class SchedulingDirectionPicker extends Combo {



    //region Config

    static get $name() {
        return 'SchedulingDirectionPicker';
    }

    // Factoryable type name
    static get type() {
        return 'schedulingdirectionpicker';
    }

    static get configurable() {
        return {
            store : {
                data : [{
                    id : 'Forward' 
                }, {
                    id : 'Backward' 
                }]
            },

            primaryFilter(rec) {
                return SchedulingDirectionPicker.localize(rec.id).toLowerCase().startsWith(this.value.toLowerCase());
            },

            hasEnforcedBadge : false,

            clearable : false,

            nullValue : 'Forward'
        };
    }

    //endregion

    get inputValue() {
        return this.L(this.value || '');
    }

    listItemTpl(rec) {
        return SchedulingDirectionPicker.localize(rec.id);
    }

    changeHasEnforcedBadge(hasEnforcedBadge) {
        this.element.classList.toggle('b-enforced-sch-direction', Boolean(hasEnforcedBadge));
    }

    getEnforcedName(task) {
        return task.name || (task.isRoot ? 'Project' : `Task #${ task.id }`);
    }

    assignFieldValue(record) {
        const
            me                 = this,
            effectiveDirection = record?.effectiveDirection;

        me.taskRecord = record;

        if (effectiveDirection) {
            me.value = effectiveDirection.direction;

            if (effectiveDirection.kind === 'enforced') {
                me.hasEnforcedBadge = true;
                me.tooltip = me.L('L{enforcedBy}') + ` "${ this.getEnforcedName(effectiveDirection.enforcedBy) }"`;
                me.disable();
            }
            else if (effectiveDirection.kind === 'inherited') {
                me.hasEnforcedBadge = true;
                me.tooltip = me.L('L{inheritedFrom}') + ` "${ this.getEnforcedName(effectiveDirection.inheritedFrom) }"`;
                me.enable();
            }
            else {
                me.hasEnforcedBadge = false;
                me.tooltip = undefined;
                me.enable();
            }
        }
        else {
            me.clear();
            me.disable();
        }
    }
}

// Register this widget type with its Factory
SchedulingDirectionPicker.initClass();
