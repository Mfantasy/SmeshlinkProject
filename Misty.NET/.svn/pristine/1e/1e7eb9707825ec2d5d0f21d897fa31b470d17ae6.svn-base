/*
 * SmeshLink.Misty.Service.MistyService.cs
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
using SmeshLink.Misty.Entity;
using SmeshLink.Misty.Formatter;
using SmeshLink.Misty.Service.Channel;

namespace SmeshLink.Misty.Service
{
    /// <summary>
    /// Provides methods to access Misty services.
    /// </summary>
    public class MistyService
    {
        /// <summary>
        /// Default API host.
        /// </summary>
        public static readonly String DefaultApiHost = "api.misty.smeshlink.com";
        /// <summary>
        /// Version of this library.
        /// </summary>
        public static readonly String Version = "Misty.NET-Lib/1.0";

        private IServiceChannel _channel;
        private ICredential _credential;

        public MistyService()
            : this(DefaultApiHost, true)
        { }

        public MistyService(String host)
            : this(host, true)
        { }

        public MistyService(Boolean useHttp)
            : this(DefaultApiHost, useHttp)
        { }

        public MistyService(String host, Boolean useHttp)
            : this(useHttp ? (IServiceChannel)new HttpChannel(host) : new TcpChannel(host))
        { }

        public MistyService(IServiceChannel channel)
        {
            _channel = channel;
        }

        /// <summary>
        /// Gets or sets the <see cref="ICredential"/> to use.
        /// </summary>
        public ICredential Credential
        {
            get { return _credential; }
            set { _credential = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="IServiceChannel"/> to use.
        /// </summary>
        public IServiceChannel Channel
        {
            get { return _channel; }
            set { _channel = value; }
        }

        /// <summary>
        /// Sets a API key as the credential.
        /// </summary>
        public void SetApiKey(String apiKey)
        {
            _credential = new ApiKeyCredential(apiKey);
        }

        /// <summary>
        /// Sets a user as the credential.
        /// </summary>
        public void SetUser(String username, String password)
        {
            _credential = new UserCredential(username, password);
        }

        /// <summary>
        /// Gets a <see cref="IUserService"/>.
        /// </summary>
        public IUserService User()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a <see cref="IFeedService"/>.
        /// </summary>
        public IFeedService Feed()
        {
            return new FeedServiceImpl(this, null);
        }

        /// <summary>
        /// Gets a <see cref="IFeedService"/>.
        /// </summary>
        public IFeedService Feed(User owner)
        {
            return new FeedServiceImpl(this, owner.Username);
        }

        /// <summary>
        /// Gets a <see cref="IFeedService"/>.
        /// </summary>
        /// <param name="parent">the parent feed</param>
        public IFeedService Feed(Feed parent)
        {
            return new FeedServiceImpl(this, parent == null ? null : ("/feeds/" + parent.Path));
        }

        /// <summary>
        /// Executes a request and returns its response.
        /// </summary>
        /// <param name="request">the <see cref="IServiceRequest"/> to send</param>
        /// <returns>a <see cref="IServiceResponse"/>, or null if timeout</returns>
        public IServiceResponse Execute(IServiceRequest request)
        {
            return _channel.Execute(request);
        }

        private ServiceRequestImpl NewRequest()
        {
            ServiceRequestImpl request = new ServiceRequestImpl();
            request.Credential = _credential;
            KeyValuePair<String, String> cred = _credential.GetCredential();
            request.Headers[cred.Key] = cred.Value;
            return request;
        }

        private static IFeedFormatter GetFormatter(String format)
        {
            if (String.IsNullOrEmpty(format) || "json".Equals(format, StringComparison.OrdinalIgnoreCase))
                return JsonFormatter.Instance;
            else
                return null;
        }

        private static void Dispose(IServiceResponse response)
        {
            IDisposable disposable = response as IDisposable;
            if (disposable != null)
                disposable.Dispose();
        }

        private static void ToParameters(ServiceRequestImpl request, QueryOption opt)
        {
            if (opt.Limit > -1)
                request.Parameters["limit"] = opt.Limit;
            if (opt.Offset > 0)
                request.Parameters["offset"] = opt.Offset;
            if (opt.StartTime.HasValue)
                request.Parameters["start"] = opt.StartTime.Value;
            if (opt.EndTime.HasValue)
                request.Parameters["end"] = opt.EndTime.Value;
            if (!String.IsNullOrEmpty(opt.Order))
                request.Parameters["order"] = opt.Order;
            if (opt.Desc)
                request.Parameters["desc"] = opt.Desc;
            if (opt.Depth >= 0)
                request.Parameters["depth"] = opt.Depth;

            if (opt.Content != QueryContent.Whatever)
                request.Parameters["content"] = opt.Content;
            if (opt.Sample != QuerySample.Whatever)
                request.Parameters["sample"] = opt.Sample;
            if (opt.View != QueryView.Whatever)
                request.Parameters["view"] = opt.View;

            if (opt.Status.HasValue)
                request.Parameters["status"] = opt.Status;
        }

        /// <summary>
        /// Gets the content-type of given format.
        /// </summary>
        public static String GetContentType(String format)
        {
            if ("json".Equals(format, StringComparison.OrdinalIgnoreCase))
                return "application/json";
            else if ("xml".Equals(format, StringComparison.OrdinalIgnoreCase))
                return "application/xml";
            else if ("csv".Equals(format, StringComparison.OrdinalIgnoreCase))
                return "text/csv";
            else
                return null;
        }

        class FeedServiceImpl : IFeedService
        {
            private MistyService _service;
            private readonly String _context;

            public FeedServiceImpl(MistyService service, String context)
            {
                _service = service;

                if (String.IsNullOrEmpty(context))
                    _context = "/feeds";
                else if (context[0] == '/')
                    _context = context;
                else
                    _context = "/" + context;
            }

            public IEnumerable<Feed> List()
            {
                return List(QueryOption.Default);
            }

            public IEnumerable<Feed> List(QueryOption opt)
            {
                ServiceRequestImpl request = _service.NewRequest();
                request.Method = "GET";
                request.Resource = _context;
                ToParameters(request, opt);

                IServiceResponse response = null;
                try
                {
                    response = _service.Execute(request);
                    if (response == null)
                        ServiceException.ThrowTimeout(null);
                    else if (response.StatusCode == 200)
                    {
                        IFeedFormatter formatter = GetFormatter(request.Format);
                        if (response.Body == null)
                            return formatter.ParseFeeds(response.GetResponseStream());
                        else
                            return formatter.ParseFeeds(response.Body);
                    }
                    else
                        ServiceException.Throw(response.StatusCode);
                }
                catch (ServiceException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ServiceException.ThrowError(e);
                }
                finally
                {
                    if (response != null)
                        Dispose(response);
                }

                return null;
            }

            public Feed Find(String path)
            {
                return Find(path, QueryOption.Default);
            }

            public Feed Find(String path, QueryOption opt)
            {
                ServiceRequestImpl request = _service.NewRequest();
                request.Method = "GET";
                request.Resource = _context + "/" + path;
                ToParameters(request, opt);

                IServiceResponse response = null;
                try
                {
                    response = _service.Execute(request);
                    if (response == null)
                        ServiceException.ThrowTimeout(null);
                    else if (response.StatusCode == 200)
                    {
                        IFeedFormatter formatter = GetFormatter(request.Format);
                        if (response.Body == null)
                            return formatter.ParseFeed(response.GetResponseStream());
                        else
                            return formatter.ParseFeed(response.Body);
                    }
                    else if (response.StatusCode != 404)
                        ServiceException.Throw(response.StatusCode);
                }
                catch (ServiceException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ServiceException.ThrowError(e);
                }
                finally
                {
                    if (response != null)
                        Dispose(response);
                }

                return null;
            }

            public Boolean Create(Feed feed)
            {
                ServiceRequestImpl request = _service.NewRequest();
                request.Method = "POST";
                request.Resource = _context;

                using (StringWriter writer = new StringWriter())
                {
                    GetFormatter(request.Format).Format(writer, feed, FormatOption.All);
                    request.BodyString = writer.ToString();
                }

                IServiceResponse response = null;
                try
                {
                    response = _service.Execute(request);
                    if (response == null)
                        ServiceException.ThrowTimeout(null);
                    else if (response.StatusCode == 200 || response.StatusCode == 201)
                        return true;
                    else if (response.StatusCode != 404)
                        ServiceException.Throw(response.StatusCode);
                }
                catch (ServiceException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ServiceException.ThrowError(e);
                }
                finally
                {
                    if (response != null)
                        Dispose(response);
                }

                return false;
            }

            public Boolean Update(Feed feed)
            {
                ServiceRequestImpl request = _service.NewRequest();
                request.Method = "PUT";
                request.Resource = _context + "/" + feed.Name;

                using (StringWriter writer = new StringWriter())
                {
                    GetFormatter(request.Format).Format(writer, feed, FormatOption.All);
                    request.BodyString = writer.ToString();
                }

                IServiceResponse response = null;
                try
                {
                    response = _service.Execute(request);
                    if (response == null)
                        ServiceException.ThrowTimeout(null);
                    else if (response.StatusCode == 200 || response.StatusCode == 204)
                        return true;
                    else if (response.StatusCode != 404)
                        ServiceException.Throw(response.StatusCode);
                }
                catch (ServiceException)
                {
                    throw;
                }
                catch (Exception e)
                {
                    ServiceException.ThrowError(e);
                }
                finally
                {
                    if (response != null)
                        Dispose(response);
                }

                return false;
            }
        }
    }
}
