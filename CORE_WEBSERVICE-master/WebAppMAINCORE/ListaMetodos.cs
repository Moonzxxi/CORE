using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBCORE
{
    public class ListaMetodos
    {
        public bool RegistrarClient(string name, string last, string identifier, string email, string pin, string direction, string password)
        {
            try
            {
                CoreProyectoDBEntities entities = new CoreProyectoDBEntities();
                entities.clientRegister(identifier, name, last, password, pin, direction, email);
                entities.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}