using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    //class to manage the display functionality of the menus
    internal class MenuManager
    {
        //attributes
        private string _header;
        private string _footer;
        private int _windowWidth;
        private List<Menu> _menus;
        private int _activeMenuPointer;
        private Menu _activeMenu;

        public MenuManager(List<Menu> menus)
        {
            //Console.SetWindowSize(100, 30);
            //Console.SetBufferSize(100, 30);
            //_header = "Crossword Solver & Builder";
            //_footer = "Created by Misbah Ahmad";

            //_windowWidth = Console.WindowWidth;
            //int leftPadding = (_windowWidth - _header.Length) / 2;
            //Console.SetCursorPosition(leftPadding, Console.CursorTop);

            _menus = menus;
            _activeMenuPointer = 0;
            _activeMenu = menus[_activeMenuPointer];

            _activeMenu.Active = true;
            _activeMenu.ActivateCurrentMenuItem();


        }

        //method to update the current menu based on the key pressed by the user
        public string UpdateMenu(ConsoleKey keyPressed)
        {
            //switch case statement to chck which arrow key is pressed
            switch (keyPressed) {
                //if the right key is pressed
                case ConsoleKey.RightArrow:
                    _activeMenu.DeactivateAllItems();
                    _activeMenu.Active = false;

                    if (_activeMenuPointer == _menus.Count() - 1) 
                    {
                        _activeMenuPointer = 0;
                    }

                    else { _activeMenuPointer++; }

                    _activeMenu = _menus[_activeMenuPointer];
                    _activeMenu.Active = true;

                    _activeMenu.ActivateCurrentMenuItem();
                    break;

                //if the left key is pressed
                case ConsoleKey.LeftArrow:
                    _activeMenu.DeactivateAllItems();
                    _activeMenu.Active = false;

                    if (_activeMenuPointer == 0)
                    {
                        _activeMenuPointer = _menus.Count() -1;
                    }

                    else { _activeMenuPointer--; }

                    _activeMenu = _menus[_activeMenuPointer];
                    _activeMenu.Active = true;

                    _activeMenu.ActivateCurrentMenuItem();
                    break;

                //if the up key is pressed
                case ConsoleKey.UpArrow:
                    _activeMenu.MenuItemUp();
                    break;

                //if the down key is pressed
                case ConsoleKey.DownArrow:
                    _activeMenu.MenuItemDown();
                    break;

                case ConsoleKey.Enter:
                    return _activeMenu.GetActiveChoice();
                    

                default:
                    switch (keyPressed)
                    {
                        case ConsoleKey.L:
                            return "(L) Login";
                        case ConsoleKey.C:
                            return "(C) Create Crossword";
                        case ConsoleKey.S:
                            return "(S) Solve Crossword";
                        case ConsoleKey.A:
                            return "(A) Create Account";
                        case ConsoleKey.Q:
                            return "(Q) Logout";
                        case ConsoleKey.R:
                            return "(R) Change Role";
                        default:
                            break;
                    }
                    break;
            }
            return _activeMenu.GetActiveChoice();
        
        
        }

        //method to display the menu on the console
        public void DisplayMenu() 
        {
            int startCol = 0;
            int startRow = 3;
            int spacing = 25;

            //loops through the list of menus
            foreach (Menu menu in _menus)
            {
                Console.SetCursorPosition(startCol, startRow - 1);
                //if the menu is active/selected
                if (menu.Active == true) 
                {
                    //write the menu name and change the text colour of the selected menu to cyan
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.Write(menu.Name);
                    //resets the console colour so that all the text on the console isn't the same colour
                    Console.ResetColor();
                    
                    //Console.WriteLine();
                }
                else 
                {
                    Console.Write(menu.Name);
                   
                    //Console.WriteLine();

                }

                menu.DisplayMenu(startCol, startRow);
                startCol += spacing;
            }    

        }

       

       

        public string RunMenu()
        {
            ConsoleKey keyPressed;
            DisplayMenu();

            while (true) 
            {
                keyPressed = Console.ReadKey(true).Key;
                string choice = UpdateMenu(keyPressed);

                Console.Clear();

                Console.SetCursorPosition(38, 0);

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Welcome to the Crossword Solver & Builder!");
                Console.ResetColor();

                Console.SetCursorPosition(20, 28);
                Console.ForegroundColor = ConsoleColor.Cyan;

                Console.WriteLine("Created by Misbah Ahmad, a student at SHU for the module Programming Fundamentals");
                Console.ResetColor();
                Console.SetCursorPosition(0, 2);
                
                DisplayMenu();


                if (keyPressed == ConsoleKey.Enter ||
                    keyPressed == ConsoleKey.L ||
                    keyPressed == ConsoleKey.C ||
                    keyPressed == ConsoleKey.S||
                    keyPressed == ConsoleKey.A ||
                    keyPressed == ConsoleKey.Q)
                {
                    return choice;
                }
            
            
            }
        
        
        
        
        }

        ////method for menu functionality
        //public void UseMenu() 
        //{
        //    //DisplayMenu();
        //    ConsoleKey keyPressed;
        //    //keyPressed = Console.ReadKey(true).Key;
            
        //    do
        //    {
        //        Console.Clear();

        //        DisplayMenu();
        //        keyPressed = Console.ReadKey(true).Key;

        //        switch (keyPressed)
        //        {
        //            case ConsoleKey.D1:
                    
        //                Console.WriteLine("You have chosen to create a crossword!");
        //                Console.ReadKey(true);
        //                break;

        //            case ConsoleKey.D2:
        //                Console.WriteLine("You have chosen to solve a crossword!");
        //                Console.ReadKey(true);
        //                break;


        //            case ConsoleKey.D3:
        //                Console.WriteLine("You have chosen to login!");
        //                Console.ReadKey(true);
        //                break;

                    
        //            default:
        //                break;
        //        }


        //    } while (keyPressed != ConsoleKey.D4);
        
        
        
        
        
        //}


    }
}
