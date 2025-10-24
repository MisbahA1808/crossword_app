using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Crossword crossword = new Crossword(10, 10);
            CrosswordManager crosswordManager = new CrosswordManager(crossword);

            crosswordManager.AddWord("Hello", "across", 0, 0);
            crosswordManager.AddWord("Goodbye", "down", 0, 9);

            crossword.DisplayCrossword();
            Console.ReadLine();


        }
    }
}
