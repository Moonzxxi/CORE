﻿using System;

namespace Integration.Code
{
    #region Enumerations
    [Serializable]
    public enum AccountTypes
    {
        EMPRESARIAL = 1,
        AHORRO,
        CORRIENTE,
        OTRO
    }

    [Serializable]
    public enum AccountStates
    {
        ACTIVA = 1,
        INACTIVA,
        ARCHIVADA
    }

    [Serializable]
    public enum ClientStates
    {
        SUSCRITO = 1,
        INSUSCRITO
    }
    #endregion
}