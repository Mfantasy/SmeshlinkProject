/*
 * SmeshLink.Misty.Entity.Feed.cs
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

namespace SmeshLink.Misty.Entity
{
    /// <summary>
    /// Represents a feed.
    /// </summary>
    [Serializable]
    public class Feed
    {
        private Int64 _id;
        private Int64 _parentId;
        private Feed _parent;
        private String _name;
        private String _title;
        private String _description;
        private KeyTypeEnum _keyType;
        private ValueTypeEnum _valueType;
        private DateTime _created;
        private DateTime _updated;
        private Location _location;
        private Unit _unit;
        private String _website;
        private String _email;
        private FeedStatus _status;
        private Accessibility? _access;
        private Object _currentValue;
        private List<Feed> _children = new List<Feed>();
        private List<String> _tags = new List<String>();
        private List<Waypoint> _waypoints = new List<Waypoint>();
        private Dictionary<Object, Entry> _entries = new Dictionary<Object, Entry>();

        /// <summary>
        /// Gets or sets the id of this feed.
        /// </summary>
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the parent id of this feed.
        /// </summary>
        public Int64 ParentId
        {
            get { return _parentId; }
            set { _parentId = value; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public Feed Parent
        {
            get { return _parent; }
            set { _parent = value; ParentId = value == null ? 0 : value.Id; }
        }

        /// <summary>
        /// Gets or sets the name of this feed.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets the full path of this feed.
        /// </summary>
        public String Path
        {
            get { return _parent == null ? _name : (_parent.Path + "/" + _name); }
        }

        /// <summary>
        /// Gets or sets the title of this feed.
        /// </summary>
        public String Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Gets or sets the description of this feed.
        /// </summary>
        public String Description
        {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Gets or sets the type of key.
        /// </summary>
        public KeyTypeEnum KeyType
        {
            get { return _keyType; }
            set { _keyType = value; }
        }

        /// <summary>
        /// Gets or sets the type of value.
        /// </summary>
        public ValueTypeEnum ValueType
        {
            get { return _valueType; }
            set { _valueType = value; }
        }

        /// <summary>
        /// Gets or sets the created time of this feed.
        /// </summary>
        public DateTime Created
        {
            get { return _created; }
            set
            {
                switch (value.Kind)
                {
                    case DateTimeKind.Unspecified:
                        // treat as a UTC time
                        _created = new DateTime(value.Ticks, DateTimeKind.Utc);
                        break;
                    case DateTimeKind.Utc:
                        _created = value;
                        break;
                    default:
                        _created = value.ToUniversalTime();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the updated time of this feed.
        /// </summary>
        public DateTime Updated
        {
            get { return _updated; }
            set
            {
                switch (value.Kind)
                {
                    case DateTimeKind.Unspecified:
                        // treat as a UTC time
                        _updated = new DateTime(value.Ticks, DateTimeKind.Utc);
                        break;
                    case DateTimeKind.Utc:
                        _updated = value;
                        break;
                    default:
                        _updated = value.ToUniversalTime();
                        break;
                }
            }
        }

        /// <summary>
        /// Gets or sets the location of this feed.
        /// </summary>
        public Location Location
        {
            get { return _location; }
            set { _location = value; }
        }

        /// <summary>
        /// Gets or sets the unit of this feed.
        /// </summary>
        public Unit Unit
        {
            get { return _unit; }
            set { _unit = value; }
        }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        public String Website
        {
            get { return _website; }
            set { _website = value; }
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public String Email
        {
            get { return _email; }
            set { _email = value; }
        }

        /// <summary>
        /// Gets or sets the status of this feed.
        /// </summary>
        public FeedStatus Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Gets or sets the accessibility.
        /// </summary>
        public Accessibility? Access 
        {
            get { return _access; }
            set { _access = value; }
        }

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        public Object CurrentValue
        {
            get { return _currentValue; }
            set { _currentValue = value; }
        }

        /// <summary>
        /// Gets the list of tags of this feed.
        /// </summary>
        public IList<String> Tags
        {
            get { return _tags; }
        }

        /// <summary>
        /// Gets the collection of waypoints of this feed.
        /// </summary>
        public ICollection<Waypoint> Waypoints
        {
            get { return _waypoints; }
        }

        /// <summary>
        /// Gets the children of this feed.
        /// </summary>
        public IEnumerable<Feed> Children
        {
            get { return _children; }
            set
            {
                if (value != null)
                {
                    if (_children == null)
                        _children = new List<Feed>(value);
                    else
                    {
                        _children.Clear();
                        _children.AddRange(value);
                    }
                }
                else if (_children != null)
                {
                    _children.Clear();
                }
            }
        }

        /// <summary>
        /// Gets the entries of this feed.
        /// </summary>
        public IEnumerable<Entry> Entries
        {
            get { return _entries.Values; }
        }

        /// <summary>
        /// Adds a child feed.
        /// </summary>
        /// <param name="child">the child feed to add</param>
        public void AddChild(Feed child)
        {
            child.Parent = this;
            _children.Add(child);
        }

        /// <summary>
        /// Adds a tag.
        /// </summary>
        public void AddTag(String tag)
        {
            _tags.Add(tag);
        }

        /// <summary>
        /// Adds tags.
        /// </summary>
        public void AddTags(IEnumerable<String> tags)
        {
            _tags.AddRange(tags);
        }

        /// <summary>
        /// Adds a entry.
        /// </summary>
        public void AddEntry(Entry entry)
        {
            _entries[entry.Key] = entry;
        }

        /// <summary>
        /// Adds entries.
        /// </summary>
        public void AddEntries(IEnumerable<Entry> entries)
        {
            foreach (Entry entry in entries)
            {
                AddEntry(entry);
            }
        }
    }

    /// <summary>
    /// Type of keys.
    /// </summary>
    public enum KeyTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Date
        /// </summary>
        Date,
        /// <summary>
        /// String
        /// </summary>
        String,
    }

    /// <summary>
    /// Type of values.
    /// </summary>
    public enum ValueTypeEnum
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// Integer
        /// </summary>
        Integer,
        /// <summary>
        /// Number
        /// </summary>
        Number,
        /// <summary>
        /// String
        /// </summary>
        String,
        /// <summary>
        /// Bytes
        /// </summary>
        Bytes
    }

    /// <summary>
    /// Accessibility
    /// </summary>
    public enum Accessibility
    {
        /// <summary>
        /// Public
        /// </summary>
        Public = 0,
        /// <summary>
        /// Private
        /// </summary>
        Private = 1,
        /// <summary>
        /// Hidden
        /// </summary>
        Hidden = 3
    }
}
