import GridFeatureManager from '../../Grid/feature/GridFeatureManager.js';
import AbstractTimeRanges from '../../Scheduler/feature/AbstractTimeRanges.js';

/**
 * @module Gantt/feature/ProjectLines
 */

/**
 * This feature draws two vertical lines in the schedule area, indicating project start/end dates.
 *
 * This feature is **enabled** by default
 *
 * <div class="external-example" data-file="guides/gettingstarted/basic.js"></div>
 * @extends Scheduler/feature/TimeRanges
 * @demo Gantt/basic
 * @classtype projectLines
 */
export default class ProjectLines extends AbstractTimeRanges {
    //region Config

    static get $name() {
        return 'ProjectLines';
    }

    static get defaultConfig() {
        return {
            showHeaderElements : true,
            cls                : 'b-gantt-project-line'
        };
    }

    //endregion

    //region Init

    // We must override the TimeRanges superclass implementation which ingests the client's project's
    // timeRangeStore. We implement our own store
    startConfigure() {}

    /**
     * Called when gantt is painted.
     * @private
     */
    onPaint() {
        const me = this;

        [me.startDateLine, me.endDateLine] = me.store.add([{
            name : me.L('L{Project Start}')
        }, {
            name : me.L('L{Project End}')
        }]);

        me.updateDateFromProject();

        me.client.project.on({
            name    : 'project',
            refresh : me.onProjectRefresh,
            thisObj : me
        });

        super.onPaint();
    }

    updateLocalization() {
        const me = this;

        if (me.client.rendered) {
            // Updating the store to use proper locale for labels
            me.startDateLine.name = me.L('L{Project Start}');
            me.endDateLine.name = me.L('L{Project End}');
        }
    }

    updateDateFromProject() {
        const { project } = this.client;

        this.startDateLine.startDate = project.startDate;
        this.endDateLine.startDate = project.endDate;
    }

    //endregion

    onProjectRefresh() {
        this.updateDateFromProject();
    }
}

GridFeatureManager.registerFeature(ProjectLines, true, 'Gantt');
