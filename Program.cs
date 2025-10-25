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
            MenuManager menuManager = new MenuManager();
            menuManager.DisplayMenu();



            //Crossword crossword = new Crossword(8,8,"Crossword #1");
            //CrosswordManager crosswordManager = new CrosswordManager(crossword);

            //crosswordManager.AddWord("Hello", "across", 0, 0);
            //crosswordManager.AddWord("Goodbye", "down", 0, 7);

            //crossword.DisplayCrossword();
            //crosswordManager.StoreCurrentCrossword();





            
            
        }
    }
}
