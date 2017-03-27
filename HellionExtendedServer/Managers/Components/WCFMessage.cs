using System.Runtime.Serialization;
using System;

namespace HellionExtendedServer.Managers.Components
{
    [DataContract]
    public class WCFMessage
    {
        private bool _isError;
        private string _message;
        private Exception _error;

        [DataMember]
        private bool IsError
        {
            get { return _isError; }
            set { _isError = value; }
        }

        [DataMember]
        private string Message
        {
            get
            {
                
                return _isError ? _error.ToString() :_message;
            }
            set { _message = value; }
        }

        public WCFMessage(bool isError, string message, Exception exception = null)
        {
            _error = exception;
            _isError = isError;
            _message = message;
        }
    }
}