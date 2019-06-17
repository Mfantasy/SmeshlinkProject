/*
 * SmeshLink.Misty.Service.QueryOption.cs
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
using SmeshLink.Misty.Entity;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Represents options when querying feeds.
    /// </summary>
    public class QueryOption
    {
        /// <summary>
        /// Default query options.
        /// </summary>
        public static readonly QueryOption Default = new QueryOption();

        private Int32 _offset = 0;
        private Int32 _limit = -1;
        private DateTime? _startTime;
        private DateTime? _endTime;
        private String _order;
        private Boolean _desc = false;
        private Boolean _hidden;
        private QueryContent _content;
        private QuerySample _sample;
        private QueryView _view;
        private FeedStatus? _status;
        private Int32 _depth = -1;

        /// <summary>
        /// Gets or sets the start position.
        /// </summary>
        public Int32 Offset
        {
            get { return _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// Gets or sets the limit.
        /// </summary>
        public Int32 Limit
        {
            get { return _limit; }
            set { _limit = value; }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        public DateTime? StartTime
        {
            get { return _startTime; }
            set { _startTime = value; }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        public DateTime? EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="QueryContent"/>.
        /// </summary>
        public QueryContent Content
        {
            get { return _content; }
            set { _content = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="QuerySample"/>.
        /// </summary>
        public QuerySample Sample
        {
            get { return _sample; }
            set { _sample = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="QueryView"/>.
        /// </summary>
        public QueryView View
        {
            get { return _view; }
            set { _view = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="FeedStatus"/>.
        /// </summary>
        public FeedStatus? Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Gets or sets the field for ordering.
        /// </summary>
        public String Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating descendent or not.
        /// </summary>
        public Boolean Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether query hidden items or not.
        /// </summary>
        public Boolean Hidden
        {
            get { return _hidden; }
            set { _hidden = value; }
        }

        public Int32 Depth
        {
            get { return _depth; }
            set { _depth = value; }
        }
    }

    /// <summary>
    /// Content option of query result.
    /// </summary>
    public enum QueryContent
    {
        /// <summary>
        /// Not specified.
        /// </summary>
        Whatever,
        /// <summary>
        /// Summary only.
        /// </summary>
        Summary,
        /// <summary>
        /// Full result.
        /// </summary>
        Full
    }

    /// <summary>
    /// View option of query result.
    /// </summary>
    public enum QueryView
    {
        /// <summary>
        /// Not specified.
        /// </summary>
        Whatever,
        /// <summary>
        /// Grouped.
        /// </summary>
        Group,
        /// <summary>
        /// Ungrouped.
        /// </summary>
        Flat
    }

    /// <summary>
    /// Sample option of query result.
    /// </summary>
    public enum QuerySample
    {
        /// <summary>
        /// Not specified.
        /// </summary>
        Whatever,
        None,
        Avg,
        Max,
        Min,
        Random
    }
}
