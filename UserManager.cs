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

            string jsonString = JsonConvert.SerializeObject(_users, Formatting.Indented);
            File.WriteAllText("users.json", jsonString);


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
