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
        //these are json properties so that they can be serialised to then be stored in the json file
        [JsonProperty] private int _rows;
        [JsonProperty] private int _columns;
        [JsonProperty] private char[,] _grid;
        [JsonProperty] private string _crosswordTitle;
        [JsonProperty] private string _clue;
        [JsonProperty] private List<Word> _words;
        //getters and setters for the attirbutes above
        public int Rows { get => _rows; set => _rows = value; }
        public int Columns { get => _columns; set => _columns = value; }
        public string CrosswordTitle { get => _crosswordTitle; set => _crosswordTitle = value; }
        public string Clue { get => _clue; set => _clue = value; }
        internal List<Word> Words { get => _words; set => _words = value; }
        public char[,] Grid { get => _grid; set => _grid = value; }


        //constructor for crossword class
        public Crossword(int rows, int columns, string title)
        {
            //initialising attributes
            _rows = rows;
            _columns = columns;
            _grid = new char[rows, columns];
            _crosswordTitle = title;
            _words = new List<Word>();

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
                    //for each grid position, it displays it on screen, adds space so that it is formatted nicely
                    Console.Write(_grid[i, j] + " ");
                }
                //moves to the next row in the crossword
                Console.WriteLine();
            }
        
        }

        //method to display a word on the crossword grid
        public void DisplayWord(string wordInput, string direction, int startRow, int startColumn, string clue) 
        {
            //formats the direction and the word given by the user for extra validation
            direction = direction.ToLower().Trim();
            wordInput = wordInput.ToUpper().Trim();

            //creates a word object and stores the relevant word related data in it
            Word word = new Word(wordInput, direction, clue, startRow, startColumn);
            
            //if they choose the word to be going across
            if (direction == "across")
            {
                //validation that the word will fit into the crossword
                //grid.GetLength(1) gets the number of the columns
                if (wordInput.Length <= (_grid.GetLength(1) - startColumn))
                {

                    for (int i = 0; i < wordInput.Length; i++)
                    {
                        //loops through the gris and changes the relevant indexes to the letters of the word
                        _grid[startRow, startColumn + i] = wordInput[i];

                    }
                    Console.WriteLine();
                    Console.WriteLine("Word Added Successfully!");
                }
                //invalid entry as the word can not fit onto the crossword
                else { Console.WriteLine("Invalid Entry!"); }

            }
            //if they choose the direction to be down
            else if (direction == "down")
            {
                //validation that the word wil fit into the crossword
                //grid.GetLength(1) gets the number of the rows
                if (wordInput.Length <= (_grid.GetLength(0) - startRow))
                {

                    //loops through the gris and changes the relevant indexes to the letters of the word
                    for (int i = 0; i < wordInput.Length; i++)
                    {
                        _grid[startRow + i, startColumn] = wordInput[i];

                    }
                    Console.WriteLine();
                    Console.WriteLine("Word Added Successfully!");

                }
                //invalid entry as the word can not fit onto the crossword
                else { Console.WriteLine("Invalid Entry!"); }

            }
            else 
            {
                Console.WriteLine("Invalid Entry!");
            }
            //adds the input word to the list of words
            _words.Add(word);
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
