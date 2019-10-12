using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestDeposit : Request
    {
        #region Atributes and Constructors
        private string account_name;
        private decimal balance_to_deposit;

        public string Account_Name { get => account_name; set => account_name = value; }

        public decimal Balance_To_Deposit { get => balance_to_deposit; set => balance_to_deposit = value; }

        private RequestDeposit()
        {

        }

        public RequestDeposit(string account, string identifier, decimal balance, string pin)
        {
            Identifier = identifier.ToUpper();
            Account_Name = account.ToUpper();
            Balance_To_Deposit = balance;
            Pin = pin.ToUpper();
        }
        #endregion
    }
}