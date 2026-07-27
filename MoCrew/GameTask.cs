namespace MoCrew;

public struct GameTask(Card card, Player player, int dependencyIndex)
{
	public Card Card = card;
	public Player Player = player;
	public int DependencyIndex = dependencyIndex;
	public bool IsCompleted;
}

public enum DependencyType
{
	NoDependency,
	MustBeCompleted,
	MustNotBeCompleted,
}
