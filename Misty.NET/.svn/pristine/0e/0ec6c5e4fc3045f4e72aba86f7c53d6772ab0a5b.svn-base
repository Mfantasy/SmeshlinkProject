/*
 * SmeshLink.Misty.Util.PagedList.cs
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

namespace SmeshLink.Misty.Util
{
    /// <summary>
    /// A list with paging infomation.
    /// </summary>
    public class PagedList<T> : List<T>
    {
        /// <summary>
        /// Gets or sets the total number of items.
        /// </summary>
        public Int32 Total { get; set; }
        /// <summary>
        /// Gets or sets the offset of this page.
        /// </summary>
        public Int32 Offset { get; set; }
        /// <summary>
        /// Gets or sets the limit number of each page.
        /// </summary>
        public Int32 Limit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public PagedList()
        { }

        /// <summary>
        /// 
        /// </summary>
        public PagedList(IEnumerable<T> collection)
            : base(collection)
        { }

        /// <summary>
        /// 
        /// </summary>
        public PagedList(Int32 capacity)
            : base(capacity)
        { }
    }
}
