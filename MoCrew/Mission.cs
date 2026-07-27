using System.Net;

namespace MoCrew;

public enum TaskDistribution
{
	PlayersChoice,
	ByCommander,
	ByCommanderButHidden,
}

public enum Communication
{
	FromStart,
	AfterFirstTrick,
	AfterSecondTrick,
	Limited,
	OneChosenPlayerDisallowed,
}

public enum SpecialCondition
{
	None,
	OneChosenPlayerWinsNoTricks,
	OneTrickWithAValue1Card,
	TwoTricksWithAValue1Card,
	NoValue9CardMustWinATrick,
	EachRocketMustWinATrick,
	EachRocketMustWinATrickInAscendingOrder,
	DrawCardFromNeighborAfterFirstTrick,
	PlayerLeftOfPink9WinsAllPinkCards,
	OmegaTask,
	OmegaTaskMustBeLastTrick,
	TwoTilesMayBeSwapped,
	OneTileMayBeMovedToTaskWithoutTile,
	OnePlayerMustWinFirstAndLastTrickWithoutRockets,
	OneChosenPlayerMustWinExactlyOneTrickWithoutRockets,
	CommanderMustWinFirstAndLastTrick,
	CommanderChoosesPlayerForOnlyTheFirstFourTricksAndAnotherForOnlyTheLastTrick,
}

public class Mission()
{
	public static readonly Mission[] AllMissions =
	[
		new() // Mission 1
		{
			BasicTaskCount = 1,
		},
		new() // Mission 2
		{
			BasicTaskCount = 2,
		},
		new() // Mission 3
		{
			PriorityTaskCount = 2,
		},
		new() // Mission 4
		{
			BasicTaskCount = 3,
		},
		new() // Mission 5
		{
			SpecialCondition = SpecialCondition.OneChosenPlayerWinsNoTricks,
		},
		new() // Mission 6
		{
			SequentialTaskCount = 2,
			BasicTaskCount = 1,
			Communication = Communication.Limited,
		},
		new() // Mission 7
		{
			BasicTaskCount = 2,
			SpecialCondition = SpecialCondition.OmegaTask,
		},
		new() // Mission 8
		{
			PriorityTaskCount = 3,
		},
		new() // Mission 9
		{
			SpecialCondition = SpecialCondition.OneTrickWithAValue1Card,
		},
		new() // Mission 10
		{
			BasicTaskCount = 4,
		},
		new() // Mission 11
		{
			PriorityTaskCount = 1, BasicTaskCount = 3, Communication = Communication.OneChosenPlayerDisallowed,
		},
		new() // Mission 12
		{
			BasicTaskCount = 3,
			SpecialCondition = SpecialCondition.DrawCardFromNeighborAfterFirstTrick,
		},
		new() // Mission 13
		{
			SpecialCondition = SpecialCondition.EachRocketMustWinATrick,
		},
		new() // Mission 14
		{
			SequentialTaskCount = 3,
			BasicTaskCount = 1,
			Communication = Communication.Limited,
		},
		new() // Mission 15
		{
			PriorityTaskCount = 4,
		},
		new() // Mission 16
		{
			SpecialCondition = SpecialCondition.NoValue9CardMustWinATrick,
		},
		new() // Mission 17
		{
			BasicTaskCount = 2,
			SpecialCondition = SpecialCondition.NoValue9CardMustWinATrick,
		},
		new() // Mission 18
		{
			BasicTaskCount = 5,
			Communication = Communication.AfterFirstTrick,
		},
		new() // Mission 19
		{
			PriorityTaskCount = 1,
			BasicTaskCount = 4,
			Communication = Communication.AfterSecondTrick,
		},
		new() // Mission 20
		{
			BasicTaskCount = 2,
			TaskDistribution = TaskDistribution.ByCommanderButHidden,
		},
		new() // Mission 21
		{
			PriorityTaskCount =2,
			BasicTaskCount = 3,
			Communication = Communication.Limited,
		},
		new() // Mission 22
		{
			BasicTaskCount = 1,
			SequentialTaskCount = 4,
		},
		new() // Mission 23
		{
			PriorityTaskCount = 5,
			SpecialCondition = SpecialCondition.TwoTilesMayBeSwapped,
		},
		new() // Mission 24
		{
			BasicTaskCount = 6,
			TaskDistribution = TaskDistribution.ByCommander,
		},
		new() // Mission 25
		{
			SequentialTaskCount = 2,
			BasicTaskCount = 4,
			Communication = Communication.Limited,
		},
		new() // Mission 26
		{
			SpecialCondition = SpecialCondition.TwoTricksWithAValue1Card,
		},
		new() // Mission 27
		{
			BasicTaskCount = 3,
			TaskDistribution = TaskDistribution.ByCommanderButHidden,
		},
		new() // Mission 28
		{
			PriorityTaskCount = 1,
			BasicTaskCount = 4,
			Communication = Communication.AfterSecondTrick,
		},
		new() // Mission 29
		{
			MaxTrickDifference = 1,
		},
		new() // Mission 30
		{
			BasicTaskCount = 3,
			SequentialTaskCount = 3,
			Communication = Communication.AfterFirstTrick,
		},
		new() // Mission 31
		{
			PriorityTaskCount = 3,
			BasicTaskCount = 3,
		},
		new() // Mission 32
		{
			BasicTaskCount = 7,
			TaskDistribution = TaskDistribution.ByCommander,
		},
		new() // Mission 33
		{
			SpecialCondition = SpecialCondition.OneChosenPlayerMustWinExactlyOneTrickWithoutRockets,
		},
		new() // Mission 34
		{
			MaxTrickDifference = 1,
			SpecialCondition = SpecialCondition.CommanderMustWinFirstAndLastTrick,
		},
		new() // Mission 35
		{
			SequentialTaskCount = 3,
			BasicTaskCount = 4,
		},
		new() // Mission 36
		{
			PriorityTaskCount = 2,
			BasicTaskCount = 5,
			TaskDistribution = TaskDistribution.ByCommander,
		},
		new() // Mission 37
		{
			BasicTaskCount = 4,
			TaskDistribution = TaskDistribution.ByCommanderButHidden,
		},
		new() // Mission 38
		{
			BasicTaskCount = 8,
			Communication = Communication.AfterSecondTrick,
		},
		new() // Mission 39
		{
			SequentialTaskCount = 3,
			BasicTaskCount = 5,
			Communication = Communication.Limited,
		},
		new() // Mission 40
		{
			PriorityTaskCount = 3,
			BasicTaskCount = 5,
			SpecialCondition = SpecialCondition.OneTileMayBeMovedToTaskWithoutTile,
		},
		new() // Mission 41
		{
			SpecialCondition = SpecialCondition.OnePlayerMustWinFirstAndLastTrickWithoutRockets,
		},
		new() // Mission 42
		{
			BasicTaskCount = 9,
		},
		new() // Mission 43
		{
			BasicTaskCount = 9,
			TaskDistribution = TaskDistribution.ByCommander,
		},
		new() // Mission 44
		{
			SpecialCondition = SpecialCondition.EachRocketMustWinATrickInAscendingOrder,
		},
		new() // Mission 45
		{
			SequentialTaskCount = 3,
			BasicTaskCount = 6,
		},
		new() // Mission 46
		{
			SpecialCondition = SpecialCondition.PlayerLeftOfPink9WinsAllPinkCards,
		},
		new() // Mission 47
		{
			BasicTaskCount = 10,
		},
		new() // Mission 48
		{
			BasicTaskCount = 2,
			SpecialCondition = SpecialCondition.OmegaTaskMustBeLastTrick,
		},
		new() // Mission 49
		{
			SequentialTaskCount = 3,
			BasicTaskCount = 7,
		},
		new() // Mission 50
		{
			SpecialCondition = SpecialCondition.CommanderChoosesPlayerForOnlyTheFirstFourTricksAndAnotherForOnlyTheLastTrick,
		},
	];

	public int BasicTaskCount { init; get; }
	public int SequentialTaskCount { init; get; }
	public int PriorityTaskCount { init; get; }
	public int MaxTrickDifference { init; get; }
	public Communication Communication { init; get; }
	public SpecialCondition SpecialCondition { init; get; }
	public TaskDistribution TaskDistribution { init; get; }

	public int TotalTaskCount => BasicTaskCount + SequentialTaskCount + PriorityTaskCount;
}
