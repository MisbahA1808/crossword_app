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

        //constructor
        public MenuManager(List<Menu> menus)
        {
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
                    //if there is only one menu on the console
                    if (_menus.Count == 1) 
                    {
                        //calls menu item up method within the menu class
                        _activeMenu.MenuItemUp();
                        break; 
                    }
                    //if the drop down menu is expanded and the active menu item is the first item in the list (i.e. you are at the top of the menu)
                    if (_activeMenu.IsExpanded && _activeMenu.GetActiveChoice() == _activeMenu.GetFirstMenuItem())
                    {
                        //set isexpanded to false
                        _activeMenu.IsExpanded = false;
                        //deactivate all menu items
                        _activeMenu.DeactivateAllItems();
                    }
                    else
                    {
                        //calls menu item up method within the menu class
                        _activeMenu.MenuItemUp();
                    }
                    break;

                //if the down key is pressed
                case ConsoleKey.DownArrow:
                    if (_menus.Count == 1)
                    {
                        //calls menu item down method within the menu class
                        _activeMenu.MenuItemDown();
                        break; 
                    }
                    //if the menu is not expanded
                    if (!_activeMenu.IsExpanded)
                    {
                        //set isexpanded to true
                        _activeMenu.IsExpanded = true;
                        //activate the current menu item
                        _activeMenu.ActivateCurrentMenuItem();
                    }
                    else
                    {
                        //calls menu item down method within the menu class
                        _activeMenu.MenuItemDown();
                    }
                    break;

                //if the enter key is pressed (indicating they want tro select the menu)
                case ConsoleKey.Enter:
                    //get the menu's name and return it
                    return _activeMenu.GetActiveChoice();
                    
                //default takes into account letters not just arrow keys as above
                default:
                    //makes the first menu headers drop down based on the associated numbrs pressed
                    if (char.IsDigit((char)keyPressed)) 
                    {
                        int menuNumber = (int)keyPressed - (int)ConsoleKey.D1;

                        //maps the number pressed to the menu
                        if (menuNumber >= 0 && menuNumber < _menus.Count) 
                        {
                            _activeMenu.DeactivateAllItems();
                            _activeMenu.Active = false;
                            _activeMenuPointer = menuNumber;
                            _activeMenu = _menus[_activeMenuPointer];
                            _activeMenu.Active = true;
                            _activeMenu.IsExpanded = true;
                            _activeMenu.ActivateCurrentMenuItem();
                        }
                        break;
                    }

                    //if the menu is expanded, the other keys can then be used to get to other pages
                    if (_activeMenu.IsExpanded)
                    {
                        if (keyPressed == ConsoleKey.L) 
                        { return "(L) Login"; }
                        if (keyPressed == ConsoleKey.C)
                        { return "(C) Create Crossword"; }
                        if (keyPressed == ConsoleKey.S)
                        { return "(S) Solve Crossword"; }
                        if (keyPressed == ConsoleKey.A)
                        { return "(A) Create Account"; }
                        if (keyPressed == ConsoleKey.Q)
                        { return "(Q) Logout"; }
                        if (keyPressed == ConsoleKey.R)
                        { return "(R) Change Role"; }
                    }
                    break;
            }
            //return the selected menu item
            return _activeMenu.GetActiveChoice();
        }

        //method to display the menu on the console
        public void DisplayMenu() 
        {
            //some set values to set the cursor position correctly before printing the menus on screen
            int startCol = 0;
            int startRow = 3;
            int spacing = 25;

            //if there is only one menu on the console
            if (_menus.Count == 1) 
            {
                //make sure that the men uis expanded 
                _menus[0].IsExpanded = true;
            }

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

                //welcome message printing and formatting
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
                
            }
        
        }
    }
}
