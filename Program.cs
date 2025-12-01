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
        //creating a global user manager object
        //global because if i keep creating instances, the account state of the user logged in would not be consistsent across the program
        public static UserManager UserManager = new UserManager();
        static void Main(string[] args)
        {
            //creates a default admin account using username 'admin' and password 'password' so that a default admin can always login
            Program.UserManager.CreateDefaultAdminAccount();

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
            Console.SetCursorPosition(35, 27);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Use arrow keys to navigate OR keyboard shortcuts!");
            Console.ResetColor();
            Console.SetCursorPosition(0, 2);

            int state = Program.UserManager.LoginState;

            //creating the menu objects and adding sub menus/ menu items to them
            Menu menu1 = new Menu("(1) MY ACCOUNT");
            menu1.AddMenuItem("(L) Login");
            menu1.AddMenuItem("(A) Create Account");
            menu1.AddMenuItem("(R) Change Role");

            Menu menu2 = new Menu(" (2) CROSSWORDS");
            menu2.AddMenuItem("(C) Create Crossword");
            menu2.AddMenuItem("(S) Solve Crossword");
            
            Menu menu3 = new Menu("(3) SETTINGS");
            menu3.AddMenuItem("(Q) Logout");
            
            //creating a menu manager object
            MenuManager menuManager = new MenuManager(new List<Menu> { menu1, menu2, menu3});
                        
            //gets the name of the menu item that has been selected
            string choice = menuManager.RunMenu();

            switch (choice)
            {
                //if the choice is Q/Logout
                case "(Q) Logout":
                    //if the users login state is -1 (not logged in)
                    if (Program.UserManager.LoginState == -1)
                    {
                        Console.SetCursorPosition(0, 10);

                        Console.WriteLine("No user is logged in!");
                        Console.ReadKey(true);
                        //display the menu
                        DisplayMenu();
                        return;

                    }

                    //if they are logged in
                    //logs them out of their account
                    Program.UserManager.Logout();
                    Console.SetCursorPosition(0, 10);

                    //display success message
                    Console.WriteLine("You have successfully been logged out, press any key to return to the login page");
                    Console.ReadKey(true);
                    //go back to the login menu upon any key pressed
                    DisplayLogin();
                    break;

                //if the choice is C/Create Crossword
                case "(C) Create Crossword":
                    //if the login state is not 0, n(ot admin)
                    if (Program.UserManager.LoginState != 0) 
                    {
                        Console.SetCursorPosition(0, 10);
                        //inform them that only admins can create crosswords
                        Console.WriteLine("Only admins can access this feature!");

                        //go back to the menu upon any key pressed
                        Console.ReadKey(true);
                        DisplayMenu();
                        return;
                    
                    }

                    //if they are an admin logged in
                    Console.Clear();
                    DisplayWelcomeMessage();

                    //create a crossword manager object
                    //set crossword dimensions returns type crossword
                    CrosswordManager crosswordManager = new CrosswordManager(SetCrosswordDimensions());

                    //calls the methos to allow the creation of the crossword
                    crosswordManager.AdminCreateCrossword();
                    
                    break;

                //if the choice is S/Solve Crossword
                case "(S) Solve Crossword":
                    //if the use ris not logged in
                    if (Program.UserManager.LoginState == -1)
                    {
                        Console.SetCursorPosition(0, 10);
                        //prompt them to login
                        Console.WriteLine("Please login beofre trying to access this feature!");
                        //return to the menu upon any key pressed
                        Console.ReadKey(true);
                        DisplayMenu();
                        return;

                    }
                    Console.Clear();
                    //display the crossword solver page
                    Crossword selectedCrossword = DisplayCrosswordSolver();
                    CrosswordManager crosswordSolveManager = new CrosswordManager();
                    crosswordSolveManager.PlayerSolveCrossword(selectedCrossword);
                    break;

                //if the choice is A/Create account
                case "(A) Create Account":
                    Console.Clear();
                    //display the account creation page
                    DisplayAccountCreation();
                    break;

                //if the choice is L/Login
                case "(L) Login":
                    //if the user is already logged in (their loginstate is not -1)
                    if (Program.UserManager.LoginState != -1)
                    {
                        Console.SetCursorPosition(0, 10);
                        //inform them that they are already logged in
                        Console.WriteLine("You are already logged in!");
                        //return to the menu upon any key pressed
                        Console.ReadKey(true);
                        DisplayMenu();
                        return;

                    }
                    //if they aren't logged in, go to the login page
                    VerifyUser();
                    break;

                //if the choice is R/Change Role
                case "(R) Change Role":
                    //if the user is not an admin
                    if (Program.UserManager.LoginState != 0)
                    {
                        Console.SetCursorPosition(0, 10);
                        //inform them that only admins can change roles
                        Console.WriteLine("Only admins can access this feature!");
                        Console.ReadKey(true);
                        DisplayMenu();

                    }
                    //however if they are an admin
                    Console.Clear();
                    //need to get user role, verify it then put an appropriate message here based on that
                    Console.WriteLine("This feature is still under development, press any key to return to the dashboard.");
                    Console.ReadKey(true);
                    DisplayMenu();
                    break;

                //default displays the menu
                default:
                    Console.ReadKey(true);
                    DisplayWelcomeMessage();
                    Console.SetCursorPosition(20, 27);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Use arrow keys to navigate OR keyboard shortcuts!");
                    Console.ResetColor();
                    Console.SetCursorPosition(0, 2);

                    DisplayMenu();
                    break;
            }

            

        }

        //method to create a new user
        public static void CreateUser() 
        {
            //create a usermanager object
            UserManager userManager = Program.UserManager;
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

            //gets input of their username and password
            Console.WriteLine("Enter your username:");
            Console.SetCursorPosition(0, 5);
            Console.WriteLine("Enter your password:");

            Console.SetCursorPosition(0, 3);
            string username = Console.ReadLine();

            Console.SetCursorPosition(0, 6);
            string password = Console.ReadLine();

            //stores true/false in isverified variable based on if their username and password match the ones in the json file
            bool isVerified =Program.UserManager.VerifyUser(username, password);

            //if true
            if (isVerified)
            {
                Console.WriteLine();
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

        //method to display the account creation page
        public static void DisplayAccountCreation() 
        {
            Console.Clear();
            CreateUser();
        }

        //method to display the welocome message
        //avoids repeated code
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
        public static Crossword DisplayCrosswordSolver() 
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


            //get user input
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Available Crosswords:");
            Console.ResetColor();
            Console.WriteLine();

            //loops through the list of saved crosswords and prints their titles on screen
            for (int i = 0; i < savedCrosswords.Count; i++) 
            {
                Console.WriteLine(i+ ". " + savedCrosswords[i].CrosswordTitle);
            }

            //gets user input
            Console.WriteLine();
            Console.WriteLine("Enter the number of the crossword you choose:");
            //stores their choice


            int choice = Convert.ToInt32(Console.ReadLine());

            

            
            return savedCrosswords[choice];

        }


    }

    
}
