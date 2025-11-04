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
            string jsonString = JsonConvert.SerializeObject(_currentCrossword, Formatting.Indented);
            File.WriteAllText("crossword.json", jsonString);


        }

        public void AddClue() 
        { 
        
        
        
        
        
        }
    }
}
