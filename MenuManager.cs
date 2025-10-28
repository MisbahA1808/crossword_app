using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class MenuManager
    {
        string _header;
        string _footer;
        int _windowWidth;
        private List<Menu> _menus;
        private int _activeMenuPointer;
        private Menu _activeMenu;

        public MenuManager(List<Menu> menus)
        {
            Console.SetWindowSize(100, 30);
            Console.SetBufferSize(100, 30);
            _header = "Crossword Solver & Builder";
            _footer = "Created by Misbah Ahmad";

            _windowWidth = Console.WindowWidth;
            int leftPadding = (_windowWidth - _header.Length) / 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);

            _menus = menus;
            _activeMenuPointer = 0;
            _activeMenu = menus[_activeMenuPointer];

            _activeMenu.Active = true;
            _activeMenu.ActivateCurrentMenuItem();


        }

        public string UpdateMenu(ConsoleKey keyPressed)
        {
            switch (keyPressed) {
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

                case ConsoleKey.UpArrow:
                    _activeMenu.MenuItemUp();
                    break;

                case ConsoleKey.DownArrow:
                    _activeMenu.MenuItemDown();
                    break;

                default:
                    break;
            }
            return _activeMenu.GetActiveChoice();
        
        
        }

        public void DisplayMenu() 
        {
            //Console.WriteLine(_header);
            //Console.WriteLine();
            //Console.Write("Create\t");
            //Console.Write("\tSolve Crossword\t");
            //Console.Write("\tLogin\t");
            //Console.Write("\tExit\t\t");

            foreach (var menu in _menus)
            {
                if (menu.Active == true) 
                { 
                    Console.WriteLine(menu.Name +"ACTIVE");
                    menu.DisplayMenu();
                    Console.WriteLine();
                }
                else 
                {
                    Console.WriteLine(menu.Name);
                    menu.DisplayMenu();
                    Console.WriteLine();
                
                }
            }    

        }

        //method for menu functionality
        public void UseMenu() 
        {
            //DisplayMenu();
            ConsoleKey keyPressed;
            //keyPressed = Console.ReadKey(true).Key;
            

            do
            {
                Console.Clear();

               




                DisplayMenu();
                keyPressed = Console.ReadKey(true).Key;

                switch (keyPressed)
                {
                    case ConsoleKey.D1:
                    
                        Console.WriteLine("You have chosen to create a crossword!");
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.D2:
                        Console.WriteLine("You have chosen to solve a crossword!");
                        Console.ReadKey(true);
                        break;


                    case ConsoleKey.D3:
                        Console.WriteLine("You have chosen to login!");
                        Console.ReadKey(true);
                        break;

                    
                    default:
                        break;
                }


            } while (keyPressed != ConsoleKey.D4);
        
        
        
        
        
        }


    }
}
