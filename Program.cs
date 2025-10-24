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
            crossword.DisplayCrossword();
            Console.ReadLine();


        }
    }
}
