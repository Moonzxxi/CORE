using System;

namespace ConsumirDummy
{
    [Serializable]
    public class Request
    {
        #region Master Atributes and Constructors
        private string identifier;
        private string pin;

        public string Identifier { get => identifier; set => identifier = value; }

        public string Pin { get => pin; set => pin = value; }
        
        private Request(string placeholder)
        {

        }
        public Request()
        {

        }

        public Request(string identifier, string pin)
        {
            Identifier = identifier.ToUpper();
            Pin = pin.ToUpper();
        }
        #endregion
    }
}