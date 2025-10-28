using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class MenuItem
    {
        private string _name;
        private bool _active;

        public MenuItem(string name)
        {
            _name = name;
            _active = false;
        }

    }
}
