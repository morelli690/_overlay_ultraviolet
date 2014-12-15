﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace TwistedLogik.Ultraviolet.Layout.Stylesheets
{
    partial class UvssStoryboardTargetFilter
    {
        /// <inheritdoc/>
        public List<String>.Enumerator GetEnumerator()
        {
            return types.GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator<String> IEnumerable<String>.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
