namespace MoCrew;

public struct Card(Suit suit, byte value)
{
	public Suit Suit = suit;
	public byte Value = value;

	public readonly (char Letter, ConsoleColor Color) GetSuitLetterAndColor() => Suit switch
	{
		Suit.Rocket => ('R', ConsoleColor.White),
		Suit.Blue => ('B', ConsoleColor.Blue),
		Suit.Green => ('G', ConsoleColor.Green),
		Suit.Pink => ('P', ConsoleColor.Magenta),
		Suit.Yellow => ('Y', ConsoleColor.Yellow),
		_ => throw new NotImplementedException(),
	};

	public readonly bool Beats(Card other)
		=> Suit == Suit.Rocket && other.Suit != Suit.Rocket
		|| Suit == other.Suit && Value > other.Value;

	public override readonly string ToString()
		=> $"{GetSuitLetterAndColor().Letter}{Value}";

	public readonly bool Equals(Card other)
		=> other.Suit == Suit && other.Value == Value;
}

public enum Suit
{
	Rocket,
	Green,
	Blue,
	Pink,
	Yellow,
}
