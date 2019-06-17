/*
 * SmeshLink.Misty.Service.JsonRequest.cs
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
using Newtonsoft.Json.Linq;
using SmeshLink.Misty.Util;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// JSON based request.
    /// </summary>
    public class JsonRequest : IServiceRequest
    {
        private JObject _jsonObj;
        private JObject _headersObj;
        private NameValueCollection _headers;
        private JObject _params;
        private String _method;
        private String _resource;
        private Object _body;
        private String _bodyString;

        /// <summary>
        /// </summary>
        public JsonRequest(JObject jsonObj)
        {
            _jsonObj = jsonObj;
            _method = jsonObj.Value<String>("method");
            _resource = jsonObj.Value<String>("resource");
            _headersObj = jsonObj.Value<JObject>("headers");
            _params = jsonObj.Value<JObject>("params");
            _body = jsonObj["body"];
            if (_body != null)
                _bodyString = Newtonsoft.Json.JsonConvert.SerializeObject(_body);
        }

        /// <inheritdoc/>
        public String Method
        {
            get { return _method; }
        }

        /// <inheritdoc/>
        public ICredential Credential { get; set; }

        /// <inheritdoc/>
        public String Format { get; set; }

        /// <inheritdoc/>
        public String Host { get; set; }

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
                            _headers[pair.Key] = (String)pair.Value;
                        }
                    }
                }
                return _headers;
            }
        }

        /// <inheritdoc/>
        public IEnumerable<KeyValuePair<String, Object>> Parameters
        {
            get
            {
                foreach (JProperty prop in _params.Properties())
                {
                    yield return new KeyValuePair<String, Object>(prop.Name, JsonUtils.ToObject(prop.Value));
                }
                yield break;
            }
        }

        /// <inheritdoc/>
        public String GetHeader(String name)
        {
            if (_headersObj != null)
            {
                JToken token = _headersObj.GetValue(name, StringComparison.OrdinalIgnoreCase);
                if (token != null)
                    return token.Value<String>();
            }
            return null;
        }

        /// <inheritdoc/>
        public String GetParameter(String name)
        {
            if (_params != null)
            {
                JToken token = _params.GetValue(name, StringComparison.OrdinalIgnoreCase);
                if (token != null)
                {
                    if (token.Type == JTokenType.Array)
                    {
                        JArray array = (JArray)token;
                        if (array.First != null)
                            return (String)array.First;
                    }
                    else
                    {
                        return (String)token;
                    }
                }
            }
            return null;
        }

        /// <inheritdoc/>
        public String Token
        {
            get { return _jsonObj.Value<String>("token"); }
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
    }
}
