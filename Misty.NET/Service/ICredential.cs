/*
 * SmeshLink.Misty.Service.ICredential.cs
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

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Represents a service credential.
    /// </summary>
    public interface ICredential
    {
        /// <summary>
        /// Gets credential in a key-value pair.
        /// </summary>
        KeyValuePair<String, String> GetCredential();
    }
}
