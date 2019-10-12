
using System;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseVerifyAccountExists : Response
    {
        #region Atributes and Constructors
        private Account account;

        public Account Account { get => account; set => account = value; }

        public ResponseVerifyAccountExists()
        {

        }

        public ResponseVerifyAccountExists(bool success, Account account_that_exists)
        {
            Message = null;
            Success = success;

            switch (Success)
            {
                case true:
                    {
                        if (account_that_exists != null)
                        {
                            Message = "Se ha encontrado una cuenta con los parametros insertados:";
                            Account = account_that_exists;
                        }

                        else
                        {
                            Message = "Aunque la solicitud se proceso existosamente, no se pudo encontrar una cuenta que coincida con los parametros insertados.";
                        }
                    }
                    break;

                case false:
                    {
                        Message = "La solicitud no se pudo procesar.";
                        Account = new Account();
                    }
                    break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }
        #endregion

        public static ResponseVerifyAccountExists ResponseToAccountExists(RequestAccountExists requestAccountExists)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            Account account = new Account(), account_to_send = null;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            var account_table = entities.accountTables.ToList();
            ResponseVerifyAccountExists responseAccount = null;

            try
            {
                foreach (var element in account_table)
                {
                    if (requestAccountExists.Identifier == element.IDENTIFIER && 
                        requestAccountExists.Account_Name == element.ACCOUNT_NAME)
                    {
                        account_to_send = new Account(element.ACCOUNT_NAME, element.IDENTIFIER, element.ACCOUNT_STATE,
                            element.ACCOUNT_TYPE, element.BALANCE);
                        break;
                    }

                    else
                    {
                        continue;
                    }
                }

                responseAccount = new ResponseVerifyAccountExists(true, account_to_send);
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseVerifyAccountExists'", ex);
                responseAccount = new ResponseVerifyAccountExists(false, null);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseVerifyAccountExists' se ha ejecutado exitosamente.");
            return responseAccount;
        }
    }
}