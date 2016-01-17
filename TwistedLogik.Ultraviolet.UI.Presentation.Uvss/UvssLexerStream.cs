﻿using System;
using TwistedLogik.Nucleus;

namespace TwistedLogik.Ultraviolet.UI.Presentation.Uvss
{
    /// <summary>
    /// Represents the result of lexing a UVSS document.
    /// </summary>
    public partial class UvssLexerStream
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UvssLexerStream"/> class.
        /// </summary>
        /// <param name="lexer">The lexer instance which produces tokens for this stream.</param>
        internal UvssLexerStream(UvssLexer lexer)
        {
            Contract.Require(lexer, nameof(lexer));

            this.lexer = lexer;
        }

        /// <summary>
        /// Generates tokens up to the specified index.
        /// </summary>
        /// <param name="index">The index of the last token to generate.</param>
        /// <returns>true if the requested tokens were generated; otherwise, false.</returns>
        public Boolean GenerateIndex(Int32 index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (pages == null)
                EnsurePageCapacity(4);
            
            var token = default(UvssLexerToken);
            var tokenCount = index - (count - 1);

            var pageIndex = (index - offset) / UvssLexerStreamPage.Size;
            var page = pageIndex >= pageCount ? null : pages[pageIndex];

            for (int i = 0; i < tokenCount; i++)
            {
                if (!lexer.Emit(out token))
                    return false;

                if (page == null || !page.Add(token))
                {
                    EnsurePageCapacity(pageCount + 1);
                    page = pages[pageCount++];
                    page.Add(token);
                }

                count++;
            }

            return true;
        }

        /// <summary>
        /// Discards loaded pages up to the specified token index.
        /// </summary>
        /// <param name="index">The index of the first token to preserve.</param>
        /// <returns>true if pages were trimmed; otherwise, false.</returns>
        public Boolean Trim(Int32 index)
        {
            if (index < offset || pageCount == 0)
                return false;

            var pageIndex = (index - offset) / UvssLexerStreamPage.Size;
            if (pageIndex == 0 || pageIndex >= pageCount)
                return false;
            
            pageCount = pageCount - pageIndex;
            Array.Copy(pages, pageIndex, pages, 0, pageCount);

            for (int i = pageCount; i < pages.Length; i++)
                pages[i] = new UvssLexerStreamPage();

            offset += pageIndex * UvssLexerStreamPage.Size;

            return true;
        }
        
        /// <summary>
        /// Gets a value indicating whether the specified index is beyond the
        /// end of the token stream.
        /// </summary>
        /// <param name="index">The index to evaluate.</param>
        /// <returns>true if the specified index is beyond the end of the token
        /// stream; otherwise, false.</returns>
        public Boolean IsPastEndOfStream(Int32 index)
        {
            return !GenerateIndex(index);
        }

        /// <summary>
        /// Gets the token at the specified index within the lexer stream.
        /// </summary>
        /// <param name="index">The index of the token to retrieve.</param>
        /// <returns>The token at the specified index within the lexer stream.</returns>
        public UvssLexerToken this[Int32 index]
        {
            get
            {
                if (index < Offset)
                    throw new ArgumentOutOfRangeException(nameof(index));

                if (!GenerateIndex(index))
                    throw new ArgumentOutOfRangeException(nameof(index));

                var pageIndex = (index - offset) / UvssLexerStreamPage.Size;
                var pageOffset = (index - offset) % UvssLexerStreamPage.Size;

                if (pageIndex >= pageCount)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return pages[pageIndex][pageOffset];
            }
        }

        /// <summary>
        /// Gets the index of the first token which is currently held in memory.
        /// </summary>
        public Int32 Offset
        {
            get { return offset; }
        }

        /// <summary>
        /// Gets the number of tokens which are currently held in memory.
        /// </summary>
        public Int32 Available
        {
            get
            {
                if (pageCount == 0)
                    return 0;

                var countOnFullPages = (pageCount > 1) ? (pageCount - 1 * UvssLexerStreamPage.Size) : 0;
                var countOnLastPage = pages[pageCount - 1].Count;

                return countOnFullPages + countOnLastPage;
            }
        }

        /// <summary>
        /// Gets the total number of tokens which have been generated.
        /// </summary>
        public Int32 Count
        {
            get { return count; }
        }

        /// <summary>
        /// Ensures that the stream's array of pages has at least
        /// the specified capacity.
        /// </summary>
        /// <param name="capacity">The requested capacity.</param>
        private void EnsurePageCapacity(Int32 capacity)
        {
            if (pages == null)
            {
                if (capacity < 4)
                    capacity = 4;

                pages = new UvssLexerStreamPage[capacity];
            }
            else
            {
                if (capacity <= pages.Length)
                    return;

                var size = Math.Max(capacity, pages.Length * 2);
                var temp = new UvssLexerStreamPage[size];
                Array.Copy(pages, temp, pageCount);
                pages = temp;
            }

            for (int i = 0; i < pages.Length; i++)
            {
                if (pages[i] == null)
                    pages[i] = new UvssLexerStreamPage();
            }
        }

        // Currently loaded stream pages.
        private UvssLexerStreamPage[] pages;
        private Int32 pageCount;

        // Property values.
        private Int32 offset;
        private Int32 count;

        // Lexer instance.
        private readonly UvssLexer lexer;
    }
}
