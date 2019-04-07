using System;
using System.Collections.Generic;

namespace Befunge
{
    public class ProgramCounter
    {
        private PlayField _playField;

        private Dictionary<ProgramCounterDirection, Action> Directions;

        public long Row { get; private set; }
        public long Col { get; private set; }

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

        public void Move(ProgramCounterDirection direction)
        {
            Directions[direction]();
        }

        public void MoveUp()
        {
            if (Row == _playField.TOP_ROW)
            {
                Row = _playField.BOTTOM_ROW;
            }
            else
            {
                Row--;
            }
        }

        public void MoveDown()
        {
            if (Row == _playField.BOTTOM_ROW)
            {
                Row = _playField.TOP_ROW;
            }
            else
            {
                Row++;
            }
        }

        public void MoveLeft()
        {
            if (Col == _playField.LEFT_COL)
            {
                Col = _playField.RIGHT_COL;
            }
            else
            {
                Col--;
            }
        }

        public void MoveRight()
        {
            if (Col == _playField.RIGHT_COL)
            {
                Col = _playField.LEFT_COL;
            }
            else
            {
                Col++;
            }
        }
    }
}