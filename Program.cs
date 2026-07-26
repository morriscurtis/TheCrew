using TheCrew;

int playerCount = 4;
Player[] players = new Player[playerCount];
for (int i = 0; i < playerCount; ++i)
{
	players[i] = new Player($"P{i + 1}");
}
List<Card> playedCards = new(playerCount);
List<Player> trickWinners = new(10);
Dictionary<Card, GameTask> tasksByCard = [];

bool printGames = false;

Mission[] missions = Mission.AllMissions;
for (int missionIndex = 0; missionIndex < missions.Length; missionIndex++)
{
	Mission mission = missions[missionIndex];

	int attemptNumber = 0;
	while (true)
	{
		++attemptNumber;

		if (printGames)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"Attempt #{attemptNumber}:");
			Console.WriteLine("=================================");
			Console.WriteLine();
		}

		foreach (Player player in players)
		{
			player.Reset();
		}

		Dealer.DealCards(players);

		playedCards.Clear();
		trickWinners.Clear();

		if (printGames)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Initial Hands:");
			PrintPlayerHands(players);
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Tricks:");
		}

		int startPlayerIndex = 0;
		bool missionFailed = false;
		for (int trickNumber = 1; trickNumber <= 10; ++trickNumber)
		{
			int highestCardIndex = 0;
			Card firstPlayedCard = players[startPlayerIndex].PlayCard();
			Suit suitToFollow = firstPlayedCard.Suit;
			playedCards.Add(firstPlayedCard);
			for (int i = 1; i < playerCount; ++i)
			{
				Player player = players[(startPlayerIndex + i) % playerCount];
				Card card = player.PlayCard(suitToFollow);
				if (card.Beats(playedCards[highestCardIndex]))
				{
					highestCardIndex = i;
				}
				playedCards.Add(card);
			}

			if (printGames)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write($"#{trickNumber:00}: P{startPlayerIndex + 1} /");
				foreach (Card card in playedCards[^playerCount..])
				{
					(char letter, ConsoleColor color) = card.GetSuitLetterAndColor();
					Console.ForegroundColor = color;
					Console.Write($" {letter}{card.Value}");
				}
				Console.WriteLine();
			}
			startPlayerIndex = highestCardIndex;
			Player trickWinner = players[highestCardIndex];
			trickWinner.TrickCount += 1;
			trickWinners.Add(trickWinner);

			for (int i = 1; i < playerCount; ++i)
			{
				if (Math.Abs(players[i - 1].TrickCount - players[i].TrickCount) > 1)
				{
					if (printGames)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Failed in trick {trickNumber} - Trick counts diverged too much!");
					}
					missionFailed = true;
					break;
				}
			}
			if (missionFailed)
			{
				break;
			}
		}
		if (!missionFailed)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"Mission accomplished in attempt {attemptNumber}!");
			if (printGames)
			{
				Console.WriteLine();
			}
			break;
		}
		if (printGames)
		{
			Console.WriteLine();
			Console.ReadKey(true);
		}
	}
	if (Console.ReadKey(true).Key == ConsoleKey.Escape)
	{
		break;
	}
}

Console.ResetColor();

static void PrintPlayerHands(Span<Player> players)
{
	foreach (Player player in players)
	{
		Console.ForegroundColor = ConsoleColor.Gray;
		Console.Write($"{player.Name}:");

		foreach (Card card in player.Cards)
		{
			(char letter, ConsoleColor color) = card.GetSuitLetterAndColor();
			Console.ForegroundColor = color;
			Console.Write($" {letter}{card.Value}");
		}
		Console.WriteLine();
	}
}
