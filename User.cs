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
        [JsonProperty] private int _state;

        public User(int userId, string name, string username, string password, string email, string accountType)
        {
            _userId = userId;
            _name = name;
            _username = username;
            _password = password;
            _email = email;
            _state = -1;
            _accountType = SetAccountType(accountType);

        }

        public string Username { get => _username; set => _username = value; }
        public string Password { get => _password; set => _password = value; }
        public int State { get => _state; set => _state = value; }

        //method to set the account type of the user
        public string SetAccountType(string accountType) 
        {
            if (String.IsNullOrEmpty(accountType)) 
            {
                _accountType = "Player";
                return accountType; 
            }
            accountType = accountType.ToLower().Trim();

            if (accountType == "player")
            {
                _accountType = "Player";

            }
            else if (accountType == "admin")
            {
                _accountType = "Admin";

            }
            else { _accountType = "Player"; }

                return _accountType;
        }

        //method to set/change account state (between player, admin and not logged in)
        public int SetState(string accountType) 
        {
            //if the account type is admin
            if (_accountType == "admin")
            {
                //the state = 0
                _state = 0;
            }
            //if the account type is player, the state = 1
            else if (_accountType == "player")
            {
                _state = 1;
            }
            //otherwise the state = -1 (not logged in)
            else { _state = -1; }
            

            return _state;

        }
        //when log out, state returns to -1, -1's can only log in, to create anoter username u have to be an admin
        //a player can only change their password and solve crosswords, admins have free reign
        public bool ChangeUserRole(User user) 
        {
            bool validChange = false;


            //needs updating further!!!
            if (user._state == 1)
            {
                validChange = true;
                return validChange;

            }
            else
            {
                return validChange;

             };
                        
        
        }








    }
}
