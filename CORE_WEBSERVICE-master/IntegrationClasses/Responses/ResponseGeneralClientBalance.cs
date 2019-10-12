using Integration.Code;
using Integration.EntityFramework;
using Integration.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Responses
{
    [Serializable]
    public class ResponseGeneralClientBalance : Response
    {
        private decimal totalClient;

        public decimal TotalClient { get => totalClient; set => totalClient = value; }

        public ResponseGeneralClientBalance()
        {

        }

        public ResponseGeneralClientBalance(bool success)
        {
            Success = success;

            switch (Success)
            {
                case false:
                    {
                        TotalClient = decimal.MinusOne;
                        Message = "Hubo un problema al poder consultar el balance general del cliente.";
                    } break;
            }
        }

        public ResponseGeneralClientBalance(bool success, bool inactive, decimal total_amount)
        {
            Success = success;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        if (inactive == true)
                        {
                            TotalClient = total_amount;
                            Message = $"El ciente, juntando todas las cuentas, tiene un total de ${TotalClient}.";
                        }

                        else
                        {
                            TotalClient = total_amount;
                            Message = $"El cliente, sin considerar las cuentas inactivas o archivadas, tiene un total de ${TotalClient}.";
                        }
                    }
                    break;

                case false:
                    {
                        TotalClient = 0;
                        Message = "No se pudo procesar la solicitud de obtener el balance general.";
                    }
                    break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }

        public static ResponseGeneralClientBalance ResponseToGeneralClient(RequestGeneralClientBalance clientBalance)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            decimal total = 0;
            List<accountTable> accounts;
            ResponseGeneralClientBalance responseGeneral = null;
            accounts = Entities.Integration.accountTables.ToList();

            try
            {
                switch (clientBalance.IncInactive)
                {
                    case true:
                        {
                            for (int c = 0; c<accounts.Count(); c++)
                            {
                                if (clientBalance.Identifier == accounts.ElementAt(c).IDENTIFIER)
                                {
                                    total = total + accounts.ElementAt(c).BALANCE;
                                }

                                else
                                {
                                    continue;
                                }
                            }
                        } break;

                    case false:
                        {
                            for (int c = 0; c < accounts.Count(); c++)
                            {
                                if (clientBalance.Identifier == accounts.ElementAt(c).IDENTIFIER &&
                                    accounts.ElementAt(c).ACCOUNT_STATE == AccountStates.ACTIVA.ToString())
                                {
                                    total = total + accounts.ElementAt(c).BALANCE;
                                }

                                else
                                {
                                    continue;
                                }
                            }
                        } break;
                }

                responseGeneral = new ResponseGeneralClientBalance(true, clientBalance.IncInactive, total); 
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseGeneralClientBalance'", ex);
                responseGeneral = new ResponseGeneralClientBalance(false);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseGeneralClientBalance' se ha ejecutado exitosamente.");
            return responseGeneral;
        }
    }
}