using Integration.Code;
using Integration.Requests;
using System;
using System.Linq;

namespace Integration.Responses
{
    [Serializable]
    public class ResponseToDeposit : Response
    {
        #region Atributes and Constructors
        private string identifier;
        private string name;
        private decimal value;
        private static string description;

        public string Identifier { get => identifier; set => identifier = value; }

        public string Name { get => name; set => name = value; }

        public decimal Value { get => value; set => this.value = value; }

        public ResponseToDeposit()
        {

        }

        public ResponseToDeposit(bool deposit_event, string identifier, string name, decimal value)
        {
            Success = deposit_event;
            Message = null;
            Value = value;
            Identifier = identifier;
            Name = name;

            switch (Success)
            {
                case true:
                    {
                        Message = $"El deposito de ${Value} fue realizado exitosamente a la cuenta '{Name}' por parte del cliente con cédula {Identifier}.";
                    }
                    break;

                case false:
                    {
                        Message = $"No se pudo realizar el deposito porque la cuenta del cliente con cédula {Identifier} no existe.";
                    }
                    break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }

        public ResponseToDeposit(bool deposit_event)
        {
            Success = deposit_event;
            Message = null;

            switch (Success)
            {
                case false:
                    {
                        Message = "No se pudo procesar la solicitud de deposito porque la cuenta esta inactiva, archivada, o no existe.";
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

        public static ResponseToDeposit DepositResponse(RequestDeposit requestToDeposit)
        {
            if (requestToDeposit.Pin.Length > 8) requestToDeposit.Pin = requestToDeposit.Pin.Substring(0, 7);

            ResponseToDeposit depositResponse = null;
            var account_table = Entities.Integration.accountTables.ToList();

            try
            {
                if (requestToDeposit.Balance_To_Deposit > decimal.MaxValue - 1 || requestToDeposit.Balance_To_Deposit < 0)
                {
                    depositResponse = new ResponseToDeposit(false);
                }

                else
                {
                    bool correctPin = Response.IsThePinCorrect(new Request(requestToDeposit.Identifier, requestToDeposit.Pin));

                    switch (correctPin)
                    {
                        case true:
                            {
                                for (int c = 0; c < account_table.Count() ; c++)
                                {
                                    if (account_table.ElementAt(c).IDENTIFIER == requestToDeposit.Identifier && 
                                        account_table.ElementAt(c).ACCOUNT_NAME == requestToDeposit.Account_Name)
                                    {
                                        if (account_table.ElementAt(c).ACCOUNT_STATE == AccountStates.INACTIVA.ToString() ||
                                            account_table.ElementAt(c).ACCOUNT_STATE == AccountStates.ARCHIVADA.ToString())
                                        {
                                            return depositResponse = new ResponseToDeposit(false);
                                        }

                                        description = TransactionTypes.Deposit(requestToDeposit.Identifier, requestToDeposit.Account_Name, requestToDeposit.Balance_To_Deposit);

                                        Entities.Integration.bankDeposit(requestToDeposit.Balance_To_Deposit, requestToDeposit.Identifier,
                                            requestToDeposit.Account_Name, description);

                                        depositResponse = new ResponseToDeposit(true, requestToDeposit.Identifier, requestToDeposit.Account_Name, requestToDeposit.Balance_To_Deposit);
                                        break;
                                    }
                                }
                            }
                            break;

                        case false:
                            {
                                depositResponse = new ResponseToDeposit(false);
                            }
                            break;
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToDeposit'.", ex);
                depositResponse = new ResponseToDeposit(false, null, null, decimal.MinusOne);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseToDeposit' se ha ejecutado exitosamente.");
            return depositResponse;
        }
    }
}