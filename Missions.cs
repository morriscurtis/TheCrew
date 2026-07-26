namespace TheCrew;

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
	AtMostOneTrickDifference,
	PlayerLeftOfPink9WinsAllPinkCards,
	OmegaTask,
	OmegaTaskMustBeLastTrick,
	TwoTilesMayBeSwapped,
	OneTileMayBeMovedToTaskWithoutTile,
	OnePlayerMustWinFirstAndLastTrickWithoutRockets,
	OneChosenPlayerMustWinExactlyOneTrickWithoutRockets,
	CommanderMustWinFirstAndLastTrickAndAtMostOneTrickDifference,
	CommanderChoosesPlayerForOnlyTheFirstFourTricksAndAnotherForOnlyTheLastTrick,
}

public class Mission()
{
	public static readonly Mission[] AllMissions =
	[
		/* 01 */ BasicTasks(1),
		/* 02 */ BasicTasks(2),
		/* 03 */ PriorityTasks(2),
		/* 04 */ BasicTasks(3),
		/* 05 */ From(SpecialCondition.OneChosenPlayerWinsNoTricks),
		/* 06 */ BasicTasks(1).AndSequentialTasks(2).And(Communication.Limited),
		/* 07 */ BasicTasks(2).And(SpecialCondition.OmegaTask),
		/* 08 */ PriorityTasks(3),
		/* 09 */ From(SpecialCondition.OneTrickWithAValue1Card),
		/* 10 */ BasicTasks(4),
		/* 11 */ BasicTasks(3).AndPriorityTasks(1).And(Communication.OneChosenPlayerDisallowed),
		/* 12 */ BasicTasks(3).And(SpecialCondition.DrawCardFromNeighborAfterFirstTrick),
		/* 13 */ From(SpecialCondition.EachRocketMustWinATrick),
		/* 14 */ BasicTasks(1).AndSequentialTasks(3).And(Communication.Limited),
		/* 15 */ PriorityTasks(4),
		/* 16 */ From(SpecialCondition.NoValue9CardMustWinATrick),
		/* 17 */ BasicTasks(2).And(SpecialCondition.NoValue9CardMustWinATrick),
		/* 18 */ BasicTasks(5).And(Communication.AfterFirstTrick),
		/* 19 */ BasicTasks(4).AndPriorityTasks(1).And(Communication.AfterSecondTrick),
		/* 20 */ BasicTasks(2).And(TaskDistribution.ByCommanderButHidden),
		/* 21 */ BasicTasks(3).AndPriorityTasks(2).And(Communication.Limited),
		/* 22 */ BasicTasks(1).AndSequentialTasks(4),
		/* 23 */ PriorityTasks(5).And(SpecialCondition.TwoTilesMayBeSwapped),
		/* 24 */ BasicTasks(6).And(TaskDistribution.ByCommander),
		/* 25 */ BasicTasks(4).AndSequentialTasks(2).And(Communication.Limited),
		/* 26 */ From(SpecialCondition.TwoTricksWithAValue1Card),
		/* 27 */ BasicTasks(3).And(TaskDistribution.ByCommanderButHidden),
		/* 28 */ BasicTasks(4).AndPriorityTasks(1).And(Communication.AfterSecondTrick),
		/* 29 */ From(SpecialCondition.AtMostOneTrickDifference),
		/* 30 */ BasicTasks(3).AndSequentialTasks(3).And(Communication.AfterFirstTrick),
		/* 31 */ BasicTasks(3).AndPriorityTasks(3),
		/* 32 */ BasicTasks(7).And(TaskDistribution.ByCommander),
		/* 33 */ From(SpecialCondition.OneChosenPlayerMustWinExactlyOneTrickWithoutRockets),
		/* 34 */ From(SpecialCondition.CommanderMustWinFirstAndLastTrickAndAtMostOneTrickDifference),
		/* 35 */ BasicTasks(4).AndSequentialTasks(3),
		/* 36 */ BasicTasks(5).AndPriorityTasks(2).And(TaskDistribution.ByCommander),
		/* 37 */ BasicTasks(4).And(TaskDistribution.ByCommanderButHidden),
		/* 38 */ BasicTasks(8).And(Communication.AfterSecondTrick),
		/* 39 */ BasicTasks(5).AndSequentialTasks(3).And(Communication.Limited),
		/* 40 */ BasicTasks(5).AndPriorityTasks(3).And(SpecialCondition.OneTileMayBeMovedToTaskWithoutTile),
		/* 41 */ From(SpecialCondition.OnePlayerMustWinFirstAndLastTrickWithoutRockets),
		/* 42 */ BasicTasks(9),
		/* 43 */ BasicTasks(9).And(TaskDistribution.ByCommander),
		/* 44 */ From(SpecialCondition.EachRocketMustWinATrickInAscendingOrder),
		/* 45 */ BasicTasks(6).AndSequentialTasks(3),
		/* 46 */ From(SpecialCondition.PlayerLeftOfPink9WinsAllPinkCards),
		/* 47 */ BasicTasks(10),
		/* 48 */ BasicTasks(2).And(SpecialCondition.OmegaTaskMustBeLastTrick),
		/* 49 */ BasicTasks(7).AndSequentialTasks(3),
		/* 50 */ From(SpecialCondition.CommanderChoosesPlayerForOnlyTheFirstFourTricksAndAnotherForOnlyTheLastTrick),
	];

	public int BasicTaskCount;
	public int SequentialTaskCount;
	public int PriorityTaskCount;
	public Communication Communication;
	public SpecialCondition SpecialCondition;
	public TaskDistribution TaskDistribution;

	public static Mission BasicTasks(int count) => new() { BasicTaskCount = count };
	public static Mission SequentialTasks(int count) => new() { SequentialTaskCount = count };
	public static Mission PriorityTasks(int count) => new() { PriorityTaskCount = count };
	public static Mission From(SpecialCondition value) => new() { SpecialCondition = value };

	public Mission AndNormalTasks(int count)
	{
		BasicTaskCount = count;
		return this;
	}
	public Mission AndSequentialTasks(int count)
	{
		SequentialTaskCount = count;
		return this;
	}
	public Mission AndPriorityTasks(int count)
	{
		PriorityTaskCount = count;
		return this;
	}

	public Mission And(SpecialCondition value)
	{
		SpecialCondition = value;
		return this;
	}
	public Mission And(Communication value)
	{
		Communication = value;
		return this;
	}
	public Mission And(TaskDistribution value)
	{
		TaskDistribution = value;
		return this;
	}
}
