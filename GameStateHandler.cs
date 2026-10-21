class GameStateHandler : GameObject
{
    interface GameState
    {

    }

    public GameStateHandler()
    {
        new MenuHandler();
        new Button(new(200, 200), new(300, 200), StartGame, Color.Cyan)
        {
            text = new Text()
            {
                DisplayedString = "niklas",
                Position = new(200, 200),
                Font = new Font("fonts/saturno.ttf"),
                FillColor = Color.Red,
                OutlineColor = Color.Black,
                CharacterSize = 67,
            }
        };
        new Button(new(700, 200), new(300, 200), () => System.Console.WriteLine("clicked"), Color.Red);
    }

    void ClearList()
    {
        foreach (var o in handler.GetAllGameObjects())
        {
            if (o == this) continue;
            else o.remove = true;
        }
    }

    void StartGame()
    {
        new GameSpawner(handler.PlayArea, 1);
        ClearList();
    }

    void StartAfterGameMenu()
    {
        
    }

    public override void Update(float deltaTime)
    {

    }

    
}