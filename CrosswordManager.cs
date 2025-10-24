using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
