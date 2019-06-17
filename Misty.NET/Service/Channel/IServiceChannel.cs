/*
 * SmeshLink.Misty.Service.Channel.IServiceChannel.cs
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

namespace SmeshLink.Misty.Service.Channel
{
    /// <summary>
    /// Represents a channel for service communication.
    /// </summary>
    public interface IServiceChannel
    {
        /// <summary>
        /// Occurs when a <see cref="IServiceRequest"/> is received.
        /// </summary>
        event EventHandler<RequestEventArgs> RequestReceived;
        /// <summary>
        /// Gets or sets the time-out value in milliseconds for <see cref="Execute(IServiceRequest)"/> method.
        /// </summary>
        Int32 Timeout { get; set; }
        /// <summary>
        /// Executes a request and returns its response.
        /// </summary>
        /// <param name="request">the <see cref="IServiceRequest"/> to send</param>
        /// <returns>a <see cref="IServiceResponse"/>, or null if timeout</returns>
        IServiceResponse Execute(IServiceRequest request);
    }
}
