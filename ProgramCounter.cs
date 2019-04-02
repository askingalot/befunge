using System;
using System.Collections.Generic;

namespace Befunge
{
    public class ProgramCounter
    {
        private PlayField _playField;

        private Dictionary<ProgramCounterDirection, Action> Directions;

        public int Row { get; private set; }
        public int Col { get; private set; }

        public ProgramCounter(int row, int col, PlayField playField)
        {
            Row = row;
            Col = col;
            _playField = playField;

            Directions = new Dictionary<ProgramCounterDirection, Action>{
                {ProgramCounterDirection.Up, MoveUp},
                {ProgramCounterDirection.Down, MoveDown},
                {ProgramCounterDirection.Left, MoveLeft},
                {ProgramCounterDirection.Right, MoveRight},
            };
        }

        public void Move(ProgramCounterDirection direction) {
            Directions[direction]();
        }

        public void MoveUp()
        {
            if (!_playField.IsLegalPosition(Row - 1, Col))
            {
                throw new ArgumentOutOfRangeException();
            }
            Row--;
        }

        public void MoveDown()
        {
            if (!_playField.IsLegalPosition(Row + 1, Col))
            {
                throw new ArgumentOutOfRangeException();
            }
            Row++;
        }

        public void MoveLeft()
        {
            if (!_playField.IsLegalPosition(Row, Col - 1))
            {
                throw new ArgumentOutOfRangeException();
            }
            Col--;
        }

        public void MoveRight()
        {
            if (!_playField.IsLegalPosition(Row, Col + 1))
            {
                throw new ArgumentOutOfRangeException();
            }
            Col++;
        }
    }
}