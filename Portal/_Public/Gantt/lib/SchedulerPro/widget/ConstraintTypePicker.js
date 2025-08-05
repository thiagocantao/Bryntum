import Combo from '../../Core/widget/Combo.js';

/**
 * @module SchedulerPro/widget/ConstraintTypePicker
 */
const alapIds = {
    assoonaspossible : 1,
    aslateaspossible : 1
};

/**
 * Combo box preconfigured with possible constraint type values.
 * This picker doesn't support {@link Core/widget/Combo#config-multiSelect multiSelect}.
 *
 * This field can be used as an editor for a {@link Grid/column/Column}.
 * It is used as the default editor for the `ConstraintTypeColumn` in the Gantt chart.
 *
 * {@inlineexample SchedulerPro/widget/ConstraintTypePicker.js}
 * @extends Core/widget/Combo
 * @classType constrainttypepicker
 * @inputfield
 */
export default class ConstraintTypePicker extends Combo {



    //region Config

    static get $name() {
        return 'ConstraintTypePicker';
    }

    // Factoryable type name
    static get type() {
        return 'constrainttypepicker';
    }

    static get configurable() {
        return {
            valueField : 'id',

            store : {
                data : [{
                    id : 'none' 
                }, {
                    id : 'assoonaspossible' 
                }, {
                    id : 'aslateaspossible' 
                }, {
                    id : 'muststarton' 
                }, {
                    id : 'mustfinishon' 
                }, {
                    id : 'startnoearlierthan' 
                }, {
                    id : 'startnolaterthan' 
                }, {
                    id : 'finishnoearlierthan' 
                }, {
                    id : 'finishnolaterthan' 
                }]
            },

            primaryFilter(rec) {
                return ConstraintTypePicker.localize(rec.id).toLowerCase().startsWith(this.value.toLowerCase());
            },

            includeAsapAlapAsConstraints : true,

            nullValue : 'none'
        };
    }
    //endregion

    //region Internal

    // Gantt CellEdit sets this
    loadEvent(record) {
        this.taskRecord = record;
        this.store.filter();
    }

    set value(value) {
        super.value = value;
    }

    get value() {
        const value = super.value;

        return value === 'none' ? null : value;
    }

    get inputValue() {
        return this.L(this.selected?.id || this.nullValue);
    }

    listItemTpl(rec) {
        return ConstraintTypePicker.localize(rec.id);
    }

    changeStore(store) {
        return super.changeStore({
            ...store,

            // We filter our store to either show or not show the ASAP/ALAP constraints
            // depending on our includeAsapAlapAsConstraints setting
            filters : {
                filterBy : r =>  alapIds[r.id] ? this._includeAsapAlapAsConstraints : (this.taskRecord?.recurringEvent || this.taskRecord)?.run('isConstraintTypeApplicable', r.id)
            }
        });
    }

    updateIncludeAsapAlapAsConstraints() {
        this.store.filter();
    }
    //endregion
}

// Register this widget type with its Factory
ConstraintTypePicker.initClass();
