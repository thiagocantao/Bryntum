var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
import { calculate, Entity, generic_field } from "../../../ChronoGraph/replica/Entity.js";
import Model from "../../../Common/data/Model.js";
import { model_field, ModelReferenceField } from "../../chrono/ModelFieldAtom.js";
import { PartOfProjectGenericMixin } from "../PartOfProjectGenericMixin.js";
import { ChronoModelMixin } from "./mixin/ChronoModelMixin.js";
import { PartOfProjectMixin } from "./mixin/PartOfProjectMixin.js";
const hasMixin = Symbol('AssignmentMixin');
export const AssignmentMixin = (base) => {
    class AssignmentMixin extends base {
        [hasMixin]() { }
        getUnits() {
            return this.units;
        }
        setUnits(units) {
            return this.event.setAssignmentUnits(this, units);
        }
        *calculateUnits(proposedValue) {
            const event = yield this.$.event;
            // if event of assignment presents - we always delegate to it
            // (so that various assignment logic can be overridden by single event mixin)
            if (event)
                return yield* event.calculateAssignmentUnits(this, proposedValue);
            // otherwise use proposed or current consistent value
            return proposedValue !== undefined ? proposedValue : this.$.units.getConsistentValue();
        }
    }
    __decorate([
        model_field({ type: 'number', defaultValue: 100 })
    ], AssignmentMixin.prototype, "units", void 0);
    __decorate([
        generic_field({
            bucket: 'assigned',
            resolver: function (id) { return this.getEventById(id); },
            // NOTE: modelFieldConfig here somehow has effect since there is "event" relation
            // defined on the scheduler Assignment model
            modelFieldConfig: {
                serialize: event => event.id
            }
        }, ModelReferenceField)
    ], AssignmentMixin.prototype, "event", void 0);
    __decorate([
        generic_field({
            bucket: 'assigned',
            resolver: function (id) { return this.getResourceById(id); },
            // NOTE: modelFieldConfig here somehow has effect since there is "resource" relation
            // defined on the scheduler Assignment model
            modelFieldConfig: {
                serialize: resource => resource.id
            }
        }, ModelReferenceField)
    ], AssignmentMixin.prototype, "resource", void 0);
    __decorate([
        calculate('units')
    ], AssignmentMixin.prototype, "calculateUnits", null);
    return AssignmentMixin;
};
/**
 * Function to build a minimal possible [[AssignmentMixin]] class
 */
export const BuildMinimalAssignment = (base = Model) => AssignmentMixin(PartOfProjectMixin(PartOfProjectGenericMixin(ChronoModelMixin(Entity(base)))));
export class MinimalAssignment extends BuildMinimalAssignment() {
}
/**
 * The typeguard for the [[AssignmentMixin]]
 */
export const hasAssignmentMixin = (model) => Boolean(model && model[hasMixin]);
