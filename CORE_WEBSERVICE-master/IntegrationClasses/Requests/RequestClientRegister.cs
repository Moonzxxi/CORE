using System;

namespace Integration.Requests
{
    [Serializable]
    public class RequestClientRegister : Request
    {
        #region Atributes and Constructures
        private string name_regs;
        private string lastname_regs;
        private string password_regs;
        private string pin_regs;
        private string email_regs;
        private string direction_regs;
        private bool autoGenerate;

        public string Name_Regs { get => name_regs; set => name_regs = value; }

        public string Lastname_Regs { get => lastname_regs; set => lastname_regs = value; }

        public string Password_Regs { get => password_regs; set => password_regs = value; }

        public string Pin_Regs { get => pin_regs; set => pin_regs = value; }

        public string Email_Regs { get => email_regs; set => email_regs = value; }

        public string Direction_Regs { get => direction_regs; set => direction_regs = value; }

        public bool AutoGenerate { get => autoGenerate; set => autoGenerate = value; }

        private RequestClientRegister()
        {

        }

        public RequestClientRegister(string name, string last, string identifier, string password, string pin, 
            string email, string direction, bool generation)
        {
            this.Name_Regs = name.ToUpper();
            this.Lastname_Regs = last.ToUpper();
            this.Identifier = identifier.ToUpper();
            this.Password_Regs = password;
            this.Pin_Regs = pin.ToUpper();
            this.Pin = Pin_Regs;
            this.Email_Regs = email.ToUpper();
            this.Direction_Regs = direction.ToUpper();
            this.AutoGenerate = generation;
        }
        #endregion
    }
}