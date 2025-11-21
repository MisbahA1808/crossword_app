using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordApp
{
    //class to represent a word on the crossword
    internal class Word
    {
        //attributes
        private string _clue;
        private string _text;
        private int _length;
        private string _direction;
        private int _startRow;
        private int _startColumn;

        //getters and setters
        public string Direction { get => _direction; set => _direction = value; }
        public int StartRow { get => _startRow; set => _startRow = value; }
        public int StartColumn { get => _startColumn; set => _startColumn = value; }
        public string Clue { get => _clue; set => _clue = value; }

        //constructor
        public Word(string text, string direction, string clue,int startRow, int startColumn)
        {
            _text = text;
            _direction = direction;
            _clue = clue;
            _startRow = startRow;
            _startColumn = startColumn;
            //_length = _text.Length;

        }


    }
}
