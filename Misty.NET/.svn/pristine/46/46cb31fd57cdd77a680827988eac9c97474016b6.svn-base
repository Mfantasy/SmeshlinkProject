/*
 * SmeshLink.Misty.Entity.Entry.cs
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

namespace SmeshLink.Misty.Entity
{
    /// <summary>
    /// Represents a data entry with a certain key-value pair.
    /// </summary>
    public class Entry
    {
        private Int64 _id;
        private Int64 _feedId;
        private KeyTypeEnum _keyType;
        private ValueTypeEnum _valueType;
        private Object _key;
        private Object _value;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the feed id of this entry.
        /// </summary>
        public Int64 FeedId
        {
            get { return _feedId; }
            set { _feedId = value; }
        }

        /// <summary>
        /// Gets the type of key.
        /// </summary>
        public KeyTypeEnum KeyType
        {
            get { return _keyType; }
        }

        /// <summary>
        /// Gets the type of value.
        /// </summary>
        public ValueTypeEnum ValueType
        {
            get { return _valueType; }
        }

        /// <summary>
        /// Gets or sets the key of this entry.
        /// </summary>
        public Object Key
        {
            get { return _key; }
            set
            {
                _key = value;
                if (value != null)
                {
                    if (value is DateTime)
                        _keyType = KeyTypeEnum.Date;
                    else
                        _keyType = KeyTypeEnum.String;
                }
            }
        }

        /// <summary>
        /// Gets or sets the value of this entry.
        /// </summary>
        public Object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (value != null)
                {
                    if (value is Int16 || value is UInt16
                        || value is Int32 || value is UInt32)
                        _valueType = ValueTypeEnum.Integer;
                    else if (value is Double || value is Single || value is Decimal)
                        _valueType = ValueTypeEnum.Number;
                    else if (value is String)
                        _valueType = ValueTypeEnum.String;
                    else if (value is Boolean)
                        _valueType = ValueTypeEnum.Integer;
                    else if (value is Byte[])
                        _valueType = ValueTypeEnum.Bytes;
                }
            }
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return String.Format("{{ {0} : {1} }}", Key, Value);
        }
    }
}
