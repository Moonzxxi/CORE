using Integration;
using Integration.Requests;
using Integration.Responses;
using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestClientExists : Request
    {
        private string first_name;
        private string last_name;

        public string First_Name { get => first_name; set => first_name = value; }

        public string Last_Name { get => last_name; set => last_name = value; }

        private RequestClientExists()
        {

        }

        public RequestClientExists(string identifier, string name, string last_name)
        {
            Identifier = identifier.ToUpper();
            First_Name = name.ToUpper();
            Last_Name = last_name.ToUpper();
        }
    }
}