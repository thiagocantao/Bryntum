const examples = {
    'Power demos' : {
        items : [{
            folder  : 'advanced',
            title   : 'Advanced',
            updated : '5.3'
        }, {
            folder  : 'bigdataset',
            title   : 'Big data set',
            since   : '4.0',
            updated : '5.2.0'
        }, {
            folder : 'gantt-schedulerpro',
            title  : 'Gantt + Scheduler Pro',
            since  : '4.0'
        }, {
            folder : 'gantt-taskboard',
            title  : 'Gantt + TaskBoard',
            since  : '5.0'
        }]
    },
    Features : {
        items : [{
            folder : 'basic',
            title  : 'Basic setup'
        }, {
            folder  : 'aggregation-column',
            title   : 'Aggregation column',
            updated : '5.4.0'
        }, {
            folder  : 'fieldfilters',
            title   : 'Advanced filtering',
            since   : '5.2.0',
            updated : '5.4.0'
        }, {
            folder  : 'baselines',
            title   : 'Baselines',
            since   : '1.1.0',
            updated : '5.4.0'
        }, {
            folder : 'calendars',
            title  : 'Highlighting task non-working time',
            since  : '5.2.0'
        }, {
            folder : 'cellselection',
            title  : 'Cell selection',
            since  : '5.3.0'
        }, {
            folder : 'collapsible-columns',
            title  : 'Collapsible columns',
            since  : '5.2.0'
        }, {
            folder : 'criticalpaths',
            title  : 'Critical paths',
            since  : '1.1.0'
        }, {
            folder : 'early-render',
            title  : 'Early render vs legacy render',
            since  : '5.0'
        }, {
            folder : 'filterbar',
            title  : 'Filtering',
            since  : '5.1.4'
        }, {
            folder : 'fixed-columns',
            title  : 'Fixed Columns',
            since  : '5.2.2'
        }, {
            folder  : 'grouping',
            title   : 'Grouping',
            since   : '5.0',
            updated : '5.4.0'
        }, {
            folder : 'grid-sections',
            title  : 'Grid Sections',
            since  : '5.0.0'
        }, {
            folder : 'highlight-time-spans',
            title  : 'Highlight time spans',
            since  : '5.0'
        }, {
            folder : 'inactive-tasks',
            title  : 'Inactive tasks',
            since  : '4.3.0'
        }, {
            folder : 'indicators',
            title  : 'Indicators',
            since  : '2.1.0'
        }, {
            folder : 'infinite-scroll',
            title  : 'Infinite timeline scrolling',
            since  : '4.2.0'
        }, {
            folder : 'labels',
            title  : 'Labels'
        }, {
            folder : 'parent-area',
            title  : 'Parent area',
            since  : '5.0.0'
        }, {
            folder : 'pin-successors',
            title  : 'Pin successors during drag drop',
            since  : '5.0.0'
        }, {
            folder  : 'progressline',
            title   : 'Progress line',
            since   : '1.1.0',
            updated : '5.2.0'
        }, {
            folder : 'responsive',
            title  : 'Responsive'
        }, {
            folder : 'rollups',
            title  : 'Rollups',
            since  : '1.2.0'
        }, {
            folder : 'conflicts',
            title  : 'Scheduling conflict resolution popup',
            since  : '5.0.0'
        }, {
            folder : 'split-tasks',
            title  : 'Split tasks',
            since  : '5.2.0'
        }, {
            folder : 'static',
            title  : 'Static mode',
            since  : '4.2.7'
        }, {
            folder  : 'state',
            title   : 'Storing and restoring state',
            since   : '5.0.0',
            updated : '5.2.0'
        }, {
            folder : 'fill-ticks',
            title  : 'Stretch tasks to fill ticks',
            since  : '5.2.0'
        }, {
            folder : 'summary',
            title  : 'Summary feature',
            since  : '4.3.2'
        }, {
            folder  : 'timeranges',
            title   : 'Time Ranges',
            updated : '1.3'
        }, {
            folder : 'timezone',
            title  : 'Time zone support',
            since  : '5.3.0'
        }, {
            folder : 'undoredo',
            title  : 'Undo/Redo'
        }, {
            folder : 'versions',
            title  : 'Versions',
            since  : '5.3.0'
        }, {
            folder : 'wbs',
            group  : 'Features',
            title  : 'WBS',
            since  : '5.3.0'
        }]
    },
    'Export & import' : {
        items : [{
            folder : 'exporttoexcel',
            title  : 'Export to Excel'
        }, {
            folder : 'msprojectexport',
            title  : 'Export to MS Project',
            since  : '4.0'
        }, {
            folder  : 'msprojectimport',
            title   : 'Import MS Project & Primavera P6 files',
            updated : '5.3.6'
        }, {
            folder : 'export',
            title  : 'Export to PDF / PNG',
            since  : '2.0'
        }]
    },
    Customization : {
        items : [{
            folder : 'custom-header',
            title  : 'Custom time axis header',
            since  : '4.2.5'
        }, {
            folder  : 'custom-rendering',
            title   : 'Customizing task bar contents',
            since   : '4.0',
            updated : '4.1.1'
        }, {
            folder : 'resourceassignment',
            title  : 'Customizing the resource assignment picker',
            since  : '1.0.1'
        }, {
            folder : 'custom-taskbar',
            title  : 'Customized task bar',
            since  : '4.1.0'
        }, {
            folder  : 'taskeditor',
            title   : 'Customizing the task editor',
            updated : '5.4'
        }, {
            folder  : 'taskmenu',
            title   : 'Customizing the task menu',
            updated : '5.4'
        }, {
            folder : 'taskstyles',
            title  : 'Customizing the task styling',
            since  : '5.4'
        }, {
            folder  : 'tooltips',
            title   : 'Customized tooltips',
            since   : '1.1.4',
            updated : '4.3.3'
        }, {
            folder : 'localization',
            title  : 'Localization'
        }, {
            folder : 'theme',
            title  : 'Theme browser'
        }]
    },
    'Drag drop' : {
        items : [{
            folder  : 'drag-resources-from-grid',
            title   : 'Drag resources from a grid',
            since   : '4.3.4',
            updated : '5.3.0'
        }, {
            folder : 'drag-resources-from-utilization-panel',
            title  : 'Drag resources from utilization panel',
            since  : '5.1.5'
        }, {
            folder : 'drag-from-grid',
            title  : 'Drag unplanned tasks from a grid',
            since  : '4.3.1'
        }]
    },
    'Additional widgets' : {
        items : [{
            folder : 'resourcehistogram',
            title  : 'Resource histogram widget',
            since  : '4.0'
        }, {
            folder  : 'resourceutilization',
            title   : 'Resource utilization widget',
            since   : '5.0',
            updated : '5.4.0'
        }, {
            folder : 'timeline',
            title  : 'Timeline widget'
        }]
    },
    Integration : {
        items : [{
            folder : 'custom-taskmenu',
            title  : 'Replace the task menu',
            since  : '4.0'
        }, {
            folder : 'csp',
            title  : 'Include in CSP page',
            since  : '5.0.2'
        }, {
            folder : 'esmodule',
            title  : 'Include using EcmaScript module'
        }, {
            folder  : 'extjsmodern',
            title   : 'ExtJS Modern App integration',
            overlay : 'extjs',
            version : 'ExtJS 6.5.3',
            since   : '1.0.2'
        }, {
            folder  : 'php',
            overlay : 'php',
            title   : 'Backend in PHP',
            offline : true,
            updated : '5.3.2'
        }, {
            folder    : 'salesforce',
            title     : 'Integrate with Salesforce Lightning',
            globalUrl : 'https://bryntum-dev-ed.develop.lightning.force.com/lightning/n/BryntumGantt',
            since     : '4.0',
            updated   : '5.4.1',
            overlay   : 'salesforce'
        }, {
            folder : 'scripttag',
            title  : 'Include using a script tag'
        }, {
            folder : 'webcomponents',
            title  : 'Use as web component',
            since  : '1.0.2'
        }, {
            folder  : 'frameworks/aspnet',
            title   : 'ASP.NET',
            overlay : 'dotnet',
            offline : true,
            since   : '2.1.1'
        }, {
            folder  : 'frameworks/aspnetcore',
            title   : 'ASP.NET Core',
            overlay : 'dotnet',
            offline : true,
            since   : '2.1.1'
        }, {
            folder  : 'frameworks/webpack',
            title   : 'Custom build using WebPack',
            overlay : 'webpack',
            version : 'WebPack 4',
            since   : '1.2',
            offline : true
        }, {
            folder  : 'frameworks/ionic/ionic-4',
            title   : 'Integrate with Ionic',
            build   : true,
            overlay : 'ionic',
            version : 'Ionic 4',
            since   : '4.3.0'
        }]
    },
    'Angular examples' : {
        overlay : 'angular',
        tab     : 'angular',
        build   : true,
        items   : [{
            folder  : 'frameworks/angular/angular-11',
            title   : 'Inline data for Angular View Engine',
            version : 'Angular 11',
            since   : '5.3.3'
        }, {
            folder  : 'frameworks/angular/advanced',
            title   : 'Advanced',
            version : 'Angular 13',
            updated : '4.3.4'
        }, {
            folder  : 'frameworks/angular/baselines',
            title   : 'Baselines',
            version : 'Angular 13',
            since   : '5.1.0'
        }, {
            folder  : 'frameworks/angular/drag-from-grid',
            title   : 'Drag from a grid',
            version : 'Angular 15',
            since   : '5.3.1'
        }, {
            folder  : 'frameworks/angular/gantt-resource-utilization',
            title   : 'Gantt + Resource Utilization',
            version : 'Angular 14',
            since   : '5.1.0'
        }, {
            folder  : 'frameworks/angular/gantt-schedulerpro',
            title   : 'Gantt + Scheduler Pro',
            version : 'Angular 13',
            updated : '4.3.4',
            since   : '4.2.0'
        }, {
            folder  : 'frameworks/angular/inline-data',
            title   : 'Inline data',
            version : 'Angular 13',
            since   : '5.0.0'
        }, {
            folder  : 'frameworks/angular/pdf-export',
            title   : 'PDF/PNG export',
            version : 'Angular 15',
            since   : '2.0',
            updated : '5.3.3'
        }, {
            folder  : 'frameworks/angular/rollups',
            title   : 'Rollups',
            version : 'Angular 15',
            since   : '2.0.1',
            updated : '5.3.3'
        }, {
            folder  : 'frameworks/angular/taskeditor',
            title   : 'Task editor customization',
            version : 'Angular 13',
            updated : '4.3.4',
            since   : '1.0.2'
        }, {
            folder  : 'frameworks/angular/timeranges',
            title   : 'Time ranges',
            version : 'Angular 15',
            since   : '2.0.1',
            updated : '5.3.3'
        }, {
            folder  : 'frameworks/angular/undoredo',
            title   : 'Undo/Redo',
            version : 'Angular 13',
            since   : '5.1.0'
        }]
    },
    'React examples' : {
        overlay : 'react',
        tab     : 'react',
        build   : true,
        items   : [{
            folder  : 'frameworks/react/javascript/advanced',
            title   : 'Advanced',
            version : 'React 16'
        }, {
            folder  : 'frameworks/react/javascript/basic',
            title   : 'Basic setup',
            version : 'React 16',
            since   : '1.1.2',
            updated : '1.3'
        }, {
            folder  : 'frameworks/react/javascript/baselines',
            title   : 'Baselines',
            version : 'React 18',
            since   : '5.1.0'
        }, {
            folder  : 'frameworks/react/javascript/gantt-schedulerpro',
            title   : 'Gantt + Scheduler Pro',
            version : 'React 18',
            since   : '4.2.0',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/react/javascript/inline-data',
            title   : 'Inline data',
            version : 'React 17',
            since   : '5.0.0'
        }, {
            folder  : 'frameworks/react/javascript/pdf-export',
            title   : 'PDF/PNG export',
            version : 'React 16',
            since   : '2.0'
        }, {
            folder  : 'frameworks/react/javascript/resource-histogram',
            title   : 'Resource Histogram',
            version : 'React 17',
            since   : '4.1.1'
        }, {
            folder  : 'frameworks/react/javascript/rollups',
            title   : 'Rollups',
            version : 'React 16',
            since   : '2.0.1'
        }, {
            folder  : 'frameworks/react/javascript/taskeditor',
            title   : 'Task editor customization',
            version : 'React 16',
            since   : '2.0.1'
        }, {
            folder  : 'frameworks/react/javascript/timeline',
            title   : 'Timeline',
            version : 'React 17',
            since   : '4.1'
        }, {
            folder  : 'frameworks/react/javascript/timeranges',
            title   : 'Time ranges',
            version : 'React 16',
            since   : '2.0.1'
        }, {
            folder  : 'frameworks/react/javascript/undoredo',
            title   : 'Undo/Redo',
            version : 'React 18',
            since   : '5.1.0'
        }, {
            folder  : 'frameworks/react/javascript/gantt-redux',
            title   : 'Redux toolkit',
            version : 'React 18',
            since   : '5.2.5',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/react/typescript/basic',
            title   : 'Basic setup with TypeScript',
            version : 'React 16',
            since   : '1.1.3',
            updated : '5.0.3'
        }, {
            folder  : 'frameworks/react/typescript/sharepoint-fabric',
            title   : 'SharePoint Workbench with TypeScript',
            version : 'React 16',
            offline : true,
            since   : '4.0.4',
            updated : '5.3.0'
        }]
    },
    'React + Next.js examples' : {
        overlay : 'react',
        tab     : 'react',
        build   : true,
        items   : [{
            folder  : 'frameworks/react-nextjs/app-router',
            title   : 'Basic setup with NEXT.js app router',
            version : 'React 18 + Next.js 13',
            since   : '5.4.2'
        }, {
            folder  : 'frameworks/react-nextjs/pages-router',
            title   : 'Basic setup with NEXT.js pages router',
            version : 'React 18 + Next.js 13',
            since   : '5.4.2'
        }]
    },
    'Vue 3 examples' : {
        overlay : 'vue',
        tab     : 'vue',
        build   : true,
        items   : [{
            folder  : 'frameworks/vue-3/javascript/baselines',
            title   : 'Baselines',
            version : 'Vue 3',
            since   : '5.1.0',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/vue-3/javascript/gantt-schedulerpro',
            title   : 'Gantt + Scheduler Pro',
            version : 'Vue 3',
            since   : '4.2.0',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/vue-3/javascript/inline-data',
            title   : 'Inline data ',
            version : 'Vue 3',
            since   : '5.0.0',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/vue-3/javascript/simple',
            title   : 'Simple setup',
            version : 'Vue 3',
            since   : '4.1',
            updated : '5.3.0'
        }, {
            folder  : 'frameworks/vue-3/javascript/undoredo',
            title   : 'Undo/Redo',
            version : 'Vue 3',
            since   : '5.1.0',
            updated : '5.3.0'
        }]
    },
    'Vue 2 examples' : {
        overlay : 'vue',
        tab     : 'vue',
        build   : true,
        items   : [{
            folder  : 'frameworks/vue/javascript/advanced',
            title   : 'Advanced',
            version : 'Vue 2'
        }, {
            folder  : 'frameworks/vue/javascript/vue-renderer',
            title   : 'Cell Renderer',
            version : 'Vue 2',
            since   : '4.1'
        }, {
            folder  : 'frameworks/vue/javascript/gantt-schedulerpro',
            title   : 'Gantt + Scheduler Pro',
            version : 'Vue 2',
            since   : '4.2.0'
        }, {
            folder  : 'frameworks/vue/javascript/inline-data',
            title   : 'Inline data',
            version : 'Vue 2',
            since   : '5.0.0'
        }, {
            folder  : 'frameworks/vue/javascript/pdf-export',
            title   : 'PDF/PNG export',
            version : 'Vue 2',
            since   : '2.0'
        }, {
            folder  : 'frameworks/vue/javascript/rollups',
            title   : 'Rollups',
            version : 'Vue 2',
            since   : '2.0.1'
        }, {
            folder  : 'frameworks/vue/javascript/taskeditor',
            title   : 'Task editor customization',
            version : 'Vue 2',
            since   : '2.0.1'
        }, {
            folder  : 'frameworks/vue/javascript/timeranges',
            title   : 'Time ranges',
            version : 'Vue 2',
            since   : '2.0.1'
        }]
    }
};

// Flatten examples tree
window.examples = Object.entries(examples).map(([group, parent]) => parent.items.map(item => Object.assign(item, parent, {
    group,
    items : undefined
}))).flat();
