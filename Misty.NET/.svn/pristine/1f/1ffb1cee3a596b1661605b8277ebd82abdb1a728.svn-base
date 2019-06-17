/*
 * SmeshLink.Misty.Service.IFeedService.cs
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
using SmeshLink.Misty.Entity;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Provides methods to access services of feeds.
    /// </summary>
    public interface IFeedService
    {
        /// <summary>
        /// Gets all feeds with default options.
        /// </summary>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        IEnumerable<Feed> List();
        /// <summary>
        /// Gets all feeds with given options.
        /// </summary>
        /// <param name="opt">the <see cref="QueryOption"/></param>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        IEnumerable<Feed> List(QueryOption opt);
        /// <summary>
        /// Finds a feed by its path.
        /// </summary>
        /// <param name="path">the feed path to find</param>
        /// <returns>the feed of the path, or null if not found</returns>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        Feed Find(String path);
        /// <summary>
        /// Finds a feed by its path with given options.
        /// </summary>
        /// <param name="path">the feed path to find</param>
        /// <param name="opt">the <see cref="QueryOption"/></param>
        /// <returns>the feed of the path, or null if not found</returns>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        Feed Find(String path, QueryOption opt);
        /// <summary>
        /// Creates a feed.
        /// </summary>
        /// <param name="feed">the feed to create</param>
        /// <returns>true if succeed, otherwise false</returns>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        Boolean Create(Feed feed);
        /// <summary>
        /// Updates a feed.
        /// </summary>
        /// <param name="feed">the feed to update</param>
        /// <returns>true if succeed, otherwise false</returns>
        /// <exception cref="ServiceException"><seealso cref="ServiceException.StatusCode"/></exception>
        Boolean Update(Feed feed);
    }
}
