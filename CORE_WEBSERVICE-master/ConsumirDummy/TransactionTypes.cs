using System;

namespace ConsumirDummy
{
    [Serializable]
    public struct TransactionTypes
    {
        private static string Transaction;

        public static string Withdrawal(string identifier, string account_name, decimal balance)
        {
            Transaction = $"El usuario con identificador {identifier} realizó un retiro con valor de ${balance} a la cuenta {account_name}";
            return Transaction;
        }

        public static string Deposit(string identifier, string account_name, decimal balance)
        {
            Transaction = $"El usuario con identificador {identifier} realizó un deposito con valor de ${balance} a la cuenta {account_name}";
            return Transaction;
        }

        public static string Transfer(string identifier1, string identifier2, string account_name1, string account_name2, decimal balance, bool interbank)
        {
            Transaction = null;

            switch (interbank)
            {
                case true:
                    {
                        Transaction = $"El usuario con identificador {identifier1} realizó una transferencia interbancaria a otro usuario con identificador {identifier2}, afectando a las cuentas {account_name1} y {account_name2}, respectivamente, con un valor de ${balance}.";
                    }
                    break;

                case false:
                    {
                        Transaction = $"El usuario con identificador {identifier1} realizó una transferencia no interbancaria a otro usuario con identificador {identifier2}, afectando a las cuentas {account_name1} y {account_name2}, respectivamente, con un valor de ${balance}.";
                    }
                    break;

                default: throw new ArgumentException("A boolean only contains a true or false value.");
            }

            return Transaction;
        }

        public static string ThirPartyTransfer(string identifier, string account_name, string thirdparty, decimal balance)
        {
            Transaction = $"El usuario con identificador {identifier} realizó una transacción a un tercero {thirdparty} con un valor de ${balance} a la cuenta {account_name}";
            return Transaction;
        }
    }
}
