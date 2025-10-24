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
        private int columns;
        private char[,] grid;
        public Crossword(int rows, int columns)
        {
            grid = new char[rows, columns]; 
        }
    }
}
