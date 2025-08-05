export const PartOfProjectGenericMixin = (base) => {
    class PartOfProjectGenericMixin extends base {
        calculateProject() {
            throw new Error("Implement me");
        }
        /**
         * The method to set the [[ProjectMixin]] instance, this entity belongs to.
         */
        setProject(project) {
            return this.project = project;
        }
        /**
         * The method to get the [[ProjectMixin]] instance, this entity belongs to.
         */
        getProject() {
            if (this.project)
                return this.project;
            return this.setProject(this.calculateProject());
        }
        /**
         * The method to get the `ChronoGraph` instance, this entity belongs to.
         */
        getGraph() {
            const project = this.getProject();
            return project && project.getGraph();
        }
        /**
         * Convenience method to get the instance of the event store in the [[ProjectMixin]] instance, this entity belongs to.
         */
        getEventStore() {
            const project = this.getProject();
            return project && project.eventStore;
        }
        /**
         * Convenience method to get the instance of the dependency store in the [[ProjectMixin]] instance, this entity belongs to.
         */
        getDependencyStore() {
            const project = this.getProject();
            return project && project.dependencyStore;
        }
        /**
         * Convenience method to get the instance of the assignment store in the [[ProjectMixin]] instance, this entity belongs to.
         */
        getAssignmentStore() {
            const project = this.getProject();
            return project && project.assignmentStore;
        }
        /**
         * Convenience method to get the instance of the resource store in the [[ProjectMixin]] instance, this entity belongs to.
         */
        getResourceStore() {
            const project = this.getProject();
            return project && project.resourceStore;
        }
        /**
         * Convenience method to get the instance of the calendar manager store in the [[ProjectMixin]] instance, this entity belongs to.
         */
        getCalendarManagerStore() {
            const project = this.getProject();
            return project && project.calendarManagerStore;
        }
        // EOF Store getters
        // Entity getters
        getEventById(id) {
            return this.getEventStore() && this.getEventStore().getById(id);
        }
        getDependencyById(id) {
            return this.getDependencyStore() && this.getDependencyStore().getById(id);
        }
        getResourceById(id) {
            return this.getResourceStore() && this.getResourceStore().getById(id);
        }
        getAssignmentById(id) {
            return this.getAssignmentStore() && this.getAssignmentStore().getById(id);
        }
        getCalendarById(id) {
            return this.getCalendarManagerStore() && this.getCalendarManagerStore().getById(id);
        }
    }
    return PartOfProjectGenericMixin;
};
