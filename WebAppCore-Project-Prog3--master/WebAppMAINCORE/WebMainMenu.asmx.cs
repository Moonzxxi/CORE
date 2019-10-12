using ConsumirDummy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WebAppMAINCORE.DataSetDummyTableAdapters;
using static WebAppMAINCORE.DataSetDummy;

namespace WebAppMAINCORE
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
        public string RegistrarClient(string name, string last, string identifier, string email, string pin, string direction, string password)
        {
            try
            {
                clientTableTableAdapter adapter = new clientTableTableAdapter();
                adapter.InsertClientNuevo(identifier, name, last, password, pin, direction, email);

                return "Creado con exito";
            }
            catch
            {
                return "Datos erroneos";
            }
        }

        [WebMethod]
        public List<Client> General_Devolver_AllClient()
        {
            List<Client> listaClientes = new List<Client>();

            clientTableTableAdapter adapterClient = new clientTableTableAdapter();
            clientTableDataTable client = new clientTableDataTable();
            adapterClient.FillByNone_ALLCLIENT(client);

            foreach (clientTableRow row in client.Rows)
            {
                Client tempClient = new Client();
                tempClient.Name = row.NAME;
                tempClient.Last_Name = row.LAST;
                tempClient.ID_Number = row.IDENTIFIER;
                tempClient.Email = row.EMAIL;
                tempClient.Client_State = row.STATE;
                tempClient.Direction = row.DIRECTION;
                tempClient.Pin = row.PIN;
                tempClient.Password = row.PASSWORD;
                listaClientes.Add(tempClient);
            }

            return listaClientes;
        }

        [WebMethod]
        public Client General_Consulta_Cliente(string cedula)
        {
            //CREAR OBJETO VACIO
            Client objClient = new Client();
            List<Account> listAccounts = new List<Account>();

            //GET DATOS_EN_clientTable
            clientTableTableAdapter adapterClient = new clientTableTableAdapter();
            clientTableDataTable clientDataTable = new clientTableDataTable();
            adapterClient.FillByCedula_Cliente(clientDataTable, cedula);

            //GET DATOS_EN_accountTable
            accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
            accountTableDataTable accountDataTable = new accountTableDataTable();
            adapterAccount.FillByCedula_Account(accountDataTable, cedula);

            //Transferir datos de clientTable a ObjClient
            foreach (clientTableRow row in clientDataTable.Rows)
            {
                Client tempClient = new Client();
                objClient.Name = row.NAME;
                objClient.Last_Name = row.LAST;
                objClient.ID_Number = row.IDENTIFIER;
                objClient.Email = row.EMAIL;
                objClient.Client_State = row.STATE;
                objClient.Direction = row.DIRECTION;
                objClient.Pin = row.PIN;
                objClient.Password = row.PASSWORD;
                objClient.Number_Of_Accounts = row.ACCOUNTS;
            }
            //Transferir datos de clientTable a ObjClient
            foreach (accountTableRow row in accountDataTable.Rows)
            {
                Account tempAccount = new Account();
                tempAccount.Client_ID = row.IDENTIFIER;
                tempAccount.Account_Name = row.ACCOUNT_NAME;
                tempAccount.Account_State = row.ACCOUNT_STATE;
                tempAccount.Account_Type = row.ACCOUNT_TYPE;
                tempAccount.Balance = row.BALANCE;
            }
            objClient.Accounts = listAccounts;
            return objClient;
        }

        [WebMethod]
        public List<Account> General_Devolver_ListCuenta(string Cedula)
        {
            List<Account> listAccount = new List<Account>();
            accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
            accountTableDataTable accountDataTable = new accountTableDataTable();
            adapterAccount.FillByCedula_Account(accountDataTable, Cedula);
            //Transferir datos de clientTable a ObjClient
            foreach (accountTableRow row in accountDataTable.Rows)
            {
                Account tempAccount = new Account();
                tempAccount.Client_ID = row.IDENTIFIER;
                tempAccount.Account_Name = row.ACCOUNT_NAME;
                tempAccount.Account_State = row.ACCOUNT_STATE;
                tempAccount.Account_Type = row.ACCOUNT_TYPE;
                tempAccount.Balance = row.BALANCE;
                listAccount.Add(tempAccount);
            }

            return listAccount;
        }

        [WebMethod]
        public string General_HabilitarAccount(string identifier, string account_name, string account_type)
        {
            try
            {
                accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
                adapterAccount.HabilitarCuentaByCedula("Activa", identifier, account_name,account_type);
                return "Habilitado con exito!";
            }
            catch
            {
                return "Datos Erroneos";
            }
        }

        [WebMethod]
        public string General_DesabilitarAccount(string identifier, string account_name, string account_type)
        {
            try
            {
                accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
                adapterAccount.HabilitarCuentaByCedula("Inactiva", identifier, account_name, account_type);
                return "Habilitado con exito!";
            }
            catch
            {
                return "Datos Erroneos";
            }
        }

        [WebMethod]
        public string General_DepositoCuenta(string cedula, string account_name, decimal balance_to_deposit)
        {
            try
            {
                //Prove Account has Funds
                accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
                accountTableDataTable accountDataTable = new accountTableDataTable();
                adapterAccount.FillByCedula_Account(accountDataTable, cedula);
                //Create Account
                Account tempAccount = new Account();
                //Transfer Data to Local Object
                foreach (accountTableRow row in accountDataTable.Rows)
                {
                    tempAccount.Client_ID = row.IDENTIFIER;
                    tempAccount.Account_Name = row.ACCOUNT_NAME;
                    tempAccount.Account_State = row.ACCOUNT_STATE;
                    tempAccount.Account_Type = row.ACCOUNT_TYPE;
                    tempAccount.Balance = row.BALANCE;
                }
                //Calculo de balances
                decimal balance_actual = 0;
                balance_actual += tempAccount.Balance;
                balance_actual += balance_to_deposit;

                adapterAccount.DepositoCuenta_Normal(balance_actual, cedula, account_name);
                return "El deposito fue exitoso";
            }
            catch
            {
                return "Datos Erroneos";
            }
        }

        [WebMethod]
        public string General_RetiroCuenta(string cedula, string account_name, decimal balance_to_withdraw)
        {
            try
            {
                //Prove Account has Funds
                accountTableTableAdapter adapterAccount = new accountTableTableAdapter();
                accountTableDataTable accountDataTable = new accountTableDataTable();
                adapterAccount.FillByCedula_Account(accountDataTable, cedula);
                //Create Account
                Account tempAccount = new Account();
                //Transfer Data to Local Object
                foreach (accountTableRow row in accountDataTable.Rows)
                {
                    tempAccount.Client_ID = row.IDENTIFIER;
                    tempAccount.Account_Name = row.ACCOUNT_NAME;
                    tempAccount.Account_State = row.ACCOUNT_STATE;
                    tempAccount.Account_Type = row.ACCOUNT_TYPE;
                    tempAccount.Balance = row.BALANCE;
                }

                //Calculo de balances
                decimal Balance_Actual = tempAccount.Balance;

                if (Balance_Actual > balance_to_withdraw)
                {
                    decimal balance_despuesDeRetiro = 0;
                    balance_despuesDeRetiro = Balance_Actual - balance_to_withdraw;
                    adapterAccount.DepositoCuenta_Normal(balance_despuesDeRetiro, cedula, account_name);
                    return "Retiro Exitoso!";
                }
                else
                {
                    return "Fondos insuficientes";
                }
            }
            catch
            {
                return "Datos Erroneos";
            }
        }


    }
}
