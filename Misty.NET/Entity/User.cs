/*
 * SmeshLink.Misty.Entity.User.cs
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
    /// Represents a user.
    /// </summary>
    public class User
    {
        private Int64 _id;
        private String _username;

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Int64 Id
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public String Username
        {
            get { return _username; }
            set { _username = value; }
        }
    }
}
