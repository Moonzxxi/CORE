using Integration.Requests;
using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestAccountRegister : Request
    {
        #region Atributes and Constructors
        private string account_name;
        private string account_type;

        public string Account_Name { get => account_name; set => account_name = value; }

        public string Account_Type { get => account_type; set => account_type = value; }

        private RequestAccountRegister()
        {

        }

        public RequestAccountRegister(string identifier, string name, string type)
        {
            Identifier = identifier.ToUpper();
            Account_Name = name.ToUpper();
            Account_Type = type.ToUpper();
        }
        #endregion
    }
}
