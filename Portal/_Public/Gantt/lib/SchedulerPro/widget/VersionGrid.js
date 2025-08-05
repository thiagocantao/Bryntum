import StringHelper from '../../Core/helper/StringHelper.js';
import DateHelper from '../../Core/helper/DateHelper.js';
import GridRowModel from '../../Grid/data/GridRowModel.js';
import TreeGrid from '../../Grid/view/TreeGrid.js';
import ArrayHelper from '../../Core/helper/ArrayHelper.js';
import ObjectHelper from '../../Core/helper/ObjectHelper.js';

/**
 * @module SchedulerPro/widget/VersionGrid
 */

const
    EMPTY_ARRAY = [],
    actionTypeOrder = { remove : 1, add : 2, update : 3 },
    entityTypeOrder = { TaskModel : 1, DependencyModel : 2, AssignmentModel : 3, ProjectModel : 4 },
    // For moves, describe the former and current locations
    describePosition = ({ parent, index }) => `${parent.name}[${index}]`,
    knownEntityTypes = {
        AssignmentModel : 'Assignment',
        DependencyModel : 'Dependency'
    };

class VersionGridRow extends GridRowModel {
    static fields = [
        {
            name : 'description',
            type : 'string'
        }, {
            name : 'occurredAt',
            type : 'date'
        }, {
            name : 'versionModel'
        }, {
            name : 'transactionModel'
        }, {
            name : 'propertyUpdate'
        }, {
            name : 'action'
        }
    ];
}

/**
 * Displays a list of versions and the transactions they contain. For use with the {@link SchedulerPro.feature.Versions}
 * feature.
 *
 * Configure the VersionGrid with a {@link SchedulerPro.model.ProjectModel} using the {@link #config-project} config.
 *
 * @extends Grid/view/TreeGrid
 * @classType versiongrid
 * @widget
 */
export default class VersionGrid extends TreeGrid {
    static $name = 'VersionGrid';

    static type = 'versiongrid';

    static configurable = {

        store : {
            tree       : true,
            modelClass : VersionGridRow,
            sorters    : [
                {
                    field     : 'occurredAt',
                    ascending : false
                },
                VersionGrid.sortActionRows
            ],
            reapplySortersOnAdd : true
        },

        /**
         * The {@link SchedulerPro.model.ProjectModel} whose versions and changes are being observed in this grid.
         * @config {SchedulerPro.model.ProjectModel}
         */
        project : null,

        /**
         * Whether to display transactions not yet associated with a version.
         * @prp {Boolean}
         */
        showUnattachedTransactions : true,

        /**
         * Whether to show only versions that have been assigned a specific name.
         * @prp {Boolean}
         */
        showNamedVersionsOnly : false,

        /**
         * Whether to include version rows in the display.
         * @prp {Boolean}
         */
        showVersions : true,

        /**
         * The id of the version currently being compared, if any.
         * @prp {Boolean}
         */
        comparingVersionId : null,

        flex : 0,

        features : {
            group : {
                field : 'id'
            },

            cellEdit : {
                continueEditingOnCellClick : false,
                editNextOnEnterPress       : false
            },

            cellMenu : {
                items : {
                    removeRow    : false,
                    cut          : false,
                    copy         : false,
                    paste        : false,
                    renameButton : {
                        text   : 'L{VersionGrid.rename}',
                        icon   : 'b-icon b-icon-edit',
                        onItem : ({ record, source : grid }) => {
                            grid.startEditing({
                                id     : record.id,
                                column : grid.columns.get('description')
                            });
                        }
                    },
                    restoreButton : {
                        text   : 'L{VersionGrid.restore}',
                        icon   : 'b-icon b-icon-undo',
                        onItem : ({ record, source : grid }) => {
                            grid.triggerRestore(record.versionModel);
                        }
                    },
                    compareButton : {
                        text   : 'L{VersionGrid.compare}',
                        icon   : 'b-icon b-icon-compare',
                        onItem : ({ record, source : grid }) => {
                            grid.triggerCompare(record.versionModel);
                        }
                    },
                    stopComparingButton : {
                        text   : 'L{VersionGrid.stopComparing}',
                        onItem : ({ record, source : grid }) => {
                            grid.triggerStopCompare();
                        }
                    }
                }
            },

            rowCopyPaste : false
        },

        columns : [
            { type : 'tree', text : 'L{VersionGrid.description}', field : 'description', flex : 4, groupable : false, renderer : ({ grid, ...rest }) => grid.renderDescription({ grid, ...rest }), autoHeight : true },
            { text : 'L{VersionGrid.occurredAt}', field : 'occurredAt', type : 'date', flex : 1, groupable : false }
        ],

        /**
         * The date format used for displaying date values in change actions.
         * @config {String}
         */
        dateFormat : 'M/D/YY h:mm a',

        internalListeners : {
            beforeCellEditStart({ editorContext : { column, record } }) {
                // Only version descriptions are editable
                if (!(column.field === 'description' && record.versionModel)) {
                    return false;
                }
            },

            finishCellEdit({ editorContext : { record, value } }) {
                record.versionModel.name = (value != null && value.trim()) ? value : null;
            },

            cellMenuBeforeShow({ source, record, items }) {
                items.stopComparingButton.disabled = !source.comparingVersionId;
                return Boolean(record.versionModel);
            },

            toggleNode({ record, collapse }) {
                this._expandedById.set(record.id, !collapse);
            }
        }
    };

    static delayable = {
        processUpdates : {
            type              : 'raf',
            cancelOutstanding : true
        }
    };

    // Bookkeeping fields
    static get properties() {
        return {
            _rowsByUnderlyingRecord : new WeakMap(),
            _expandedById           : new Map()
        };
    };

    _transactionChanges = [];
    _versionChanges = [];
    comparingRowCls = `b-${VersionGrid.type}-comparing`;

    construct(config) {
        super.construct({
            ...config,
            features : ObjectHelper.merge({}, VersionGrid.configurable.features, config.features)
        });
    }

    afterConstruct() {
        if (!this.project) {
            throw new Error(`${VersionGrid.$name} requires the project config.`);
        }
        this.refreshGrid();
    }

    updateDateFormat(newDateFormat) {
        const occurredAtColumn = this.columns.get('occurredAt');
        if (occurredAtColumn) {
            occurredAtColumn.format = newDateFormat;
        }
    }

    updateProject(newProject) {
        const me = this;

        me.detachListeners('storeChange');

        me._versionStore = newProject.getCrudStore('versions');
        me._transactionStore = newProject.getCrudStore('changelogs');

        me._versionStore.ion({
            name    : 'storeChange',
            change  : me.onVersionStoreChange,
            thisObj : me
        });
        me._transactionStore.ion({
            name    : 'storeChange',
            change  : me.onTransactionStoreChange,
            thisObj : me
        });
    }

    updateShowNamedVersionsOnly() {
        if (this.isPainted) {
            this.refreshGrid();
        }
    }

    updateShowUnattachedTransactions() {
        if (this.isPainted) {
            this.refreshGrid();
        }
    }

    updateShowVersions() {
        if (this.isPainted) {
            this.refreshGrid();
        }
    }

    updateComparingVersionId(newVersionId, oldVersionId) {
        const [oldHighlightedRow, newHighlightedRow] = [oldVersionId, newVersionId].map(versionId =>
            this.store.getById(`v-${versionId}`));
        if (oldHighlightedRow) {
            oldHighlightedRow.cls = '';
            oldHighlightedRow.iconCls = 'b-icon b-icon-version';
        }
        if (newHighlightedRow) {
            newHighlightedRow.cls = this.comparingRowCls;
            newHighlightedRow.iconCls = 'b-icon b-icon-compare';
        }
    }

    onVersionStoreChange({ action, records }) {
        this._versionChanges.push({ action, records });
        this.processUpdates();
    }

    onTransactionStoreChange({ action, records }) {
        this._transactionChanges.push({ action, records });
        this.processUpdates();
    }

    /**
     * This is an optimization to more efficiently replace grid rows when the underlying stores change.
     * We wait a tick, then replace the set of rows corresponding to the modified records with the new
     * projected rowset.
     *
     * The code below does not handle record remove, or updating transactions without their version in the
     * same tick. (Versions can be updated without their transactions, as when renamed.)
     * @private
     */
    processUpdates() {
        const
            me = this,
            versions = ArrayHelper.unique(me._versionChanges.flatMap(({ records }) => records)),

            versionIds = new Set(versions.map(version => String(version.id))),
            transactions = ArrayHelper.unique(
                me._transactionChanges.flatMap(({ records }) => records)
                    // Expand to all transactions for incoming versions
                    .concat(versions.length === 0 ? []
                        : me._transactionStore.query(txn => versionIds.has(txn.versionId))));
        // Expand to all versions for incoming transaction
        for (const transaction of transactions) {
            if (transaction.versionId && !versionIds.has(transaction.versionId)) {
                versions.push(me._versionStore.getById(transaction.versionId));
                versionIds.add(transaction.versionId);
            }
        }
        me.replaceRows(ArrayHelper.unique(versions), transactions);
        me._transactionChanges = [];
        me._versionChanges = [];
    }

    replaceRows(versions, transactions) {
        const
            me = this,
            { showNamedVersionsOnly, showUnattachedTransactions, store } = me,
            rowsToReplaceSet = new Set(),
            transactionsByVersionId = ArrayHelper.groupBy(transactions, 'versionId'),
            allRecords = transactions.concat(versions),
            versionsToShow = showNamedVersionsOnly
                ? versions.filter(version => version.name != null)
                : versions;
        for (const record of allRecords) {
            for (const row of me._rowsByUnderlyingRecord.get(record) ?? EMPTY_ARRAY) {
                rowsToReplaceSet.add(row);
            }
        }

        me.suspendRefresh();

        store.remove(Array.from(rowsToReplaceSet));
        for (const version of versionsToShow) {
            const newRows = store.add(me.getGridRows(version, transactionsByVersionId[version.id]));
            me._rowsByUnderlyingRecord.set(version, newRows);
        }
        if (showUnattachedTransactions) {
            for (const transaction of transactions.filter(txn => txn.versionId == null)) {
                const newRows = store.add(me.getGridRows(null, [transaction]));
                me._rowsByUnderlyingRecord.set(transaction, newRows);
            }
        }

        me.resumeRefresh();

        store.sort(store.sorters);
    }

    /**
     * Does a full replace of all rows in the grid using all records currently in the two stores.
     * @private
     */
    refreshGrid() {
        this.replaceRows(this._versionStore.records, this._transactionStore.records);
    }

    /**
     * Transform a set of transactions (and optional parent version) into tree structure needed by grid
     * @private
     */
    getGridRows(version, transactions) {
        const
            me = this,
            { showVersions, comparingVersionId } = me,
            transactionRows = transactions?.map(transaction => {
                const id = `t-${transaction.id}`;
                return {
                    id,
                    expanded         : Boolean(me._expandedById?.get(id)),
                    description      : transaction.description,
                    occurredAt       : transaction.occurredAt,
                    transactionModel : transaction,
                    rootVersionModel : version,
                    children         : transaction.actions.map((action, index) => {
                        const id = `a-${transaction.id}-${index}`;
                        return {
                            id,
                            expanded         : Boolean(me._expandedById?.get(id)),
                            action,
                            rootVersionModel : version,
                            children         : action.propertyUpdates?.map(propertyUpdate => ({
                                rootVersionModel : version,
                                propertyUpdate
                            })) ?? []
                        };
                    })
                };
            }) || [],
            id = `v-${version?.id}`;
        return version && showVersions ? {
            id,
            expanded     : Boolean(me._expandedById.get(id)),
            description  : version.description,
            occurredAt   : version.savedAt,
            children     : transactionRows,
            versionModel : version,
            iconCls      : 'b-icon-version',
            cls          : version.id === comparingVersionId ? me.comparingRowCls : null
        } : transactionRows;
    }

    renderDescription(event) {
        const { record } = event;
        if (record.propertyUpdate) {
            return this.renderPropertyUpdate(record.propertyUpdate);
        }
        else if (record.action) {
            return this.renderActionDescription(record.action);
        }
        return record.description;
    }

    renderPropertyUpdate(propertyUpdate) {
        const
            clsPrefix = VersionGrid.type,
            { property, before, after } = propertyUpdate;
        return {
            children : [{
                tag      : 'div',
                class    : `b-${clsPrefix}-property-update-desc`,
                children : [
                    {
                        tag   : 'span',
                        class : `b-${clsPrefix}-property-name`,
                        html  : `${this.formatPropertyName(property)}`
                    },
                    this.renderPropertyValue(before, 'before'),
                    {
                        tag   : 'i',
                        class : 'b-icon b-icon-right'
                    },
                    this.renderPropertyValue(after, 'after')
                ]
            }]
        };
    }

    /**
     * Return DomConfig for an individual data value.
     * @param {*} value
     * @param {'before'|'after'} side
     * @returns {DomConfig}
     * @private
     */
    renderPropertyValue(value, side) {
        return {
            tag   : 'span',
            class : [
                `b-${VersionGrid.type}-property-${side}`,
                value == null && `b-${VersionGrid.type}-empty-value`
            ],
            html : value == null ? this.L('L{Versions.nullValue}') : this.formatValueString(value) ?? ``
        };
    }

    /**
     * Convert an individual data value to a string.
     * @param {*} value The raw data value
     * @returns {String} A string representing the value, for display
     * @private
     */
    formatValueString(value) {
        if (DateHelper.isDate(value)) {
            return DateHelper.format(value, this.dateFormat);
        }
        else if (typeof (value) === 'number') {
            return value.toFixed(2);
        }
        return value;
    }

    /**
     * Format a property name in the change log to a displayable string. By default,
     * converts e.g. "camelCase" to "Camel case".
     * @param {String} propertyName The raw field name
     * @returns {String} A string formatted for display
     * @private
     */
    formatPropertyName(propertyName) {
        return StringHelper.separate(propertyName);
    }

    getAssignmentTextTokens(assignmentChange) {
        return {
            event    : assignmentChange.event.name,
            resource : assignmentChange.resource.name
        };
    }

    getDependencyTextTokens(dependencyChange) {
        return {
            from : dependencyChange.fromTask.name,
            to   : dependencyChange.toTask.name
        };
    }

    /**
     * Produces a text description to show in the description column for an 'action' row.
     * @param {SchedulerPro.model.changelog.ChangeLogAction} action The action to describe
     * @returns DomConfig of description text with highlightable entity names
     * @private
     */
    renderActionDescription(action) {
        const
            me = this,
            { actionType, entity } = action,
            entityNames = me.L(`L{Versions.entityNames}`);
        let description,
            tokens = {
                type : entityNames[entity.type],
                name : entity.name
            };
        if (actionType === 'move') {
            tokens.from = describePosition(action.from);
            tokens.to = describePosition(action.to);
        }

        // Concatenate action and entity type to get description pattern from localizations
        // e.g. 'L{Versions.addDependency}' | 'L{Versions.updateEntity}'
        description = me.L(`L{Versions.${actionType}${knownEntityTypes[entity.type] ?? 'Entity'}}`);

        if (entity.type === 'DependencyModel') {
            tokens = me.getDependencyTextTokens(entity);
        }
        else if (entity.type === 'AssignmentModel') {
            tokens = me.getAssignmentTextTokens(entity);
        }

        description = description.replace(/\{(\w+)\}/g, (_, variable) => tokens[variable] ?? variable);
        if (action.isUser) {
            description = `[!] ${description}`;
        }
        return me.renderHighlightedTextElements(StringHelper.capitalize(description), tokens);
    }

    /**
     * Sorts the actions within a transaction using precedence heuristic to show most "significant"
     * actions first.
     * @param {SchedulerPro.model.changelog.ChangeLogAction[]} actions
     */
    static sortActionRows(row1, row2) {
        if (row1.parent === row2.parent && row1.action && row2.action) {
            const
                isUser1 = Boolean(row1.action.isUser),
                isUser2 = Boolean(row2.action.isUser),
                { actionType : type1, entity : { type : entityType1 } } = row1.action,
                { actionType : type2, entity : { type : entityType2 } } = row2.action;

            // Initial user actions first
            if (isUser1 !== isUser2) {
                return isUser1 ? -1 : 1;
            }

            // Adds/removes first, then updates; within those groups, tasks first
            return Math.sign(actionTypeOrder[type1] - actionTypeOrder[type2]) ||
                Math.sign(entityTypeOrder[entityType1] - entityTypeOrder[entityType2]) ||
                0;
        }
        return 0;
    }

    triggerRestore(version) {
        /**
         * Fires when the user chooses to restore a selected version.
         * @event restore
         * @param {SchedulerPro.model.VersionModel} version The {@link SchedulerPro.model.VersionModel} being restored
         */
        this.trigger('restore', { version });
    }

    triggerCompare(version) {
        /**
         * Fires when the user chooses to compare a selected version.
         * @event compare
         * @param {SchedulerPro.model.VersionModel} version The {@link SchedulerPro.model.VersionModel} being restored
         */
        this.trigger('compare', { version });
    }

    triggerStopCompare(version) {
        /**
         * Fires when the user chooses to stop comparing a currently compared version.
         * @event stopCompare
         */
        this.trigger('stopCompare');
    }

    /**
     * Produce a DomConfig for cell text where **-delimited tokens are replaced by specified values. Used to
     * allow CSS styling of replaced tokens (e.g. task names) in the changelog.
     *
     * @param {String} text Text string containing optional **delimited tokens**, taken from localizations
     * @returns {DomConfig} DomConfig with text string broken into <span>s and tokens replaced
     * @internal
     */
    renderHighlightedTextElements(text) {
        const clsPrefix = this.constructor.type;
        return {
            children : [{
                tag      : 'span',
                class    : `b-${clsPrefix}-highlighted-text`,
                children : text.split(/\*\*/g).reduce((out, chunk) => {
                    out.children.push({
                        tag   : 'span',
                        text  : chunk,
                        class : out.isEntity ? `b-${clsPrefix}-highlighted-entity` : null
                    });
                    out.isEntity = !out.isEntity;
                    return out;
                }, { children : [], isEntity : false }).children
            }]
        };
    }

}

VersionGrid.initClass();
