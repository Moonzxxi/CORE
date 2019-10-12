using Integration.Code;
using Integration.EntityFramework;
using Integration.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Integration.Responses
{
    [Serializable]
    public class ResponseChangedAccountState : Response
    {
        #region Atributes and Constructors
        private string accountState;
        private Account theAccount;

        public string AccountState { get => accountState; set => accountState = value; }

        public Account TheAccount { get => theAccount; set => theAccount = value; }

        public ResponseChangedAccountState()
        {

        }

        public ResponseChangedAccountState(bool success)
        {
            Success = success;
            Message = null;

            switch (Success)
            {
                case false:
                    {
                        Message = "No se puede ejecutar el proceso de habilitación o deshabilitación porque el dueño esta insuscrito.";
                    }
                    break;
            }
        }

        public ResponseChangedAccountState(bool success, bool activation, Account _account)
        {
            Success = success;
            Message = null;
            TheAccount = _account;

            switch (Success)
            {
                case true:
                    {
                        switch (activation)
                        {
                            case true:
                                {
                                    Message = $"Rehabilitación de la cuenta del cliente con cédula {TheAccount.Client_ID} exitosa.";
                                }
                                break;

                            case false:
                                {
                                    Message = $"Deshabilitación la cuenta del cliente con cédula {TheAccount.Client_ID} exitosa.";
                                }
                                break;
                        }
                    }
                    break;

                case false:
                    {
                        Message = $"Hubo un problema al operar con la cuenta del cliente {TheAccount.Client_ID}.";
                        AccountState = (Double.NaN).ToString();
                    } break;
            }
        }
        #endregion

        public static ResponseChangedAccountState SelectionResponse(RequestChangeAccountState requestChange)
        {
            ResponseChangedAccountState responseChanged = null;

            switch (requestChange.Activation)
            {
                case true:
                    {
                        responseChanged = ResponseChangedAccountState.ResponseToRenableAccount(requestChange);
                    }
                    break;

                case false:
                    {
                        responseChanged = ResponseChangedAccountState.ResponseToDisableAccount(requestChange);
                    }
                    break;

                default:
                    {
                        throw new Exception("There's no middle state in which a boolean value can be.");
                    }
                    break;
            }

            return responseChanged;
        }

        private static ResponseChangedAccountState ResponseToDisableAccount(RequestChangeAccountState disableAccount)
        {
            if (disableAccount.Pin.Length > 8) disableAccount.Pin = disableAccount.Pin.Substring(0,7);
            List<accountTable> accounts;
            List<clientTable> clients;

            ResponseChangedAccountState responseChanged = null;
            accounts = Entities.Integration.accountTables.ToList();
            bool correctPin = Response.IsThePinCorrect(new Request(disableAccount.Identifier, disableAccount.Pin));
            clients = Entities.Integration.clientTables.ToList();

            try
            {
                switch (correctPin)
                {
                    case true:
                        {
                            foreach (var _element in clients)
                            {
                                if (_element.IDENTIFIER == disableAccount.Identifier &&
                                    _element.STATE == ClientStates.INSUSCRITO.ToString())
                                {
                                    responseChanged = new ResponseChangedAccountState(false);
                                    return responseChanged;
                                }

                                else
                                {
                                    foreach (var element in accounts)
                                    {
                                        if (disableAccount.Identifier == element.IDENTIFIER &&
                                            disableAccount.Account_Name == element.ACCOUNT_NAME)
                                        {
                                            Entities.Integration.accountDeactivate(disableAccount.Identifier, disableAccount.Account_Name);
                                            Account account_to_send = new Account(disableAccount.Account_Name, disableAccount.Identifier,
                                                AccountStates.INACTIVA.ToString(), element.ACCOUNT_TYPE, element.BALANCE);
                                            responseChanged = new ResponseChangedAccountState(true, disableAccount.Activation, account_to_send);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case false:
                        {
                            responseChanged = new ResponseChangedAccountState(false, disableAccount.Activation, new Account());
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseChangedAccountState: Disable'.", ex);
                responseChanged = new ResponseChangedAccountState(false, false, null);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseChangedAccountState: Disable' se ha ejecutado exitosamente.");
            return responseChanged;
        }

        private static ResponseChangedAccountState ResponseToRenableAccount(RequestChangeAccountState enableAccount)
        {
            if (enableAccount.Pin.Length > 8) enableAccount.Pin = enableAccount.Pin.Substring(0,7);
            List<accountTable> accounts;
            List<clientTable> clients;

            ResponseChangedAccountState responseChanged = null;
            accounts = Entities.Integration.accountTables.ToList();
            bool correctPin = Response.IsThePinCorrect(new Request(enableAccount.Identifier, enableAccount.Pin));
            clients = Entities.Integration.clientTables.ToList();

            try
            {
                switch (correctPin)
                {
                    case true:
                        {
                            foreach (var _element in clients)
                            {
                                if (_element.IDENTIFIER == enableAccount.Identifier &&
                                    _element.STATE == ClientStates.INSUSCRITO.ToString())
                                {
                                    responseChanged = new ResponseChangedAccountState(false);
                                    return responseChanged;
                                }

                                else
                                {
                                    foreach (var element in accounts)
                                    {
                                        if (enableAccount.Identifier == element.IDENTIFIER &&
                                            enableAccount.Account_Name == element.ACCOUNT_NAME)
                                        {
                                            Entities.Integration.accountReactivate(enableAccount.Identifier, enableAccount.Account_Name);
                                            Account account_to_send = new Account(enableAccount.Account_Name, enableAccount.Identifier,
                                               AccountStates.INACTIVA.ToString(), element.ACCOUNT_TYPE, element.BALANCE);
                                            responseChanged = new ResponseChangedAccountState(true, enableAccount.Activation, account_to_send);
                                        }
                                    }
                                }
                            }
                        }
                        break;

                    case false:
                        {
                            responseChanged = new ResponseChangedAccountState(false, enableAccount.Activation, new Account());
                        }
                        break;
                }
            }

            catch (Exception ex)
            {
                Log.Error("Ocurrió un error al procesar 'ResponseChangedAccountState: Enable'.", ex);
                responseChanged = new ResponseChangedAccountState(false, false, null);
            }

            finally
            {
                GC.Collect(); Entities.Integration.SaveChanges();
            }

            Log.Info("El 'ResponseChangedAccountState: Enable' se ha ejecutado exitosamente.");
            return responseChanged;
        }
    }
}