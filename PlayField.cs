using System;
using System.Collections.Generic;
using System.Text;

namespace Befunge
{
    public class PlayField
    {
        private const int ROW_SIZE = 80;
        private const int COL_SIZE = 25;
        private Token[,] _field = new Token[ROW_SIZE, COL_SIZE];

        public ProgramCounter ProgramCounter { get; }

        public PlayField(IEnumerable<Token> initialTokens)
        {
            foreach (var token in initialTokens)
            {
                _field[token.Row, token.Column] = token;
            }

            for (int i = 0; i < ROW_SIZE; i++)
            {
                for (int j = 0; j < COL_SIZE; j++)
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

        public Token this[int row, int col]
        {
            get
            {
                return _field[row, col];
            }
        }

        public bool IsLegalPosition(int row, int col)
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

        private IEnumerable<Token> GetRow(Token[,] tokens, int row)
        {
            for (var i = 0; i < COL_SIZE; i++)
            {
                yield return tokens[row, i];
            }
        }
    }
}