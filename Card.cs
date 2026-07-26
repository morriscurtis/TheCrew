namespace TheCrew;

public struct Card(Suit suit, byte value)
{
	public Suit Suit = suit;
	public byte Value = value;

	public (char Letter, ConsoleColor Color) GetSuitLetterAndColor() => Suit switch
	{
		Suit.Rocket => ('R', ConsoleColor.White),
		Suit.Blue => ('B', ConsoleColor.Blue),
		Suit.Green => ('G', ConsoleColor.Green),
		Suit.Pink => ('P', ConsoleColor.Magenta),
		Suit.Yellow => ('Y', ConsoleColor.Yellow),
		_ => throw new NotImplementedException(),
	};

	public readonly bool Beats(Card other)
		=> Suit == Suit.Rocket && other.Suit != Suit.Rocket || Suit == other.Suit && Value > other.Value;

	public override string ToString() => $"{GetSuitLetterAndColor().Letter}{Value}";
}

public enum Suit
{
	Rocket,
	Green,
	Blue,
	Pink,
	Yellow,
}
