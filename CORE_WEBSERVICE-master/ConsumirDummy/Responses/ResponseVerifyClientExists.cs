
using System;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseVerifyClientExists : Response
    {
        #region Atributes and Constructors
        private Client client;

        public Client Client { get => client; set => client = value; }

        public ResponseVerifyClientExists()
        {

        }

        public ResponseVerifyClientExists(bool success, Client client_that_exists)
        {
            Success = success;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        Message = "Se ha encontrado un cliente con los parametros insertados:";
                        Client = client_that_exists;
                    } break;

                case false:
                    {
                        Message = "La solicitud no se pudo procesar.";
                        Client = null;
                    } break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }
        #endregion

        public static ResponseVerifyClientExists ResponseToClientExists(RequestClientExists requestClientExists)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            Client client = new Client(), client_to_send = null;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            var client_table = entities.clientTables.ToList();
            ResponseVerifyClientExists responseClient = null;

            try
            {
                foreach (var element in client_table)
                {
                    if (requestClientExists.Identifier == element.IDENTIFIER && requestClientExists.First_Name == element.NAME &&
                        requestClientExists.Last_Name == element.LAST)
                    {
                        client_to_send = new Client(element.NAME, element.LAST, element.IDENTIFIER, element.STATE,
                            element.ACCOUNTS, element.EMAIL, element.DIRECTION);
                        break;
                    }

                    else
                    {
                        continue;
                    }
                }

                responseClient = new ResponseVerifyClientExists(true, client_to_send);
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseVerifyClientExists'.", ex);
                responseClient = new ResponseVerifyClientExists(false, null);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseVerifyClientExists' se ha ejecutado exitosamente.");
            return responseClient;
        }
    }
}