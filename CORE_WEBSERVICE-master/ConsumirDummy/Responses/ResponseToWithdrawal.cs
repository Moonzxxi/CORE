
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseToWithdrawal : Response
    {
        #region Atributes and Constructors
        private string identifier;
        private string name;
        private decimal value;
        private static string description;

        public string Identifier { get => identifier; set => identifier = value; }

        public string Name { get => name; set => name = value; }

        public decimal Value { get => value; set => this.value = value; }

        public ResponseToWithdrawal()
        {

        }

        public ResponseToWithdrawal(bool withdrawal_event, string identifier, string account_name, decimal value)
        {
            Success = withdrawal_event;
            Message = null;
            Value = value;
            Identifier = identifier;
            Name = account_name;
            Value = value;

            switch (Success)
            {
                case true:
                    {
                        Message = $"El retiro de ${Value} fue realizado exitosamente a la cuenta '{Name}' por parte del cliente con cédula {Identifier}.";
                    }
                    break;

                case false:
                    {
                        Message = "No se pudo realizar el deposito porque la cuenta no existe.";
                    }
                    break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }

        public ResponseToWithdrawal(char underflow)
        {
            switch (underflow)
            {
                case 'Y': case 'y':
                    {
                        Success = false;
                        Message = "No se puede realizar la operación de retiro porque la cuenta presenta sobregiro.";
                    } break;

                case 'N': case 'n':
                    {
                        Success = false;
                        Message = "No se puede realizar el retiro porque el balance consultado es mayor al balance presente en la cuenta.";
                    } break;
            }
        }

        public ResponseToWithdrawal(bool hardcoded_withdrawal_event)
        {
            Success = hardcoded_withdrawal_event;
            Message = null;

            switch (Success)
            {
                case false:
                    {
                        Message = "No se pudo procesar la solicitud de retiro porque la cuenta esta inactiva, archivada, o no existe.";
                    }
                    break;
            }
        }

        public ResponseToWithdrawal(bool withdrawal_event, decimal balance)
        {
            Success = withdrawal_event;
            Message = null;
            Value = balance;

            switch (Success)
            {
                case false:
                    {
                        Message = $"No se pudo procesar la solicitud de retiro porque la cuenta no tiene balance: ${Value}.";
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

        public static ResponseToWithdrawal WithdrawalResponse(RequestWithdrawal requestToWithdraw)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            if (requestToWithdraw.Pin.Length > 8) requestToWithdraw.Pin = requestToWithdraw.Pin.Substring(0, 7);

            ResponseToWithdrawal withdrawalResponse = null;
            decimal balanceVerifier = 0;
            List<accountTable> accounts = null;
            bool correctPin = Response.IsThePinCorrect(new Request(requestToWithdraw.Identifier, requestToWithdraw.Pin));
            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            accounts = entities.accountTables.ToList();

            try
            {
                switch (correctPin)
                {
                    case true:
                        {
                            for (int c = 0; c < accounts.Count();  c++)
                            {
                                if (accounts.ElementAt(c).IDENTIFIER == requestToWithdraw.Identifier &&
                                    accounts.ElementAt(c).ACCOUNT_NAME == requestToWithdraw.Account_Name)
                                {
                                    if (accounts.ElementAt(c).ACCOUNT_STATE == AccountStates.INACTIVA.ToString() ||
                                        accounts.ElementAt(c).ACCOUNT_STATE == AccountStates.ARCHIVADA.ToString())
                                    {
                                        withdrawalResponse = new ResponseToWithdrawal(false);
                                        break;
                                    }

                                    else
                                    {
                                        balanceVerifier = accounts.ElementAt(c).BALANCE;

                                        if (balanceVerifier < 0)
                                        {
                                            withdrawalResponse = new ResponseToWithdrawal('Y');
                                            break;
                                        }

                                        if (balanceVerifier == 0)
                                        {
                                            withdrawalResponse = new ResponseToWithdrawal(false, balanceVerifier);
                                            break;
                                        }

                                        if (balanceVerifier < requestToWithdraw.Balance_To_Withdraw)
                                        {
                                            withdrawalResponse = new ResponseToWithdrawal('N');
                                            break;
                                        }

                                        description = TransactionTypes.Withdrawal(requestToWithdraw.Identifier, requestToWithdraw.Account_Name, requestToWithdraw.Balance_To_Withdraw);

                                        entities.bankWithdraw(requestToWithdraw.Balance_To_Withdraw, requestToWithdraw.Identifier,
                                            requestToWithdraw.Account_Name, description);

                                        withdrawalResponse = new ResponseToWithdrawal(true, requestToWithdraw.Identifier, requestToWithdraw.Account_Name, requestToWithdraw.Balance_To_Withdraw);
                                        break;
                                    }
                                }
                            }
                        }
                        break;

                    case false:
                        {
                            withdrawalResponse = new ResponseToWithdrawal(false);
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToWithdrawal'.", ex);
                withdrawalResponse = new ResponseToWithdrawal(false, decimal.MinusOne);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseToWithdrawal' se ha procesado exitosamente.");
            return withdrawalResponse;
        }
    }
}