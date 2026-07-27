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
List<int> completingTaskIndices = new(playerCount);
List<Player> taskPlayers = new(maxTrickCount);
List<string> failureReasons = [];

bool printGames = true;

Mission[] missions = Mission.AllMissions;
int missionIndex = 0;
while (true)
{
	Mission mission = missions[missionIndex];
	bool missionCanFinishEarly = mission.MaxTrickDifference == 0
		&& mission.SpecialCondition != SpecialCondition.OneChosenPlayerWinsNoTricks;
	int totalTaskCount = mission.TotalTaskCount;

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

		switch (mission.SpecialCondition)
		{
			case SpecialCondition.OneChosenPlayerWinsNoTricks:
				int sickPlayerIndex = Random.Shared.Next(1, playerCount - 1);
				players[sickPlayerIndex].IsSick = true;
				break;
		}

		Dealer.DealPlayingCards(players);

		failureReasons.Clear();
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
		int completedTaskCount = 0;
		int trickNumber = 1;
		for (; trickNumber <= maxTrickCount && failureReasons.Count == 0; ++trickNumber)
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
				foreach (Card card in playedCards.AsReadOnlySpan()[^playerCount..])
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

			completingTaskIndices.Clear();
			foreach (Card card in playedCards.AsReadOnlySpan()[^playerCount..])
			{
				for (int t = 0; t < tasks.Count; t++)
				{
					GameTask task = tasks[t];
					if (card.Equals(task.Card))
					{
						if (task.Player == trickWinner)
						{
							task.IsCompleted = true;
							tasks[t] = task;
							completingTaskIndices.Add(t);
							++completedTaskCount;
						}
						else
						{
							failureReasons.Add("Task completed by wrong player!");
							break;
						}
					}
				}
				if (failureReasons.Count > 0)
				{
					break;
				}
			}
			for (int i = 0, n = completingTaskIndices.Count; i < n; ++i)
			{
				int t = completingTaskIndices[i];
				int d = tasks[t].DependencyIndex;
				if (d >= 0 && !tasks[d].IsCompleted)
				{
					failureReasons.Add("Tasks completed in wrong order!");
					break;
				}
			}

			if (trickWinner.IsSick)
			{
				failureReasons.Add("Sick player won a trick!");
			}

			if (mission.MaxTrickDifference > 0)
			{
				int min = int.MaxValue, max = int.MinValue;
				foreach (Player player in players)
				{
					min = int.Min(min, player.TrickCount);
					max = int.Max(max, player.TrickCount);
				}
				if (max - min > mission.MaxTrickDifference)
				{
					failureReasons.Add("Trick counts diverged too much!");
				}
			}

			if (missionCanFinishEarly && completedTaskCount == totalTaskCount && failureReasons.Count == 0)
			{
				break;
			}
		}
		if (trickNumber > maxTrickCount && completedTaskCount != totalTaskCount)
		{
			failureReasons.Add("Not all tasks were completed!");
		}

		Console.WriteLine();
		if (failureReasons.Count == 0)
		{
			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine(printGames ? "Mission completed!\n"
				: $"Mission #{missionIndex + 1} completed in attempt #{attemptNumber} in trick #{trickNumber}!\n");
			break;
		}
		else if (printGames)
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine($"Mission failed:");
			foreach (string failureReason in failureReasons)
			{
				Console.Write("- ");
				Console.WriteLine(failureReason);
			}
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
