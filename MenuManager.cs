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

        public MenuManager()
        {
            Console.SetWindowSize(100, 30);
            Console.SetBufferSize(100, 30);
            _header = "Crossword Solver & Builder";
            _footer = "Created by Misbah Ahmad";

            _windowWidth = Console.WindowWidth;
            int leftPadding = (_windowWidth - _header.Length)/ 2;
            Console.SetCursorPosition(leftPadding, Console.CursorTop);

            
        }

        public void DisplayMenu() 
        {
            Console.WriteLine(_header);
            Console.Write("Create\t");
            Console.Write("\tSolve Crossword\t");
            Console.Write("\tLogin\t");
            Console.Write("\tExit\t\t");

        }

        public void UseMenu() 
        {
            DisplayMenu();

            do
            {
                DisplayMenu();
            } while (true);
        
        
        
        
        
        }


    }
}
