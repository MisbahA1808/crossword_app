using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace CrosswordApp
{
    internal class UserManager
    {
        //list of all users
        private List<User> _users;
        private int _userId;
        private int _loginState = -1;
        private User _loggedInUser;

        public int LoginState { get => _loginState; set => _loginState = value; }
        internal User LoggedInUser { get => _loggedInUser; set => _loggedInUser = value; }

        //constructor
        public UserManager()
        {
            _users = new List<User>();
         
        }

        //method to add a new user 
        public void AddUser(string name, string username, string password, string email, string accountType) 
        { 
            User user = new User(_userId, username, username, password, email, accountType);
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

        //method to save user data to users.json
        public void SaveUserData() 
        {
            //var json = JsonConvert.SerializeObject(users);
            //var path = @"";

            //File.WriteAllText(path, json);
            string path = "users.json";
            //var jsonData = File.ReadAllText(path);
            //_users = JsonConvert.DeserializeObject<List<User>>(jsonData);

            //jsonData = JsonConvert.SerializeObject(_users, Formatting.Indented);
            //File.AppendAllText(path, jsonData);
            List <User> existingUsers = new List<User> ();

            //check if the json file exists
            if (File.Exists(path)) 
            {
                //read all text in the json file
                string json = File.ReadAllText(path);

                //if the file is not empty
                if (!string.IsNullOrEmpty(json)) 
                {
                    //gets the list of existing users
                    existingUsers = JsonConvert.DeserializeObject<List<User>>(json); 

                    //for each user that exists
                    foreach (User user in _users)
                    {
                        //add them to a new list
                        existingUsers.Add(user);


                    }


                }
            
            
            }
            //inform the user if the file is not found
            else { Console.WriteLine("File not found"); }

               
            //then writing the complete list of users to the json file
            string jsonString = JsonConvert.SerializeObject(existingUsers, Formatting.Indented);
            File.WriteAllText(path, jsonString);


        }

        //method to verify a user who has already created an account
        public bool VerifyUser(string username, string password) 
        {
            //file path
            string path = "users.json";

            //if the json file doesn't exist
            if (!File.Exists(path))
            {
                //inform the user
                Console.WriteLine("file not found..");
                //return false as user could not be verified
                return false;
            }
            //if file is found, read the json file
            string json = File.ReadAllText(path);

            //store all the user objects from the json file in a list of type user
            var users = JsonConvert.DeserializeObject<List<User>>(json);

            //for each user in the list
            foreach (User user in users) 
            {
                //if their username and password matches the user input
                if (user.Username == username && user.Password == password)
                {
                    //inform them of a successful login
                    Console.WriteLine("Login Successful");

                    _loggedInUser = user;

                    if (user.AccountType == "Admin")
                    {
                        LoginState = 0;
                    }
                    else { LoginState = 1; }


                        //return true as they have been verified successfully
                        return true;
                }

               
            }
            //otherwise return false and inform them of incorrect login details
             Console.WriteLine("Incorrect details entered - please try again!");
            return false;




        }

        //method to logout
        public void Logout() 
        {
            //sets login state to -1 (not logges in) and logged in user to null
            _loggedInUser = null;
            _loginState = -1;
                                
        
        }

        //method to create a default admin account
        public void CreateDefaultAdminAccount()
        {
            //define file path of json
            string path = "users.json";

            //create a new list of users
            List<User> users = new List<User>();

            //if the json file exists
            if (File.Exists(path)) 
            {
                //read the entire file
                string json = File.ReadAllText(path);

                //if the file is not empty 
                if (!string.IsNullOrWhiteSpace(json))
                {
                    //deserialise the file and store all the user objects in users
                    users = JsonConvert.DeserializeObject<List<User>>(json);
                }
            
            
            }

            //variable to check if an admin account alreayd exists
            bool exists = false;

            //lopo through all the users in the user list
            foreach (User user in users) 
            {
                //if the usrname and password match the default admin username and password
                if (user.Username == "admin" && user.Password == "password") 
                {
                    //set exists to true
                    exists = true;
                    //breka out of the loop
                    break;
                }
            
            }

            //if the defualt accoutn does not exust
            if (!exists) 
            {
                //create an admin acocunt b ycreating and admin object
                User admin = new User(0, "Default Admin", "admin", "password", "admin@crosswordbuilder", "Admin");
                //add admin to the user list
                users.Add(admin);
            }

            //serialise the file and write all of the users from the users list on to users.json
            string jsonString = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(path,jsonString);

        }

    }
}
