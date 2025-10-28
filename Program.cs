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
        //    MenuManager menuManager = new MenuManager();
        //    menuManager.UseMenu();



        //Crossword crossword = new Crossword(8,8,"Crossword #1");
        //CrosswordManager crosswordManager = new CrosswordManager(crossword);

        //crosswordManager.AddWord("Hello", "across", 0, 0);
        //crosswordManager.AddWord("Goodbye", "down", 0, 7);

        //crossword.DisplayCrossword();
        //crosswordManager.StoreCurrentCrossword();

            Menu menu1 = new Menu("Menu 1");
            menu1.AddMenuItem("option 1");

            menu1.AddMenuItem("option 2");
            Menu menu2 = new Menu("Menu 2");
            menu2.AddMenuItem("option 1");

            menu2.AddMenuItem("option 2");
            MenuManager menuManager = new MenuManager(new List<Menu> {menu1, menu2});

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
