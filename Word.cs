using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    internal class Word
    {
        private string _clue;
        private string _text;
        private int _length;
        private string _direction;

        public string Direction { get => _direction; set => _direction = value; }

        public Word(string text, string direction, string clue)
        {
            _text = text;
            _direction = direction;
            _clue = clue;

            _length = _text.Length;

        }

        //method to determine whether or not a word will fit onto a given crossword
        public bool IfValidWord(Crossword crossword) 
        {
            bool valid = false;

            _direction.ToLower().Trim();
            if (_direction == "down")
            {
                if (crossword.Columns >= _length)
                {
                    valid = true;
                }
            }
            else if (_direction == "across") 
            {
                if(crossword.Rows >= _length)
                {
                    valid = true;
                }
            
            
            }
            return valid;
        
        }

    }
}
