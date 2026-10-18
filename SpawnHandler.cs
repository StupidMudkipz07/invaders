class SpawnHandler : ISpawnHandleable
{
    const string playerDataFilePath = "data/playerData.json";
    const string seraphimDataFilePath = "data/seraphimData.json";
    const string virtueDataFilePath = "data/virtueData.json";
    const string archangelDataFilePath = "data/archangelData.json";

    ControlLayout player1Controls = new(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space);
    ControlLayout player2Controls = new(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl);
    Clock clock = new();

    float timeThatHasPassedInSeconds;
    float enemySpawnCooldownInSeconds;
    float StartingSpawnTime = 7;
    float approachingSpawnTime = 1;
    float amountOfChange = 50;


    Vector2u playArea;
    readonly Timer enemySpawnTimer;
    volatile bool enemySpawnReady;
    bool enemySpawnTimerScheduled;

    public SpawnHandler(Vector2u playArea)
    {
        enemySpawnTimer = new Timer(_ => enemySpawnReady = true, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        this.playArea = playArea;
    }

    float GetEnemyRespawnTime(float time)
    {
        float output = (StartingSpawnTime - approachingSpawnTime) * MathF.Pow(0.5f, time / amountOfChange) + approachingSpawnTime;
        System.Console.WriteLine($"input: {time} output: {output}");

        return output;
    }

    void CheckEnemySpawn()
    {
        if (enemySpawnReady)
        {
            enemySpawnReady = false;
            enemySpawnTimerScheduled = false;
            SpawnEnemy(seraphimDataFilePath);
        }

        if (!enemySpawnTimerScheduled)
        {
            float respawnTime = GetEnemyRespawnTime(timeThatHasPassedInSeconds);
            enemySpawnTimer.Change(dueTime: TimeSpan.FromSeconds(respawnTime), period: Timeout.InfiniteTimeSpan);
            enemySpawnTimerScheduled = true;
        }
    }

    void SpawnEnemy(string dataFilepath)
    {
        SpaceShipData stats = new SpaceShipData(dataFilepath);
        SpaceShip.SpawnEnemy(stats, new(playArea.X / 2, -stats.size.X / 2));
    }

    void SpawnPlayer1()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath), new(playArea.X / 2, 700), player1Controls);
    }

    void SpawnPlayer2()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath) { sprite = "köttigbild" }, new(playArea.X / 2, 700), player2Controls);
    }

    public void SpawnStartingObjects()
    {
        SpawnPlayer1();
        // SpawnPlayer2();
        SpawnEnemy(archangelDataFilePath);
        // SpawnEnemy(seraphimDataFilePath);       
        // SpawnEnemy(virtueDataFilePath);
    }

    public void UpdateSpawner()
    {
        timeThatHasPassedInSeconds += clock.Restart().AsSeconds();
        CheckEnemySpawn();
    }
}