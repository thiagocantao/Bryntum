/**
 * @module Core/data/stm/action/InsertChildAction
 */
import ActionBase from './ActionBase.js';

const
    PARENT_MODEL_PROP = Symbol('PARENT_MODEL_PROP'),
    CHILD_MODELS_PROP = Symbol('CHILD_MODELS_PROP'),
    INSERT_INDEX_PROP = Symbol('INSERT_INDEX_PROP'),
    CONTEXT_PROP      = Symbol('CONTEXT_PROP');

/**
 * Action to record the fact of adding a children models into a parent model.
 * @extends Core/data/stm/action/ActionBase
 */
export default class InsertChildAction extends ActionBase {

    static get defaultConfig() {
        return {
            /**
             * Reference to a parent model a child model has been added to.
             *
             * @config {Core.data.Model}
             * @default
             */
            parentModel : undefined,

            /**
             * Children models inserted.
             *
             * @config {Core.data.Model[]}
             * @default
             */
            childModels : undefined,

            /**
             * Index a children models are inserted at
             *
             * @config {Number}
             * @default
             */
            insertIndex : undefined,

            /**
             * Map having children models as keys and values containing previous parent
             * of each model and index at the previous parent.
             *
             * @config {Object}
             * @default
             */
            context : undefined
        };
    }

    get type() {
        return 'InsertChildAction';
    }



    get parentModel() {
        return this[PARENT_MODEL_PROP];
    }

    set parentModel(model) {


        this[PARENT_MODEL_PROP] = model;
    }

    get childModels() {
        return this[CHILD_MODELS_PROP];
    }

    set childModels(models) {


        this[CHILD_MODELS_PROP] = models.slice(0);
    }

    get insertIndex() {
        return this[INSERT_INDEX_PROP];
    }

    set insertIndex(index) {


        this[INSERT_INDEX_PROP] = index;
    }

    get context() {
        return this[CONTEXT_PROP];
    }

    set context(ctx) {


        this[CONTEXT_PROP] = ctx;
    }

    undo() {
        const
            { parentModel, context, childModels } = this,
            byFromParent = new Map(),
            newlyAdded = new Set();

        for (const childModel of childModels) {
            const ctx = context.get(childModel);

            if (!ctx) {
                newlyAdded.add(childModel);
            }
            else {
                let undoTaskData = byFromParent.get(ctx.parent);

                if (!undoTaskData) {
                    undoTaskData = { moveRight : [], moveLeft : [], moveFromAnotherParent : [] };
                    byFromParent.set(ctx.parent, undoTaskData);
                }

                if (ctx.parent === parentModel) {
                    if (ctx.index > childModel.parentIndex) {
                        undoTaskData.moveRight.push({ parent : ctx.parent, model : childModel, index : ctx.index + 1 });
                    }
                    else {
                        undoTaskData.moveLeft.push({ parent : ctx.parent, model : childModel, index : ctx.index });
                    }
                }
                else {
                    undoTaskData.moveFromAnotherParent.push({ parent : ctx.parent, model : childModel, index : ctx.index });
                }
            }
        }

        for (const undoTaskData of byFromParent.values()) {
            const { moveRight, moveLeft } = undoTaskData;

            moveLeft.sort((a, b) => a.index - b.index);
            moveRight.sort((a, b) => b.index - a.index);
        }

        newlyAdded.forEach(model => model.parent.removeChild(model));

        for (const undoTaskData of byFromParent.values()) {
            const { moveRight, moveLeft, moveFromAnotherParent } = undoTaskData;

            moveLeft.forEach(task => {
                task.parent.insertChild(task.model, task.index);
            });
            moveRight.forEach(task => {
                task.parent.insertChild(task.model, task.index);
            });
            moveFromAnotherParent.forEach(task => {
                task.parent.insertChild(task.model, task.index);
            });
        }
    }

    redo() {
        const
            { parentModel, insertIndex, childModels } = this,
            insertBefore = parentModel.children[insertIndex];

        parentModel.insertChild(childModels, insertBefore, false, {
            orderedBeforeNode : insertBefore?.previousSibling?.nextOrderedSibling
        });
    }
}
