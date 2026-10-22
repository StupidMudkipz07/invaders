class InGameManager : GameObject, IDrawable, IFloatReturnable
{
    const string playerDataFilePath = "data/playerData.json";
    const string seraphimDataFilePath = "data/seraphimData.json";
    const string virtueDataFilePath = "data/virtueData.json";
    const string archangelDataFilePath = "data/archangelData.json";

    ControlLayout player1Controls = new(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space);
    ControlLayout player2Controls = new(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl);
    int amountOfPlayers;

    float score = 0;
    Text Score;

    public float GetFloat()
    {
        return score;
    }

    float timeThatHasPassedInSeconds;
    float StartingSpawnTime = 7;
    float lowestSpawnTime = 1;
    float amountOfChange = 50;

    Vector2u playArea;
    readonly Timer enemySpawnTimer;
    volatile bool enemySpawnReady;
    bool enemySpawnTimerIsOnSchedule;

    IFloatReturnable playerShipHealth;
    public event EventHandler OnGameEnd;

    public InGameManager(Vector2u playArea, int amountOfPlayers)
    {
        enemySpawnTimer = new Timer(_ => enemySpawnReady = true, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        this.playArea = playArea;
        SpawnStartingObjects();
        this.amountOfPlayers = amountOfPlayers;
        Score = new()
        {
            CharacterSize = 30,
            DisplayedString = $"Score: {MathF.Round(score)}",
            Position = new(20, playArea.Y - 50),
            Font = new Font("fonts/future.ttf"),
            FillColor = Color.Black,
            OutlineColor = Color.White,
            OutlineThickness = 2
        };

    }

    float GetEnemyRespawnTime(float time)
    {
        float output = (StartingSpawnTime - lowestSpawnTime) * MathF.Pow(0.5f, time / amountOfChange) + lowestSpawnTime;
        System.Console.WriteLine($"input: {time} output: {output}");

        return output;
    }

    void CheckEnemySpawn()
    {
        if (enemySpawnReady)
        {
            enemySpawnReady = false;
            enemySpawnTimerIsOnSchedule = false;
            SpawnEnemy(seraphimDataFilePath);
        }

        if (!enemySpawnTimerIsOnSchedule)
        {
            float respawnTime = GetEnemyRespawnTime(timeThatHasPassedInSeconds);
            enemySpawnTimer.Change(dueTime: TimeSpan.FromSeconds(respawnTime), period: Timeout.InfiniteTimeSpan);
            enemySpawnTimerIsOnSchedule = true;
        }
    }

    void SpawnEnemy(string dataFilepath)
    {
        SpaceShipData stats = new SpaceShipData(dataFilepath);
        SpaceShip.SpawnEnemy(stats, new(playArea.X / 2, -stats.size.X / 2));
    }

    void SpawnPlayer1()
    {
        playerShipHealth = SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath), new(playArea.X / 2, 700), player1Controls);
    }

    void SpawnPlayer2()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath) { sprite = "köttigbild" }, new(playArea.X / 2, 700), player2Controls);
    }

    void PlayerHealthCheck()
    {
        if (playerShipHealth.GetFloat() <= 0)
        {
            OnGameEnd.Invoke(this, EventArgs.Empty);
        }
    }

    public void SpawnStartingObjects()
    {
        SpawnPlayer1();
        // SpawnPlayer2();
        SpawnEnemy(archangelDataFilePath);
        // SpawnEnemy(seraphimDataFilePath);       
        // SpawnEnemy(virtueDataFilePath);
        new GuiBar(playerShipHealth, new(40, 40), new(50, 50));
    }

    public override void Update(float deltaTime)
    {
        timeThatHasPassedInSeconds += deltaTime;
        CheckEnemySpawn();
        PlayerHealthCheck();
        score += deltaTime;
    }

    public void Draw(RenderWindow widnow)
    {
        Score.DisplayedString = $"Score: {MathF.Round(score)}";
        widnow.Draw(Score);
    }
}