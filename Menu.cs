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

        public string Name { get => _name; set => _name = value; }
        public bool Active { get => _active; set => _active = value; }

        //constructor
        public Menu(string name)
        {
            _name = name;
            _active = false;
            _items = new List<MenuItem>();
            _activeItemPointer = 0;
        }

        //method to display the menu items
        public void DisplayMenu()
        {
            //loops through the list of menu items 
            foreach (MenuItem item in _items)
            {
                //if the menu item is the active one (the one selected)
                if (item.Active == true)
                {
                    //print it on screen with 'active' to let us know that is the one selected
                    Console.WriteLine(item.Name + "ACTIVE");
                }
                //if it is not the active menu, just print it on the console
                else { Console.WriteLine(item.Name); }

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

        public string GetActiveChoice()
        {
            return _items[_activeItemPointer].Name;
        
        }
    }
}
