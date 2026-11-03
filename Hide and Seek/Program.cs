using Hide_and_Seek;

GameController gameController = new GameController();

while (true)
{
    Console.WriteLine(gameController.Status);
    Console.WriteLine(gameController.Prompt);
    Console.WriteLine(gameController.ParseInput(Console.ReadLine()));
    Console.WriteLine();
}