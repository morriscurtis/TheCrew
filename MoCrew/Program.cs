using MoCrew;

int playerCount = 4;
int maxTrickCount = 10;
Player[] players = new Player[playerCount];
for (int i = 0; i < playerCount; ++i)
{
	players[i] = new Player($"P{i + 1}");
}
List<Card> playedCards = new(playerCount);
List<Player> trickWinners = new(maxTrickCount);
List<GameTask> tasks = new(maxTrickCount);
List<int> finishedTaskIndices = new(playerCount);
List<Player> taskPlayers = new(maxTrickCount);

bool printGames = true;

Mission[] missions = Mission.AllMissions;
int missionIndex = 0;
while (true)
{
	Mission mission = missions[missionIndex];

	int attemptNumber = 0;
	while (true)
	{
		++attemptNumber;

		if (printGames)
		{
			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine($"Mission #{missionIndex + 1} Attempt #{attemptNumber}:");
			Console.WriteLine("=================================");
			Console.WriteLine();
		}

		foreach (Player player in players)
		{
			player.Reset();
		}

		Dealer.DealPlayingCards(players);

		playedCards.Clear();
		trickWinners.Clear();
		tasks.Clear();

		{
			taskPlayers.Clear();
			for (int i = 0, n = mission.TotalTaskCount; i < n; ++i)
			{
				taskPlayers.Add(players[i % playerCount]);
			}
			taskPlayers.Shuffle();

			int p = 0, c = 0;
			ReadOnlySpan<Card> taskCards = Dealer.DealTaskCards();
			for (int t = 0; t < mission.PriorityTaskCount; ++t)
			{
				tasks.Add(new(taskCards[c++], taskPlayers[p++], t - 1));
			}
			int lastPriorityTaskIndex = mission.PriorityTaskCount - 1;
			for (int t = 0; t < mission.SequentialTaskCount; ++t)
			{
				tasks.Add(new(taskCards[c++], taskPlayers[p++], t > 0 ? t - 1 : lastPriorityTaskIndex));
			}
			for (int t = 0; t < mission.BasicTaskCount; ++t)
			{
				tasks.Add(new(taskCards[c++], taskPlayers[p++], lastPriorityTaskIndex));
			}
		}

		if (printGames)
		{
			if (tasks.Count > 0)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("Tasks:");
				int p = 0, s = 0;
				for (int i = 0; i < tasks.Count; ++i)
				{
					GameTask task = tasks[i];
					(char letter, ConsoleColor color) = task.Card.GetSuitLetterAndColor();
					Console.ForegroundColor = ConsoleColor.Gray;
					Console.Write($"{task.Player.Name}: ");
					Console.ForegroundColor = color;
					Console.Write($"{letter}{task.Card.Value}");
					Console.ForegroundColor = ConsoleColor.Gray;
					if (p < mission.PriorityTaskCount)
					{
						Console.WriteLine($" [{++p}]");
					}
					else if (s < mission.SequentialTaskCount)
					{
						Console.Write(" [");
						++s;
						for (int j = 0; j < s; ++j)
						{
							Console.Write(">");
						}
						Console.WriteLine("]");
					}
					else
					{
						Console.WriteLine();
					}
				}
				Console.WriteLine();
			}

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Initial Hands:");
			PrintPlayerHands(players);
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.Gray;
			Console.WriteLine("Tricks:");
		}

		int startingPlayerIndex = 0;
		bool missionFailed = false;
		int trickNumber = 1;
		for (; trickNumber <= maxTrickCount; ++trickNumber)
		{
			Player startingPlayer = players[startingPlayerIndex];
			int trickWinnerIndex = startingPlayerIndex;
			Card startingCard = startingPlayer.PlayCard();
			Card highestCard = startingCard;
			Suit suitToFollow = startingCard.Suit;
			playedCards.Add(startingCard);
			for (int i = 1; i < playerCount; ++i)
			{
				int playerIndex = (startingPlayerIndex + i) % playerCount;
				Player player = players[playerIndex];
				Card card = player.PlayCard(suitToFollow);
				if (card.Beats(highestCard))
				{
					highestCard = card;
					trickWinnerIndex = playerIndex;
				}
				playedCards.Add(card);
			}

			if (printGames)
			{
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write($"#{trickNumber:00}: {players[startingPlayerIndex].Name} =>");
				foreach (Card card in playedCards[^playerCount..])
				{
					(char letter, ConsoleColor color) = card.GetSuitLetterAndColor();
					Console.ForegroundColor = color;
					Console.Write($" {letter}{card.Value}");
				}
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine($" => {players[trickWinnerIndex].Name}");
			}
			startingPlayerIndex = trickWinnerIndex;
			Player trickWinner = players[trickWinnerIndex];
			trickWinner.TrickCount += 1;
			trickWinners.Add(trickWinner);

			finishedTaskIndices.Clear();
			foreach (Card card in playedCards[^playerCount..])
			{
				for (int t = 0; t < tasks.Count; t++)
				{
					var task = tasks[t];
					if (card.Equals(task.Card))
					{
						if (task.Player == trickWinner)
						{
							task.IsCompleted = true;
							tasks[t] = task;
							finishedTaskIndices.Add(t);
						}
						else
						{
							missionFailed = true;
							if (printGames)
							{
								Console.ForegroundColor = ConsoleColor.Red;
								Console.WriteLine($"Failed in trick #{trickNumber} - Task completed by wrong player!");
							}
							break;
						}
					}
				}
				if (missionFailed)
				{
					break;
				}
			}

			for (int i = 0, n = finishedTaskIndices.Count; i < n; ++i)
			{
				int t = finishedTaskIndices[i];
				int d = tasks[t].DependencyIndex;
				if (d >= 0 && !tasks[d].IsCompleted)
				{
					missionFailed = true;
					if (printGames)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Failed in trick #{trickNumber} - Tasks completed in wrong order!");
					}
					break;
				}
			}

			bool allTasksCompleted = true;
			foreach (GameTask task in tasks)
			{
				if (!task.IsCompleted)
				{
					allTasksCompleted = false;
					break;
				}
			}

			if (!missionFailed && mission.MaxTrickDifference > 0)
			{
				int min = int.MaxValue, max = int.MinValue;
				foreach (Player player in players)
				{
					min = int.Min(min, player.TrickCount);
					max = int.Max(max, player.TrickCount);
				}
				if (max - min > mission.MaxTrickDifference)
				{
					missionFailed = true;
					if (printGames)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Failed in trick #{trickNumber} - Trick counts diverged too much!");
					}
				}
			}
			if (missionFailed || allTasksCompleted)
			{
				break;
			}
		}
		if (!missionFailed)
		{
			foreach (GameTask task in tasks)
			{
				if (!task.IsCompleted)
				{
					missionFailed = true;
					if (printGames)
					{
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine($"Failed - Not all tasks were completed!");
					}
					break;
				}
			}
		}
		if (!missionFailed)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"Mission #{missionIndex + 1} accomplished in attempt #{attemptNumber} in trick #{trickNumber}!");
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
	ConsoleKey key = Console.ReadKey(true).Key;
	if (key == ConsoleKey.Escape)
	{
		break;
	}
	else if (key != ConsoleKey.R)
	{
		++missionIndex;
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
