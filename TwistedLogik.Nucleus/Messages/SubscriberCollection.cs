﻿using System;
using System.Collections.Generic;

namespace TwistedLogik.Nucleus.Messages
{
    /// <summary>
    /// Represents a collection of message subscribers.
    /// </summary>
    public class SubscriberCollection<TMessageType> where TMessageType : IEquatable<TMessageType>
    {
        /// <summary>
        /// Purges the subscriber list associated with the specified message type.
        /// </summary>
        /// <param name="messageType">The type of message for which to purge subscribers.</param>
        public void Purge(TMessageType messageType)
        {
            subscribers.Remove(messageType);
        }

        /// <summary>
        /// Purges the subscriber lists associated with all message types.
        /// </summary>
        public void PurgeAll()
        {
            subscribers.Clear();
        }

        /// <summary>
        /// Removes the specified subscriber from all subscriber lists.
        /// </summary>
        /// <param name="subscriber">The subscriber to remove.</param>
        public void RemoveFromAll(IMessageSubscriber<TMessageType> subscriber)
        {
            Contract.Require(subscriber, "subscriber");

            foreach (var collection in subscribers)
            {
                collection.Value.Remove(subscriber);
            }
        }

        /// <summary>
        /// Gets the set of subscribers which are subscribed to the specified message type.
        /// </summary>
        /// <param name="type">The type of message for which to get a subscriber list.</param>
        /// <returns>The set of subscribers which are subscribed to the specified message type.</returns>
        public HashSet<IMessageSubscriber<TMessageType>> this[TMessageType type]
        {
            get
            {
                HashSet<IMessageSubscriber<TMessageType>> subscribersToMessageType;
                if (!subscribers.TryGetValue(type, out subscribersToMessageType))
                {
                    subscribersToMessageType = new HashSet<IMessageSubscriber<TMessageType>>();
                    subscribers[type] = subscribersToMessageType;
                }
                return subscribersToMessageType;
            }
        }

        // The underlying table of subscribers for each message type.
        private readonly Dictionary<TMessageType, HashSet<IMessageSubscriber<TMessageType>>> subscribers =
            new Dictionary<TMessageType, HashSet<IMessageSubscriber<TMessageType>>>();
    }
}
