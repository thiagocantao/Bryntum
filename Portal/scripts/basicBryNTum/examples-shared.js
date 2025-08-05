


Sch.gantt.plugins.Bez856Menu = Ext.extend(Ext.menu.Menu, {
    
    plain : true,
    
    /**
     * @cfg {String} triggerEvent
     * The event upon which the menu shall be shown. Defaults to 'taskcontextmenu', meaning the menu is shown when right-clicking a task.
     * You can change this to 'rowcontextmenu' if you want the menu to be shown when right clicking the left grid area too.
     */
    triggerEvent : 'taskcontextmenu',

    texts : {
        newTaskText : 'New task', 
        newMilestoneText : 'New milestone', 
        deleteTask : 'Delete task',
        editLeftLabel : 'Edit left label',
        editRightLabel : 'Edit right label',
        add : 'Add...',
        deleteDependency : 'Delete dependency...',
        addTaskAbove : 'Task above',
        addTaskBelow : 'Task below',
        addMilestone : 'Milestone',
        addSubtask :'Sub-task',
        addSuccessor : 'Successor',
        addPredecessor : 'Predecessor'
    },
    
    buildMenuItems : function() {
        var t = this.texts,
            actions = this.actions;

        Ext.apply(this, {
            items : [
                {
                    handler : actions.deleteTask,
                    scope : this,
                    text : t.deleteTask
                },
                {
                    handler : actions.editLeftLabel,
                    scope : this,
                    text : t.editLeftLabel
                },
                {
                    handler : actions.editRightLabel,
                    scope : this,
                    text : t.editRightLabel
                },
                {
                    scope : this,
                    text : t.add,
                    menu : {
                        plain : true,
                        items : [
                            {
                                handler : actions.addTaskAbove,
                                scope : this,
                                text : t.addTaskAbove
                            },
                            {
                                handler : actions.addTaskBelow,
                                scope : this,
                                text : t.addTaskBelow
                            },
                            {
                                handler : actions.addMilestone,
                                scope : this,
                                text : t.addMilestone
                            },
                            {
                                handler : actions.addSubtask,
                                scope : this,
                                text : t.addSubtask
                            },
                            {
                                handler : actions.addSuccessor,
                                scope : this,
                                text : t.addSuccessor
                            },
                            {
                                handler : actions.addPredecessor,
                                scope : this,
                                text : t.addPredecessor
                            }
                        ]
                    }
                },
                {
                    text : t.deleteDependency,
                    menu : { 
                        plain : true,
                        listeners : {
                            beforeshow : {
                                fn : this.populateDependencyMenu,
                                scope : this
                            },
                    
                            // highlight dependencies on mouseover of the menu item
                            mouseover : {
                                fn : this.onDependencyMouseOver,
                                scope : this
                            },
                    
                            // unhighlight dependencies on mouseout of the menu item
                            mouseout : {
                                fn : this.onDependencyMouseOut,
                                scope : this
                            }
                        }
                    }
                }
            ]
        });
    },

    initComponent : function() {
        this.buildMenuItems();
        
        Sch.gantt.plugins.Bez856Menu.superclass.initComponent.call(this);
    },
    
    populateDependencyMenu : function(menu) {
        var g = this.grid, 
            taskStore = g.getTaskStore(),
            dependencies = g.getDependenciesForTask(this.rec),
            depStore = g.dependencyStore;
        
        menu.removeAll();

        if (dependencies.length === 0) {
            return false;
        }
        
        dependencies.each(function(dep) {
            var fromId = dep.get('From'),
                task = g.getTaskById(fromId == this.rec.id ? dep.get('To') : fromId),
                text = Ext.util.Format.ellipsis(task.get('Name'), 30);
            
            menu.addMenuItem({
                depId : dep.id,
                text : text,
                scope : this,
                handler : function(menuItem) {
                    depStore.removeAt(depStore.indexOfId(menuItem.depId));
                }
            });
        }, this);
    },
    
    onDependencyMouseOver : function(menu, e, item) {
        if (item) {
            this.grid.highlightDependency(item.depId);
        }
    },
    
    onDependencyMouseOut : function(menu, e, item) {
        if (item) {
            this.grid.unhighlightDependency(item.depId);
        }
    },
        
    cleanUp : function() {
        if (this.menu) {
            this.menu.destroy();
        }
    },
    
    init:function(grid) {
        grid.on('destroy', this.cleanUp, this);
        grid.on(this.triggerEvent, this.onActivate, this);
        this.grid = grid;
    },
    
    onActivate : function(g, rec, e) {
        e.stopEvent();

        if (typeof rec === 'number') {
            rec = this.grid.getTaskStore().getAt(rec);
        }

        this.rec = rec;
        this.showAt(e.getXY());
    },
    
    copyTask : function(originalRecord) {
        var s = originalRecord.store,
            newTask = new s.recordType({
                PercentDone : 0,
                Name : this.texts.newTaskText,
                StartDate : originalRecord.get('StartDate'),
                EndDate : originalRecord.get('EndDate'),
                ParentId : originalRecord.get('ParentId'),
                IsLeaf : true
            });
        return newTask;
    },
    
    actions : {
        deleteTask: function() {
            this.grid.getTaskStore().remove(this.rec);
        },
        
        editLeftLabel : function() {
            this.grid.editLeftLabel(this.rec);
        },
            
        editRightLabel : function() {
            this.grid.editRightLabel(this.rec);
        },
            
        addTaskAbove : function() {
            var s = this.rec.store,
                newTask = this.copyTask(this.rec);
                
            s.insert(s.indexOf(this.rec), newTask);
        },
            
        addTaskBelow : function() {
            var s = this.rec.store,
                newTask = this.copyTask(this.rec), 
                insertIndex;
            
            if (s.isLeafNode(this.rec)) {
                insertIndex = s.indexOf(this.rec) + 1;
            } else {
                var sibling = s.getNodeNextSibling(this.rec);
                
                insertIndex = sibling ? s.indexOf(sibling) : s.getCount();
            }
            s.insert(insertIndex, newTask);
        },
            
        addSubtask : function() {
            var s = this.rec.store,
                newTask = this.copyTask(this.rec);
            
            this.rec.set(this.rec.store.leaf_field_name, false);
            s.addToNode(this.rec, newTask);
            s.expandNode(this.rec);
        },
            
        addSuccessor : function() {
            var s = this.rec.store,
                depStore = this.grid.dependencyStore,
                index = this.rec.store.indexOf(this.rec),
                newTask = this.copyTask(this.rec);
            
            newTask.set('StartDate', this.rec.get('EndDate'));
            newTask.set('EndDate', Sch.util.Date.add(this.rec.get('EndDate'), Sch.util.Date.DAY, 1));
            
            s.insert(index + 1, newTask);
            depStore.add(new depStore.recordType({
                From : this.rec.id,
                To : newTask.id,
                Type : Sch.gantt.Dependency.EndToStart
            })
            );
        },
            
        addPredecessor : function() {
            var s = this.rec.store,
                 depStore = this.grid.dependencyStore,
                 index = this.rec.store.indexOf(this.rec),
                 newTask = this.copyTask(this.rec),
                 newEnd = this.rec.get('StartDate');
            
            newTask.set('EndDate', newEnd);
            newTask.set('StartDate', Sch.util.Date.add(newEnd, Sch.util.Date.DAY, -1));
            
            s.insert(index, newTask);
            depStore.add(new depStore.recordType({
                From : newTask.id,
                To : this.rec.id,
                Type : Sch.gantt.Dependency.EndToStart
            })
            );
        },
            
        addMilestone : function() {
            var s = this.rec.store,
                newMilestone = this.copyTask(this.rec);
            index = this.rec.store.indexOf(this.rec);
            newMilestone.set('StartDate', newMilestone.get('EndDate'));    
            s.insert(index + 1, newMilestone);
        }
    }
}); 