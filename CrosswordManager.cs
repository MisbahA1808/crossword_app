using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace CrosswordApp
{
    internal class CrosswordManager
    {
        //an instance of the current crossword
        private Crossword _currentCrossword;
        public CrosswordManager(Crossword crossword)
        {
            _currentCrossword = crossword;
        }

        //method for adding words to the current crossword
        public void AddWord(string word, string direction, int startRow, int startColumn) 
        {
            _currentCrossword.DisplayWord(word, direction, startRow, startColumn);
        
        
        }

        public void StoreCurrentCrossword() 
        {
            string jsonString = JsonConvert.SerializeObject(_currentCrossword, Formatting.Indented);
            File.WriteAllText("crossword.json", jsonString);


        }
    }
}
