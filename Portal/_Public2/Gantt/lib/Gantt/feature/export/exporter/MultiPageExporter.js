import SchedulerMultiPageExporter from '../../../../Scheduler/feature/export/exporter/MultiPageExporter.js';
import GanttExporterMixin from './GanttExporterMixin.js';

export default class MultiPageExporter extends GanttExporterMixin(SchedulerMultiPageExporter) {

    static get $name() {
        return 'MultiPageExporter';
    }

}
