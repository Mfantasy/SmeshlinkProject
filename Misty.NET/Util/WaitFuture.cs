/*
 * SmeshLink.Misty.Util.WaitFuture.cs
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

namespace SmeshLink.Misty.Util
{
    public class WaitFuture<TRequest, TResponse> : IDisposable
    {
        private readonly TRequest _request;
        private TResponse _response;
        private System.Threading.ManualResetEvent _mre = new System.Threading.ManualResetEvent(false);

        public WaitFuture(TRequest request)
        {
            _request = request;
        }

        public TRequest Request
        {
            get { return _request; }
        }

        public TResponse Response
        {
            get { return _response; }
            set
            {
                _response = value;
                _mre.Set();
                _mre.Close();
            }
        }

        public WaitFuture<TRequest, TResponse> Wait()
        {
            _mre.WaitOne();
            return this;
        }

        public WaitFuture<TRequest, TResponse> Wait(Int32 millisecondsTimeout)
        {
            _mre.WaitOne(millisecondsTimeout);
            return this;
        }

        public void Dispose()
        {
            ((IDisposable)_mre).Dispose();
        }

        public static void WaitAll(IEnumerable<WaitFuture<TRequest, TResponse>> futures)
        {
            foreach (var f in futures)
            {
                f.Wait();
            }
        }

        public static void WaitAll(IEnumerable<WaitFuture<TRequest, TResponse>> futures, Int32 millisecondsTimeout)
        {
            foreach (var f in futures)
            {
                f.Wait(millisecondsTimeout);
            }
        }
    }
}
