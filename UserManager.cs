using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class UserManager
    {
        //list of all users
        private List<User> _users;
        private int _userId;

        //constructor
        public UserManager()
        {
            _users = new List<User>();
            _userId = 1;
        }

        //method to add a new user 
        public void AddUser(string name, string username, string password, string email) 
        { 
            User user = new User(name, username, password, email);
            _users.Add(user);
            _userId++;
        
        }

        //method to remove a user form the users list
        public void RemoveUser(User user) 
        { 
            _users.Remove(user);
        
        }

        //method to return the list of all users
        public List<User> GetAllUsers() 
        { 
            return _users;
        }


    }
}
