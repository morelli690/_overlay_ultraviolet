﻿using System;
using TwistedLogik.Nucleus;
using TwistedLogik.Nucleus.Data;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Styles
{
	/// <summary>
	/// Represents one of the conditions of a property trigger.
	/// </summary>
	public class UvssPropertyTriggerCondition
    {
		/// <summary>
		/// Initializes a new instance of the <see cref="UvssPropertyTriggerCondition"/> class.
		/// </summary>
		/// <param name="op">A <see cref="TriggerComparisonOp"/> value that specifies the type of comparison performed by this condition.</param>
		/// <param name="propertyName">The name of the property to evaluate.</param>
		/// <param name="propertyValue">The value to compare to the value of the evaluated property.</param>
		internal UvssPropertyTriggerCondition(TriggerComparisonOp op, DependencyName propertyName, DependencyValue propertyValue)
		{
			this.op = op;
			this.propertyName = propertyName;
			this.propertyValue = propertyValue;
		}

        /// <summary>
        /// Evaluates whether the condition is true for the specified object.
        /// </summary>
        /// <param name="uv">The Ultraviolet context.</param>
        /// <param name="dobj">The object against which to evaluate the trigger condition.</param>
        /// <returns><c>true</c> if the condition is true for the specified object; otherwise, <c>false</c>.</returns>
        internal Boolean Evaluate(UltravioletContext uv, DependencyObject dobj)
        {
            Contract.Require(uv, "uv");
            Contract.Require(dobj, "dobj");

            var dprop = DependencyProperty.FindByStylingName(uv, dobj, propertyName.Owner, propertyName.Name);
            if (dprop == null)
                return false;

            var refvalCacheType = (propertyValueCachhe == null) ? null : propertyValueCachhe.GetType();
            if (refvalCacheType == null || (refvalCacheType != dprop.PropertyType &&  refvalCacheType != dprop.UnderlyingType))
            {
                propertyValueCachhe = ObjectResolver.FromString(
					propertyValue.Value, dprop.PropertyType, propertyValue.Culture);
            }

            var comparison = TriggerComparisonCache.Get(dprop.PropertyType, op);
            if (comparison == null)
                throw new InvalidOperationException(PresentationStrings.InvalidTriggerComparison.Format(propertyName, op, dprop.PropertyType));

            return comparison(dobj, dprop, propertyValueCachhe);
        }

        /// <summary>
        /// Gets the comparison operation performed by this condition.
        /// </summary>
        public TriggerComparisonOp ComparisonOperation
        {
            get { return op; }
        }

        /// <summary>
        /// Gets the name of the dependency property which is evaluated by this condition.
        /// </summary>
        public DependencyName PropertyName
        {
            get { return propertyName; }
        }

        /// <summary>
        /// Gets a string which represents the reference value for this condition.
        /// </summary>
        public DependencyValue PropertyValue
        {
            get { return propertyValue; }
        }

        // State values.
        private readonly TriggerComparisonOp op;
        private readonly DependencyName propertyName;
        private readonly DependencyValue propertyValue;
		private Object propertyValueCachhe;
    }
}
