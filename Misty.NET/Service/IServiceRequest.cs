/*
 * SmeshLink.Misty.Service.IServiceRequest.cs
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
using System.Collections.Specialized;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Represents a request to a service.
    /// </summary>
    public interface IServiceRequest
    {
        /// <summary>
        /// Gets the name of the method with which this
        /// request was made, for example, GET, POST, PUT, or DELETE.
        /// </summary>
        String Method { get; }
        /// <summary>
        /// Gets the fully qualified name of the resource being requested.
        /// </summary>
        String Resource { get; }
        /// <summary>
        /// Gets or sets the name of format used in this request.
        /// </summary>
        String Format { get; }
        /// <summary>
        /// Gets the collection of request headers.
        /// </summary>
        NameValueCollection Headers { get; }
        /// <summary>
        /// Gets the request parameters.
        /// </summary>
        IEnumerable<KeyValuePair<String, Object>> Parameters { get; }
        /// <summary>
        /// Gets or sets the <see cref="ICredential"/> associated with this request.
        /// </summary>
        ICredential Credential { get; set; }
        /// <summary>
        /// Gets or sets the token.
        /// </summary>
        String Token { get; }
        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        Object Body { get; set; }
        /// <summary>
        /// Gets or sets the string representation of body.
        /// </summary>
        String BodyString { get; set; }
        /// <summary>
        /// Gets the value of a header.
        /// </summary>
        /// <param name="name">the header's name</param>
        /// <returns>the value or null if not found</returns>
        String GetHeader(String name);
        /// <summary>
        /// Gets the value of a parameter.
        /// </summary>
        /// <param name="name">the parameter's name</param>
        /// <returns>the value or null if not found</returns>
        String GetParameter(String name);
    }

    /// <summary>
    /// Request EventArgs
    /// </summary>
    public class RequestEventArgs : EventArgs
    {
        readonly IServiceRequest _request;
        private IServiceResponse _response;

        /// <summary>
        /// </summary>
        public RequestEventArgs(IServiceRequest request)
        {
            _request = request;
        }

        /// <summary>
        /// Gets the request.
        /// </summary>
        public IServiceRequest Request
        {
            get { return _request; }
        }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        public IServiceResponse Response
        {
            get { return _response; }
            set { _response = value; }
        }
    }
}
