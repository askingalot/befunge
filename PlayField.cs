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
        public PlayField() {
            for (int i=0; i<ROW_SIZE; i++) {
                for(int j=0; j<COL_SIZE; j++) {
                    _field[i, j] = new BlankToken(' ', i, j);
                }
            }
        }

        public override string ToString() {
            var sb = new StringBuilder();
            for (var i=0; i<ROW_SIZE; i++) {
                sb.Append(string.Join(", ", GetRow(_field, i)));
                sb.Append('\n');
            }
            return sb.ToString();
        }

        private IEnumerable<Token> GetRow(Token[,] tokens, int row) {
            for (var i=0; i<COL_SIZE; i++) {
                yield return tokens[row, i];
            }
        }
    }
}