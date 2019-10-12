
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseChangedClientState : Response
    {
        #region Atributes and Constructors
        private Client theClient;
        private string accountStates;

        public Client TheClient { get => theClient; set => theClient = value; }

        public string AccountStates { get => accountStates; set => accountStates = value; }

        public ResponseChangedClientState()
        {

        }

        public ResponseChangedClientState(bool success, bool activation, Client _client)
        {
            Success = success;
            Message = null;
            TheClient = _client;

            switch (Success)
            {
                case true:
                    {
                        switch (activation)
                        {
                            case true:
                                {
                                    Message = "Rehabilitación del cliente exitosa.";
                                    TheClient = _client;
                                    AccountStates = $"{_client.Number_Of_Accounts} cuenta(s) activada(s)";
                                }
                                break;

                            case false:
                                {
                                    Message = "Deshabilitación del cliente exitosa.";
                                    TheClient = _client;
                                    AccountStates = $"{_client.Number_Of_Accounts} cuenta(s) archivada(s)";
                                }
                                break;
                        }
                    } break;

                case false:
                    {
                        switch (activation)
                        {
                            case true:
                                {
                                    Message = "El cliente a rehabilitar no existe o ya esta rehabilitado.";
                                    TheClient = null;
                                    AccountStates = (Double.NaN).ToString();
                                }
                                break;

                            case false:
                                {
                                    Message = "El cliente a deshabilitar no existe o ya esta deshabilitado.";
                                    TheClient = null;
                                    AccountStates = (Double.NaN).ToString();
                                }
                                break;
                        }
                        break;
                    }
            }
        }
        #endregion

        public static ResponseChangedClientState SelectionResponse(RequestChangeClientState requestChange)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            ResponseChangedClientState responseChanged = null;

            switch (requestChange.Activation)
            {
                case true:
                    {
                        responseChanged = ResponseChangedClientState.ReponseToRenableClient(requestChange);
                    } break;

                case false:
                    {
                        responseChanged = ResponseChangedClientState.ResponseToDisableClient(requestChange);
                    } break;
            }

            return responseChanged;
        }
        private static ResponseChangedClientState ResponseToDisableClient(RequestChangeClientState disableClient)
        {
            List<clientTable> clients = null;
            ResponseChangedClientState responseChanged = null;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();

            try
            {
                
                clients = entities.clientTables.ToList();
                for (int c = 0; c < clients.Count(); c++)
                {
                    if (clients.ElementAt(c).IDENTIFIER == disableClient.Identifier && 
                        clients.ElementAt(c).PASSWORD == disableClient.Password &&
                        clients.ElementAt(c).STATE == ClientStates.SUSCRITO.ToString())
                    {
                        entities.clientUnsubscribe(disableClient.Identifier);

                        Client client_to_send = new Client(clients.ElementAt(c).NAME, clients.ElementAt(c).LAST, clients.ElementAt(c).IDENTIFIER,
                            ClientStates.INSUSCRITO.ToString(), clients.ElementAt(c).ACCOUNTS, clients.ElementAt(c).EMAIL, clients.ElementAt(c).DIRECTION);
                        responseChanged = new ResponseChangedClientState(true, false, client_to_send);
                        break;
                    }

                    else
                    {
                        responseChanged = new ResponseChangedClientState(false, false, new Client());
                        continue;
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseChangedClientState: Unsusbscribe'.", ex);
                responseChanged = new ResponseChangedClientState(false, false, null);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseChangedClientState: Unsusbscribe' se ha ejecutado exitosamente.");
            return responseChanged;
        }

        private static ResponseChangedClientState ReponseToRenableClient(RequestChangeClientState enableClient)
        {
            ResponseChangedClientState responseChanged = null;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();

            try
            {

                var clients = entities.clientTables.ToList();
                for (int c = 0; c < clients.Count(); c++)
                {
                    if (clients.ElementAt(c).IDENTIFIER == enableClient.Identifier &&
                        clients.ElementAt(c).PASSWORD == enableClient.Password &&
                        clients.ElementAt(c).STATE == ClientStates.INSUSCRITO.ToString())
                    {
                        entities.clientSubscribe(enableClient.Identifier);

                        Client client_to_send = new Client(clients.ElementAt(c).NAME, clients.ElementAt(c).LAST, clients.ElementAt(c).IDENTIFIER,
                            ClientStates.SUSCRITO.ToString(), clients.ElementAt(c).ACCOUNTS, clients.ElementAt(c).EMAIL, clients.ElementAt(c).DIRECTION);
                        responseChanged = new ResponseChangedClientState(true, true, client_to_send);
                        break;
                    }

                    else
                    {

                        responseChanged = new ResponseChangedClientState(false, true, new Client());
                        continue;
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseChangedClientState: Susbscribe'", ex);
                responseChanged = new ResponseChangedClientState(false, true, null);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseChangedClientState: Susbscribe' se ha ejecutado exitosamente.");
            return responseChanged;
        }
    }
}