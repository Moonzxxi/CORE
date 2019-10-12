using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestChangeAccountState : Request
    {
        #region Atributes and Constructors
        private string account_name;
        private bool activation;

        public string Account_Name { get => account_name; set => account_name = value; }

        public bool Activation { get => activation; set => activation = value; }

        private RequestChangeAccountState()
        {

        }

        public RequestChangeAccountState(string identifier, string account_Name, string pin, bool activation)
        {
            Identifier = identifier.ToUpper();
            Account_Name = account_Name.ToUpper();
            Pin = pin.ToUpper();
            Activation = activation;
        }
        #endregion
    }
}