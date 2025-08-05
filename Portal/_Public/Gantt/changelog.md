# Bryntum Gantt version history

## 5.5.0 - 2023-07-31

* This release is a replacement for the 5.4.3 patch release. It was changed to a minor version because of some larger
  changes behind the scenes to pave the way for future support for live updates in Scheduler Pro and Gantt.

### FEATURES / ENHANCEMENTS

* We have refactored a few features and changed their prototype chain. If you are importing those features directly, or
  are extending them, you need to use correct base class and import path:
  `Grid/feature/RowReorder` ->  `Scheduler/feature/RowReorder`,
  `Grid/feature/CellEdit` -> `SchedulerPro/feature/CellEdit`,
  `Scheduler/feature/Dependencies` -> `SchedulerPro/feature/Dependencies`

### BUG FIXES

* [#7154](https://github.com/bryntum/support/issues/7154) - `FillTicks` and `Indicator` placement is not correct after setting start date
* [#7187](https://github.com/bryntum/support/issues/7187) - WBS value is not completely committed after initial commit
* [#7221](https://github.com/bryntum/support/issues/7221) - [VUE] Vue vite app doesn't compile with Bryntum vue wrappers
* [#7241](https://github.com/bryntum/support/issues/7241) - Should not show task tooltip after drag drop
* [#7242](https://github.com/bryntum/support/issues/7242) - Whiteouts while scrolling task into view

## 5.4.2 - 2023-07-26

### FEATURES / ENHANCEMENTS

* Added `TaskStore` config option `forceWbsOrderForChanges` to control task order during sync
* [REACT] Added new React Next.js demos showing App Router and Pages Router ways of creating applications. Demos are
  located in `examples/frameworks/react-nextjs/app-router` and `examples/frameworks/react-nextjs/pages-router` folders

### BUG FIXES

* [#4376](https://github.com/bryntum/support/issues/4376) - `newTaskDefaults` not applied when pressing Enter on last row
* [#5980](https://github.com/bryntum/support/issues/5980) - Outdent records changed nodes in the reversed order
* [#7024](https://github.com/bryntum/support/issues/7024) - Possible Memory Leak: Tasks of paired Gantt cycled active/inactive may cause the leak
* [#7135](https://github.com/bryntum/support/issues/7135) - Outdented nodes appear in wrong position within new parent's children on remote synced client
* [#7154](https://github.com/bryntum/support/issues/7154) - FillTicks and Indicator Placement is not correct after setting start date
* [#7171](https://github.com/bryntum/support/issues/7171) - Task record is inserted into wrong position in the tree
* [#7186](https://github.com/bryntum/support/issues/7186) - Returning from `tooltipTemplate` doesn't make any changes to the UI

## 5.4.1 - 2023-07-13

### FEATURES / ENHANCEMENTS

* We have created a public repository to showcase Salesforce demos. All previous demos are merged into one Lightning
  Application which is easy to install to a new scratch org. You can find more information in updated guides and in this
  repository: https://github.com/bryntum/bryntum-salesforce-showcase#bryntum-salesforce-showcase
* We have created a public Salesforce org where this app is preinstalled. You can find link to it and login credentials
  on the updated examples page
* Scheduler `beforeDependencyEdit` event now supports async handlers ([#7109](https://github.com/bryntum/support/issues/7109))

### BUG FIXES

* [#4869](https://github.com/bryntum/support/issues/4869) - [REACT] StateTrackingManager configuration triggering an error
* [#6077](https://github.com/bryntum/support/issues/6077) - [TypeScript] `Model` constructors should allow second param
* [#6347](https://github.com/bryntum/support/issues/6347) - Store `idChange` event not fired
* [#6977](https://github.com/bryntum/support/issues/6977) - Document `dataReady` event's `records` argument
* [#7056](https://github.com/bryntum/support/issues/7056) - Updating tasks in `onBeforeTaskEdit` is not possible if returning false
* [#7089](https://github.com/bryntum/support/issues/7089) - Buggy positioning of dependencies when resizing
* [#7101](https://github.com/bryntum/support/issues/7101) - `TaskNonWorkingTime` renders wrong on expand / collapse tree
* [#7130](https://github.com/bryntum/support/issues/7130) - Updating tasks on `beforeTaskEdit` event provide weird results
* [#7143](https://github.com/bryntum/support/issues/7143) - Drag and drop functionality doesn't work for task bars in hidden parent element
* [#7147](https://github.com/bryntum/support/issues/7147) - Constraint data gets cleared when loading

## 5.4.0 - 2023-06-30

### FEATURES / ENHANCEMENTS

* This release introduces a new `TimelineHistogram` class which implements a grid with histogram charts displayed
  for rows in the time axis section. Please check the new "Timeline histogram" guide for more details
* The release also includes refactored `ResourceUtilization` and `ResourceHistogram` views which now better support
  `TreeGroup` and `Group` features, and provide some additional APIs for customization. For more details please check
  the new "Resource histogram" and "Resource utilization" guides and see the updated  `resourcehistogram` and
  `resourceutilization` examples
* Gantt now supports setting the `eventColor` field on a Task to a pre-defined named color (`red`, `green`, `blue` etc.)
  or a CSS color value. This changes the background color of the rendered task bar. It is also possible to configure the
  Gantt with a default `eventColor` that applies to all tasks, overriding the default coloring ([#857](https://github.com/bryntum/support/issues/857))
* The `TaskCopyPaste` feature has been enhanced to use a page-global internal clipboard and also supports the browser's
  native Clipboard API if accessible. This means that it is possible to copy and paste tasks between multiple instances
  of Gantt or other Grid-based components. It is also possible to copy a task and paste it inside a Spreadsheet app like
  ([#4169](https://github.com/bryntum/support/issues/4169))
* Setting scheduling direction is now supported on the task level. See the docs for `direction` field on the Task model
  for more details ([#2131](https://github.com/bryntum/support/issues/2131))
* `Widget` has a new config, `maximizeOnMobile` which takes effect only on `floating` widgets on a mobile device. It
  causes the widget to be maximized instead of positioned in order to accommodate the mobile virtual keyboard. This will
  make event editing much easier to use on mobile devices ([#6522](https://github.com/bryntum/support/issues/6522))
* On mobile devices, `type : 'checkbox'` is rendered as a `slidetoggle` widget. The API and value is the same, it is
  just a more appropriate UI for the platform
* We have also added default editors for the `eventColor` field. There is one in the `TaskMenu` feature's context menu
  and one in the `TaskEdit` feature's task editing panel. Just set `showTaskColorPickers` to `true` and the editors will
  appear ([#3157](https://github.com/bryntum/support/issues/3157))
* There is also a new `EventColorColumn` which can be added to any Gantt. It renders a colored element which the user
  can click and select a new color for each task ([#3537](https://github.com/bryntum/support/issues/3537))
* [ANGULAR] `taskeditor` demo has been updated to show use of `showTaskColorPickers` Gantt configuration option
* [REACT] `taskeditor` demo has been updated to show use of `showTaskColorPickers` Gantt configuration option
* [VUE] `taskeditor` demo has been updated to show use of `showTaskColorPickers` Gantt configuration option
* For a slightly better docs experience for most users, the docs browser now by default hides some more obscure APIs
  normally only used when implementing own widgets and features. Advanced users in need of these APIs can still opt in
  to see them using the `Show` menu in the docs browser
* Added a set of baseline columns: "Baseline start" (`BaselineStartDateColumn`), "Baseline finish"
  (`BaselineEndDateColumn`), "Baseline duration" (`BaselineDurationColumn`), "Baseline start variance"
  (`BaselineStartVarianceColumn`), "Baseline end variance" (`BaselineEndVarianceColumn`) & "Baseline duration variance"
  (`BaselineDurationVarianceColumn`). Used in the updated `baselines` demo

### API CHANGES

* Because of the support for `eventColor` in Gantt (mentioned above), changes to the CSS classes coloring Gantt Tasks
  has been made. If your app customizes the task bar's background color, we would recommend you to take a look at the
  result after upgrading to this version. The most notable changes are that `$gantt-task-hover-background-color` and
  `$gantt-task-parent-hover-background` has been removed. Instead, hovering now adds a `linear-gradient` on top of the
  background color. Please read the Upgrade guide for more information
* The `TaskCopyPaste` feature's `copyRow` and `pasteRow` has been made asynchronous due to the enhancements mentioned
  above
* The `TaskCopyPaste` feature's `beforeCopy` and `beforePaste` events are now asynchronously preventable
* The `ScaleColumn` class has been moved from Pro to regular Scheduler classes. This should not affect your application
  unless it imports the class from its individual file (not the case for the vast majority of customers).
  The distribution still includes `SchedulerPro/column/ScaleColumn.js` file which is now an empty wrapper importing
  the class from its new location. The wrapper is there till the next major release so please update your code and
  import the file from its new location if needed ([#6176](https://github.com/bryntum/support/issues/6176))

### LOCALE UPDATES

* Localization for the new class `SchedulingDirectionColumn` has been added. Keys are:
  `SchedulingDirectionColumn.schedulingDirection`, `SchedulingDirectionColumn.inheritedFrom`,
  `SchedulingDirectionColumn.enforcedBy`
* Localization for the new class `SchedulingDirectionPicker` has been added. Keys are:
  `SchedulingDirectionPicker.Forward`, `SchedulingDirectionPicker.Backward`, `SchedulingDirectionPicker.inheritedFrom`,
  `SchedulingDirectionPicker.enforcedBy`
* New entries for `ConstraintTypePicker`: `asap`, `alap`

### BUG FIXES

* [#4205](https://github.com/bryntum/support/issues/4205) - Error when undoing delete action on filtered store
* [#6956](https://github.com/bryntum/support/issues/6956) - Freeze after copy pasting top level node
* [#6959](https://github.com/bryntum/support/issues/6959) - Drag proxy misplaced when picking up Name column
* [#7009](https://github.com/bryntum/support/issues/7009) - Panel height increased when add more items but isn't decreased when remove items
* [#7018](https://github.com/bryntum/support/issues/7018) - Exported content is cut-off when normal grid is collapsed

## 5.3.7 - 2023-06-20

### BUG FIXES

* [#5908](https://github.com/bryntum/support/issues/5908) - Undo/Redo Segmented task change throws `TypeError: a is not iterable`
* [#6837](https://github.com/bryntum/support/issues/6837) - An exception when opening & closing the taskeditor in combination with tooltip
* [#6838](https://github.com/bryntum/support/issues/6838) - Still possible to add dependency by task editor when gantt is `readOnly`
* [#6843](https://github.com/bryntum/support/issues/6843) - Avatars and Icons not sized the same
* [#6849](https://github.com/bryntum/support/issues/6849) - Error `Failed to execute 'setEndBefore' on 'Range': the given Node has no parent`
* [#6851](https://github.com/bryntum/support/issues/6851) - Child nodes get duplicated when copy-pasting parent node
* [#6868](https://github.com/bryntum/support/issues/6868) - "Invalid date input" on manually enter date when custom date format used
* [#6873](https://github.com/bryntum/support/issues/6873) - Not able to hide units column using showUnits config
* [#6881](https://github.com/bryntum/support/issues/6881) - The background of the assignment column cells is not cleared after selection
* [#6894](https://github.com/bryntum/support/issues/6894) - Crash when deleting task from context menu in Gantt Big Data Set Demo
* [#6912](https://github.com/bryntum/support/issues/6912) - [REACT] Custom column editor lose value in React 18
* [#6944](https://github.com/bryntum/support/issues/6944) - Crash when copy pasting split task
* [#7006](https://github.com/bryntum/support/issues/7006) - `StateProvider` not restoring scroll position of left panel
* [#7010](https://github.com/bryntum/support/issues/7010) - Zoom level state not restored when timeline is collapsed
* [#7011](https://github.com/bryntum/support/issues/7011) - Gantt `addTaskAbove` and `addTaskBelow` methods do not copy the reference task duration unit

## 5.3.6 - 2023-05-26

### FEATURES / ENHANCEMENTS

* `ResourceAssignmentColumn` has a new `showAllNames` config used to determine if the names of all overflowing resources
  are displayed in the tooltip for overflow indicator. The `avatarTooltipTemplate` function also has a new argument for
  the indicator tooltip, called `overflowAssignments`, that contains overflowing assignments ([#6751](https://github.com/bryntum/support/issues/6751))

### API CHANGES

* The `change` event for the `FieldFilterPickerGroup` now includes an additional property `validFilters`, the subset of
  filters that are complete and valid ([#6774](https://github.com/bryntum/support/issues/6774))

### BUG FIXES

* [#5585](https://github.com/bryntum/support/issues/5585) - Start date of manually scheduled task that is before project start can't be edited
* [#6099](https://github.com/bryntum/support/issues/6099) - Delete task takes a while when big dataset used
* [#6291](https://github.com/bryntum/support/issues/6291) - Date formats are not working properly in startdate column
* [#6371](https://github.com/bryntum/support/issues/6371) - German localization improvements
* [#6655](https://github.com/bryntum/support/issues/6655) - Invalid revisions chain error in Gantt test
* [#6669](https://github.com/bryntum/support/issues/6669) - 2 sync requests triggered when editing last task and pressing `[ENTER]`
* [#6761](https://github.com/bryntum/support/issues/6761) - Row reordering crashes when combined with `RowNumberColumn` and `ResourceAvatarColumn`
* [#6802](https://github.com/bryntum/support/issues/6802) - `lagUnit` for newly created dependency in `DependencyTab` should use default model field value
* [#6833](https://github.com/bryntum/support/issues/6833) - Enabling/disabling filter checkbox in `FieldFilterPickerGroup` throws error on the console
* [#6848](https://github.com/bryntum/support/issues/6848) - Filter with rounded values not working

## 5.3.5 - 2023-05-11

### FEATURES / ENHANCEMENTS

* Added new event `beforeShowTerminals` to allow preventing dependency terminals from being displayed on a specific
  task ([#6609](https://github.com/bryntum/support/issues/6609))

### BUG FIXES

* [#4775](https://github.com/bryntum/support/issues/4775) - Error when clicking `[Shift + Left/Right]` arrow inside an empty Gantt
* [#6247](https://github.com/bryntum/support/issues/6247) - Conflict resolution dialog re-opens in circle when clicked Cancel
* [#6431](https://github.com/bryntum/support/issues/6431) - Error when trying to undo changes to multiple predecessors
* [#6495](https://github.com/bryntum/support/issues/6495) - `ReorderFeature` - `gripOnly` doesn't work when first column is a checkbox column
* [#6586](https://github.com/bryntum/support/issues/6586) - `addNewAtEnd` wrong indentation
* [#6668](https://github.com/bryntum/support/issues/6668) - Uneven button spacing in PHP demo
* [#6678](https://github.com/bryntum/support/issues/6678) - View refresh suspended after multiple sync requests
* [#6701](https://github.com/bryntum/support/issues/6701) - [IONIC] `Scrollbar` width could not be determined under Ionic framework
* [#6713](https://github.com/bryntum/support/issues/6713) - Time spans highlights re-appear after calling `unhighlightTimeSpans()`

## 5.3.4 - 2023-04-28

### BUG FIXES

* [#2234](https://github.com/bryntum/support/issues/2234) - Project throws when `effortUnit` is `null`
* [#5664](https://github.com/bryntum/support/issues/5664) - Long time export to MS project
* [#6630](https://github.com/bryntum/support/issues/6630) - Filter are applied before recalculating schedule
* [#6637](https://github.com/bryntum/support/issues/6637) - Pin successors not working correctly
* [#6648](https://github.com/bryntum/support/issues/6648) - `sync` called when expand tree node after `gantt.project.suspendAutoSync()`
* [#6652](https://github.com/bryntum/support/issues/6652) - Minified UMD bundle does not export `bryntum` namespace

## 5.3.3 - 2023-04-21

### FEATURES / ENHANCEMENTS

* [TypeScript] Type definition files were added for `gantt.node.cjs` and `gantt.node.mjs` bundles ([#6523](https://github.com/bryntum/support/issues/6523))
* [ANGULAR] Bryntum Gantt now ships with two npm Angular wrapper packages to support different versions of Angular
  framework. Existing `@bryntum/gantt-angular` package is now designed to work with Angular 12 and newer versions,
  which use the IVY rendering engine. New `@bryntum/gantt-angular-view` package is designed to work with Angular 11
  and older versions, which use the View Engine rendering. Check Upgrading and Angular integration guides in
  documentation for more information ([#6270](https://github.com/bryntum/support/issues/6270))
* [ANGULAR] `angular-11` demo has been added to show use of `@bryntum/gantt-angular-view` package with Angular 11.
  Demo is located in `examples/frameworks/angular/angular-11` folder
* [ANGULAR] `pdf-export`, `rollups` and `timeranges` demos have been upgraded to use Angular 15. Demos are located in
  subfolders inside `examples/frameworks/angular/` folder

### BUG FIXES

* [#2695](https://github.com/bryntum/support/issues/2695) - Resource Histogram is not updated when primary partner is zoomed or gets shifted
* [#5059](https://github.com/bryntum/support/issues/5059) - [REACT] Histogram not updating when doing `shiftPrevious()`
* [#6032](https://github.com/bryntum/support/issues/6032) - `visibleDate` incorrectly positioned on startup when `infiniteScroll` used
* [#6296](https://github.com/bryntum/support/issues/6296) - Undo 2 event drop actions throws an error when `nestedEvents` enabled
* [#6406](https://github.com/bryntum/support/issues/6406) - Setting `calendar` in project data throws an error
* [#6469](https://github.com/bryntum/support/issues/6469) - Syncing a new tree data set redraws for each node
* [#6528](https://github.com/bryntum/support/issues/6528) - TypeError `r.ion` is not a function
* [#6535](https://github.com/bryntum/support/issues/6535) - Add configurable baseline renderer
* [#6546](https://github.com/bryntum/support/issues/6546) - WBS cells are fillable with `fillHandle`
* [#6585](https://github.com/bryntum/support/issues/6585) - Error in versions demo when clicking "save version"
* [#6594](https://github.com/bryntum/support/issues/6594) - `GridSelection` should selectively not act for gestures inside a `TimeAxisColumn`
* [#6607](https://github.com/bryntum/support/issues/6607) - Timeline breaks when scroll if custom `viewPreset` used with empty data

## 5.3.2 - 2023-04-04

### FEATURES / ENHANCEMENTS

* The backend for the `php` demo was updated to work with PHP 8

### BUG FIXES

* [#6150](https://github.com/bryntum/support/issues/6150) - `scrollToDate` with animation does not work at edges of date buffer
* [#6280](https://github.com/bryntum/support/issues/6280) - After removing Invalid dependency, it's not clearing invalid dependency error
* [#6349](https://github.com/bryntum/support/issues/6349) - Optimize dependency hovering performance
* [#6358](https://github.com/bryntum/support/issues/6358) - Hard to select row in advanced demo
* [#6413](https://github.com/bryntum/support/issues/6413) - Filter is not working for total slack column
* [#6416](https://github.com/bryntum/support/issues/6416) - Filter gets reset in when opening number filter
* [#6470](https://github.com/bryntum/support/issues/6470) - Filtering the data will cause calendar events displacement
* [#6491](https://github.com/bryntum/support/issues/6491) - Drag-selection issues in Gantt

## 5.3.1 - 2023-03-17

### FEATURES / ENHANCEMENTS

* [ANGULAR] Added new Angular 15 demo "Drag from a grid". Demo is located in
  `examples/frameworks/angular/drag-from-grid` folder ([#6157](https://github.com/bryntum/support/issues/6157))

### API CHANGES

* `ProjectModel` convenience getter methods (`tasks`, `resources` etc.) now returns `allRecords` instead of `records`
* Date parsing was made more forgiving in regard to character used to separate date parts. For example these strings are
  now all acceptable as `HH:mm`: `10:20`, `10 20`, `10-20`, `10/20` ([#6344](https://github.com/bryntum/support/issues/6344))

### BUG FIXES

* [#5764](https://github.com/bryntum/support/issues/5764) - Gantt performance issues in Firefox with 5k tasks
* [#5975](https://github.com/bryntum/support/issues/5975) - Error on apply `StateProvider` state
* [#6241](https://github.com/bryntum/support/issues/6241) - Gantt crashes when after page reload with collapsed `timeAxis` subgrid
* [#6245](https://github.com/bryntum/support/issues/6245) - Undo not working with baselines
* [#6262](https://github.com/bryntum/support/issues/6262) - Engine performance issue
* [#6287](https://github.com/bryntum/support/issues/6287) - Cell selection styling remains on scroll
* [#6289](https://github.com/bryntum/support/issues/6289) - Predecessor column with editor + `clearable: true` triggers an error
* [#6312](https://github.com/bryntum/support/issues/6312) - Splitter does not move after updating to `5.3.0`
* [#6313](https://github.com/bryntum/support/issues/6313) - Errors when adding / deleting predecessors using the edit task popup
* [#6315](https://github.com/bryntum/support/issues/6315) - [SALESFORCE] PointerEvent is not a constructor error in LWC
* [#6320](https://github.com/bryntum/support/issues/6320) - `effortDriven` + fixed duration not working when setting it in the data level initially
* [#6324](https://github.com/bryntum/support/issues/6324) - `ProjectModel.tasks` should return all tasks
* [#6325](https://github.com/bryntum/support/issues/6325) - `'dataChange'` should not trigger for collapse & expand of parent nodes
* [#6341](https://github.com/bryntum/support/issues/6341) - Not possible to replace columns
* [#6351](https://github.com/bryntum/support/issues/6351) - Components do not render into containers not already in DOM
* [#6367](https://github.com/bryntum/support/issues/6367) - Gantt's project `onHasChanges` shows wrong tasks count after task drag operation

## 5.3.0 - 2023-03-02

### FEATURES / ENHANCEMENTS

* CSS changes in Scheduler has cut the size of Gantt's standalone CSS-bundles roughly in half (because Gantt includes
  Schedulers styling). See Schedulers upgrade guide for more information
* Support for Time zone conversion has been added to all Bryntum scheduling products. Simply set a IANA time zone
  identifier as value for the `timeZone` config and that's it. But, since time zones is not supported natively in
  JavaScript we strongly recommend to read our Time zone guide ([#1533](https://github.com/bryntum/support/issues/1533))
* `php` demo backend was updated to handle calendar intervals.
* Selection in a Grid with a `TreeStore` has been improved by addition of the `includeParents` config. Set it to `all`
  or `true` to auto select a parent if all its children gets selected. If one gets deselected, the parent will also be
  deselected. Set it to `some` to select a parent if one of its children gets selected. The parent will be deselected if
  all its children gets deselected ([#5726](https://github.com/bryntum/support/issues/5726))
* Added `Versions` feature, enabling capturing snapshots of projects together with a detailed changelog
* Localization demos updated to show up-to-date localization approach
* `AjaxHelper.fetch` now supports using request body to pass parameters for non-GET requests. Please check
  `addQueryParamsToBody` argument in the method documentation ([#2855](https://github.com/bryntum/support/issues/2855))
* `TaskMenu` feature now offers "Add dependencies" and "Remove dependencies" to link and link selected tasks
  ([#5846](https://github.com/bryntum/support/issues/5846))
* Lots (but not all) of the not so informative `object` types in our TypeScript typings have been replaced with more
  specific types. Objects that in our JavaScript are used as maps are now declared as `Record<keyType, valueType>`, and
  for functions that accept object arguments many are replaced with anonymous type declarations, such as
  `{ foo: string, bar: number }` (Partially fixed [#5176](https://github.com/bryntum/support/issues/5176))
* Gantt Sharepoint example has been upgraded to use latest spfx version and Node 16 ([#6239](https://github.com/bryntum/support/issues/6239))

### API CHANGES

* [DEPRECATED] `LocaleManager.registerLocale` and `LocaleManager.extendLocale` are deprecated.
  `LocaleHelper.publishLocale` should be used instead.
* When configuring a Gantt with a time zone at initialization, and there's also a `startDate` and/or a `endDate`
  initially, those days will be treated as in local system time zone and will therefore be converted to the configured
  time zone. Previously (in `5.3.0-alpha-1` and `5.3.0-beta-1`) those dates was treated as in the provided time zone

### LOCALE UPDATES

* Locales format and process for applying locales have been simplified
* New locales for 31 languages have been added. Currently available languages are listed in the localization guide
  (Guides/Customization/Localization)
* "Add dependencies" (`linkTasks`) and "Remove dependencies" (`unlinkTasks`) added to all locales for `TaskMenu`

### BUG FIXES

* [#3213](https://github.com/bryntum/support/issues/3213) -`[Cmd/Ctrl] + [Right click]` weird behavior
* [#3733](https://github.com/bryntum/support/issues/3733) - `selectedRecords` are in wrong order after shift selection
* [#4807](https://github.com/bryntum/support/issues/4807) - STM not tracking after updating project
* [#5400](https://github.com/bryntum/support/issues/5400) - Selecting tasks with `[Shift] + [Click]` get merged with previous selected task
* [#5858](https://github.com/bryntum/support/issues/5858) - Time zone demo missing scheduled tasks
* [#5900](https://github.com/bryntum/support/issues/5900) - Allow dragging resource avatars
* [#5989](https://github.com/bryntum/support/issues/5989) - Timezone conversion of baselines sometimes incorrect
* [#6018](https://github.com/bryntum/support/issues/6018) - Wrong position of highlighted region when `taskRecord` is set
* [#6020](https://github.com/bryntum/support/issues/6020) - Highlighted task timespan height not adjusting with the height of the rows
* [#6091](https://github.com/bryntum/support/issues/6091) - Deselect tasks in gantt using `Ctrl` selection not working correctly
* [#6195](https://github.com/bryntum/support/issues/6195) - Inactive + Ignore resource calendar fields missing in docs
* [#6201](https://github.com/bryntum/support/issues/6201) - Intervals names showing in random places when indicators are on
* [#6233](https://github.com/bryntum/support/issues/6233) - Exception when resizing splitter by the header part with active cell editor
* [#6242](https://github.com/bryntum/support/issues/6242) - Copying Calendar not working anymore
* [#6246](https://github.com/bryntum/support/issues/6246) - Crash when pasting rows if one of the copied tasks is removed
* [#6251](https://github.com/bryntum/support/issues/6251) - Resources disappearing on unselect in `ResourceAssignmentColumn`
* [#6260](https://github.com/bryntum/support/issues/6260) - Milestone is rendered incorrectly

## 5.2.10 - 2023-02-17

### FEATURES / ENHANCEMENTS

* Added a `trackProjectModelChanges` config to the `ProjectModel` to optionally track own changes of the `ProjectModel`
  ([#5355](https://github.com/bryntum/support/issues/5355))
* Added `beforeTaskAdd` event which fires when adding task from the UI ([#3724](https://github.com/bryntum/support/issues/3724))

### API CHANGES

* Recently browsers have added support for Unicode 15, which changes the output of `Intl.DateTimeFormat` when
  formatting time to include `AM`/`PM`. Those browsers now use "thin space" (`\u202f`) instead of regular space. This
  affects the `DateHelper.format()` function, but likely you do not need to take any action in your application. It
  also affects `DateHelper.parse()`, which has been updated to support the new unicode space ([#6193](https://github.com/bryntum/support/issues/6193))

### BUG FIXES

* [#6010](https://github.com/bryntum/support/issues/6010) - Project start label too small
* [#6012](https://github.com/bryntum/support/issues/6012) - `ZoomIn`/`ZoomOut` goes to a different location
* [#6036](https://github.com/bryntum/support/issues/6036) - Sync requests should not be issued while typing in `DurationField`
* [#6082](https://github.com/bryntum/support/issues/6082) - Crash when clearing predecessor filter
* [#6084](https://github.com/bryntum/support/issues/6084) - Escape key has no effect in cell editing in invalid dependency cell
* [#6110](https://github.com/bryntum/support/issues/6110) - `TaskStore` filter doesn't work when `TreeGroup` feature used
* [#6128](https://github.com/bryntum/support/issues/6128) - `visibleDateRangeChange` event has not accurate dates
* [#6135](https://github.com/bryntum/support/issues/6135) - Dialog for number filter closes when using arrows
* [#6154](https://github.com/bryntum/support/issues/6154) - MS Project import demo is broken
* [#6165](https://github.com/bryntum/support/issues/6165) - `Tooltip` positioning when having other elements with the Gantt

## 5.2.9 - 2023-01-30

### FEATURES / ENHANCEMENTS

* The `php` demo was updated to be compatible with PHP 8.2

### API CHANGES

* The `filter` feature of the grid will use `sameDay` operator, when filtering by the date field match, (will include
  all records with that field's value having the specified date, regardless of time). Previously it was performing the
  exact match, by both date and time ([#5976](https://github.com/bryntum/support/issues/5976))
* As of version 6.0, `remove` event will no longer be fired when moving a node in a tree store. To enable this behavior
  now (recommended), you can set a new `fireRemoveEventForMoveAction` on your tree store to `false` ([#5371](https://github.com/bryntum/support/issues/5371))

### BUG FIXES

* [#5905](https://github.com/bryntum/support/issues/5905) - Critical path on segmented tasks is set on gap instead of body
* [#5924](https://github.com/bryntum/support/issues/5924) - Error with invalid dependency with new task
* [#5964](https://github.com/bryntum/support/issues/5964) - Calendar interval wrongly applied on DST period
* [#5966](https://github.com/bryntum/support/issues/5966) - `constraintTypeField` needs more space to see content
* [#5971](https://github.com/bryntum/support/issues/5971) - Dependency terminals not shown when enabling feature at runtime
* [#6019](https://github.com/bryntum/support/issues/6019) - [TypeScript] Feature classes and configs have `on` event handlers exposed on owner class
* [#6056](https://github.com/bryntum/support/issues/6056) - `StateProvider` not trigger event `onSave` with `GanttState`
* [#6071](https://github.com/bryntum/support/issues/6071) - `DomHelper` `getRootElement` throwing error on creating dependency
* [#6076](https://github.com/bryntum/support/issues/6076) - Duplicate listeners added if clicking New task quickly

## 5.2.8 - 2023-01-19

### BUG FIXES

* [#5386](https://github.com/bryntum/support/issues/5386) - Improved panel collapse animation when collapsed panel header is perpendicular to expanded
* [#5721](https://github.com/bryntum/support/issues/5721) - Exception during undo
* [#5797](https://github.com/bryntum/support/issues/5797) - `fillTicks` does not look correct for static non working time
* [#5814](https://github.com/bryntum/support/issues/5814) - `StateProvider` throws during component construction
* [#5855](https://github.com/bryntum/support/issues/5855) - Assignment with zero units causes an exception for effort driven tasks
* [#5914](https://github.com/bryntum/support/issues/5914) - Syncing changes after outdenting node leads to wrong WBS index
* [#5921](https://github.com/bryntum/support/issues/5921) - Baseline changes do not get synchronized
* [#5931](https://github.com/bryntum/support/issues/5931) - Deprecated API use. Handle size is from CSS
* [#5936](https://github.com/bryntum/support/issues/5936) - Wrong milestone layout in "custom-rendering" Gantt demo

## 5.2.7 - 2023-01-11

### FEATURES / ENHANCEMENTS

* `Tree` feature now supports expanding to multiple nodes ([#2287](https://github.com/bryntum/support/issues/2287))

### API CHANGES

* The `StartDateField/EndDateField` fields now sets the default value of their `max` property to be 200 years after the
  project's end date, see the docs for the corresponding classes ([#5779](https://github.com/bryntum/support/issues/5779))

### BUG FIXES

* [#3901](https://github.com/bryntum/support/issues/3901) - `DateField` validation do not allows to change date manually for foreign locales
* [#5729](https://github.com/bryntum/support/issues/5729) - Dependency creation difficult on touch devices
* [#5877](https://github.com/bryntum/support/issues/5877) - Dependencies between tasks not copied

## 5.2.6 - 2022-12-28

### FEATURES / ENHANCEMENTS

* [REACT] React wrapper now supports React components in widgets and tooltips ([#774](https://github.com/bryntum/support/issues/774))
* [REACT] New React Redux demo has been added. Demo located in `examples/frameworks/react/javascript/gantt-redux` folder
* The `RowCopyPaste` feature will now paste copied or cut row(s) *below* selected or provided reference record.
  Previously the documentation stated that the copied or cut row(s) would be pasted above the reference record. However,
  the behaviour was inconsistent and cut-paste was done above while copy-paste was done below ([#4890](https://github.com/bryntum/support/issues/4890))

### BUG FIXES

* [#5394](https://github.com/bryntum/support/issues/5394) - Non working days not painted after adding a calendar interval programmatically
* [#5718](https://github.com/bryntum/support/issues/5718) - Replacing calendars with same id causes an error
* [#5723](https://github.com/bryntum/support/issues/5723) - Non-working events partially disappear during scroll
* [#5771](https://github.com/bryntum/support/issues/5771) - Gantt Cut/Paste rows freezes gantt with 3k tasks
* [#5783](https://github.com/bryntum/support/issues/5783) - Calendar dropdown getting focused when click on the label
* [#5794](https://github.com/bryntum/support/issues/5794) - Hide overflow scrollbar in resource assignment column with no avatars

## 5.2.5 - 2022-12-16

### FEATURES / ENHANCEMENTS

* `TaskCopyPaste` feature used to trigger excessive events on the `TaskStore`. Now `add` event is fired only for tasks
  which, after paste, make up top-level siblings
* `RowCopyPaste` feature supports copying rows in a tree. Copied records will have same hierarchy
* Paste after cut and copy behavior is unified, records are moved below the paste target
* `msprojectimport` demo has been updated to MPXJ v10.14.1 to better support up-to-date MS Project file types

### API CHANGES

* [DEPRECATED] `Gantt/data/Wbs.js` was deprecated and moved to `Core/data/Wbs.js`

### BUG FIXES

* [#5287](https://github.com/bryntum/support/issues/5287) - WBS should be recalculated correctly on the sorted task store
* [#5488](https://github.com/bryntum/support/issues/5488) - Dependency arrows overlap task bar
* [#5614](https://github.com/bryntum/support/issues/5614) - Gantt crashes when dragging a resource to a collapsed grid
* [#5676](https://github.com/bryntum/support/issues/5676) - Improve `onPaste` event return data
* [#5709](https://github.com/bryntum/support/issues/5709) - Creating invalid dependency to linked task shows green circle
* [#5716](https://github.com/bryntum/support/issues/5716) - Grouping feature shows duration in tooltip as `NaN`
* [#5736](https://github.com/bryntum/support/issues/5736) - Dependencies incorrectly positioned when hovered if document is scrolled

## 5.2.4 - 2022-11-28

### FEATURES / ENHANCEMENTS

* We recently launched a new homepage over at [bryntum.com](https://bryntum.com), and have now slightly updated the
  styling for demos and docs to better match it (new logo, new header color, new font). Please note that this is not a
  change to our themes, only the look of the demos, and it won't affect your application
* Now it is possible to unschedule the task by entering an empty string to its start/end/duration field.
  Such tasks are not rendered in the scheduling part and does not influence other tasks. To schedule task
  back, one can enter a missing value ([#4318](https://github.com/bryntum/support/issues/4318))

### BUG FIXES

* [#5475](https://github.com/bryntum/support/issues/5475) - Bryntum Gantt with zoom less than 100% on Firefox not rendering correctly
* [#5541](https://github.com/bryntum/support/issues/5541) - Timeline header is not rendered when calling `zoomToFit`
* [#5591](https://github.com/bryntum/support/issues/5591) - Error on expand collapsible panel by click on header
* [#5595](https://github.com/bryntum/support/issues/5595) - Fix panel collapse icon directions
* [#5614](https://github.com/bryntum/support/issues/5614) - Gantt crashes when dragging a resource to a collapsed grid
* [#5626](https://github.com/bryntum/support/issues/5626) - Race condition in project model prevents persisting removals
* [#5627](https://github.com/bryntum/support/issues/5627) - Canceling cell editing with `ESC` key lost the focus from cell
* [#5631](https://github.com/bryntum/support/issues/5631) - Crash when clicking non-editable `DependencyField`

## 5.2.3 - 2022-11-17

### BUG FIXES

* [#5466](https://github.com/bryntum/support/issues/5466) - Gantt crash when setting `visibleDate`
* [#5542](https://github.com/bryntum/support/issues/5542) - Project lines are not rendered properly on `shiftNext()` and `shiftPrevious()`
* [#5556](https://github.com/bryntum/support/issues/5556) - Separator class is missing on menu items
* [#5562](https://github.com/bryntum/support/issues/5562) - Scheduling conflict and exception when reading `lateStartDate` or `lateEndDate` properties inside
  a task store filter
* [#5563](https://github.com/bryntum/support/issues/5563) - Error when disabling key-mapping on copy/paste feature
* [#5587](https://github.com/bryntum/support/issues/5587) - Do not patch `offsetX`/`offsetY`

## 5.2.2 - 2022-11-08

### API CHANGES

* [DEPRECATED] The behaviour of the `store.data` getter will be changed in 6.0. Currently, it returns the **initial**
  raw dataset, in 6.0 it will be changed to have the more expected behaviour of returning the data objects for the
  **current** state instead. See Grid's upgrade guide for more information ([#5499](https://github.com/bryntum/support/issues/5499))

### BUG FIXES

* [#5354](https://github.com/bryntum/support/issues/5354) - `ignoreRemoteChangesInSTM` config leads to exception when two clients edit task at the same time
* [#5485](https://github.com/bryntum/support/issues/5485) - Dropping task revert the changes when mouse is over the tooltip
* [#5503](https://github.com/bryntum/support/issues/5503) - Multi-select not possible with `cmd` key
* [#5504](https://github.com/bryntum/support/issues/5504) - P6 file import issue
* [#5531](https://github.com/bryntum/support/issues/5531) - Column rename not compatible with `Filter` feature

## 5.2.1 - 2022-10-28

### BUG FIXES

* [#1802](https://github.com/bryntum/support/issues/1802) - Exception on a scheduling conflict
* [#4417](https://github.com/bryntum/support/issues/4417) - Typing into Gantt `DateField` freezes browser
* [#4927](https://github.com/bryntum/support/issues/4927) - An error occurs if `task.critical` called with disabled `criticalpaths` feature
* [#5149](https://github.com/bryntum/support/issues/5149) - Angular demos now use component-local styles using `ViewEncapsulation.None`
* [#5362](https://github.com/bryntum/support/issues/5362) - `TimeRange` header element styling bug
* [#5423](https://github.com/bryntum/support/issues/5423) - Gantt `DependencyField` filters using wrong input when filtering using picker's field
* [#5429](https://github.com/bryntum/support/issues/5429) - Exception "Identifier can not read from higher level identifier"
* [#5434](https://github.com/bryntum/support/issues/5434) - `DragCreate` exception when dragging right then left beyond start point
* [#5451](https://github.com/bryntum/support/issues/5451) - `DatePicker` animation glitch
* [#5478](https://github.com/bryntum/support/issues/5478) - Enable certain tasks to not allow resizing progress

## 5.2.0 - 2022-10-13

### FEATURES / ENHANCEMENTS

* Gantt has gained built-in support for segmented tasks, by using the new `EventSegments`, `TaskSegmentDrag` and
  `TaskSegmentResize` features. The features cover splitting tasks to segments, rendering of such tasks and individual
  segments dragging. Please check the new `split-tasks` demo to see how it works ([#2975](https://github.com/bryntum/support/issues/2975))
* A new widget, `ViewPresetCombo`, is available to Scheduler, SchedulerPro and Gantt. Put it in the toolbar, and it will
  provide easy-to-setup view switching. It uses the built-in ViewPresets functionality which is easily customized
  ([#4539](https://github.com/bryntum/support/issues/4539))
* Added a new `collapsible-columns` demo showing how to use collapsible column groups ([#4878](https://github.com/bryntum/support/issues/4878))
* The `fillTicks` config from Scheduler was ported over to Gantt. Enable it to stretch tasks to always fill the time
  cells/ticks ([#3983](https://github.com/bryntum/support/issues/3983))
* Menu has a `separator` config to make it easier to visually separate menu items
* The responsive state objects used in the `responsive` config of the `Responsive` mixin now support a `once` property
  to allow configs to only be set on first activation of the state
* The `Core.helper.DateHelper` class has a new method `formatRange` method which can format date ranges, as well as new
  formatting options for week numbers
* Added new feature `TaskNonWorkingTime` which highlights the non-working time of the tasks, meaning time intervals when
  they can not be worked on. Try it out in the new `calendars` demo ([#3260](https://github.com/bryntum/support/issues/3260))
* Baselines can now be styled by supplying `cls` and/or `style` as part of their data ([#2873](https://github.com/bryntum/support/issues/2873))
* The `TreeGroup` feature was reworked to allow manipulating the tasks in the generated structure, applying the same
  rules as in the original view
* PdfExport feature is refactored to render content directly. This significantly improves performance and robustness by
  eliminating component scrolling. This behavior is enabled by default. You can revert to the old behavior by setting
  `enableDirectRendering` config on the export feature to `false`. ([#4449](https://github.com/bryntum/support/issues/4449))
* Baselines are now exported to PDF ([#4834](https://github.com/bryntum/support/issues/4834))
* New `fieldfilters` demo showing how to add multi-filter UI to a Gantt
* `ResourceHistogram` has got a new `generateScalePoints` event that allows customizing its scale points at runtime
  ([#5025](https://github.com/bryntum/support/issues/5025))

### LOCALE UPDATES

* Added localization for the new task split functionality, key `EventSegments.splitTask`

### API CHANGES

* `EventModel` has new `ignoreResourceCalendar` boolean field. When field is set to `true` the event will not take its
  assigned resource calendars into account and will perform according to its own calendar only. ([#3349](https://github.com/bryntum/support/issues/3349))

### BUG FIXES

* [#336](https://github.com/bryntum/support/issues/336) - Task baselines need to track changes and modify Task
* [#1947](https://github.com/bryntum/support/issues/1947) - Update MSProjectImport demo to use the latest Maven
* [#2422](https://github.com/bryntum/support/issues/2422) - Wrong baselines dates should not cause crash
* [#2755](https://github.com/bryntum/support/issues/2755) - Task baselines are not imported correctly
* [#4096](https://github.com/bryntum/support/issues/4096) - `CalendarIntervalMixin` class uses hardcoded date field formats
* [#4218](https://github.com/bryntum/support/issues/4218) - It is not possible to drag resize manuallyScheduled parent tasks
* [#4573](https://github.com/bryntum/support/issues/4573) - Issue with the Import MPP
* [#4828](https://github.com/bryntum/support/issues/4828) - MPP file import throwing error
* [#5053](https://github.com/bryntum/support/issues/5053) - MPP file import throwing error
* [#5055](https://github.com/bryntum/support/issues/5055) - MS Project import demo cannot handle a file
* [#5143](https://github.com/bryntum/support/issues/5143) - `calendarInterval` doesn't change after an interval is added
* [#5148](https://github.com/bryntum/support/issues/5148) - MPP File import not working
* [#5169](https://github.com/bryntum/support/issues/5169) - `setBaseline` not triggering sync method
* [#5249](https://github.com/bryntum/support/issues/5249) - Split task outside of task element does nothing
* [#5251](https://github.com/bryntum/support/issues/5251) - Converting a segmented task into milestone removes it
* [#5257](https://github.com/bryntum/support/issues/5257) - Gantt split-tasks demo throws on deleting task
* [#5259](https://github.com/bryntum/support/issues/5259) - PDF export insert extra margin at bottom
* [#5268](https://github.com/bryntum/support/issues/5268) - Project line is rendered at the start of the timeline if corresponding project date is out of the gantt
  time span
* [#5294](https://github.com/bryntum/support/issues/5294) - Extra dependency line is drawn on the exported page
* [#5334](https://github.com/bryntum/support/issues/5334) - MS Project import demo produces invalid calendar intervals
* [#5356](https://github.com/bryntum/support/issues/5356) - Exception thrown when trying to export `Gantt` with a dependency which doesn't have target record
* [#5387](https://github.com/bryntum/support/issues/5387) - Exporter throws exception when trying to render dependency to unscheduled task
* [#5399](https://github.com/bryntum/support/issues/5399) - MS Project import demo corrupts Japanese characters

## 5.1.5 - 2022-10-12

### FEATURES / ENHANCEMENTS

* New demo `drag-resources-from-utilization-panel` showing how to drag resources from utilization and drop on a `Gantt`
  row to assign the resource to a task.
* New records are assigned a generated `id` if none is provided. The generated `id` is meant to be temporary (a
  phantom `id`), and should be replaced by the backend on commit. Previously the `id` was based on a global counter
  incremented with each assignment. That simplistic scheme assured no two records got the same `id` during a session,
  but if an application serialized the generated `id` (note, they should not) and then reloaded it, it would eventually
  collide with a new generated `id`. To prevent this, the generated `id`s are now based on a random UUID instead
* Stores now by default show a warning on console when loading records that has generated `id`s, as a reminder that it
  should be replaced by the backend on commit

### BUG FIXES

* [#4645](https://github.com/bryntum/support/issues/4645) - Improve error message "Bryntum bundle included twice"
* [#4654](https://github.com/bryntum/support/issues/4654) - [REACT] Bryntum widget wrappers don't accept all component properties in React 18
* [#5312](https://github.com/bryntum/support/issues/5312) - Crash when partnering with a hidden partner
* [#5337](https://github.com/bryntum/support/issues/5337) - [Angular] `ZoneAwarePromise` for Angular 8 and below doesn't support `Promise.allSettled`
* [#5343](https://github.com/bryntum/support/issues/5343) - `AggregateColumn` not working if added after Grid is painted

## 5.1.4 - 2022-09-29

### FEATURES / ENHANCEMENTS

* New `filterbar` demo added showing the `FilterBar` feature ([#5208](https://github.com/bryntum/support/issues/5208))
* New `p6-import` demo added for importing Primavera P6 projects ([#5300](https://github.com/bryntum/support/issues/5300))

### BUG FIXES

* [#5047](https://github.com/bryntum/support/issues/5047) - Conflict dialog looks bad in dark theme
* [#5173](https://github.com/bryntum/support/issues/5173) -`FilterBar` doesn't work for `startDate`/`endDate` columns when date has `hour`/`minute` provided
* [#5180](https://github.com/bryntum/support/issues/5180) - Dependency contents are misaligned when `fromSide` and `toSide` set in the console
* [#5212](https://github.com/bryntum/support/issues/5212) - Duration is exported incorrectly for localized gantt
* [#5219](https://github.com/bryntum/support/issues/5219) - PNG export doesn't render dependency arrows
* [#5260](https://github.com/bryntum/support/issues/5260) - Rendering artefact in Angular and React timeranges demos
* [#5309](https://github.com/bryntum/support/issues/5309) - Project throws when applying changes with a task outdented to the top level

## 5.1.3 - 2022-09-09

### BUG FIXES

* [#415](https://github.com/bryntum/support/issues/415) - Improve docs on formatting currency values on `NumberField`
* [#3680](https://github.com/bryntum/support/issues/3680) - Support Salesforce Winter 22 release
* [#4457](https://github.com/bryntum/support/issues/4457) - [React] Error when change locale for gantt in routing page
* [#5125](https://github.com/bryntum/support/issues/5125) - Setting an initial value for `activeTab` on a `TabPanel` no longer animates that tab into view
* [#5158](https://github.com/bryntum/support/issues/5158) - `TaskRecord` should be available in the params of `beforeTaskResizeFinalize`
* [#5189](https://github.com/bryntum/support/issues/5189) - `Project` throws exception when trying to apply changes for copied tasks hierarchy

## 5.1.2 - 2022-08-29

### FEATURES / ENHANCEMENTS

* An application's filters on a store may now be configured with an `internal` property. This indicates that they are
  fixed, and must not be ingested and modified by filtering UIs such as the `Filter` and `FilterBar` features
  ([#4980](https://github.com/bryntum/support/issues/4980))
* Configs that accept configuration options for a widget (or other class) are now (mostly) documented to accept a typed
  config object rather than a plain object. For example instead of `{Object} tooltip - A tooltip configuration object`,
  it is now `{TooltipConfig} tooltip - A tooltip configuration object`. This improves our TypeScript typings (transforms
  to `Partial<TooltipConfig>` in typings) when using such configs, but also improves our docs by linking to the configs
  of the type
* Gantt now supports using keyboard shortcuts for indenting and outdenting tasks. Defaults are `Alt + Shift + Right` for
  indenting, and `Alt + Shift + Left` for outdenting. Note that the `Alt` key matches the `Option` key on Mac
  Configurable with the `KeyMap` mixin **Guides/Customization/Keyboard shortcuts**
* Added a config to allow State Tracking Manager to ignore remote changes coming in a sync response. This allows user
  to only undo/redo local changes (`ignoreRemoteChangesInSTM` config on the `ProjectModel`) ([#5083](https://github.com/bryntum/support/issues/5083))

### API CHANGES

* CalendarModel `expanded` field now defaults to `true`

### BUG FIXES

* [#3729](https://github.com/bryntum/support/issues/3729) - Assignment field selection is cleared when picker store has a filter
* [#4110](https://github.com/bryntum/support/issues/4110) - `selectionchange` event is fired too often
* [#4984](https://github.com/bryntum/support/issues/4984) - Infinite loop when reordering rows
* [#5017](https://github.com/bryntum/support/issues/5017) - [TypeScript] Property type is missing in `DataFieldConfig`
* [#5018](https://github.com/bryntum/support/issues/5018) - [Vue] Prop Validation fails for `String` options
* [#5023](https://github.com/bryntum/support/issues/5023) - Error `menuFeature.hideContextMenu is not a function` when dragging
* [#5033](https://github.com/bryntum/support/issues/5033) - Dependency creation always create dependency end to start even if dragged end to end
* [#5040](https://github.com/bryntum/support/issues/5040) - Dependency tooltip contents are misaligned
* [#5041](https://github.com/bryntum/support/issues/5041) - Gantt crashes when destroyed while the React editor is open
* [#5054](https://github.com/bryntum/support/issues/5054) - New tasks now display their `wbsCode` in Predecessors/Successors picker. Advanced example now uses
  `wbsMode: 'auto'`
* [#5086](https://github.com/bryntum/support/issues/5086) - Wrong constraint is set on parent task if child gets modified during the active sync
* [#5102](https://github.com/bryntum/support/issues/5102) - Calendars missing unless parent calendar is `expanded`
* [#5114](https://github.com/bryntum/support/issues/5114) - Gantt SharePoint fabric demo fails on `npm install`

## 5.1.1 - 2022-07-28

### BUG FIXES

* [#3427](https://github.com/bryntum/support/issues/3427) - Infinite sync loop if `percentDone` value is incorrect

## 5.1.0 - 2022-07-21

### FEATURES / ENHANCEMENTS

* The `Dependencies` feature was refactored, it now continuously redraws dependencies during transitions. It also allows
  customizing the marker (arrow head) and applying a radius to line corners for a more rounded look
* Our TypeScript typings for string types that have a predefined set of alternatives was improved to only accept
  those alternatives. For example previously the `dock` config which was previously declared as `dock: string` is now
  `dock : 'top'|'right'|'bottom'|'left'`
* Create React App templates now available
* Configuring the crud manager functionality of the project was made a little easier by introducing shortcuts for
  setting load and sync urls using the new `loadUrl` and `syncUrl` configs
* Updated the built-in version of FontAwesome Free to `6.1.1`
* Added new Angular Gantt + Resource Utilization combination demo
* `KeyMap` is a mixin that allows for standardized and customizable keyboard shortcuts functionality. `KeyMap` is by
  default mixed in to `Widget` and therefore available to all `Widget`'s child classes. There is a new guide
  **Guides/Customization/Keyboard shortcuts** describing how to customize currently integrated keyboard shortcuts
  ([#4300](https://github.com/bryntum/support/issues/4300), [#4313](https://github.com/bryntum/support/issues/4313), [#4328](https://github.com/bryntum/support/issues/4328))
* Added `applyProjectChanges` method to the Project model which allows applying changes object from another
  project. Projects should be isomorphic
* Project optionally allows `sync()` calls without local changes, to retrieve changes from the backend. Configure
  `forceSync : true` to enable this new behaviour ([#4575](https://github.com/bryntum/support/issues/4575))

### API CHANGES

* [BREAKING] [ANGULAR] Angular wrappers now use the more modern module bundle by default, instead of the legacy umd
  bundle. Hence application imports must be changed to match. This will slightly improve application size and
  performance ([#2786](https://github.com/bryntum/support/issues/2786))
* [BREAKING] `gantt.lite.umd.js` bundle is no longer available
* [BREAKING] WebComponents has been removed from `gantt.module.js` ES modules bundle. New bundle with WebComponents
  is `gantt.wc.module.js`
* [DEPRECATED] The `drawForTask()` fn of the `Dependencies` feature was deprecated. Calling it should no longer be
  necessary

### BUG FIXES

* [#4301](https://github.com/bryntum/support/issues/4301) - Fixed radio button keyboard navigation is conflict dialog
* [#4696](https://github.com/bryntum/support/issues/4696) - Parents sorted below children in docs
* [#4697](https://github.com/bryntum/support/issues/4697) - Too dark code background in docs
* [#4884](https://github.com/bryntum/support/issues/4884) - UI bug when editing a task duration and expanding a parent task
* [#4926](https://github.com/bryntum/support/issues/4926) - Scrollbar remains after removing all rows
* [#4935](https://github.com/bryntum/support/issues/4935) - Editing last record in gantt moves focus to the first cell if `autoSync` is enabled
* [#4947](https://github.com/bryntum/support/issues/4947) - Tasks are duplicated in the view when collapsing nodes

## 5.0.7 - 2022-07-13

### FEATURES / ENHANCEMENTS

* The `AssignmentField` now lets you configure its tooltip using the `tooltipTemplate` config ([#4821](https://github.com/bryntum/support/issues/4821))
* Added preventable `beforeSelectionChange` event which fires before selection changes ([#4705](https://github.com/bryntum/support/issues/4705))

### BUG FIXES

* [#3980](https://github.com/bryntum/support/issues/3980) - Dependency creation target changes if moving mouse quickly after mouse up after creating dependency
* [#4681](https://github.com/bryntum/support/issues/4681) - STM issues when using with backend
* [#4756](https://github.com/bryntum/support/issues/4756) - PDF export hangs trying to restore component
* [#4805](https://github.com/bryntum/support/issues/4805) - Changes not reverted when close `TaskEditor` with `Esc`
* [#4815](https://github.com/bryntum/support/issues/4815) - Paste event doesn't fire
* [#4852](https://github.com/bryntum/support/issues/4852) - Assignments editing broken when using mapped fields
* [#4863](https://github.com/bryntum/support/issues/4863) - Setting task calendar to null does not trigger sync request
* [#4916](https://github.com/bryntum/support/issues/4916) - `Fullscreen` is not working on mobile Safari
* [#4919](https://github.com/bryntum/support/issues/4919) - Engine throws exception on referencing a destroyed project

## 5.0.6 - 2022-06-20

### BUG FIXES

* [#4430](https://github.com/bryntum/support/issues/4430) - PDF Export dialog is still masked after failed export
* [#4431](https://github.com/bryntum/support/issues/4431) - Fix docs for `isValidDependency` method to allow pass instances
* [#4476](https://github.com/bryntum/support/issues/4476) - Task date resets after deleting 3 last chars of start date
* [#4719](https://github.com/bryntum/support/issues/4719) - Toggling parent task with collapsed grid causes a crash
* [#4736](https://github.com/bryntum/support/issues/4736) - Task editor not shown when task is not scrolled into view
* [#4754](https://github.com/bryntum/support/issues/4754) - Auto-scroll for dependency creation doesn't work
* [#4759](https://github.com/bryntum/support/issues/4759) - Using mapped `lag` field breaks cell editing
* [#4779](https://github.com/bryntum/support/issues/4779) - Crash when adding empty `bbar`
* [#4784](https://github.com/bryntum/support/issues/4784) - Lag not copied when copying tasks
* [#4789](https://github.com/bryntum/support/issues/4789) - Wrong drag behavior when trying to select text of a task
* [#4803](https://github.com/bryntum/support/issues/4803) - Circular Dependency Dialog edit box is too small
* [#4808](https://github.com/bryntum/support/issues/4808) - Typings are wrong for async functions
* [#4813](https://github.com/bryntum/support/issues/4813) - Scheduling Cycle combo shows previous content on resolving next conflict
* [#4820](https://github.com/bryntum/support/issues/4820) - ExcelExporter encoding problem with resourceAssignment when showAvatars is true

## 5.0.5 - 2022-05-30

### BUG FIXES

* [#4444](https://github.com/bryntum/support/issues/4444) - Tooltip not displayed when calling showBy targeting a widget
* [#4537](https://github.com/bryntum/support/issues/4537) - Crash when toggling parent node with timeline section collapsed
* [#4567](https://github.com/bryntum/support/issues/4567) - Using too new `replaceChildren` API in Row
* [#4580](https://github.com/bryntum/support/issues/4580) - Crash when dragging task card on to Gantt chart
* [#4581](https://github.com/bryntum/support/issues/4581) - Cut and paste tasks removes dependencies
* [#4590](https://github.com/bryntum/support/issues/4590) - `highlightTimeSpan` function throwing error when taskStore is filtered
* [#4591](https://github.com/bryntum/support/issues/4591) - `response.message` not shown in error mask in case of response code not `200`
* [#4598](https://github.com/bryntum/support/issues/4598) - `unhighlightTimeSpans=true` breaks the `TimeSpanHighlight` feature
* [#4599](https://github.com/bryntum/support/issues/4599) - Timeline not updated after changing task with custom mapped fields
* [#4601](https://github.com/bryntum/support/issues/4601) - `ResourceHistogram` still partnered with gantt even after using `removePartner`
* [#4607](https://github.com/bryntum/support/issues/4607) - [VUE] Incorrect prop types in Vue wrapper
* [#4624](https://github.com/bryntum/support/issues/4624) - XSS security bugs
* [#4625](https://github.com/bryntum/support/issues/4625) - Dropdown editor not working for new subtask

## 5.0.4 - 2022-05-11

### API CHANGES

* `ProjectModel` has got new `maxCalendarRange` option that allows supporting long running projects. For more details,
  see [What's new](https://bryntum.com/products/gantt/docs/guide/Gantt/whats-new/5.0.4) ([#2962](https://github.com/bryntum/support/issues/2962))

### BUG FIXES

* [#4309](https://github.com/bryntum/support/issues/4309) - Baseline with no dates is shown as full task's duration baseline
* [#4365](https://github.com/bryntum/support/issues/4365) - Fullscreen button should not have a border in framework demos
* [#4513](https://github.com/bryntum/support/issues/4513) - Sync response format documentation missing in Gantt guide
* [#4562](https://github.com/bryntum/support/issues/4562) - [REACT] React wrappers have incorrect source mapping urls
* [#4574](https://github.com/bryntum/support/issues/4574) - `highlightTimeSpans` works even though the `timeSpanHighlight` feature is disabled

## 5.0.3 - 2022-04-26

### API CHANGES

* The `validateResponse` flag on `ProjectModel` has been changed to default to `true`. When enabled, it validates
  responses from the backend and outputs a message on console if the format isn't valid. This is helpful during the
  development phase, but can be turned off in production
* New Vue 2/3 wrapper config option `relayStoreEvents` (defaults to `false`). When set to `true`, the events fired by
  stores are relayed to the Bryntum Grid instance
* [REACT] React wrappers now include TypeScript definitions ([#3378](https://github.com/bryntum/support/issues/3378))

### BUG FIXES

* [#4127](https://github.com/bryntum/support/issues/4127) - [LWC] `DomHelper.isInView()` throws
* [#4222](https://github.com/bryntum/support/issues/4222) - [LWC] Performance degradation in 5.0 release
* [#4238](https://github.com/bryntum/support/issues/4238) - [WRAPPERS] Use `ProjectModel` wrapper component as `project` parameter
* [#4289](https://github.com/bryntum/support/issues/4289) - Using `TreeGroup` modifies tasks
* [#4337](https://github.com/bryntum/support/issues/4337) - Tasks disappear when clicking Change data in demo
* [#4387](https://github.com/bryntum/support/issues/4387) - ASPNet demos should use trial packages in trial distribution
* [#4432](https://github.com/bryntum/support/issues/4432) - [LWC] Mouse events do not work
* [#4439](https://github.com/bryntum/support/issues/4439) - Add public event to track task edit cancel action
* [#4461](https://github.com/bryntum/support/issues/4461) - [Vue] wrapper triggers doubled `dataChange` events with different params
* [#4515](https://github.com/bryntum/support/issues/4515) - `validateResponse` should be `true` by default
* [#4531](https://github.com/bryntum/support/issues/4531) - [DOCS] "Include in your app" section missing in Gantt
* [#4532](https://github.com/bryntum/support/issues/4532) - Hovered row looks bad while row reordering `TreeGrid` with handle enabled
* [#4533](https://github.com/bryntum/support/issues/4533) - Duration column updates multiple times when using spinner triggers

## 5.0.2 - 2022-04-13

### FEATURES / ENHANCEMENTS

* The `beforeTaskEdit`, `beforeTaskSave` and `beforeTaskDelete` events triggered by the TaskEdit feature now handle
  async flows. Use async / await in your listener or return a Promise and it will be awaited before execution
  continues. Useful for example to ask for a confirmation on save etc ([#4421](https://github.com/bryntum/support/issues/4421))

### BUG FIXES

* [#4049](https://github.com/bryntum/support/issues/4049) - `beforeEventEdit` does not fire on Scheduler Pro, whereas `beforeTaskEdit` does
* [#4224](https://github.com/bryntum/support/issues/4224) - Fix background applied to selected parent milestones
* [#4257](https://github.com/bryntum/support/issues/4257) - Selecting a milestone task does not change its background
* [#4291](https://github.com/bryntum/support/issues/4291) - React editors are not working in new version
* [#4304](https://github.com/bryntum/support/issues/4304) - MS Project export issues
* [#4308](https://github.com/bryntum/support/issues/4308) - Problems with rendering React component in column renderer and as cell editor
* [#4320](https://github.com/bryntum/support/issues/4320) - Crash when drag dropping in LWC
* [#4333](https://github.com/bryntum/support/issues/4333) - Gantt task editor end date datepicker arrows are not working
* [#4344](https://github.com/bryntum/support/issues/4344) - Replace `%` lag values in MS Project importer
* [#4363](https://github.com/bryntum/support/issues/4363) - [REACT] Advanced example - recreates project when any params updated for its react wrapper component
* [#4370](https://github.com/bryntum/support/issues/4370) - Assigning tasks to project when `syncDataOnLoad:true` results in empty `Gantt`
* [#4381](https://github.com/bryntum/support/issues/4381) - Pin successors feature calculates lag incorrectly for calendars with less than 24h per day
* [#4385](https://github.com/bryntum/support/issues/4385) - Problem with encoding on export to excel
* [#4405](https://github.com/bryntum/support/issues/4405) - Exception occurred on task drag if snap is enabled
* [#4406](https://github.com/bryntum/support/issues/4406) - Fixed items in disabled `fieldset`/`radiogroup` not being disabled
* [#4482](https://github.com/bryntum/support/issues/4482) - Gantt not refreshed when supplying empty array to `store.filter()` with `replace: true`

## 5.0.1 - 2022-03-04

### API CHANGES

* [WRAPPERS] New `ResourceUtilization` widget wrapper available for Angular, React and Vue frameworks ([#4276](https://github.com/bryntum/support/issues/4276))

### BUG FIXES

* [#3861](https://github.com/bryntum/support/issues/3861) - Bug in Baseline Demo after setting baseline `3`
* [#4162](https://github.com/bryntum/support/issues/4162) - Left arrow key does not navigate from selected task
* [#4252](https://github.com/bryntum/support/issues/4252) - No dependencies in `CycleResolutionPopup` combo
* [#4286](https://github.com/bryntum/support/issues/4286) - When `TreeGroup` feature is active, `Indent`, `Outdent`, add `SubTask` should be disabled
* [#4295](https://github.com/bryntum/support/issues/4295) - Edit / View task modal will not open when gantt view is collapsed

## 5.0.0 - 2022-02-21

* We are thrilled to announce version 5.0 of our Gantt product. This release marks a big milestone for us, after more
  than a year of development. This update includes performance improvements, a new ResourceUtilization widget, a
  TreeGroup feature as well as bug fixes and other enhancements requested by our community. A big thanks to our
  customers who helped us with testing our alpha & beta versions
* You are most welcome to join us on March 16th, at 9am PST (6pm CET) for a 5.0 walkthrough webinar, demonstrating all
  the shiny new features
  [Click here to register](https://us06web.zoom.us/webinar/register/5116438317103/WN_4MkExpZPQsGYNpzh1mR_Ag)
* We hope you will enjoy this release and we are looking forward to hearing your feedback of what you would like us to
  develop next
* / Mats Bryntse, CEO @Bryntum

### FEATURES / ENHANCEMENTS

* Added a new Resource Utilization view displaying resource allocation. Please check its demo for details
  ([#2348](https://github.com/bryntum/support/issues/2348))
* With this release Gantt starts displaying a popup informing users of scheduling conflicts, cycles and calendar
  misconfigurations. The popup allows the user to pick an appropriate resolution for the case at hand. On the data
  level the cases are indicated by new events triggered on the project ([#1264](https://github.com/bryntum/support/issues/1264), [#1265](https://github.com/bryntum/support/issues/1265))
* Gantt now performs the initial rendering of tasks quicker than before, by rendering them using raw data prior to
  performing calculations. Especially on large datasets this makes it feel much snappier. Requires loading normalized
  data to work optimally. Depending on how much non-UI manipulating your app does on the tasks the delayed calculations
  might affect your code, be sure to check out the upgrade guide ([#2251](https://github.com/bryntum/support/issues/2251))
* Gantt has a new `TreeGroup` feature that can transform the task tree on the fly. It generates a new tree structure
  based on an array of field names (or functions), each entry yields a new level in the resulting tree. Check it out in
  the new `grouping` demo ([#3543](https://github.com/bryntum/support/issues/3543))
* Each product has a new "thin" JavaScript bundle. The thin bundle only contains product specific code, letting you
  combine multiple Bryntum products without downloading the shared code multiple times (previously only possible with
  custom-built bundles from sources). Find out more in the What's new guide ([#2805](https://github.com/bryntum/support/issues/2805))
* Each theme is now available in a version that only has product specific CSS in it, called a `thin` version. These
  files are name `[product].[theme].thin.css` - `gantt.stockholm.thin.css` for example. They are intended for using
  when you have multiple different bryntum products on the same page, to avoid including shared CSS multiple times
  Read more about it in the `What's new` section in docs ([#3276](https://github.com/bryntum/support/issues/3276))
* `Model` has a new `readOnly` field that is respected by UI level editing features to disallow editing records having
  `readOnly : true`. It does not directly affect the datalayer, meaning that you can still programmatically edit the
  records ([#665](https://github.com/bryntum/support/issues/665))
* Manually scheduled tasks are changed to not skip non-working time for proposed start/end date values. Check the
  upgrade guide if you want to revert to the previous behaviour ([#2326](https://github.com/bryntum/support/issues/2326))
* Added a `pinSuccessors` config that allows successors to stay in place by adjusting lag when dragging a task
  ([#3998](https://github.com/bryntum/support/issues/3998))
* Gantt's task rendering now uses absolute positioning instead of translation to position the task bars. This was
  changed to match changes in Scheduler and Scheduler Pro ([#4055](https://github.com/bryntum/support/issues/4055))
* New `ParentArea` feature highlighting the area encapsulating all child tasks ([#1290](https://github.com/bryntum/support/issues/1290))
* New `ProjectModel` setters/getters for `assignments`, `calendars`, `dependencies`, `resources`, `tasks`, `timeRanges`
  ([#4043](https://github.com/bryntum/support/issues/4043))
* New `highlight-time-spans` demo showing how to visualize time spans
* New `grid-regions` demo showing how to place columns on the right side of the timeline
* `window` references are replaced with `globalThis` which is supported in all modern browsers and across different JS
  environments ([#4071](https://github.com/bryntum/support/issues/4071))
* Gantt now triggers `beforeMspExport` and `mspExport` events when `MspExport` feature is used ([#3565](https://github.com/bryntum/support/issues/3565))
* A new function called `downloadTestCase()` was added to Bryntum widgets, it is intended to simplify creating test
  cases for reporting issues on Bryntum's support forum. Running it collects the current value for the configs your app
  is using, inlines the current dataset and compiles that into a JavaScript app that is then downloaded. The app will
  most likely require a fair amount of manual tweaking to reproduce the issue, but we are hoping it will simplify the
  process for you. Run `gantt.downloadTestCase()` on the console in a demo to try it
* Updated FontAwesome Free to version 6, which includes some new icons sponsored by Bryntum in the charts category:
  https://fontawesome.com/search?m=free&c=charts-diagrams&s=solid
* When configured with a StateProvider and `stateId`, Gantt state is stored automatically as stateful properties
  change ([#1859](https://github.com/bryntum/support/issues/1859))
* You can now style `CalendarIntervals` by providing a `cls` field in their data. This makes it very easy to style non
  working time elements ([#3255](https://github.com/bryntum/support/issues/3255))
* [WRAPPERS] Gantt has a new `ProjectModel` framework wrapper available for React, Vue and Angular. It simplifies
  sharing data between multiple Bryntum components  ([#4877](https://github.com/bryntum/support/issues/4877))
* [ANGULAR] New demo showing use of inline data. Demo located in `examples/frameworks/angular/inline-data` folder
* [REACT] New demo showing use of inline data. Demo located in `examples/frameworks/react/javascript/inline-data`
  folder
* [VUE] New demo showing use of inline data. Demo located in `examples/frameworks/vue/javascript/inline-data` folder
* [VUE-3] New demo showing use of inline data. Demo located in `examples/frameworks/vue-3/javascript/inline-data` folder

For more details, see [What's new](https://bryntum.com/products/gantt/docs/guide/Gantt/whats-new/5.0.0)
and [Upgrade guide](https://bryntum.com/products/gantt/docs/guide/Gantt/upgrades/5.0.0) in docs

### API CHANGES

* [BREAKING] The Engine `ResourceAllocationInfo` class `allocation` property has been changed from an `Array` to
  an `Object` with two properties `total` and `byAssignments`. The `total` property contains an array of the resource
  allocation intervals. And the `byAssignments` is a `Map` keeping individual assignment allocation intervals with
  assignments as keys and arrays of allocation intervals as values
  Please check [Upgrade guide](https://bryntum.com/products/gantt/docs/guide/Gantt/upgrades/5.0.0) if
  your code uses that class
* [BREAKING] React wrappers now use the more modern module bundle by default, instead of the legacy umd bundle. Hence
  application imports must be changed to match. This will slightly improve application size and performance
  ([#2787](https://github.com/bryntum/support/issues/2787))
* [DEPRECATED] ResourceHistogram `getBarTip` config has been deprecated in favour of new `barTooltipTemplate` config
  Please check the upgrade guide and update your code accordingly
* Store's `toJSON()` method now ignores all local filters and returns all records ([#4101](https://github.com/bryntum/support/issues/4101))
* `DependencyModel.active` field has been changed to be persistable by default. To revert to the old behavior
  please override the field and set its `persist` config to `false`
* The following previously deprecated Gantt configs, functions etc. where removed:
* `TaskContextMenu` feature - previously replaced by `TaskMenu` feature
* Arguments `startText`, `endText`, `startClockHtml`, `endClockHtml` & `dragData` of `TaskDrag#tooltipTemplate()`
* Config `TaskEdit#tabsConfig` - previously replaced by `items`
* Config `ProjectModel#eventModelClass` - previously replaced by `taskModelClass`
* Config `ProjectModel#eventStoreClass` - previously replaced by `taskStoreClass`
* Field `TaskModel#wbsIndex` - previously replaced by `wbsValue`
* Argument `tplData` of `Gantt#taskRenderer()` - previously replaced by `renderData`
* Config `AssignmentPicker#grid` - previously replaced by configuring `AssignmentPicker` directly
* Config `TaskEditor#durationDecimalPrecision` - previously replaced by `durationDisplayPrecision`
* Event `Gantt#beforeExport` - in favor of `beforePdfExport` event
* Event `Gantt#export` - in favor of `pdfExport` event

### BUG FIXES

* [#209](https://github.com/bryntum/support/issues/209) - TaskArea layer plugin for Gantt
* [#1771](https://github.com/bryntum/support/issues/1771) - Parent task not rerendered on expand or collapse
* [#1965](https://github.com/bryntum/support/issues/1965) - Exception is thrown when adding a dependency results in constraint violation
* [#3786](https://github.com/bryntum/support/issues/3786) - Erratic dragging when using `getDateConstraints`
* [#3898](https://github.com/bryntum/support/issues/3898) - MS Project import fails for MPX files using wk duration unit
* [#3957](https://github.com/bryntum/support/issues/3957) - Gantt label editing only works once
* [#3966](https://github.com/bryntum/support/issues/3966) - Dependency error message should be shown on the field
* [#3993](https://github.com/bryntum/support/issues/3993) - MS Project import demo fails to import MPX file
* [#4003](https://github.com/bryntum/support/issues/4003) - Bigdataset initial rendering artefacts
* [#4113](https://github.com/bryntum/support/issues/4113) - Calendar is not applies when switching via projects
* [#4142](https://github.com/bryntum/support/issues/4142) - Timeline demo issue
* [#4165](https://github.com/bryntum/support/issues/4165) - Exception in custom task bar demo
* [#4179](https://github.com/bryntum/support/issues/4179) - Events disappear when clicking size buttons
* [#4232](https://github.com/bryntum/support/issues/4232) - Rollups + loading nodes on demand causes a crash

## 4.3.8 - 2022-02-07

### BUG FIXES

* [#4072](https://github.com/bryntum/support/issues/4072) - `Container.setValues` should use each widget's `defaultBindProperty`, not hardcode value
* [#4104](https://github.com/bryntum/support/issues/4104) - MS Project export feature does not extract assignments
* [#4107](https://github.com/bryntum/support/issues/4107) - MS Project export feature retrieves wrong duration for manually scheduled tasks
* [#4118](https://github.com/bryntum/support/issues/4118) - Assigning resource via `ResourceAssignmentColumn` doesn't trigger events in `assignmentStore`

## 4.3.7 - 2022-02-02

### FEATURES / ENHANCEMENTS

* The new `newTaskDefaults` allows you to customize values applied to newly created tasks through UI ([#4004](https://github.com/bryntum/support/issues/4004))
* CellEdit can now be configured to stop editing after clicking another cell via its new `continueEditingOnCellClick`
  config ([#4046](https://github.com/bryntum/support/issues/4046))

### API CHANGES

* [DEPRECATED] Gantt `beforeExport` and `export` events (triggered by `PdfExport` feature) were deprecated in favor of
  the `beforePdfExport` and `pdfExport` events respectively. The old events names will be dropped in v5.0.0

### BUG FIXES

* [#3997](https://github.com/bryntum/support/issues/3997) - Store id property documentation is not clear
* [#4007](https://github.com/bryntum/support/issues/4007) - End to end dependency is not drawn in some cases
* [#4012](https://github.com/bryntum/support/issues/4012) - Unexpected scheduling conflict
* [#4029](https://github.com/bryntum/support/issues/4029) - `autoEdit` should not react when CTRL / CMD key is used to copy & paste
* [#4033](https://github.com/bryntum/support/issues/4033) - `taskKeyDown`, `taskKeyUp` event parameter missing
* [#4037](https://github.com/bryntum/support/issues/4037) - Resource name in Assignment Picker is too escaped
* [#4041](https://github.com/bryntum/support/issues/4041) - `TextArea` ignores arrowDown key press
* [#4064](https://github.com/bryntum/support/issues/4064) - Unexpected constraint when setting a dependency lag
* [#4082](https://github.com/bryntum/support/issues/4082) - Relayed listeners do not trigger onFunctions

## 4.3.6 - 2022-01-13

### BUG FIXES

* [#3788](https://github.com/bryntum/support/issues/3788) - Minimum value for duration field in Task Editor works incorrect
* [#3981](https://github.com/bryntum/support/issues/3981) - Predecessor/successor column filter shows incorrect value when clicked Equals
* [#3990](https://github.com/bryntum/support/issues/3990) - Chrome & Content Security Policy causes failure because of debug code section
* [#4008](https://github.com/bryntum/support/issues/4008) - Filter icon disappears when a column is hidden

## 4.3.5 - 2021-12-24

### BUG FIXES

* [#3746](https://github.com/bryntum/support/issues/3746) - Minimum value violated error for end date field in the task editor
* [#3750](https://github.com/bryntum/support/issues/3750) - Resizing task end date puts start date constraint
* [#3887](https://github.com/bryntum/support/issues/3887) - Calendar applies wrong if it's startDate is far in the past
* [#3896](https://github.com/bryntum/support/issues/3896) - [TypeScript] Wrong typings of model class configs
* [#3900](https://github.com/bryntum/support/issues/3900) - MS Project export feature uses wrong UID type
* [#3907](https://github.com/bryntum/support/issues/3907) - [TypeScript] Cannot pass Scheduler instance to `Store.relayAll`
* [#3923](https://github.com/bryntum/support/issues/3923) - Dependency line has extra points with certain system scaling
* [#3928](https://github.com/bryntum/support/issues/3928) - DateHelper `k` format behaves incorrectly

## 4.3.4 - 2021-12-13

### FEATURES / ENHANCEMENTS

* Updated `advanced`, `gantt-schedulerpro` and `taskeditor` Angular demos to use Angular 13 ([#3742](https://github.com/bryntum/support/issues/3742))
* Enhanced `wbsMode` to allow an object indicating what triggers renumbering WBS values ([#3858](https://github.com/bryntum/support/issues/3858))

### BUG FIXES

* [#3621](https://github.com/bryntum/support/issues/3621) - [TypeScript] Improve typings of mixins
* [#3777](https://github.com/bryntum/support/issues/3777) - Online angular pdf export demo has wrong export server configured
* [#3803](https://github.com/bryntum/support/issues/3803) - Can't change dependency type using column editor
* [#3850](https://github.com/bryntum/support/issues/3850) - [TypeScript] Missing static properties in typings
* [#3854](https://github.com/bryntum/support/issues/3854) - `DependencyColumn` does not produce valid value for the Filter Feature
* [#3878](https://github.com/bryntum/support/issues/3878) - `DependencyField` renders (undefined) if used out of scope of the project

## 4.3.3 - 2021-11-30

### FEATURES / ENHANCEMENTS

* `ResourceAssignmentColumn` has a new `avatarTooltipTemplate` config that lets you supply custom content for the avatar
  tooltip ([#2954](https://github.com/bryntum/support/issues/2954))

### BUG FIXES

* [#3626](https://github.com/bryntum/support/issues/3626) - Tasks overlap each other on vertical drag
* [#3628](https://github.com/bryntum/support/issues/3628) - Task Editor combo `listItemTpl` has no effect
* [#3639](https://github.com/bryntum/support/issues/3639) - `wbsValue` is now correctly updated when sorting the `TaskStore` and `wbsMode: 'auto'` is enabled
* [#3645](https://github.com/bryntum/support/issues/3645) - Dependency links are not shown up at browser zoom level 75%
* [#3648](https://github.com/bryntum/support/issues/3648) - [DOCS] Content navigation is broken
* [#3676](https://github.com/bryntum/support/issues/3676) - Error when using predecessor/successor filters
* [#3677](https://github.com/bryntum/support/issues/3677) - Crash in `cellEdit` when starting edit record in collapsed parent
* [#3689](https://github.com/bryntum/support/issues/3689) - Implement support of PDF export feature on Salesforce
* [#3720](https://github.com/bryntum/support/issues/3720) - `dataSource` property not working on dependency from and to fields
* [#3726](https://github.com/bryntum/support/issues/3726) - Remove outline when using mouse
* [#3734](https://github.com/bryntum/support/issues/3734) - Resource Assignment Picker needs a `minHeight`
* [#3743](https://github.com/bryntum/support/issues/3743) - [DOCS] `web.config` file for Windows IIS server
* [#3751](https://github.com/bryntum/support/issues/3751) - Gantt localization bundle contains unwanted `Core` code
* [#3794](https://github.com/bryntum/support/issues/3794) - `ProjectLines` feature doesn't react to project change
* [#3807](https://github.com/bryntum/support/issues/3807) - `beforeTaskDrag` event is fired on mouse down
* [#3821](https://github.com/bryntum/support/issues/3821) - `MspExport` feature exports only expanded node tasks

## 4.3.2 - 2021-10-29

### FEATURES / ENHANCEMENTS

* New `summary` demo showing how to add a summary bar to the Gantt ([#3601](https://github.com/bryntum/support/issues/3601))
* The `WBS` field can now be automatically refreshed by setting `wbsMode: 'auto'` on the `TaskStore` class ([#2721](https://github.com/bryntum/support/issues/2721))
* `TaskCopyPaste` feature now fires `beforeCopy` and `beforePaste` events to let you prevent the actions ([#3303](https://github.com/bryntum/support/issues/3303))

### BUG FIXES

* [#3464](https://github.com/bryntum/support/issues/3464) - Infinite loop when `earlystart` is displayed with a `null` startDate/endDate
* [#3572](https://github.com/bryntum/support/issues/3572) - `wbsValue` updates are not included into sync pack after it set to be persistent
* [#3584](https://github.com/bryntum/support/issues/3584) - Project start date is not updated after task addition, when initially no tasks have start/end/duration
  data
* [#3597](https://github.com/bryntum/support/issues/3597) - Removing the `startDate`/`endDate` of a task with dependencies prevent you from scheduling it again
* [#3599](https://github.com/bryntum/support/issues/3599) - `readOnly` flag allows task drag drop
* [#3605](https://github.com/bryntum/support/issues/3605) - Pressing ESC during task drag schedule leaves an element
* [#3610](https://github.com/bryntum/support/issues/3610) - `readOnly` not disabling actions in Task Context Menu
* [#3612](https://github.com/bryntum/support/issues/3612) - `TaskModelClass` cannot be defined in project settings
* [#3618](https://github.com/bryntum/support/issues/3618) - LockerService exception: The addEventListener method on ShadowRoot does not support any options

## 4.3.1 - 2021-10-21

### FEATURES / ENHANCEMENTS

* Bumped builtin Font Awesome Free to version 5.15.4
* Added new demo `drag-from-grid` showing how to drag unplanned tasks onto the Gantt
* Added new config `scrollTaskIntoViewOnCellClick` which scrolls the task bar into when clicking a cell in the table
* `ProjectModel` now has a `resetUndoRedoQueuesAfterLoad` flag to optionally clear undo / redo queues after each load
  ([#3549](https://github.com/bryntum/support/issues/3549))
* A group column can now be `sealed` meaning you are not allowed to drop columns into it ([#3536](https://github.com/bryntum/support/issues/3536))

### BUG FIXES

* [#802](https://github.com/bryntum/support/issues/802)  - Rollup elements should be rendered same way as task itself
* [#3389](https://github.com/bryntum/support/issues/3389) - Tasks copy/paste doesn't copy dependencies nor maintain tree indent level properly
* [#3425](https://github.com/bryntum/support/issues/3425) - Infinite cycle in the Engine
* [#3429](https://github.com/bryntum/support/issues/3429) - Reload dependencies as `inlineData` causes error when visible in React
* [#3558](https://github.com/bryntum/support/issues/3558) - Id column in TaskEditor´s `DependencyTab` is not sortable
* [#3561](https://github.com/bryntum/support/issues/3561) - Crash after right clicking columns on Gantt Predecessor / Successor, only works on the first one
* [#3563](https://github.com/bryntum/support/issues/3563) - Gantt - Feature toggle event for baselines feature does not fire
* [#3567](https://github.com/bryntum/support/issues/3567) - Minified css bundle contains unicode chars
* [#3576](https://github.com/bryntum/support/issues/3576) - Gantt `TaskDragCreate` must not delete task on ESC
* [#3578](https://github.com/bryntum/support/issues/3578) - Crash when setting calendar column editor to false

## 4.3.0 - 2021-10-12

### FEATURES / ENHANCEMENTS

* Inactive tasks support has been added. Such tasks don't take part in the scheduling yet stay in the project plan. For
  more details please check
  ["Inactive tasks"](https://bryntum.com/products/gantt/docs/guide/Gantt/basics/inactive_tasks) guide and see
  [this new demo](https://bryntum.com/products/gantt/examples/inactive-tasks/) ([#2981](https://github.com/bryntum/support/issues/2981), [#807](https://github.com/bryntum/support/issues/807))
* `ResourceHistogram` now supports resource grouping. It displays the aggregated resources allocation on the group
  level ([#2608](https://github.com/bryntum/support/issues/2608))
* [IONIC] Added Ionic framework integration demo. Demo located in `examples/frameworks/ionic/ionic-4` folder
  ([#2622](https://github.com/bryntum/support/issues/2622))

### API CHANGES

* [DEPRECATED] Buttons `menuIconCls` config was deprecated in favor of the new `menuIcon` config, which better matches
  the naming of other configs

### BUG FIXES

* [#3457](https://github.com/bryntum/support/issues/3457) - Filter field inside toolbar overflow hides after every keypress
* [#3484](https://github.com/bryntum/support/issues/3484) - Gantt does not handle if resource store is grouped

## 4.2.7 - 2021-10-01

### FEATURES / ENHANCEMENTS

* Added new `setCalculations` method to the `ProjectModel` class. The method toggles the project
  owned model fields calculation functions, which allows changing the behavior of standard fields dynamically
  Please check the new `static` demo for details ([#2284](https://github.com/bryntum/support/issues/2284), [#2327](https://github.com/bryntum/support/issues/2327))

### BUG FIXES

* [#2909](https://github.com/bryntum/support/issues/2909) - MS Project importer doesn't support project calendar recurring exceptions
* [#3243](https://github.com/bryntum/support/issues/3243) - Dependency links are misplaced when browser zoom level is 75%
* [#3272](https://github.com/bryntum/support/issues/3272) - Expanding last node sometimes doesn't increase scroll size
* [#3382](https://github.com/bryntum/support/issues/3382) - visibleDate works incorrect if `startDate` is not provided (re-fix)
* [#3396](https://github.com/bryntum/support/issues/3396) - MSProjectReader does not import calendar intervals properly
* [#3424](https://github.com/bryntum/support/issues/3424) - Crash when showing id column of predecessor grid in task editor
* [#3426](https://github.com/bryntum/support/issues/3426) - Button with menu should show extra menu arrow icon
* [#3441](https://github.com/bryntum/support/issues/3441) - Invalid dependency format errors when filtered
* [#3450](https://github.com/bryntum/support/issues/3450) - Unable to set columns store data on Gantt instance
* [#3453](https://github.com/bryntum/support/issues/3453) - QuickFind feature doesn't work in Gantt
* [#3454](https://github.com/bryntum/support/issues/3454) - Timeline tasks should use 'cls' field
* [#3458](https://github.com/bryntum/support/issues/3458) - Document nested fields
* [#3459](https://github.com/bryntum/support/issues/3459) - Add an event when expanding/collapsing subgrids
* [#3460](https://github.com/bryntum/support/issues/3460) - Dependency terminals not hidden after task resize
* [#3462](https://github.com/bryntum/support/issues/3462) - Exception in the Gantt when Project endDate is not calculated
* [#3465](https://github.com/bryntum/support/issues/3465) - Gantt ResourceAssignmentColumn too many decimal places
* [#3466](https://github.com/bryntum/support/issues/3466) - An error if totalslack column used and Saturday work scheduled

## 4.2.6 - 2021-09-15

### BUG FIXES

* [#3382](https://github.com/bryntum/support/issues/3382) - `visibleDate` works incorrect if `startDate` is not provided
* [#3400](https://github.com/bryntum/support/issues/3400) - Export to MS Project throws error when `startDate` or `endDate` is `null`
* [#3408](https://github.com/bryntum/support/issues/3408) - Updated typings to support spread operator for method parameters

## 4.2.5 - 2021-09-08

### FEATURES / ENHANCEMENTS

* New `gantt.node.mjs` and `gantt.node.cjs` bundles are available. Both are compatible with the
  Node.js environment ([#3224](https://github.com/bryntum/support/issues/3224))
* Added `keyMap` config to reconfigure or disable cut / copy / paste keyboard shortcuts for TaskCopyPaste ([#3351](https://github.com/bryntum/support/issues/3351))
* ProjectModel now fires a `dataReady` event when the engine has finished its calculations and the result has been
  written back to the records ([#2019](https://github.com/bryntum/support/issues/2019))
* The API documentation now better communicates when a field or property accepts multiple input types but uses a single
  type for output. For example date fields on models, which usually accepts a `String` or `Date` but always outputs a
  `Date` ([#2933](https://github.com/bryntum/support/issues/2933))
* New `custom-headers` demo showing how to customize the rows shown in the time axis header

### BUG FIXES

* [#1847](https://github.com/bryntum/support/issues/1847) - Gantt bundle does not export Engine classes
* [#3120](https://github.com/bryntum/support/issues/3120) - Cannot read property 'map' of undefined in task store toJSON
* [#3283](https://github.com/bryntum/support/issues/3283) - Resources grouping works incorrect with some data set
* [#3322](https://github.com/bryntum/support/issues/3322) - Add `dataChange` event to framework guides
* [#3337](https://github.com/bryntum/support/issues/3337) - Gantt `ProjectModel#sync` Promise does not return response data
* [#3345](https://github.com/bryntum/support/issues/3345) - AspNet demos use wrong `@bryntum` npm package version
* [#3346](https://github.com/bryntum/support/issues/3346) - Gantt web component does not show critical paths
* [#3347](https://github.com/bryntum/support/issues/3347) - Inconsistent Language - Start to Finish/Start to End
* [#3350](https://github.com/bryntum/support/issues/3350) - Disabling task copy paste feature leaves keyboard shortcuts still active
* [#3359](https://github.com/bryntum/support/issues/3359) - Dependency id column in predecessors tab render the `dependencyIdField` rather than `task.id`
* [#3365](https://github.com/bryntum/support/issues/3365) - Indicators config is mutated preventing reuse
* [#3371](https://github.com/bryntum/support/issues/3371) - Label editor misaligned
* [#3379](https://github.com/bryntum/support/issues/3379) - Crash when dates missing when using critical path

## 4.2.4 - 2021-08-27

### FEATURES / ENHANCEMENTS

* MS Project export feature now exports task baselines ([#3278](https://github.com/bryntum/support/issues/3278))
* Project now triggers a `change` event when data in any of its stores changes. Useful to listen for to keep an external
  data model up to date for example ([#3281](https://github.com/bryntum/support/issues/3281))
* Project got a new config `adjustDurationToDST` which is `false` by default, meaning project will no longer try to keep
  task duration in hours an integer multiple of 24. This change fixes the problem with task end date shifting 1 hour
  from midnight in certain cases. To return to the old behavior set this config to `true` ([#3329](https://github.com/bryntum/support/issues/3329))

### BUG FIXES

* [#794](https://github.com/bryntum/support/issues/794) - Dependency creation tooltip is initially misaligned
* [#1432](https://github.com/bryntum/support/issues/1432) - Gantt doesn't take DST into account for task duration
* [#3116](https://github.com/bryntum/support/issues/3116) - Gantt throws on task terminal drag
* [#3127](https://github.com/bryntum/support/issues/3127) - appendChild function of task model is not working
* [#3132](https://github.com/bryntum/support/issues/3132) - Add support of "mo" units to MS Project import
* [#3170](https://github.com/bryntum/support/issues/3170) - Tasks are not rescheduled after adding intervals to calendar
* [#3250](https://github.com/bryntum/support/issues/3250) - Menus do not work in fullscreen mode
* [#3264](https://github.com/bryntum/support/issues/3264) - Progress line ignores milestones
* [#3265](https://github.com/bryntum/support/issues/3265) - Docs are not scrolled to the referenced member
* [#3270](https://github.com/bryntum/support/issues/3270) - Cutting and Pasting tasks does not preserve the order
* [#3283](https://github.com/bryntum/support/issues/3283) - Resources grouping works incorrect with some data set
* [#3284](https://github.com/bryntum/support/issues/3284) - PercentDoneColumn cannot use other model field
* [#3291](https://github.com/bryntum/support/issues/3291) - Gantt dependency tooltip should render the dependencyIdField rather than `task.id`
* [#3296](https://github.com/bryntum/support/issues/3296) - Task editor does not show if triggered when schedule region is collapsed
* [#3301](https://github.com/bryntum/support/issues/3301) - Copy/Paste should not react if cell or editor text is selected
* [#3305](https://github.com/bryntum/support/issues/3305) - Guides look bad in the docs search results
* [#3306](https://github.com/bryntum/support/issues/3306) - Doc browser does not scroll to member
* [#3311](https://github.com/bryntum/support/issues/3311) - alwaysWrite field causing stack overflow for task 'calendar'
* [#3321](https://github.com/bryntum/support/issues/3321) - Baseline bars misrendered if task has no start / end dates
* [#3324](https://github.com/bryntum/support/issues/3324) - Not possible to select end date after editing

## 4.2.3 - 2021-08-05

### FEATURES / ENHANCEMENTS

* Project can now log warnings to the browser console when it detects an unexpected response format. To enable these
  checks please use the `validateResponse` config ([#2668](https://github.com/bryntum/support/issues/2668))
* [NPM] Bryntum Npm server now supports remote private repository access for Artifactory with username and password
  authentication ([#2864](https://github.com/bryntum/support/issues/2864))
* The PdfExport feature now supports configuring its ExportDialog to pre-select columns to export or to customize any of
  the child widgets ([#2052](https://github.com/bryntum/support/issues/2052))
* [TYPINGS] Type definitions now contain typed `features` configs and properties ([#2740](https://github.com/bryntum/support/issues/2740))

### API CHANGES

* [DEPRECATED] PdfExport feature `export` event is deprecated and will be removed in 4.3.0. Use `export` event on the
  Gantt instead
* [DEPRECATED] Gantt `beforeExport` event signature is deprecated and will be removed in 4.3.0. New signature wraps
  config object to the corresponding key

### BUG FIXES

* [#431](https://github.com/bryntum/support/issues/431) - Baseline index is NaN in tooltip
* [#3116](https://github.com/bryntum/support/issues/3116) - Gantt throws on task terminal drag
* [#3184](https://github.com/bryntum/support/issues/3184) - Visual artifact with full % Done column in circle mode
* [#3202](https://github.com/bryntum/support/issues/3202) - Bug with scrolling the column visibility pop-up
* [#3204](https://github.com/bryntum/support/issues/3204) - TimeRanges data provided inline is not rendered
* [#3206](https://github.com/bryntum/support/issues/3206) - Selection is not updated when triggering contextmenu on expander icon
* [#3214](https://github.com/bryntum/support/issues/3214) - Resizing or dragging column in a grid inside a Popup starts drag & drop of outer popup
* [#3229](https://github.com/bryntum/support/issues/3229) - Filter by typing in Predecessor and Successor field doesn't filter by wbsCode
* [#3235](https://github.com/bryntum/support/issues/3235) - Export to MSProject does not support cyrillic symbols
* [#3247](https://github.com/bryntum/support/issues/3247) - Scroller position reset to 0 when filtering using FilterBar with no results
* [#3248](https://github.com/bryntum/support/issues/3248) - Crash when dragging task if no 'locked' region exists

## 4.2.2 - 2021-07-21

### FEATURES / ENHANCEMENTS

* Added a new `hideRangesOnZooming` config to `NonWorkingTime` feature ([#2788](https://github.com/bryntum/support/issues/2788)). The config allows to disable the
  feature default behavior when it hides ranges shorter than the base timeaxis unit on zooming out
* [NPM] Bryntum Npm server now supports `npm token` command for managing access tokens for CI/CD ([#2703](https://github.com/bryntum/support/issues/2703))

### API CHANGES

* [DEPRECATED] Class `GanttMspExport` exported from the bundle is deprecated, use `MspExport` instead
  `GanttMspExport` will be removed from the bundle in 5.0

### BUG FIXES

* [#416](https://github.com/bryntum/support/issues/416) - TreeNode children field cannot be mapped
* [#435](https://github.com/bryntum/support/issues/435) - Cannot map baselines field and baseline start/end fields
* [#2071](https://github.com/bryntum/support/issues/2071) - Support configuring eventeditor / taskeditor child items with 'true' value
* [#3114](https://github.com/bryntum/support/issues/3114) - TaskEdit crashes when tab panel items configured with true
* [#3153](https://github.com/bryntum/support/issues/3153) - Critical Paths feature does not include all paths
* [#3159](https://github.com/bryntum/support/issues/3159) - Gantt Task Editor Resources tab user combo should be editable for filtering
* [#3160](https://github.com/bryntum/support/issues/3160) - ResourceAssignmentColumn throws an error after closed with filtered data
* [#3167](https://github.com/bryntum/support/issues/3167) - LWC bundle is missing from trial packages
* [#3169](https://github.com/bryntum/support/issues/3169) - Unhovered task dependency terminals not properly hidden
* [#3176](https://github.com/bryntum/support/issues/3176) - Dependency arrow not pointing task for end-to-start relationship if target task is short
* [#3178](https://github.com/bryntum/support/issues/3178) - Syntax highlighter messes up code snippets in docs
* [#3185](https://github.com/bryntum/support/issues/3185) - Add CSS class to indicate that an event is being created
* [#3196](https://github.com/bryntum/support/issues/3196) - MspExport feature is missing from the angular wrapper

## 4.2.1 - 2021-07-07

### FEATURES / ENHANCEMENTS

* [FRAMEWORKS] Added `taskCopyPasteFeature` to frameworks wrappers ([#3135](https://github.com/bryntum/support/issues/3135))

### BUG FIXES

* [#3136](https://github.com/bryntum/support/issues/3136) - [NPM] Running `npm install` twice creates modified `package-lock.json` file

## 4.2.0 - 2021-06-30

### FEATURES / ENHANCEMENTS

* Gantt has a new config option `infiniteScroll` meaning that as the user scrolls the timeline back or forward in time,
  the "window" of time encapsulated by the time axis is moved. Added `infinite-scroll` demo. ([#3048](https://github.com/bryntum/support/issues/3048))
* The `TaskResize` feature now uses the task's data to change the appearance by updating `endDate` live but in batched
  mode so that the changes are not propagated until the operation is finished. ([#2541](https://github.com/bryntum/support/issues/2541))
* Dependencies can now be created by dropping on the target task without hitting the terminal circle element. The
  defaultValue of the DependencyModel `type` field will be used in this case. ([#3003](https://github.com/bryntum/support/issues/3003))
* Dependency creation can now be finalized asynchronously, for example after showing the user a confirmation dialog
* Name column in predecessors / successors grid is now editable by default for easy filtering of tasks ([#3045](https://github.com/bryntum/support/issues/3045))
* Added "Upgrade Font Awesome icons to Pro version" guide
* Added "Replacing Font Awesome with Material Icons" guide
* [FRAMEWORKS] Added SchedulerPro component wrappers and `gantt-schedulerpro` demos for frameworks ([#2970](https://github.com/bryntum/support/issues/2970))

### LOCALE UPDATES

* `removeRows` label of CellMenu & GridBase was removed
* Value of `removeRow` label of CellMenu & GridBase was updated to say just 'Remove'
* RowCopyPaste locales were updated to just say 'Copy', 'Cut' & 'Paste'. `copyRows`, `cutRows` & `pasteRows` keys were
  removed
* EventCopyPaste locales were updated to just say 'Copy', 'Cut' & 'Paste'. `copyRows`, `cutRows` & `pasteRows` keys were
  removed
* TaskCopyPaste locales were updated to just say 'Copy', 'Cut' & 'Paste'. `copyRows`, `cutRows` & `pasteRows` keys were
  removed
* Gantt `Edit` text was updated to be just 'Edit'
* Gantt `Delete task` text was updated to be just 'Delete'

### BUG FIXES

* [#164](https://github.com/bryntum/support/issues/164) - No drag proxy visible while setting start/end dates by dragging
* [#516](https://github.com/bryntum/support/issues/516) - Cannot create dependencies if showCreationTooltip is false
* [#3074](https://github.com/bryntum/support/issues/3074) - Throwing exception when create dependency on drag
* [#3091](https://github.com/bryntum/support/issues/3091) - Wrong label for Delete task when several tasks are selected
* For more details, see [What's new](https://bryntum.com/products/gantt/docs/guide/Gantt/whats-new/4.2.0) and
  [Upgrade guide](https://bryntum.com/products/gantt/docs/guide/Gantt/upgrades/4.2.0) in docs

## 4.1.6 - 2021-06-23

### FEATURES / ENHANCEMENTS

* TaskEdit has a new `scrollIntoView` boolean config allowing to opt-out of scrolling the task being edited into view
  ([#997](https://github.com/bryntum/support/issues/997))
* Indicators feature now support a `tooltipTemplate` defining the tooltip markup ([#3032](https://github.com/bryntum/support/issues/3032))

### BUG FIXES

* [#2267](https://github.com/bryntum/support/issues/2267) - Parent row is expanded on task drop, even if task is added as a sibling
* [#2738](https://github.com/bryntum/support/issues/2738) - Not possible to set initial value for combo if ajaxstore used
* [#3005](https://github.com/bryntum/support/issues/3005) - [VUE-3] Problem with Critical Paths due to Vue Proxy and double native events firing bug
* [#3012](https://github.com/bryntum/support/issues/3012) - Labels demo functionality is broken
* [#3014](https://github.com/bryntum/support/issues/3014) - Collapsed tasks do not appear as options in dependency column editors
* [#3024](https://github.com/bryntum/support/issues/3024) - Task context menu should hide when drag drop starts on touch device
* [#3026](https://github.com/bryntum/support/issues/3026) - [VUE-2] and [VUE-3] typescript type declarations are missing
* [#3028](https://github.com/bryntum/support/issues/3028) - Parent task turned into leaf after removing child task
* [#3029](https://github.com/bryntum/support/issues/3029) - Child nodes not removed after collapsing parent node in tree grid
* [#3030](https://github.com/bryntum/support/issues/3030) - editorClass has no effect on TaskEdit config
* [#3041](https://github.com/bryntum/support/issues/3041) - Should be possible to define a type on editorConfig of TaskEdit feature

## 4.1.5 - 2021-06-09

### FEATURES / ENHANCEMENTS

* Gantt now has a `minHeight` of `10em` by default. This assures that the Gantt will get a size even if no other sizing
  rules are applied for the element it is rendered to. When the default `minHeight` is driving the height, a warning is
  shown on the console to let the dev know that sizing rules are missing. The warning is not shown if a `minHeight` is
  explicitly configured ([#2915](https://github.com/bryntum/support/issues/2915))
* [TYPINGS] API singleton classes are correctly exported to typings ([#2752](https://github.com/bryntum/support/issues/2752))

### BUG FIXES

* [#674](https://github.com/bryntum/support/issues/674) - Setting field value or visibility does not work in beforeTaskEditShow when field has "name" property
  specified
* [#2889](https://github.com/bryntum/support/issues/2889) - [ANGULAR] Project model event listeners do not fire on production angular build
* [#2955](https://github.com/bryntum/support/issues/2955) - Constraint type column combo filtering issue
* [#2985](https://github.com/bryntum/support/issues/2985) - RowReorder drag proxy element misplaced
* [#2986](https://github.com/bryntum/support/issues/2986) - ResourceAssigmentColumn initials duplicated after resizing column
* [#2990](https://github.com/bryntum/support/issues/2990) - [ANGULAR] Preventable async events don't work
* [#3001](https://github.com/bryntum/support/issues/3001) - Excel export - excel reports error when percent done column is exported

## 4.1.4 - 2021-05-28

### FEATURES / ENHANCEMENTS

* TypeScript definitions updated to use typed `Partial<>` parameters where available
* New migration guide describing how to migrate an Ext JS-based Gantt demo app to use Bryntum Gantt ([#378](https://github.com/bryntum/support/issues/378))
* You can now access the combo box for the "Add New" column using its `combo` property ([#2938](https://github.com/bryntum/support/issues/2938))
* Buttons now has a new style `b-transparent` that renders them without background or borders ([#2853](https://github.com/bryntum/support/issues/2853))
* [NPM] repository package `@bryntum/gantt` now includes source code ([#2723](https://github.com/bryntum/support/issues/2723))
* [NPM] repository package `@bryntum/gantt` now includes minified versions of bundles ([#2842](https://github.com/bryntum/support/issues/2842))
* [FRAMEWORKS] Frameworks demos packages dependencies updated to support Node v12

### API CHANGES

* CSS classes for baseline elements changed slightly so make sure to revise any styling you have used based on
  `b-baseline-milestone` CSS class (removed). Each baseline task now has `b-task-baseline` and milestones have
  `b-task-baseline-milestone`

### BUG FIXES

* [#1848](https://github.com/bryntum/support/issues/1848) - Manually scheduled summary tasks slack value
* [#2104](https://github.com/bryntum/support/issues/2104) - "Core" code not isomorphic
* [#2119](https://github.com/bryntum/support/issues/2119) - Manually scheduled tasks are always critical
* [#2702](https://github.com/bryntum/support/issues/2702) - Baseline duration is calculated differently than for regular tasks
* [#2775](https://github.com/bryntum/support/issues/2775) - Combo replaces its store data on set value if filterParamName defined
* [#2828](https://github.com/bryntum/support/issues/2828) - Memory leak when replacing project instance
* [#2834](https://github.com/bryntum/support/issues/2834) - Core should not use b-fa for icon prefix
* [#2914](https://github.com/bryntum/support/issues/2914) - Crash after destroying Gantt with custom tip config object
* [#2921](https://github.com/bryntum/support/issues/2921) - Timeaxis configuration error on wrongly calculated project date range
* [#2931](https://github.com/bryntum/support/issues/2931) - Baseline milestone lack unique CSS class for styling

## 4.1.3 - 2021-05-13

### FEATURES / ENHANCEMENTS

* Percent Bar feature allows to use a custom field instead of percentDone ([#2739](https://github.com/bryntum/support/issues/2739))
* Bumped the built in version of FontAwesome Free to 5.15.3 and added missing imports to allow stacked icons etc
* Bumped the `@babel/preset-env` config target to `chrome: 75` for the UMD and Module bundles. This decreased bundle
  sizes and improved performance for modern browsers
* TaskResize now has a configurable `tooltipTemplate` so you can easily show custom contents in the resizing tooltip
  See updated 'tooltips' demo to try it out ([#2244](https://github.com/bryntum/support/issues/2244))
* Updated Angular Wrappers to be compatible with Angular 6-7 in production mode for target `es2015`

### API CHANGES

* The locale key that defines the width of the task editor was moved to Scheduler Pro, since it can also use Gantt's
  task editor. If you are using a locale based custom width for it, you will need to update your locale. Please see the
  upgrade guide ([#2789](https://github.com/bryntum/support/issues/2789))
* [DEPRECATED] TaskDrag#dragTipTemplate was renamed to `tooltipTemplate` to better match the naming scheme of other
  features
* [DEPRECATED] The `startText`, `endText`, `startClockHtml`, `endClockHtml`, `dragData` params of the TaskDrag
  dragTipTemplate / tooltipTemplate methods have been deprecated and will be removed in 5.0

### BUG FIXES

* [#2313](https://github.com/bryntum/support/issues/2313) - Filter operator should use contains search if "*" used
* [#2543](https://github.com/bryntum/support/issues/2543) - Task editor doesn't reset not saved data if clicked Cancel and reopen
* [#2604](https://github.com/bryntum/support/issues/2604) - Gantt trial LWC does not render task elements
* [#2665](https://github.com/bryntum/support/issues/2665) - Timeline does not render events when used as container item
* [#2766](https://github.com/bryntum/support/issues/2766) - Max call stack error if schedulingconflict event has listeners
* [#2772](https://github.com/bryntum/support/issues/2772) - When task resize feature is disabled, cursor should not change to ew-resize in start-resize areas
* [#2773](https://github.com/bryntum/support/issues/2773) - Uncaught TypeError with certain feature combinations
* [#2776](https://github.com/bryntum/support/issues/2776) - WBS column exports padded value
* [#2778](https://github.com/bryntum/support/issues/2778) - Wrong module declaration in typings file
* [#2818](https://github.com/bryntum/support/issues/2818) - Milliseconds are not displayed correctly in Gantt headers
* [#2871](https://github.com/bryntum/support/issues/2871) - Export - Percent done column doesn't render when showCircle is true

## 4.1.2 - 2021-04-27

### BUG FIXES

* [#2760](https://github.com/bryntum/support/issues/2760) - [WRAPPERS] Missing taskDragFeature, TaskDragCreateFeature, TaskResizeFeature configs in Gantt
* [#2761](https://github.com/bryntum/support/issues/2761) - Task editor padding missing

## 4.1.1 - 2021-04-23

### FEATURES / ENHANCEMENTS

* Scheduler / Gantt / Calendar will now react when CTRL-Z key to undo / redo recent changes made. Behavior can be
  controlled with the new `enableUndoRedoKeys` config ([#2532](https://github.com/bryntum/support/issues/2532))
* New React Gantt Resource Histogram demo has been added

### BUG FIXES

* [#868](https://github.com/bryntum/support/issues/868) - Should be possible to show all available context menus programmatically
* [#1913](https://github.com/bryntum/support/issues/1913) - "Already entered replica" error when setting taskStore.data with syncDataOnLoad flag
* [#1987](https://github.com/bryntum/support/issues/1987) - DOCS: React guide needs a section on how to listen for events
* [#2138](https://github.com/bryntum/support/issues/2138) - ResourceHistogram is not refreshed after inline data reset and load again
* [#2266](https://github.com/bryntum/support/issues/2266) - Extra icons are displayed during row reordering in advanced demo
* [#2488](https://github.com/bryntum/support/issues/2488) - TaskEditor tab configuration in beforeTaskEditShow does not work correct
* [#2493](https://github.com/bryntum/support/issues/2493) - GeneralTab defines the height of the TaskEditor
* [#2538](https://github.com/bryntum/support/issues/2538) - Error when closing assignment field picker with filter applied
* [#2618](https://github.com/bryntum/support/issues/2618) - Crash when using Gantt without 'dependenciesFeature'
* [#2635](https://github.com/bryntum/support/issues/2635) - Milestone Resize Error
* [#2636](https://github.com/bryntum/support/issues/2636) - [WRAPPERS] Features are not updated at runtime
* [#2647](https://github.com/bryntum/support/issues/2647) - Timeline doesn't render without appendTo config
* [#2679](https://github.com/bryntum/support/issues/2679) - on-owner events should be added to owner too in docs
* [#2681](https://github.com/bryntum/support/issues/2681) - Yarn. Package trial alias can not be installed
* [#2728](https://github.com/bryntum/support/issues/2728) - Lag unit should be based on task durationUnit (if omitted) when setting lag for predecessors/successors

## 4.1.0 - 2021-04-02

### FEATURES / ENHANCEMENTS

* We are happy to announce that Bryntum Gantt now can be directly installed using our npm registry
  We've updated all our frameworks demos to use `@bryntum` npm packages. See them in `examples/frameworks` folder
  Please refer to "Npm packages" guide in docs for registry login and usage information
* Improved styling guide with more examples
* New "custom-taskbar" example showing how to customize the task bar to include a number for every time axis tick
  ([#2572](https://github.com/bryntum/support/issues/2572))
* ProjectModel now exposes a `changes` property returning an object with the current changes in its stores
* Bryntum demos were updated with XSS protection code. `StringHelper.encodeHtml` and `StringHelper.xss` functions were
  used for this
* Parent task bars now have 2em max-height to look nicer in taller rows
* Added new Vue Cell Renderer demo to show Vue Components as cell renderers (Partial fix [#946](https://github.com/bryntum/support/issues/946))
* Added new Vue 3 example for Gantt ([#1435](https://github.com/bryntum/support/issues/1435))
* `ResourceAssignmentColumn` now shows resource initials if no avatar image exists. `ResourceAssignmentGrid` resource
  column now shows a resource avatar, or initials if no avatar image exists ([#2202](https://github.com/bryntum/support/issues/2202))
* Updated React SharePoint Fabric demo to use Gantt toolbar component
* Added new React 17 demo for Gantt with Timeline widget. The example also implements theme switching ([#1823](https://github.com/bryntum/support/issues/1823) and
  [#2213](https://github.com/bryntum/support/issues/2213))
* Added new Vue 3 Simple demo to show how to use Bryntum Gantt in Vue 3 ([#1315](https://github.com/bryntum/support/issues/1315))
* Rollups Angular demo was updated for Angular 10 ([#2361](https://github.com/bryntum/support/issues/2361))
* Updated Angular/React/Vue frameworks Advanced demos to not use wrapping Panel ([#2165](https://github.com/bryntum/support/issues/2165))

### API CHANGES

* [BREAKING] Removed RequireJS demos and integration guides in favor of modern ES6 Modules technology ([#1963](https://github.com/bryntum/support/issues/1963))
* [BREAKING] `init` method is no longer required in Lightning Web Components and was removed from the LWC bundle
* [DEPRECATED] CrudManager/ProjectModel `commit` was deprecated in favor of `acceptChanges`
* [DEPRECATED] CrudManager/ProjectModel `commitCrudStores` was deprecated in favor of `acceptChanges`
* [DEPRECATED] CrudManager/ProjectModel `reject` was deprecated in favor of `revertChanges`
* [DEPRECATED] CrudManager/ProjectModel `rejectCrudStores` was deprecated in favor of `revertChanges`
* [DEPRECATED] In the `DependencyCreation` feature, the `data` param of all events was deprecated. All events now have
  useful documented top level params

### BUG FIXES

* [#888](https://github.com/bryntum/support/issues/888) - Copy / Cut / Paste task API + context menu entries
* [#1525](https://github.com/bryntum/support/issues/1525) - Improve Localization guide
* [#1678](https://github.com/bryntum/support/issues/1678) - The dirty indicator is not shown after changing duration when showDirty is enabled
* [#1689](https://github.com/bryntum/support/issues/1689) - Investigate sharing static resource between multiple LWC on the same page
* [#1964](https://github.com/bryntum/support/issues/1964) - Selecting one record highlights two elements on specific screen size
* [#1968](https://github.com/bryntum/support/issues/1968) - MS Project import demo should make imported data phantom
* [#1983](https://github.com/bryntum/support/issues/1983) - [REACT] JSX renderer not supported for TreeColumn
* [#2065](https://github.com/bryntum/support/issues/2065) - Adding manually scheduled parent task as predecessor from the cell editor fails
* [#2089](https://github.com/bryntum/support/issues/2089) - Adding new assignments from ResourceAssignmentColumn should respect data field mapping
* [#2117](https://github.com/bryntum/support/issues/2117) - TaskStore reports having changes after filtering tasks
* [#2203](https://github.com/bryntum/support/issues/2203) - Improve Project data loading / syncing docs
* [#2211](https://github.com/bryntum/support/issues/2211) - Add test coverage for XSS
* [#2220](https://github.com/bryntum/support/issues/2220) - Exception when modifying new task in advanced demo
* [#2221](https://github.com/bryntum/support/issues/2221) - Task start/end/duration go blank when pressing Enter in duration field of new task
* [#2226](https://github.com/bryntum/support/issues/2226) - Undo button reacts when critical path button is clicked
* [#2297](https://github.com/bryntum/support/issues/2297) - Hiding task menu subitems is broken
* [#2249](https://github.com/bryntum/support/issues/2249) - MSP importer returns success response with java exception as data
* [#2323](https://github.com/bryntum/support/issues/2323) - Dependency drag creation fails
* [#2330](https://github.com/bryntum/support/issues/2330) - Assignment change not recorded correctly
* [#2335](https://github.com/bryntum/support/issues/2335) - Changes returned in onSync are sent back in the next onSync call
* [#2342](https://github.com/bryntum/support/issues/2342) - Wrong validation successor when change successor to dependent task
* [#2351](https://github.com/bryntum/support/issues/2351) - Crash when changing Start Date after setting new project
* [#2359](https://github.com/bryntum/support/issues/2359) - Update readme files in all framework demos in all products
* [#2363](https://github.com/bryntum/support/issues/2363) - TaskModel should have public "expanded" field
* [#2372](https://github.com/bryntum/support/issues/2372) - TaskEditor doesn't reset cell editor on reopen if invalid value entered
* [#2373](https://github.com/bryntum/support/issues/2373) - Some cyclic dependencies for Successors incorrectly validated by TaskEditor
* [#2377](https://github.com/bryntum/support/issues/2377) - Prevent End Date from being set before start date
* [#2379](https://github.com/bryntum/support/issues/2379) - Add minified version of *.lite.umd.js to the bundle
* [#2382](https://github.com/bryntum/support/issues/2382) - project.load() throws an error when called on filtered store
* [#2400](https://github.com/bryntum/support/issues/2400) - Sync failure messages displayed in `syncMask` where not auto-closing
* [#2420](https://github.com/bryntum/support/issues/2420) - Duration filter is not restored from state properly when custom filterFn is specified
* [#2434](https://github.com/bryntum/support/issues/2434) - Content encoding issues (XSS related)
* [#2441](https://github.com/bryntum/support/issues/2441) - Demo control sizes and styling issues
* [#2460](https://github.com/bryntum/support/issues/2460) - Comma should be a valid decimal separator in DurationField
* [#2464](https://github.com/bryntum/support/issues/2464) - WBS field is now updated correctly on indent/outdent
* [#2486](https://github.com/bryntum/support/issues/2486) - Month/year picker is not aligned to date picker properly
* [#2491](https://github.com/bryntum/support/issues/2491) - Make a field on a dependency model to disable it
* [#2492](https://github.com/bryntum/support/issues/2492) - Removed dependency is rendered
* [#2517](https://github.com/bryntum/support/issues/2517) - Crash when deleting task
* [#2578](https://github.com/bryntum/support/issues/2578) - Progress circle display artefacts

## 4.0.8 - 2021-01-27

### FEATURES / ENHANCEMENTS

* Task Editor start/end date fields now support entering of time if they are configured with `keepTime` config as
  `entered` ([#1685](https://github.com/bryntum/support/issues/1685)) and if the fields `format` includes time info
* Project (Crud Manager) now supports less strict `sync` response format allowing to respond only server side changes
  See `supportShortSyncResponse` config for details
* Added preventable beforeIndent/beforeOutdent events on TaskStore ([#2288](https://github.com/bryntum/support/issues/2288))

### API CHANGES

* [BREAKING] Project (Crud Manager) default behaviour has been changed to allow `sync` response to include only
  server-side changes
  Previously it was mandatory to mention each updated/removed record in the response to confirm the changes
  With this release the project automatically confirms changes of all updated/removed records mentioned in
  corresponding request
  To revert to previous strict behaviour please use `supportShortSyncResponse` config

### BUG FIXES

* [#1970](https://github.com/bryntum/support/issues/1970) - Infinite requests if wrong response received
* [#2214](https://github.com/bryntum/support/issues/2214) - Exception when creating new task tree in PHP demo
* [#2217](https://github.com/bryntum/support/issues/2217) - Task editor throws when constraint type field is disabled
* [#2263](https://github.com/bryntum/support/issues/2263) - Dependency is not redrawn after changing dependency type
* [#2286](https://github.com/bryntum/support/issues/2286) - Non persistable field modification should not add record to update request package if `writeAllFields`
  is true

## 4.0.7 - 2021-01-12

### BUG FIXES

* [#1815](https://github.com/bryntum/support/issues/1815) - Dependency lines missing after changing project start date on filtered task store
* [#2118](https://github.com/bryntum/support/issues/2118) - Undo/redo popup titles are not localized
* [#2178](https://github.com/bryntum/support/issues/2178) - ResourceAssignmentColumn#itemTpl config is broken

## 4.0.6 - 2020-12-29

### FEATURES / ENHANCEMENTS

* The picker for DependencyField now also displays a dependencies identifier, as defined by the `dependencyIdField`
  config ([#2078](https://github.com/bryntum/support/issues/2078))
* Added support of imageUrl field to ResourceAssignmentColumn ([#1914](https://github.com/bryntum/support/issues/1914))
* Tasks in the predecessor / successor tabs in the TaskEditor are now sorted by name
* Tasks in the name column combo editor inside the predecessor / successor tabs in the TaskEditor are now sorted by name
  ([#1790](https://github.com/bryntum/support/issues/1790))

### BUG FIXES

* [#1388](https://github.com/bryntum/support/issues/1388) - Dependency creation tooltip styling broken in Safari
* [#1992](https://github.com/bryntum/support/issues/1992) - Manually scheduled summary tasks should allow editing of their duration and end date
* [#1993](https://github.com/bryntum/support/issues/1993) - Port task/dependency isEditable method from Ext Gantt
* [#2025](https://github.com/bryntum/support/issues/2025) - Predecessor and Successor don't work as expected with locales
* [#2060](https://github.com/bryntum/support/issues/2060) - Add sequenceNumber column to examples
* [#2095](https://github.com/bryntum/support/issues/2095) - Task editor shows incorrect set of constraints for the parent task
* [#2136](https://github.com/bryntum/support/issues/2136) - Changing task dependencies in cell editor leads to remove + add actions
* [#2171](https://github.com/bryntum/support/issues/2171) - Resource assignment cell content is rendered incorrectly for the first task

## 4.0.5 - 2020-12-15

### API CHANGES

* [DEPRECATED] `TaskModel#wbsIndex` is deprecated in favor of `TaskModel#wbsValue`

### BUG FIXES

* [#1314](https://github.com/bryntum/support/issues/1314) - Fix for ASPNET demo build in Windows cmd environment
* [#1938](https://github.com/bryntum/support/issues/1938) - Sorter should be reapplied after initial project calculation
* [#2050](https://github.com/bryntum/support/issues/2050) - Duration filter is applied incorrectly
* [#2070](https://github.com/bryntum/support/issues/2070) - wbsIndex field removed by mistake
* [#2079](https://github.com/bryntum/support/issues/2079) - WBS filter doesn't work from the context menu
* [#2080](https://github.com/bryntum/support/issues/2080) - WBS filter is not applied correctly
* [#2081](https://github.com/bryntum/support/issues/2081) - TaskModel.setBaseline call with the index greater than 1 when task has no baselines fails
* [#2087](https://github.com/bryntum/support/issues/2087) - CellEdit#addNewAtEnd not respected

## 4.0.4 - 2020-12-09

### API CHANGES

* TaskEdit feature now exposes an 'isEditing' boolean to detect if the editor is currently visible ([#1935](https://github.com/bryntum/support/issues/1935))

### FEATURES / ENHANCEMENTS

* Added config to specify allowed units (`DurationField.allowedUnits`) for the duration field ([#1891](https://github.com/bryntum/support/issues/1891))
* A new config `discardPortals` on the React wrapper, that controls the behaviour of cell renderers using React
  components. Set to `false` (default) to enhance performance. Set to `true` to limit memory consumption
* A new config dependencyIdField to set the Task field to use when creating / displaying a dependency between two
  tasks ([#681](https://github.com/bryntum/support/issues/681))

### BUG FIXES

* [#1229](https://github.com/bryntum/support/issues/1229) - Dependency fields fromTask/toTask are not serialized
* [#1485](https://github.com/bryntum/support/issues/1485) - Task Editor centered config doesn't have effect
* [#1812](https://github.com/bryntum/support/issues/1812) - Make tables look better in docs
* [#1842](https://github.com/bryntum/support/issues/1842) - Resource assignment column does not trigger beforeFinishCellEdit and finishCellEdit events
* [#1869](https://github.com/bryntum/support/issues/1869) - Very low performance of React cell renderers
* [#1881](https://github.com/bryntum/support/issues/1881) - Incorrect response to CrudManager sync leads to infinite request loop
* [#1885](https://github.com/bryntum/support/issues/1885) - WBS column filter returns no results when you type visible WBS value
* [#1920](https://github.com/bryntum/support/issues/1920) - Crash when setting constraint
* [#1931](https://github.com/bryntum/support/issues/1931) - Gantt demos localization issues
* [#1934](https://github.com/bryntum/support/issues/1934) - Crash when replacing gantt dataset while task editor is open
* [#1940](https://github.com/bryntum/support/issues/1940) - TaskEditor´s beforeClose event is not triggered when clicking on Cancel button
* [#1946](https://github.com/bryntum/support/issues/1946) - Multi sort UI is broken columns with `sortable` function
* [#1962](https://github.com/bryntum/support/issues/1962) - Dependencies are not refreshed when replace resources
* [#2013](https://github.com/bryntum/support/issues/2013) - Code editor styles broken in Custom Task menu demo
* [#2020](https://github.com/bryntum/support/issues/2020) - Spin up does not honor instantUpdate setting
* [#2026](https://github.com/bryntum/support/issues/2026) - Row reorder broken when header menu is disabled

## 4.0.3 - 2020-11-17

### FEATURES / ENHANCEMENTS

* A new Scheduler widget type `undoredo` has been added which, when added to the `tbar` of a scheduling widget
  (such as a `Scheduler`, `Gantt`, or `Calendar`), provides undo and redo functionality
* `gantt.umd.js` and `gantt.lite.umd.js` bundles are now compiled with up-to-date `@babel/preset-env` webpack preset
  with no extra polyfilling. This change decreases size for the bundle by ~20% and offers performance enhancements for
  supported browsers
* [DEPRECATED] `gantt.lite.umd.js` was deprecated in favor of `gantt.umd.js` and will be removed in version 5.0
* The behaviour of our WBS column now more closely matches that of MS Project, keeping the initial values when sorting
  and filtering instead of always generating new ([#1788](https://github.com/bryntum/support/issues/1788))

### BUG FIXES

* [#1681](https://github.com/bryntum/support/issues/1681) - React applications are compiled with patched `react-scripts` presets. Check
  `examples/react/_scripts/readme.md` for more information
* [#1724](https://github.com/bryntum/support/issues/1724) - Crash after drag drop with manually scheduled parent task
* [#1773](https://github.com/bryntum/support/issues/1773) - Duration column filter issue
* [#1774](https://github.com/bryntum/support/issues/1774) - Filter popup closes when field trigger is clicked
* [#1809](https://github.com/bryntum/support/issues/1809) - Browser hangs on when certain data is loaded
* [#1838](https://github.com/bryntum/support/issues/1838) - Resource assignment column sorting doesn't work
* [#1849](https://github.com/bryntum/support/issues/1849) - Rollups and baselines combined are overlapping
* [#1866](https://github.com/bryntum/support/issues/1866) - Negative Lag calculation is broken
* [#1867](https://github.com/bryntum/support/issues/1867) - TaskEditor will not show if it ever hides without stopping editing

## 4.0.2 - 2020-11-04

### BUG FIXES

* [#1745](https://github.com/bryntum/support/issues/1745) - MS Project Export version 2013 compatibility
* [#1787](https://github.com/bryntum/support/issues/1787) - After loading a Project without date range set on the Gantt chart, it should set Gantt start/end dates
  based on the project

## 4.0.1 - 2020-11-03

### FEATURES / ENHANCEMENTS

* Constraint type column now has a proper list based filter ([#1772](https://github.com/bryntum/support/issues/1772))
* Resource assignment column is now filterable ([#1767](https://github.com/bryntum/support/issues/1767))

### API CHANGES

* [BREAKING] AssignmentField now has a picker which is an AssigmentGrid, where as previously the field's picker
  *contained* the grid. Please update any configuration code for the 'grid' of the picker to instead configure the
  picker directly. See the upgrade guide for more information

### BUG FIXES

* [#1377](https://github.com/bryntum/support/issues/1377) - Grouping and sorting is available in predecessors grid header context menu, but features don't work
* [#1669](https://github.com/bryntum/support/issues/1669) - Assignment picker malfunctioning if groups are collapsed
* [#1706](https://github.com/bryntum/support/issues/1706) - Toolbar should not be exported
* [#1708](https://github.com/bryntum/support/issues/1708) - Changing rowHeight and barMargin breaks dependency rendering
* [#1712](https://github.com/bryntum/support/issues/1712) - Skip non-exportable columns in export dialog window
* [#1723](https://github.com/bryntum/support/issues/1723) - Exception when exporting gantt to multiple pages

## 4.0.0 - 2020-10-19

### FEATURES / ENHANCEMENTS

* [BREAKING] Dropped Support for Edge 18 and older. Our Edge <=18 fixes are still in place and active, but we will not
  be adding more fixes. Existing fixes will be removed in a later version
* [BREAKING] The `Core/adapter` directory has been removed. There are no Widget adapters. All Widget classes register
  themselves  with the `Widget` class, and the `Widget` class is the source of Widget `type` mapping and Widget
  registration and lookup by `id`
* Gantt ships with a new version of the built in calculation engine, which should greatly improve its performance in
  many scenarios - especially on larger datasets. A lighter version of the engine is also used in the new Scheduler Pro,
  allowing them to pair up easily. To highlight this, a new `gantt-schedulerpro` demo was added. It shows Gantt sharing
  a project with Scheduler Pro ([#892](https://github.com/bryntum/support/issues/892))
* Context menu features refactoring: naming was simplified by removing the word "Context" in feature names and in event
  names, introduced named objects for menu items, split context menu features by area of responsibility, made TaskMenu
  feature responsible for cell menu items. Please check out the upgrade guide for details ([#128](https://github.com/bryntum/support/issues/128))
* New `resourcehistogram` demo showing the Histogram widget paired with the Gantt chart
* Added new exporter: MultiPageVertical. It fits content horizontally and then generates vertical pages to fit
  vertical content. ([#1092](https://github.com/bryntum/support/issues/1092))
* Added new configuration toggleParentTasksOnClick. It disables the default behaviour of collapsing/expanding parent
  tasks when clicking their task bar. ([#1240](https://github.com/bryntum/support/issues/1240))
* Added new exporter feature: mspExport. It generates a XML file locally that can be imported in Microsoft Project. A
  new `msprojectexport` demo was added. ([#1250](https://github.com/bryntum/support/issues/1250))
* Gantt now extends `Panel` instead of `Container`. This allows you to easily add toolbars to it ([#1417](https://github.com/bryntum/support/issues/1417))
* New custom-rendering demo showing how to add custom HTML markup to the task bar content element
* Added `gantt.lite.umd.js` module that does not include `Promise` polyfill. This module is primarily intended to be
  used with Angular to prevent `zone.js` polyfills overwrite
* Added a property to get critical paths information from the project. See the Gantt `ProjectModel.criticalPaths` for
  details
* Added a new way of cycles handling that basically ignores them. That mode is used by default for data loading stage
  So instead of throwing exceptions when loading the data, cycles will be reported in the browser console (fixed [#1640](https://github.com/bryntum/support/issues/1640))
* Assigning a calendar to a task/resource/project model (via `setCalendar` call or using `calendar` property)
  will automatically cause adding of the calendar to the calendar manager store
* Calendars guide has been updated: added more examples and details on calendars effect on the scheduling process
* Added experimental support for Salesforce Locking Service ([#359](https://github.com/bryntum/support/issues/359)). The distributed bundle only supports modern
  browsers (no IE11 or non-chromium based Edge), since Salesforce drops support for these in January 1st 2021 too
* Added Lightning Web Component demo, see `examples/salesforce/src/lwc`

### API CHANGES

* [BREAKING] `TaskModel.calendar` property behavior has been changed. Now it returns the task own calendar only (so it
  could be `null` if the task has no own calendar specified). To get the effective calendar used by the task please use
  `TaskModel.effectiveCalendar` which refers to the actual calendar used by the task (either the tasks own calendar if
  provided or the project calendar)
* [BREAKING] The `Default`, `Light` and `Dark` themes were renamed to `Classic`, `Classic-Light` and `Classic-Dark`
  This change highlights the fact that they are variations of the same theme, and that it is not the default theme
  (Stockholm is our default theme since version 2.0)
* [DEPRECATED] `TaskContextMenu` feature was renamed to `TaskMenu`
* [DEPRECATED] `TaskEditor#durationDecimalPrecision` config was renamed to `durationDisplayPrecision`. Its value
  is now always provided from the Gantt instance upon creation
* Cell context menu items are handled by `TaskMenu` feature now, because grid row and all cells in it represent a task
  record (Partial fix [#128](https://github.com/bryntum/support/issues/128))
* Localization `GanttCommon.dependencyTypes` moved to `DependencyType.short`
* Cycle exceptions format has been changed. Now it consists of two parts
  The first part is less detailed and more user friendly. It just enumerates tasks that build the cycle
  And second part shows full list of involved identifiers for those who need detailed info
* Task menu for the TimeAxis schedule area is shown by default now. For details, see the `enableCellContextMenu` of
  `Gantt.column.TimeAxisColumn`
* Gantt `CrudManager` class has beed removed in favor of `ProjectModel` using which also implements Crud Manager API
* Propagation caused by data loading has been changed and now supports two alternative ways (fixed [#1346](https://github.com/bryntum/support/issues/1346))
  The changes happened due to the propagation are applied either:
  1. silently: no store `change`/`update` events are triggered, records aren't modified after the propagation. This mode
     is used by default
  2. not silently: store `change`/`update` events are triggered, records are modified after the propagation. Which in
     turn might cause data persisting if project `autoSync` is enabled
  Please check Project `silenceInitialCommit` config for details
* Gantt's "main" stores (EventStore, ResourceStore, AssignmentStore and DependencyStore) has had their event
  triggering modified to make sure data is in a calculated state when relevant events are triggered. This affects the
  timing of the `add`, `remove`, `removeAll`, `change` and `refresh` events. Please see the upgrade guide for more
  information ([#1486](https://github.com/bryntum/support/issues/1486))
* Model fields in derived classes are now merged with corresponding model fields (by name) in super classes. This allows
  serialization and other attributes to be inherited when a derived class only wants to change the `defaultValue` or
  other attribute of the field
* The `dateFormat` config for `type='date'` model fields has been simplified to `format`
* Model date fields are serialized by default according to the field `format`
* Field `serialize` function `this` has been changed to refer the field definition (it used to refer the model instance
  before)
* The following previously deprecated members/classes was removed:
  - `TaskEditorTab`
  - `CalendarField`*
  - `ConstraintTypePicker`*
  - `DependencyTypePicker`*
  - `EffortField`*
  - `ModelCombo`*
  - `SchedulingModePicker`*
  - `EventLoader`*
  - `ReadyStatePropagator`*
  - `AdvancedTab`*
  - `DependencyTab`*
  - `FormTab`*
  - `GeneralTab`*
  - `NotesTab`*
  - `PredecessorsTab`*
  - `ResourcesTab`*
  - `SuccessorsTab`*

  \* - removed from Gantt sources but still available from SchedulerPro

### BUG FIXES

* [#267](https://github.com/bryntum/support/issues/267) - Preserve endDate for "Manually scheduled" summary task
* [#534](https://github.com/bryntum/support/issues/534) - Dependency line jumps while scrolling vertically
* [#812](https://github.com/bryntum/support/issues/812) - Adding incorrect predecessor throws exception
* [#853](https://github.com/bryntum/support/issues/853) - Add-> Predecessor from context menu stuck in disabled mode
* [#856](https://github.com/bryntum/support/issues/856) - Sync triggered (fromSide and toSide removed) after opening and saving unmodified dependency
* [#958](https://github.com/bryntum/support/issues/958) - Column sorting causes record modifications
* [#1001](https://github.com/bryntum/support/issues/1001) - Typings missing for Gantt
* [#1043](https://github.com/bryntum/support/issues/1043) - Critical paths lines stay highlighted after move or resize the task to non critical
* [#1046](https://github.com/bryntum/support/issues/1046) - Critical milestone gets extra background applied
* [#1049](https://github.com/bryntum/support/issues/1049) - Unintended baseline transitions on drop
* [#1074](https://github.com/bryntum/support/issues/1074) - Duplicated task when reordering while a filter exists
* [#1090](https://github.com/bryntum/support/issues/1090) - Milestone task label overlaps dependency arrow
* [#1208](https://github.com/bryntum/support/issues/1208) - Should show indicators when are outside of timeline but in date interval
* [#1218](https://github.com/bryntum/support/issues/1218) - ComboBox list should be anchored to top/bottom sides only
* [#1249](https://github.com/bryntum/support/issues/1249) - Columns lines are not exported correctly
* [#1252](https://github.com/bryntum/support/issues/1252) - Adding predecessor removes dependency line to the successor
* [#1255](https://github.com/bryntum/support/issues/1255) - Zooming performance tuning
* [#1270](https://github.com/bryntum/support/issues/1270) - Adding a new task registered as 2 transactions when computer is slow
* [#1288](https://github.com/bryntum/support/issues/1288) - Gantt taskRender event does not pass task element
* [#1294](https://github.com/bryntum/support/issues/1294) - Dependencies line are not redrawn on sorting
* [#1331](https://github.com/bryntum/support/issues/1331) - Constraint removal is not revertable in task editor
* [#1340](https://github.com/bryntum/support/issues/1340) - TaskStore doesn't extend AjaxStore
* [#1347](https://github.com/bryntum/support/issues/1347) - Translation missing in bigdataset demo
* [#1348](https://github.com/bryntum/support/issues/1348) - Dependencies not rendered after changing to 10k tasks in bigdataset demo
* [#1383](https://github.com/bryntum/support/issues/1383) - Scale column in resource histogram should not be editable
* [#1387](https://github.com/bryntum/support/issues/1387) - Focus outline broken for parent tasks
* [#1390](https://github.com/bryntum/support/issues/1390) - Parent task not rendered in rollups demo
* [#1433](https://github.com/bryntum/support/issues/1433) - Critical paths lines stay partially gray when has more than 1 dependency
* [#1467](https://github.com/bryntum/support/issues/1467) - MSProject export demo issue
* [#1470](https://github.com/bryntum/support/issues/1470) - Replacing dataset is very slow
* [#1475](https://github.com/bryntum/support/issues/1475) - Deprecate tabsConfig and extraItems properly
* [#1540](https://github.com/bryntum/support/issues/1540) - Unquoted column.id in selectors
* [#1543](https://github.com/bryntum/support/issues/1543) - Paths disappearing on scroll
* [#1548](https://github.com/bryntum/support/issues/1548) - [ANGULAR] Investigate zone.js loading order and set it to Angular default
* [#1559](https://github.com/bryntum/support/issues/1559) - Show task menu for empty part of timeaxis
* [#1633](https://github.com/bryntum/support/issues/1633) - Re-applying JSON to DependencyStore removes newly created dependency
* [#1643](https://github.com/bryntum/support/issues/1643) - Propagating text not translated in big dataset demo
* [#1644](https://github.com/bryntum/support/issues/1644) - Fixed `NumberField` enforcement of min/max values to allow typing beyond those ranges
* [#1646](https://github.com/bryntum/support/issues/1646) - Error on project.setCalendar method call
* [#1652](https://github.com/bryntum/support/issues/1652) - Document STM on ProjectModel
* [#1657](https://github.com/bryntum/support/issues/1657) - Setting tasks on Vue wrapper reinitialises STM

## 2.1.9 - 2020-08-26

### FEATURES/ENHANCEMENTS

* No Gantt specific changes, but Grid and Scheduler changes are included

## 2.1.8 - 2020-08-11

### BUG FIXES

* [#1214](https://github.com/bryntum/support/issues/1214) - Gantt error on applyState
* [#1244](https://github.com/bryntum/support/issues/1244) - Initial export options are shown incorrectly in the export dialog
* [#1284](https://github.com/bryntum/support/issues/1284) - Exception when trying to open context menu on row border

## 2.1.7 - 2020-07-24

### BUG FIXES

* [#910](https://github.com/bryntum/support/issues/910) - Crash when exporting to PDF if schedule area has no width
* [#933](https://github.com/bryntum/support/issues/933) - Exported PDF corrupt after adding task
* [#953](https://github.com/bryntum/support/issues/953) - Load mask appearing on top of export progress
* [#969](https://github.com/bryntum/support/issues/969) - Multi page export of more than 100 tasks fails
* [#970](https://github.com/bryntum/support/issues/970) - Export feature yields corrupted PDF when chart is scrolled down
* [#972](https://github.com/bryntum/support/issues/972) - Export feature does not export dependencies unless visible first
* [#973](https://github.com/bryntum/support/issues/973) - Export feature does not respect left grid section width
* [#988](https://github.com/bryntum/support/issues/988) - Deleted tasks appear after reapplying filters
* [#1172](https://github.com/bryntum/support/issues/1172) - Wrapper should not relay store events to the instance
* [#1180](https://github.com/bryntum/support/issues/1180) - Exported grid should end with the last row

## 2.1.6 - 2020-07-10

### FEATURES/ENHANCEMENTS

* Added Docker image of the PDF Export Server. See server README for details. ([#905](https://github.com/bryntum/support/issues/905))

### API CHANGES

* [DEPRECATED] To avoid risk of confusing the Gantt instance with the calculation engine, `ganttEngine` has been
  deprecated in favor of `ganttInstance` for all framework wrappers (Angular, React, Vue). [#776](https://github.com/bryntum/support/issues/776)

### BUG FIXES

* [#858](https://github.com/bryntum/support/issues/858) - Sync tries to remove assignment added on a previous cancelled task edit
* [#968](https://github.com/bryntum/support/issues/968) - Task editing is broken after saving new resource
* [#984](https://github.com/bryntum/support/issues/984) - Indenting a lot of tasks causes incorrect indentation
* [#1056](https://github.com/bryntum/support/issues/1056) - enableCellContextMenu: false doesn't disable context menu in Gantt
* [#1131](https://github.com/bryntum/support/issues/1131) - Task editing is broken after canceling new resource
* [#1139](https://github.com/bryntum/support/issues/1139) - `Duration` column error Tooltip not show up when `finalizeCellEditor` returns false

## 2.1.5 - 2020-06-09

### FEATURES/ENHANCEMENTS

* Updated Font Awesome Free to v5.13.0

### BUG FIXES

* [#801](https://github.com/bryntum/support/issues/801) - Document wrapperCls param in taskRenderer
* [#815](https://github.com/bryntum/support/issues/815) - Gantt %-done bar should be semi-transparent
* [#827](https://github.com/bryntum/support/issues/827) - nonWorkingTime feature stops working with large resources
* [#838](https://github.com/bryntum/support/issues/838) - Unexpected lag between tasks with dependencies and assignment
* [#852](https://github.com/bryntum/support/issues/852) - Project lines appear even if feature is disabled
* [#859](https://github.com/bryntum/support/issues/859) - Crash when dragging task and mouse moves over timeline element
* [#860](https://github.com/bryntum/support/issues/860) - Crash if dragging task with dependency to a filtered out task
* [#862](https://github.com/bryntum/support/issues/862) - Crash if opening Gantt demo in Iran timezone

## 2.1.4 - 2020-05-19

### BUG FIXES

* [#772](https://github.com/bryntum/support/issues/772) - undefined query parameter in CrudManager URLs
* [#783](https://github.com/bryntum/support/issues/783) - Crash if schedule grid is collapsed with progressline enabled

## 2.1.3 - 2020-05-14

### BUG FIXES

* [#257](https://github.com/bryntum/support/issues/257) - Task not rendered correctly after drag drop
* [#268](https://github.com/bryntum/support/issues/268) - Wrong sync request on dependency creation
* [#527](https://github.com/bryntum/support/issues/527) - Gantt sends wrong server request when adding a resource
* [#553](https://github.com/bryntum/support/issues/553) - Loadmask not hidden after load fails
* [#558](https://github.com/bryntum/support/issues/558) - Crash when mouseout happens on a task terminal of a task being removed
* [#559](https://github.com/bryntum/support/issues/559) - Crash if zooming with schedule collapsed
* [#566](https://github.com/bryntum/support/issues/566) - Constraint type: "MUST START ON" not working
* [#577](https://github.com/bryntum/support/issues/577) - Moving task that is partially outside the view fails with an exception
* [#580](https://github.com/bryntum/support/issues/580) - Child calendars does not include intervals from parent
* [#649](https://github.com/bryntum/support/issues/649) - autoSync not triggered when deleting task in TaskEditor
* [#671](https://github.com/bryntum/support/issues/671) - Gantt localization of New task/milestone is broken
* [#675](https://github.com/bryntum/support/issues/675) - Task context menu localization is broken
* [#733](https://github.com/bryntum/support/issues/733) - Changing start date of manual task does not move successors
* [#740](https://github.com/bryntum/support/issues/740) - Should be possible to pass task instance to from/to fields when create a new dependency
* [#744](https://github.com/bryntum/support/issues/744) - Drag drop / resize using touch shows empty tooltip
* [#748](https://github.com/bryntum/support/issues/748) - Effort is updated for effort driven task

## 2.1.2 - 2020-04-17

### FEATURES / ENHANCEMENTS

* The gantt.module.js bundle is now lightly transpiled to ECMAScript 2015 using Babel to work with more browsers out of
  the box
* The PDF Export feature scrolls through the dataset in a more efficient manner ([#578](https://github.com/bryntum/support/issues/578))

### BUG FIXES

* [#123](https://github.com/bryntum/support/issues/123) - Successors and predecessors stores in TaskEditor should not be filtered when task nodes are collapsed
* [#367](https://github.com/bryntum/support/issues/367) - No 'change' event fired after indent operation
* [#464](https://github.com/bryntum/support/issues/464) - Dependencies are not refreshed after filtering with schedule region collapsed
* [#490](https://github.com/bryntum/support/issues/490) - Project can't be loaded with console error Cannot read property 'startDate' of undefined
* [#495](https://github.com/bryntum/support/issues/495) - Ctrl/Cmd Drag a task fails with exception
* [#506](https://github.com/bryntum/support/issues/506) - Adding a milestone for a task fails if the task is ahead the project start date
* [#508](https://github.com/bryntum/support/issues/508) - Wrong rollup rendering on changing zoom level
* [#543](https://github.com/bryntum/support/issues/543) - Having TaskEdit feature disabled breaks TaskContextMenu
* [#544](https://github.com/bryntum/support/issues/544) - Task indicators shown even if they are not part of the time axis

## 2.1.1 - 2020-03-27

### FEATURES / ENHANCEMENTS

* Added new demo showing integration with .NET backend and .NET Core backend ([#300](https://github.com/bryntum/support/issues/300))
* New .NET integration guide added to the docs

### API CHANGES

* GanttDateColumn no longer shows its step triggers by default. Enable the triggers by setting the `step` value
  available on the DateColumn class

### BUG FIXES

* [#399](https://github.com/bryntum/support/issues/399) - Task incorrectly rendered after duration change
* [#409](https://github.com/bryntum/support/issues/409) - Crash when clicking next time arrow in event editor if end date is cleared
* [#418](https://github.com/bryntum/support/issues/418) - Resource assignment column not refreshed after resource update
* [#424](https://github.com/bryntum/support/issues/424) - New resource record throws exception when serializing if propagate wasn't called
* [#426](https://github.com/bryntum/support/issues/426) - Gantt throws when trying to load invalid empty calendar id
* [#429](https://github.com/bryntum/support/issues/429) - Crash if project is loaded with task editor open
* [#430](https://github.com/bryntum/support/issues/430) - Gantt selection not updated after project reload
* [#436](https://github.com/bryntum/support/issues/436) - Crash when exporting to PDF in Angular demo
* [#438](https://github.com/bryntum/support/issues/438) - Rollups are not rendered for collapsed parent node
* [#442](https://github.com/bryntum/support/issues/442) - Default resource images not loaded
* [#444](https://github.com/bryntum/support/issues/444) - Phantom parent id is not included to changeset package and children are
* [#445](https://github.com/bryntum/support/issues/445) - React: Scheduler crashes when features object passed as prop
* [#446](https://github.com/bryntum/support/issues/446) - TaskEditor does not detach from project on consecutive edits
* [#447](https://github.com/bryntum/support/issues/447) - Should round percentDone value for tasks in task editor
* [#449](https://github.com/bryntum/support/issues/449) - Issues when using filter field in assignment editor
* [#450](https://github.com/bryntum/support/issues/450) - Date column too narrow to fit its cell editor
* [#451](https://github.com/bryntum/support/issues/451) - collapseAll does not update selection
* [#457](https://github.com/bryntum/support/issues/457) - Docker container with gantt ASP.NET Core demo cannot connect to MySQL container
* [#458](https://github.com/bryntum/support/issues/458) - Crash when clicking leaf row in undo grid
* [#463](https://github.com/bryntum/support/issues/463) - Filter not applied when deleting character

## 2.1.0 - 2020-03-11

### FEATURES / ENHANCEMENTS

* Indicators for constraint date, early and late dates and more can be added per task row using the new Indicators
  feature. See the new `indicators` demo
* Deadline support was added in form of a field on `TaskModel` and a `DeadlineDateColumn` to display and edit it
  Compatible with the new Indicators feature ([#235](https://github.com/bryntum/support/issues/235))
* Resource Assignment column can now show avatars for resources. See new `showAvatars` config used in the updated
  advanced demo ([#381](https://github.com/bryntum/support/issues/381))
* AssignmentGrid's selection model can now be customised like any regular Grid ([#370](https://github.com/bryntum/support/issues/370))
* Font Awesome 5 Pro was replaced with Font Awesome 5 Free as the default icon font (MIT / SIL OFL license)

### API CHANGES

* [DEPRECATED] The `tplData` param to `taskRenderer` was renamed to `renderData` to better reflect its purpose. The old
  name has been deprecated and will be removed in version 4

### BUG FIXES

* [#255](https://github.com/bryntum/support/issues/255) - Tasks disappear after adding tasks with schedule region collapsed
* [#330](https://github.com/bryntum/support/issues/330) - Id collision happens when you add or move records after filters are cleared
* [#338](https://github.com/bryntum/support/issues/338) - Crash when mouse over splitter during dependency creation
* [#352](https://github.com/bryntum/support/issues/352) - Crash when clicking Units cell of newly added assignment row in task editor
* [#353](https://github.com/bryntum/support/issues/353) - Crash upon load if using Iran Standard Time zone
* [#366](https://github.com/bryntum/support/issues/366) - writeAllFields is not honored in ProjectModel
* [#391](https://github.com/bryntum/support/issues/391) - Crash when clicking outside assignment editor with cell editing active

## 2.0.4 - 2020-02-24

### API CHANGES

* [DEPRECATED] PercentDoneCircleColumn, use PercentDoneColumn instead with `showCircle` config enabled

### BUG FIXES

* [#159](https://github.com/bryntum/support/issues/159) - Context menu differs in schedule vs grid
* [#215](https://github.com/bryntum/support/issues/215) - PDF export feature doesn't work on zoomed page
* [#286](https://github.com/bryntum/support/issues/286) - Parent node expanded after reorder
* [#296](https://github.com/bryntum/support/issues/296) - Missing title for "New column" in Gantt demos
* [#297](https://github.com/bryntum/support/issues/297) - Note column not updated after set to empty value
* [#317](https://github.com/bryntum/support/issues/317) - Outdent should maintain task parent index
* [#326](https://github.com/bryntum/support/issues/326) - "Graph cycle detected" exception when reordering node
* [#331](https://github.com/bryntum/support/issues/331) - Crash when trying to scroll a task into view if gantt panel is collapsed

## 2.0.3 - 2020-02-13

### FEATURES / ENHANCEMENTS

* ProgressBar feature now has an `allowResize` config to enable or disable resizing ([#242](https://github.com/bryntum/support/issues/242))
* Added a new Rollup column allowing control of which tasks should roll up. Added a new Rollup field to the Advanced tab
  ([#259](https://github.com/bryntum/support/issues/259))

### BUG FIXES

* [#040](https://github.com/bryntum/support/issues/040) - Focusing a task partially outside of timeaxis extends timeaxis
* [#067](https://github.com/bryntum/support/issues/067) - Wrong typedef of ProjectModel in gantt.umd.d.ts
* [#139](https://github.com/bryntum/support/issues/139) - Dependencies not painted after converting task to milestone
* [#154](https://github.com/bryntum/support/issues/154) - Cannot type into duration field of new task
* [#244](https://github.com/bryntum/support/issues/244) - Dependency drawn after being deleted
* [#256](https://github.com/bryntum/support/issues/256) - Progress line not redrawn after an invalid drop
* [#262](https://github.com/bryntum/support/issues/262) - Resizing to small width which doesn't update data gets UI out of sync
* [#240](https://github.com/bryntum/support/issues/240) - Crash when editing assignment twice
* [#272](https://github.com/bryntum/support/issues/272) - TaskEdit allows to assign same resource to a Task multiple times
* [#274](https://github.com/bryntum/support/issues/274) - Crash after adding subtask
* [#283](https://github.com/bryntum/support/issues/283) - Gantt Baselines example tooltips not localized

## 2.0.2 - 2020-01-30

### FEATURES / ENHANCEMENTS

* PDF export server was refactored. Removed websocket support until it is implemented on a client side
  Added logging. Added configuration file (see `app.config.js`) which can be overriden by CLI options
  Multipage export performance was increased substantially (see `max-workers` config in server readme)
  ([#112](https://github.com/bryntum/support/issues/112))

### BUG FIXES

* [#95](https://github.com/bryntum/support/issues/95)  - Task end date moves to the previous day when business calendar is used
* [#155](https://github.com/bryntum/support/issues/155) - Task editor displaced upon show
* [#237](https://github.com/bryntum/support/issues/237) - Project lines not shown after project load

## 2.0.1 - 2020-01-17

### FEATURES / ENHANCEMENTS

* Added new Angular examples: Rollups and Time ranges
* PDF Export feature uses first task's name or *Gantt* as the default file name ([#117](https://github.com/bryntum/support/issues/117))

### BUG FIXES

* [#53](https://github.com/bryntum/support/issues/53)  - Cell editing is broken when a column uses a field that is missing in the model
* [#94](https://github.com/bryntum/support/issues/94)  - Non-symmetric left/right margin for labels
* [#103](https://github.com/bryntum/support/issues/103) - Wrong date format in timeline labels
* [#115](https://github.com/bryntum/support/issues/115) - Project Start field highlighted as invalid while page is loading
* [#118](https://github.com/bryntum/support/issues/118) - Constraint / Start date columns should not include hour info by default
* [#126](https://github.com/bryntum/support/issues/126) - Scheduling engine docs missing
* [#132](https://github.com/bryntum/support/issues/132) - Not possible to open task editor for new task while it's open
* [#134](https://github.com/bryntum/support/issues/134) - 'b-' CSS class seen in task editor element
* [#140](https://github.com/bryntum/support/issues/140) - Crash if calling scrollTaskIntoView on an unscheduled task
* [#142](https://github.com/bryntum/support/issues/142) - Crash when adding a task below an unscheduled task
* [#149](https://github.com/bryntum/support/issues/149) - Timeline widget only shows tasks in expanded parent nodes
* [#160](https://github.com/bryntum/support/issues/160) - Task label not vertically centered
* [#161](https://github.com/bryntum/support/issues/161) - Drag creating dates on an unscheduled task does not set duration
* [#162](https://github.com/bryntum/support/issues/162) - Gantt is not restored properly after export
* [#165](https://github.com/bryntum/support/issues/165) - Name field turns red/invalid upon save
* [#166](https://github.com/bryntum/support/issues/166) - Cannot save unscheduled task with ENTER key
* [#172](https://github.com/bryntum/support/issues/172) - Should not be possible to create dependency between already linked tasks

## 2.0.0 - 2019-12-19

### FEATURES / ENHANCEMENTS

* Gantt has a new rendering pipeline, built upon a method of syncing changes to DOM developed for the vertical mode in
  Scheduler. This change allows us to remove about 1000 lines of code in this release, making maintenance and future
  development easier
* Added support for exporting the Gantt chart to PDF and PNG. It is showcased in several examples, pdf-export for
  Angular, React and Vue frameworks, as well as in examples/export. The feature requires a special export server,
  which you can find in the examples/_shared/server folder. You will find more instructions in the README.md file in
  each new demo. (#6268)

### API CHANGES

* [BREAKING] (for those who build from sources): "Common" package was renamed to "Core", so all our basic classes
  should be imported from `lib/Core/`
* [BREAKING] Gantt `nonWorkingTime` feature class replaced with `SchedulerPro/feature/ProNonWorkingTime` which uses
  project's calendar to obtain non-working time
* Gantt `nonWorkingTime` feature is now enabled by default

## 1.2.2 - 2019-11-21

### BUG FIXES

* [#13](https://github.com/bryntum/support/issues/13) - Dragging progress bar handle causes task move

## 1.2.1 - 2019-11-15

### BUG FIXES

* `exporttoexcel` demo broken with bundles

## 1.2.0 - 2019-11-06

### FEATURES / ENHANCEMENTS

* Added support for rollups feature (#4774)
* Added a thinner version of Gantt called `GanttBase`. It is a Gantt without default features, allowing smaller custom
  builds using for example WebPack. See the new `custom-build` demo for a possible setup (#7883)
* Experimental: The React wrapper has been updated to support using React components (JSX) in cell renderers and as cell
  editors. Please check out the updated React demos to see how it works (#7334, #9043)
* Added Export to Excel demo (#9133)
* Added a new 'Aggregated column demo' that shows how to add a custom column summing values (#9211)
* Support for disabling features at runtime has been improved, all features except Tree can now be disabled at any time
* Widgets may now adopt a preexisting DOM node to use as their encapsulating `element`. This reduces DOM footprint when
  widgets are being placed inside existing applications, or when used inside UI frameworks which provide a DOM node. See
  the `adopt` config option (#9414)
* The task context menu has been augmented to add indent and outdent. (#4779)

### BUG FIXES

* #8976 - Prevent task editor from closing if there is an invalid field
* #9146 - "No rows to display" shown while loading data
* #9161 - Locked grid scroll is reset upon task bar click
* #9243 - Date columns change format after zooming
* #9253 - Recreating Gantt when a tab in taskeditor is disabled leads to exception
* #9416 - Adding a resource in the TaskEditor, then clicking Save throws an error
* #9304 - Tasks duplicated on drag
* #9240 - Duration misrendered when editing
* #9242 - Sync is called on TaskEdit dialog cancel when autosync is true

## 1.1.5 - 2019-09-09

### FEATURES / ENHANCEMENTS

* Added a new `showCircle` config to PercentDoneColumn that renders a circular progress bar of the percentDone field
  value (#9162)

### BUG FIXES

* #8548 - DOCS: `propagate` missing in Project docs
* #8763 - Crash after editing predecessors
* #8967 - PHP demo: error when removing tasks with children
* #9092 - TaskStore id collision
* #9148 - Crash after resizing task progress bar in Timeline demo
* #9163 - STYLING: Milestone displaced

## 1.1.4 - 2019-08-28

### FEATURES / ENHANCEMENTS

* Added Tooltips demo that shows how to customize the task tooltip (#9109)

### API CHANGES

* The `TaskEdit#getEditor()` function was made public, can be used to retrieve the TaskEditor instance

### BUG FIXES

* #8560 - Adding task below last task creates empty row
* #8618 - STYLING: Dark theme nonworking time headers look bad
* #8619 - STYLING: Dark theme check column unchecked checkboxes are invisible
* #8690 - STYLING: Selected task innerEl rendition needs to be more of a contrast so that the current, possibly
  multiple selection can be seen at a glance
* #8844 - PHP demo: dragging and tooltip are broken after a newly created task is saved
* #9008 - Progress bar resizable in readOnly mode
* #9073 - vue drag-from-grid demo cannot be built with yarn
* #9084 - Task row disappears on Drag'n'Drop
* #9087 - Resource Avatar images reloaded upon every change to Task model
* #9093 - Phantom dependencies are rendered after clearing task store
* #9097 - STYLING: Toolbar fields misaligned in advanced demo
* #9108 - 'beforeTaskEdit' only fired once if listener returns false

## 1.1.3 - 2019-08-19

### FEATURES / ENHANCEMENTS

* Added React Basic Gantt demo with TypeScript (#8977)
* Added support for importing MS Project MPP files (see 'msprojectimport' demo). Requires JAVA and PHP on the backend
  See README in the example dir for details (#8987)

### BUG FIXES

* Fixes #8336 - Switching locale in advanced demo takes ~2 seconds
* #8653 - Unexpected task scheduling after undo operation
* #8712 - PHP demo: after creating a new task and saving it, when try to interact with the task demo fails with
  exceptions
* #8715 - PHP demo: after creating a new task and saving it selection is broken
* #8716 - Dependency line for a deleted dependency is redrawn after it's "to" side is appended to its "from" side
* #8884 - Critical paths demo is broken online
* #8885 - tabsConfig is not taken into account by TaskEditor
* #8966 - PHP demo: task sort order is not stored
* #8988 - React demo in trial distribution refers to scheduler folder which may not exist
* #8995 - Progress bar in some tasks cannot be resized after some point
* #9006 - Pan feature doesn't work in Gantt
* #9027 - ColumnLines feature doesn't work in Gantt

## 1.1.2 - 2019-07-05

### BUG FIXES

* #8804 - Error / warnings in console of web components demo
* #8805 - Animations not working
* #8811 - Crash when using context menu in web components demo
* #8839 - Save/Delete/Cancel button order in TaskEditor should match order in EventEditor

## 1.1.1 - 2019-06-27

### FEATURES / ENHANCEMENTS

* Added Integration Guide for Vue, Angular and React (#8686)
* Added new config option `tabsConfig` for `taskEdit` feature (#8765)

### BUG FIXES

* #8754 - Sluggish drag drop experience in advanced demo
* #8778 - Baselines disappear if scrolling down and back up
* #8785 - Passing listeners to editor widget in TaskEditor removes internal listeners

## 1.1.0 - 2019-06-20

### FEATURES / ENHANCEMENTS

* There is now a `Baselines` feature which shows baselines for tasks. A task's data block may now contain
  a `baselines` property which is an array containing baseline data blocks which must contain at least
  `startDate` and `endDate`. See the new example for details. (#6286)
* New `CriticalPaths` feature which visualizes the project critical paths. Check how it works in the new `criticalpaths`
  demo. (#6269)
* New `ProgressLine` feature - a vertical graph that provides the highest level view of schedule progress. Check how it
  works in the new `progressline` demo. (#8643)
* New `EarlyStartDate`, `EarlyEndDate`, `LateStartDate`, `LateEndDate` and `TotalSlack` columns. Check how they works in
  the new `criticalpaths` demo. (#6285)

### BUG FIXES

* #8539 - Some task editor fields turns red moments before editor is closed after clicking save
* #8602 - TaskEditor should invalidate an end date < start date
* #8603 - STYLING: Milestones lack hover color
* #8604 - Clicking task element does not select the row
* #8632 - Task end date/duration is not properly editing after cancel
* #8665 - Task interaction events are not documented
* #8707 - Resizing column expands collapsed section

## 1.0.2 - 2019-06-03

### FEATURES / ENHANCEMENTS

* New integration demo with Ext JS Modern toolkit (#8447)
* New webcomponents demo (#8495)
* TaskEdit feature now fires an event before show to prevent editing or to show a custom editor (#8510)
* TaskEdit feature now optionally shows a delete button
* Gantt repaints dependencies asynchronously when dependency or task is changed. Use `dependenciesDrawn` event to know
  when dependency lines are actually painted. `draw`, `drawDependency` and  `drawForEvent` are still synchronous

### API CHANGES

* [DEPRECATED] TaskEditor's `extraWidgets` config was deprecated and will be removed in a future version. Please use
  `extraItems` instead

### BUG FIXES

* #7925 - Dependency line should only be drawn once on dependency change
* #8517 - Angular demo tasks animate into view
* #8518 - React + Vue demos broken rendering
* #8520 - Labels demo timeaxis incorrectly configured
* #8529 - Pan feature reacts when dragging task bar
* #8516 - Customizing resourceassignment picker demo issues
* #8532 - Adding task above/below of a milestone creates a task with wrong dates
* #8533 - Cannot destroy ButtonGroup
* #8556 - Add Task button throws in extjs modern demo
* #8586 - Add new column header not localized properly

## 1.0.1 - 2019-05-24

### FEATURES / ENHANCEMENTS

* Delimiter used in Successors and Predecessors columns is now configurable, defaulting to ; (#8292)
* New `timeranges` demo showing how to add custom date indicator lines as well as date ranges to the Gantt chart
  (#8320)
* Demos now have a built in code editor that allows you to play around with their code (Chrome only) and CSS
  (#7210)
* [BREAKING] Context menu Features are configured in a slightly different way in this version. If you have used
  the `extraItems` or `processItems` options to change the contents of the shown menu, this code must be
  updated. Instead of using `extraItems`, use `items`

  The `items` config is an *`Object`* rather than an array. The property values are your new submenu configs, and the
  property name is the menu item name. In this way, you may add new items easily, but also, you may override the
  configuration of the default menu items that we provide

  The default menu items now all have documented names (see the `defaultItems` config of the Feature), so you may apply
  config objects which override default config. To remove a provided default completely, specify the config value as
  `false`

  This means that the various `showXxxxxxxInContextMenu` configs in the Gantt are now ineffective. Simply
  use for example, `items : { addTaskAbove : false }` to remove a provided item by name

  `processItems` now recieves its `items` parameter as an `Object`, so finding predefined named menu items to mutate is
  easier. Adding your own entails adding a new named config object. Use the `weight` config to affect the menu item
  order. Provided items are `weight : 0`. Weight values may be negative to cause your new items  (#8287)

### BUG FIXES

* #7561 - Should be able to use Grid & Scheduler & Gantt bundles on the same page
* #8075 - TimeRanges store not populated if incoming CrudManager dataset contains data
* #8210 - Terminals not visible when hovering task after creating dependency
* #8261 - ProjectLines not painted after propagation complete
* #8264 - Reordering a task into a parent task doesn't recalculate the parent
* #8275 - Framework integrations missing value in start date field
* #8276 - Crash if invoking task editor for unscheduled task
* #8279 - Gantt PHP demo requestPromise.abort is not a function in AjaxTransport.js
* #8293 - Gantt advanced demo. Graph cycle detected
* #8295 - Gantt umd bundle doesn't work in angular
* #8296 - Typings for gantt.umd bundles are incomplete
* #8325 - Some translations are missing (NL)
* #8334 - Clicking on a blank space selects a task and scrolls it into view
* #8341 - Task elements are missing after adding new tasks
* #8342 - Collapsing all records fails in advanced demo
* #8357 - TaskEditor needs to provide a simple way of adding extra fields to each tab
* #8381 - loadMask not shown if Project is using autoLoad true
* #8384 - Crash in React demo when clicking Edit button
* #8390 - Undoing project start date change doesn't update project start line
* #8391 - Progress bar element overflows task bar on hover if task is narrow
* #8394 - CrudManager reacts incorrectly and tries to save empty changeset
* #8397 - Inserting two tasks at once breaks normal view
* #8404 - addTaskBelow fails on 2nd call
* #8457 - Rendering broken after adding subtask to parent
* #8462 - Error throw in undoredo example when second transaction is canceled
* #8475 - STYLING: Misalignment of resource assignment filter field
* #8494 - Exception thrown when adding task via context menu
* #8496 - Crash in Gantt docs when viewing ResourceTimeRanges

## 1.0.0 - 2019-04-26

* Today we are super excited to share with you the 1.0 GA of our new Bryntum Gantt product. It is a powerful and high
  performance Gantt chart component for any web application. It is built from the ground up with pure JavaScript and
  TypeScript, and integrates easily with React, Angular, Vue or any other JS framework you are already using
* For a full introduction, please see our blog post for more details about this release. In our docs page you will find
  extensive API documentation including a getting started guide
* Blog post: https://bryntum.com/blog/announcing-bryntum-gantt-1-0

