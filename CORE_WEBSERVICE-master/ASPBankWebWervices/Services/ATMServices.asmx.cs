using System.Web.Services;
using Integration.Responses;
using Integration.Requests;

namespace ASPBankWebWervices.Services
{
    [WebService(Namespace = "http://moneybox.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class ATMServices : System.Web.Services.WebService
    {

        [WebMethod]
        public ResponseToDeposit Deposit(string account_name, string identifier, decimal balance, string pin)
        {
            RequestDeposit deposit1 = new RequestDeposit(account_name, identifier, balance, pin);
            ResponseToDeposit deposit2 = ResponseToDeposit.DepositResponse(deposit1);

            return deposit2;
        }

        [WebMethod]
        public ResponseToTransfer Transfer(string identifier1, string identifier2, string account1, string account2, decimal balance, string pin)
        {
            RequestTransfer transfer1 = new RequestTransfer(identifier1, identifier2, account1, account2, balance, pin);
            ResponseToTransfer transfer2 = ResponseToTransfer.ResponseToATransfer(transfer1);
            return transfer2;
        }

        [WebMethod]
        public ResponseToWithdrawal Withdrawal(string account_name, string identifier, decimal balance, string pin)
        {
            RequestWithdrawal withdrawal1 = new RequestWithdrawal(identifier, account_name, balance, pin);
            ResponseToWithdrawal withdrawal2 = ResponseToWithdrawal.WithdrawalResponse(withdrawal1);
            return withdrawal2;
        }


    }
}