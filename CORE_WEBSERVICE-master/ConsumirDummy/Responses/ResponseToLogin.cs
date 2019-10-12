
using System;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseToLogin : Response
    {
        #region Atributes and Constructors
        public ResponseToLogin()
        {

        }

        public ResponseToLogin(bool verified)
        {
            Success = verified;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        Message = "Datos correctos.";
                    }
                    break;

                case false:
                    {
                        Message = "Datos incorrectos.";
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

        public static ResponseToLogin ResponseToClientLogin(RequestLogToClient requestLog)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            ResponseToLogin response = null;
            bool verified = false;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            try
            {
                var clients = entities.clientTables.ToList();

                foreach (var element in clients)
                {
                    if (requestLog.Identifier == element.IDENTIFIER && 
                        requestLog.Password == element.PASSWORD)
                    {
                        entities.updateLogin(requestLog.Identifier, requestLog.Password);
                        verified = true;
                        break;
                    }

                    else
                    {
                        verified = false;
                        continue;
                    }
                }

                response = new ResponseToLogin(verified);
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToLogin'", ex);
                response = new ResponseToLogin(false);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseToLogin' se ha procesado exitosamente.");
            return response;
        }
    }
}