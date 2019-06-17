/*
 * SmeshLink.Misty.Service.ApiKeyCredential.cs
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
    class ApiKeyCredential : ICredential
    {
        private readonly String _apiKey;

        public ApiKeyCredential(String apiKey)
        {
            _apiKey = apiKey;
        }

        public String ApiKey
        {
            get { return _apiKey; }
        }

        public System.Collections.Generic.KeyValuePair<String, String> GetCredential()
        {
            return new System.Collections.Generic.KeyValuePair<String, String>("X-ApiKey", _apiKey);
        }
    }
}
