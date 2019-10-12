using Integration.Requests;
using Integration.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using log4net;
using Integration.Code;

namespace ASPBankWebWervices.Services
{
    [WebService(Namespace = "http://internetbanking.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class InternetBankingServices : System.Web.Services.WebService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        [WebMethod]
        public ResponseClientAccounts AccountsRequest(bool include_inactive, string identifier)
        {
            RequestClientAccounts request = new RequestClientAccounts(include_inactive, identifier);
            ResponseClientAccounts response = ResponseClientAccounts.ResponseToAllClients(request);

            return response;
        }

        [WebMethod]
        public ResponseToTransfer TransferenciaCuentaPropia(string identifier_Root, string identifier_To_Affect, string account_Root, string account_To_Affect,
            decimal balance, string pin)
        {
            RequestTransfer request = new RequestTransfer(identifier_Root, identifier_To_Affect, account_Root, account_To_Affect,
             balance, pin);
            ResponseToTransfer response = ResponseToTransfer.ResponseToATransfer(request);

            return response;
        }

        [WebMethod]
        public ResponseToThirdParties TransferenciaTerceros(string identifier, string tp_name, string account, decimal balance, string pin)
        {
            RequestThirdPartyTransfer request = new RequestThirdPartyTransfer(identifier, tp_name, account, balance, pin);
            ResponseToThirdParties response = ResponseToThirdParties.ResponseToThirdPartyTansfer(request);

            return response;
        }

        //Transferencia Interbancaria

        //[WebMethod]
        //public ResponseInterbankTransfer TransferenciaInterbancaria(string identifier_Root, string identifier_To_Affect, string account_Root,
        //    string account_To_Affect, decimal balance, string pin)
        //{
        //    RequestInterbankTransfer request = new RequestInterbankTransfer(identifier_Root, identifier_To_Affect, account_Root,
        //     account_To_Affect, balance, pin);
        //    ResponseInterbankTransfer response = ResponseInterbankTransfer;

        //    return response;
        //}

        [WebMethod]
        public ResponseVerifyClientExists VerifyClient(string identifier, string name, string last_name)
        {
            RequestClientExists requestClientExists = new RequestClientExists(identifier, name, last_name);
            ResponseVerifyClientExists response = ResponseVerifyClientExists.ResponseToClientExists(requestClientExists);



            return response;
        }

        [WebMethod]
        public ResponseVerifyAccountExists VerifyAccount(string identifier, string account_name)
        {
            RequestAccountExists request = new RequestAccountExists(identifier, account_name);
            ResponseVerifyAccountExists response = ResponseVerifyAccountExists.ResponseToAccountExists(request);

            return response;
        }

        [WebMethod]
        public ResponseToAllTransactionsOfClient MovimientosCuenta(string identifier, string password)
        {
            RequestAllTransactionsOfClient request = new RequestAllTransactionsOfClient(identifier, password);
            ResponseToAllTransactionsOfClient response = ResponseToAllTransactionsOfClient.ResponseToAllTransactionsOfTheClient(request);

            return response;
        }


        //Objetos como parámetros

        //[WebMethod]
        //public ResponseClientAccounts AccountsRequest(RequestClientAccounts cliente)
        //{
        //    RequestClientAccounts request = new RequestClientAccounts(cliente.IncInactive, cliente.Identifier);
        //    ResponseClientAccounts response = ResponseClientAccounts.ResponseToAllClients(request);

        //    return response;
        //}

        //[WebMethod]
        //public ResponseToTransfer TransferenciaCuentaPropia(RequestTransfer transfer)
        //{
        //    RequestTransfer request = new RequestTransfer(transfer.Identifier_Root, transfer.Identifier_To_Affect, transfer.Account_Root, transfer.Account_To_Affect, transfer.Balance_To_Transfer, transfer.Pin);
        //    ResponseToTransfer response = ResponseToTransfer.ResponseToATransfer(request);

        //    return response;
        //}

        //[WebMethod]
        //public ResponseToThirdParties TransferenciaTerceros(RequestThirdPartyTransfer thirdPartyTransfer)
        //{
        //    RequestThirdPartyTransfer request = new RequestThirdPartyTransfer(thirdPartyTransfer.Identifier, thirdPartyTransfer.ThirdPartyName, thirdPartyTransfer.Account_Name,
        //        thirdPartyTransfer.Balance_To_Thirds, thirdPartyTransfer.Pin);
        //    ResponseToThirdParties response = ResponseToThirdParties.ResponseToThirdPartyTansfer(request);

        //    return response;
        //}

        ////[WebMethod]
        ////public ResponseInterbankTransfer TransferenciaInterbancaria(string identifier_Root, string identifier_To_Affect, string account_Root,
        ////    string account_To_Affect, decimal balance, string pin)
        ////{
        ////    RequestInterbankTransfer request = new RequestInterbankTransfer(identifier_Root, identifier_To_Affect, account_Root,
        ////     account_To_Affect, balance, pin);
        ////    ResponseInterbankTransfer response = ResponseInterbankTransfer;

        ////    return response;
        ////}

        //[WebMethod]
        //public ResponseVerifyClientExists VerifyClient(RequestClientExists clientExists)
        //{
        //    RequestClientExists requestClientExists = new RequestClientExists(clientExists.Identifier, clientExists.First_Name, clientExists.Last_Name);
        //    ResponseVerifyClientExists response = ResponseVerifyClientExists.ResponseToClientExists(requestClientExists);



        //    return response;
        //}

        //[WebMethod]
        //public ResponseVerifyAccountExists VerifyAccount(RequestAccountExists accountExists)
        //{
        //    RequestAccountExists request = new RequestAccountExists(accountExists.Identifier, accountExists.Account_Name);
        //    ResponseVerifyAccountExists response = ResponseVerifyAccountExists.ResponseToAccountExists(request);

        //    return response;
        //}

        //[WebMethod]
        //public ResponseToAllTransactionsOfClient MovimientosCuenta(RequestAllTransactionsOfClient transactions)
        //{
        //    RequestAllTransactionsOfClient request = new RequestAllTransactionsOfClient(transactions.Identifier, transactions.Pin);
        //    ResponseToAllTransactionsOfClient response = ResponseToAllTransactionsOfClient.ResponseToAllTransactionsOfTheClient(request);

        //    return response;
        //}


    }
}
