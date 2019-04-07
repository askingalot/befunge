using System;
using System.Collections.Generic;
using System.Text;

namespace Befunge
{
    public class PlayField
    {
        private const long ROW_SIZE = 80;
        private const long COL_SIZE = 25;
        private Token[,] _field = new Token[COL_SIZE, ROW_SIZE];

        public long LEFT_COL => 0;
        public long RIGHT_COL => ROW_SIZE - 1;
        public long TOP_ROW => 0;
        public long BOTTOM_ROW => COL_SIZE - 1;

        public ProgramCounter ProgramCounter { get; }


        public PlayField(IEnumerable<Token> initialTokens)
        {
            foreach (var token in initialTokens)
            {
                _field[token.Row, token.Column] = token;
            }

            for (long i = 0; i < COL_SIZE; i++)
            {
                for (long j = 0; j < ROW_SIZE; j++)
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
            return row >= 0 && row < ROW_SIZE &&
                   col >= 0 && col < COL_SIZE;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            for (var i = 0; i < ROW_SIZE; i++)
            {
                sb.Append(string.Join(", ", GetRow(_field, i)));
                sb.Append('\n');
            }
            return sb.ToString();
        }

        private IEnumerable<Token> GetRow(Token[,] tokens, long row)
        {
            for (var i = 0; i < COL_SIZE; i++)
            {
                yield return tokens[row, i];
            }
        }
    }
}