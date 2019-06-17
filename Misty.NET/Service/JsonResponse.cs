/*
 * SmeshLink.Misty.Service.JsonResponse.cs
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
using Newtonsoft.Json.Linq;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// JSON based response.
    /// </summary>
    public class JsonResponse : IServiceResponse
    {
        private JObject _jsonObj;
        private JObject _headersObj;
        private NameValueCollection _headers;
        private Int32 _status;
        private String _resource;
        private Object _body;
        private String _bodyString;

        /// <summary>
        /// </summary>
        public JsonResponse(JObject jsonObj)
        {
            _jsonObj = jsonObj;
            _status = jsonObj.Value<Int32>("status");
            _resource = jsonObj.Value<String>("resource");
            _headersObj = jsonObj.Value<JObject>("headers");
            _body = jsonObj["body"];
            if (_body != null)
                _bodyString = Newtonsoft.Json.JsonConvert.SerializeObject(_body);
        }

        /// <inheritdoc/>
        public Int32 StatusCode
        {
            get { return _status; }
            set { _jsonObj["status"] = _status = value; }
        }

        /// <inheritdoc/>
        public NameValueCollection Headers
        {
            get
            {
                if (_headers == null)
                {
                    _headers = new NameValueCollection();
                    if (_headersObj != null)
                    {
                        foreach (var pair in _headersObj)
                        {
                            _headers[pair.Key] = pair.Value.Value<String>();
                        }
                    }
                }
                return _headers;
            }
        }

        /// <inheritdoc/>
        public void AppendHeader(String name, String value)
        {
            if (_headersObj == null)
            {
                _headersObj = new JObject();
                _jsonObj["headers"] = _headersObj;
            }
            _headersObj[name] = value;
        }

        /// <inheritdoc/>
        public Object Body
        {
            get { return _body; }
            set { _body = value; }
        }

        /// <inheritdoc/>
        public String BodyString
        {
            get { return _bodyString; }
            set { _bodyString = value; }
        }

        /// <inheritdoc/>
        public String Resource
        {
            get { return _resource; }
        }

        /// <inheritdoc/>
        public String Token
        {
            get { return _jsonObj.Value<String>("token"); }
            set { _jsonObj["token"] = value; }
        }

        /// <inheritdoc/>
        public System.IO.Stream GetResponseStream()
        {
            return null;
        }
    }
}
