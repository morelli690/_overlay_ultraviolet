﻿using System.Xml.Linq;
using TwistedLogik.Ultraviolet.Content;

namespace TwistedLogik.Ultraviolet.Graphics.Graphics2D.Text
{
    /// <summary>
    /// Loads formatted text table layout assets.
    /// </summary>
    [ContentProcessor]
    public sealed class TextTableLayoutProcessor : ContentProcessor<XDocument, TextTableLayout>
    {
        /// <summary>
        /// Processes the specified data structure into a game asset.
        /// </summary>
        /// <param name="manager">The content manager with which the asset is being processed.</param>
        /// <param name="metadata">The asset's metadata.</param>
        /// <param name="input">The input data structure to process.</param>
        /// <returns>The game asset that was created.</returns>
        public override TextTableLayout Process(ContentManager manager, IContentProcessorMetadata metadata, XDocument input)
        {
            return new TextTableLayout(input);
        }
    }
}
