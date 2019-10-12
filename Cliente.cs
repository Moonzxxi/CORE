using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBCORE 
{
    public class Cliente

    {

        //fields
        private string identifier;

        private double accountNumber;
        protected string accountType;

        protected double balance;
        protected double deposit;
        protected double withdrawal;

        //properties
        public string AccountType
        { get { return this.accountType; } }

        public double Withdrawal
        {
            get { return this.withdrawal; }
            set { this.withdrawal = value; }
        }
        public double Deposit
        {
            get { return this.deposit; }
            set { this.deposit = value; }
        }
        public double AcctNumber
        { get { return this.accountNumber; } }

        public double Bal
        { get { return this.balance; } }

        //constructors
        public Cliente()
        {
            identifier = "Client";
        }

        //methods
        //creates random Account Number
        public virtual double AccountNumb()
        {
            Random rand = new Random();
            this.accountNumber = rand.Next(100000000, 1000000000);
            return accountNumber;
        }

        //Computes General Balance(resets values)
        public virtual double Balance()
        {
            balance = balance + deposit - withdrawal;
            deposit = 0;
            withdrawal = 0;
            return balance;
        }
        //Computers Balance when withdrawal equals zero
        public virtual double DepositBalance(double input)
        {
            deposit = input;
            withdrawal = 0;
            balance = balance + deposit - withdrawal;
            return balance;
        }

        //Computers balance when deposit equals zero
        public virtual double WithBalance(double input)
        {
            withdrawal = input;
            deposit = 0;
            balance = balance + deposit - withdrawal;
            return balance;
        }

        //displays online banking menu
        public virtual void DisplayClientMenu()
        {
            Console.WriteLine("__________________________________________________________________________________________");
            Console.WriteLine("-Test Bank \nWelcome to your online bank account," + identifier + "                       ");
            Console.WriteLine("-Please choose from the options below:\n -1.View Client Info\n2.View Account Balance:       ");
            Console.WriteLine("-     2A.Checking\n    - 2B.Reserve\n    - 2C.Savings                                       ");
            Console.WriteLine("- 3.Deposit Funds:\n    - 3A.Checking\n   -  3B.Reserve\n    - 3C.Savings                    ");
            Console.WriteLine("- 4.Withdraw Funds:\n    - 4A.Checking\n   -  4B.Reserve\n   -  4C.Savings                   ");
            Console.WriteLine("- 5.Exit");
            Console.WriteLine("__________________________________________________________________________________________");
        }
        //client info
        public string ClientInfo()
        {
            string clientinfo = (@"Account Holder: " + identifier);
            return clientinfo;
        }
    }
}
