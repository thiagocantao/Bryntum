import Widget from './Widget.js';
import Rectangle from '../helper/util/Rectangle.js';
import EventHelper from '../helper/EventHelper.js';
import DomHelper from '../helper/DomHelper.js';
import GlobalEvents from '../GlobalEvents.js';
import ObjectHelper from '../helper/ObjectHelper.js';

let lastTouchTime = 0;

const hasRipple = w => w.ripple;

export default class Ripple extends Widget {
    static get defaultConfig() {
        return {
            old_element : {
                children : [{
                    className : 'b-ripple-inner',
                    reference : 'rippleElement'
                }]
            },

            element : {
                children : [{
                    tag       : 'svg',
                    class     : 'b-ripple-inner',
                    reference : 'rippleElement',
                    ns        : 'http://www.w3.org/2000/svg',
                    version   : '1.1',
                    viewBox   : '0 0 100 100',
                    children  : [{
                        reference : 'circleElement',
                        tag       : 'circle',
                        cx        : '0',
                        cy        : '0',
                        r         : 10
                    }]
                }]
            },

            floating      : true,
            hideAnimation : false,
            showAnimation : false,
            scrollAction  : 'realign',
            color         : 'rgba(0,0,0,.3)',
            startRadius   : 10,
            radius        : 100
        };
    }

    static get $name() {
        return 'Ripple';
    }

    afterConstruct() {
        super.afterConstruct();

        EventHelper.on({
            element   : this.rootElement,
            mousedown : 'onRippleControllingEvent',
            thisObj   : this,
            capture   : true,
            once      : true
        });
    }

    onRippleControllingEvent(event) {
        const me = this;

        me.show();

        const rippleAnimation = DomHelper.getStyleValue(me.circleElement, 'animationName');

        me.hide();

        me.listenerDetacher?.();

        // If our theme supports ripples, add our listeners
        if (rippleAnimation && rippleAnimation !== 'none') {
            me.listenerDetacher = EventHelper.on({
                // Trap all mousedowns and see if the encapsulating Component is configured to ripple
                mousedown : {
                    element : me.rootElement,
                    capture : true,
                    handler : 'onMousedown'
                },
                touchstart : {
                    element : me.rootElement,
                    capture : true,
                    handler : 'onTouchStart'
                },
                // Hide at the end of the ripple
                animationend : {
                    element : me.circleElement,
                    handler : 'onAnimationEnd'
                },
                thisObj : me
            });

            // If this is the first mousedown, start listening to theme changes and trigger ripple manually
            if (event.type === 'mousedown') {
                me.onMousedown(event);

                GlobalEvents.ion({
                    theme   : 'onRippleControllingEvent',
                    thisObj : this
                });
            }
        }
    }

    onTouchStart(event) {
        lastTouchTime = performance.now();
        this.handleTriggerEvent(event);
    }

    onMousedown(event) {
        // We need to prevent a touchend->mousedown simulated mousedown from triggering a ripple.
        // https://developer.mozilla.org/en-US/docs/Web/API/Touch_events/Supporting_both_TouchEvent_and_MouseEvent
        if (performance.now() - lastTouchTime > 200) {
            this.handleTriggerEvent(event);
        }
    }

    handleTriggerEvent(event) {
        const targetWidget = Widget.fromElement(event.target, hasRipple);

        if (targetWidget) {
            const
                rippleCfg = targetWidget.ripple,
                target    = rippleCfg.delegate
                    ? event.target.closest(rippleCfg.delegate)
                    : (targetWidget.focusElement || targetWidget.element);

            if (target) {
                const ripple = ObjectHelper.assign({
                    event,
                    target,
                    radius : this.radius
                }, rippleCfg);

                // The clip option is specified as a string property name or delegate
                if (typeof ripple.clip === 'string') {
                    ripple.clip = targetWidget[ripple.clip] || event.target.closest(ripple.clip);

                    // Not inside an instance of the clip delegate, then no ripple
                    if (!ripple.clip) {
                        return;
                    }
                }
                this.ripple(ripple);
            }
        }
    }

    ripple({
        event,
        point = EventHelper.getClientPoint(event),
        target = event.target,
        clip = target,
        radius = this.radius,
        color = this.color
    }) {
        this.clip = clip;


        clip = Rectangle.from(clip, null, true);

        const
            me            = this,
            centreDelta   = clip.getDelta(point),
            rippleStyle   = me.rippleElement.style,
            circleElement = me.circleElement,
            borderRadius  = DomHelper.getStyleValue(target, 'border-radius');

        me.hide();
        rippleStyle.transform    = `translateX(${centreDelta[0]}px) translateY(${centreDelta[1]}px)`;
        rippleStyle.height       = rippleStyle.width = `${radius}px`;
        me.element.style.borderRadius = borderRadius;
        circleElement.setAttribute('r', radius);
        circleElement.setAttribute('fill', color);

        // Show aligned center to center with our clipping region.
        me.showBy({
            target    : clip,
            align     : 'c-c',
            matchSize : true
        });
    }

    // When fully expanded, it's all over.
    onAnimationEnd(event) {
        if (event.animationName === 'b-ripple-expand') {
            this.hide();
        }
    }
}

Widget.RippleClass = Ripple;
