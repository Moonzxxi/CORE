using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestGeneralClientBalance : Request
    {
        #region Atributes and Constructors
        private bool incInactive;
        private string password;

        public bool IncInactive { get => incInactive; set => incInactive = value; }

        public string Password { get => password; set => password = value; }

        private RequestGeneralClientBalance()
        {

        }

        public RequestGeneralClientBalance(string identifier, string password, bool inactive_too)
        {
            IncInactive = inactive_too;
            Identifier = identifier.ToUpper();
            Password = password;
        }
        #endregion
    }
}
