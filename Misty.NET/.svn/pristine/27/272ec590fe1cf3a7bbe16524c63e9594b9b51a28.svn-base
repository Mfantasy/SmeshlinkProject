/*
 * SmeshLink.Misty.Formatter.JsonFormatter.cs
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmeshLink.Misty.Entity;
using SmeshLink.Misty.Service;
using SmeshLink.Misty.Util;

namespace SmeshLink.Misty.Formatter
{
    /// <summary>
    /// JSON formatter.
    /// </summary>
    public class JsonFormatter : IFeedFormatter
    {
        /// <summary>
        /// Instance of JsonFormatter.
        /// </summary>
        public static readonly JsonFormatter Instance = new JsonFormatter();

        const String TAG_NAME = "name";
        const String TAG_TITLE = "title";
        const String TAG_DESCRIPTION = "description";
        const String TAG_WEBSITE = "website";
        const String TAG_EMAIL = "email";
        const String TAG_CREATED = "created";
        const String TAG_UPDATED = "updated";
        const String TAG_CURRENT = "current";
        const String TAG_KEYTYPE = "keyType";
        const String TAG_VALUETYPE = "valueType";
        const String TAG_STATUS = "status";
        const String TAG_TAGS = "tags";
        const String TAG_LOCATION = "location";
        const String TAG_LOCATION_NAME = "name";
        const String TAG_DOMAIN = "domain";
        const String TAG_EXPOSURE = "exposure";
        const String TAG_DISPOSITION = "disposition";
        const String TAG_ELEVATION = "ele";
        const String TAG_LONGITUDE = "lng";
        const String TAG_LATITUDE = "lat";
        const String TAG_SPEED = "speed";
        const String TAG_BEARING = "bearing";
        const String TAG_UNIT = "unit";
        const String TAG_UNIT_LABEL = "label";
        const String TAG_UNIT_TYPE = "type";
        const String TAG_UNIT_SYMBOL = "symbol";
        const String TAG_PRIVATE = "private";
        const String TAG_HIDDEN = "hidden";
        const String TAG_CHILDREN = "children";
        const String TAG_DATA = "data";
        const String TAG_KEY = "key";
        const String TAG_AT = "at";
        const String TAG_VALUE = "value";
        const String TAG_ERROR = "error";
        const String TAG_ERROR_STATUS = "status";
        const String TAG_ERROR_MESSAGE = "message";

        /// <inheritdoc/>
        public Feed ParseFeed(Stream stream)
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            set.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            JObject jObj = (JObject)JsonConvert.DeserializeObject(new StreamReader(stream).ReadToEnd());
            return ParseFeed(jObj);
        }

        /// <inheritdoc/>
        public IEnumerable<Feed> ParseFeeds(Stream stream)
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            set.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            JObject jObj = (JObject)JsonConvert.DeserializeObject(new StreamReader(stream).ReadToEnd());
            return ParseFeeds(jObj);
        }

        /// <inheritdoc/>
        public IEnumerable<Feed> ParseFeeds(Object obj)
        {
            IEnumerable<Feed> feeds = obj as IEnumerable<Feed>;
            if (feeds != null)
                return feeds;
            JObject jObj = obj as JObject;
            if (jObj != null)
                return ParseFeeds(jObj);
            return null;
        }

        /// <inheritdoc/>
        public Feed ParseFeed(Object obj)
        {
            Feed feed = obj as Feed;
            if (feed != null)
                return feed;
            JObject jObj = obj as JObject;
            if (jObj != null)
                return ParseFeed(jObj);
            return null;
        }

        private static IEnumerable<Feed> ParseFeeds(JObject jObj)
        {
            PagedList<Feed> list = new PagedList<Feed>();
            list.Total = jObj.Value<Int32>("totalResults");
            JArray array = jObj.Value<JArray>("results");
            if (array != null)
            {
                list.Capacity = array.Count;
                foreach (JObject item in array)
                {
                    if (item != null)
                        list.Add(ParseFeed(item));
                }
            }
            return list;
        }

        private static Feed ParseFeed(JObject jObj)
        {
            Feed feed = new Feed();

            feed.Name = jObj.Value<String>(TAG_NAME);
            feed.Title = jObj.Value<String>(TAG_TITLE);
            feed.Description = jObj.Value<String>(TAG_DESCRIPTION);
            feed.Website = jObj.Value<String>(TAG_WEBSITE);
            feed.Email = jObj.Value<String>(TAG_EMAIL);
            feed.Created = jObj.Value<DateTime>(TAG_CREATED);
            feed.Updated = jObj.Value<DateTime>(TAG_UPDATED);
            feed.KeyType = ParseEnum<KeyTypeEnum>(jObj.Value<String>(TAG_KEYTYPE));
            feed.ValueType = ParseEnum<ValueTypeEnum>(jObj.Value<String>(TAG_VALUETYPE));
            feed.Status = ParseEnum<FeedStatus>(jObj.Value<String>(TAG_STATUS));

            Boolean? hidden = jObj.Value<Boolean?>(TAG_HIDDEN);
            if (hidden.HasValue)
            {
                feed.Access = hidden.Value ? Accessibility.Hidden : Accessibility.Private;
            }
            else
            {
                Boolean? priv = jObj.Value<Boolean?>(TAG_PRIVATE);
                if (priv.HasValue)
                    feed.Access = priv.Value ? Accessibility.Private : Accessibility.Public;
            }

            JObject location = jObj.Value<JObject>(TAG_LOCATION);
            if (location != null)
            {
                Location loc = new Location();
                feed.Location = loc;
                loc.Name = location.Value<String>(TAG_LOCATION_NAME);
                loc.Domain = ParseNullableEnum<LocationDomain>(location.Value<String>(TAG_DOMAIN));
                loc.Exposure = ParseNullableEnum<LocationExposure>(location.Value<String>(TAG_EXPOSURE));
                loc.Disposition = ParseNullableEnum<LocationDisposition>(location.Value<String>(TAG_DISPOSITION));
                loc.Elevation = location.Value<Double?>(TAG_ELEVATION);
                loc.Longitude = location.Value<Double?>(TAG_LONGITUDE);
                loc.Latitude = location.Value<Double?>(TAG_LATITUDE);
                loc.Speed = location.Value<Double?>(TAG_SPEED);
                loc.Bearing = location.Value<Double?>(TAG_BEARING);

                JArray wpArray = location.Value<JArray>("waypoints");
                if (wpArray != null)
                {
                    foreach (JToken item in wpArray)
                    {
                        Waypoint wp = new Waypoint();
                        wp.At = item.Value<DateTime>(TAG_AT);
                        wp.Latitude = item.Value<Double?>(TAG_LATITUDE);
                        wp.Longitude = item.Value<Double?>(TAG_LONGITUDE);
                        wp.Elevation = item.Value<Double?>(TAG_ELEVATION);
                        wp.Speed = item.Value<Double?>(TAG_SPEED);
                        wp.Bearing = item.Value<Double?>(TAG_BEARING);
                        feed.Waypoints.Add(wp);
                    }
                }
            }

            JArray tags = jObj.Value<JArray>(TAG_TAGS);
            if (tags != null)
            {
                feed.AddTags(tags.Values<String>());
            }

            JToken token = jObj[TAG_CURRENT] ?? jObj[TAG_VALUE];
            if (token != null)
                feed.CurrentValue = ParseValue(feed, token);

            JArray data = jObj.Value<JArray>(TAG_DATA);
            if (data != null)
            {
                foreach (JObject entryObj in data)
                {
                    if (entryObj == null)
                        continue;
                    Entry entry = ParseEntry(feed, entryObj);
                    if (entry != null)
                        feed.AddEntry(entry);
                }
            }

            JArray children = jObj.Value<JArray>(TAG_CHILDREN);
            if (children != null)
            {
                foreach (JObject childObj in children)
                {
                    if (jObj == null)
                        continue;
                    Feed child = ParseFeed(childObj);
                    feed.AddChild(child);
                }
            }

            return feed;
        }

        private static Entry ParseEntry(Feed feed, JObject jObj)
        {
            JValue jVal = jObj.Value<JValue>(TAG_AT);
            Object key;
            if (jVal.Type == JTokenType.Date)
                key = jVal.Value;
            else
                key = jObj.Value<String>(TAG_KEY);

            if (key == null)
                return null;
            else
            {
                ValueTypeEnum valueType = feed.ValueType;
                JToken valueToken = jObj.Value<JToken>(TAG_VALUE);
                if (valueType == ValueTypeEnum.None)
                    valueType = GetValueType(valueToken);

                Entry entry = new Entry();
                if (entry != null)
                {
                    entry.Key = key;
                    entry.Value = ParseValue(feed, valueToken);
                }
                return entry;
            }
        }

        private static ValueTypeEnum GetValueType(JToken jToken)
        {
            if (jToken.Type == JTokenType.String)
                return ValueTypeEnum.String;
            else if (jToken.Type == JTokenType.Integer)
                return ValueTypeEnum.Integer;
            else if (jToken.Type == JTokenType.Bytes)
                return ValueTypeEnum.Bytes;
            else
                // assume number
                return ValueTypeEnum.Number;
        }

        private static Object ParseValue(Feed feed, JToken jToken)
        {
            ValueTypeEnum valueType = feed.ValueType;
            if (valueType == ValueTypeEnum.None)
                valueType = GetValueType(jToken);

            if (jToken.Type == JTokenType.Array)
            {
                JArray array = (JArray)jToken;
                Object[] objs = new Object[array.Count];
                for (int i = 0; i < objs.Length; i++)
                {
                    objs[i] = ParseValue(array[i], valueType);
                }
                return objs;
            }
            else
            {
                return ParseValue(jToken, valueType);
            }
        }

        private static Object ParseValue(JToken jToken, ValueTypeEnum valueType)
        {
            switch (valueType)
            {
                case ValueTypeEnum.Integer:
                    return (Int32)jToken;
                case ValueTypeEnum.Number:
                    return (Double)jToken;
                case ValueTypeEnum.String:
                    return (String)jToken;
                case ValueTypeEnum.Bytes:
                    return (Byte[])jToken;
                default:
                    break;
            }

            return null;
        }

        /// <inheritdoc/>
        public void Format(Stream output, IEnumerable<Feed> feeds, FormatOption option)
        {
            using (JsonWriter jw = GetWriter(output))
            {
                Write(jw, feeds, option);

                jw.Flush();
            }
        }

        /// <inheritdoc/>
        public void Format(TextWriter writer, Feed feed, FormatOption option)
        {
            using (JsonWriter jw = GetWriter(writer))
            {
                Write(jw, feed, option);

                jw.Flush();
            }
        }

        /// <inheritdoc/>
        public void Format(Stream output, Feed feed, FormatOption option)
        {
            Format(new StreamWriter(output), feed, option);
        }

        private static void Write(JsonWriter jw, IEnumerable<Feed> feeds, FormatOption option)
        {
            jw.WriteStartObject();

            PagedList<Feed> pl = feeds as PagedList<Feed>;
            if (pl != null)
            {
                WriteValue(jw, "totalResults", pl.Total);
            }

            jw.WritePropertyName("results");

            jw.WriteStartArray();
            foreach (Feed feed in feeds)
            {
                Write(jw, feed, option);
            }
            jw.WriteEndArray();

            jw.WriteEndObject();
        }

        private static void Write(JsonWriter jw, Feed feed, FormatOption option)
        {
            jw.WriteStartObject();

            if ((option & FormatOption.Basic) == FormatOption.Basic)
            {
                WriteValue(jw, TAG_NAME, feed.Name);
                //WriteValue(jw, "feed", API_URI + "feeds/" + feed.name + ".json");
            }

            if ((option & FormatOption.Metadata) == FormatOption.Metadata)
            {
                WriteValue(jw, TAG_TITLE, feed.Title);
                WriteValue(jw, TAG_DESCRIPTION, feed.Description);
                WriteValue(jw, TAG_STATUS, feed.Status.ToString().ToLowerInvariant());
                WriteValue(jw, TAG_WEBSITE, feed.Website);
                WriteValue(jw, TAG_EMAIL, feed.Email);

                if (feed.Created != DateTime.MinValue)
                    WriteValue(jw, TAG_CREATED, feed.Created);
                if (feed.Updated != DateTime.MinValue)
                    WriteValue(jw, TAG_UPDATED, feed.Updated);

                if (feed.KeyType != KeyTypeEnum.None)
                    WriteValue(jw, TAG_KEYTYPE, feed.KeyType.ToString().ToLowerInvariant());
                if (feed.ValueType != ValueTypeEnum.None)
                    WriteValue(jw, TAG_VALUETYPE, feed.ValueType.ToString().ToLowerInvariant());

                if (feed.Access == Accessibility.Private)
                    WriteValue(jw, TAG_PRIVATE, true);
                else if (feed.Access == Accessibility.Hidden)
                {
                    WriteValue(jw, TAG_PRIVATE, true);
                    WriteValue(jw, TAG_HIDDEN, true);
                }

                if (feed.Tags != null)
                {
                    jw.WritePropertyName(TAG_TAGS);
                    jw.WriteStartArray();
                    foreach (String tag in feed.Tags)
                    {
                        jw.WriteValue(tag);
                    }
                    jw.WriteEndArray();
                }

                if (feed.Location != null || feed.Waypoints.Count > 0)
                {
                    Location loc = feed.Location;

                    jw.WritePropertyName(TAG_LOCATION);
                    jw.WriteStartObject();
                    if (loc.Domain.HasValue)
                        WriteValue(jw, TAG_DOMAIN, loc.Domain.ToString().ToLowerInvariant());
                    if (loc.Disposition.HasValue)
                        WriteValue(jw, TAG_DISPOSITION, loc.Disposition.ToString().ToLowerInvariant());
                    if (loc.Exposure.HasValue)
                        WriteValue(jw, TAG_EXPOSURE, loc.Exposure.ToString().ToLowerInvariant());
                    WriteValue(jw, TAG_LOCATION_NAME, loc.Name);
                    WriteValue(jw, TAG_LATITUDE, loc.Latitude);
                    WriteValue(jw, TAG_LONGITUDE, loc.Longitude);
                    WriteValue(jw, TAG_ELEVATION, loc.Elevation);
                    WriteValue(jw, TAG_SPEED, loc.Speed);
                    WriteValue(jw, TAG_BEARING, loc.Bearing);

                    if (feed.Waypoints.Count > 0)
                    {
                        jw.WritePropertyName("waypoints");
                        jw.WriteStartArray();
                        foreach (Waypoint p in feed.Waypoints)
                        {
                            jw.WriteStartObject();
                            WriteValue(jw, TAG_AT, p.At);
                            WriteValue(jw, TAG_LATITUDE, p.Latitude);
                            WriteValue(jw, TAG_LONGITUDE, p.Longitude);
                            WriteValue(jw, TAG_ELEVATION, p.Elevation);
                            WriteValue(jw, TAG_SPEED, p.Speed);
                            WriteValue(jw, TAG_BEARING, p.Bearing);
                            jw.WriteEndObject();
                        }
                        jw.WriteEndArray();
                    }

                    jw.WriteEndObject();
                }

                if (feed.Unit != null)
                {
                    Unit unit = feed.Unit;
                    jw.WritePropertyName(TAG_UNIT);
                    jw.WriteStartObject();
                    WriteValue(jw, TAG_UNIT_LABEL, unit.Label);
                    WriteValue(jw, TAG_UNIT_SYMBOL, unit.Symbol);
                    WriteValue(jw, TAG_UNIT_TYPE, unit.Type);
                    jw.WriteEndObject();
                }

                if (feed.CurrentValue != null)
                {
                    WriteValue(jw, TAG_CURRENT, feed.CurrentValue);
                }
            }

            if ((option & FormatOption.Data) == FormatOption.Data)
            {
                IEnumerator<Entry> itEntry = feed.Entries.GetEnumerator();
                if (itEntry.MoveNext())
                {
                    jw.WritePropertyName(TAG_DATA);
                    jw.WriteStartArray();
                    do
                    {
                        Write(jw, itEntry.Current);
                    } while (itEntry.MoveNext());
                    jw.WriteEndArray();
                }
            }

            IEnumerator<Feed> itChild = feed.Children.GetEnumerator();
            if (itChild.MoveNext())
            {
                jw.WritePropertyName(TAG_CHILDREN);
                jw.WriteStartArray();
                do
                {
                    Write(jw, itChild.Current, option);
                } while (itChild.MoveNext());
                jw.WriteEndArray();
            }

            jw.WriteEndObject();
        }

        private static void Write(JsonWriter jw, Entry entry)
        {
            jw.WriteStartObject();

            if (entry.KeyType == KeyTypeEnum.Date)
                jw.WritePropertyName(TAG_AT);
            else
                jw.WritePropertyName(TAG_KEY);

            if (entry.KeyType == KeyTypeEnum.Date)
                jw.WriteValue(DateTimeUtils.ToDateTime8601((DateTime)entry.Key));
            else
                jw.WriteValue(entry.Key.ToString());

            jw.WritePropertyName(TAG_VALUE);
            jw.WriteValue(entry.Value);

            jw.WriteEndObject();
        }

        private static void WriteValue(JsonWriter jw, String name, Object value)
        {
            if (value != null)
            {
                jw.WritePropertyName(name);
                jw.WriteValue(value);
            }
        }

        private static JsonWriter GetWriter(TextWriter writer)
        {
            JsonTextWriter jtw = new JsonTextWriter(writer);
            jtw.CloseOutput = false;
            jtw.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            jtw.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            return jtw;
        }

        private static JsonWriter GetWriter(Stream stream)
        {
            return GetWriter(new StreamWriter(stream));
        }

        /// <summary>
        /// Parses a Enum type from a string.
        /// </summary>
        public static T ParseEnum<T>(String text)
        {
            if (String.IsNullOrEmpty(text))
                return default(T);
            else
                return (T)Enum.Parse(typeof(T), text, true);
        }

        /// <summary>
        /// Parses a Nullable Enum type from a string.
        /// </summary>
        public static Nullable<T> ParseNullableEnum<T>(String text) where T : struct
        {
            if (String.IsNullOrEmpty(text))
                return null;
            else
                return (T)Enum.Parse(typeof(T), text, true);
        }

        /// <summary>
        /// Formats a <see cref="IServiceRequest"/>.
        /// </summary>
        public void Format(Stream stream, IServiceRequest request)
        {
            using (JsonWriter jw = GetWriter(new StreamWriter(stream)))
            {
                jw.WriteStartObject();

                WriteValue(jw, "method", request.Method);
                WriteValue(jw, "resource", request.Resource);

                WriteValue(jw, "token", request.Token);

                NameValueCollection nvc = request.Headers;
                if (nvc != null && nvc.Count > 0)
                {
                    jw.WritePropertyName("headers");
                    jw.WriteStartObject();
                    foreach (String key in nvc.Keys)
                    {
                        WriteValue(jw, key, nvc[key]);
                    }
                    jw.WriteEndObject();
                }

                IEnumerable<KeyValuePair<String, Object>> ps = request.Parameters;
                if (ps != null)
                {
                    using (IEnumerator<KeyValuePair<String, Object>> it = ps.GetEnumerator())
                    {
                        if (it.MoveNext())
                        {
                            jw.WritePropertyName("params");
                            jw.WriteStartObject();
                            do
                            {
                                WriteValue(jw, it.Current.Key, it.Current.Value);
                            } while (it.MoveNext());
                            jw.WriteEndObject();
                        }
                    }
                }

                Object body = request.Body;
                if (body != null)
                {
                    jw.WritePropertyName("body");

                    if (body is Feed)
                        Write(jw, (Feed)body, FormatOption.All);
                    else if (body is IEnumerable<Feed>)
                        Write(jw, (IEnumerable<Feed>)body, FormatOption.All);
                    //else if (request.Body is CommandRequest)
                    //    Write(jw, (CommandRequest)request.Body);
                    else
                        jw.WriteValue(body);
                }
                else if (!String.IsNullOrEmpty(request.BodyString))
                {
                    jw.WritePropertyName("body");
                    jw.WriteRawValue(request.BodyString);
                }

                jw.WriteEndObject();

                jw.Flush();
            }
        }

        /// <summary>
        /// Formats a <see cref="IServiceResponse"/>.
        /// </summary>
        public void Format(Stream stream, IServiceResponse response)
        {
            using (JsonWriter jw = GetWriter(new StreamWriter(stream)))
            {
                jw.WriteStartObject();

                WriteValue(jw, "status", response.StatusCode);
                WriteValue(jw, "resource", response.Resource);

                NameValueCollection nvc = response.Headers;
                if (nvc.Count > 0)
                {
                    jw.WritePropertyName("headers");
                    jw.WriteStartObject();
                    foreach (String key in nvc.Keys)
                    {
                        WriteValue(jw, key, nvc[key]);
                    }
                    jw.WriteEndObject();
                }

                WriteValue(jw, "token", response.Token);

                Object body = response.Body;
                if (response.Body != null)
                {
                    jw.WritePropertyName("body");

                    if (body is Feed)
                        Write(jw, (Feed)body, FormatOption.All);
                    else if (body is IEnumerable<Feed>)
                        Write(jw, (IEnumerable<Feed>)body, FormatOption.All);
                    //else if (response.Body is CommandResponse)
                    //    Write(jw, (CommandResponse)response.Body);
                    //else if (response.Body is ServiceException)
                    //    Write(jw, (ServiceException)response.Body);
                    else if (body is JToken)
                        ((JToken)body).WriteTo(jw);
                    else
                        jw.WriteValue(body);
                }

                jw.WriteEndObject();

                jw.Flush();
            }
        }
    }
}
