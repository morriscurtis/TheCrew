namespace MoCrew;

public class Player(string name)
{
	public string Name = name;
	public List<Card> Cards = new(10);
	public int TrickCount = 0;

	public void Reset()
	{
		Cards.Clear();
		TrickCount = 0;
	}

	public Card PlayCard() => PlayCard(Random.Shared.Next(Cards.Count));

	public Card PlayCard(Suit suitToFollow)
	{
		for (int i = 0, n = Cards.Count; i < n; i++)
		{
			if (Cards[i].Suit == suitToFollow)
			{
				return PlayCard(i);
			}
		}
		return PlayCard();
	}

	private Card PlayCard(int index)
	{
		Card card = Cards[index];
		Cards.SwapRemoveAt(index);
		return card;
	}
}
