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

    }
}
