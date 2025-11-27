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
        //attributes
        //all as json properties so that they cna be serialised to the json file users.josn for storage
        [JsonProperty] private string _name;
        [JsonProperty] private string _accountType;
        [JsonProperty] private string _username;
        [JsonProperty] private string _password;
        [JsonProperty] private string _email;
        [JsonProperty] private int _userId;
        //[JsonProperty] private int _state;

        //constructor
        public User(int userId, string name, string username, string password, string email, string accountType)
        {
            _userId = userId;
            _name = name;
            _username = username;
            _password = password;
            _email = email;
            //by default, the state is -1 (not logged in)
            _accountType = SetAccountType(accountType);

        }

        //getters and setters
        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public string AccountType { get => _accountType; set => _accountType = value; }

        //public int State { get => _state; set => _state = value; }

        //method to set the account type of the user
        public string SetAccountType(string accountType) 
        {
            //if no acocunt type is selected, they areby default a player
            if (String.IsNullOrEmpty(accountType)) 
            {
                _accountType = "Player";
                return accountType; 
            }
            accountType = accountType.ToLower().Trim();

            //if the usre input is player
            if (accountType == "player")
            {
                _accountType = "Player";

            }
            //if the user input is admin
            else if (accountType == "admin")
            {
                _accountType = "Admin";

            }
            //if input is invalid, by default set to player
            else { _accountType = "Player"; }

            //returns the account type
            return _accountType;
        }

    }
}
