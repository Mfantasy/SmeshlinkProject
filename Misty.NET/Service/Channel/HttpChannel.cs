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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using SmeshLink.Misty.Util;

namespace SmeshLink.Misty.Service.Channel
{
    /// <summary>
    /// HTTP channel.
    /// </summary>
    public class HttpChannel : IServiceChannel
    {
        private String _host;
        private Int32 _timeout;

        /// <inheritdoc/>
        public event EventHandler<RequestEventArgs> RequestReceived;

        /// <summary>
        /// </summary>
        public HttpChannel(String host)
        {
            _host = host;
        }

        /// <summary>
        /// Gets or set the host.
        /// </summary>
        public String Host
        {
            get { return _host; }
            set { _host = value; }
        }

        /// <inheritdoc/>
        public Int32 Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <inheritdoc/>
        public IServiceResponse Execute(IServiceRequest request)
        {
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(BuildUri(request));
            webReq.Method = request.Method;
            webReq.UserAgent = MistyService.Version;
            webReq.Accept = MistyService.GetContentType(request.Format);
            if (_timeout != 0)
                webReq.Timeout = _timeout;

            NameValueCollection headers = request.Headers;
            if (headers != null)
            {
                foreach (String key in headers.Keys)
                {
                    webReq.Headers[key] = headers[key];
                }
            }

            if (request.Body != null &&
                ("POST".Equals(request.Method, StringComparison.OrdinalIgnoreCase)
                || "PUT".Equals(request.Method, StringComparison.OrdinalIgnoreCase)))
            {
                using (Stream stream = webReq.GetRequestStream())
                using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                {
                    writer.Write(request.Body);
                }
            }

            try
            {
                return new HttpResponse(request.Resource, (HttpWebResponse)webReq.GetResponse());
            }
            catch (WebException e)
            {
                return new HttpResponse(request.Resource, (HttpWebResponse)e.Response);
            }
        }

        private Uri BuildUri(IServiceRequest request)
        {
            StringBuilder sb = new StringBuilder();
            Boolean sep = false;
            foreach (KeyValuePair<String, Object> pair in request.Parameters)
            {
                if (sep)
                    sb.Append('&');
                else
                    sep = true;
                sb.Append(pair.Key).Append('=');
                if (pair.Value is DateTime)
                    sb.Append(DateTimeUtils.ToDateTime8601((DateTime)pair.Value));
                else
                    sb.Append(pair.Value);
            }

            UriBuilder ub = new UriBuilder(Uri.UriSchemeHttp, _host);
            ub.Path = String.IsNullOrEmpty(request.Format) ? request.Resource
                : (request.Resource + "." + request.Format);
            ub.Query = sb.ToString();
            return ub.Uri;
        }

        class HttpResponse : IServiceResponse, IDisposable
        {
            private readonly String _resource;
            private readonly HttpWebResponse _response;

            public HttpResponse(String resource, HttpWebResponse response)
            {
                _resource = resource;
                _response = response;
            }

            public void Dispose()
            {
                ((IDisposable)_response).Dispose();
            }

            public Int32 StatusCode
            {
                get { return (Int32)_response.StatusCode; }
                set { throw new NotSupportedException(); }
            }

            public String Token
            {
                get { return null; }
                set { /* do nothing */ }
            }

            public Object Body
            {
                get { return null; }
                set { /* do nothing */ }
            }

            public NameValueCollection Headers
            {
                get { return _response.Headers; }
            }

            public String Resource
            {
                get { return _resource; }
            }

            public Stream GetResponseStream()
            {
                return _response.GetResponseStream();
            }

            public void AppendHeader(String name, String value)
            {
                throw new NotSupportedException();
            }
        }
    }
}
