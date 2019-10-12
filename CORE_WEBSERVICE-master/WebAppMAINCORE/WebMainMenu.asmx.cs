
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;


namespace DBCORE
{
    /// <summary>
    /// Summary description for WebMainMenu
    /// </summary>
    [WebService(Namespace = "http://intec.edu.do.org")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebMainMenu : System.Web.Services.WebService
    {
        //METODOS GENERALES
        
          

        [WebMethod]
        public ResponseAccountRegister AccountRegister(RequestAccountRegister requestAccountRegister)
            {
                ResponseAccountRegister objAccountRegister = new ResponseAccountRegister();
                string identifier, name, type;
                identifier = requestAccountRegister.Identifier;
                name = requestAccountRegister.Account_Name;
                type = requestAccountRegister.Account_Type;

                try
                {
                    entities.accountRegister(identifier, name, type);
                    objAccountRegister.Success = true;
                    objAccountRegister.Message = "La cuenta fue registrada con exito!";
                }
                catch
                {
                    objAccountRegister.Success = false;
                    objAccountRegister.Message = "Hubo un error en con el proceso de registro";
                }

                return objAccountRegister;
            }

        
        [WebMethod]
        public ResponseChangedAccountState ChangedAccountState(RequestChangeAccountState requestChangeAccountState)
        {
            ResponseChangedAccountState objChangedAccountState = new ResponseChangedAccountState();
            string identifier, name, estadoAccount;
            identifier = requestChangeAccountState.Identifier;
            name = requestChangeAccountState.Account_Name;
            estadoAccount = entities.getAccountState(identifier, name).ToString();

            if (estadoAccount.ToUpper() == "ACTIVA")
            {
                try
                {
                    entities.accountDeactivate(identifier, name);
                    entities.SaveChanges();
                    objChangedAccountState.Success = true;
                    objChangedAccountState.Message = "Se ha cambiado el estado de la cuenta con exito!";
                    objChangedAccountState.AccountState = entities.getAccountState(identifier, name).ToString();
                }
                catch
                {
                    objChangedAccountState.Success = false;
                    objChangedAccountState.Message = "Hubo error en el cambio del estado de la cuenta!";
                    objChangedAccountState.AccountState = entities.getAccountState(identifier, name).ToString();
                }
            }
            else
            {
                try
                {
                    entities.accountReactivate(identifier, name);
                    entities.SaveChanges();
                    objChangedAccountState.Success = true;
                    objChangedAccountState.Message = "Se ha cambiado el estado de la cuenta con exito!";
                    objChangedAccountState.AccountState = entities.getAccountState(identifier, name).ToString();
                }
                catch
                {
                    objChangedAccountState.Success = false;
                    objChangedAccountState.Message = "Hubo error en el cambio del estado de la cuenta!";
                    objChangedAccountState.AccountState = entities.getAccountState(identifier, name).ToString();
                }
            }
            
            return objChangedAccountState;
        }

        [WebMethod]
        public ResponseChangedClientState ChangedClientState(RequestChangeClientState requestChangeClientState)
        {
            ResponseChangedClientState objChangedClientState = new ResponseChangedClientState();
            string identifier;
            identifier = requestChangeClientState.Identifier;


            return objChangedClientState;
        }

        
        [WebMethod]
        public ResponseClientAccounts ClientAccounts(RequestClientAccounts requestClientAccounts)
        {
            ResponseClientAccounts objClientAccounts = new ResponseClientAccounts();
            string identifier;
            identifier = requestClientAccounts.Identifier;

            try
            {
                var accountTbl = entities.accountTables.ToList();
                foreach (accountTable item in accountTbl)
                {
                    if (item.IDENTIFIER == identifier)
                    {
                        Account tempAccount = new Account();
                        tempAccount.Account_Name = item.ACCOUNT_NAME;
                        tempAccount.Account_State = item.ACCOUNT_STATE;
                        tempAccount.Account_Type = item.ACCOUNT_TYPE;
                        tempAccount.Balance = item.BALANCE;

                        objClientAccounts.Accounts.Add(tempAccount);
                    }
                    
                }
                objClientAccounts.Success = true;
                objClientAccounts.Message = "Se han obtenido todas las cuentas del cliente!";

            }
            catch
            {
                objClientAccounts.Success = false;
                objClientAccounts.Message = "Este cliente no tiene cuentas";
            }

            return objClientAccounts;
        }

        [WebMethod]
        public ResponseClientRegister ClientRegister(RequestClientRegister requestClientRegister)
        {
            ResponseClientRegister objClientRegister = new ResponseClientRegister();
            string identifier, name, last, password, pin, direction, email;
            identifier = requestClientRegister.Identifier;
            name = requestClientRegister.Name_Regs;
            last = requestClientRegister.Lastname_Regs;
            password = requestClientRegister.Password_Regs;
            pin = requestClientRegister.Pin_Regs;
            direction = requestClientRegister.Direction_Regs;
            email = requestClientRegister.Email_Regs;

            try
            {
                entities.clientRegister(identifier, name, last, password, pin, direction, email);
                objClientRegister.Success = true;
                objClientRegister.Message = "El cliente fue registrado con exito!";
            }
            catch
            {
                objClientRegister.Success = false;
                objClientRegister.Message = "Datos erroneos";
            }

            return objClientRegister;
        }

        [WebMethod]
        public ResponseGeneralClientBalance GeneralClientBalance (RequestGeneralClientBalance requestGeneralClientBalance)
        {
            ResponseGeneralClientBalance objGeneralClientBalance = new ResponseGeneralClientBalance();
            string identifier;
            decimal balance = 0;

            identifier = requestGeneralClientBalance.Identifier;
            List<Account> clientAcc = new List<Account>();

            try
            {
                var accountTbl = entities.accountTables.ToList();
                foreach (accountTable item in accountTbl)
                {
                    if (item.IDENTIFIER == identifier)
                    {
                        Account tempAccount = new Account();
                        tempAccount.Account_Name = item.ACCOUNT_NAME;
                        tempAccount.Account_State = item.ACCOUNT_STATE;
                        tempAccount.Account_Type = item.ACCOUNT_TYPE;
                        tempAccount.Balance = item.BALANCE;

                        clientAcc.Add(tempAccount);
                    }
                }
                
                foreach (Account element in clientAcc)
                {
                    balance += element.Balance;
                }

                objGeneralClientBalance.Success = true;
                objGeneralClientBalance.Message = "Se ha encontrado el balance de todas las cuentas";

            }
            catch
            {
                objGeneralClientBalance.Success = false;
                objGeneralClientBalance.Message = "Error, este cliente no tiene cuentas";
            }
            finally
            {
                objGeneralClientBalance.TotalClient = balance;
                
            }


            return objGeneralClientBalance;
        }

        [WebMethod]
        public ResponseToAllClients ToAllClients (RequestAllClients requestAllClients)
        {
            ResponseToAllClients objToAllClients = new ResponseToAllClients();
            
            try
            {
                var clienteTbl = entities.clientTables.ToList();
                foreach (clientTable item in clienteTbl)
                {
                    Client tempClient = new Client();
                    tempClient.Client_State = item.STATE;
                    tempClient.Direction = item.DIRECTION;
                    tempClient.Email = item.EMAIL;
                    tempClient.ID_Number = item.IDENTIFIER;
                    tempClient.Last_Name = item.LAST;
                    tempClient.Name = item.NAME;
                    tempClient.Password = item.PASSWORD;
                    tempClient.Pin = item.PIN;

                    var accountTbl = entities.accountTables.ToList();
                    foreach (accountTable element in accountTbl)
                    {
                        if (item.IDENTIFIER == tempClient.ID_Number)
                        {
                            Account tempAccount = new Account();
                            tempAccount.Account_Name = element.ACCOUNT_NAME;
                            tempAccount.Account_State = element.ACCOUNT_STATE;
                            tempAccount.Account_Type = element.ACCOUNT_TYPE;
                            tempAccount.Balance = element.BALANCE;

                            tempClient.Accounts.Add(tempAccount);
                        }
                    }

                    tempClient.Number_Of_Accounts = tempClient.Accounts.Count;
                    objToAllClients.Success = true;
                    objToAllClients.Message = "Se ha encontrado todos los clientes!";

                }
            }
            catch
            {
                objToAllClients.Success = false;
                objToAllClients.Message = "Hubo un error en la busqueda de clientes, intentelo mas tarde";
            }

            return objToAllClients;
        }

        [WebMethod]
        public ResponseToAllTransactionsOfClient ToAllTransactionsOfClient (RequestAllTransactionsOfClient requestAllTransactionsOfClient)
        {
            ResponseToAllTransactionsOfClient objToAllTransactionsOfClient = new ResponseToAllTransactionsOfClient();
            string identifier;
            identifier = requestAllTransactionsOfClient.Identifier;

            var transactionTbl = entities.transactionTables.ToList();
            try
            {
                foreach (transactionTable transaction in transactionTbl)
                {
                    Transaction tempTransaction = new Transaction();
                    tempTransaction.Account_affected = transaction.ACCOUNT_AFFECTED;
                    tempTransaction.Account_root = transaction.ACCOUNT_ROOT;
                    tempTransaction.Balance = transaction.BALANCE;
                    tempTransaction.Description1 = transaction.DESCRIPTION;
                    tempTransaction.Identifier_affected = transaction.IDENTIFIER_AFFECTED;
                    tempTransaction.Identifier_root = transaction.IDENTIFIER_ROOT;
                    tempTransaction.Transaction_Date = transaction.TRANSDATE;

                    objToAllTransactionsOfClient.Transactions.Add(tempTransaction);
                }

                objToAllTransactionsOfClient.Success = true;
                objToAllTransactionsOfClient.Message = "Se ha encontrado todas las transacciones!";
            }
            catch
            {

                objToAllTransactionsOfClient.Success = false;
                objToAllTransactionsOfClient.Message = "No se ha encontrado las transacciones, verifique sus datos";
            }

            objToAllTransactionsOfClient.Trans_Count = objToAllTransactionsOfClient.Transactions.Count;
            return objToAllTransactionsOfClient;
        }

        [WebMethod]
        public ResponseToDeposit toDeposit(RequestDeposit requestDeposit)
        {
            ResponseToDeposit objToDeposit = new ResponseToDeposit();
            string identifier, name, description;
            decimal deposito;
            identifier = requestDeposit.Identifier;
            name = requestDeposit.Account_Name;
            deposito = requestDeposit.Balance_To_Deposit;
            description = "";
            try
            {
                entities.bankDeposit(deposito, identifier, name, description);
                objToDeposit.Success = true;
                objToDeposit.Message = "Se ha depositado con exito!";
            }
            catch
            {
                objToDeposit.Success = false;
                objToDeposit.Message = "Error con el proceso de deposito, verifique sus datos";
            }
            
            return objToDeposit;
        }

        //Incompleto, no tiene funcion
        [WebMethod]
        public ResponseToInterbankTransferFromBank2toBank1 ToInterbankTransferFromBank2ToBank1 (RequestInterbankTransferFromBank2toBank1 requestInterbankTransferFromBank2ToBank1)
        {
            ResponseToInterbankTransferFromBank2toBank1 objToInterbackTransferFromBank2ToBank1 = new ResponseToInterbankTransferFromBank2toBank1();

            entities.interbankTransfer();

            return objToInterbackTransferFromBank2ToBank1;
        }

        [WebMethod]
        public ResponseToLogin ToLogin (RequestLogToClient requestLogToClient)
        {
            ResponseToLogin objToLogin = new ResponseToLogin();
            string identifier, password;
            identifier = requestLogToClient.Identifier;
            password = requestLogToClient.Password;
            try
            {
                entities.updateLogin(identifier, password);
                objToLogin.Success = true;
                objToLogin.Message = "El login fue procesado con exito!";
            }
            catch
            {
                objToLogin.Success = false;
                objToLogin.Message = "Datos erroneos, por favor revise sus datos.....";
            }

            return objToLogin;

        }

        [WebMethod]
        public ResponseToThirdParties ToThirdParties (RequestThirdPartyTransfer requestThirdPartyTransfer)
        {
            ResponseToThirdParties objToThirdParties = new ResponseToThirdParties();
            string identifier, name, thirdParty, description;
            decimal transferAmount;
            identifier = requestThirdPartyTransfer.Identifier;
            name = requestThirdPartyTransfer.Account_Name;
            thirdParty = requestThirdPartyTransfer.ThirdPartyName;
            description = "";
            transferAmount = requestThirdPartyTransfer.Balance_To_Thirds;

            try
            {
                entities.thirdpartyTransfer(identifier, transferAmount, name, thirdParty, description);
                objToThirdParties.Success = true;
                objToThirdParties.Message = "La transferencia se hizo con exito!";
            }
            catch
            {
                objToThirdParties.Success = false;
                objToThirdParties.Message = "Hubo error con la transferencia, intentelo mas tarde";
            }
            entities.thirdpartyTransfer(identifier, transferAmount, name, thirdParty, description);
            return objToThirdParties;
        }

        [WebMethod]
        public ResponseToTransfer ToTransfer (RequestTransfer requestTransfer)
        {
            ResponseToTransfer objToTransfer = new ResponseToTransfer();
            decimal transfer_amount;
            //1: ORIGEN (AL QUE SE LE RESTA) 2: DESTINO(EL QUE RECIBE)
            string identification1, name1, identification2, name2, description;
            identification1 = requestTransfer.Identifier_Root;
            identification2 = requestTransfer.Identifier_To_Affect;
            name1 = requestTransfer.Account_Root;
            name2 = requestTransfer.Account_To_Affect;
            description = "";
            transfer_amount = requestTransfer.Balance_To_Transfer;
            try
            {
                entities.bankTransfer(transfer_amount, identification1, name1, identification2, name2, description);
                objToTransfer.Success = true;
                objToTransfer.Message = "La transferencia se hizo con exito!";

            }
            catch
            {
                objToTransfer.Success = false;
                objToTransfer.Message = "La transferencia no se hizo con exito, intentelo mas tarde";
            }
            
            return objToTransfer;
        }

        [WebMethod]
        public ResponseToWithdrawal ToWithdrawal (RequestWithdrawal requestWithdrawal)
        {
            ResponseToWithdrawal objToWithdrawal = new ResponseToWithdrawal();
            decimal withdraw_amount;
            string identification, name, description;
            withdraw_amount = requestWithdrawal.Balance_To_Withdraw;
            identification = requestWithdrawal.Identifier;
            name = requestWithdrawal.Account_Name;
            description = "";

            try
            {
                entities.bankWithdraw(withdraw_amount, identification, name, description);
                objToWithdrawal.Success = true;
                objToWithdrawal.Message = "Se ha retirado dinero con exito!";
            }
            catch
            {
                objToWithdrawal.Success = false;
                objToWithdrawal.Message = "Error, revise sus datos";
            }


            return objToWithdrawal;
        }

        [WebMethod]
        public ResponseVerifyAccountExists VerifyAccountExists (RequestAccountExists requestAccountExists)
        {
            ResponseVerifyAccountExists objVerifyAccountExists = new ResponseVerifyAccountExists();

            string identification, name;
            identification = requestAccountExists.Identifier;
            name = requestAccountExists.Account_Name;

            try
            {
                accountTable accountTable = new accountTable();
                accountTable.IDENTIFIER = identification;
                accountTable.ACCOUNT_NAME = name;
                bool exist = entities.accountTables.Contains(accountTable);
                if(exist == true)
                {
                    objVerifyAccountExists.Success = true;
                    objVerifyAccountExists.Message = "La cuenta si existe!";
                }
            }
            catch
            {
                objVerifyAccountExists.Success = false;
                objVerifyAccountExists.Message = "La cuenta no existe";
            }

            return objVerifyAccountExists;
        }

        [WebMethod]
        public ResponseVerifyClientExists VerifyClientExists (RequestClientExists requestClientExists)
        {
            ResponseVerifyClientExists objVerifyClientExists = new ResponseVerifyClientExists();

            string identification, name, last;
            identification = requestClientExists.Identifier;
            name = requestClientExists.First_Name;
            last = requestClientExists.Last_Name;
            
           
            try
            {
                clientTable clientTable = new clientTable();
                clientTable.IDENTIFIER = identification;
                clientTable.NAME = name;
                clientTable.LAST = last;

                bool exist = entities.clientTables.Contains(clientTable);

                if (exist == true)
                {
                    objVerifyClientExists.Success = true;
                    objVerifyClientExists.Message = "El cliente si existe!";
                }
            }
            catch
            {
                objVerifyClientExists.Success = false;
                objVerifyClientExists.Message = "El cliente no existe :( ";
            }

            return objVerifyClientExists;
        }
    }
}
