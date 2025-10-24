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

        //constructor for crossword class
        public Crossword(int rows, int columns)
        {
            //initialising attributes
            _rows = rows;
            _columns = columns;
            _grid = new char[rows, columns];

            //initially setting all characters in the grid to be '*'
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    _grid[i, j] = '*';
                }


            }
        }

        //method to display the crossword on the console
        public void DisplayCrossword() 
        {
            //loops through the rows and columns of the crossword grid
            for (int i = 0; i < _rows; i++) 
            {
                for (int j = 0; j < _columns; j++) 
                {
                    Console.Write(_grid[i, j] + " ");
                }
                Console.WriteLine();
            }
        
        }

        //method to display a word on the crossword grid
        public void DisplayWord(string word, string direction, int startRow, int startColumn) 
        {
            //formats the direction and the word given by the user for extra validation
            direction = direction.ToLower().Trim();
            word = word.ToUpper().Trim();

            //if they choose the word to be going across
            if (direction == "across")
            {
                for (int i = 0; i < word.Length; i++)
                {
                    //loops through the gris and changes the relevant indexes to the letters of the word
                    _grid[startRow, startColumn + i] = word[i];

                }

            }
            //if they choose the direction to be down
            else if (direction == "down")
            {
                //loops through the gris and changes the relevant indexes to the letters of the word
                for (int i = 0; i < word.Length; i++)
                {
                    _grid[startRow = +i, startColumn] = word[i];

                }

            }
            else 
            {
                Console.WriteLine("Invalid entry");
            
            }

        
        
        
        }
    }
}
