import ProjectWebSocketHandlerMixin from './mixin/ProjectWebSocketHandlerMixin.js';
import ProjectModel from './ProjectModel.js';

/**
 * @module SchedulerPro/model/WebSocketProjectModel
 */

/**
 * This project allows to connect to the websocket server synchronizing changes between several client projects
 * @private
 */
export default class WebSocketProjectModel extends ProjectWebSocketHandlerMixin(ProjectModel) {
    static $name = 'WebSocketProjectModel';
}
