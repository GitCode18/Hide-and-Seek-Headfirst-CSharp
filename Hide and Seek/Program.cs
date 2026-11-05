using Hide_and_Seek;

while (true)
{
    GameController gameController = new GameController();

    while (!gameController.GameOver)
    {
        Console.WriteLine(gameController.Status);
        Console.WriteLine(gameController.Prompt);
        Console.WriteLine(gameController.ParseInput(Console.ReadLine()));
        Console.WriteLine();
    }

    Console.WriteLine($"You won the game in {gameController.MoveNumber} moves!");
    Console.WriteLine("Press P to play again, any other key to quit.");
    if (Console.ReadKey(true).KeyChar.ToString().ToUpper() != "P") return;
}