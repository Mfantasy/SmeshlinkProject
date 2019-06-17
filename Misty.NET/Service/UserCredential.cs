using System;

namespace SmeshLink.Misty.Service
{
    class UserCredential : ICredential
    {
        private readonly String _username;
        private readonly String _password;
        private readonly String _auth;

        public UserCredential(String username, String password)
        {
            _username = username;
            _password = password;
            _auth = "BASIC " + Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(username + ':' + password));
        }

        public String Username
        {
            get { return _username; }
        }

        public String Password
        {
            get { return _password; }
        }

        public System.Collections.Generic.KeyValuePair<String, String> GetCredential()
        {
            return new System.Collections.Generic.KeyValuePair<String, String>("Authorization", _auth);
        }
    }
}
