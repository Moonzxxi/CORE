using Integration.Code;
using Integration.EntityFramework;
using Integration.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Responses
{
    [Serializable]
    public class ResponseToThirdParties : Response
    {
        #region Atributes and Constructors
        private string identifier;
        private decimal balance;
        private string name;
        private string third_party;
        private static string description;

        public string Identifier { get => identifier; set => identifier = value; }

        public decimal Balance { get => balance; set => balance = value; }

        public string Name { get => name; set => name = value; }

        public string Third_party { get => third_party; set => third_party = value; }

        public ResponseToThirdParties()
        {

        }

        public ResponseToThirdParties(bool transfer_complete, string identifier, decimal balance, string name, string third_party)
        {
            Success = transfer_complete;
            Identifier = identifier;
            Balance = balance;
            Name = name;
            Third_party = third_party;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        Message = $"La tranferencia al tercero {Third_party} con valor ${Balance} por parte del usuario con cedula {Identifier}, usando su cuenta {Name}, fue exitosa";
                    } break;

                case false:
                    {
                        Message = $"La tranferencia a terceros no se pudo realizar.";
                    } break;
            }
        }

        public ResponseToThirdParties(char underflow)
        {
            switch (underflow)
            {
                case 'Y': case 'y':
                    {
                        Success = false;
                        Message = "No se puede realizar transacciones porque la cuenta presenta sobregiro.";
                    }
                    break;

                case 'N': case 'n':
                    {
                        Success = false;
                        Message = "No se puede realizar transacciones porque el balance solicitado es mayor al balance presente en la cuenta.";
                    }
                    break;
            }
        }

        public ResponseToThirdParties(bool transfer_complete)
        {
            Success = transfer_complete;

            switch (Success)
            {
                case false:
                    {
                        Message = "No se pudo procesar la solicitud de transferencia a terceros.";
                    }
                    break; 
            }
        }

        public ResponseToThirdParties(bool transfer_complete, decimal balance)
        {
            Success = transfer_complete;
            Message = null;

            switch (Success)
            {
                case false:
                    {
                        Message = $"No se pudo procesar la solicitud de transferencia porque la cuenta no tiene balance: ${balance}.";
                    }
                    break;
            }
        }
        #endregion

        public static ResponseToThirdParties ResponseToThirdPartyTansfer(RequestThirdPartyTransfer requestThirdPartyTransfer)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            if (requestThirdPartyTransfer.Pin.Length > 8) requestThirdPartyTransfer.Pin = requestThirdPartyTransfer.Pin.Substring(0, 7);

            List<accountTable> accounts = null;
            ResponseToThirdParties responseThird = null;
            decimal balanceVerifier = 0;

            accounts = Entities.Integration.accountTables.ToList();
            bool correctPin = Response.IsThePinCorrect(new Request(requestThirdPartyTransfer.Identifier, requestThirdPartyTransfer.Pin));

            try
            {
                switch (correctPin)
                {
                    case true:
                        {
                            for (int c = 0; c < accounts.Count(); c++)
                            {
                                if (accounts.ElementAt(c).ACCOUNT_NAME == requestThirdPartyTransfer.Account_Name &&
                                    accounts.ElementAt(c).IDENTIFIER == requestThirdPartyTransfer.Identifier)
                                {
                                    if (accounts.ElementAt(c).ACCOUNT_STATE == AccountStates.INACTIVA.ToString() ||
                                        accounts.ElementAt(c).ACCOUNT_STATE == AccountStates.ARCHIVADA.ToString())
                                    {
                                        responseThird = new ResponseToThirdParties(false);
                                        break;
                                    }

                                    else
                                    {
                                        balanceVerifier = accounts.ElementAt(c).BALANCE;

                                        if (balanceVerifier < 0)
                                        {
                                            responseThird = new ResponseToThirdParties('Y');
                                            break;
                                        }

                                        if (balanceVerifier == 0)
                                        {
                                            responseThird = new ResponseToThirdParties(false, balanceVerifier);
                                            break;
                                        }

                                        if (balanceVerifier < requestThirdPartyTransfer.Balance_To_Thirds)
                                        {
                                            responseThird = new ResponseToThirdParties('N');
                                            break;
                                        }

                                        description = TransactionTypes.ThirPartyTransfer(requestThirdPartyTransfer.Identifier, requestThirdPartyTransfer.Account_Name,
                                            requestThirdPartyTransfer.ThirdPartyName, requestThirdPartyTransfer.Balance_To_Thirds);

                                        Entities.Integration.thirdpartyTransfer(requestThirdPartyTransfer.Identifier, requestThirdPartyTransfer.Balance_To_Thirds,
                                            requestThirdPartyTransfer.Account_Name, requestThirdPartyTransfer.ThirdPartyName, description);

                                        responseThird = new ResponseToThirdParties(true, requestThirdPartyTransfer.Identifier, requestThirdPartyTransfer.Balance_To_Thirds,
                                            requestThirdPartyTransfer.Account_Name, requestThirdPartyTransfer.ThirdPartyName);
                                        break;
                                    }
                                }
                            }
                        }            
                        break;

                    case false:
                        {
                            responseThird = new ResponseToThirdParties(false, requestThirdPartyTransfer.Identifier, requestThirdPartyTransfer.Balance_To_Thirds,
                                            requestThirdPartyTransfer.Account_Name, requestThirdPartyTransfer.ThirdPartyName);
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToThirdParties'.", ex);
                responseThird = new ResponseToThirdParties(false, decimal.MinusOne);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseToThirdParties' se ha ejecutado exitosamente.");
            return responseThird;
        }
    }
}