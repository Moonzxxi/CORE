using System.Web.Services;
using System.Media;
using System.Threading;
using Integration.Responses;
using Integration.Requests;
using System;

namespace ASPBankWebWervices.Services
{
    [WebService(Namespace = "http://integrationservices.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class IntegrationServices : WebService
    {
        #region Atributes and Constructors
        public IntegrationServices()
        {

        }
        #endregion

        #region Service Methods
        /*
        [WebMethod]
        public ResponseClientRegister GetResponseClientRegister(RequestClientRegister requestRegister)
        {
            ResponseClientRegister responseRegister = ResponseClientRegister.ResponseToClientRegistration(requestRegister);
            return responseRegister;
        }

        [WebMethod]
        public ResponseClientRegister ClientRegister(string name, string last, string identifier, string email,
   string pin, string direction, string password, bool auto_generated_pin)
        {
            RequestClientRegister n = new RequestClientRegister(name, last, identifier, password, pin, email, direction, auto_generated_pin);
            ResponseClientRegister ñ = ResponseClientRegister.ResponseToClientRegistration(n);
            return ñ;
        }


        [WebMethod]
        public RequestClientRegister GetClientRegister(string name, string last, string identifier, string password, string pin,
            string email, string direction, bool generation)
        {
            return new RequestClientRegister(name, last, identifier, password, pin,
            email, direction, generation);
        }
        */
        #endregion

        #region Objectless Methods
        [WebMethod]
        public ResponseClientRegister ClientRegister(string name, string last, string identifier, string email,
            string pin, string direction, string password, bool auto_generated_pin)
        {
            RequestClientRegister n = new RequestClientRegister(name, last, identifier, password, pin, email, direction, auto_generated_pin);
            ResponseClientRegister ñ = ResponseClientRegister.ResponseToClientRegistration(n);
            return ñ;
        }

        [WebMethod]
        public ResponseAccountRegister AccountRegister(string identifier, string account_name, string account_type)
        {
            RequestAccountRegister a = new RequestAccountRegister(identifier, account_name, account_type);
            ResponseAccountRegister ra = ResponseAccountRegister.ResponseToAccountRegister(a); ;
            return ra;
        }

        [WebMethod]
        public ResponseToThirdParties TransferToThirdParties(string identifier, string account_name, string third_party_name, decimal balance, string pin)
        {
            RequestThirdPartyTransfer requestThirdPartyTransfer = new RequestThirdPartyTransfer(identifier, third_party_name, account_name, balance, pin);
            ResponseToThirdParties r = ResponseToThirdParties.ResponseToThirdPartyTansfer(requestThirdPartyTransfer);
            return r;
        }

        [WebMethod]
        public ResponseClientAccounts AllAccountsOfClient(string identifier, bool include_inactive)
        {
            RequestClientAccounts c = new RequestClientAccounts(include_inactive, identifier);
            ResponseClientAccounts ca = ResponseClientAccounts.ResponseToAllClients(c);
            return ca;
        }

        [WebMethod]
        public ResponseToAllClients AllClients(bool include_inactive)
        {
            RequestAllClients q = new RequestAllClients(include_inactive);
            ResponseToAllClients ac = ResponseToAllClients.ResponseToAllTheClients(q);
            return ac;
        }

        [WebMethod]
        public ResponseChangedAccountState ChangeAccountOfClientState(bool activate, string pin, string account, string identifier)
        {
            RequestChangeAccountState s = new RequestChangeAccountState(identifier, account, pin, activate);
            ResponseChangedAccountState aa = ResponseChangedAccountState.SelectionResponse(s);
            return aa;

        }

        [WebMethod]
        public ResponseToDeposit DepositToAccount(string account_name, string identifier, decimal balance, string pin)
        {
            RequestDeposit q = new RequestDeposit(account_name, identifier, balance, pin);
            ResponseToDeposit a = ResponseToDeposit.DepositResponse(q);
            return a;
        }

        [WebMethod]
        public ResponseToWithdrawal WithdrawalToAccount(string account_name, string identifier, decimal balance, string pin)
        {
            RequestWithdrawal z = new RequestWithdrawal(identifier, account_name, balance, pin);
            ResponseToWithdrawal q = ResponseToWithdrawal.WithdrawalResponse(z);
            return q;
        }

        [WebMethod]
        public ResponseToTransfer TransferToProperBankAccount(string identifier1, string identifier2, string account1, string account2, decimal balance, string pin)
        {
            RequestTransfer v = new RequestTransfer(identifier1, identifier2, account1, account2, balance, pin);
            ResponseToTransfer y = ResponseToTransfer.ResponseToATransfer(v);
            return y;
        }

        [WebMethod]
        public ResponseToInterbankTransferFromBank2toBank1 TransferToOtherBankAccount(string identifier1, string identifier2, string account1, string account2, decimal balance, string pin)
        {
            IntegrationServices.ActivateException();
            throw new Exception("This feature is yet to be implemented.");
        }

        [WebMethod]
        public ResponseToAllTransactionsOfClient AllTransactionsOfClient(string identifier, string pin)
        {
            RequestAllTransactionsOfClient b = new RequestAllTransactionsOfClient(identifier, pin);
            ResponseToAllTransactionsOfClient y = ResponseToAllTransactionsOfClient.ResponseToAllTransactionsOfTheClient(b);
            return y;
        }

        [WebMethod]
        public ResponseChangedClientState ChangeClientState(string identifier, string password, bool activation)
        {
            RequestChangeClientState rh = new RequestChangeClientState(identifier, password, activation);
            ResponseChangedClientState g = ResponseChangedClientState.SelectionResponse(rh);
            return g;
        }

        [WebMethod]
        public ResponseToLogin Login(string identifier, string password)
        {
            RequestLogToClient lgn = new RequestLogToClient(identifier, password);
            ResponseToLogin ljn = ResponseToLogin.ResponseToClientLogin(lgn);
            return ljn;
        }

        [WebMethod]
        public ResponseVerifyAccountExists AccountExistsQuestion(string identifier, string name)
        {
            RequestAccountExists acc = new RequestAccountExists(identifier, name);
            ResponseVerifyAccountExists ace = ResponseVerifyAccountExists.ResponseToAccountExists(acc);
            return ace;
        }

        [WebMethod]
        public ResponseVerifyClientExists ClientExistsQuestion(string identifier, string name, string last)
        {
            RequestClientExists ca = new RequestClientExists(identifier, name, last);
            ResponseVerifyClientExists ce = ResponseVerifyClientExists.ResponseToClientExists(ca);
            return ce;
        }

        [WebMethod]
        public ResponseGeneralClientBalance GeneralClientBalance(string identifier, string password, bool inactives)
        {
            RequestGeneralClientBalance v = new RequestGeneralClientBalance(identifier, password, inactives);
            ResponseGeneralClientBalance h = ResponseGeneralClientBalance.ResponseToGeneralClient(v);
            return h;
        }
        #endregion

        //For use in exception environments.
        public static void ActivateException()
        {
            SoundPlayer sound = new SoundPlayer
            {
                SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\vox\alert.wav"
            };
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\vox\processing.wav";
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\vox\denied.wav";
            sound.PlaySync();
            Thread.Sleep(150);
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\vox\activate.wav";
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\vox\explosion.wav";
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\buttons\button1.wav";
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\weapons\explode3.wav";
            sound.PlaySync();
            sound.SoundLocation = @"C:\Users\milkc\Music\Half Life\HL SFX\ambience\bigwarning.wav";
            sound.PlayLooping();
            sound.Dispose();
        }
    }
}