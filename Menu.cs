using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    //class that represents any given menu
    internal class Menu
    {
        //attributes
        //a list of menu items
        private List<MenuItem> _items;
        //name of the menu
        private string _name;
        //a bool to represent if the menu selected is active or not
        private bool _active;
        //a pointer to the active/current menu
        private int _activeItemPointer;

        private bool _isExpanded = false;

        //getters and setters
        public string Name { get => _name; set => _name = value; }
        public bool Active { get => _active; set => _active = value; }
        public bool IsExpanded { get => _isExpanded; set => _isExpanded = value; }

        //constructor
        public Menu(string name)
        {
            _name = name;
            //by default, the menu's status is set to not active (i.e not selected)
            _active = false;
            _items = new List<MenuItem>();
            _activeItemPointer = 0;
        }

        //method to display the menu on the console
        //takes parameters x and y for the start position
        public void DisplayMenu(int x, int y)
        {
            //moves the cursor to the starting point to draw the menu from
            Console.SetCursorPosition(x, y);

            //buffer variable to help evenly space out the menu on the console
            int buffer = 1;
            if (_isExpanded)
            {
                //loops through the list of menu items 
                foreach (MenuItem item in _items)
                {
                    //moves the cursor down by one row for the next menu item
                    Console.SetCursorPosition(x, y + buffer);
                    //if the menu item is the active one (the one selected)
                    if (item.Active == true)
                    {
                        //change the colour of the writing the men name is written in
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine(item.Name);
                        Console.ResetColor();
                    }
                    //if it is not the active menu, just print it on the console
                    else { Console.WriteLine(item.Name); }
                    //increment the buffer so that it moves to the next line for the next menu item
                    buffer++;
                }
            }

        }

        //method to add a new menu item to the list of menu items
        public void AddMenuItem(string item)
        {
            _items.Add(new MenuItem(item));
        }

        //Sets the current menu item selected to 'active'
        public void ActivateCurrentMenuItem()
        {
            _items[_activeItemPointer].Activate();
        }

        //Sets the current menu item to 'inactive' / not active
        public void DeactivateCurrentMenuItem()
        {
            _items[_activeItemPointer].Deactivate();
        }

        //moves the menu selection up by one menu item
        //if the selection i salready at the top, it then moves to the menu item at the bottom of thel ist
        public void MenuItemUp()
        {
            //deactivates the current menu item
            DeactivateCurrentMenuItem();

            //if the current menu item's pointer is 0 (it is the first one in the list)
            if (_activeItemPointer == 0)
            {
                //change the active item pointer to the last item in the list
                _activeItemPointer = _items.Count() - 1;
            }
            //else, increment the pointer to the next menu item in the list
            else { _activeItemPointer--; }

            //atcivates the new current menu
            ActivateCurrentMenuItem();
        }

        //moves the menu selection down by one menu item
        //if the selection is already at the bottom, it then moves to the menu item at the top of the list
        public void MenuItemDown()
        {
            //deactivates current menu item
            DeactivateCurrentMenuItem();

            //if the active item pointer points to the last item in the list
            if (_activeItemPointer == _items.Count - 1)
            {
                //chnage the pointer to point to the first item in the list
                _activeItemPointer = 0;
            }
            //else decrement the pointer value
            else { _activeItemPointer++; }

            //activates the new current menu
            ActivateCurrentMenuItem();
        }

        //method to deactivate all menu items
        public void DeactivateAllItems()
        {
            //loops through the list of menu items
            foreach (MenuItem item in _items)
            {
                //sets their 'Active' property to false
                item.Active = false;
            }
        }

        //returns the name of the currently selected menu item
        public string GetActiveChoice()
        {
            //active item is th eone pointe dto by the active item pointer
            //returns the name attribute of the menu item
            return _items[_activeItemPointer].Name;
        
        }

        public string GetFirstMenuItem()
        {
            return _items[0].Name;
        
        }
    }
}
