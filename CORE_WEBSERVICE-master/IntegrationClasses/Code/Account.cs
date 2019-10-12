using System;

namespace Integration.Code
{
    [Serializable]
    public class Account
    {
        #region Private Atributes
        private string account_name;
        private string client_ID;
        private string account_state;
        private string account_type;
        private decimal balance;
        private string underflow;
        #endregion

        #region Encapsulation
        public string Account_Name { get => account_name; set => account_name = value; }

        public string Client_ID { get => client_ID; set => client_ID = value; }

        public string Account_State { get => account_state; set => account_state = value; }

        public string Account_Type { get => account_type; set => account_type = value; }

        public decimal Balance { get => balance; set => balance = value; }

        public string Underflow { get => underflow; set => underflow = value; }
        #endregion

        #region Constructors
        public Account()
        {

        }

        public Account(string name, string client_id, string accountState, string accountType, decimal balance)
        {
            Account_Name = name;
            Client_ID = client_id;
            Account_State = accountState;
            Account_Type = accountType;
            Balance = balance;
            Underflow = null;

            if (Balance < 0) Underflow = "YES";
            else Underflow = "NO";
        }
        #endregion
    }
}