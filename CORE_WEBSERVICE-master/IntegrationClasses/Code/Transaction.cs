using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integration.Code
{
    [Serializable]
    public class Transaction
    {
        private string identifier_root;
        private string identifier_affected;
        private string account_root;
        private string account_affected;
        private string type;
        private decimal balance;
        private DateTime transaction_Date;
        private string Description;

        public string Identifier_root { get => identifier_root; set => identifier_root = value; }

        public string Identifier_affected { get => identifier_affected; set => identifier_affected = value; }

        public string Account_root { get => account_root; set => account_root = value; }

        public string Account_affected { get => account_affected; set => account_affected = value; }

        public string Type { get => type; set => type = value; }

        public decimal Balance { get => balance; set => balance = value; }

        public DateTime Transaction_Date { get => transaction_Date; set => transaction_Date = value; }

        public string Description1 { get => Description; set => Description = value; }

        public Transaction()
        {

        }

        public Transaction(string identifier_root, string identifier_affected, string account_root, string account_affected, 
            string type, decimal balance, DateTime trans_date, string description)
        {
            Identifier_root = identifier_root;
            Identifier_affected = identifier_affected;
            Account_root = account_root;
            Account_affected = account_affected;
            Type = type;
            Balance = balance;
            Transaction_Date = trans_date;
            Description = description;
        }
    }
}