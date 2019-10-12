using System;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestLogToClient : Request
    {
        #region Atributes and Constructors
        private string password;

        public string Password { get => password; set => password = value; }

        private RequestLogToClient()
        {

        }

        public RequestLogToClient(string identifier, string password)
        {
            Identifier = identifier.ToUpper();
            Password = password;
        }
        #endregion
    }
}