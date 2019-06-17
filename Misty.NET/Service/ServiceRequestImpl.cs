using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SmeshLink.Misty.Service
{
    public class ServiceRequestImpl : IServiceRequest
    {
        private String _method;
        private String _resource;
        private String _format = "json";
        private NameValueCollection _headers = new NameValueCollection();
        private ICredential _credential;
        private readonly IDictionary<String, Object> _params = new Dictionary<String, Object>();
        private Object _body;
        private String _bodyString;
        private String _token = Guid.NewGuid().ToString();

        public String Method
        {
            get { return _method; }
            set { _method = value; }
        }

        public String Resource
        {
            get { return _resource; }
            set { _resource = value; }
        }

        public String Format
        {
            get { return _format; }
            set { _format = value; }
        }

        public NameValueCollection Headers
        {
            get { return _headers; }
        }

        public IDictionary<String, Object> Parameters
        {
            get { return _params; }
        }

        IEnumerable<KeyValuePair<String, Object>> IServiceRequest.Parameters
        {
            get { return _params; }
        }

        public Object Body
        {
            get { return _body; }
            set { _body = value; }
        }

        public String BodyString
        {
            get { return _bodyString; }
            set { _bodyString = value; }
        }

        public String Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public ICredential Credential
        {
            get { return _credential; }
            set { _credential = value; }
        }

        public String GetParameter(String name)
        {
            Object obj;
            if (_params.TryGetValue(name, out obj) && obj != null)
                return obj.ToString();
            else
                return null;
        }

        public String GetHeader(String name)
        {
            return _headers[name];
        }
    }
}
