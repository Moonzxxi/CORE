
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseClientAccounts : Response
    {
        #region Atributes and Constructors
        private List<Account> accounts = new List<Account> { };

        public List<Account> Accounts { get => accounts; set => accounts = value; }


        public ResponseClientAccounts()
        {

        }
        #endregion

        public ResponseClientAccounts(List<Account> accounts, bool success)
        {
            Accounts = accounts;
            Success = success;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        if (Accounts.Count == 0)
                        {
                            Message = "La solicitud se ha procesado exitosamente, pero no se encontraron cuentas.";
                        }

                        else
                        {
                            Message = "La solicitud se ha procesado exitosamente";
                        }
                    }
                    break;

                case false:
                    {
                        Message = "No se pudo procesar la solicitud.";
                    }
                    break;
            }
        }

        public static ResponseClientAccounts ResponseToAllClients(RequestClientAccounts requestAll)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            ResponseClientAccounts responseAll = null;
            List<Account> accounts = new List<Account> { };
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            List<accountTable> AccountTable = entities.accountTables.ToList();

            try
            {
                switch (requestAll.IncInactive)
                {

                    case true:
                        {
                            for (int c = 0; c < AccountTable.Count; c++)
                            {
                                if (AccountTable.ElementAt(c).IDENTIFIER == requestAll.Identifier)
                                {
                                    Account account = new Account(AccountTable.ElementAt(c).ACCOUNT_NAME, AccountTable.ElementAt(c).IDENTIFIER,
                                        AccountTable.ElementAt(c).ACCOUNT_STATE, AccountTable.ElementAt(c).ACCOUNT_TYPE, AccountTable.ElementAt(c).BALANCE); ;
                                    accounts.Add(account);
                                }
                            }

                            responseAll = new ResponseClientAccounts(accounts, true);
                        }
                        break;

                    case false:
                        {
                            for (int c = 0; c < AccountTable.Count; c++)
                            {
                                if (AccountTable.ElementAt(c).IDENTIFIER != requestAll.Identifier ||
                                    (AccountTable.ElementAt(c).ACCOUNT_STATE == AccountStates.INACTIVA.ToString() ||
                                    AccountTable.ElementAt(c).ACCOUNT_STATE == AccountStates.ARCHIVADA.ToString()))
                                    continue;

                                else
                                {
                                    Account account = new Account(AccountTable.ElementAt(c).ACCOUNT_NAME, AccountTable.ElementAt(c).IDENTIFIER,
                                        AccountTable.ElementAt(c).ACCOUNT_STATE, AccountTable.ElementAt(c).ACCOUNT_TYPE, AccountTable.ElementAt(c).BALANCE);
                                    accounts.Add(account);
                                }
                            }

                            responseAll = new ResponseClientAccounts(accounts, true);
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseClientAccounts'.", ex);
                responseAll = new ResponseClientAccounts(null, false);
                return responseAll;
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseClientAccounts' se ha ejecutado exitosamente.");
            return responseAll;
        }
    }
}