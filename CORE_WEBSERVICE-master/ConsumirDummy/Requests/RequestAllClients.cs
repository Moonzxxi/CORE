using System;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestAllClients : Request
    {
        #region Atributes and Constructors
        private bool incUnsubs;

        public bool IncUnsubs { get => incUnsubs; set => incUnsubs = value; }

        private RequestAllClients()
        {

        }

        public RequestAllClients(bool include_unsubscribed)
        {
            IncUnsubs = include_unsubscribed;
        }
        #endregion
    }
}
