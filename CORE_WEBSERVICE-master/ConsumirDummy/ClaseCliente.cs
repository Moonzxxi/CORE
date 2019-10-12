using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumirDummy
{
    public class Client
    {
        #region Private atributes
        private string name;
        private string last_name;
        private string ID_number;
        private string email;
        private string client_state;
        private List<Account> accounts;
        private int number_of_accounts;
        private string direction;
        private string pin;
        private string password;
        #endregion

        #region Encapsulation 
        public string Name { get => name; set => name = value; }

        public string Last_Name { get => last_name; set => last_name = value; }

        public string ID_Number { get => ID_number; set => ID_number = value; }

        public string Email { get => email; set => email = value; }

        public string Client_State { get => client_state; set => client_state = value; }

        public List<Account> Accounts { get => accounts; set => accounts = value; }

        public int Number_Of_Accounts { get => number_of_accounts; set => number_of_accounts = value; }

        public string Direction { get => direction; set => direction = value; }

        public string Pin { get => pin; set => pin = value; }

        public string Password { get => password; set => password = value; }
        #endregion

        #region Constructors
        public Client()
        {

        }

        public Client(string name, string last, string identifier, string state, List<Account> accounts,
            string email, string direction, string pin, string password)
        {
            Name = name;
            Last_Name = last;
            ID_Number = identifier;
            Email = email;
            Client_State = state;
            Accounts = accounts;
            Number_Of_Accounts = accounts.Count();
            Direction = direction;
            Pin = pin;
            Password = password;
        }

        public Client(string nAME, string lAST, string iDENTIFIER, string sTATE, int aCCOUNTS, string eMAIL, string dIRECTION)
        {
            name = nAME;
            email = eMAIL;
            direction = dIRECTION;
        }
        #endregion
    }

}
