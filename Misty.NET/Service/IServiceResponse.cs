/*
 * SmeshLink.Misty.Service.IServiceResponse.cs
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
using System.Collections.Specialized;
using System.IO;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Represents a response from a service.
    /// </summary>
    public interface IServiceResponse
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        Int32 StatusCode { get; set; }
        /// <summary>
        /// Gets the collection of response headers.
        /// </summary>
        NameValueCollection Headers { get; }
        /// <summary>
        /// Gets the fully qualified name of the resource being requested.
        /// </summary>
        String Resource { get; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        String Token { get; set; }
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        Object Body { get; set; }
        /// <summary>
        /// Appends a response header.
        /// </summary>
        void AppendHeader(String name, String value);
        /// <summary>
        /// Gets the response stream.
        /// </summary>
        Stream GetResponseStream();
    }
}
