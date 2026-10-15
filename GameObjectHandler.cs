class GameObjectHandler : IGameObjectHandleable
{
    List<GameObject> gameObjects = new();

    List<GameObject> buffer = new();

    ControlLayout player1Controls = new(Keyboard.Key.W, Keyboard.Key.S, Keyboard.Key.A, Keyboard.Key.D, Keyboard.Key.Space);
    ControlLayout player2Controls = new(Keyboard.Key.Up, Keyboard.Key.Down, Keyboard.Key.Left, Keyboard.Key.Right, Keyboard.Key.RControl);

    const string playerDataFilePath = "data/playerData.json";
    const string seraphimDataFilePath = "data/seraphimData.json";
    const string virtueDataFilePath = "data/virtueData.json";
    const string archangelDataFilePath = "data/archangelData.json";

    public GameObjectHandler()
    {
        GameObject.SetObjectHandler(this);
        SpawnObjects();
    }

    void SpawnEnemy(string dataFilepath)
    {
        SpaceShipData stats = new SpaceShipData(dataFilepath);
        SpaceShip.SpawnEnemy(stats, new(Program.WindowSize.X / 2, -stats.size.X / 2));
    }

    void SpawnPlayer1()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath), new(Program.WindowSize.X / 2, 700), player1Controls);
    }

    void SpawnPlayer2()
    {
        SpaceShip.SpawnPlayer(new SpaceShipData(playerDataFilePath) { sprite = "köttigbild" }, new(Program.WindowSize.X / 2, 700), player2Controls);
    }

    void SpawnObjects()
    {
        SpawnPlayer1();
        //SpawnPlayer2();
        SpawnEnemy(archangelDataFilePath);
        SpawnEnemy(seraphimDataFilePath);
        SpawnEnemy(virtueDataFilePath);
    }


    public List<GameObject> GetAllGameObjects() => gameObjects;

    public void AddToGameHandlerList(GameObject gameObject)
    {
        buffer.Add(gameObject);
    }

    public void AddToGameList(GameObject[] gameObject)
    {
        buffer.AddRange(gameObject);
    }

    public void AddBuffer()
    {
        gameObjects.AddRange(buffer);
        buffer.Clear();
    }

    public void UpdateGameObjects(float deltaTime)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            //först uppdatera alla värden
            gameObjects[i].Update(deltaTime);
            // Console.Write(gameObjects);
        }

        //   Console.Write("\n");

        // tar bort alla objekt efter man har itererat så inte listan förstörs
        gameObjects.RemoveAll(obj => obj.remove == true);
    }

    public void DrawGameObjects(RenderWindow window)
    {
        for (int i = 0; i < gameObjects.Count; i++)
        {
            //kollar så att objektet finns i skärmen innan den ritar det
            if (gameObjects[i].IsInView()) gameObjects[i].Draw(window);
        }
    }

}