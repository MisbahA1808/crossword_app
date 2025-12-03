using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public CrosswordManager()
        {
            
        }
        
        //method to add crossword to the list of crosswords
        public void AddCrosswordToList() 
        {
            _crosswords.Add(_currentCrossword);
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
            //if it finds thsat the current crossword already exists (based on the name), it will update it rather than make a duplicate
            for (int i = 0; i < crosswords.Count; i++) 
            {
                //if the titles of the 2 crosswords match
                if (crosswords[i].CrosswordTitle == _currentCrossword.CrosswordTitle)
                {
                    crosswords[i] = _currentCrossword;
                    break;
                }
                else 
                {
                    crosswords.Add(_currentCrossword);
                    break;
                }
            }

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
            //gets user to press a key to contniue
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

        //method that deals with the player solving their chosen crossword
        public void PlayerSolveCrossword(Crossword crossword) 
        {
            //start at the top left of the crossword by setting the start row and column to zero
            int selectedRow = 0;
            int selectedColumn = 0;
            //variable to store which key is pressed
            ConsoleKey keyPressed;

            //gets and stores the masked crossword forrhe chosen crossword
            //masked crossword is a 2d array version of the crossword, only used for displaying purposes so that the original crossword object is not altered when solving
            char[,] maskedCrossword;

            //if the player has saved trhe crosssword previosly as a draft
            if (crossword.CrosswordToSave != null)
            {
                //load that crossword
                maskedCrossword = crossword.CrosswordToSave;
            }
            else
            {
                //otherwise get and store the masked crossword for the chosen crossword
                maskedCrossword = CreateSolvableCrossword(crossword);
            }

            //loop to display the crossword and its updated changes each time until the admin pressed the escape key
            do
            {
                //clears the console
                Console.Clear();
                //display the welcome message
                Program.DisplayWelcomeMessage();

                //information for the user
                Console.SetCursorPosition(25, 27);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Use arrows keys to move, enter to select a position to guess from.");
                Console.ResetColor();

                Console.SetCursorPosition(0, 2);

                //printig the masked crossword on the console
                for (int i = 0; i < crossword.Rows; i++)
                {
                    for (int j = 0; j < crossword.Columns; j++)
                    {
                        //if it is the grid position that the user has selected with the arrow keys, starts at 0,0
                        if (i == selectedRow && j == selectedColumn)
                        {
                            //change its colour to red
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(maskedCrossword[i, j] + " ");
                            Console.ResetColor();
                        }
                        else
                        {
                            //otherwise print in normally
                            Console.Write(maskedCrossword[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
                //stores key pressed by user
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

                    case ConsoleKey.Enter:
                        PlayerEnterWord(crossword, maskedCrossword, selectedRow, selectedColumn);
                        break;

                    default:
                        //CheckUserLetter(crossword, maskedCrossword, selectedRow, selectedColumn, keyPressed);
                        break;
                }
                //if the key pressed is the escape key, the loop breaks
            } while (keyPressed != ConsoleKey.Escape);

            //saves the players progress when they press escape, so they can come back to it later
            //_currentCrossword = crossword;
            //StoreCurrentCrossword();

            crossword.CrosswordToSave = maskedCrossword;
            _currentCrossword = crossword;
            StoreCurrentCrossword();

            //returns back to the menu
            Program.DisplayMenu();
        }

        //method to check the word that the user guesses whilst solving the crossword
        public bool CheckUserWord(Crossword crossword, char[,] maskedCrossword,  string word, string direction, int startRow, int startColumn) 
        {
            if (direction == "across")
            {
                //checking that their guess will fit into the crossword
                if (startColumn + word.Length > crossword.Columns)
                {
                    return false;
                }
            }
            else if (direction == "down")
            {
                //checking that their guess will fit into the crossword
                if (startRow + word.Length > crossword.Rows)
                {
                    return false;
                }
            }
            //returns false if they put in an invalid direction
            else { return false; }

            //for each letter int their guess word
            for (int i = 0; i < word.Length; i++)
            {
                //define the current start row and column
                int currentRow = startRow;
                int currentColumn = startColumn;

                //if the direction they chose is across
                if (direction == "across")
                {
                    currentColumn = startColumn + i;
                }
                //else if the direction is down
                else { currentRow = startRow + i; }

                //get the values of the correct letter from the crossword and the masked letter too
                char correctLetter = crossword.Grid[currentRow, currentColumn];
                char maskedLetter = maskedCrossword[currentRow, currentColumn];

                //if there is a letter in that position
                if (maskedLetter == '?')
                {
                    //if the letter the user guessed for the position is wrong
                    if (word[i] != correctLetter)
                    {
                        return false;
                    }
                
                }
            }

            //for revealing the correct letter on screen if they entered it correct;y
            for (int i = 0; i < word.Length; i++) 
            {
                int currentRow = startRow;
                int currentColumn = startColumn;

                //if the direction is across
                if (direction == "across")
                {
                    currentColumn = startColumn + i;
                }
                //else if the direction is down
                else { currentRow = startRow + i; }
                
                //if there is a letter in that position
                if (maskedCrossword[currentRow, currentColumn] == '?') 
                {
                    //replace it with the users guess as it will be the corrct letter
                    maskedCrossword[currentRow, currentColumn] = word[i];
                }
            }
            return true;
        }

        //method that gets the word the user guesses whilst solving the crossword and checks if it is correct/incorrect
        public void PlayerEnterWord(Crossword crossword, char[,] maskedCrossword, int startRow, int startColumn) 
        {
            //gets word at the users slected position
            Word w = GetWord(crossword, startRow, startColumn);

            //if there is no word that starts at that position in the crossword
            if (w == null) 
            {
                //inform the user
                Console.WriteLine("No word starts here! Press any key to continue.");
                Console.ReadKey(true);
                return;
            }

            //if there is a word there. write the clue and the direction that th word goes on screen
            Console.WriteLine();
            Console.WriteLine("Clue: " + w.Clue);
            Console.WriteLine("Direction: " + w.Direction);
            Console.WriteLine();
            Console.SetCursorPosition(0, 16);

            //gets user input
            Console.WriteLine("Enter the word you guess:");
            string guess = Console.ReadLine();

            //get word in correct formato to then validate
            guess = guess.ToUpper().Trim();

            //checks if the word the user enters is correct
            bool correct = CheckUserWord(crossword, maskedCrossword, guess, w.Direction, startRow, startColumn);

            //if their guess is correct
            if (correct)
            {
                Console.WriteLine("Correct!!!");

                //checks if the crossword is fully solved or not
                if (IsCrosswordFullySolved(maskedCrossword))
                {
                    //if it is fully solved, inform the user and prompt them to return to the main menu
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Congratulations! You have solved the entire crossword! Well done :)");
                    Console.ResetColor();
                    Console.WriteLine("Press any key to return to the main menu.");
                    Console.ReadKey(true);
                    Program.DisplayMenu();
                    return;
                }
            }
            //if their guess is incorrect
            else 
            { 
                Console.WriteLine("Incorrect :("); 
            }
            Console.ReadKey(true);
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
                        //replace it with a '#'
                        solvableCrossword[i, j] = '#';

                    }
                    else
                    {
                        //otherwise if it is any letter
                        //replace it with a '?'
                        solvableCrossword[i, j] = '?';
                    }
                }
            }
            //returns the char[,]
            return solvableCrossword;
        }

        //method to get the word at the users selected start position
        public Word GetWord(Crossword crossword, int row, int column) 
        {
            //loops through th elist of words
            foreach (Word w in crossword.Words) 
            {
                //if at the users selected grid position, a word starts there
                if (w.StartRow == row && w.StartColumn == column) 
                {
                    //return the word
                    return w;
                }
            }
            //if no word starts at that position, return null
            return null;
        }

        //method to check if the user has completed the whole crossword or not
        public bool IsCrosswordFullySolved(char[,] maskedCrossword)
        {
            //loops through all of the grid positions
            for (int i = 0; i < maskedCrossword.GetLength(0); i++) 
            {
                for (int j = 0; j < maskedCrossword.GetLength(1); j++) 
                {
                    //if any still contain a '?' (there is still words left to guess)
                    if (maskedCrossword[i, j] == '?') 
                    {
                        //return false
                        return false;
                    }
                
                }
            }
            //otherwise return true
            return true;
        }
    }
}
