using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class User
    {
        [JsonProperty] private string _name;
        [JsonProperty] private string _accountType;
        [JsonProperty] private string _username;
        [JsonProperty] private string _password;
        [JsonProperty] private string _email;
        [JsonProperty] private int _userId;

        public User(int userId, string name, string username, string password, string email, string accountType)
        {
            _userId = userId;
            _name = name;
            _username = username;
            _password = password;
            _email = email;

            _accountType = SetAccountType(accountType);

        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }

        //method to set the account type of the user
        public string SetAccountType(string accountType) 
        { 
            accountType = accountType.ToLower().Trim();

            if (accountType == "player") 
            {
                _accountType = "Player";
            
            }
            else if(accountType == "admin") 
            {
                _accountType = "Admin";            
            
            }

            return _accountType;
        }






    }
}
