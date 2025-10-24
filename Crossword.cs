using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class Crossword
    {
        private int _rows;
        private int _columns;
        private char[,] _grid;
        private string _crosswordTitle;
        public Crossword(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _grid = new char[rows, columns];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _grid[i, j] = '*';
                }


            }
        }


        public void DisplayCrossword() 
        {
            for (int i = 0; i < _rows; i++) 
            {
                for (int j = 0; j < _columns; j++) 
                {
                    Console.Write(_grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        
        }

        public void DisplayWord(string word, string direction, int startRow, int startColumn) 
        {
            direction.ToLower().Trim();

            if (direction == "across")
            {
                for (int i = 0; i < word.Length; i++)
                {
                    _grid[startRow + 1, startColumn] = word[i];

                }

            }
            else if (direction == "down") 
            {

                for (int i = 0; i < word.Length; i++)
                {
                    _grid[startRow + 1, startColumn] = word[i];

                }

            }

        
        
        
        }
    }
}
