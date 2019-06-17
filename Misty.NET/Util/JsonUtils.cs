/*
 * SmeshLink.Misty.Util.JsonUtils.cs
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
using Newtonsoft.Json.Linq;

namespace SmeshLink.Misty.Util
{
    /// <summary>
    /// Json Utilities.
    /// </summary>
    public static class JsonUtils
    {
        /// <summary>
        /// Converts a <see cref="JToken"/> to an object.
        /// </summary>
        public static Object ToObject(JToken token)
        {
            switch (token.Type)
            {
                case JTokenType.Array:
                    JArray array = (JArray)token;
                    Object[] objs = new Object[array.Count];
                    for (Int32 i = 0; i < objs.Length; i++)
                    {
                        objs[i] = ToObject(array[i]);
                    }
                    return objs;
                case JTokenType.Boolean:
                    return (Boolean)token;
                case JTokenType.Bytes:
                    return (Byte[])token;
                case JTokenType.Date:
                    return (DateTime)token;
                case JTokenType.Float:
                    return (Single)token;
                case JTokenType.Integer:
                    return (Int32)token;
                case JTokenType.Object:
                    JObject jObj = (JObject)token;
                    Dictionary<String, Object> dic = new Dictionary<String, Object>();
                    foreach (KeyValuePair<String, JToken> item in jObj)
                    {
                        dic[item.Key] = ToObject(item.Value);
                    }
                    return dic;
                case JTokenType.String:
                default:
                    return (String)token;
            }
        }
    }
}
