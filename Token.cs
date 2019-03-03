using System;

namespace Befunge
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string message) : base(message) { }
    }

    public abstract class Token
    {
        public char Lexeme {get;}
        public Token(char lexeme) {
            Lexeme = lexeme;
        }
        public virtual CharToken AsCharToken() {
            return new CharToken(Lexeme);
        }
        public override string ToString(){
            return $"{this.GetType().Name}('{Lexeme}')";
        }
    }

    public class AddToken : Token {
        public AddToken(char lexeme) : base(lexeme) {}
    }

    public class SubtractToken : Token {
        public SubtractToken(char lexeme) : base(lexeme) {}
    }

    public class MultiplyToken : Token {
        public MultiplyToken(char lexeme) : base(lexeme) {}
    }

    public class DivideToken : Token {
        public DivideToken(char lexeme) : base(lexeme) {}
    }

    public class ModulusToken : Token {
        public ModulusToken(char lexeme) : base(lexeme) {}
    }

    public class NotToken : Token {
        public NotToken(char lexeme) : base(lexeme) {}
    }

    public class GreaterToken : Token {
        public GreaterToken(char lexeme) : base(lexeme) {}
    }

    public class RightToken : Token {
        public RightToken(char lexeme) : base(lexeme) {}
    }

    public class LeftToken : Token {
        public LeftToken(char lexeme) : base(lexeme) {}
    }

    public class UpToken : Token {
        public UpToken(char lexeme) : base(lexeme) {}
    }

    public class DownToken : Token {
        public DownToken(char lexeme) : base(lexeme) {}
    }

    public class RandomToken : Token {
        public RandomToken(char lexeme) : base(lexeme) {}
    }

    public class HorizontalIfToken : Token {
        public HorizontalIfToken(char lexeme) : base(lexeme) {}
    }

    public class VerticalIfToken : Token {
        public VerticalIfToken(char lexeme) : base(lexeme) {}
    }

    public class QuoteToken : Token {
        public QuoteToken(char lexeme) : base(lexeme) {}
        public override CharToken AsCharToken() {
            throw new TokenizerException("Cannot convert quote (\") to CharToken");
        }
    }

    public class DuplicateToken : Token {
        public DuplicateToken(char lexeme) : base(lexeme) {}
    }

    public class SwapToken : Token {
        public SwapToken(char lexeme) : base(lexeme) {}
    }

    public class PopToken : Token {
        public PopToken(char lexeme) : base(lexeme) {}
    }

    public class OutputIntToken : Token {
        public OutputIntToken(char lexeme) : base(lexeme) {}
    }

    public class OutputCharToken : Token {
        public OutputCharToken(char lexeme) : base(lexeme) {}
    }

    public class JumpToken : Token {
        public JumpToken(char lexeme) : base(lexeme) {}
    }

    public class GetToken : Token {
        public GetToken(char lexeme) : base(lexeme) {}
    }

    public class PutToken : Token {
        public PutToken(char lexeme) : base(lexeme) {}
    }

    public class InputIntToken : Token {
        public InputIntToken(char lexeme) : base(lexeme) {}
    }

    public class InputCharToken : Token {
        public InputCharToken(char lexeme) : base(lexeme) {}
    }

    public class HaltToken : Token {
        public HaltToken(char lexeme) : base(lexeme) {}
    }

    public class NumberToken : Token {
        public byte Value {get; private set;}
        public NumberToken(char lexeme) : base(lexeme) {
            Value = byte.Parse(lexeme.ToString());
        }
    }

    public class CharToken : Token {
        public CharToken(char lexeme) : base(lexeme) {}
    }

    public class BlankToken : Token {
        public BlankToken() : base(' ') {}
        public override CharToken AsCharToken() {
            throw new TokenizerException("Cannot convert BlankToken to CharToken");
        }
    }
}