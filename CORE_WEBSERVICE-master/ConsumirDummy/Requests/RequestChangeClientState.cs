using System;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestChangeClientState : Request
    {
        #region Atributes and Constructors
        private string password;
        private bool activation;

        public string Password { get => password; set => password = value; }


        public bool Activation { get => activation; set => activation = value; }

        private RequestChangeClientState()
        {

        }

        public RequestChangeClientState(string identifier, string password, bool activate)
        {
            Identifier = identifier.ToUpper();
            Password = password;
            Activation = activate;
        }
        #endregion
    }
}