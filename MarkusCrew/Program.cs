using MarkusCrew.Game;
using MarkusCrew.Game.Missions;
using MarkusCrew.Game.Missions.Options;
using MarkusCrew.Task;
using Microsoft.Extensions.DependencyInjection;

Console.WriteLine("Hello, World!");

ServiceCollection services = new ServiceCollection();
services.AddTransient<MissionOptions>();
services.AddTransient<IMission, Mission>();
services.AddTransient<ITaskFactory, MarkusCrew.Task.TaskFactory>();
services.AddTransient<GameManager>();

var provider = services.BuildServiceProvider();
GameManager gameManager = provider.GetRequiredService<GameManager>();
var tries = gameManager.SimulateMission();

Console.WriteLine($"Tries until successful: {tries}");
