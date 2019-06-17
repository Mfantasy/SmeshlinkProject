/*
 * SmeshLink.Misty.Formatter.IFeedFormatter.cs
 * 
 * Copyright (c) 2009-2014 SmeshLink Technology Corporation.
 * All rights reserved.
 * 
 * Authors:
 *  Longxiang He
 * 
 * This file is part of the Misty, a sensor cloud for IoT.
 */

using System;
using System.Collections.Generic;
using System.IO;
using SmeshLink.Misty.Entity;

namespace SmeshLink.Misty.Formatter
{
    /// <summary>
    /// Providers methods for serializing and deserializing the <see cref="SmeshLink.Misty.Entity.Feed"/>.
    /// </summary>
    public interface IFeedFormatter
    {
        /// <summary>
        /// Formats a single feed.
        /// </summary>
        void Format(Stream output, Feed feed, FormatOption option);
        /// <summary>
        /// Formats a single feed.
        /// </summary>
        void Format(TextWriter writer, Feed feed, FormatOption option);
        /// <summary>
        /// Formats feeds.
        /// </summary>
        void Format(Stream output, IEnumerable<Feed> feed, FormatOption option);

        /// <summary>
        /// Parses feeds.
        /// </summary>
        IEnumerable<Feed> ParseFeeds(Stream stream);
        /// <summary>
        /// Parses feeds.
        /// </summary>
        IEnumerable<Feed> ParseFeeds(Object obj);
        /// <summary>
        /// Parses a single feed.
        /// </summary>
        Feed ParseFeed(Stream stream);
        /// <summary>
        /// Parses a single feed.
        /// </summary>
        Feed ParseFeed(Object obj);
    }

    /// <summary>
    /// Format options.
    /// </summary>
    [Flags]
    public enum FormatOption : byte
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Basic info.
        /// </summary>
        Basic = 0x1,
        /// <summary>
        /// Metadata
        /// </summary>
        Metadata = 0x2,
        /// <summary>
        /// Data
        /// </summary>
        Data = 0x4,
        /// <summary>
        /// All info.
        /// </summary>
        All = Basic | Metadata | Data
    }
}
