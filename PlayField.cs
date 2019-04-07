using System;
using System.Collections.Generic;
using System.Text;

namespace Befunge
{
    public class PlayField
    {
        private const long ROW_COUNT = 25;
        private const long COL_COUNT = 80;
        private Token[,] _field = new Token[ROW_COUNT, COL_COUNT];

        public long TOP_ROW => 0;
        public long BOTTOM_ROW => ROW_COUNT - 1;
        public long LEFT_COL => 0;
        public long RIGHT_COL => COL_COUNT - 1;

        public ProgramCounter ProgramCounter { get; }


        public PlayField(IEnumerable<Token> initialTokens)
        {
            foreach (var token in initialTokens)
            {
                _field[token.Row, token.Column] = token;
            }

            for (long i = 0; i < ROW_COUNT; i++)
            {
                for (long j = 0; j < COL_COUNT; j++)
                {
                    if (_field[i, j] == null)
                    {
                        _field[i, j] = new BlankToken(' ', i, j);
                    }
                }
            }

            ProgramCounter = new ProgramCounter(0, 0, this);
        }

        public Token Current {
            get {
                return this[ProgramCounter.Row, ProgramCounter.Col];
            }
        }

        public Token this[long row, long col]
        {
            get
            {
                return _field[row, col];
            } 
            set {
                _field[row, col] = value;
            }
        }

        public bool IsLegalPosition(long row, long col)
        {
            return row >= 0 && row < COL_COUNT &&
                   col >= 0 && col < ROW_COUNT;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < COL_COUNT; i++)
            {
                sb.Append(string.Join(", ", GetRow(_field, i)));
                sb.Append('\n');
            }
            return sb.ToString();
        }

        private IEnumerable<Token> GetRow(Token[,] tokens, long row)
        {
            for (var i = 0; i < ROW_COUNT; i++)
            {
                yield return tokens[row, i];
            }
        }
    }
}