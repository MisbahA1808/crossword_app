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

        //constructor
        public UserManager()
        {
            _users = new List<User>();
            _userId = 1;
        }

        //method to add a new user 
        public void AddUser(string name, string username, string password, string email, string accountType) 
        { 
            User user = new User(_userId, username, username, password, email, accountType);
            user.SetState(accountType);
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
            else { Console.WriteLine("File not found"); }

                //then writing the complete list of users to the json file
                string jsonString = JsonConvert.SerializeObject(existingUsers, Formatting.Indented);
            File.WriteAllText(path, jsonString);


        }

        public bool VerifyUser(string username, string password) 
        {
            string path = "users.json";

            if (!File.Exists(path))
            {
                Console.WriteLine("file not found..");
                return false;
            }
            string json = File.ReadAllText(path);

            var users = JsonConvert.DeserializeObject<List<User>>(json);

            foreach (User user in users) 
            {
                if (user.Username == username && user.Password == password)
                {
                    Console.WriteLine("Login Successful");
                    return true;
                }

               
            }
             Console.WriteLine("Incorrect details entered - please try again!");
            return false;
            //User user = JsonConvert.DeserializeObject<User>(username);




        }


    }
}
