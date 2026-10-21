class GameObjectHandler : IGameObjectHandleable
{
    List<GameObject> gameObjects = new();

    List<GameObject> buffer = new();

    Vector2u playArea;

    public Vector2u PlayArea
    {
        get { return playArea; }
        private set { playArea = value; }
    }

    public GameObjectHandler(Vector2u playArea)
    {
        PlayArea = playArea;
        GameObject.SetObjectHandler(this);
        new GameStateHandler();
    }

    public void AddBuffer()
    {
        gameObjects.AddRange(buffer);
        buffer.Clear();
    }

    public List<GameObject> GetAllGameObjects() => gameObjects;

    public void AddToGameHandlerList(GameObject gameObject)
    {
        buffer.Add(gameObject);
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

            if (gameObjects[i] is IDrawable)
            {
                var slop = gameObjects[i] as IDrawable;
                slop.DrawBåt(window);
            }

            if (gameObjects[i] is PhysicalObject)
            {
                var slop = gameObjects[i] as PhysicalObject;
                slop.DebugDraw(window);
            }
        }
    }
}