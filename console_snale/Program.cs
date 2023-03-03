using console_snale;
StartLogic _startLogic = new StartLogic();
_startLogic.GameBegin();




public class StartLogic{
    
    public void GameBegin()
    {
        ReadGameFile readGameFile = new();
        Game game = new();
        var gameInfo = readGameFile.ReadSettingFile();
        var score = game.Start();
        if (score > gameInfo.HighScore) gameInfo.HighScore = score;
        readGameFile.WriteSettingFile(gameInfo);
        bool closeGame = false;
        while (!closeGame)
        {
            var pressedKey = Console.ReadKey().Key;

            switch (pressedKey)
            {
                case ConsoleKey.R:
                    GameBegin();
                    break;
                case ConsoleKey.Escape:
                    closeGame = true;
                    break;
            }
        }
    }
}
