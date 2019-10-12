using Integration.Code;
using Integration.Requests;
using System;
using System.Linq;

namespace Integration.Responses
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
            ResponseToLogin response = null;
            bool verified = false;

            try
            {
                var clients = Entities.Integration.clientTables.ToList();

                foreach (var element in clients)
                {
                    if (requestLog.Identifier == element.IDENTIFIER && 
                        requestLog.Password == element.PASSWORD)
                    {
                        Entities.Integration.updateLogin(requestLog.Identifier, requestLog.Password);
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
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseToLogin' se ha procesado exitosamente.");
            return response;
        }
    }
}