using System;

namespace Befunge
{
    public class ProgramCounter
    {
        private int _row;
        private int _col;
        private PlayField _playField;

        public ProgramCounter(int row, int col, PlayField playField)
        {
            _row = row;
            _col = col;
            _playField = playField;
        }

        public void MoveUp()
        {
            if (!_playField.IsLegalPosition(_row - 1, _col))
            {
                throw new ArgumentOutOfRangeException();
            }
            _row--;
        }

        public void MoveDown()
        {
            if (!_playField.IsLegalPosition(_row + 1, _col))
            {
                throw new ArgumentOutOfRangeException();
            }
            _row++;
        }

        public void MoveLeft()
        {
            if (!_playField.IsLegalPosition(_row, _col - 1))
            {
                throw new ArgumentOutOfRangeException();
            }
            _col--;
        }

        public void MoveRight()
        {
            if (!_playField.IsLegalPosition(_row, _col + 1))
            {
                throw new ArgumentOutOfRangeException();
            }
            _col++;
        }
    }
}