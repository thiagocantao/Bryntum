import Base from '../Base.js';
import OH from '../helper/ObjectHelper.js';
import StringHelper from '../helper/StringHelper.js';
import VersionHelper from '../helper/VersionHelper.js';
import ModelStm from './stm/mixin/ModelStm.js';
import TreeNode from './mixin/TreeNode.js';
import DataField from './field/DataField.js';

// The built-in model field types:
import './field/BooleanDataField.js';
import './field/DateDataField.js';
import './field/IntegerDataField.js';
import './field/ModelDataField.js';
import './field/NumberDataField.js';
import './field/ObjectDataField.js';
import './field/StringDataField.js';

const
    { defineProperty } = Reflect,
    { hasOwnProperty } = Object.prototype,
    _undefined         = undefined,
    internalProps      = {
        children : 1,
        data     : 1,
        meta     : 1
    },
    abbreviationFields = [
        'name',
        'title',
        'text',
        'label',
        'description'
    ];

/**
 * @module Core/data/Model
 */

/**
 * A Model is a definition for a record in a store. It defines which fields the data contains and exposes an interface
 * to access and manipulate that data.
 *
 * Models are created from json objects, the input json is stored in `Model#data`. By default it stores a shallow copy of
 * the raw json, but for records in stores configured with `useRawData: true` it stores the supplied json object as is.
 *
 * ## Defining Fields
 * A Model can either define its fields explicitly (see {@link #property-fields-static}) or have them created from its
 * data (see {@link #property-autoExposeFields-static}). This snippet shows a model with 4 fields defined explicitly:
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             'name',
 *             { name : 'birthday', type : 'date', format : 'YYYY-MM-DD' },
 *             { name : 'shoeSize', type : 'number', defaultValue : 11 },
 *             { name : 'age', readOnly : true }
 *         ];
 *     }
 * }
 * ```
 *
 * The first field (name) has an unspecified type, which means the field's value is held as received with no conversion
 * applied. The second field (birthday) is defined to be a date, which will make the model parse any supplied value into
 * an actual date. The parsing is handled by {@link Core/helper/DateHelper#function-parse-static DateHelper.parse()}
 * using the specified `format`, or if no format is specified using
 * {@link Core/helper/DateHelper#property-defaultFormat-static DateHelper.defaultFormat}.
 *
 * The set of standard field types is as follows:
 *
 *  - {@link Core.data.field.BooleanDataField `boolean`}
 *  - {@link Core.data.field.DateDataField `date`}
 *  - {@link Core.data.field.IntegerDataField `integer`}
 *  - {@link Core.data.field.NumberDataField `number`}
 *  - {@link Core.data.field.StringDataField `string`}
 *
 * You can also set a `defaultValue` that will be used if the data does not contain a value for the field:
 *
 * ```javascript
 * { name : 'shoeSize', type : 'number', defaultValue : 11 }
 * ```
 *
 * To create a record from a Model, supply data to its constructor:
 *
 * ```javascript
 * let guy = new Person({
 *   id       : 1,
 *   name     : 'Dude',
 *   birthday : '2014-09-01'
 * });
 * ```
 *
 * If no id is specified, a temporary id will be generated.
 *
 * ## Persisting Fields
 * By default all fields are persisted. If you don't want particular field to get saved to the server, configure it with
 * `persist: false`. In this case field will not be among changes which are sent by
 * {@link Core/data/AjaxStore#function-commit store.commit()}, otherwise its behavior doesn't change.
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             'name',
 *             { name : 'age', persist : false }
 *         ];
 *     }
 * }
 * ```
 *
 * ## The `idField`
 * By default Model expects its id to be stored in a field named "id". The name of the field can be customized by
 * setting {@link #property-idField-static}:
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             'name',
 *             { name : 'age', persist : false },
 *             { name : 'personId' },
 *             { name : 'birthday', type : 'date' }
 *         ];
 *     }
 * }
 *
 * // Id drawn from 'id' property by default; use custom field here
 * Person.idField = 'personId';
 *
 * let girl = new Person({
 *    personId : 2,
 *    name     : 'Lady',
 *    birthday : '2011-11-05'
 * });
 * ```
 *
 * ## Getting and Setting Values
 * Fields are used to generate getters and setters on the records. Use them to access or modify values (they are
 * reactive):
 *
 * ```javascript
 * console.log(guy.name);
 * girl.birthday = new Date(2011,10,6);
 * ```
 *
 * NOTE: In an application with multiple different models you should subclass Model, since the prototype is decorated
 * with getters and setters. Otherwise you might get unforeseen collisions.
 *
 * ## Field Data Mapping
 * By default fields are mapped to data using their name. If you for example have a "name" field it expects data to be
 * `{ name: 'Some name' }`. If you need to map it to some other property, specify `dataSource` in your field definition:
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields {
 *         return [
 *             { name : 'name', dataSource : 'TheName' }
 *         ];
 *     }
 * }
 *
 * // This is now OK:
 * let dude = new Person({ TheName : 'Manfred' });
 * console.log(dude.name); // --> Manfred
 * ```
 * ## Field Inheritance
 * Fields declared in a derived model class are added to those from its superclass. If a field declared by a derived
 * class has also been declared by its super class, the field properties of the super class are merged with those of
 * the derived class.
 *
 * For example:
 * ```javascript
 *  class Person extends Model {
 *      static get fields() {
 *          return [
 *              'name',
 *              { name : 'birthday', type : 'date', format : 'YYYY-MM-DD' }
 *          ];
 *      }
 *  }
 *
 *  class User extends Person {
 *      static get fields() {
 *          return [
 *              { name : 'birthday', dataSource : 'dob' },
 *              { name : 'lastLogin', type : 'date' }
 *          ];
 *      }
 *  }
 * ```
 * In the above, the `Person` model declares the `birthday` field as a `date` with a specified `format`. The `User`
 * model extends `Person` and also declares the `birthday` field. This redeclared field only specifies `dataSource`, so
 * all of the other fields are preserved from `Person`. The `User` model also adds a `lastLogin` field.
 *
 * The `User` from above could have been declared like so to achieve the same `fields`:
 * ```javascript
 *  class User extends Model {
 *      static get fields() {
 *          return [
 *              'name',
 *              { name : 'birthday', type : 'date', format : 'YYYY-MM-DD', dataSource : 'dob' },
 *              { name : 'lastLogin', type : 'date' }
 *          ];
 *      }
 *  }
 * ```
 *
 * ## Override default values
 *
 * In case you need to define default value for a specific field, or override an existing default value, you can
 * define a new or re-define an existing field definition in {@link #property-fields-static} static getter:
 *
 * ```javascript
 * class Person extends Model {
 *     static get fields() {
 *         return [
 *             { name : 'username', defaultValue : 'New person' },
 *             { name : 'birthdate', type : 'date' }
 *         ];
 *     }
 * }
 *
 * class Bot extends Person {
 *     static get fields() {
 *         return [
 *             { name : 'username', defaultValue : 'Bot' } // default value of 'username' field is overridden
 *         ];
 *     }
 * }
 * ```
 *
 * ## Tree API
 * This class mixes in the {@link Core/data/mixin/TreeNode TreeNode} mixin which provides an API for tree related functionality (only relevant if your
 * store is configured to be a {@link Core/data/Store#config-tree tree}).
 *
 * @mixes Core/data/mixin/TreeNode
 * @mixes Core/data/stm/mixin/ModelStm
 */
export default class Model extends Base.mixin(ModelStm, TreeNode) {
    static get $name() {
        return 'Model';
    }

    static get declarable() {
        return [
            /**
             * Array of defined fields for this model class. Subclasses add new fields by implementing this static
             * getter:
             *
             * ```javascript
             * // Model defining two fields
             * class Person extends Model {
             *     static get fields() {
             *         return [
             *             { name : 'username', defaultValue : 'New person' },
             *             { name : 'birthdate', type : 'date' }
             *         ];
             *     }
             * }
             *
             * // Subclass overriding one of the fields
             * class Bot extends Person {
             *     static get fields() {
             *         return [
             *             // Default value of 'username' field is overridden, any other setting from the parents
             *             // definition is preserved
             *             { name : 'username', defaultValue : 'Bot' }
             *         ];
             *     }
             * }
             * ```
             *
             * Fields in a subclass are merged with those from the parent class, making it easy to override mappings,
             * formats etc.
             *
             * @member {Object[]|Core.data.field.DataField[]} fields
             * @readonly
             * @static
             * @category Fields
             */
            'fields'
        ];
    }

    static get fields() {
        return [
            // The index of this item in its parent (respects filtering)
            { name : 'parentIndex', type : 'number', persist : false }
        ];
    }

    /**
     * Template static getter which is supposed to be overridden to define default field values for the Model class.
     * Overrides `defaultValue` config specified by the {@link #property-fields-static} getter.
     * Returns a named object where key is a field name and value is a default value for the field.
     *
     * NOTE: This is a legacy way of defining default values, we recommend using {@link #property-fields-static} moving
     * forward.
     *
     * ```javascript
     * class Person extends Model {
     *     static get fields() {
     *         return [
     *             { name : 'username', defaultValue : 'New person' }
     *         ];
     *     }
     * }
     *
     * class Bot extends Person {
     *     static get defaults() {
     *         return {
     *             username : 'Bot' // default value of 'username' field is overridden
     *         };
     *     }
     * }
     * ```
     *
     * @member {Object[]} defaults
     * @static
     * @category Fields
     */
    // TODO: deprecate in 5.0 in favor of static fields getter
    // static get defaults() {
    //     return {};
    // }

    /**
     * The name of the data field which provides the ID of instances of this Model.
     * @property {String}
     * @category Fields
     */
    static set idField(idField) {
        this._assignedIdField = true;
        this._idField = idField;
    }

    static get idField() {
        return this._idField;
    }

    /**
     * The name of the data field which holds children of this Model when used in a tree structure
     * ```javascript
     * MyModel.childrenField = 'kids';
     * const parent = new MyModel({
     *   name : 'Dad',
     *   kids : [
     *     { name : 'Daughter' },
     *     { name : 'Son' }
     *   ]
     * });
     * ```
     * @property {String}
     * @category Fields
     */
    static set childrenField(childrenField) {
        this._childrenField = childrenField;
    }

    static get childrenField() {
        return this._childrenField || 'children';
    }

    /**
     * Returns index path to this node. This is the index of each node in the node path
     * starting from the topmost parent. (only relevant when its part of a tree store).
     * @returns {Number[]} The index of each node in the path from the topmost parent to this node.
     * @category Parent & children
     * @private
     */
    get indexPath() {
        const indices = [];

        let node = this,
            depth = node.childLevel;

        for (node = this; node && !node.isRoot; node = node.parent) {
            indices[depth--] = node.parentIndex + 1;
        }

        return indices;
    }

    /**
     * Unique identifier for the record. Might be mapped to another dataSource using idField, but always exposed as
     * record.id. Will get a generated value if none is specified in records data.
     * @member {String|Number} id
     * @category Identification
     */

    //region Init

    /**
     * Constructs a new record from the supplied data.
     * @param {Object} [data] Raw data
     * @param {Core.data.Store} [store] Data store
     * @param {Object} [meta] Meta data
     * @function constructor
     * @category Lifecycle
     */
    construct(data = {}, store = null, meta = null, skipExpose = false) {
        const
            me     = this,
            stores = store ? Array.isArray(store) ? store : [store] : [],
            { constructor, fieldMap } = me;

        // null passed to Base construct inhibits config processing.
        let configs = null;

        store = stores[0];

        me.meta = {
            modified : {},
            ...constructor.metaConfig,
            ...meta
        };

        // Should apply configs?
        if (constructor.applyConfigs) {
            // Extract from data and combine with defaultConfigs
            for (const key in me.$meta.config) {
                if (!configs) {  // if (first config)
                    configs = {};

                    if (!me.useRawData || !me.useRawData.enabled) {
                        // Shallow copy of data to not mutate incoming object
                        data = { ...data };
                    }
                }

                if (key in data) {
                    // Use as config
                    configs[key] = data[key];
                    // Remove from data
                    delete data[key];
                }
            }
        }

        super.construct(configs);

        // make getters/setters for fields, needs to be done before processing data to make sure defaults are available
        if (!skipExpose) {
            constructor.exposeProperties(data);
        }

        // It's only valid to do this once, on construction of the first instance
        if (!hasOwnProperty.call(constructor, 'idFieldProcessed')) {
            // idField can be overridden from meta, or from the store if we have not had an idField set programmatically
            // and if we have not had an id field defined above the base Model class level.

            let overriddenIdField = me.meta.idField;

            if (!overriddenIdField) {
                // Might have been set to Model after construction but before load
                if (constructor._assignedIdField) {
                    overriddenIdField = constructor.idField;
                }
                // idField on store was deprecated, but should still work to not break code
                // TODO: Remove in 3.0? Or reintroduce it...
                else if (store) {
                    overriddenIdField = store.idField;
                }
            }

            // If it's overridden to something different than we already have, replace the 'id' field in the fieldMap
            if (overriddenIdField && overriddenIdField !== fieldMap.id.dataSource) {
                constructor.addField({
                    name       : 'id',
                    dataSource : overriddenIdField
                });
            }
            constructor.idFieldProcessed = true;
        }

        // assign internalId, unique among all records
        me._internalId = Model._internalIdCounter++;

        // relation code expects store to be available for relation lookup, but actual join done below
        me.stores = [];
        me.unjoinedStores = [];

        // Superclass constructors may set this in their own way before this is called.
        if (!me.originalData) {
            me.originalData = data;
        }

        me.data = constructor.processData(data, false, store);

        // Consider undefined and null as missing id and generate one
        if (me.id == null) {
            // Assign a generated id silently, record should not be considered modified
            me.setData('id', me.generateId(store));
        }
        if (me.data[constructor.childrenField]) {
            me.processChildren(stores);
        }
        me.generation = 0;
    }

    /**
     * Compares this Model instance to the passed instance. If they are of the same type, and all fields
     * (except, obviously, `id`) are equal, this returns `true`.
     * @param {Core.data.Model} other The record to compare this record with.
     * @returns {Boolean} `true` if the other is of the same class and has all fields equal.
     */
    equals(other) {
        if (other instanceof this.constructor) {
            for (let fields = this.$meta.fields.defs, i = 0, { length } = fields; i < length; i++) {
                const
                    field    = fields[i],
                    { name } = field;

                if (name !== 'id' && !field.isEqual(this[name], other[name])) {
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    get subclass() {
        return new this.constructor(Object.setPrototypeOf({
            id : _undefined
        }, this.data), this.stores[0], null, true);
    }

    /**
     * Processes raw data, converting values and setting defaults.
     * @private
     * @param {Object} data Raw data
     * @param {Boolean} [ignoreDefaults] Ignore setting default values, used when updating
     * @returns {Object} Processed data
     * @category Fields
     */
    static processData(data, ignoreDefaults = false, store) {
        const
            { fieldMap, defaultValues } = this,
            { useRawData = { enabled : false } } = store || { },
            // Store configured with useRawData uses the supplied data object, polluting it. When not configured with
            // useRawData it instead makes a shallow copy.
            processed = useRawData.enabled ? data : { ...data };

        let fieldName;

        ignoreDefaults = ignoreDefaults || useRawData.disableDefaultValue;

        if (!ignoreDefaults) {
            for (fieldName in defaultValues) {
                if (processed[fieldName] === _undefined) {
                    processed[fieldName] = defaultValues[fieldName];
                }
            }
        }

        if (!useRawData.disableTypeConversion) {
            // Convert field types which need converting
            for (fieldName in fieldMap) {
                const
                    field                = fieldMap[fieldName],
                    { name, dataSource } = field,
                    // Value might have been supplied either using mapped dataSource (when loading JSON etc. for example
                    // event.myStartDate) or as field name (from internal code, for example event.startDate). If [name]
                    // exists but not [dataSource], use it.
                    hasSource            = dataSource !== name,
                    sourceExists         = hasSource && OH.pathExists(data, dataSource),
                    useNameForValue      = (name in data) && (!hasSource || !sourceExists),
                    convert              = !useRawData.disableTypeConversion && field.convert;

                // Only action field definitions which have a convert function or remap data
                if (useNameForValue || convert) {
                    // When ignoringDefaults, do not convert unspecified values
                    if (!ignoreDefaults || useNameForValue || sourceExists) {
                        const value = useNameForValue ? processed[name] : OH.getPath(processed, dataSource);

                        OH.setPath(processed, dataSource, convert ? field.convert(value) : value);

                        // Remove [startDate] from internal data holder, only keeping [myStartDate]
                        if (hasSource) {
                            delete processed[name];
                        }
                    }
                }
            }
        }

        return processed;
    }

    static setupClass(meta) {
        super.setupClass(meta);

        if (!meta.fields) {
            // Normally setupFields will only run when a Model defines a fields getter, but we want to always run it:
            this.setupFields(this, meta);
        }
    }

    static setupFields(cls, meta) {
        const
            classFields = hasOwnProperty.call(cls, 'fields') && cls.fields,
            base        = meta.super.fields,
            fieldsInfo  = meta.fields = {
                defs : base?.defs.slice() ?? [],

                // Set to true when an instance's data object is run through exposeProperties
                exposedData : false,

                // These objects are all keyed by field name:
                defaults : base ? { ...base.defaults } : {}, // value=field.defaultValue
                exposed  : Object.create(base?.exposed  ?? null),   // value=true if we've done defineProperty
                ordinals : Object.create(base?.ordinals ?? null),   // value=index in the defs array
                map      : Object.create(base?.map      ?? null),   // value=definition object
                sources  : Object.create(base?.sources  ?? null)    // value=source definition object
            };

        // We use Object.create(null) as the base for these maps because some models declare "constructor" as a field
        // NOTE: instead of chaining the defaults, we copy them so the defaults object can be used with Object.assign
        // in other contexts (since it does not copy inherited properties from the prototype chain)

        // Clone the superclass's defaults, and override that with our own defaults.
        // As we find fields with a defaultValue, more defaults may be added
        if (hasOwnProperty.call(cls, 'defaults')) {
            Object.assign(fieldsInfo.defaults, cls.defaults);
        }

        // Hook up our field maps with the class hierarchy's fieldMaps.
        // We need to be able to look up field definitions by the name, or by the dataSource property name

        // If the idField is overridden at this level, create a new field
        if (hasOwnProperty.call(cls, 'idField')) {
            cls.addField({
                name       : 'id',
                dataSource : cls.idField
            });
            fieldsInfo.exposed[cls.idField] = true;
        }

        // Process fields defined in the class definition
        if (classFields?.length) {
            classFields.map(cls.addField, cls);
        }

        cls.exposeRelations();
    }

    static get defaultValues() {
        return this.$meta.fields.defaults;
    }

    /**
     * An object containing all the _defined_ fields for this Model class. This will include all superclass's
     * defined fields through its prototype chain. So be aware that `Object.keys` and `Object.entries` will only
     * access this class's defined fields.
     * @property {Object}
     * @static
     * @readonly
     * @category Fields
     */
    static get fieldMap() {
        return this.$meta.fields.map;
    }

    /**
     * Same as {@link #property-fieldMap-static}.
     * @property {Object}
     * @readonly
     * @category Fields
     */
    get fieldMap() {
        return this.$meta.fields.map;
    }

    static get fieldDataSourceMap() {
        return this.$meta.fields.sources;
    }

    /**
     * Makes getters and setters for fields (from definitions and data). Called once when class is defined and once when
     * data is loaded first time.
     * @internal
     * @param {Object} [data] Raw data
     * @category Fields
     */
    static exposeProperties(data) {
        const
            me = this,
            fieldsInfo = me.$meta.fields;

        // Process the raw data properties and expose them as fields unless the property name
        // has already been used by the "dataSource" of a defined field.
        if (data && me.autoExposeFields && !fieldsInfo.exposedData) {
            let dataProperty, fieldDef, type;

            for (dataProperty in data) {
                if (!fieldsInfo.exposed[dataProperty]) {
                    type = typeof data[dataProperty];

                    // Create a field definition in our fieldMap with the flag that it's from data
                    fieldDef = {
                        name       : dataProperty,
                        dataSource : dataProperty,
                        fromData   : true
                    };

                    if (type === 'boolean' || type === 'number') {
                        fieldDef.type = type;
                    }

                    me.addField(fieldDef);
                }
            }

            fieldsInfo.exposedData = true;
        }

        me.exposeRelations();
    }

    /**
     * Add a field definition in addition to those predefined in `fields`.
     * @param {String|Object} fieldDef A field name or definition
     */
    static addField(fieldDef) {
        if (fieldDef == null) {
            return;
        }

        if (typeof fieldDef === 'string') {
            fieldDef = {
                name : fieldDef
            };
        }

        const
            me = this.initClass(),
            fieldsInfo = me.$meta.fields,
            { ordinals } = fieldsInfo,
            propertiesExposed = fieldsInfo.exposed,
            { name } = fieldDef,
            existing = fieldsInfo.map[name],
            dataSource = fieldDef.dataSource || (fieldDef.dataSource = name);

        let field, key;

        if (!existing || (fieldDef.type && fieldDef.type !== existing.type)) {
            field = DataField.create(fieldDef);
            field.definedBy = existing ? existing.definedBy : me;
            field.ordinal = existing ? existing.ordinal : (ordinals[name] = fieldsInfo.defs.length);
        }
        else {
            field = Object.create(existing);

            for (key in fieldDef) {
                if (key !== 'type') {
                    field[key] = fieldDef[key];
                }
            }
        }

        field.owner = me;
        fieldsInfo.defs[field.ordinal] = field;
        fieldsInfo.map[name] = field;

        if (!fieldsInfo.sources[dataSource]) {
            fieldsInfo.sources[dataSource] = field;
        }

        // With complex mapping avoid exposing object as model field
        if (dataSource.includes('.')) {
            field.complexMapping = true;

        }
        if (field.complexMapping) {  // model fields have this set on their prototype...
            propertiesExposed[dataSource.split('.')[0]] = true;
        }
        else {
            // When iterating through the raw data, if autoExposeFields is set
            // We do not need to create properties for raw property names we've processed here
            propertiesExposed[dataSource] = true;
        }

        // Maintain an object of defaultValues for fields.
        if ('defaultValue' in field) {
            fieldsInfo.defaults[dataSource] = field.defaultValue;
        }

        // Create a property on this Model's prototype, named for the defined field name
        // which reads the correct property out of the raw data object.
        if (!internalProps[name]) {
            field.defineAccessor(me.prototype);
        }

        return field;
    }

    /**
     * Remove a field definition by name.
     * @param {String} fieldName Field name
     */
    static removeField(fieldName) {
        const
            me = this.initClass(),
            fieldsInfo = me.$meta.fields,
            definition = fieldsInfo.map[fieldName],
            { ordinals } = fieldsInfo,
            index = ordinals[fieldName];

        if (definition) {
            fieldsInfo.defs.splice(index, 1);

            delete ordinals[fieldName];
            delete fieldsInfo.defaults[fieldName];
            delete fieldsInfo.exposed[fieldName];
            delete fieldsInfo.map[fieldName];
            delete fieldsInfo.sources[definition.dataSource];

            for (const name in ordinals) {
                if (ordinals[name] > index) {
                    --ordinals[name];
                }
            }

            // Note: if field was exposed by superclass, this won't do anything...
            delete me.prototype[fieldName];
        }
    }

    /**
     * Makes getters and setters for related records. Populates a Model#relation array with the relations, to allow it
     * to be modified later when assigning stores.
     * @internal
     * @category Relations
     */
    static exposeRelations() {
        const me = this;

        if (hasOwnProperty.call(me, 'relationsExposed')) {
            return;
        }

        if (me.relationConfig) {
            me.relationsExposed = true;
            me.relations = [];

            me.relationConfig.forEach(relation => {
                me.relations.push(relation);

                const name = relation.relationName;

                // getter and setter for related object
                if (!Reflect.ownKeys(me.prototype).includes(name)) {
                    defineProperty(me.prototype, name, {
                        enumerable : true,
                        get        : function() {
                            // noinspection JSPotentiallyInvalidUsageOfClassThis
                            return this.getForeign(name);
                        },
                        set : function(value) {
                            // noinspection JSPotentiallyInvalidUsageOfClassThis
                            this.setForeign(name, value, relation);
                        }
                    });
                }
            });
        }
    }

    //endregion

    //region Fields

    /**
     * Flag checked from Store when loading data that determines if fields found in first records should be exposed in
     * same way as predefined fields.
     * @returns {Boolean}
     * @category Fields
     */
    static get autoExposeFields() {
        return true;
    }

    /**
     * Convenience getter to get field definitions from class.
     * @returns {Core.data.field.DataField[]}
     * @category Fields
     */
    get fields() {
        return this.$meta.fields.defs;
    }

    /**
     * Convenience function to get the definition for a field from class.
     * @param {String} fieldName Field name
     * @returns {Core.data.field.DataField}
     * @category Fields
     */
    getFieldDefinition(fieldName) {
        return this.$meta.fields.map[fieldName];
    }

    getFieldDefinitionFromDataSource(dataSource) {
        return this.$meta.fields.sources[dataSource];
    }

    /**
     * Get the names of all fields in data.
     * @returns {String[]} Field names
     * @readonly
     * @category Fields
     */
    get fieldNames() {
        return Object.keys(this.data);
    }

    /**
     * Get the definition for a field by name. Caches results.
     * @param {String} fieldName Field name
     * @returns {Core.data.field.DataField} Field definition or null if none found
     * @category Fields
     */
    static getFieldDefinition(fieldName) {
        return this.$meta.fields.map[fieldName];
    }

    /**
     * Returns dataSource configuration for a given field name
     * @param {String} fieldName
     * @returns {String} Field `dataSource` mapping
     * @internal
     */
    static getFieldDataSource(fieldName) {
        return this.getFieldDefinition(fieldName).dataSource;
    }

    /**
     * Get the data source used by specified field. Returns the fieldName if no data source specified.
     * @param {String} fieldName Field name
     * @returns {String}
     * @category Fields
     */
    getDataSource(fieldName) {
        const def = this.constructor.getFieldDefinition(fieldName);

        return def?.dataSource || def?.name;
    }

    /**
     * Processes input to a field, converting to expected type.
     * @param {String} fieldName Field dataSource
     * @param {*} value Value to process
     * @returns {*} Converted value
     * @category Fields
     */
    static processField(fieldName, value) {
        const field = this.fieldMap[fieldName];

        return field?.convert ? field.convert(value) : value;
    }

    //endregion

    //region Relations

    /**
     * Initializes model relations. Called from store when adding a record.
     * @private
     * @category Relations
     */
    initRelations() {
        const me        = this,
            relations = me.constructor.relations;

        if (!relations) return;

        // TODO: feels strange to have to look at the store for relation config but didn't figure out anything better.
        // TODO: because other option would be to store it on each model instance, not better...

        me.stores.forEach(store => {
            if (!store.modelRelations) {
                store.initRelations();
            }

            // TODO: not at all tested for multiple stores, can't imagine it works as is
            const relatedRecords = [];

            store.modelRelations?.forEach(config => {
                relatedRecords.push({ related : me.initRelation(config), config });
            });
            store.updateRecordRelationCache(me, relatedRecords);
        });
    }

    /**
     * Initializes/updates a single relation.
     * @param config Relation config
     * @returns {Core.data.Model} Related record
     * @private
     * @category Relations
     */
    initRelation(config) {
        const
            me          = this,
            keyValue    = me.get(config.fieldName),
            foreign     = keyValue !== _undefined && typeof config.store !== 'string' && config.store.getById(keyValue),
            placeHolder = { id : keyValue, placeHolder : true };

        if (!me.meta.relationCache) {
            me.meta.relationCache = {};
        }

        // apparently scheduler tests expect cache to work without matched related record, thus the placeholder
        me.meta.relationCache[config.relationName] = foreign || (keyValue != null ? placeHolder : null);

        return foreign;
    }

    removeRelation(config) {
        // (have to check for existence before deleting to work in Safari)
        if (this.meta.relationCache[config.relationName]) {
            delete this.meta.relationCache[config.relationName];
            if (config.nullFieldOnRemove) {
                // Setting to null silently, to not trigger additional relation behaviour
                this.setData(config.fieldName, null);
            }
        }
    }

    getForeign(name) {
        return this.meta.relationCache?.[name];
    }

    setForeign(name, value, config) {
        const id = Model.asId(value);
        return this.set(config.fieldName, id);
    }

    //endregion

    //region Get/set values, data handling

    /**
     * Get value for specified field name. You can also use the generated getters if loading through a Store.
     * If model is currently in batch operation this will return updated batch values which are not applied to Model
     * until endBatch() is called.
     * @param {String} fieldName Field name to get value from
     * @returns {*} Fields value
     * @category Fields
     */
    get(fieldName) {
        const
            me           = this,
            batchChanges = me.meta.batchChanges,
            { fieldMap } = me,
            recData      = me.data;

        let field = fieldMap[fieldName];

        const dataSource = field ? field.dataSource : fieldName;

        // When changes are batched, they get stored by field name, not dataSource
        if (batchChanges && fieldName in batchChanges) {
            return batchChanges[fieldName];
        }

        // If changes are not batched, it should be resolved from the data using dataSource
        if (dataSource) {
            // Getting property of nested record?
            if (!field && fieldName.includes('.')) {
                const nestedName = fieldName.split('.')[0];

                field = fieldMap[nestedName];
            }

            if (field?.complexMapping) {
                return OH.getPath(recData, dataSource);
            }

            return (dataSource in recData) ? recData[dataSource] : recData[fieldName];
        }
    }

    /**
     * Internal function used to update a records underlying data block (record.data) while still respecting field
     * mappings. Needed in cases where a field needs setting without triggering any associated behaviour and it has a
     * dataSource with a different name.
     *
     * For example:
     * ```javascript
     * // startDate mapped to data.beginDate
     * { name : 'startDate', dataSource : 'beginDate' }
     *
     * // Some parts of our code needs to update the data block without triggering any of the behaviour associated with
     * // calling set. This would then not update "beginDate":
     * record.data.startDate = xx;
     *
     * // But this would
     * record.setData('startDate', xx);
     * ```
     * @internal
     * @category Editing
     */
    setData(toSet, value) {
        const { data, fieldMap } = this;

        // Two separate paths for performance reasons

        // setData('name', 'Quicksilver');
        if (typeof toSet === 'string') {
            const
                field      = fieldMap[toSet],
                dataSource = field?.dataSource ?? toSet;

            if (field?.complexMapping) {
                OH.setPath(data, dataSource, value);
            }
            else {
                data[dataSource] = value;
            }
        }
        // setData({ name : 'Magneto', power : 'Magnetism' });
        else {
            const keys = Object.keys(toSet);

            for (let i = 0; i < keys.length; i++) {
                const
                    fieldName  = keys[i],
                    field      = fieldMap[fieldName],
                    dataSource = field?.dataSource ?? fieldName;

                if (dataSource) {
                    if (field?.complexMapping) {
                        OH.setPath(data, dataSource, toSet[fieldName]);
                    }
                    else {
                        data[dataSource] = toSet[fieldName];
                    }
                }
            }
        }

    }

    /**
     * Returns raw data from the encapsulated data object for the passed field name
     * @param {String} fieldName The field to get data for.
     * @returns {*} The raw data value for the field.
     */
    getData(fieldName) {
        const
            field      = this.fieldMap[fieldName],
            dataSource = field?.dataSource ?? fieldName;

        if (dataSource) {
            if (field?.complexMapping) {
                return OH.getPath(this.data, dataSource);
            }

            return this.data[dataSource];
        }
    }

    /**
     * Silently updates record's id with no flagging the property as modified.
     * Triggers onModelChange event for changed id.
     * @param {String|Number} value id value
     * @private
     */
    syncId(value) {
        const oldValue = this.id;
        if (oldValue !== value) {
            this.setData('id', value);
            const data = { id : { value, oldValue } };
            this.afterChange(data, data);
        }
    }

    /**
     * Set value for the specified field. You can also use the generated setters if loading through a Store.
     *
     * Setting a single field, supplying name and value:
     *
     * ```javascript
     * record.set('name', 'Clark');
     * ```
     *
     * Setting multiple fields, supplying an object:
     *
     * ```javascript
     * record.set({
     *    name : 'Clark',
     *    city : 'Metropolis'
     * });
     * ```
     *
     * @param {String|Object} field The field to set value for, or an object with multiple values to set in one call
     * @param {*} value Value to set
     * @param {Boolean} [silent] Set to true to not trigger events. If event is recurring, occurrences won't be updated automatically.
     * @fires Store#idChange
     * @fires Store#update
     * @fires Store#change
     * @category Editing
     */
    set(field, value, silent = false, fromRelationUpdate = false, skipAccessors = false) {
        const me = this;

        // We use beforeSet/inSet/afterSet approach here because mixin interested in overriding set() method
        // like STM, for example, might be mixed before Model class or after. In general I have no control over this.
        // STM mixed before, so the only option to wrap set() method body is actually to call
        // beforeSet()/afterSet().

        if (me.isBatchUpdating) {
            me.inBatchSet(field, value);
            return null;
        }
        else {
            const
                preResult = me.beforeSet ? me.beforeSet(field, value, silent, fromRelationUpdate) : _undefined,
                wasSet    = me.inSet(field, value, silent, fromRelationUpdate, skipAccessors);
            me.afterSet?.(field, value, silent, fromRelationUpdate, preResult, wasSet);
            return wasSet;
        }
    }

    fieldToKeys(field, value) {
        if (typeof field !== 'string') {
            // will get in trouble when setting same field on multiple models without this
            return OH.assign({}, field);
        }

        return {
            [field] : value
        };
    }

    inBatchSet(field, value) {
        const { meta, constructor } = this;

        let changed = false;

        if (typeof field !== 'string') {
            Object.keys(this.fieldToKeys(field, value)).forEach(key => {
                value = constructor.processField(key, field[key]);

                // Store batch changes
                if (meta.batchChanges[key] !== value) {
                    meta.batchChanges[key] = value;
                    changed = true;
                }
            });
        }
        else {
            // Minor optimization for engine writing back a lot of changes
            if (meta.batchChanges[field] !== value) {
                meta.batchChanges[field] = value;
                changed = true;
            }
        }

        // Callers need to be able to detect changes
        if (changed) {
            this.generation++;
        }
    }

    inSet(field, value, silent, fromRelationUpdate, skipAccessors = false) {
        const
            me       = this,
            {
                data,
                meta,
                fieldMap,
                constructor
            }        = me,
            {
                prototype : myProto,
                childrenField
            }        = constructor,
            wasSet   = {},
            toSet    = me.fieldToKeys(field, value),
            keys     = Object.keys(toSet);
        let
            changed  = false;

        // Give a chance to cancel action before records updated.
        if (!silent && !me.triggerBeforeUpdate(toSet)) {
            return null;
        }

        for (let i = 0; i < keys.length; i++) {
            const key = keys[i];

            // Currently not allowed to set children in a TreeNode this way, will be ignored
            if (key === childrenField) {
                continue;
            }

            const
                field    = fieldMap[key],
                cmp      = field || OH,
                readOnly = field?.readOnly,
                mapping  = field?.dataSource ?? key,
                useProp  = !skipAccessors && !field && (key in myProto),
                oldValue = useProp ? me[mapping] : field?.complexMapping ? OH.getPath(data, mapping) : data[mapping],
                value    = constructor.processField(key, toSet[key]),
                val      = toSet[key] = { value },
                relation = me.getRelationConfig(key);

            if (!readOnly && !cmp.isEqual(oldValue, value)) {
                // Indicate to observers that data has changed.
                me.generation++;
                val.oldValue = oldValue;

                changed = true;

                // Update `modified` state which is used in sync request
                if (cmp.isEqual(me.meta.modified[key], value)) {
                    // Remove changes if values are the same
                    Reflect.deleteProperty(meta.modified, key);
                }
                else if (!me.ignoreBag) { // Private flag in engine, speeds initial commit up by not recording changes
                    // Cache its original value
                    if (!(key in meta.modified)) {
                        meta.modified[key] = oldValue;
                    }

                    if (val.oldValue === _undefined) {
                        Reflect.deleteProperty(val, 'oldValue');
                    }
                }

                // The wasSet object keys must be the field *name*, not its dataSource.
                wasSet[key] = val;

                me.applyValue(useProp, mapping, value, skipAccessors, field);

                // changing foreign key
                if (relation && !fromRelationUpdate) {
                    me.initRelation(relation);
                    me.stores.forEach(store => store.cacheRelatedRecord(me, value, relation.relationName, val.oldValue));
                }
            }
        }

        if (changed) {
            me.afterChange(toSet, wasSet, silent, fromRelationUpdate, skipAccessors);
        }

        return changed ? wasSet : null;
    }

    // Provided as a hook for SchedulingEngine to do what needs to be done which ever way a field value is changed
    applyValue(useProp, key, value, skipAccessors, field) {
        // If we don't have a field, but we have a property defined
        // eg, the fullDuration property defined in TaskModel, then
        // use the property
        if (useProp) {
            this[key] = value;
            return;
        }

        // Might be setting value of nested object
        if (!field && key.includes('.')) {
            const nestedName = key.split('.')[0];

            field = this.constructor.fieldMap[nestedName];
        }

        // Use complex mapping?
        if (field?.complexMapping) {
            OH.setPath(this.data, key, value);
        }
        // Otherwise, push the value through into the data.
        else {
            this.data[key] = value;
        }
    }

    // skipAccessors argument is used in the engine override
    afterChange(toSet, wasSet, silent, fromRelationUpdate, skipAccessors) {
        this.stores.forEach(store => {
            store.onModelChange(this, toSet, wasSet, silent, fromRelationUpdate);
        });
    }

    get isPersistable() {
        return true;
    }

    /**
     * True if this model has any uncommitted changes.
     * @property {Boolean}
     * @readonly
     * @category Editing
     */
    get isModified() {
        return Boolean(this.meta.modified && Object.keys(this.meta.modified).length > 0);
    }

    // TODO: Make this the behaviour of isModified?
    get hasPersistableChanges() {
        return this.isPersistable && !OH.isEmpty(this.rawModificationData);
    }

    /**
     * Returns true if this model has uncommitted changes for the provided field.
     * @param {String} fieldName Field name
     * @returns {Boolean} True if the field is changed
     */
    isFieldModified(fieldName) {
        return this.isModified && this.meta.modified[fieldName];
    }

    /**
     * Returns field value that should be persisted, or `undefined` if field is configured with `persist: false`.
     * @param {String|Core.data.field.DataField} nameOrField Name of the field to get value for, or its field definition
     * @private
     * @category Fields
     */
    getFieldPersistentValue(nameOrField) {
        const
            field = typeof nameOrField === 'string' ? this.getFieldDefinition(nameOrField) : nameOrField,
            name  = field?.name || nameOrField;

        let result;

        if (!field || field.persist) {
            result = this[name];
            // if serialize function is provided we use it to prepare the persistent value
            if (field?.serialize) {
                result = field.serialize(result, this);
            }
        }

        return result;
    }

    /**
     * Get a map of the modified fields in form of an object. The field *names* are used as the property names
     * in the returned object.
     * @property {Object}
     * @readonly
     * @category Editing
     */
    get modifications() {
        const data = this.rawModifications;

        if (data && Object.keys(data).length) {
            data[this.constructor.idField] = this.id;
        }

        return data;
    }

    get rawModifications() {
        const
            me = this,
            data = {};

        if (!me.isModified) {
            return null;
        }

        let keySet = false;

        Object.keys(me.meta.modified).forEach(key => {
            // TODO: isModified will report record as modified even if a modification wont be persisted here. Should it?
            const value = me.getFieldPersistentValue(key);
            if (value !== _undefined) {
                data[key] = value;
                keySet = true;
            }
        });

        return keySet ? data : null;
    }

    /**
     * Get a map of the modified fields in form of an object. The field *dataSources* are used as the property names
     * in the returned object.
     * @property {Object}
     * @readonly
     * @category Editing
     */
    get modificationData() {
        const data = this.rawModificationData;

        // If there are some persistable field changes, append record id
        if (data && Object.keys(data).length) {
            OH.setPath(data, this.constructor.getFieldDefinition(this.constructor.idField).dataSource, this.id);
        }

        return data;
    }

    get rawModificationData() {
        const
            me = this,
            { fieldMap } = me.constructor,
            data = {};

        if (!me.isModified) {
            return null;
        }

        let keySet = false;

        Object.keys(me.meta.modified).forEach(fieldName => {
            // TODO: isModified will report record as modified even if a modification wont be persisted here. Should it?
            const field = fieldMap[fieldName];

            // No field definition means there's no original dataSource to update
            if (field?.persist) {
                const value = me.getFieldPersistentValue(fieldName);

                if (value !== _undefined) {
                    OH.setPath(data, field.dataSource, value);
                    keySet = true;
                }
            }
        });

        return keySet ? data : null;
    }

    /**
     * Get persistable data in form of an object.
     * @property {Object}
     * @internal
     * @readonly
     * @category Editing
     */
    get persistableData() {
        const
            me   = this,
            data = {};

        me.fields.forEach(field => {
            const value = me.getFieldPersistentValue(field);

            if (value !== _undefined) {
                if (field?.complexMapping) {
                    OH.setPath(data, field.dataSource, value);
                }
                else {
                    data[field.dataSource] = value;
                }
            }
        });

        return data;
    }

    /**
     * True if this models changes are currently being committed.
     * @property {boolean}
     * @category Editing
     */
    get isCommitting() {
        return Boolean(this.meta.committing);
    }

    /**
     * Clear stored changes, used on commit. Does not revert changes.
     * @privateparam {Boolean} [removeFromStoreChanges] Update related stores modified collection or not
     * @privateparam {Boolean} [includeDescendants] Set true to clear store descendants
     * @category Editing
     */
    clearChanges(removeFromStoreChanges = true, includeDescendants = true) {
        const me = this,
            { meta } = me;

        meta.modified = {};
        meta.committing = false;

        if (removeFromStoreChanges) {
            me.stores.forEach(store => {
                store.modified.remove(me);
                store.added.remove(me);
                if (includeDescendants) {
                    const descendants = store.collectDescendants(me).all;
                    store.added.remove(descendants);
                    store.modified.remove(descendants);
                }
            });
        }
    }

    /**
     * Reverts changes in this back to their original values.
     * @category Editing
     */
    revertChanges() {
        this.set(this.meta.modified);
    }
    //endregion

    //region Id

    /**
     * Gets the records internalId. It is assigned during creation, guaranteed to be globally unique among models.
     * @property {Number}
     * @category Identification
     */
    get internalId() {
        return this._internalId;
    }

    /**
     * Returns true if the record is new and has not been persisted (and received a proper id).
     * @property {Boolean}
     * @readonly
     * @category Identification
     */
    get isPhantom() {
        return this.id === '' || this.id == null || this.hasGeneratedId;
    }

    get isModel() {
        return true;
    }

    /**
     * Checks if record has a generated id. New records are assigned a generated id (starting with _generated), which should be
     * replaced on commit.
     * @property {Boolean}
     * @category Identification
     */
    get hasGeneratedId() {
        return typeof this.id === 'string' && this.id.startsWith('_generated');
    }

    static generateId(text = this.$$name) {
        if (!this.generatedIdIndex) {
            this.generatedIdIndex = 0;
        }

        return `_generated${text}${++this.generatedIdIndex}`;
    }

    /**
     * Generates id for new record which starts with _generated.
     * @category Identification
     */
    generateId() {
        return this.constructor.generateId();
    }

    /**
     * Gets the id of specified model or the value if passed string/Number.
     * @param {Core.data.Model|String|Number} model
     * @returns {String|Number} id
     * @category Identification
     */
    static asId(model) {
        return model?.isModel ? model.id : model;
    }

    //endregion

    //region JSON

    /**
     * Get the records data as a json string.
     *
     * ```javascript
     * const record = new Model({
     *   title    : 'Hello',
     *   children : [
     *     ...
     *   ]
     * });
     *
     * const jsonString = record.json;
     *
     * //jsonString:
     * '{"title":"Hello","children":[...]}'
     * ```
     *
     * @member {String}
     * @category JSON
     */
    get json() {
        return StringHelper.safeJsonStringify(this);  // calls our toJSON() method
    }

    /**
     * Used by `JSON.stringify()` to correctly convert this record to json.
     *
     * In most cases no point in calling it directly.
     *
     * ```
     * // This will call `toJSON()`
     * const json = JSON.stringify(record);
     * ```
     *
     * If called manually, the resulting object is a clone of `record.data` + the data of any children:
     *
     * ```
     * const record = new Model({
     *   title    : 'Hello',
     *   children : [
     *     ...
     *   ]
     * });
     *
     * const jsonObject = record.toJSON();
     *
     * // jsonObject:
     * {
     *   title : 'Hello',
     *   children : [
     *       ...
     *   ]
     * }
     * ```
     *
     * @returns {Object}
     * @category JSON
     */
    toJSON() {
        const
            { children } = this,
            jsonData     = this.persistableData;

        if (children) {
            jsonData[this.constructor.childrenField] = children.map(c => c.toJSON());
        }

        return jsonData;
    }

    /**
     * Represent the record as a string, by default as a JSON string. Tries to use an abbreviated version of the objects
     * data, using id + name/title/text/label/description. If no such field exists, the full data is used.
     *
     * ```javascript
     * const record = new Model({ id : 1, name : 'Steve Rogers', alias : 'Captain America' });
     * console.log(record.toString()); // logs { "id" : 1, "name" : "Steve Rogers" }
     * ```
     *
     * @returns {string}
     */
    toString() {
        const
            me        = this,
            nameField = abbreviationFields.find(field => field in me.constructor.fieldMap),
            data      = nameField ? { [me.constructor.idField] : me.id, [nameField] : me[nameField] } : me.data;

        return StringHelper.safeJsonStringify(data);
    }

    //endregion

    //region Batch

    /**
     * True if this Model is currently batching its changes.
     * @property {Boolean}
     * @readonly
     * @category Editing
     */
    get isBatchUpdating() {
        return Boolean(this.batching);
    }

    /**
     * Begin a batch, which stores changes and commits them when the batch ends.
     * Prevents events from being fired during batch.
     * ```
     * record.beginBatch();
     * record.name = 'Mr Smith';
     * record.team = 'Golden Knights';
     * record.endBatch();
     * ```
     * Please note that you can also set multiple fields in a single call using {@link #function-set}, which in many
     * cases can replace using a batch:
     * ```
     * record.set({
     *   name : 'Mr Smith',
     *   team : 'Golden Knights'
     * });
     * ```
     * @category Editing
     */
    beginBatch() {
        const me = this;
        if (!me.batching) {
            me.batching = 0;
            me.meta.batchChanges = {};
        }
        me.batching++;
    }

    /**
     * End a batch, triggering events if data has changed.
     * @param {Boolean} [silent] Specify `true` to not trigger events. If event is recurring, occurrences won't be updated automatically.
     * @category Editing
    */
    endBatch(silent = false, skipAccessors = false) {
        const
            me = this,
            { parentIdField } = me.constructor;

        if (!me.batching) {
            return;
        }

        me.batching--;

        if (me.batching > 0) {
            return;
        }

        // Set pending batch changes
        if (!OH.isEmpty(me.meta.batchChanges)) {
            const batchChanges = { ...me.meta.batchChanges };
            me.meta.batchChanges = null;

            // Move to its new parent before applying the other changes.
            if (batchChanges[parentIdField]) {
                me.parentId = batchChanges[parentIdField];
                delete batchChanges[parentIdField];
            }

            me.set(batchChanges, _undefined, silent, false, skipAccessors);
            me.cancelBatch();
        }
    }

    /**
     * Cancels current batch operation. Any changes during the batch are discarded.
     * @category Editing
     */
    cancelBatch() {
        this.batching = null;
        this.meta.batchChanges = null;
    }

    //endregion

    //region Events

    /**
     * Triggers beforeUpdate event for each store and checks if changes can be made from event return value.
     * @param {Object} changes Data changes
     * @returns {Boolean} returns true if data changes are accepted
     * @private
     */
    triggerBeforeUpdate(changes) {
        return !this.stores.some(s => {
            if (s.trigger('beforeUpdate', { record : this, changes }) === false) {
                return true;
            }
        });
    }

    //endregion

    //region Additional functionality

    /**
     * Makes a copy of this model, assigning the specified id or a generated id and also allowing you to pass field values to
     * the created copy.
     *
     * ```
     * const record = new Model({ name : 'Super model', hairColor : 'Brown' });
     * const clone = new record.copy({ name : 'Super model clone' });
     * ```
     * @param {Number|String|Object} [newId] The id for the copied instance, or any field values to apply
     * (overriding the values from the source record). If no id provided, one will be auto-generated
     * @param {Number|String} [newId.id] DEPRECATED: Id to set, leave out to use generated id or specify false to also copy id
     * @param {Boolean} [newId.deep] DEPRECATED: true to also clone children (use separate argument)
     * @param {Boolean} [deep] True to also clone children
     * @returns {Core.data.Model} Copy of this model
     * @category Editing
     */
    copy(newId = null, params) {
        const
            me      = this,
            data    = { ...me.data },
            idField = me.constructor.idField;

        let deep, id;

        // Chrono model is adding more logic to copy and that logic should be manageable from arguments. So there is
        // option to pass object as a last argument to switch method behavior. Used internally only, shouldn't be public.
        deep = OH.isObject(params) ?  params.deep : params;

        // We do not include the id from this record
        if (newId !== false) {
            delete data[idField];
        }

        if (newId && typeof newId === 'object') {
            id = newId.id;

            if ('deep' in newId) {
                deep = newId.deep;

                VersionHelper.deprecate('Grid', '5.0.0', '`deep` attribute deprecated, in favor of a separate second parameter');
                // Only use id once to avoid collisions
                delete newId.id;
                delete newId.deep;
            }
            else {
                Object.assign(data, newId);
            }
        }
        else {
            id = newId;
        }

        // Iterate over instance children, because data may not reflect actual children state
        if (deep && me.children) {
            data.children = me.children.map(child => child.copy(undefined, params));
        }
        else {
            delete data.children;
            delete data.expanded;
        }

        // No id dataSource value in the data copy.
        // We can use the value from the 'id' property, but as a fallback, generate the id.
        if (data[idField] == null) {
            data[idField] = id || me.generateId(me.firstStore);
        }

        const copy = new me.constructor(data);

        // Store original record internal id to lookup from copy later
        copy.originalInternalId = me.internalId;

        return copy;
    }

    /**
     * Removes this record from all stores (and in a tree structure, also from its parent if it has one).
     * @param {Boolean} [silent] Specify `true` to not trigger events. If event is recurring, occurrences won't be updated automatically.
     * @category Editing
     */
    remove(silent = false) {
        const me = this,
            { parent } = this;

        // Remove from parent if we're in a tree structure.
        // This informs the owning store(s)
        if (parent) {
            parent.removeChild(me);
        }
        // Store handles remove
        else if (me.stores.length) {
            // Not sure what should happen if you try to remove a special row (group row for example), bailing out
            if (!me.isSpecialRow) {
                me.stores.forEach(s => s.remove(me, silent, false, true));
            }
        }
    }

    /**
     * Get the first store that this model is assigned to.
     * @returns {Core.data.Store}
     * @category Misc
     */
    get firstStore() {
        return this.stores.length > 0 && this.stores[0];
    }

    /**
     * Get a relation config by name, from the first store.
     * @param {String} name
     * @returns {Object}
     * @private
     * @category Relations
     */
    getRelationConfig(name) {
        // using first store for relations, might have to revise later..
        return this.firstStore?.modelRelations?.find(r => r.fieldName === name);
    }

    //endregion

    //region Validation

    /**
     * Check if record has valid data. Default implementation returns true, override in your model to do actual validation.
     * @returns {Boolean}
     * @category Editing
     */
    get isValid() {
        return true;
    }

    //endregion

    //region Store

    /**
     * Joins this record and any children to specified store, if not already joined.
     * @internal
     * @param {Core.data.Store} store Store to join
     * @category Misc
     */
    joinStore(store) {
        const me = this,
            { stores, unjoinedStores } = me;

        if (!stores.includes(store)) {
            super.joinStore && super.joinStore(store);
            store.register(me);
            stores.push(store);
            if (unjoinedStores.includes(store)) {
                unjoinedStores.splice(unjoinedStores.indexOf(store), 1);
            }
            me.isLoaded && me.children.forEach(child => child.joinStore(store));
            me.initRelations();

            if (store.tree && !me.isRoot) {
                me.instanceMeta(store.id).collapsed = !me.expanded;
            }
        }
    }

    /**
     * Unjoins this record and any children from specified store, if already joined.
     * @internal
     * @param {Core.data.Store} store Store to join
     * @param {Boolean} [isReplacing] `true` if this record is being replaced
     * @category Misc
     */
    unJoinStore(store, isReplacing = false) {
        const me = this,
            { stores, unjoinedStores } = me;

        if (stores.includes(store)) {
            store.unregister(me);
            me.children?.forEach(child => child.unJoinStore(store, isReplacing));
            stores.splice(stores.indexOf(store), 1);
            // keep the cord to allow removed records to reach the store when needed
            unjoinedStores.push(store);
            super.unJoinStore && super.unJoinStore(store, isReplacing);

            // remove from relation cache
            store.uncacheRelatedRecord(me);
        }
    }

    /**
     * Returns true if this record is contained in the specified store, or in any store if store param is omitted.
     * @internal
     * @param {Core.data.Store} store Store to join
     * @returns {Boolean}
     * @category Misc
     */
    isPartOfStore(store) {
        if (store) {
            return store.includes(this);
        }

        return this.stores.length > 0;
    }

    /**
     * Returns true if this record is not part of any store.
     * @property {Boolean}
     * @readonly
     * @internal
     */
    get isRemoved() {
        return !this.isPartOfStore();
    }

    //endregion

    //region Per instance meta

    /**
     * Used to set per external instance meta data. For example useful when using a record in multiple grids to store some state
     * per grid.
     * @param {String|Object} instanceOrId External instance id or the instance itself, if it has id property
     * @private
     * @category Misc
     */
    instanceMeta(instanceOrId) {
        const
            { meta } = this,
            id       = instanceOrId.id || instanceOrId;

        if (!meta.map) {
            meta.map = {};
        }

        return meta.map[id] || (meta.map[id] = {});
    }

    get isSpecialRow() {
        return this.meta.specialRow;
    }

    //endregion
}

Model._idField = 'id';
Model._internalIdCounter = 1;
Model._assignedIdField = false;

Model.exposeProperties();
