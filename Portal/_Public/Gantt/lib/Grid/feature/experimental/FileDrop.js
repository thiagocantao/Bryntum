import InstancePlugin from '../../../Core/mixin/InstancePlugin.js';
import GridFeatureManager from '../GridFeatureManager.js';
import EventHelper from '../../../Core/helper/EventHelper.js';

/**
 * @module Grid/feature/experimental/FileDrop
 */

/**
 * An experimental feature that lets users drop files on a Widget. The widget fires an event when a file is dropped onto it.
 * In the event, you get access to the raw files as strings, that were parsed by calling `readAsBinaryString`.
 *
 * NOTE: Currently only supports dropping one file at a time.
 *
 * @experimental
 * @extends Core/mixin/InstancePlugin
 * @classtype fileDrop
 * @feature
 */
export default class FileDrop extends InstancePlugin {
    static $name = 'FileDrop';

    construct(client, config) {
        const me = this;

        super.construct(client, config);

        // Setup event listeners for dragging files onto the grid element
        EventHelper.on({
            element   : client.element,
            thisObj   : me,
            drop      : me.onFileDrop,
            dragover  : me.onFileDragOver,
            dragenter : me.onFileDragEnter,
            dragleave : me.onFileDragLeave
        });
    }

    onFileLoad(domEvent) {

        this.client.trigger('fileDrop', {
            file : this.file, domEvent
        });
    }

    onFileDragEnter() {
        // Mouse over styling while dragging a file
        this.client.element.classList.add('b-dragging-file');
    }

    onFileDragOver(event) {
        event.preventDefault();
    }

    onFileDragLeave(event) {
        const { element } = this.client;

        if (event.relatedTarget && !element.contains(event.relatedTarget)) {
            this.client.element.classList.remove('b-dragging-file');
        }
    }

    onFileDrop(domEvent) {
        // Prevent default behavior (prevents the file from being opened)
        domEvent.preventDefault();
        const file = domEvent.dataTransfer.items[0].getAsFile();

        /**
         * Fired when a file is dropped on the widget element
         * @event fileDrop
         * @param {Grid.view.Grid} source The owning Grid instance
         * @param {DataTransferItem} file The dropped file descriptor
         * @param {DragEvent} domEvent The native DragEvent
         * @on-owner
         */
        this.client.trigger('fileDrop', { file, domEvent });

        this.onFileDragLeave(domEvent);
    }
}

GridFeatureManager.registerFeature(FileDrop, false, 'Grid');
