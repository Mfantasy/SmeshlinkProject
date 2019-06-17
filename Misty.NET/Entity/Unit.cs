/*
 * SmeshLink.Misty.Entity.Unit.cs
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
    /// Represents a unit.
    /// </summary>
    public class Unit
    {
        private String _label;
        private String _symbol;
        private String _type;

        /// <summary>
        /// Gets or sets the unit of this feed, e.g. Celsius.
        /// </summary>
        public String Label
        {
            get { return _label; }
            set { _label = value; }
        }

        /// <summary>
        /// Gets or sets the unit type of this feed.
        /// </summary>
        public String Type
        {
            get { return _type; }
            set { _type = value; }
        }

        /// <summary>
        /// Gets or sets the unit symbol of this feed, e.g. C.
        /// </summary>
        public String Symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
    }
}
