/*
 * SmeshLink.Misty.Entity.Location.cs
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
    /// Represents a location.
    /// </summary>
    public class Location
    {
        private String _name;
        private DateTime? _time;
        private LocationDisposition? _disposition;
        private LocationExposure? _exposure;
        private LocationDomain? _domain;
        private Double? _latitude;
        private Double? _longitude;
        private Double? _elevation;
        private Double? _speed;
        private Double? _bearing;

        /// <summary>
        /// Gets or sets the location name.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the location time.
        /// </summary>
        public DateTime? Time
        {
            get { return _time; }
            set { _time = value; }
        }

        /// <summary>
        /// Gets or sets the location disposition.
        /// </summary>
        public LocationDisposition? Disposition
        {
            get { return _disposition; }
            set { _disposition = value; }
        }

        /// <summary>
        /// Gets or sets the location exposure.
        /// </summary>
        public LocationExposure? Exposure
        {
            get { return _exposure; }
            set { _exposure = value; }
        }

        /// <summary>
        /// Gets or sets the location domain.
        /// </summary>
        public LocationDomain? Domain
        {
            get { return _domain; }
            set { _domain = value; }
        }

        /// <summary>
        /// Gets or sets the location latitude.
        /// </summary>
        public Double? Latitude
        {
            get { return _latitude; }
            set { _latitude = value; }
        }

        /// <summary>
        /// Gets or sets the location longitude.
        /// </summary>
        public Double? Longitude
        {
            get { return _longitude; }
            set { _longitude = value; }
        }

        /// <summary>
        /// Gets or sets the location elevation.
        /// </summary>
        public Double? Elevation
        {
            get { return _elevation; }
            set { _elevation = value; }
        }

        /// <summary>
        /// Gets or sets the location speed.
        /// </summary>
        public Double? Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        /// <summary>
        /// Gets or sets the location bearing.
        /// </summary>
        public Double? Bearing
        {
            get { return _bearing; }
            set { _bearing = value; }
        }
    }

    /// <summary>
    /// Location Exposure
    /// </summary>
    public enum LocationExposure
    {
        /// <summary>
        /// Indoor
        /// </summary>
        Indoor,
        /// <summary>
        /// Outdoor
        /// </summary>
        Outdoor
    }

    /// <summary>
    /// Location Exposure
    /// </summary>
    public enum LocationDisposition
    {
        /// <summary>
        /// Fixed
        /// </summary>
        Fixed,
        /// <summary>
        /// Mobile
        /// </summary>
        Mobile
    }

    /// <summary>
    /// Location Exposure
    /// </summary>
    public enum LocationDomain
    {
        /// <summary>
        /// Physical
        /// </summary>
        Physical,
        /// <summary>
        /// Virtual
        /// </summary>
        Virtual
    }
}
