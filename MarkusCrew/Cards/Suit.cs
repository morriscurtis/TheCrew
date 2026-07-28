using System.Drawing;

namespace MarkusCrew.Cards
{
    public class Suit(SuitType type, string symbol, ConsoleColor color)
    {
        public SuitType Type { get; } = type;
        public string Symbol { get; } = symbol;
        public ConsoleColor Color { get; } = color;

        public override bool Equals(object? obj)
        {
            return obj is Suit suit &&
                   Type == suit.Type &&
                   Symbol == suit.Symbol;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Symbol);
        }

        public override string? ToString()
        {
            return Symbol;
        }
    }
}
