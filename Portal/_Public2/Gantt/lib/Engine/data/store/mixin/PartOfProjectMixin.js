const hasMixin = Symbol('PartOfProjectStoreMixin');
export const PartOfProjectStoreMixin = (base) => {
    class PartOfProjectStoreMixin extends base {
        [hasMixin]() { }
        calculateProject() {
            // project is supposed to be provided for stores from outside
            return this.project;
        }
        loadData(data) {
            super.loadData(data);
            const project = this.getProject();
            project && project.trigger('storerefresh', { store: this });
        }
    }
    return PartOfProjectStoreMixin;
};
/**
 * Type guard
 */
export const hasPartOfProjectStoreMixin = (store) => Boolean(store && store[hasMixin]);
