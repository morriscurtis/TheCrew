using MarkusCrew.Game;

Console.WriteLine("Hello, World!");

GameManager manager = new GameManager(1);
int tries = manager.SimulateMission();
Console.WriteLine($"Tries until successful: {tries}");
