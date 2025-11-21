using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    //class that represents each option in the menu (menu item)
    internal class MenuItem
    {
        //name attribute
        private string _name;
        //if the men item is active (is it the one being selected at the moment)
        private bool _active;

        //constructor
        public MenuItem(string name)
        {
            _name = name;
            _active = false;
        }

        //getters and setters
        public string Name { get => _name; set => _name = value; }
        public bool Active { get => _active; set => _active = value; }

        //method to change active to true
        public void Activate() 
        {
            _active = true;
        }

        //method to chnage active to false
        public void Deactivate() 
        {
            _active = false;
        }
    }
}
