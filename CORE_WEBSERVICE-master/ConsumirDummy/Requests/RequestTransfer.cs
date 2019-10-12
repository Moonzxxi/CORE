using System;
using System.ComponentModel.DataAnnotations;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestTransfer
    {
        #region Atributes and Constructors
        private string identifier_root;
        private string identifier_to_affect;
        private string account_root;
        private string account_to_affect;
        private decimal balance_To_Transfer;
        private string pin;

        public string Identifier_Root { get => identifier_root; set => identifier_root = value; }

        public string Identifier_To_Affect { get => identifier_to_affect; set => identifier_to_affect = value; }

        public string Account_Root { get => account_root; set => account_root = value; }

        public string Account_To_Affect { get => account_to_affect; set => account_to_affect = value; }

        public string Pin { get => pin; set => pin = value; }

        public decimal Balance_To_Transfer { get => balance_To_Transfer; set => balance_To_Transfer = value; }

        private RequestTransfer()
        {

        }

        public RequestTransfer(string identifier_Root, string identifier_To_Affect, string account_Root, string account_To_Affect, 
            decimal balance, string pin)
        {
            Identifier_Root = identifier_Root.ToUpper();
            Identifier_To_Affect = identifier_To_Affect.ToUpper();
            Account_Root = account_Root.ToUpper();
            Account_To_Affect = account_To_Affect.ToUpper();
            Balance_To_Transfer = balance;
            Pin = pin.ToUpper();
        }
        #endregion
    }
}