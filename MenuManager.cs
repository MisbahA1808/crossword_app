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
            //initially set activemenupointer to zero
            _activeMenuPointer = 0;
            //set the active menu to the first menu in the list
            _activeMenu = menus[_activeMenuPointer];
            _activeMenu.Active = true;

            //activate the first item in the active menu
            _activeMenu.ActivateCurrentMenuItem();


        }

        //method to update the current menu based on the key pressed by the user
        // determines the menu item which is active
        public string UpdateMenu(ConsoleKey keyPressed)
        {
            //switch case statement to chck which  key is pressed
            switch (keyPressed) {
                //if the right key is pressed (moves to the next menu on the right)
                case ConsoleKey.RightArrow:
                    //deactivate all items in the currently active menu
                    _activeMenu.DeactivateAllItems();
                    _activeMenu.Active = false;

                    //if the current menu is the last menu (furthest right)
                    if (_activeMenuPointer == _menus.Count() - 1) 
                    {
                        //wrap back to the first menu
                        _activeMenuPointer = 0;
                    }
                    //otherwise the active menu is the next on the right
                    else { _activeMenuPointer++; }

                    //update the active menu based on the new pointer value
                    _activeMenu = _menus[_activeMenuPointer];
                    _activeMenu.Active = true;

                    //activate the first item in the new active menu
                    _activeMenu.ActivateCurrentMenuItem();
                    break;

                //if the left key is pressed (moves to the next menu on the left)
                case ConsoleKey.LeftArrow:
                    //deactivate all items in the currently active menu
                    _activeMenu.DeactivateAllItems();
                    _activeMenu.Active = false;

                    //if the current menu is the first menu (furthest left)
                    if (_activeMenuPointer == 0)
                    {
                        //wrap around to the last menu
                        _activeMenuPointer = _menus.Count() -1;
                    }
                    //otherwise the active menu is the next one on the left
                    else { _activeMenuPointer--; }

                    //update the active menu based on the new pointer value
                    _activeMenu = _menus[_activeMenuPointer];
                    _activeMenu.Active = true;

                    //activate the first item in the new active menu
                    _activeMenu.ActivateCurrentMenuItem();
                    break;

                //if the up key is pressed
                case ConsoleKey.UpArrow:
                    //calls menu item up method within the menu class
                    _activeMenu.MenuItemUp();
                    break;

                //if the down key is pressed
                case ConsoleKey.DownArrow:
                    //calls menu item down method within the menu class
                    _activeMenu.MenuItemDown();
                    break;

                //if the enter key is pressed (indicating they want tro select the menu)
                case ConsoleKey.Enter:
                    //get the menu's name and return it
                    return _activeMenu.GetActiveChoice();
                    
                //default takes into account letters not just arrow keys as above
                default:
                    switch (keyPressed)
                    {
                        //for each vlid letter pressed, returns the name of that menu item
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
            //return the selected menu item
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
                //calls the display menumethod from the menu class
                menu.DisplayMenu(startCol, startRow);
                //adds 25 to the startcolumn so that menus don't print on top of each other
                startCol += spacing;
            }    

        }

       

       
        //runs the main menu loop and also returns the menu choice chosen by the user
        public string RunMenu()
        {
            ConsoleKey keyPressed;

            //_activeMenu.DeactivateAllItems();
            //_activeMenuPointer = 0;
            //_activeMenu = _menus[0];
            //_activeMenu.ActivateCurrentMenuItem();

            //initially display the menu
            DisplayMenu();

            while (true) 
            {
                //read the key ressed by the user
                keyPressed = Console.ReadKey(true).Key;
                //updates the selected menu option based on what key the user presses
                string choice = UpdateMenu(keyPressed);

                //clear the console
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
                
                //redisplay the menu after updating it
                DisplayMenu();

                //if (_menus.Count == 1)
                //{
                //    if (keyPressed == ConsoleKey.Enter) { return choice; }

                //}
                //else
                //{
                    //if the user selects enter or one of the valid key choices
                    if (keyPressed == ConsoleKey.Enter ||
                        keyPressed == ConsoleKey.L ||
                        keyPressed == ConsoleKey.C ||
                        keyPressed == ConsoleKey.S ||
                        keyPressed == ConsoleKey.A ||
                        keyPressed == ConsoleKey.Q)
                    {
                        //returns the user's selection
                        return choice;
                    }
                //}
            
            
            }
        
        
        
        
        }

        


    }
}
