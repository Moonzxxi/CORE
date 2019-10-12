using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestAccountExists : Request
    {
        #region Atributes and Constructors
        private string account_name;

        public string Account_Name { get => account_name; set => account_name = value; }


        private RequestAccountExists()
        {

        }

        public RequestAccountExists(string identifier, string name_of_account)
        {
            Account_Name = name_of_account.ToUpper();
            Identifier = identifier.ToUpper();
        }
        #endregion
    }
}