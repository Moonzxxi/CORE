using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolesNLogs.Admin
{
    public class Administrator : Client
    {

        public string username;
        public string password;
        private int id;
     
        public Administrator(string username, string password, int id)
        {
            this.username = username;
            this.password = password;
            this.id = id;
        }

        public void AdminFuncs()
        {

            var arrUsers = new Administrator[]
        {
            new Administrator("Kevin","zen123",2605),
            new Administrator("Salma","san123",15),
            new Administrator("Axel","gru123",32)
        };

        Start:
            Console.WriteLine("1. Login as administrator \n 2. Register a new system admininstrator");
            var input = Console.ReadLine();



            bool successfull = false;
            while (!successfull)
            {

                if (input == "1")
                {
                    Console.WriteLine("Write your username:");
                    var username = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    var password = Console.ReadLine();


                    foreach (Administrator user in arrUsers)
                    {
                        if (username == user.username && password == user.password)
                        {
                            Console.WriteLine("You have successfully logged in !!!");
                            Console.ReadLine();
                            successfull = true;
                            break;
                        }
                    }

                    if (!successfull)
                    {
                        Console.WriteLine("Your username or password is incorrect, try again !!!");
                    }

                }

                else if (input == "2")
                {

                    Console.WriteLine("Enter your username:");
                    var username = Console.ReadLine();

                    Console.WriteLine("Enter your password:");
                    var password = Console.ReadLine();

                    Console.WriteLine("Enter your id:");
                    int id = int.Parse(Console.ReadLine());


                    Array.Resize(ref arrUsers, arrUsers.Length + 1);
                    arrUsers[arrUsers.Length - 1] = new Administrator(username, password, id);
                    successfull = true;
                    goto Start;

                }
                else
                {
                    Console.WriteLine("Try again !!!");
                    break;


                }

            }
        }

        public virtual void DisplayAdminMenu()
        {
            Console.WriteLine("Test Bank \n Welcome back, sysadmin");
        }

     

    }
}

   
  

