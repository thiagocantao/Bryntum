/**
@class Sch.data.ResourceStore
 
This is a class holding the collection the {@link Sch.model.Resource resources} to be rendered into a {@link Sch.panel.SchedulerGrid scheduler panel}. 
Its a subclass of "Ext.data.Store" - a store with linear data presentation.

*/
Ext.define("Sch.data.ResourceStore", {
    extend  : 'Ext.data.Store',
    model   : 'Sch.model.Resource',
    config : { model : 'Sch.model.Resource' },
    
    mixins  : [
        'Sch.data.mixin.ResourceStore'
    ],

    constructor : function() {
        this.callParent(arguments);

        if (this.getModel() !== Sch.model.Resource && !(this.getModel().prototype instanceof Sch.model.Resource)) {
            throw 'The model for the ResourceStore must subclass Sch.model.Resource';
        }
    }
});