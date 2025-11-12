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
            //Crossword crossword = new Crossword(5, 5, "test");
            //CrosswordManager crosswordManager = new CrosswordManager(crossword);
            //crosswordManager.StoreCurrentCrossword();

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
            DisplayWelcomeMessage();
            Console.SetCursorPosition(0, 2);

            //creating the menu objects and adding sub menus/ menu items to them
            Menu menu1 = new Menu("MY ACCOUNT");
            menu1.AddMenuItem("(L) Login");
            menu1.AddMenuItem("(A) Create Account");
            menu1.AddMenuItem("(R) Change Role");

            Menu menu2 = new Menu("CROSSWORDS");
            menu2.AddMenuItem("(C) Create Crossword");
            menu2.AddMenuItem("(S) Solve Crossword");
            
            
            Menu menu3 = new Menu("SETTINGS");
            menu3.AddMenuItem("(Q) Logout");
            
            //creating a menu manager object
            MenuManager menuManager = new MenuManager(new List<Menu> { menu1, menu2, menu3});
                        
                       
            string choice = menuManager.RunMenu();

            switch (choice)
            {
                case "(Q) Logout":
                    DisplayLogin();
                    break;

                case "(C) Create Crossword":
                    Console.Clear();
                    DisplayWelcomeMessage();

                    CrosswordManager crosswordManager = new CrosswordManager(SetCrosswordDimensions());
                    crosswordManager.AdminCreateCrossword();
                    crosswordManager.AddCrosswordToList();
                    crosswordManager.StoreCurrentCrossword();
                    break;

                case "(S) Solve Crossword":
                    Console.Clear();
                    DisplayCrosswordSolver();
                    break;

                case "(A) Create Account":
                    Console.Clear();
                    DisplayAccountCreation();
                    break;

                case "(L) Login":
                    Console.WriteLine("You are already Logged In!");
                    break;

                case "(R) Change Role":
                    Console.Clear();
                    //need to get user role, verify it then put an appropriate message here based on that
                    Console.WriteLine("Change User Role:");
                    break;

                default:
                    Console.ReadKey(true);
                    DisplayWelcomeMessage();
                    Console.SetCursorPosition(0, 2);

                    DisplayMenu();
                    break;
            }

            

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
            DisplayWelcomeMessage();
            Console.SetCursorPosition(0, 2);
            UserManager userManager = new UserManager();

            Console.WriteLine("Enter your username:");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Enter your password:");

            Console.SetCursorPosition(0, 3);
            string username = Console.ReadLine();

            Console.SetCursorPosition(0, 6);
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
            while (true)
            {
                Console.Clear();
                Console.SetCursorPosition(38, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to the Crossword Solver & Builder!");
                Console.WriteLine();
                Console.ResetColor();

                //Console.WriteLine("Would you like to: \n(L) Login  \n(C) Create an account (C) ?");

                Menu menu1 = new Menu("Please select an option below to continue:");
                menu1.AddMenuItem("(L) Login");
                menu1.AddMenuItem("(A) Create Account");

                MenuManager menuManager = new MenuManager(new List<Menu> { menu1 });
                string selectedOption = menuManager.RunMenu();

                switch (selectedOption)
                {
                    case "(L) Login":
                        Console.Clear();
                        VerifyUser();
                        break;

                    case "(A) Create Account":
                        Console.Clear();
                        CreateUser();
                        break;

                    default:
                        break;
                }
            }
           
            
        }

        //method to get user input for crossword dimensions and title
        public static Crossword SetCrosswordDimensions()
        {
            Console.SetCursorPosition(50, 2);
            Console.WriteLine("CREATE CROSSWORD:");
            Console.WriteLine();

            Console.SetCursorPosition(0, 4);
            Console.WriteLine("Title: ");

            Console.SetCursorPosition(0, 7);
            Console.WriteLine("Number of Rows: ");

            Console.SetCursorPosition(0, 10);
            Console.WriteLine("Number of Columns: ");

            Console.SetCursorPosition(0, 5);
            string title = Console.ReadLine();

            Console.SetCursorPosition(0, 8);
            int rows = Convert.ToInt32(Console.ReadLine());

            Console.SetCursorPosition(0, 11);
            int columns = Convert.ToInt32(Console.ReadLine());


            //creates a new crossword object with the user input as parameters
            Crossword crossword = new Crossword(rows, columns, title);

            //calls the display method from crossword.cs to show the crossword on screen
            crossword.DisplayCrossword();

            return crossword;
                
        
        }

        public static void DisplayAccountCreation() 
        {
            Console.Clear();
        
        
        }

        //method to display the welocome message
        public static void DisplayWelcomeMessage() 
        {
            Console.SetCursorPosition(38, 0);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to the Crossword Solver & Builder!");
            Console.ResetColor();

            Console.SetCursorPosition(20,28);
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("Created by Misbah Ahmad, a student at SHU for the module Programming Fundamentals");
            Console.ResetColor();
        
        }

        public static void DisplayCrosswordSolver() 
        {
            DisplayWelcomeMessage();
            Console.SetCursorPosition(0, 3);
            //need to get the list of saved crosswords, and add them all to a menu
            //then take the keypressed and use that to open the corresponding crossword from memory
            List<Crossword> savedCrosswords = new List<Crossword>();
            CrosswordManager crosswordManager = new CrosswordManager();

            savedCrosswords = crosswordManager.GetStoredCrosswords();
            Menu crosswordMenu = new Menu("Select a crossword to solve!");

            


            foreach (Crossword crossword in savedCrosswords)
            {
                crosswordMenu.AddMenuItem(crossword.CrosswordTitle);
            
            }
            MenuManager menuManager = new MenuManager(new List<Menu> { crosswordMenu });
            string selectedOption = menuManager.RunMenu();


        }


    }

    
}
