using System;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestAllTransactionsOfClient : Request
    {
        private RequestAllTransactionsOfClient()
        {

        }

        public RequestAllTransactionsOfClient(string identifier, string pin)
        {
            Identifier = identifier.ToUpper();
        }
    }
}