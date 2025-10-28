using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using Newtonsoft.Json;

namespace CrosswordApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
        
            Menu menu1 = new Menu("My Account");
            menu1.AddMenuItem("Login");
            menu1.AddMenuItem("Create Account");
            menu1.AddMenuItem("Logout");

            Menu menu2 = new Menu("Menu 2");
            menu2.AddMenuItem("option 3");
            menu2.AddMenuItem("option 4");

            Menu menu3 = new Menu("Menu 2");
            menu3.AddMenuItem("option 5");
            menu3.AddMenuItem("option 6");

            Menu menu4 = new Menu("Menu 4");
            menu4.AddMenuItem("option 7");
            menu4.AddMenuItem("option 8");
            MenuManager menuManager = new MenuManager(new List<Menu> {menu1, menu2, menu3, menu4});

            ConsoleKey keyPressed;
            menuManager.DisplayMenu();

            while (true)
            {
                keyPressed = Console.ReadKey(true).Key;
                string choice = menuManager.UpdateMenu(keyPressed);
                Console.Clear();
                Console.WriteLine(choice);

                menuManager.DisplayMenu();

            }
            
        }


    }

    
}
