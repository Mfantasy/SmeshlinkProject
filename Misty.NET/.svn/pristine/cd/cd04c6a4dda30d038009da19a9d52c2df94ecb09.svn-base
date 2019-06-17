/*
 * SmeshLink.Misty.Util.DateTimeUtils.cs
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
using System.Globalization;

namespace SmeshLink.Misty.Util
{
    /// <summary>
    /// DateTime Utilities.
    /// </summary>
    public static class DateTimeUtils
    {
        static readonly String[] iso8601Formats = new String[] { 
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'ffK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fK",
            "yyyy'-'MM'-'dd'T'HH':'mm':'ssK",
        };

        /// <summary>
        /// Unix epoch
        /// </summary>
        public static readonly DateTime UnixTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Converts a date to a unix timestamp.
        /// </summary>
        public static Double ToUnixTimestamp(DateTime date)
        {
            if (date.Kind != DateTimeKind.Utc)
                date = date.ToUniversalTime();
            return (date - UnixTime).TotalSeconds;
        }

        /// <summary>
        /// Converts a unix timestamp to a date.
        /// </summary>
        public static DateTime FromUnixTimestamp(Double timestamp)
        {
            return UnixTime.AddSeconds(timestamp);
        }

        /// <summary>
        /// Converts a date to ISO8601 date string.
        /// </summary>
        public static String ToDateTime8601(DateTime d)
        {
            return d.ToUniversalTime().ToString(iso8601Formats[0]);
        }

        /// <summary>
        /// Converts an ISO8601 date string to a date.
        /// </summary>
        public static DateTime FromDateTime8601(String dateTime8601)
        {
            DateTime result;
            if (!TryParseDateTime8601(dateTime8601, out result))
                result = DateTime.MinValue;
            return result;
        }

        private static Boolean TryParseDateTime8601(String date, out DateTime result)
        {
            return DateTime.TryParseExact(date, iso8601Formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out result);
        }
    }
}
