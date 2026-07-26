namespace TheCrew;

public class Player(string name)
{
	private static readonly List<int> validIndices = new(10);

	public string Name = name;
	public List<Card> Cards = new(10);
	public int TrickCount = 0;

	public void Reset()
	{
		TrickCount = 0;
		Cards.Clear();
	}

	public Card PlayCard() => PlayCard(Random.Shared.Next(Cards.Count));

	public Card PlayCard(Suit suitToFollow)
	{
		validIndices.Clear();
		int n = Cards.Count;
		for (int i = 0; i < n; ++i)
		{
			if (Cards[i].Suit == suitToFollow)
			{
				validIndices.Add(i);
			}
		}
		n = validIndices.Count;
		return n > 0 ? PlayCard(validIndices[Random.Shared.Next(n)]) : PlayCard();
	}

	private Card PlayCard(int chosenIndex)
	{
		Card chosenCard = Cards[chosenIndex];
		int lastIndex = Cards.Count - 1;
		Cards[chosenIndex] = Cards[lastIndex];
		Cards.RemoveAt(lastIndex);
		return chosenCard;
	}
}
