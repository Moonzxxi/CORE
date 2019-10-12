using System;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestThirdPartyTransfer : Request
    {
        #region Atributes and Constructors
        private string thirdPartyName;
        private string account_Name;
        private decimal balance_To_Thirds;

        public string ThirdPartyName { get => thirdPartyName; set => thirdPartyName = value; }

        public string Account_Name { get => account_Name; set => account_Name = value; }

        public decimal Balance_To_Thirds { get => balance_To_Thirds; set => balance_To_Thirds = value; }

        private RequestThirdPartyTransfer()
        {

        }

        public RequestThirdPartyTransfer(string identifier, string tp_name, string account, decimal balance, string pin)
        {
            ThirdPartyName = tp_name.ToUpper();
            Identifier = identifier.ToUpper();
            Account_Name = account.ToUpper();
            Balance_To_Thirds = balance;
            Pin = pin.ToUpper();
        }
        #endregion
    }
}