using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class Crossword
    {
        private int _rows;
        private int _columns;
        private char[,] grid;
        public Crossword(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            grid = new char[rows, columns];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    grid[i, j] = '*';
                }


            }
        }

        public void DisplayCrossword() 
        {
            for (int i = 0; i < _rows; i++) 
            {
                for (int j = 0; j < _columns; j++) 
                {
                    Console.Write(grid[i, j] + " ");
                }
                Console.WriteLine();
            
            }
        
        
        
        }
    }
}
