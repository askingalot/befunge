using System.Collections.Generic;

namespace Befunge {
    public class Tokenizer {
        private string _source;

        public Tokenizer(string source) {
            _source = source;
        }

        public IEnumerable<Token> Tokenize() {
            foreach(var lexeme in _source) {
                switch(lexeme) {
                    case '+':
                        yield return new AddToken(lexeme);
                    break;
                    case '-':
                        yield return new SubtractToken(lexeme);
                    break;
                    case '*':
                        yield return new MultiplyToken(lexeme);
                    break;
                    case '/':
                        yield return new DivideToken(lexeme);
                    break;
                    case '%':
                        yield return new ModulusToken(lexeme);
                    break;
                    case '!':
                        yield return new NotToken(lexeme);
                    break;
                    case '`':
                        yield return new GreaterToken(lexeme);
                    break;
                    case '>':
                        yield return new RightToken(lexeme);
                    break;
                    case '<':
                        yield return new LeftToken(lexeme);
                    break;
                    case '^':
                        yield return new UpToken(lexeme);
                    break;
                    case 'v':
                        yield return new DownToken(lexeme);
                    break;
                    case '?':
                        yield return new RandomToken(lexeme);
                    break;
                    case '_':
                        yield return new HorizontalIfToken(lexeme);
                    break;
                    case '|':
                        yield return new VerticalIfToken(lexeme);
                    break;
                    case '"':
                        yield return new QuoteToken(lexeme);
                    break;
                    case ':':
                        yield return new DuplicateToken(lexeme);
                    break;
                    case '\\':
                        yield return new SwapToken(lexeme);
                    break;
                    case '$':
                        yield return new PopToken(lexeme);
                    break;
                    case '.':
                        yield return new OutputIntToken(lexeme);
                    break;
                    case ',':
                        yield return new OutputCharToken(lexeme);
                    break;
                    case '#':
                        yield return new JumpToken(lexeme);
                    break;
                    case 'g':
                        yield return new GetToken(lexeme);
                    break;
                    case 'p':
                        yield return new PutToken(lexeme);
                    break;
                    case '&':
                        yield return new InputIntToken(lexeme);
                    break;
                    case '~':
                        yield return new InputCharToken(lexeme);
                    break;
                    case '@':
                        yield return new HaltToken(lexeme);
                    break;
                    case char ch when char.IsDigit(ch):
                        yield return new NumberToken(ch);
                    break;
                    case char ch:
                        yield return new CharToken(ch);
                    break;
                    default:
                        throw new TokenizerException($"Unknown token: {lexeme}");
                }
            }
        }
    }
}