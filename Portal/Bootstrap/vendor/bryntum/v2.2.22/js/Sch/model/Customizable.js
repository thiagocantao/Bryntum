/**
@class Sch.model.Customizable
@extends Ext.data.Model

This class represent a model with customizable field names. Customizable fields are defined in separate
class config `customizableFields`. The format of definition is just the same as for usual fields:

    Ext.define('BaseModel', {
        extend      : 'Sch.model.Customizable',

        customizableFields  : [
            { name      : 'StartDate',  type    : 'date', dateFormat : 'c' },
            { name      : 'EndDate',    type    : 'date', dateFormat : 'c' }
        ],

        fields              : [
            'UsualField'
        ],

        getEndDate : function () {
            return "foo"
        }
    });

For each customizable field will be created getter and setter, using the camel-cased name of the field ("stable name"),
prepended with "get/set" respectively. They will not overwrite any existing methods:

    var baseModel   = new BaseModel({
        StartDate   : new Date(2012, 1, 1),
        EndDate     : new Date(2012, 2, 3)
    });

    // using getter for "StartDate" field
    // returns date for "2012/02/01"
    var startDate   = baseModel.getStartDate();

    // using custom getter for "EndDate" field
    // returns "foo"
    var endDate     = baseModel.getEndDate();

You can change the name of the customizable fields in the subclasses of the model or completely re-define them.
For that, add a special property to the class, name of this property should be formed as name of the field with lowercased first
letter, appended with "Field". The value of the property should contain the new name of the field.

    Ext.define('SubModel', {
        extend      : 'BaseModel',

        startDateField      : 'beginDate',
        endDateField        : 'finalizeDate',

        fields              : [
            { name      : 'beginDate',  type    : 'date', dateFormat : 'Y-m-d' },
        ]
    });

    var subModel       = new SubModel({
        beginDate       : new Date(2012, 1, 1),
        finalizeDate    : new Date(2012, 2, 3)
    });

    // name of getter is still the same
    var startDate   = subModel.getStartDate();

In the example above the `StartDate` field was completely re-defined to the `beginDate` field with different date format.
The `EndDate` has just changed its name to "finalizeDate". Note, that getters and setters are always named after "stable"
field name, not the customized one.
*/

// Don't redefine the class, which will screw up instanceof checks etc
if (!Ext.ClassManager.get("Sch.model.Customizable")) {

    Ext.define('Sch.model.Customizable', {
        extend      : 'Ext.data.Model',

        // Defined in subclasses, this prevents the default 'id' from being added to the fields collection
        idProperty  : null,

        /**
         * @cfg {Array} customizableFields
         *
         * The array of customizale fields definitions.
         */
        customizableFields      : null,

        // @private
        // Keeps temporary state of the previous state for a model, but is only available
        // when a model has changed, e.g. after 'set' or 'reject'. After those operations are completed, this property is cleared.
        previous                : null,

        constructor             : function() {
            // Sencha Touch requires the return value to be returned, hard crash without it
            var retVal = this.callParent(arguments);

            // Ext JS 5 adoptions, 'modified' is no longer created by default
            this.modified = this.modified || {};

            return retVal;
        },

        onClassExtended : function (cls, data, hooks) {
            var onBeforeCreated = hooks.onBeforeCreated;

            hooks.onBeforeCreated = function (cls, data) {
                onBeforeCreated.apply(this, arguments);

                var proto                   = cls.prototype;
                var isExt5                  = Ext.versions.extjs && Ext.versions.extjs.isGreaterThanOrEqual('5.0');

                if (!proto.customizableFields) {
                    return;
                }

                // combining our customizable fields with ones from superclass
                // our fields goes after fields from superclass to overwrite them if some names match
                proto.customizableFields    = (cls.superclass.customizableFields || []).concat(proto.customizableFields);

                var customizableFields      = proto.customizableFields;

                // collect fields here, overwriting old ones with new
                var customizableFieldsByName    = {};

                Ext.Array.each(customizableFields, function (field) {
                    // normalize to object
                    if (typeof field == 'string') field = { name : field };

                    customizableFieldsByName[ field.name ] = field;
                });

                // already processed by the Ext.data.Model `onBeforeCreated`
                var fields                  = proto.fields;


                // Ext JS 5
                if (Ext.isArray(fields)) {
                    var coll = new Ext.util.MixedCollection();

                    for (var i = 0; i < fields.length; i++) {
                        coll.add(fields[i].name, fields[i]);
                    }

                    fields = coll;
                }

                var toRemove                = [];

                fields.each(function (field) {
                    if (field.isCustomizableField) toRemove.push(field);
                });

                fields.removeAll(toRemove);

                Ext.Object.each(customizableFieldsByName, function (name, customizableField) {
                    // mark all customizable fields with special property, to be able remove them later
                    customizableField.isCustomizableField     = true;

                    var stableFieldName     = customizableField.name || customizableField.getName();
                    var fieldProperty       = stableFieldName === 'Id' ? 'idProperty' : stableFieldName.charAt(0).toLowerCase() + stableFieldName.substr(1) + 'Field';
                    var overrideFieldName   = proto[ fieldProperty ];

                    var realFieldName       = overrideFieldName || stableFieldName;
                    var field;

                    if (fields.containsKey(realFieldName)) {
                        field = Ext.applyIf({ name : stableFieldName, isCustomizableField : true }, fields.getByKey(realFieldName));

                        // if user has re-defined some customizable field, mark it accordingly
                        // such fields weren't be inheritable though (won't replace the customizable field)
                        fields.getByKey(realFieldName).isCustomizableField = true;

                        // add it to our customizable fields list on the last position, so in the subclasses
                        // it will overwrite other fields with this name

                        if (isExt5) {
                            field = Ext.create('data.field.' + (field.type || 'auto'), field);
                        } else {
                            field = new Ext.data.Field(field);
                        }

                        customizableFields.push(field);
                    } else {
                        field = Ext.applyIf({ name : realFieldName, isCustomizableField : true }, customizableField);

                        if (isExt5) {
                            field = Ext.create('data.field.' + (field.type || 'auto'), field);
                        } else {
                            field = new Ext.data.Field(field);
                        }

                        // we create a new copy of the `customizableField` using possibly new name
                        fields.add(realFieldName, field);
                    }

                    var capitalizedStableName  = Ext.String.capitalize(stableFieldName);

                    // don't overwrite `getId` method
                    if (capitalizedStableName != 'Id') {
                        var getter              = 'get' + capitalizedStableName;
                        var setter              = 'set' + capitalizedStableName;

                        // overwrite old getters, pointing to a different field name
                        if (!proto[ getter ] || proto[ getter ].__getterFor__ && proto[ getter ].__getterFor__ != realFieldName) {
                            proto[ getter ] = function () {
                                return this.data[ realFieldName ];
                            };

                            proto[ getter ].__getterFor__   = realFieldName;
                        }

                        // same for setters
                        if (!proto[ setter ] || proto[ setter ].__setterFor__ && proto[ setter ].__setterFor__ != realFieldName) {
                            proto[ setter ] = function (value) {
                                return this.set(realFieldName, value);
                            };

                            proto[ setter ].__setterFor__   = realFieldName;
                        }
                    }
                });

                if (isExt5) {
                    proto.fields.splice(0, proto.fields.length);
                    proto.fields.push.apply(proto.fields, fields.items);
                }
            };
        },

        // Overridden to be able to track previous record field values
        set : function(fieldName, value) {
            var currentValue;
            this.previous = this.previous || {};

            if (arguments.length > 1) {
                currentValue = this.get(fieldName);

                // Store previous field value
                if (currentValue !== value) {
                    this.previous[fieldName] = currentValue;
                }
            } else {
                for (var o in fieldName) {
                    currentValue = this.get(o);

                    // Store previous field value
                    if (currentValue !== fieldName[o]) {
                        this.previous[o] = currentValue;
                    }
                }
            }
            this.callParent(arguments);

            if (!this.__editing) {
                delete this.previous;
            }
        },

        // Overridden to be able to clear the previous record field values. Must be done here to have access to the 'previous' object after
        // an endEdit call.
        beginEdit : function() {
            this.__editing = true;

            this.callParent(arguments);
        },

        cancelEdit : function() {
            this.callParent(arguments);

            this.__editing = false;
            delete this.previous;
        },

        // Overridden to be able to clear the previous record field values. Must be done here to have access to the 'previous' object after
        // an endEdit call.
        endEdit : function() {
            this.callParent(arguments);

            this.__editing = false;
            delete this.previous;
        },

        // Overridden to be able to track previous record field values
        reject : function () {
            var me = this,
                modified = me.modified,
                field;

            me.previous = me.previous || {};
            for (field in modified) {
                if (modified.hasOwnProperty(field)) {
                    if (typeof modified[field] != "function") {
                        me.previous[field] = me.get(field);
                    }
                }
            }
            me.callParent(arguments);

            // Reset the previous tracking object
            delete me.previous;
        }
    });
}
