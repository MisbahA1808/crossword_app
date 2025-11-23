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
        //bool to check if the file is loaded
        private static bool _isFileLoaded;

        static void Main(string[] args)
        {
            //Crossword crossword = new Crossword(5, 5, "test");
            //CrosswordManager crosswordManager = new CrosswordManager(crossword);
            //crosswordManager.StoreCurrentCrossword();

            //needs further updating
            

            //if the file of user data exists (i.e if the program can access it and therefore load it)
            if (File.Exists("users.json"))
            {
                //display the login page
                DisplayLogin();

            }
            else 
            {
                //else inform the user and prompt them to press enter to continue
                Console.WriteLine("File NOT loaded, use 'admin' and 'password to login. Press ENTER to continue.");
                Console.ReadKey(true);
                DisplayLogin();
            }

            
        }

        //method to display the main menus
        public static void DisplayMenu() 
        {
            //clear the console
            Console.Clear();
            //dipplay the welcome message
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
                        
            //gets the name of the menu item that has been selected
            string choice = menuManager.RunMenu();

            switch (choice)
            {
                //if the choice is Q/Logout
                case "(Q) Logout":
                    //go back to the login menu
                    DisplayLogin();
                    break;

                //if the choice is C/Create Crossword
                case "(C) Create Crossword":
                    Console.Clear();
                    DisplayWelcomeMessage();

                    //create a crossword manager object
                    //set crossword dimensions returns type crossword
                    CrosswordManager crosswordManager = new CrosswordManager(SetCrosswordDimensions());

                    //calls the methos to allow the creation of the crossword
                    crosswordManager.AdminCreateCrossword();
                    //method adds the created crossword to the list of crosswords
                    crosswordManager.AddCrosswordToList();
                    //then stores it in the json file
                    crosswordManager.StoreCurrentCrossword();
                    break;

                //if the choice is S/Solve Crossword
                case "(S) Solve Crossword":
                    Console.Clear();
                    //display the crossword solver page
                    DisplayCrosswordSolver();
                    break;

                //if the choice is A/Create account
                case "(A) Create Account":
                    Console.Clear();
                    //display the account creation page
                    DisplayAccountCreation();
                    break;

                //if the choice is L/Login
                case "(L) Login":
                    //(for now) informs the user that they are already logged in
                    Console.WriteLine("You are already Logged In!");
                    break;

                //if the choice is R/Change Role
                case "(R) Change Role":
                    Console.Clear();
                    //need to get user role, verify it then put an appropriate message here based on that
                    Console.WriteLine("Change User Role:");
                    break;

                //default displays the menu
                default:
                    Console.ReadKey(true);
                    DisplayWelcomeMessage();
                    Console.SetCursorPosition(0, 2);

                    DisplayMenu();
                    break;
            }

            

        }

        //method to create a new user
        public static void CreateUser() 
        {
            //create a usermanager object
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

            //prompts the user to return to the login page
            Console.WriteLine("Account created successfully! Press Enter to return to the Login Page.");
            keyPressed = Console.ReadKey(true).Key;

            Console.ReadKey(true);
            DisplayLogin();
           

        }

        //method to verify a user that has already created an account

        public static void VerifyUser() 
        {
            Console.Clear();
            DisplayWelcomeMessage();
            Console.SetCursorPosition(0, 2);
            UserManager userManager = new UserManager();

            //gets input of their username and password
            Console.WriteLine("Enter your username:");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Enter your password:");

            Console.SetCursorPosition(0, 3);
            string username = Console.ReadLine();

            Console.SetCursorPosition(0, 6);
            string password = Console.ReadLine();

            //stores true/false in isverified variable based on if their username and password match the ones in the json file
            bool isVerified = userManager.VerifyUser(username, password);

            //if true
            if (isVerified)
            {
                Console.WriteLine("Press any key to continue");
                Console.ReadKey(true);
                //displays the menu
                DisplayMenu();
            }
            //if false
            else 
            {
                //prompts them to enter login details again
                Console.WriteLine("Invalid login details, press any key to retry.");
                Console.ReadKey(true);

                //recursion to call the same method
                VerifyUser();
            }


        }

        //method to display the login menu
        public static void DisplayLogin() 
        {
            //indefinite loop
            while (true)
            {
                //displays welcome message
                Console.Clear();
                Console.SetCursorPosition(38, 0);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to the Crossword Solver & Builder!");
                Console.WriteLine();
                Console.ResetColor();

                //Console.WriteLine("Would you like to: \n(L) Login  \n(C) Create an account (C) ?");

                //creates a new menu and adds menu items to it
                Menu menu1 = new Menu("Please select an option below to continue:");
                menu1.AddMenuItem("(L) Login");
                menu1.AddMenuItem("(A) Create Account");

                //creates a menu manager object and passes the list of menus into it
                MenuManager menuManager = new MenuManager(new List<Menu> { menu1 });

                //gets the name of the selected menu option
                string selectedOption = menuManager.RunMenu();

                switch (selectedOption)
                {
                    //if the option is L/Login
                    case "(L) Login":
                        Console.Clear();
                        //call the method to allow the user to login
                        VerifyUser();
                        break;

                    //if the option is A/Create account
                    case "(A) Create Account":
                        Console.Clear();
                        //call the create user method
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

            //gets user input for title, number of rows, columns
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

        //method to display the crossword solving page
        public static void DisplayCrosswordSolver() 
        {
            //displays the welcome message
            DisplayWelcomeMessage();
            Console.SetCursorPosition(0, 3);

            //create a new list to store all the crosswords that are in the json file
            List<Crossword> savedCrosswords = new List<Crossword>();
            //create a crossword manager object
            CrosswordManager crosswordManager = new CrosswordManager();

            //fill the list with the crosswords stored in the json
            savedCrosswords = crosswordManager.GetStoredCrosswords();

            

            Console.WriteLine("Write the number of the crossword you want to solve");
            Console.WriteLine();

            for (int i = 0; i < savedCrosswords.Count; i++) 
            {
                Console.WriteLine(i+ ". " + savedCrosswords[i].CrosswordTitle);
            }

            Console.WriteLine("Enter a number:");
            int choice = Convert.ToInt32(Console.ReadLine());

            Console.Clear();

            Console.WriteLine(savedCrosswords[choice].CrosswordTitle + ": ");
            Console.WriteLine();


            crosswordManager.DisplaySolvableCrossword(savedCrosswords[choice]);
            //savedCrosswords[choice].DisplayCrossword();
            Console.ReadKey(true);

            ConsoleKey keyPressed;
            keyPressed = Console.ReadKey(true).Key;

            if (keyPressed == ConsoleKey.Enter)
            {
                DisplayMenu();
            }
            

        }


    }

    
}
