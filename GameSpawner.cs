class GameSpawner : GameObject
{
    const string playerDataFilePath = "data/playerData.json";
    const string seraphimDataFilePath = "data/seraphimData.json";
    const string virtueDataFilePath = "data/virtueData.json";
    const string archangelDataFilePath = "data/archangelData.json";

    ControlLayout player1Controls = new(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space);
    ControlLayout player2Controls = new(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl);
    int amountOfPlayers;

    float timeThatHasPassedInSeconds;
    float enemySpawnCooldownInSeconds;
    float StartingSpawnTime = 7;
    float lowestSpawnTime = 1;
    float amountOfChange = 50;

    Vector2u playArea;
    readonly Timer enemySpawnTimer;
    volatile bool enemySpawnReady;
    bool enemySpawnTimerIsOnSchedule;

    SpaceShip playerShip;
    

    public GameSpawner(Vector2u playArea, int amountOfPlayers)
    {
        enemySpawnTimer = new Timer(_ => enemySpawnReady = true, state: null, dueTime: Timeout.Infinite, period: Timeout.Infinite);
        this.playArea = playArea;
        SpawnStartingObjects();
        this.amountOfPlayers = amountOfPlayers;
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
        playerShip = SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath), new(playArea.X / 2, 700), player1Controls);
    }

    void SpawnPlayer2()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath) { sprite = "köttigbild" }, new(playArea.X / 2, 700), player2Controls);
    }

    void PlayerHealthCheck()
    {
        if(playerShip.health <= 0)
        {
            
        }
    }

    public void SpawnStartingObjects()
    {
        SpawnPlayer1();
        // SpawnPlayer2();
        SpawnEnemy(archangelDataFilePath);
        // SpawnEnemy(seraphimDataFilePath);       
        // SpawnEnemy(virtueDataFilePath);
    }

    public override void Update(float deltaTime)
    {
        timeThatHasPassedInSeconds += deltaTime;
        CheckEnemySpawn();
    }


}