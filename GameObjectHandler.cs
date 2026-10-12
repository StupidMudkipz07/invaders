class AngelGameHandler : IGameObjectHandleable
{
    List<GameObject> gameObjects = new();

    List<GameObject> buffer = new();

    IEnemyHandler enemyController;

    public AngelGameHandler()
    {
        
    }

    public List<GameObject> GetAllGameObjects() => gameObjects;

    public void AddToGameList(GameObject gameObject)
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