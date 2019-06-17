/*
 * SmeshLink.Misty.Service.ServiceException.cs
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

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Represents errors that occur during service communication.
    /// </summary>
    [Serializable]
    public class ServiceException : Exception
    {
        private readonly Int32 _status;

        public ServiceException(Int32 status)
        {
            _status = status;
        }

        public ServiceException(Int32 status, String message) : base(message)
        {
            _status = status;
        }

        public ServiceException(Int32 status, String message, Exception inner)
            : base(message, inner)
        {
            _status = status;
        }

        /// <inheritdoc/>
        public override String Message
        {
            get
            {
                String msg = base.Message;
                return msg == null && InnerException != null ? InnerException.Message : msg;
            }
        }

        /// <inheritdoc/>
        protected ServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }

        /// <summary>
        /// Gets the status code.
        /// </summary>
        public Int32 StatusCode { get { return _status; } }

        public static void Throw(Int32 status)
        {
            throw new ServiceException(status);
        }

        public static void ThrowForbidden()
        {
            throw new ServiceException(403);
        }

        public static void ThrowError(Exception cause)
        {
            throw new ServiceException(500, null, cause);
        }

        public static void ThrowBadRequest(String message)
        {
            throw new ServiceException(400, message);
        }

        public static void ThrowTimeout(String message)
        {
            throw new ServiceException(504, message);
        }
    }
}
