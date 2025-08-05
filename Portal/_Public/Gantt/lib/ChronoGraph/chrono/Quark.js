import { MixinAny } from "../class/Mixin.js";
import { NOT_VISITED } from "../graph/WalkDepth.js";
import { MAX_SMI, MIN_SMI } from "../util/Helpers.js";
//---------------------------------------------------------------------------------------------------------------------
export var EdgeType;
(function (EdgeType) {
    EdgeType[EdgeType["Normal"] = 1] = "Normal";
    EdgeType[EdgeType["Past"] = 2] = "Past";
})(EdgeType || (EdgeType = {}));
let ORIGIN_ID = 0;
//---------------------------------------------------------------------------------------------------------------------
export class Quark extends MixinAny([Map], (base) => class Quark extends base {
    constructor() {
        super(...arguments);
        // required
        this.createdAt = undefined;
        this.identifier = undefined;
        // quark state
        this.value = undefined;
        this.proposedValue = undefined;
        this.proposedIsPrevious = false;
        this.proposedArguments = undefined;
        this.usedProposedOrPrevious = false;
        // eof quark state
        this.previous = undefined;
        this.origin = undefined;
        this.originId = MIN_SMI;
        this.needToBuildProposedValue = false;
        this.edgesFlow = 0;
        this.visitedAt = NOT_VISITED;
        this.visitEpoch = 0;
        this.promise = undefined;
        this.$outgoingPast = undefined;
    }
    static new(props) {
        const instance = new this();
        props && Object.assign(instance, props);
        return instance;
    }
    get level() {
        return this.identifier.level;
    }
    get calculation() {
        return this.identifier.calculation;
    }
    get context() {
        return this.identifier.context || this.identifier;
    }
    forceCalculation() {
        this.edgesFlow = MAX_SMI;
    }
    cleanup() {
        this.cleanupCalculation();
    }
    isShadow() {
        return Boolean(this.origin && this.origin !== this);
    }
    resetToEpoch(epoch) {
        this.visitEpoch = epoch;
        this.visitedAt = NOT_VISITED;
        // we were clearing the edgeFlow on epoch change, however see `030_propagation_2.t.ts` for a counter-example
        
        if (this.edgesFlow < 0)
            this.edgesFlow = 0;
        this.usedProposedOrPrevious = false;
        this.cleanupCalculation();
        // if there's no value, then generally should be no outgoing edges
        // (which indicates that the value has been used somewhere else)
        // but there might be outgoing "past" edges, created if `HasProposedValue`
        // or similar effect has been used on the identifier
        // if (this.value !== undefined) this.clearOutgoing()
        // the `this.value !== undefined` condition above smells very "monkey-patching"
        // it was probably solving some specific problem in Gantt/SchedulerPro
        // (engine tests seems to pass w/o it)
        // in general, should always clear the outgoing edges on new epoch
        this.clearOutgoing();
        this.promise = undefined;
        if (this.origin && this.origin === this) {
            this.proposedArguments = undefined;
            // only overwrite the proposed value if the actual value has been already calculated
            // otherwise, keep the proposed value as is
            if (this.value !== undefined) {
                this.proposedValue = this.value;
            }
            this.value = undefined;
        }
        else {
            this.origin = undefined;
            this.value = undefined;
        }
        if (this.identifier.proposedValueIsBuilt && this.proposedValue !== TombStone) {
            this.needToBuildProposedValue = true;
            this.proposedValue = undefined;
        }
    }
    copyFrom(origin) {
        this.value = origin.value;
        this.proposedValue = origin.proposedValue;
        this.proposedArguments = origin.proposedArguments;
        this.usedProposedOrPrevious = origin.usedProposedOrPrevious;
    }
    clearProperties() {
        this.value = undefined;
        this.proposedValue = undefined;
        this.proposedArguments = undefined;
    }
    mergePreviousOrigin(latestScope) {
        const origin = this.origin;
        if (origin !== this.previous)
            throw new Error("Invalid state");
        this.copyFrom(origin);
        const outgoing = this.getOutgoing();
        for (const [identifier, quark] of origin.getOutgoing()) {
            const ownOutgoing = outgoing.get(identifier);
            if (!ownOutgoing) {
                const latest = latestScope.get(identifier);
                if (!latest || latest.originId === quark.originId)
                    outgoing.set(identifier, latest || quark);
            }
        }
        if (origin.$outgoingPast !== undefined) {
            const outgoingPast = this.getOutgoingPast();
            for (const [identifier, quark] of origin.getOutgoingPast()) {
                const ownOutgoing = outgoingPast.get(identifier);
                if (!ownOutgoing) {
                    const latest = latestScope.get(identifier);
                    if (!latest || latest.originId === quark.originId)
                        outgoingPast.set(identifier, latest || quark);
                }
            }
        }
        // changing `origin`, but keeping `originId`
        this.origin = this;
        // some help for garbage collector
        origin.clearProperties();
        origin.clear();
    }
    setOrigin(origin) {
        this.origin = origin;
        this.originId = origin.originId;
    }
    getOrigin() {
        if (this.origin)
            return this.origin;
        return this.startOrigin();
    }
    startOrigin() {
        this.originId = ORIGIN_ID++;
        return this.origin = this;
    }
    getOutgoing() {
        return this;
    }
    getOutgoingPast() {
        if (this.$outgoingPast !== undefined)
            return this.$outgoingPast;
        return this.$outgoingPast = new Map();
    }
    addOutgoingTo(toQuark, type) {
        const outgoing = type === EdgeType.Normal ? this : this.getOutgoingPast();
        outgoing.set(toQuark.identifier, toQuark);
    }
    clearOutgoing() {
        this.clear();
        if (this.$outgoingPast !== undefined)
            this.$outgoingPast.clear();
    }
    getValue() {
        const origin = this.origin;
        return origin === this
            ? this.value
            : origin
                ? origin.getValue()
                : undefined;
    }
    setValue(value) {
        if (this.origin && this.origin !== this)
            throw new Error('Can not set value to the shadow entry');
        this.getOrigin().value = value;
        // // @ts-ignore
        // if (value !== TombStone) this.identifier.DATA = value
    }
    hasValue() {
        return this.getValue() !== undefined;
    }
    hasProposedValue() {
        if (this.isShadow())
            return false;
        return this.hasProposedValueInner();
    }
    hasProposedValueInner() {
        return this.proposedValue !== undefined;
    }
    getProposedValue(transaction) {
        if (this.needToBuildProposedValue) {
            this.proposedValue = this.identifier.buildProposedValue.call(this.identifier.context || this.identifier, this.identifier, this, transaction);
            // setting this flag _after_ attempt to build the proposed value, because it might actually throw
            // (if there's a cycle during sync computation, like during `effectiveDirection`)
            // in such case, we need to re-enter this block
            this.needToBuildProposedValue = false;
        }
        return this.proposedValue;
    }
    outgoingInTheFutureCb(revision, forEach) {
        let current = this;
        while (current) {
            for (const outgoing of current.getOutgoing().values()) {
                if (outgoing.originId === revision.getLatestEntryFor(outgoing.identifier).originId)
                    forEach(outgoing);
            }
            if (current.isShadow())
                current = current.previous;
            else
                current = null;
        }
    }
    outgoingInTheFutureAndPastCb(revision, forEach) {
        let current = this;
        while (current) {
            for (const outgoing of current.getOutgoing().values()) {
                const latestEntry = revision.getLatestEntryFor(outgoing.identifier);
                if (latestEntry && outgoing.originId === latestEntry.originId)
                    forEach(outgoing);
            }
            if (current.$outgoingPast !== undefined) {
                for (const outgoing of current.$outgoingPast.values()) {
                    const latestEntry = revision.getLatestEntryFor(outgoing.identifier);
                    if (latestEntry && outgoing.originId === latestEntry.originId)
                        forEach(outgoing);
                }
            }
            if (current.isShadow())
                current = current.previous;
            else
                current = null;
        }
    }
    outgoingInTheFutureAndPastTransactionCb(transaction, forEach) {
        let current = this;
        while (current) {
            for (const outgoing of current.getOutgoing().values()) {
                const latestEntry = transaction.getLatestStableEntryFor(outgoing.identifier);
                if (latestEntry && outgoing.originId === latestEntry.originId)
                    forEach(outgoing);
            }
            if (current.$outgoingPast !== undefined) {
                for (const outgoing of current.$outgoingPast.values()) {
                    const latestEntry = transaction.getLatestStableEntryFor(outgoing.identifier);
                    if (latestEntry && outgoing.originId === latestEntry.originId)
                        forEach(outgoing);
                }
            }
            if (current.isShadow())
                current = current.previous;
            else
                current = null;
        }
    }
    // ignores the "past" edges by design, as they do not form cycles
    outgoingInTheFutureTransactionCb(transaction, forEach) {
        let current = this;
        while (current) {
            for (const outgoing of current.getOutgoing().values()) {
                const latestEntry = transaction.getLatestEntryFor(outgoing.identifier);
                if (latestEntry && outgoing.originId === latestEntry.originId)
                    forEach(outgoing);
            }
            if (current.isShadow())
                current = current.previous;
            else
                current = null;
        }
    }
}) {
}
//---------------------------------------------------------------------------------------------------------------------
export const TombStone = Symbol('Tombstone');
