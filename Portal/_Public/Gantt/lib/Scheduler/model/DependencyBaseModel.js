import DateHelper from '../../Core/helper/DateHelper.js';
import Model from '../../Core/data/Model.js';
import Duration from '../../Core/data/Duration.js';

/**
 * @module Scheduler/model/DependencyBaseModel
 */

const canonicalDependencyTypes = [
    'SS',
    'SF',
    'FS',
    'FF'
];

/**
 * Base class used for both Scheduler and Gantt. Not intended to be used directly
 *
 * @extends Core/data/Model
 */
export default class DependencyBaseModel extends Model {
    static get $name() {
        return 'DependencyBaseModel';
    }

    /**
     * Set value for the specified field(s), triggering engine calculations immediately. See
     * {@link Core.data.Model#function-set Model#set()} for arguments.
     **
     * ```javascript
     * dependency.set('from', 2);
     * // dependency.fromEvent is not yet up to date
     *
     * await dependency.setAsync('from', 2);
     * // dependency.fromEvent is up to date
     * ```
     *
     * @param {String|Object} field The field to set value for, or an object with multiple values to set in one call
     * @param {*} value Value to set
     * @param {Boolean} [silent=false] Set to true to not trigger events
     * automatically.
     * @function setAsync
     * @category Editing
     * @async
     */

    //region Fields

    /**
     * An enumerable object, containing names for the dependency types integer constants.
     * - 0 StartToStart
     * - 1 StartToEnd
     * - 2 EndToStart
     * - 3 EndToEnd
     * @property {Object}
     * @readonly
     * @category Dependency
     */
    static get Type() {
        return {
            StartToStart : 0,
            StartToEnd   : 1,
            EndToStart   : 2,
            EndToEnd     : 3
        };
    }

    static get fields() {
        return [
            // 3 mandatory fields

            /**
             * From event, id of source event
             * @field {String|Number} from
             * @category Dependency
             */
            { name : 'from' },

            /**
             * To event, id of target event
             * @field {String|Number} to
             * @category Dependency
             */
            { name : 'to' },

            /**
             * Dependency type, see static property {@link #property-Type-static}
             * @field {Number} type=2
             * @category Dependency
             */
            { name : 'type', type : 'int', defaultValue : 2 },

            /**
             * CSS class to apply to lines drawn for the dependency
             * @field {String} cls
             * @category Styling
             */
            { name : 'cls', defaultValue : '' },

            /**
             * Bidirectional, drawn with arrows in both directions
             * @field {Boolean} bidirectional
             * @category Dependency
             */
            { name : 'bidirectional', type : 'boolean' },

            /**
             * Start side on source (top, left, bottom, right)
             * @field {'top'|'left'|'bottom'|'right'} fromSide
             * @category Dependency
             */
            { name : 'fromSide', type : 'string' },

            /**
             * End side on target (top, left, bottom, right)
             * @field {'top'|'left'|'bottom'|'right'} toSide
             * @category Dependency
             */
            { name : 'toSide', type : 'string' },

            /**
             * The magnitude of this dependency's lag (the number of units).
             * @field {Number} lag
             * @category Dependency
             */
            { name : 'lag', type : 'number', allowNull : true, defaultValue : 0 },

            /**
             * The units of this dependency's lag, defaults to "d" (days). Valid values are:
             *
             * - "ms" (milliseconds)
             * - "s" (seconds)
             * - "m" (minutes)
             * - "h" (hours)
             * - "d" (days)
             * - "w" (weeks)
             * - "M" (months)
             * - "y" (years)
             *
             * This field is readonly after creation, to change `lagUnit` use {@link #function-setLag setLag()}.
             * @field {'ms'|'s'|'m'|'h'|'d'|'w'|'M'|'y'} lagUnit
             * @category Dependency
             * @readonly
             */
            {
                name         : 'lagUnit',
                type         : 'string',
                defaultValue : 'd'
            },

            { name : 'highlighted', persist : false, internal : true }
        ];
    }

    // fromEvent/toEvent defined in CoreDependencyMixin in engine

    /**
     * Gets/sets the source event of the dependency.
     *
     * Accepts multiple formats but always returns an {@link Scheduler.model.EventModel}.
     *
     * **NOTE:** This is not a proper field but rather an alias, it will be serialized but cannot be remapped. If you
     * need to remap, consider using {@link #field-from} instead.
     *
     * @field {Scheduler.model.EventModel} fromEvent
     * @accepts {String|Number|Scheduler.model.EventModel}
     * @category Dependency
     */

    /**
     * Gets/sets the target event of the dependency.
     *
     * Accepts multiple formats but always returns an {@link Scheduler.model.EventModel}.
     *
     * **NOTE:** This is not a proper field but rather an alias, it will be serialized but cannot be remapped. If you
     * need to remap, consider using {@link #field-to} instead.
     *
     * @field {Scheduler.model.EventModel} toEvent
     * @accepts {String|Number|Scheduler.model.EventModel}
     * @category Dependency
     */

    //endregion

    //region Init

    construct(data) {
        const
            from = data[this.fieldMap.from.dataSource],
            to   = data[this.fieldMap.to.dataSource];

        // Engine expects fromEvent and toEvent, not from and to. We need to support both
        if (from != null) {
            data.fromEvent = from;
        }

        if (to != null) {
            data.toEvent = to;
        }

        super.construct(...arguments);
    }

    //endregion

    get eventStore() {
        return this.eventStore || this.unjoinedStores[0]?.eventStore;
    }

    set from(value) {
        const { fromEvent } = this;

        // When assigning a new id to an event, it will update the eventId of the assignment. But the assignments
        // event is still the same so we need to announce here
        if (fromEvent?.isModel && fromEvent.id === value) {
            this.set('from', value);
        }
        else {
            this.fromEvent = value;
        }
    }

    get from() {
        return this.get('from');
    }

    set to(value) {
        const { toEvent } = this;

        // When assigning a new id to an event, it will update the eventId of the assignment. But the assignments
        // event is still the same so we need to announce here
        if (toEvent?.isModel && toEvent.id === value) {
            this.set('to', value);
        }
        else {
            this.toEvent = value;
        }
    }

    get to() {
        return this.get('to');
    }

    /**
     * Alias to dependency type, but when set resets {@link #field-fromSide} & {@link #field-toSide} to null as well.
     *
     * @property {Number}
     * @category Dependency
     */
    get hardType() {
        return this.getHardType();
    }

    set hardType(type) {
        this.setHardType(type);
    }

    /**
     * Returns dependency hard type, see {@link #property-hardType}.
     *
     * @returns {Number}
     * @category Dependency
     */
    getHardType() {
        return this.get('type');
    }

    /**
     * Sets dependency {@link #field-type} and resets {@link #field-fromSide} and {@link #field-toSide} to null.
     *
     * @param {Number} type
     * @category Dependency
     */
    setHardType(type) {
        let result;

        if (type !== this.hardType) {
            result = this.set({
                type,
                fromSide : null,
                toSide   : null
            });
        }

        return result;
    }

    get lag() {
        return this.get('lag');
    }

    set lag(lag) {
        this.setLag(lag);
    }

    /**
     * Sets lag and lagUnit in one go. Only allowed way to change lagUnit, the lagUnit field is readonly after creation
     * @param {Number|String|Object} lag The lag value. May be just a numeric magnitude, or a full string descriptor eg '1d'
     * @param {'ms'|'s'|'m'|'h'|'d'|'w'|'M'|'y'} [lagUnit] Unit for numeric lag value, see
     * {@link #field-lagUnit} for valid values
     * @category Dependency
     */
    setLag(lag, lagUnit = this.lagUnit) {
        // Either they're only setting the magnitude
        // or, if it's a string, parse the full duration.
        if (arguments.length === 1) {
            if (typeof lag === 'number') {
                this.lag = lag;
            }
            else {

                lag = DateHelper.parseDuration(lag);
                this.set({
                    lag     : lag.magnitude,
                    lagUnit : lag.unit
                });
            }
            return;
        }

        // Must be a number
        lag = parseFloat(lag);

        this.set({
            lag,
            lagUnit
        });
    }

    getLag() {
        if (this.lag) {
            return `${this.lag < 0 ? '-' : '+'}${Math.abs(this.lag)}${DateHelper.getShortNameOfUnit(this.lagUnit)}`;
        }
        return '';
    }

    /**
     * Property which encapsulates the lag's magnitude and units. An object which contains two properties:
     * @property {Core.data.Duration}
     * @property {Number} fullLag.magnitude The magnitude of the duration
     * @property {'ms'|'s'|'m'|'h'|'d'|'w'|'M'|'y'} fullLag.unit The unit in which the duration is measured, eg
     * `'d'` for days
     * @category Dependency
     */
    get fullLag() {
        return new Duration({
            unit      : this.lagUnit,
            magnitude : this.lag
        });
    }

    set fullLag(lag) {
        if (typeof lag === 'string') {
            this.setLag(lag);
        }
        else {
            this.setLag(lag.magnitude, lag.unit);
        }
    }

    /**
     * Returns true if the linked events have been persisted (e.g. neither of them are 'phantoms')
     *
     * @property {Boolean}
     * @readonly
     * @category Editing
     */
    get isPersistable() {
        const
            me = this,
            { stores, unjoinedStores } = me,
            store = stores[0];

        let result;

        if (store) {
            const
                { fromEvent, toEvent } = me,
                crudManager            = store.crudManager;

            // if crud manager is used it can deal with phantom source/target since it persists all records in one batch
            // if no crud manager used we have to wait till source/target are persisted
            result = fromEvent && (crudManager || !fromEvent.hasGeneratedId) && toEvent && (crudManager || !toEvent.hasGeneratedId);
        }
        else {
            result = Boolean(unjoinedStores[0]);
        }

        return result && super.isPersistable;
    }

    getDateRange() {
        const { fromEvent, toEvent } = this;

        if (fromEvent?.isScheduled && toEvent?.isScheduled) {
            const Type = DependencyBaseModel.Type;

            let sourceDate,
                targetDate;

            switch (this.type) {
                case Type.StartToStart:
                    sourceDate = fromEvent.startDateMS;
                    targetDate = toEvent.startDateMS;
                    break;

                case Type.StartToEnd:
                    sourceDate = fromEvent.startDateMS;
                    targetDate = toEvent.endDateMS;
                    break;

                case Type.EndToEnd:
                    sourceDate = fromEvent.endDateMS;
                    targetDate = toEvent.endDateMS;
                    break;

                case Type.EndToStart:
                    sourceDate = fromEvent.endDateMS;
                    targetDate = toEvent.startDateMS;
                    break;

                default:
                    throw new Error('Invalid dependency type: ' + this.type);
            }

            return {
                start : Math.min(sourceDate, targetDate),
                end   : Math.max(sourceDate, targetDate)
            };
        }

        return null;
    }

    /**
     * Applies given CSS class to dependency, the value doesn't persist
     *
     * @param {String} cls
     * @category Dependency
     */
    highlight(cls) {
        const classes = this.highlighted?.split(' ') ?? [];

        if (!classes.includes(cls)) {
            this.highlighted = classes.concat(cls).join(' ');
        }
    }

    /**
     * Removes given CSS class from dependency if applied, the value doesn't persist
     *
     * @param {String} cls
     * @category Dependency
     */
    unhighlight(cls) {
        const { highlighted } = this;

        if (highlighted) {
            const
                classes = highlighted.split(' '),
                index   = classes.indexOf(cls);

            if (index >= 0) {
                classes.splice(index, 1);
                this.highlighted = classes.join(' ');
            }
        }
    }

    /**
     * Checks if the given CSS class is applied to dependency.
     *
     * @param {String} cls
     * @returns {Boolean}
     * @category Dependency
     */
    isHighlightedWith(cls) {
        return this.highlighted && this.highlighted.split(' ').includes(cls);
    }

    getConnectorString(raw) {
        const rawValue = canonicalDependencyTypes[this.type];

        if (raw) {
            return rawValue;
        }

        // FS => empty string; it's the default
        if (this.type === DependencyBaseModel.Type.EndToStart) {
            return '';
        }

        return rawValue;
    }

    // getConnectorStringFromType(type, raw) {
    //     const rawValue = canonicalDependencyTypes[type];
    //
    //     if (raw) {
    //         return rawValue;
    //     }
    //
    //     // FS => empty string; it's the default
    //     if (type === DependencyBaseModel.Type.EndToStart) {
    //         return '';
    //     }
    //
    //     const locale = LocaleManager.locale;
    //
    //     // See if there is a local version of SS, SF or FF
    //     if (locale) {
    //         const localized = locale.Scheduler && locale.Scheduler[rawValue];
    //         if (localized) {
    //             return localized;
    //         }
    //     }
    //
    //     return rawValue;
    // }

    // getConnectorString(raw) {
    //     return this.getConnectorStringFromType(this.type);
    // }

    // * getConnectorStringGenerator(raw) {
    //     return this.getConnectorStringFromType(yield this.$.type);
    // }

    toString() {
        return `${this.from}${this.getConnectorString()}${this.getLag()}`;
    }

    /**
     * Returns `true` if the dependency is valid. It is considered valid if it has a valid type and both from and to
     * events are set and pointing to different events.
     *
     * @property {Boolean}
     * @typings ignore
     * @category Editing
     */
    get isValid() {
        const { fromEvent, toEvent, type } = this;

        return typeof type === 'number' && fromEvent && toEvent && fromEvent !== toEvent;
    }

    get fromEventName() {
        return this.fromEvent?.name || '';
    }

    get toEventName() {
        return this.toEvent?.name || '';
    }

    //region STM hooks

    shouldRecordFieldChange(fieldName, oldValue, newValue) {
        if (!super.shouldRecordFieldChange(fieldName, oldValue, newValue)) {
            return false;
        }

        if (fieldName === 'from' || fieldName === 'to' || fieldName === 'fromEvent' || fieldName === 'toEvent') {

            const eventStore = this.project?.eventStore;

            if (eventStore && eventStore.oldIdMap[oldValue] === eventStore.getById(newValue)) {
                return false;
            }
        }

        return true;
    }

    //endregion
}

DependencyBaseModel.exposeProperties();
