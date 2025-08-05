
import Base from '../Base.js';

/**
 * @module Core/mixin/Identifiable
 */
const
    idCounts = Object.create(null),
    idTypes  = {
        string : 1,
        number : 1
    };

/**
 * A mixin which provides identifier services such as auto-creation of `id`s and registration and
 * lookup of instances by `id`.
 * @mixin
 * @internal
 */
export default Target => class Identifiable extends (Target || Base) {
    static get $name() {
        return 'Identifiable';
    }

    static get declarable() {
        return [
            'identifiable'
        ];
    }

    static get configurable() {
        return {
            /**
             * The id of this object.  If not specified one will be generated. Also used for lookups through the
             * static `getById` of the class which mixes this in. An example being {@link Core.widget.Widget}.
             *
             * For a {@link Core.widget.Widget Widget}, this is assigned as the `id` of the DOM
             * {@link Core.widget.Widget#config-element element} and must be unique across all elements
             * in the page's `document`.
             * @config {String}
             * @category DOM
             */
            id : ''
        };
    }

    static setupIdentifiable(cls, meta) {
        const { identifiable } = cls;

        identifiable.idMap = Object.create(null);

        Reflect.defineProperty(cls, 'identifiable', {
            get() {
                return identifiable;
            }
        });
    }

    doDestroy() {
        this.constructor.unregisterInstance(this);
        super.doDestroy();
    }

    /**
     * This is called when the `id` config is assigned (via the setter).
     * @param {String} id The new id being assigned.
     * @param {String} oldId The old id (previously assigned) or `null`.
     * @private
     */
    changeId(id, oldId) {
        const me = this;

        if (oldId) {
            me.hasGeneratedId = false;
            me.constructor.unregisterInstance(me);  // uses me.id
        }

        id = id || me.generateAutoId();
        me.constructor.registerInstance(me, id);

        return id;
    }

    /**
     * This method generates an id for this instance.
     * @returns {String}
     * @private
     */
    generateAutoId() {
        this.hasGeneratedId = true;

        return this.constructor.generateId(`b-${this.$$name.toLowerCase()}-`);
    }

    /**
     * Generate a new id, using an internal counter and a prefix.
     * @param {String} prefix Id prefix
     * @returns {String} Generated id
     */
    static generateId(prefix = 'generatedId') {
        // This produces "b-foo-1, b-foo-2, ..." for each prefix independently of the others. In other words, it makes
        // id's more stable since the counter is on a per-class basis.
        return prefix + (idCounts[prefix] = (idCounts[prefix] || 0) + 1);
    }

    static registerInstance(instance, instanceId = instance.id) {
        const
            me        = this,
            { idMap } = me.identifiable;

        // Code editor sets `disableThrow` to not get conflicts when loading the same module again
        if (instanceId in idMap && !me.disableThrow) {
            throw new Error('Id ' + instanceId + ' already in use');
        }

        idMap[instanceId] = instance;
    }

    /**
     * Unregister Identifiable instance, normally done on destruction
     * @param {Object} instance Object to unregister
     */
    static unregisterInstance(instance) {
        const idMap = this.identifiable.idMap;

        // ID may be passed, for example if the instance is destroyed and can no longer yield an id.
        if (idTypes[typeof instance]) {
            delete idMap[instance];
        }
        else {
            // Have to check for identity in case another instance by the same id has been created.
            // Allow that to be overridden. Stores have always just evicted the previous owner of their IDs
            if (idMap[instance.id] === instance) {
                delete idMap[instance.id];
            }
        }
    }

    static getById(id) {
        const idMap = this.identifiable.idMap;

        if (idMap) {
            return idMap[id];
        }
    }

    static get registeredInstances() {
        const idMap = this.identifiable.idMap;

        return idMap ? Object.values(idMap) : [];
    }
};
