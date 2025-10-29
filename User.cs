using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class User
    {
        private string _name;
        private bool _accountType;
        private string _username;
        private string _password;
        private string _email;
        private int _userId;

        public User(string name, string username, string password, string email)
        {
            _name = name;
            _username = username;
            _password = password;
            _email = email;

        }



    }
}
