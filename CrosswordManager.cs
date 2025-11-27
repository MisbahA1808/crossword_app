using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    //class to manage all crossword related functions
    internal class CrosswordManager
    {
        //an instance of the current crossword
        private Crossword _currentCrossword;
        //list of all crosswords
        private List<Crossword> _crosswords = new List<Crossword>();

        //constructor
        public CrosswordManager(Crossword crossword)
        {
            //the curremt crossword is the one passed into the crosswordmanager object upon its creation
            _currentCrossword = crossword;
        }

        //secondary constructor with no parameters
        //constructor overloading?
        public CrosswordManager()
        {
            
        }
        

        //method to add crossword to the list of crosswords
        public void AddCrosswordToList() 
        {
            _crosswords.Add(_currentCrossword);
        
        }

        //method for adding words to the current crossword 
        public void AddWord(string word, string direction, int startRow, int startColumn, string clue) 
        {
            //calls displayword from the crossword class
            _currentCrossword.DisplayWord(word, direction, startRow, startColumn, clue);
        
        
        }

        //method to store current crossword data in crossword.json
        public void StoreCurrentCrossword() 
        {
            //creates a list of crosswords
            List<Crossword> crosswords;

            //variable for path to the json file
            string path = "crossword.json";

            //check if the json file exists
            if (File.Exists(path))
            {
                //reads the contents of the json file and stores in in a string
                string json = File.ReadAllText(path);

                //if the file is empty or contains white space only
                if (string.IsNullOrWhiteSpace(json))
                {
                    //initialise a list of crosswords (empty list)
                    crosswords = new List<Crossword>();
                }
                //if the file is not empty
                else
                {
                    //deserialise the json file into a list of crossword objects, which is stored in the predefined crossword list
                    crosswords = JsonConvert.DeserializeObject<List<Crossword>>(json);

                    //error handling for if the deserialisation returns an empty list 
                    if (crosswords == null) 
                    {
                        //initilaise a list of crosswords
                        crosswords = new List<Crossword>();
                    }
                }
            }
            //if the file doesn't exist, create a new list of crosswords
            else { crosswords = new List<Crossword>(); }

            //add the current crossword to the list
            crosswords.Add(_currentCrossword);

            //serialise the list of crossword objects
            string json1 = JsonConvert.SerializeObject(crosswords, Formatting.Indented);
            //write the updated list on to the file
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


            //gets the clue as user input
            Console.SetCursorPosition(0, 21);
            Console.WriteLine("Enter the clue for the word:");

            //set cursor position back to get input for word
            Console.SetCursorPosition(0,16);
            string word = Console.ReadLine();
            word = word.ToUpper().Trim();

            //set cursor position back to get input for direction
            Console.SetCursorPosition(0,19);
            string direction = Console.ReadLine();
            direction = direction.ToLower().Trim();

            //set cursor position back to get input for clue
            Console.SetCursorPosition(0,22);
            string clue = Console.ReadLine();

            //try catch block for error handling
            try
            {
                //displays the current word based on the input given
                _currentCrossword.DisplayWord(word, direction, startRow, startColumn,clue);
            }
            //if it could not display the word
            catch (Exception)
            {
                //exception handling in case of error
                Console.WriteLine("Word could not be added, please try again.");
            }

            Console.WriteLine("Press a key to continue");
            Console.ReadKey(true);
        }

        //method for admin to draw the crossword (add words to it)
        //deals with the admin's position on the crossword grid and making that position highlighted
        public void AdminDrawCrossword(int selectedRow, int selectedColumn)
        {
            //displays heading before the crossword grid
            Console.WriteLine("Your current crossword: ");
            Console.WriteLine();

            //loops through the rows
            for (int i = 0; i < _currentCrossword.Rows; i++)
            {
                //loops through the columns
                for (int j = 0; j < _currentCrossword.Columns; j++)
                {
                    //gets the letter or '*' that is stored at the grid position
                    char position = _currentCrossword.GetGridPosition(i, j);

                    //if  and j are equal to te row and column sleected by the admin
                    if (i == selectedRow && j == selectedColumn)
                    {
                        //change the foreground colour to red
                        Console.ForegroundColor = ConsoleColor.Red;
                        //print the selected char
                        Console.Write($"{position}" + " ");
                        //reset the console colour so that only that character has a different colour
                        Console.ResetColor();


                    }
                    else
                    {
                        //prints all other grid positions normally in white
                        Console.Write($"{position}" + " ");

                    }


                }
                //moves to the next row of the crossword
                Console.WriteLine();
            }


        }

        //method that allows admin to create crossword by moving around on the crossword grid using arrow keys
        public void AdminCreateCrossword() 
        {

            //start at the top left of the crossword by setting the start row and column to zero
            int selectedRow = 0;
            int selectedColumn = 0;
            //variable to store which key is pressed
            ConsoleKey keyPressed;

            //loop to display the crossword and its updated changes each time until the admin pressed the escape key
            do
            {
                //clears the console
                Console.Clear();
                //display the welcome message
                Program.DisplayWelcomeMessage();
                Console.SetCursorPosition(0, 2);

                //draws the crossword, starting with (0,0) highlighted/selected
                AdminDrawCrossword(selectedRow, selectedColumn);
                Console.SetCursorPosition(28, 27);
                Console.ForegroundColor = ConsoleColor.Yellow;
                //extra info for admin to navigate crossword creation
                Console.WriteLine("Use arrow keys to move, Enter to select, Esc to submit crossword");
                Console.ResetColor();
                
                //gets the key pressed b ythe admin
                keyPressed = Console.ReadKey(true).Key;

                //switch case block based on the key pressed
                switch (keyPressed) 
                {
                    //if the key pressed is the up arrow
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

            //if (keyPressed == ConsoleKey.Escape) 
            //{
            //    CrosswordManager crosswordManager = new CrosswordManager(_currentCrossword);
            //    crosswordManager.StoreCurrentCrossword();

            //}
            Console.SetCursorPosition(0, 16);
            //the crossword is created as the admin has pressed escape
            Console.WriteLine("Crossword Created Successfully!");

            //exits the method when a key is pressed and returns to the displaymenu functionality
            Console.ReadKey(true);
            //method adds the created crossword to the list of crosswords
            AddCrosswordToList();
            //then stores it in the json file
            StoreCurrentCrossword();
            Program.DisplayMenu();
            return;
        
        }

        public void PlayerSolveCrossword(Crossword crossword) 
        {
            //start at the top left of the crossword by setting the start row and column to zero
            int selectedRow = 0;
            int selectedColumn = 0;
            //variable to store which key is pressed
            ConsoleKey keyPressed;

            char[,] maskedCrossword = CreateSolvableCrossword(crossword);
            //loop to display the crossword and its updated changes each time until the admin pressed the escape key
            do
            {
                //clears the console
                Console.Clear();
                //display the welcome message
                Program.DisplayWelcomeMessage();
                Console.SetCursorPosition(0, 2);

                for (int i = 0; i < crossword.Rows; i++)
                {
                    for (int j = 0; j < crossword.Columns; j++) 
                    {
                        if (i == selectedRow && j == selectedColumn)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(maskedCrossword[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write(maskedCrossword[i,j] + " ");
                        
                        }
                    }
                    Console.WriteLine();
                }

                keyPressed = Console.ReadKey(true).Key;

                //switch case block based on the key pressed
                switch (keyPressed)
                {
                    //if the key pressed is the up arrow
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
                        if (selectedRow < crossword.Rows - 1)
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
                        if (selectedColumn < crossword.Columns)
                        {
                            //increment selected column
                            selectedColumn++;
                        }
                        break;

                    default:
                        CheckUserLetter(crossword, maskedCrossword, selectedRow, selectedColumn, keyPressed);
                        break;
                }
                //if the key pressed is the escape key, the loop breaks
            } while (keyPressed != ConsoleKey.Escape);

            Program.DisplayMenu();

        }

        public void CheckUserLetter(Crossword crossword, char[,] maskedCrossword, int row, int column, ConsoleKey keyPressed) 
        {
            //checks if input is a single vcharacter and that the character is  aletter
            if (!char.IsLetter((char)keyPressed))
            {
                return;
            }
                
        
            char guess = char.ToUpper((char)keyPressed);

            if (crossword.Grid[row, column] == guess)
            {
                maskedCrossword[row, column] = guess;

            }
            else
            {
                maskedCrossword[row, column] = 'X';
            
            }
        
        }

        //method that gets the crosswords stored in the json
        public List<Crossword> GetStoredCrosswords()
        {
            //create a new list of crosswords
            List<Crossword> crosswords = new List<Crossword>();
            //define the file path to the json
            string path = "crossword.json";

            //check if the json file exists
            if (File.Exists(path))
            {
                //if it does, read the file and store its contents in the json string
                string json = File.ReadAllText(path);

                //if the file is empty or contains only white space
                if (string.IsNullOrWhiteSpace(json))
                {
                    //create a new list of crosswords
                    crosswords = new List<Crossword>();
                }
                //if the file is not empty
                else
                {
                    //deserialise the json file into a list of crossword objects, which is stored in the predefined crossword list
                    crosswords = JsonConvert.DeserializeObject<List<Crossword>>(json, new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto }
);
                    //error handling for if the list is still empty
                    if (crosswords == null)
                    {
                        //create a new lis tof crosswords
                        crosswords = new List<Crossword>();
                    }
                }
            }

            //make a ne wlist for crossword objects that aren't empty
            List<Crossword> filledCrosswords = new List<Crossword>();

            //loop through the crossword list 
            foreach (Crossword c in crosswords) 
            {
                //if the crossword object is not empty 
                if (c != null) 
                //add the crossword to filled crosswords
                { filledCrosswords.Add(c); }
            }
            //replace crosswords with filledcrosswords to ensure there are no empty ones in the final list
            crosswords = filledCrosswords;

            
            //returns the list of crosswords
            return crosswords;


        }

        //method to create a solvable crossword
        //hides the words in any given crossword so that they cannot be seen
        //this method does not create another crossword object, rather temporarily creates a char[,] to mask the words
        public char[,] CreateSolvableCrossword(Crossword crossword) 
        {
            //setting the rows and columns based on the crossword selected
            int rows = crossword.Rows;
            int columns = crossword.Columns;

            //create a new crossword grid
            char[,] solvableCrossword = new char[rows, columns];

            //looping through the rows and the columns
            for (int i = 0; i < rows; i++) 
            {
                for (int j = 0; j < columns; j++) 
                {
                    //if the grid posiition in the crossword has a '*'
                    if (crossword.Grid[i, j] == '*')
                    {
                        //replace it with a #
                        solvableCrossword[i, j] = '#';

                    }
                    else
                    {
                        //otherwise if it is any letter
                        //replace it with a ?
                        solvableCrossword[i, j] = '?';
                    }
                }
            
            }

            //returns the char[,]
            return solvableCrossword;
        
        
        }

        //method to display the crossword for the user to solve
        public void DisplaySolvableCrossword(Crossword crossword)
        {
            //create a crossword manager object
            CrosswordManager crosswordManager = new CrosswordManager();

            //create a solvable crossword from the user selection
            char[,] solvableCrossword = crosswordManager.CreateSolvableCrossword(crossword);


            //loops through the rows and columns of the crossword grid
            for (int i = 0; i < crossword.Rows; i++)
            {
                for (int j = 0; j < crossword.Columns; j++)
                {
                    //for each grid position, it displays it on screen, adds space so that it is formatted nicely
                    Console.Write(solvableCrossword[i, j] + " ");
                }
                //moves to the next row in the crossword
                Console.WriteLine();
            }



        }

        public Crossword GetCrosswordBeingSolved() 
        {
            Crossword crosswordBeingSolved = Program.DisplayCrosswordSolver();
            return crosswordBeingSolved;
        }
    }
}
