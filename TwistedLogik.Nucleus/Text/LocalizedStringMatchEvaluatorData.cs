﻿using System;

namespace TwistedLogik.Nucleus.Text
{
    /// <summary>
    /// Represents the data for a localization match evaluator.
    /// </summary>
    public struct LocalizationMatchEvaluatorData
    {
        /// <summary>
        /// Initializes a new instance of the LocalizationMatchEvaluatorData structure.
        /// </summary>
        /// <param name="name">The match evaluator name.</param>
        /// <param name="evaluator">The match evaluator.</param>
        public LocalizationMatchEvaluatorData(String name, LocalizationMatchEvaluator evaluator)
        {
            Contract.Require(name, "name");
            Contract.Require(evaluator, "evaluator");

            this.Name = name;
            this.Evaluator = evaluator;
        }

        /// <summary>
        /// The match evaluator name.
        /// </summary>
        public readonly String Name;

        /// <summary>
        /// The match evaluator.
        /// </summary>
        public readonly LocalizationMatchEvaluator Evaluator;
    }
}
