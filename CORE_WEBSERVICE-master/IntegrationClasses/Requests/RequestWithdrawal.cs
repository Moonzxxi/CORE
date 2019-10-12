using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestWithdrawal : Request
    {
        #region Atributes and Constructors
        private string account_name;
        private decimal balance_to_withdraw;

        public string Account_Name { get => account_name; set => account_name = value; }

        public decimal Balance_To_Withdraw { get => balance_to_withdraw; set => balance_to_withdraw = value; }

        private RequestWithdrawal()
        {

        }

        public RequestWithdrawal(string identifier, string name, decimal balance, string pin)
        {
            Identifier = identifier.ToUpper();
            Account_Name = name.ToUpper();
            Balance_To_Withdraw = balance;
            Pin = pin.ToUpper();
        }
        #endregion
    }
}
