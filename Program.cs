using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace CrosswordApp
{
    internal class Program
    {
        private static bool _isFileLoaded;

        static void Main(string[] args)
        {
            _isFileLoaded = false;

            if (_isFileLoaded)
            {
                DisplayLogin();

            }
            else 
            {
                Console.WriteLine("File NOT loaded, use 'admin' and 'password to login. Press ENTER to continue.");
                Console.ReadKey(true);
                DisplayLogin();
            }

            
        }

        public static void DisplayMenu() 
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the Crossword Solver & Builder!!");
            Console.ResetColor();

            //creating the menu objects and adding sub menus/ menu items to them
            Menu menu1 = new Menu("MY ACCOUNT");
            menu1.AddMenuItem("Login");
            menu1.AddMenuItem("Create Account");
            menu1.AddMenuItem("Change Role");

            Menu menu2 = new Menu("CROSSWORDS");
            menu2.AddMenuItem("Create Crossword");
            menu2.AddMenuItem("Solve Crossword");
            
            
            Menu menu3 = new Menu("SETTINGS");
            menu3.AddMenuItem("Logout");
            


            //creating a menu manager object
            MenuManager menuManager = new MenuManager(new List<Menu> { menu1, menu2, menu3});

                       
            string choice = menuManager.RunMenu();

            switch (choice)
            {
                case "Logout":
                    DisplayLogin();
                    break;

                case "Create Crossword":
                    Console.Clear();
                    Console.WriteLine("create cross");
                    break;

                case "Solve Crossword":
                    Console.Clear();
                    Console.WriteLine("solve cross");
                    break;

                case "Create Account":
                    Console.Clear();
                    DisplayAccountCreation();
                    break;

                default:
                    Console.ReadKey(true);
                    DisplayMenu();
                    break;
            }

            //ConsoleKey keyPressed;
            //menuManager.DisplayMenu();

            ////a loop to always display the menu
            //while (true)
            //{
            //    //gets the key pressed as user input
            //    keyPressed = Console.ReadKey(true).Key;
            //    //passes the value of th ekey pressed as a parameter to menu manager's update menu method
            //    string choice = menuManager.UpdateMenu(keyPressed);

            //    //clears/refreshes the console every time a key is pressed
            //    Console.Clear();
            //    //Console.Write(choice);

            //    menuManager.DisplayMenu();

            //}
        }

        public static void CreateUser() 
        {
            UserManager userManager = new UserManager();
            ConsoleKey keyPressed;

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("CREATE ACCOUNT");
            Console.WriteLine();
            Console.ResetColor();


            //getting the username, password, email and name of the user
            Console.WriteLine("Enter your full name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter your email address:");
            string email = Console.ReadLine();

            Console.WriteLine("Enter a username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter a password");
            string password = Console.ReadLine();

            //creating an instance of the user and adding them to the json file
            userManager.AddUser(name,username,password,email, "player");

            userManager.SaveUserData();

            Console.WriteLine("Account created successfully! Press Enter to return to the Login Page.");
            keyPressed = Console.ReadKey(true).Key;

            Console.ReadKey(true);
            DisplayLogin();
            //if (keyPressed == ConsoleKey.Enter)
            //{
            //    Console.Clear();
            //    DisplayLogin();
            //}


        }

        public static void VerifyUser() 
        {
            Console.Clear();
            UserManager userManager = new UserManager();
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            bool isVerified = userManager.VerifyUser(username, password);

            if (isVerified)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                DisplayMenu();
            }
            else 
            {
                Console.WriteLine("Invalid login details, press any key to retry.");
                Console.ReadKey(true);
                VerifyUser();
            }


        }

        public static void DisplayLogin() 
        {
            Console.Clear();
            Console.SetCursorPosition(38, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the Crossword Solver & Builder!");
            Console.WriteLine();
            Console.ResetColor();

            //Console.WriteLine("Would you like to: \n(L) Login  \n(C) Create an account (C) ?");

            Menu menu1 = new Menu("Please select an option below to continue:");
            menu1.AddMenuItem("Login");
            menu1.AddMenuItem("Create Account");

            MenuManager menuManager = new MenuManager(new List<Menu> { menu1});
            string selectedOption = menuManager.RunMenu();

            switch (selectedOption) 
            {
                case "Login":
                    Console.Clear();
                    VerifyUser();
                    break;

                case "Create Account":
                    Console.Clear();
                    CreateUser();
                    break;

                default:
                    break;
            }

            //ConsoleKey keyPressed;
            //menuManager.DisplayMenu();

            ////a loop to always display the menu
            //while (true)
            //{
                
            //    //gets the key pressed as user input
            //    keyPressed = Console.ReadKey(true).Key;
            //    //passes the value of th ekey pressed as a parameter to menu manager's update menu method
            //    string choice = menuManager.UpdateMenu(keyPressed);

            //    //clears/refreshes the console every time a key is pressed
            //    Console.Clear();
            //    //Console.Write(choice);
            //    Console.SetCursorPosition(38, 0);
            //    Console.ForegroundColor = ConsoleColor.Blue;
            //    Console.WriteLine("Welcome to the Crossword Solver & Builder!");
            //    Console.WriteLine();

            //    Console.ResetColor();
            //    menuManager.DisplayMenu();

            //    if (keyPressed == ConsoleKey.Enter) 
            //    {
            //        //Console.Clear();
            //        //CreateUser();
            //        //break;
            //        menuManager.UpdateMenu(keyPressed);
            //    }
            //}

            
        }

        public void SetCrosswordDimensions()
        {
            Console.WriteLine("CREATE CROSSWORD");
            Console.WriteLine("Title: ");
            string title = Console.ReadLine();
            Console.WriteLine("Number of Rows: ");
            int rows = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Number of Columns: ");
            int columns = Convert.ToInt32(Console.ReadLine());

            Crossword crossword = new Crossword(rows, columns, title);
                
        
        }

        public static void DisplayAccountCreation() 
        {
            Console.Clear();
        
        
        }


    }

    
}
