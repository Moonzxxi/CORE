
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseToAllClients : Response
    {
        #region Atributes and Constructors
        private List<Client> allClients = new List<Client> { };
        private int count;

        public List<Client> AllClients { get => allClients; set => allClients = value; }

        public int Count { get => count; set => count = value; }

        public ResponseToAllClients()
        {

        }

        public ResponseToAllClients(List<Client> clients)
        {
            Count = clients.Count;
            Message = null;

            switch (Count)
            {
                case 0:
                    {
                        Success = false;
                        Message = "No hay ningun cliente registrado.";
                        AllClients = null;
                    } break;

                default:
                    {
                        Success = true;
                        Message = "Se han encontrado varios clientes.";
                        AllClients = clients;
                    } break;
            }
        }
        #endregion

        public static ResponseToAllClients ResponseToAllTheClients(RequestAllClients requestAll)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            ResponseToAllClients responseAll = null;
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            var clients_from_clienttable = entities.clientTables.ToList();
            List<Client> allClients = new List<Client> { };
            Client client = new Client();

            try
            {
                switch (requestAll.IncUnsubs)
                {
                    case true:
                        {
                            foreach (var element in clients_from_clienttable)
                            {
                                client.Name = element.NAME;
                                client.Last_Name = element.LAST;
                                client.ID_Number = element.IDENTIFIER;
                                client.Number_Of_Accounts = int.Parse(element.ACCOUNTS.ToString());
                                client.Client_State = element.STATE; 
                                client.Direction = element.DIRECTION;
                                client.Email = element.EMAIL;

                                Client client_to_send = new Client(client.Name, client.Last_Name, client.ID_Number, client.Client_State,
                                    client.Number_Of_Accounts, client.Email, client.Direction);
                                allClients.Add(client_to_send);
                            }

                            responseAll = new ResponseToAllClients(allClients);
                        }
                        break;

                    case false:
                        {
                            foreach (var element in clients_from_clienttable)
                            {
                                if (element.STATE == ClientStates.INSUSCRITO.ToString())
                                {
                                    continue;
                                }

                                else
                                {
                                    client.Name = element.NAME;
                                    client.Last_Name = element.LAST;
                                    client.ID_Number = element.IDENTIFIER;
                                    client.Number_Of_Accounts = int.Parse(element.ACCOUNTS.ToString());
                                    client.Client_State = element.STATE;
                                    client.Direction = element.DIRECTION;
                                    client.Email = element.DIRECTION;

                                    Client client_to_send = new Client(client.Name, client.Last_Name, client.ID_Number, client.Client_State,
                                        client.Number_Of_Accounts, client.Email, client.Direction);
                                    allClients.Add(client_to_send);
                                }
                            }

                            responseAll = new ResponseToAllClients(allClients);
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un erro al proocesar 'ResponseToAllClients'.", ex);
                responseAll = new ResponseToAllClients(allClients);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseToAllClients' se ha ejecutado exitosamente.");
            return responseAll;
        }
    }
}