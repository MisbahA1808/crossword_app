using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class UserManager
    {
        private List<User> _users;

        public UserManager()
        {
            
        }

        public void AddUser(User user) 
        { 
            _users.Add(user);
        
        }


    }
}
