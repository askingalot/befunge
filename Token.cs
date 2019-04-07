using System;

namespace Befunge
{
    public class TokenizerException : Exception
    {
        public TokenizerException(string message) : base(message) { }
    }

    public abstract class Token
    {
        public char Lexeme { get; }
        public long Row { get; }
        public long Column { get; }
        public Token(char lexeme, long row, long col)
        {
            Lexeme = lexeme;
            Row = row;
            Column = col;
        }
        public virtual CharToken AsCharToken()
        {
            return new CharToken(Lexeme, Row, Column);
        }
        public override string ToString()
        {
            return $"{this.GetType().Name}('{Lexeme}', {Row}, {Column})";
        }
    }

    public class AddToken : Token
    {
        public AddToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class SubtractToken : Token
    {
        public SubtractToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class MultiplyToken : Token
    {
        public MultiplyToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class DivideToken : Token
    {
        public DivideToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class ModulusToken : Token
    {
        public ModulusToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class NotToken : Token
    {
        public NotToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class GreaterToken : Token
    {
        public GreaterToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class RightToken : Token
    {
        public RightToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class LeftToken : Token
    {
        public LeftToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class UpToken : Token
    {
        public UpToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class DownToken : Token
    {
        public DownToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class RandomToken : Token
    {
        public RandomToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class HorizontalIfToken : Token
    {
        public HorizontalIfToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class VerticalIfToken : Token
    {
        public VerticalIfToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class QuoteToken : Token
    {
        public QuoteToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
        public override CharToken AsCharToken()
        {
            throw new TokenizerException("Cannot convert quote (\") to CharToken");
        }
    }

    public class DuplicateToken : Token
    {
        public DuplicateToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class SwapToken : Token
    {
        public SwapToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class PopToken : Token
    {
        public PopToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class OutputIntToken : Token
    {
        public OutputIntToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class OutputCharToken : Token
    {
        public OutputCharToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class JumpToken : Token
    {
        public JumpToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class GetToken : Token
    {
        public GetToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class PutToken : Token
    {
        public PutToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class InputIntToken : Token
    {
        public InputIntToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class InputCharToken : Token
    {
        public InputCharToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class HaltToken : Token
    {
        public HaltToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }

    public class NumberToken : Token
    {
        public byte Value { get; private set; }
        public NumberToken(char lexeme, long row, long col) : base(lexeme, row, col)
        {
            Value = byte.Parse(lexeme.ToString());
        }
    }

    public class CharToken : Token
    {
        public int AsciiValue { get; }
        public CharToken(char lexeme, long row, long col) : base(lexeme, row, col) { 
            AsciiValue = lexeme;
        }
    }

    public class BlankToken : Token
    {
        public BlankToken(char lexeme, long row, long col) : base(lexeme, row, col) { }
    }
}