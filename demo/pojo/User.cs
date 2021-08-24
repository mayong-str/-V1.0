using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace demo.pojo
{
    class User
    {
        private int ID;
        private string username;
        private string password;

        public int ID1 
        { 
            get => ID; 
            set => ID = value; 
        }
        public string Username 
        { 
            get => username; 
            set => username = value; 
        }
        public string Password 
        { 
            get => password; 
            set => password = value; 
        }
    }
}
