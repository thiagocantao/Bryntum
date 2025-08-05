import Column from '../../Grid/column/Column.js';
import ColumnStore from '../../Grid/data/ColumnStore.js';
import EventHelper from '../../Core/helper/EventHelper.js';
import StringHelper from '../../Core/helper/StringHelper.js';
import VersionHelper from '../../Core/helper/VersionHelper.js';

/**
 * @module Scheduler/column/ResourceInfoColumn
 */

/**
 * Displays basic resource information. Defaults to showing image + name + event count, it is all configurable.
 * If a resource has no image, you can also provide an icon using `iconCls` in the data (you then need to specify
 * `image === false` in your data).
 * Be sure to specify {@link Scheduler.view.mixin.SchedulerEventRendering#config-resourceImagePath} to instruct the
 * column where to look for the images.
 * @externalexample scheduler/ResourceInfoColumn.js
 * @classType resourceInfo
 * @extends Grid/column/Column
 */
export default class ResourceInfoColumn extends Column {

    static get $name() {
        return 'ResourceInfoColumn';
    }

    static get type() {
        return 'resourceInfo';
    }

    static get fields() {
        return ['showEventCount', 'showRole', 'showImage', 'validNames', 'autoScaleThreshold'];
    }

    static get defaults() {
        return {
            /**
             * Show image. Looks for image name in fields on the resource in the following order: 'imageUrl', 'image',
             * 'name'. Set `showImage` to a field name to use a custom field. Set `Scheduler.resourceImagePath` to
             * specify where to load images from. If no extension found, defaults to
             * {@link Scheduler.view.mixin.SchedulerEventRendering#config-resourceImageExtension}.
             * @config {Boolean}
             * @default
             */
            showImage : true,

            /**
             * Show number of events assigned to the resource below the name.
             * @config {Boolean}
             * @default
             */
            showEventCount : true,

            /**
             * Show resource role below the name. Specify `true` to display data from the `role` field, or specify a field
             * name to read this value from.
             * @config {Boolean|String}
             * @default
             */
            showRole : false,

            /**
             * Valid image names. Set to `null` to allow all names.
             * @config {String[]}
             * @default
             */
            validNames : [
                'amit',
                'angelo',
                'arcady',
                'arnold',
                'celia',
                'chang',
                'dan',
                'dave',
                'emilia',
                'george',
                'gloria',
                'henrik',
                'hitomi',
                'jong',
                'kate',
                'lee',
                'linda',
                'lisa',
                'lola',
                'macy',
                'madison',
                'malik',
                'mark',
                'maxim',
                'mike',
                'rob',
                'steve'
            ],

            /**
             * Specify 0 to prevent the column from adapting its content according to the used row height, or specify a
             * a threshold (row height) at which scaling should start.
             * @config {Number}
             * @default
             */
            autoScaleThreshold : 40,

            field      : 'name',
            htmlEncode : false,
            width      : 140,
            cellCls    : 'b-resourceinfo-cell',

            editor : VersionHelper.isTestEnv ? false : 'text'
        };
    }

    construct() {
        const me = this;

        super.construct(...arguments);

        if (me.grid.isPainted) {
            me.addErrorListener();
        }
        else {
            me.grid.on({
                paint   : me.addErrorListener,
                thisObj : me,
                once    : true
            });
        }
    }

    getImageURL(imageName) {
        return StringHelper.joinPaths([this.grid.resourceImagePath || '', imageName || '']);
    }

    addErrorListener() {
        EventHelper.on({
            element  : this.grid.element,
            delegate : '.b-resource-image',
            error    : event => this.setDefaultResourceImage(event.target),
            capture  : true
        });
    }

    setDefaultResourceImage(target) {
        const { defaultResourceImageName } = this.grid;

        if (defaultResourceImageName) {
            const defaultURL = this.getImageURL(defaultResourceImageName);
            // Set image to defaultURL if it is not already set
            if (target.src && !target.src.endsWith(defaultURL.replace(/^[./]*/gm, ''))) {
                target.src = defaultURL;
            }
        }
    }

    template(record) {
        const me        = this,
            {
                showImage,
                showRole,
                showEventCount,
                grid : scheduler
            }            = me,
            {
                timeAxis,
                resourceImageExtension,
                defaultResourceImageName
            }            = scheduler,
            roleField    = typeof showRole === 'string' ? showRole : 'role',
            count        = record.eventStore.getEvents({
                includeOccurrences : scheduler.enableRecurringEvents,
                resourceRecord     : record,
                startDate          : timeAxis.startDate,
                endDate            : timeAxis.endDate
            }).length,
            value     = record.get(me.field);

        let imageUrl;

        if (showImage && record.image !== false) {
            if (record.imageUrl) {
                imageUrl = record.imageUrl;
            }
            else {
                // record.image supposed to be a file name, located at resourceImagePath
                const imageName = typeof showImage === 'string'
                    ? showImage
                    : (record.image || value && (value.toLowerCase() + resourceImageExtension) || defaultResourceImageName);

                imageUrl = me.getImageURL(imageName);

                // Image name should have an extension
                if (!imageName.includes('.')) {
                    // If validNames is specified, check that imageName is valid
                    if (!me.validNames || me.validNames.includes(imageName)) {
                        imageUrl += resourceImageExtension;
                    }
                    // If name is not valid, use generic image
                    else if (!record.iconCls) {
                        imageUrl = me.getImageURL(defaultResourceImageName);
                    }
                }
            }
        }

        return {
            class    : 'b-resource-info',
            children : [
                imageUrl ? {
                    tag       : 'img',
                    draggable : 'false',
                    class     : 'b-resource-image',
                    src       : imageUrl
                } : null,
                record.iconCls ? {
                    tag       : 'i',
                    className : `b-resource-icon ${record.iconCls}`
                } : null,
                {
                    tag      : 'dl',
                    children : [
                        {
                            tag  : 'dt',
                            html : value
                        },
                        showRole ? {
                            tag   : 'dd',
                            class : 'b-resource-role',
                            html  : record[roleField]
                        } : null,

                        showEventCount ? {
                            tag   : 'dd',
                            class : 'b-resource-events',
                            html  : me.L('L{eventCountText}', count)
                        } : null
                    ]
                }
            ]
        };

    }

    defaultRenderer({ grid, record, cellElement, value, isExport }) {
        let result;

        if (record.isSpecialRow) {
            result = '';
        }
        else if (isExport) {
            result = value;
        }
        else {
            if (this.autoScaleThreshold && grid.rowHeight < this.autoScaleThreshold) {
                cellElement.style.fontSize = (grid.rowHeight / 40) + 'em';
            }
            else {
                cellElement.style.fontSize = '';
            }

            result = this.template(record);
        }

        return result;
    }
}

ColumnStore.registerColumnType(ResourceInfoColumn);
