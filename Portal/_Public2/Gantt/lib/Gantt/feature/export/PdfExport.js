import SchedulerPdfExport from '../../../Scheduler/feature/export/PdfExport.js';
import SinglePageExporter from './exporter/SinglePageExporter.js';
import MultiPageExporter from './exporter/MultiPageExporter.js';
import MultiPageVerticalExporter from './exporter/MultiPageVerticalExporter.js';
import GridFeatureManager from '../../../Grid/feature/GridFeatureManager.js';

/**
 * @module Gantt/feature/export/PdfExport
 */

/**
 * Generates PDF/PNG files from the Gantt component.
 *
 * <img src="resources/images/gantt-export-dialog.png" style="max-width : 300px" alt="Gantt Export dialog">
 *
 * **NOTE:** This feature will make a fetch request to the server, posting
 * the HTML fragments to be exported. The {@link #config-exportServer} URL must be configured.
 *
 * ## Usage
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         pdfExport : {
 *             exportServer : 'http://localhost:8080' // Required
 *         }
 *     }
 * })
 *
 * // Opens popup allowing to customize export settings
 * gantt.features.pdfExport.showExportDialog();
 *
 * // Simple export
 * gantt.features.pdfExport.export({
 *     columns : gantt.columns.map(c => c.id) // Required, set list of column ids to export
 * }).then(result => {
 *     // Response instance and response content in JSON
 *     let { response, responseJSON } = result;
 * });
 * ```
 *
 * ## Loading resources
 *
 * If you face a problem with loading resources when exporting, the cause might be that the application and the export server are hosted on different servers.
 * This is due to [Cross-Origin Resource Sharing](https://developer.mozilla.org/en-US/docs/Web/HTTP/CORS) (CORS). There are 2 options how to handle this:
 * - Allow cross origin requests from the server where your export is hosted to the server where your application is hosted;
 * - Copy all resources keeping the folder hierarchy from the server where your application is hosted to the server where your export is hosted
 * and setup paths using {@link Grid.feature.export.PdfExport#config-translateURLsToAbsolute} config and configure the export server to give access to the path:
 *
 * ```javascript
 * const gantt = new Gantt({
 *     features : {
 *         pdfExport : {
 *             exportServer : 'http://localhost:8080',
 *             translateURLsToAbsolute : 'http://localhost:8080/resources' // '/resources' is hardcoded in WebServer implementation
 *         }
 *     }
 * })
 * ```
 *
 * ```javascript
 * // Following path would be served by this address: http://localhost:8080/resources/
 * node ./src/server.js -h 8080 -r web/application/styles
 * ```
 *
 * where `web/application/styles` is a physical root location of the copied resources, for example:
 *
 * <img src="resources/images/export-server-resources.png" style="max-width : 500px" alt="Export server structure with copied resources" />
 *
 * @extends Scheduler/feature/export/PdfExport
 * @typings Scheduler/feature/export/PdfExport -> Scheduler/feature/export/SchedulerPdfExport
 */
export default class PdfExport extends SchedulerPdfExport {
    static get $name() {
        return 'PdfExport';
    }

    static get defaultConfig() {
        return {
            exporters : [SinglePageExporter, MultiPageExporter, MultiPageVerticalExporter]
        };
    }
}

GridFeatureManager.registerFeature(PdfExport, false, 'Gantt');
