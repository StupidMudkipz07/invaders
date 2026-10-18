class GameObjectHandler : IGameObjectHandleable
{
    List<Object> gameObjects = new();

    List<Object> buffer = new();

    public Vector2u playArea;

    ISpawnHandleable spawner;

    public Vector2u PlayArea
    {
        get { return playArea; }
    }

    public GameObjectHandler(Vector2u playArea)
    {
        this.playArea = playArea;
        Object.SetObjectHandler(this);
        spawner = new SpawnHandler(playArea);
        spawner.SpawnStartingObjects();
    }


    public void SpawnObjects()
    {
        spawner.UpdateSpawner();
        AddBuffer();
    }

    public List<Object> GetAllGameObjects() => gameObjects;

    public void AddToGameHandlerList(Object gameObject)
    {
        buffer.Add(gameObject);
    }

    void AddBuffer()
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