﻿using System;
using TwistedLogik.Nucleus;
using TwistedLogik.Nucleus.Collections;
using TwistedLogik.Ultraviolet.UI.Presentation.Styles;

namespace TwistedLogik.Ultraviolet.UI.Presentation
{
    /// <summary>
    /// Represents the method that is called when an interface element raises an event.
    /// </summary>
    /// <param name="element">The element that raised the event.</param>
    public delegate void UpfEventHandler(DependencyObject element);

    /// <summary>
    /// Represents the method that is called when an interface element raises a routed event.
    /// </summary>
    /// <param name="element">The element that raised the event.</param>
    /// <param name="handled">A value indicating whether the event has been handled.</param>
    public delegate void UpfRoutedEventHandler(DependencyObject element, ref RoutedEventData data);

    /// <summary>
    /// Represents the identifier of a routed event.
    /// </summary>
    public sealed partial class RoutedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoutedEvent"/> class.
        /// </summary>
        /// <param name="id">The event's unique identifier within the routed events system.</param>
        /// <param name="name">The routed event's name.</param>
        /// <param name="uvssName">The dependency property's name within the UVSS styling system.</param>
        /// <param name="routingStrategy">The routed event's routing strategy.</param>
        /// <param name="delegateType">The routed event's delegate type.</param>
        /// <param name="ownerType">The routed event's owner type.</param>
        internal RoutedEvent(Int64 id, String name, String uvssName, RoutingStrategy routingStrategy, Type delegateType, Type ownerType)
        {
            this.id                 = id;
            this.name               = name;
            this.uvssName           = uvssName ?? UvssNameGenerator.GenerateUvssName(name);
            this.routingStrategy    = routingStrategy;
            this.delegateType       = delegateType;
            this.ownerType          = ownerType;
            this.invocationDelegate = RoutedEventInvocation.CreateInvocationDelegate(this);
        }

        /// <summary>
        /// Registers the specified subscriber to receive routed event notifications for the specified routed event.
        /// </summary>
        /// <param name="dobj">The dependency object to monitor for changes.</param>
        /// <param name="routedEvent">The dependency property for which to receive change notifications.</param>
        /// <param name="subscriber">The subscriber that wishes to receive change notifications for the specified dependency property.</param>
        internal static void RegisterRaisedNotification(DependencyObject dobj, RoutedEvent routedEvent, IRoutedEventRaisedNotificationSubscriber subscriber)
        {
            Contract.Require(dobj, "dobj");
            Contract.Require(routedEvent, "routedEvent");
            Contract.Require(subscriber, "subscriber");

            lock (routedEvent.raisedNotificationSubs)
                routedEvent.raisedNotificationSubs.AddLast(new RaisedNotificationKey(subscriber, dobj));
        }

        /// <summary>
        /// Unregisters the specified subscriber from receiving routed event notifications for the specified routed event.
        /// </summary>
        /// <param name="dobj">The dependency object to monitor for changes.</param>
        /// <param name="routedEvent">The routed event </param>
        /// <param name="subscriber">The subscriber that wishes to stop receiving change notifications for the specified dependency property.</param>
        internal static void UnregisterRaisedNotification(DependencyObject dobj, RoutedEvent routedEvent, IRoutedEventRaisedNotificationSubscriber subscriber)
        {
            Contract.Require(dobj, "dobj");
            Contract.Require(routedEvent, "routedEvent");
            Contract.Require(subscriber, "subscriber");

            lock (routedEvent.raisedNotificationSubs)
                routedEvent.raisedNotificationSubs.Remove(new RaisedNotificationKey(subscriber, dobj));
        }

        /// <summary>
        /// Raises an event notification for this routed event.
        /// </summary>
        /// <param name="dobj">The dependency object that raised the event.</param>
        /// <param name="data">The routed event's metadata.</param>
        internal void RaiseRaisedNotification(DependencyObject dobj, ref RoutedEventData data)
        {
            lock (raisedNotificationSubs)
            {
                for (var current = raisedNotificationSubs.First; current != null; current = current.Next)
                {
                    var key = current.Value;
                    if (key.Target == dobj)
                    {
                        key.Subscriber.ReceiveRoutedEventRaisedNotification(dobj, this, ref data);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the event's unique identifier within the routed events system.
        /// </summary>
        internal Int64 ID
        {
            get { return id; }
        }

        /// <summary>
        /// Gets the routed event's name.
        /// </summary>
        internal String Name
        {
            get { return name; }
        }

        /// <summary>
        /// Gets the routed event's name within the UVSS styling system.
        /// </summary>
        internal String UvssName
        {
            get { return uvssName; }
        }

        /// <summary>
        /// Gets the routed event's routing strategy.
        /// </summary>
        internal RoutingStrategy RoutingStrategy
        {
            get { return routingStrategy; }
        }

        /// <summary>
        /// Gets the routed event's delegate type.
        /// </summary>
        internal Type DelegateType
        {
            get { return delegateType; }
        }

        /// <summary>
        /// Gets the routed event's owner type.
        /// </summary>
        internal Type OwnerType
        {
            get { return ownerType; }
        }

        /// <summary>
        /// Gets the event's invocation delegate.
        /// </summary>
        internal Delegate InvocationDelegate
        {
            get { return invocationDelegate; }
        }

        // Property values.
        private readonly Int64 id;
        private readonly String name;
        private readonly String uvssName;
        private readonly RoutingStrategy routingStrategy;
        private readonly Type delegateType;
        private readonly Type ownerType;
        private readonly Delegate invocationDelegate;

        // State values.
        private readonly PooledLinkedList<RaisedNotificationKey> raisedNotificationSubs = 
            new PooledLinkedList<RaisedNotificationKey>(1);
    }
}
