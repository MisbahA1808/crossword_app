using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CrosswordApp
{
    internal class CrosswordManager
    {
        //an instance of the current crossword
        private Crossword _currentCrossword;
        //list of all crosswords
        private List<Crossword> _crosswords = new List<Crossword>();

        //constructor
        public CrosswordManager(Crossword crossword)
        {
            _currentCrossword = crossword;
        }

        //method to add crossword to the list of crosswords
        public void AddCrosswordToList() 
        {
            _crosswords.Add(_currentCrossword);
        
        }
        //method for adding words to the current crossword 
        public void AddWord(string word, string direction, int startRow, int startColumn) 
        {
            _currentCrossword.DisplayWord(word, direction, startRow, startColumn);
        
        
        }

        //method to store current crossword data in crossword.json
        public void StoreCurrentCrossword() 
        {
            List<Crossword> crosswords;
            string path = "crossword.json";

            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);

                if (string.IsNullOrWhiteSpace(json))
                {
                    crosswords = new List<Crossword>();
                }
                else
                {
                    crosswords = JsonConvert.DeserializeObject<List<Crossword>>(json);
                    if (crosswords == null) 
                    {
                        crosswords = new List<Crossword>();
                    }
                }
            }
            else { crosswords = new List<Crossword>(); }

            crosswords.Add(_currentCrossword);

            string json1 = JsonConvert.SerializeObject(crosswords, Formatting.Indented);
            File.WriteAllText(path, json1);
        
        }

        //method for admins to enter words into the crossword
        public void AddInputWord(int startRow, int startColumn) 
        {
            //takes the word as user input
            Console.SetCursorPosition(0, 15);
            Console.WriteLine("Enter the word you would like to add:");

            //takes the direction as user input
            Console.SetCursorPosition(0,18);
            Console.WriteLine("Enter the direction you would like the word to go (across/down):");
            Console.SetCursorPosition(0,16);
            string word = Console.ReadLine();
            word = word.ToUpper().Trim();

            Console.SetCursorPosition(0,19);

            string direction = Console.ReadLine();
            direction = direction.ToLower().Trim();

            try
            {
                //displays the current word based on the input given
                _currentCrossword.DisplayWord(word, direction, startRow, startColumn);
            }
            catch (Exception)
            {
                //exception handling in case of error
                Console.WriteLine("Word could not be added, please try again.");
            }

            Console.WriteLine("Press a key to continue");
            Console.ReadKey(true);
        }

        //method for admin to draw the crossword (add words to it)
        public void AdminDrawCrossword(int selectedRow, int selectedColumn)
        {
            Console.WriteLine("Your current crossword: ");

            //loops through the rows
            for (int i = 0; i < _currentCrossword.Rows; i++)
            {
                //loops through the columns
                for (int j = 0; j < _currentCrossword.Columns; j++)
                {
                    //gets the current posotion on the grid
                    char position = _currentCrossword.GetGridPosition(i, j);
                    //if  and j are equal to te row and column sleected by the admin
                    if (i == selectedRow && j == selectedColumn)
                    {
                        //change the foreground colour to red
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write($"{position}");
                        Console.ResetColor();


                    }
                    else
                    {
                        Console.Write($"{position}");

                    }


                }
                Console.WriteLine();
            }


        }
        public void AdminCreateCrossword() 
        {

            //start at the top left of the crossword by setting the start row and column to zero
            int selectedRow = 0;
            int selectedColumn = 0;
            ConsoleKey keyPressed;

            //loop to display the crossword and its updated changes each time until the admin pressed the escape key
            do
            {
                //clears the console
                Console.Clear();
                Program.DisplayWelcomeMessage();
                Console.SetCursorPosition(0, 2);
                //draws the crossword
                AdminDrawCrossword(selectedRow, selectedColumn);
                Console.SetCursorPosition(40, 27);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Use arrow keys to move, Enter to select");
                Console.ResetColor();
                
                //gets the key pressed b ythe admin
                keyPressed = Console.ReadKey(true).Key;

                switch (keyPressed) 
                {
                    //if the key pressed is the up arroe
                    case ConsoleKey.UpArrow:
                        //if the row they have selected is > 0
                        if (selectedRow > 0) 
                        {
                            //decrement selectedRow
                            selectedRow--;
                        }
                        break;

                     //if the key pressed is the down arroow
                    case ConsoleKey.DownArrow:
                        //if the row selected is less than the amount of rowws in the crossword
                        if (selectedRow < _currentCrossword.Rows - 1)
                        {
                            //increment selectedRow
                            selectedRow++;
                        }
                        break;

                    //if the key pressed is the left arrow
                    case ConsoleKey.LeftArrow:
                        //if the selected column > 0
                        if (selectedColumn > 0)
                        {
                            //decrement selected column
                            selectedColumn--;
                        }
                        break;

                    //if the key pressed is the right arrow
                    case ConsoleKey.RightArrow:
                        //if the selected column is less than the amount of columns in the crossword
                        if (selectedColumn < _currentCrossword.Columns)
                        {
                            //increment selected column
                            selectedColumn++;
                        }
                        break;

                    //if the key pressed is the enter key (i.e. the admin wants to enter a word at this grid position)
                    case ConsoleKey.Enter:
                        //calls AddInputWord()
                        AddInputWord(selectedRow, selectedColumn);
                        break;



                }
            //if the key pressed is the escape key, the loop breaks
            } while (keyPressed != ConsoleKey.Escape);

            Console.WriteLine("crossword created!");
        
        }
    }
}
