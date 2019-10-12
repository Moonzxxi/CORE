using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestInterbankTransferFromBank2toBank1 : Request
    {
        #region Atributes and Constructors
        private string account_Root;
        private string account_To_Affect;

        public string Account_Root { get => account_Root; set => account_Root = value; }

        public string Account_To_Affect { get => account_To_Affect; set => account_To_Affect = value; }

        private RequestInterbankTransferFromBank2toBank1()
        {

        }

        public RequestInterbankTransferFromBank2toBank1(string first_Account, string second_Account)
        {
            Account_Root = first_Account.ToUpper();
            Account_To_Affect = second_Account;
        }
        #endregion
    }
}