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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Mina.Core.Buffer;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SmeshLink.Misty.Formatter;
using SmeshLink.Misty.Util;

namespace SmeshLink.Misty.Service.Channel
{
    /// <summary>
    /// TCP channel.
    /// </summary>
    public class TcpChannel : IServiceChannel, IDisposable
    {
        private String _host;
        private Int32 _port = 9011;
        private Int32 _timeout = 30000;
        private Int32 _retryInterval = 10000;
        private Int32 _maxWorkers;
        private readonly ConcurrentDictionary<String, WaitFuture<IServiceRequest, IServiceResponse>> _waitingRequests
            = new ConcurrentDictionary<String, WaitFuture<IServiceRequest, IServiceResponse>>();
        private IWorkerPool _workerPool;
        private Boolean _disposed;

        /// <inheritdoc/>
        public event EventHandler<RequestEventArgs> RequestReceived;

        /// <summary>
        /// </summary>
        public TcpChannel(String host)
        {
            Host = host;
            MaxWorkers = 1;
        }

        /// <summary>
        /// Gets or set the host.
        /// </summary>
        public String Host
        {
            get { return _host; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                Int32 index = value.IndexOf(':');
                if (index < 0)
                {
                    _host = value;
                }
                else
                {
                    _host = value.Substring(0, index);
                    _port = Int32.Parse(value.Substring(index + 1));
                }
            }
        }

        /// <summary>
        /// Gets or set the port.
        /// </summary>
        public Int32 Port
        {
            get { return _port; }
            set { _port = value; }
        }

        /// <inheritdoc/>
        public Int32 Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        /// <summary>
        /// Gets or set interval for reconnecting.
        /// </summary>
        public Int32 RetryInterval
        {
            get { return _retryInterval; }
            set { _retryInterval = value; }
        }

        /// <summary>
        /// Gets or set max number of workers.
        /// </summary>
        public Int32 MaxWorkers
        {
            get { return _maxWorkers; }
            set
            {
                if (_maxWorkers != value)
                {
                    if (_maxWorkers == 1 || value == 1)
                    {
                        if (_workerPool != null)
                            _workerPool.Dispose();

                        if (value == 1)
                            _workerPool = new SingleWorkerPool(this);
                        else
                            _workerPool = new DefaultWorkerPool(this);
                    }

                    _maxWorkers = value;
                }
            }
        }

        /// <inheritdoc/>
        public IServiceResponse Execute(IServiceRequest request)
        {
            Worker worker = _workerPool.GetWorker();
            
            if (worker == null)
                return null;

            if (request.Body != null)
                request.Headers["Content-Type"] = MistyService.GetContentType(request.Format);
            request.Headers["User-Agent"] = MistyService.Version;

            try
            {
                return worker.Send(request).Wait(_timeout).Response;
            }
            finally
            {
                worker.Free();
            }
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes resources.
        /// </summary>
        protected virtual void Dispose(Boolean disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _workerPool.Dispose();
            }

            _disposed = true;
        }

        private void ProcessResponse(IServiceResponse response)
        {
            if (response.Token != null)
            {
                WaitFuture<IServiceRequest, IServiceResponse> f;
                if (_waitingRequests.TryRemove(response.Token, out f))
                    f.Response = response;
            }
            //_channel.FireResponseReceived(response);
        }

        private void ProcessRequest(IServiceRequest request, Worker worker)
        {
            // avoid block
            ThreadPool.QueueUserWorkItem(delegate(Object _)
            {
                IServiceResponse response = FireRequestReceived(request);
                if (response != null)
                {
                    if (response.Token == null)
                        response.Token = request.Token;
                    worker.Send(response);
                }
            });
        }

        private IServiceResponse FireRequestReceived(IServiceRequest request)
        {
            EventHandler<RequestEventArgs> h = RequestReceived;
            if (h != null)
            {
                RequestEventArgs e = new RequestEventArgs(request);
                IServiceResponse response = new JsonResponse(new JObject());
                response.StatusCode = 200;
                e.Response = response;
                h(this, e);
                return e.Response;
            }
            return null;
        }

        interface IWorkerPool : IDisposable
        {
            Worker GetWorker();
            void HandleException(Worker w, Exception e);
            void Wakeup();
        }

        class SingleWorkerPool : IWorkerPool
        {
            readonly TcpChannel _channel;
            Worker _worker;

            public SingleWorkerPool(TcpChannel channel)
            {
                _channel = channel;
            }

            public Worker GetWorker()
            {
                Worker w = _worker;
                if (w == null)
                {
                    lock (this)
                    {
                        if (_worker == null)
                        {
                            _worker = new Worker(this, _channel);
                            _worker.RunAsync();
                        }
                        w = _worker;
                    }
                }

                while (!w.TryTake())
                {
                    Thread.Sleep(1);
                }
                return w;
            }

            public void HandleException(Worker w, Exception e)
            {
                if (e != null)
                {
                    Console.WriteLine("Removing worker " + e.Message);

                    SocketException se = e as SocketException;
                    if (se == null)
                        return;
                }
             
                lock (this)
                {
                    _worker = null;
                }

                try
                {
                    w.Dispose();
                }
                catch
                { /* ignore */ }
            }

            public void Wakeup()
            {
                // do nothing
            }

            public void Dispose()
            {
                Worker w = _worker;
                if (w != null)
                    w.Dispose();
            }
        }

        class DefaultWorkerPool : IWorkerPool
        {
            readonly TcpChannel _channel;
            readonly List<Worker> _workerPool = new List<Worker>();

            public DefaultWorkerPool(TcpChannel channel)
            {
                _channel = channel;
            }

            public Worker GetWorker()
            {
                lock (_workerPool)
                {
                    for (; ; )
                    {
                        foreach (Worker w in _workerPool)
                        {
                            if (w.Available)
                            {
                                w.Take();
                                return w;
                            }
                        }

                        if (_workerPool.Count < _channel._maxWorkers)
                        {
                            Worker w = new Worker(this, _channel);
                            _workerPool.Add(w);
                            w.RunAsync();
                        }

                        try
                        {
                            Monitor.Wait(_workerPool, 3000);
                        }
                        catch (ThreadInterruptedException)
                        {
                            break;
                        }
                    }
                }
                return null;
            }

            public void HandleException(Worker w, Exception e)
            {
                if (e != null)
                {
                    Console.WriteLine("Removing worker " + e.Message);

                    SocketException se = e as SocketException;
                    if (se == null)
                        return;
                }

                lock (_workerPool)
                {
                    _workerPool.Remove(w);
                }

                try
                {
                    w.Dispose();
                }
                catch
                { /* ignore */ }
            }

            public void Wakeup()
            {
                lock (_workerPool)
                {
                    Monitor.PulseAll(_workerPool);
                }
            }

            public void Dispose()
            {
                _workerPool.ForEach(w => w.Dispose());
                _workerPool.Clear();
            }
        }

        class Worker : IDisposable
        {
            readonly IWorkerPool _pool;
            readonly TcpChannel _channel;
            readonly BlockingQueue<Object> _sendingQueue = new BlockingQueue<Object>();
            TcpClient _client;
            Int32 _state = -1;
            Boolean _disposed;

            public Worker(IWorkerPool pool, TcpChannel channel)
            {
                _pool = pool;
                _channel = channel;
            }

            public Boolean Available
            {
                get { return _state == 0 && _client != null && _client.Client != null && _client.Connected; }
            }

            public void RunAsync()
            {
                ThreadPool.QueueUserWorkItem(Run);
            }

            public void Run(Object state)
            {
                for (; ; )
                {
                    try
                    {
                        _client = new TcpClient(_channel.Host, _channel.Port);
                    }
                    catch (SocketException)
                    {
                        if (_client != null)
                            ((IDisposable)_client).Dispose();
                    }

                    if (_client == null && _channel.RetryInterval > 0)
                    {
                        try
                        {
                            Thread.Sleep(_channel.RetryInterval);
                        }
                        catch (ThreadInterruptedException)
                        {
                            break;
                        }
                        continue;
                    }

                    break;
                }

                if (_client == null)
                    return;

                Free();

                ThreadPool.QueueUserWorkItem(Sending, this);
                ThreadPool.QueueUserWorkItem(Receiving, this);
            }

            public Boolean TryTake()
            {
                return Interlocked.CompareExchange(ref _state, 1, 0) == 0;
            }

            public void Take()
            {
                _state = 1;
            }

            public void Free()
            {
                _state = 0;
                _pool.Wakeup();
            }

            public void Dispose()
            {
                if (!_disposed)
                {
                    if (_client != null)
                        ((IDisposable)_client).Dispose();
                    _disposed = true;
                }
            }

            public WaitFuture<IServiceRequest, IServiceResponse> Send(IServiceRequest request)
            {
                WaitFuture<IServiceRequest, IServiceResponse> wf = new WaitFuture<IServiceRequest, IServiceResponse>(request);
                _channel._waitingRequests[request.Token] = wf;
                _sendingQueue.Enqueue(request);
                return wf;
            }

            public void Send(IServiceResponse response)
            {
                _sendingQueue.Enqueue(response);
            }

            private static void Sending(Object state)
            {
                Worker worker = (Worker)state;
                TcpClient client = worker._client;
                BlockingQueue<Object> queue = worker._sendingQueue;
                Object obj;

                while (client.Client != null && client.Connected)
                {
                    try
                    {
                        obj = queue.Dequeue(3000);
                    }
                    catch (ThreadInterruptedException)
                    {
                        obj = null;
                    }

                    if (obj == null)
                        continue;

                    IServiceResponse response = obj as IServiceResponse;
                    if (response != null)
                    {
                        try
                        {
                            JsonFormatter.Instance.Format(client.GetStream(), response);
                        }
                        catch (Exception e)
                        {
                            worker._pool.HandleException(worker, e);
                            break;
                        }
                        continue;
                    }

                    IServiceRequest request = obj as IServiceRequest;
                    if (request != null)
                    {
                        try
                        {
                            JsonFormatter.Instance.Format(client.GetStream(), request);
                        }
                        catch (Exception e)
                        {
                            worker._pool.HandleException(worker, e);
                            WaitFuture<IServiceRequest, IServiceResponse> wf;
                            if (request.Token != null &&
                                worker._channel._waitingRequests.TryRemove(request.Token, out wf))
                                wf.Response = null;
                            break;
                        }
                    }
                }
            }

            private static void Receiving(Object state)
            {
                Worker worker = (Worker)state;
                TcpChannel channel = worker._channel;
                TcpClient client = worker._client;
                Int32 counter = 0;
                Byte[] byteBuffer = new Byte[1024];
                IoBuffer ioBuffer = IoBuffer.Allocate(2048);
                ioBuffer.AutoExpand = true;

                while (client.Client != null && client.Connected)
                {
                    try
                    {
                        Int32 bytesRead = Math.Min(client.Available, byteBuffer.Length);
                        bytesRead = client.Client.Receive(byteBuffer, 0, bytesRead, SocketFlags.None);
                        
                        if (bytesRead == 0 && client.Available == 0)
                            // client disconnects
                            break;

                        for (Int32 i = 0; i < bytesRead; i++)
                        {
                            ioBuffer.Put(byteBuffer[i]);

                            if ('{' == byteBuffer[i])
                            {
                                counter++;
                            }
                            else if ('}' == byteBuffer[i])
                            {
                                counter--;

                                if (counter == 0)
                                {
                                    ioBuffer.Flip();
                                    ArraySegment<Byte> array = ioBuffer.GetRemaining();
                                    String json = Encoding.UTF8.GetString(array.Array, array.Offset, array.Count);
                                    JObject jObj = ToJsonObject(json);

                                    if (jObj["status"] != null)
                                        channel.ProcessResponse(new JsonResponse(jObj));
                                    else if (jObj["method"] != null)
                                        channel.ProcessRequest(new JsonRequest(jObj), worker);

                                    ioBuffer.Clear();
                                }
                            }
                        }
                    }
                    catch (SocketException e)
                    {
                        worker._pool.HandleException(worker, e);
                        return;
                    }
                    catch (Exception e)
                    {
                        // TODO log exception
                        Console.WriteLine(e.Message);
                    }
                }

                if (!worker._disposed)
                    worker._pool.HandleException(worker, null);
            }
        }

        private static JObject ToJsonObject(String value)
        {
            JsonSerializerSettings set = new JsonSerializerSettings();
            set.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            set.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
            return (JObject)JsonConvert.DeserializeObject(value);
        }
    }
}
