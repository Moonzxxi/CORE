using Integration.Code;
using Integration.Requests;
using System;
using System.Linq;

namespace Integration.Responses
{
    [Serializable]
    public class ResponseToTransfer : Response
    {
        #region Atributes and Constructors
        private string first_identifier;
        private string second_identifier;
        private string account_name1;
        private string account_name2;
        private decimal balance;
        private static string description;

        public string First_Identifier { get => first_identifier; set => first_identifier = value; }

        public string Second_Identifier { get => second_identifier; set => second_identifier = value; }

        public string Account_Name1 { get => account_name1; set => account_name1 = value; }

        public string Account_Name2 { get => account_name2; set => account_name2 = value; }

        public decimal Balance { get => balance; set => balance = value; }

        public ResponseToTransfer()
        {

        }

        public ResponseToTransfer(bool sameness)
        {
            First_Identifier = null;
            Second_Identifier = null;
            Account_Name1 = null;
            Account_Name2 = null;
            Message = null;

            switch (sameness)
            {
                case true:
                    {
                        Message = "Los dos identificadores son identicos; Refierase a los depositos y a los retiros.";
                    }
                    break;
            }
        }

        public ResponseToTransfer(bool success, string identifier1, string identifier2, string account1,
            string account2, decimal balance)
        {
            First_Identifier = identifier1;
            Second_Identifier = identifier2;
            Account_Name1 = account1;
            Account_Name2 = account2;
            Success = success;
            Balance = balance;
            Message = null;

            switch (Success)
            {
                case true:
                    {
                        Message = $"La transferencia de {First_Identifier} a {Second_Identifier}, a las cuentas {Account_Name1} y {Account_Name2}, respectivamente, " +
                            $"fue realizada satisfactoriamente con un valor de ${Balance}.";
                    }
                    break;

                case false:
                    {
                        Message = $"La transferencia solicitada por el usuario con cédula {First_Identifier} fracasó";
                    }
                    break;
            }
        }

        public ResponseToTransfer(char underflow)
        {
            switch (underflow)
            {
                case 'Y':
                case 'y':
                    {
                        Success = false;
                        Message = "No se puede realizar transacciones porque la cuenta presenta sobregiro.";
                    }
                    break;

                case 'N':
                case 'n':
                    {
                        Success = false;
                        Message = "No se puede realizar transacciones porque el balance solicitado es mayor al balance presente en la cuenta.";
                    }
                    break;
            }
        }

        public ResponseToTransfer(bool transfer_complete, decimal balance)
        {
            Success = transfer_complete;
            Message = null;
            Balance = balance;

            switch (Success)
            {
                case false:
                    {
                        Message = $"No se pudo procesar la solicitud de transferencia porque el cliente o la cuenta no existe " +
                            $"o porque la cuenta no tiene balance: ${Balance}.";
                    }
                    break;
            }
        }
        #endregion

        public static ResponseToTransfer ResponseToATransfer(RequestTransfer requestTransfer)
        {
            if (requestTransfer.Pin.Length > 8) requestTransfer.Pin = requestTransfer.Pin.Substring(0, 7);

            ResponseToTransfer responseTransfer = null;
            decimal balanceVerifier = 0;
            var account_table = Entities.Integration.accountTables.ToList();
            bool correctPin = Response.IsThePinCorrect(new Request(requestTransfer.Identifier_Root, requestTransfer.Pin));

            try
            {
                if (requestTransfer.Identifier_Root == requestTransfer.Identifier_To_Affect)
                {
                    bool sameness = true;
                    responseTransfer = new ResponseToTransfer(sameness);
                }

                else
                {
                    foreach (var element in account_table)
                    {
                        if (requestTransfer.Account_Root == element.ACCOUNT_NAME &&
                            requestTransfer.Identifier_Root == element.IDENTIFIER)
                        {
                            balanceVerifier = element.BALANCE;
                        }
                    }

                    if (balanceVerifier < 0) return responseTransfer = new ResponseToTransfer('Y');
                    if (balanceVerifier == 0) return responseTransfer = new ResponseToTransfer(false, balanceVerifier);
                    if (balanceVerifier < requestTransfer.Balance_To_Transfer) return responseTransfer = new ResponseToTransfer('N');

                    foreach (var element in account_table)
                    {
                        if (requestTransfer.Account_To_Affect == element.ACCOUNT_NAME &&
                            requestTransfer.Identifier_To_Affect == element.IDENTIFIER)
                        {
                            description = TransactionTypes.Transfer(requestTransfer.Identifier_Root, requestTransfer.Identifier_To_Affect, requestTransfer.Account_Root,
                                requestTransfer.Account_To_Affect, requestTransfer.Balance_To_Transfer, false);

                            Entities.Integration.bankTransfer(requestTransfer.Balance_To_Transfer, requestTransfer.Identifier_Root, requestTransfer.Account_Root,
                                requestTransfer.Identifier_To_Affect, requestTransfer.Account_To_Affect, description);

                            responseTransfer = new ResponseToTransfer(true, requestTransfer.Identifier_Root, requestTransfer.Identifier_To_Affect, requestTransfer.Account_Root,
                                requestTransfer.Account_To_Affect, requestTransfer.Balance_To_Transfer);
                            break;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToTransfer'", ex);
                responseTransfer = new ResponseToTransfer(false, decimal.MinusOne);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseToTransfer' se ha ejecutado exitosamente.");
            return responseTransfer;
        }
    }
}