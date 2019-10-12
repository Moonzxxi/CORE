using System;
using System.Collections.Generic;

namespace ConsumirDummy
{
    [Serializable]
    public class RequestClientAccounts : Request
    {
        #region Atributes and Constructors
        private bool incInactive;

        public bool IncInactive { get => incInactive; set => incInactive = value; }

        private RequestClientAccounts()
        {

        }

        public RequestClientAccounts(bool include_inactive, string identifier)
        {
            Identifier = identifier.ToUpper();
            IncInactive = include_inactive;
        }
        #endregion
    }
}