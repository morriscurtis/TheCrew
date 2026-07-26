namespace TheCrew;

public class Dealer
{
	private static readonly Card[] cards = new Card[40];

	static Dealer()
	{
		int i = 0;
		for (byte s = 4; s >= 1; --s)
		{
			cards[i++] = new(Suit.Rocket, s);
			for (byte v = 1; v <= 9; ++v)
			{
				cards[i++] = new((Suit)s, v);
			}
		}
	}

	public static void DealCards(Span<Player> players)
	{
		Shuffle(cards.AsSpan(1..));
		for (int i = 0, n = players.Length; i < 40; ++i)
		{
			players[i % n].Cards.Add(cards[i]);
		}
	}

	public static void Shuffle(Span<Card> cards)
	{
		Random rng = Random.Shared;
		for (int n = cards.Length; n >= 2; --n)
		{
			int i = rng.Next(n);
			int j = n - 1;
			(cards[j], cards[i]) = (cards[i], cards[j]);
		}
	}
}
