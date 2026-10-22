class GameStateHandler : GameObject
{
    float GameScore;

    public GameStateHandler()
    {
        StartMainMenu();
        AudioManager.PlayAudio("piano");
    }

    void ClearList()
    {
        foreach (var o in handler.GetAllGameObjects())
        {
            if (o == this) continue;
            else o.remove = true;
        }
    }

    void StartMainMenu()
    {
        ClearList();
        new MenuHandler();
        new Button(new(200, 200), new(300, 200), StartGame, Color.Cyan)
        {
            text = new Text()
            {
                DisplayedString = "PLAY!",
                Position = new(220, 200),
                Font = new Font("fonts/saturno.ttf"),
                FillColor = Color.Red,
                OutlineColor = Color.Green,
                CharacterSize = 45,
                OutlineThickness = 2
            }
        };

        new Button(new(700, 200), new(300, 200), () => Environment.Exit(0), Color.Red)
        {
            text = new Text()
            {
                DisplayedString = "EXIT!",
                Position = new(720, 200),
                Font = new Font("fonts/future.ttf"),
                FillColor = Color.Blue,
                OutlineColor = Color.Yellow,
                CharacterSize = 45,
                OutlineThickness = 2
            }
        };

         new Button(new(350, 550), new(450, 200), () => Environment.Exit(0), Color.Magenta)
        {
            text = new Text()
            {
                DisplayedString = "view highscores",
                Position = new(350, 570),
                Font = new Font("fonts/future.ttf"),
                FillColor = Color.Black,
                OutlineColor = Color.Black,
                CharacterSize = 30,
                OutlineThickness = 2
            }
        };
    }

    void StartGame()
    {
        ClearList();
        InGameManager gameSpawner = new InGameManager(handler.PlayArea, 1);
        gameSpawner.OnGameEnd += (sender, _) =>
        {
            if (sender is IFloatReturnable scoreCounter)
            {
                GameScore = scoreCounter.GetFloat();
                StartAfterGameMenu();
            }
        };
    }

    void StartAfterGameMenu()
    {
        ClearList();
        new HighScoreManager(GameScore);
        
        new MenuHandler();
        
        new Button(new(handler.PlayArea.X / 2 - 100, handler.PlayArea.Y / 2 - 100), new(230, 130), StartGame, Color.Cyan)
        {
            text = new Text()
            {
                DisplayedString = "Play again?",
                Position = new(handler.PlayArea.X / 2 - 100, handler.PlayArea.Y / 2 - 100),
                Font = new Font("fonts/saturno.ttf"),
                FillColor = Color.Red,
                OutlineColor = Color.Black,
                CharacterSize = 35,
                OutlineThickness = 2
            }
        };

        new Button(new(handler.PlayArea.X / 2 - 120, handler.PlayArea.Y / 2 + 200), new(270, 130), StartMainMenu, Color.Green)
        {
            text = new Text()
            {
                DisplayedString = "Main menu",
                Position = new(handler.PlayArea.X / 2 - 100, handler.PlayArea.Y / 2 + 200),
                Font = new Font("fonts/future.ttf"),
                FillColor = Color.Cyan,
                OutlineColor = Color.Black,
                CharacterSize = 35,
                OutlineThickness = 2
            }
        };
    }

    public override void Update(float deltaTime)
    {

    }
}