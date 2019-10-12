
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsumirDummy
{
    [Serializable]
    public class ResponseToAllTransactionsOfClient : Response
    {
        #region Atributes and Constructors
        private List<Transaction> transactions;
        private int trans_Count;

        public List<Transaction> Transactions { get => transactions; set => transactions = value; }

        public int Trans_Count { get => trans_Count; set => trans_Count = value; }

        public ResponseToAllTransactionsOfClient()
        {

        }

        public ResponseToAllTransactionsOfClient(bool success, List<Transaction> transactions_of_client)
        {
            Log.Debug("Se inició el metodo de la 'Capa de Integración'", new Exception("Bank2.ConnectionException.FaultyCore: Core services are down!"));
            Success = success;
            Message = null;
            Transactions = null;
            Trans_Count = transactions_of_client.Count;

            switch (Success)
            {
                case true:
                    {
                        Message = $"Se han encontrado {Trans_Count} transaccion(es) con los parametros insertados:";
                        Transactions = transactions_of_client;
                    }
                    break;

                case false:
                    {
                        Message = "No se puedieron encontrar transacciones del cliente.";
                        Transactions = new List<Transaction> { };
                    }
                    break;

                default:
                    {
                        throw new Exception("Success is a boolean, and can only be either true or false.");
                    }
                    break;
            }
        }


        public static ResponseToAllTransactionsOfClient ResponseToAllTransactionsOfTheClient(RequestAllTransactionsOfClient transactionsOfClient)
        {
            ResponseToAllTransactionsOfClient allTransactions = null;
            Transaction transaction = new Transaction(), transaction_to_send = null;
            List<Transaction> transactions = new List<Transaction> { };

            CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
            var transaction_table = entities.transactionTables.ToList();
            bool correctPin = Response.IsThePinCorrect(new Request(transactionsOfClient.Identifier, transactionsOfClient.Pin));

            try
            {
                switch (correctPin)
                {
                    case true:
                        {
                            foreach (var element in transaction_table)
                            {
                                if (transactionsOfClient.Identifier == element.IDENTIFIER_ROOT)
                                {
                                    transaction_to_send = new Transaction(element.IDENTIFIER_ROOT, element.IDENTIFIER_AFFECTED, element.ACCOUNT_ROOT,
                                        element.ACCOUNT_AFFECTED, element.TYPE, element.BALANCE, element.TRANSDATE, element.DESCRIPTION);
                                    transactions.Add(transaction_to_send);

                                    allTransactions = new ResponseToAllTransactionsOfClient(true, transactions);
                                    break;
                                }

                                else
                                {
                                    allTransactions = new ResponseToAllTransactionsOfClient(true, new List<Transaction> { new Transaction() });
                                    continue;
                                }
                            }
                        }
                        break;

                    case false:
                        {
                            allTransactions = new ResponseToAllTransactionsOfClient(true, new List<Transaction> { });
                        }
                        break;
                }
            }


            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseToAllTransactionsOfClient'.", ex);
                allTransactions = new ResponseToAllTransactionsOfClient(false, null);
            }

            finally
            {
                GC.Collect(); entities.SaveChanges();
            }

            Log.Info("El 'ResponseToAllTransactionsOfClient' se ha ejecutado exitosamente.");
            return allTransactions;
        }
        #endregion
    }
}