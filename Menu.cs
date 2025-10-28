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
        private List<MenuItem> _items;
        private string _name;
        private bool _active;
        private int _activeItemPointer;

        public string Name { get => _name; set => _name = value; }
        public bool Active { get => _active; set => _active = value; }

        public Menu(string name)
        {
            _name = name;
            _active = false;
            _items = new List<MenuItem>();
            _activeItemPointer = 0;
        }
    }
}
