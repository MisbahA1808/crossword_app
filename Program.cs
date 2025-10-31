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

        static void Main(string[] args)
        {
            UserManager _userManager;
            //MenuManager _menuManager = new MenuManager(); ;


            DisplayLogin();
        }

        public static void DisplayMenu(MenuManager menuManager) 
        {
            //creating the menu objects and adding sub menus/ menu items to them
            Menu menu1 = new Menu("MY ACCOUNT");
            menu1.AddMenuItem("Login");
            menu1.AddMenuItem("Create Account");
            menu1.AddMenuItem("Logout");

            Menu menu2 = new Menu("CREATE CROSSWORD");
            menu2.AddMenuItem("option 3");
            menu2.AddMenuItem("option 4");

            Menu menu3 = new Menu("SOLVE CROSSWORD");
            menu3.AddMenuItem("option 5");
            menu3.AddMenuItem("option 6");

            Menu menu4 = new Menu("MENU 4");
            menu4.AddMenuItem("option 7");
            menu4.AddMenuItem("option 8");

            Menu menu5 = new Menu("MENU 5");
            menu5.AddMenuItem("option 9");
            menu5.AddMenuItem("option 10");
            menu5.AddMenuItem("option 11");
            menu5.AddMenuItem("option 12");

            //creating a menu manager object
            menuManager = new MenuManager(new List<Menu> { menu1, menu2, menu3, menu4, menu5 });

            ConsoleKey keyPressed;
            menuManager.DisplayMenu();

            //a loop to always display the menu
            while (true)
            {
                //gets the key pressed as user input
                keyPressed = Console.ReadKey(true).Key;
                //passes the value of th ekey pressed as a parameter to menu manager's update menu method
                string choice = menuManager.UpdateMenu(keyPressed);

                //clears/refreshes the console every time a key is pressed
                Console.Clear();
                //Console.Write(choice);

                menuManager.DisplayMenu();

            }
        }

        public static void CreateUser(UserManager userManager) 
        {
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

        }

        public static void VerifyUser(UserManager userManager) 
        {
            Console.WriteLine("Enter your username:");
            string username = Console.ReadLine();
            Console.WriteLine("Enter your password:");
            string password = Console.ReadLine();

            userManager.VerifyUser(username, password);
            



        }

        public static void DisplayLogin() 
        {
            Console.SetCursorPosition(38, 0);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the Crossword Solver & Builder!");
            Console.WriteLine();
            Console.ResetColor();

            //Console.WriteLine("Would you like to: \n(L) Login  \n(C) Create an account (C) ?");

            Menu menu1 = new Menu("Please select an option below to continue:");
            menu1.AddMenuItem("* Login");
            menu1.AddMenuItem("* Create Account");

            MenuManager menuManager = new MenuManager(new List<Menu> { menu1});

            ConsoleKey keyPressed;
            menuManager.DisplayMenu();

            //a loop to always display the menu
            while (true)
            {
                

                //gets the key pressed as user input
                keyPressed = Console.ReadKey(true).Key;
                //passes the value of th ekey pressed as a parameter to menu manager's update menu method
                string choice = menuManager.UpdateMenu(keyPressed);

                //clears/refreshes the console every time a key is pressed
                Console.Clear();
                //Console.Write(choice);
                Console.SetCursorPosition(38, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to the Crossword Solver & Builder!");
                Console.WriteLine();

                Console.ResetColor();
                menuManager.DisplayMenu();

            }
            
        
        }



    }

    
}
