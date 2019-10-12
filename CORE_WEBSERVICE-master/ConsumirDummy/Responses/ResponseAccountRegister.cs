
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumirDummy
{
    public class ResponseAccountRegister : Response
    {
        #region Atributes and Constructors
        public ResponseAccountRegister()
        {

        }

        public ResponseAccountRegister(bool success, string identifier)
        {
            Success = success;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        Message = $"La cuenta del cliente asociado al identificador {identifier} se ha registrado exitosamente.";
                    }
                    break;

                case false:
                    {
                        Message = "No se pudo registrar la cuenta del cliente asociada.";
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

        public static ResponseAccountRegister ResponseToAccountRegister(RequestAccountRegister accountToRegister)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            ResponseAccountRegister responseAccount;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();

            try
            {
                switch (accountToRegister.Account_Type)
                {
                    case "EMPRESARIAL": accountToRegister.Account_Type = AccountTypes.EMPRESARIAL.ToString(); break;
                    case "AHORRO": accountToRegister.Account_Type = AccountTypes.AHORRO.ToString(); break;
                    case "CORRIENTE": accountToRegister.Account_Type = AccountTypes.CORRIENTE.ToString(); break;
                    default: accountToRegister.Account_Type = AccountTypes.OTRO.ToString(); break;
                }

                entities.accountRegister(accountToRegister.Identifier, 
                    accountToRegister.Account_Name, accountToRegister.Account_Type);

                responseAccount = new ResponseAccountRegister(true, accountToRegister.Identifier);
                Log.Info("El 'ResponseAccountRegister' se ha ejecutado exitosamente.");
            }


            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseAccountRegister'.", ex);
                responseAccount = new ResponseAccountRegister(false, accountToRegister.Identifier);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            return responseAccount;
        }
    }
}