export const ChronoModelMixin = (base) => {
    class ChronoModelMixin extends base {
        construct(config, ...args) {
            // this is to force the fields creation, because we need all fields to be created
            // for the `this.getFieldDefinition()` to return correct result
            // @ts-ignore
            this.constructor.exposeProperties();
            const commonConfig = {};
            const chronoConfig = {};
            // Cache original data before we recreate the incoming data here.
            // @ts-ignore
            this.originalData = (config = config || {});
            for (let key in config) {
                const chronoField = this.$entity.getField(key);
                if (this.getFieldDefinition(key) && !chronoField || key == 'expanded' || key == 'children')
                    commonConfig[key] = config[key];
                else {
                    chronoConfig[key] = config[key];
                }
            }
            super.construct(commonConfig, ...args);
            Object.assign(this, chronoConfig);
        }
        copy(newId = null, proposed = true) {
            const copy = super.copy(newId);
            proposed && this.forEachFieldAtom((atom, fieldName) => {
                copy.$[fieldName].put(atom.get());
            });
            return copy;
        }
    }
    return ChronoModelMixin;
};
