using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Code
{
    [Serializable]
    public class Client
    {
        #region Private Atributes
        private string name;
        private string last_name;
        private string ID_number;
        private string email;
        private string client_state;
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

        public int Number_Of_Accounts { get => number_of_accounts; set => number_of_accounts = value; }

        public string Direction { get => direction; set => direction = value; }

        public string Pin { get => pin; set => pin = value; }

        public string Password { get => password; set => password = value; }
        #endregion

        #region Constructors
        public Client()
        {

        }

        public Client(string name, string last, string identifier, string state, int number_accounts,
            string email, string direction)
        {
            Name = name;
            Last_Name = last;
            ID_Number = identifier;
            Email = email;
            Client_State = state;
            Number_Of_Accounts = number_accounts;
            Direction = direction;
        }
        #endregion
    }
}