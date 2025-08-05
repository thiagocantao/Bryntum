import { hasPartOfProjectStoreMixin } from "../../store/mixin/PartOfProjectMixin.js";
export const PartOfProjectMixin = (base) => {
    class PartOfProjectMixin extends base {
        joinStore(store) {
            let joinedProject = false;
            // Joining a store that is not part of project (for example a chained store) should not affect engine
            if (hasPartOfProjectStoreMixin(store)) {
                const project = store.getProject();
                if (project && !this.getProject() && !this.isShadowed()) {
                    this.setProject(project);
                    joinedProject = true;
                }
            }
            super.joinStore(store);
            if (joinedProject)
                this.joinProject();
        }
        unJoinStore(store) {
            super.unJoinStore(store);
            const project = this.isShadowed() ? this.shadowedProject : this.getProject();
            if (hasPartOfProjectStoreMixin(store) && project && project === store.getProject()) {
                this.leaveProject();
                this.setProject(null);
                this.shadowedProject = null;
            }
        }
        /**
         * Template method, which is called when model is joining the project (through joining some store that
         * has already joined the project)
         */
        joinProject() {
            this.getGraph().addEntity(this);
        }
        /**
         * Template method, which is called when model is leaving the project (through leaving some store usually)
         */
        leaveProject() {
            if (!this.isShadowed()) {
                this.getGraph().removeEntity(this);
            }
        }
        /**
         * Shadows an entity from a project until {@link #function~unshadow unshadow()} method call.
         *
         * Shadowed entity do not affect the project, their atoms are excluded from the graph and thus do not take part
         * in the propagation process.
         */
        shadow() {
            const project = this.getProject();
            if (project) {
                this.leaveProject();
                this.setProject(null);
                this.shadowedProject = project;
            }
            return this;
        }
        /**
         * Unshadows entity preveosly shadowed by {@link #function~shadow shadow()} call.
         */
        unshadow() {
            if (this.shadowedProject) {
                this.setProject(this.shadowedProject);
                this.shadowedProject = null;
                this.joinProject();
            }
            return this;
        }
        /**
         * Checks if an entity has been shadowed
         */
        isShadowed() {
            return !!this.shadowedProject;
        }
        getProject() {
            return this.isShadowed() ? null : super.getProject();
        }
        calculateProject() {
            // const store = this.stores[0];
            // return store && store.getProject();
            const store = this.stores.find(s => hasPartOfProjectStoreMixin(s) && !!s.getProject());
            return store && store.getProject();
        }
        afterSet(field, value, silent, fromRelationUpdate, beforeResult, wasSet) {
            // When undoing old data is set directly to the data object bypassing
            // accessors, which puts atoms like constraintDate into outdated state.
            // Iterating over modified fields and updating required atoms manually
            if (wasSet && this.getProject() && this.getProject().getStm().isRestoring) {
                Object.keys(wasSet).forEach(key => {
                    const atom = this.$[key];
                    // touch atoms affected by undo operation
                    if (atom && atom.graph) {
                        atom.graph.markAsNeedRecalculation(atom);
                    }
                });
            }
            super.afterSet && super.afterSet(field, value, silent, fromRelationUpdate, beforeResult, wasSet);
        }
    }
    return PartOfProjectMixin;
};
