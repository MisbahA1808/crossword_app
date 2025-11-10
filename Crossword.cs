using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CrosswordApp
{
    internal class Crossword
    {
        //attributes
        [JsonProperty] private int _rows;
        [JsonProperty] private int _columns;
        [JsonProperty] private char[,] _grid { get; set; }
        [JsonProperty] private string _crosswordTitle;
        public int Rows { get => _rows; set => _rows = value; }
        public int Columns { get => _columns; set => _columns = value; }
        public string CrosswordTitle { get => _crosswordTitle; set => _crosswordTitle = value; }


        //constructor for crossword class
        public Crossword(int rows, int columns, string title)
        {
            //initialising attributes
            _rows = rows;
            _columns = columns;
            _grid = new char[rows, columns];
            _crosswordTitle = title;

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
                //validation that the word will fit into the crossword
                if (word.Length <= (_grid.GetLength(1) - startColumn))
                {

                    for (int i = 0; i < word.Length; i++)
                    {
                        //loops through the gris and changes the relevant indexes to the letters of the word
                        _grid[startRow, startColumn + i] = word[i];

                    }
                    Console.WriteLine();
                    Console.WriteLine("Word Added Successfully!");
                }
                else { Console.WriteLine("Invalid Entry!"); }

            }
            //if they choose the direction to be down
            else if (direction == "down")
            {
                //validation that the word wil fit into the crossword
                if (word.Length <= (_grid.GetLength(0) - startRow))
                {

                    //loops through the gris and changes the relevant indexes to the letters of the word
                    for (int i = 0; i < word.Length; i++)
                    {
                        _grid[startRow + i, startColumn] = word[i];

                    }
                    Console.WriteLine();
                    Console.WriteLine("Word Added Successfully!");

                }
                else { Console.WriteLine("Invalid Entry!"); }

            }
            else 
            {
                Console.WriteLine("Invalid Entry!");
            
            }

        
        }

        //gets the current grid position/cell
        public char GetGridPosition(int row, int column) 
        {
            return _grid[row, column];
        
        }

        //sets the current grids position
        public void SetGridPosition(int row, int column, char value) 
        {
            _grid[row, column] = value;
        
        
        }

    }
}
