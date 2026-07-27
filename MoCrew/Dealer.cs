namespace MoCrew;

public class Dealer
{
	private static readonly Card[] playingCards = new Card[40];
	private static readonly Card[] taskCards = new Card[36];

	static Dealer()
	{
		int p = 0, t = 0;
		for (byte s = 4; s >= 1; --s)
		{
			playingCards[p++] = new(Suit.Rocket, s);
			for (byte v = 1; v <= 9; ++v)
			{
				Card card = new((Suit)s, v);
				playingCards[p++] = card;
				taskCards[t++] = card;
			}
		}
	}

	public static void DealPlayingCards(Span<Player> players)
	{
		playingCards.AsSpan(1..).Shuffle();
		for (int i = 0, n = players.Length; i < 40; ++i)
		{
			players[i % n].Cards.Add(playingCards[i]);
		}
	}

	public static ReadOnlySpan<Card> DealTaskCards()
	{
		taskCards.Shuffle();
		return taskCards.AsSpan();
	}
}
