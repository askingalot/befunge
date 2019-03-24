using System.Collections.Generic;

namespace Befunge
{
    public class Tokenizer
    {
        private string _source;

        public Tokenizer(string source)
        {
            _source = source;
        }

        public IEnumerable<Token> Tokenize()
        {
            int row = 0;
            int col = -1;

            foreach (var lexeme in _source)
            {
                if (lexeme == '\n')
                {
                    row++;
                    col = -1;
                    continue;
                }
                col++;

                switch (lexeme)
                {
                    case '+':
                        yield return new AddToken(lexeme, row, col);
                        break;
                    case '-':
                        yield return new SubtractToken(lexeme, row, col);
                        break;
                    case '*':
                        yield return new MultiplyToken(lexeme, row, col);
                        break;
                    case '/':
                        yield return new DivideToken(lexeme, row, col);
                        break;
                    case '%':
                        yield return new ModulusToken(lexeme, row, col);
                        break;
                    case '!':
                        yield return new NotToken(lexeme, row, col);
                        break;
                    case '`':
                        yield return new GreaterToken(lexeme, row, col);
                        break;
                    case '>':
                        yield return new RightToken(lexeme, row, col);
                        break;
                    case '<':
                        yield return new LeftToken(lexeme, row, col);
                        break;
                    case '^':
                        yield return new UpToken(lexeme, row, col);
                        break;
                    case 'v':
                        yield return new DownToken(lexeme, row, col);
                        break;
                    case '?':
                        yield return new RandomToken(lexeme, row, col);
                        break;
                    case '_':
                        yield return new HorizontalIfToken(lexeme, row, col);
                        break;
                    case '|':
                        yield return new VerticalIfToken(lexeme, row, col);
                        break;
                    case '"':
                        yield return new QuoteToken(lexeme, row, col);
                        break;
                    case ':':
                        yield return new DuplicateToken(lexeme, row, col);
                        break;
                    case '\\':
                        yield return new SwapToken(lexeme, row, col);
                        break;
                    case '$':
                        yield return new PopToken(lexeme, row, col);
                        break;
                    case '.':
                        yield return new OutputIntToken(lexeme, row, col);
                        break;
                    case ',':
                        yield return new OutputCharToken(lexeme, row, col);
                        break;
                    case '#':
                        yield return new JumpToken(lexeme, row, col);
                        break;
                    case 'g':
                        yield return new GetToken(lexeme, row, col);
                        break;
                    case 'p':
                        yield return new PutToken(lexeme, row, col);
                        break;
                    case '&':
                        yield return new InputIntToken(lexeme, row, col);
                        break;
                    case '~':
                        yield return new InputCharToken(lexeme, row, col);
                        break;
                    case '@':
                        yield return new HaltToken(lexeme, row, col);
                        break;
                    case ' ':
                        yield return new BlankToken(lexeme, row, col);
                        break;
                    case char ch when char.IsDigit(ch):
                        yield return new NumberToken(ch, row, col);
                        break;
                    case char ch:
                        yield return new CharToken(ch, row, col);
                        break;
                    default:
                        throw new TokenizerException($"Unknown token, {lexeme} at ({row}, {col})");
                }
            }
        }
    }
}